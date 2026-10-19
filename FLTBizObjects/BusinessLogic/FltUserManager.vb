' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports System.Configuration
Imports Contoso.Utilities
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class FltUserManager
		
        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList() As FltUserList
            Dim totRows As Integer = 0
            Return GetList(1, 0, -1, -1, totRows)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As FltUserList
            Try
                Return FltUserDB.GetList(IsActiveFilter, IsDeletedFilter, RowStart, RowCount, TotalRows)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
		
		<DataObjectMethod(DataObjectMethodType.Select, False)> _
		Public Shared Function GetItem(ByVal UserName As String) As FltUser
			Try
				Return FltUserDB.GetItem(UserName)
			Catch ex As Exception
				Throw New ApplicationException(ex.Message)
			End Try
		End Function
		
        Public Shared Sub Save(ByVal FltUser As FltUser, ByVal FltIdentity As FltIdentity)
            If ((FltUser.ConcurrencyId = Nothing) Or (FltUser.ConcurrencyId = String.Empty) Or (FltUser.ConcurrencyId = "0")) Then
                Insert(FltUser, FltIdentity)
            Else
                Update(FltUser, FltIdentity)
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal FltUser As FltUser, ByVal FltIdentity As FltIdentity)
            If FltUser.Validate() Then
                FltUser.StampUserName = IIf(FltIdentity.UserName = String.Empty, "Password Change", FltIdentity.UserName)
                Try
                    FltUserDB.Update(FltUser)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Sub Insert(ByVal FltUser As FltUser, ByVal FltIdentity As FltIdentity)
            Dim NewPassword As String = fltPassword.GeneratePassword()
            LoadPasswordToObject(FltUser, NewPassword)
            If FltUser.Validate() Then
                FltUser.StampUserName = FltIdentity.UserName
                Try
                    FltUserDB.Insert(FltUser)
                    SendPasswordEmail(FltUser.Email, FltUser.UserName, NewPassword)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal FltUser As FltUser, ByVal FltIdentity As FltIdentity)
            Try
                FltUserDB.Delete(FltUser.UserName, FltIdentity.UserName)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Sub

        Public Shared Sub UnlockUserAccount(ByVal FltUser As FltUser, ByVal FltIdentity As FltIdentity)
            FltUser.IsLockedOut = False
            FltUser.FailedPasswordAttemptCount = 0
            Update(FltUser, FltIdentity)
        End Sub

        Public Shared Sub ChangePassword(ByVal FltUser As FltUser, ByVal OldPassword As String, ByVal NewPassword As String, ByVal NewPasswordConfirm As String, ByVal FltIdentity As FltIdentity)
            Try
                If FltUser.Password <> fltPassword.CreatePasswordHash(OldPassword, FltUser.PasswordSalt) Then
                    Throw New Exception("Old Password not valid!")
                ElseIf NewPassword <> NewPasswordConfirm Then
                    Throw New Exception("Confirm New Password does not match New Password value!")
                Else
                    Try
                        LoadPasswordToObject(FltUser, NewPassword)
                        Update(FltUser, FltIdentity)
                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    End Try
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub GenerateNewPassword(ByVal FltUser As FltUser, ByVal FltIdentity As FltIdentity)
            Try
                Dim NewPassword As String = fltPassword.GeneratePassword()
                LoadPasswordToObject(FltUser, NewPassword)
                Update(FltUser, FltIdentity)
                SendPasswordEmail(FltUser.Email, FltUser.UserName, NewPassword)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub LoadPasswordToObject(ByVal FltUser As FltUser, ByVal NewPassword As String)
            Dim NewSalt As String = fltPassword.CreatePasswordSalt()
            FltUser.Password = fltPassword.CreatePasswordHash(NewPassword, NewSalt)
            FltUser.PasswordSalt = NewSalt
        End Sub

        Private Shared Sub SendPasswordEmail(ByVal RecipientEmail As String, ByVal UserName As String, ByVal NewPassword As String)

            Dim SenderEmailAddress As String = ConfigurationManager.AppSettings.Get("WebmasterEmailAddress")
            Dim EmailServerName As String = ConfigurationManager.AppSettings.Get("SMTPServer")
            Dim EmailSubject As String = "Welcome to the " & ConfigurationManager.AppSettings.Get("SysName")
            Dim EmailBody As String

            Dim sb As New System.Text.StringBuilder
            sb.Append("Welcome to the ")
            sb.AppendLine(ConfigurationManager.AppSettings.Get("SysName"))
            sb.AppendLine()
            sb.AppendLine("You can access the system by opening a web browser and navigating to the address below")
            sb.AppendLine()
            sb.AppendLine(ConfigurationManager.AppSettings.Get("LoginURL"))

            sb.AppendLine()
            sb.Append("Your user name: ")
            sb.AppendLine(UserName)
            sb.Append("Your password:  ")
            sb.AppendLine(NewPassword)
            sb.AppendLine()
            sb.AppendLine("It is highly recommended that you change your password to a new value.")
            sb.AppendLine("To do so, log into the application and select the 'User Profile/Change Password' option.")
            sb.AppendLine()
            sb.Append("The " & ConfigurationManager.AppSettings.Get("appPrefix"))
            sb.AppendLine(" User Instructions can be viewed in PDF format from the following link.")
            sb.AppendLine()
            sb.Append("http://")
            sb.Append(ConfigurationManager.AppSettings.Get("appPrefix"))
            sb.AppendLine(".contoso.example/userguide.pdf")

            EmailBody = sb.ToString

            Dim EmailServer As New System.Net.Mail.SmtpClient(EmailServerName)
            Try
                EmailServer.Send(SenderEmailAddress, RecipientEmail, EmailSubject, EmailBody)

            Catch ex As Exception
                Logger.Write(ex)
            End Try
            
        End Sub

    End Class

End Namespace
