' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal
Imports Contoso.Utilities

Namespace FLT.BL

    Public Enum fltUserAction
        CanView = 0
        CanEdit = 1
    End Enum

    Public Class FltIdentityManager

        Public Shared Function AuthenticateUserLogin(ByVal LoginName As String, ByVal Password As String) As FltIdentity

            Dim Lockout_Duration As Integer = AppConfiguration.Lockout_Duration
            Dim MaximumLoginFailuresAllowed As Integer = AppConfiguration.MaximumLoginFailuresAllowed

            Dim fltUser As FltUser = FltUserDB.GetItem(LoginName)

            If fltUser Is Nothing Then
                Logger.Write("Failed Login Attempt - unable to retrieve fltUser for Login Name '" & LoginName & "'.")
                Throw New Exception("Failed Login Attempt - either Username or Password is incorrect")
            End If
            If fltUser.IsDeleted = True Then
                Logger.Write("Failed Login Attempt - fltUser '" & LoginName & "' is marked as deleted.")
                Throw New Exception("Failed Login Attempt - either Username or Password is incorrect")
            End If
            If fltUser.IsActive = False Then
                Logger.Write("Failed Login Attempt - fltUser '" & LoginName & "' is not marked as active.")
                Throw New Exception("Failed Login Attempt - either Username or Password is incorrect")
            End If
            If (fltUser.IsLockedOut) And (DateDiff(DateInterval.Minute, fltUser.UtcFailedPasswordAttemptWindowStart, Date.UtcNow) < Lockout_Duration) Then
                Logger.Write("Failed Login Attempt - fltUser '" & LoginName & "' is locked out.")
                Throw New Exception("User account is locked out - try again later")
            Else
                If fltUser.IsLockedOut Then
                    fltUser.IsLockedOut = False
                    fltUser.FailedPasswordAttemptCount = 0
                    FltUserDB.Update(fltUser)
                    fltUser = FltUserDB.GetItem(LoginName)
                End If
            End If

            Dim hashedPassword = fltPassword.CreatePasswordHash(Password, fltUser.PasswordSalt)
            If fltUser.Password <> hashedPassword Then
                If fltUser.FailedPasswordAttemptCount < (MaximumLoginFailuresAllowed - 1) Then
                    fltUser.FailedPasswordAttemptCount = fltUser.FailedPasswordAttemptCount + 1
                    FltUserDB.Update(fltUser)
                    Logger.Write("Failed Login Attempt - invalid password for fltUser '" & LoginName & "'.")
                    Throw New Exception("Failed Login Attempt - either Username or Password is incorrect")
                Else
                    fltUser.IsLockedOut = True
                    fltUser.UtcFailedPasswordAttemptWindowStart = Date.UtcNow
                    FltUserDB.Update(fltUser)
                    Logger.Write("Failed Login Attempt - fltUser '" & LoginName & "' has exceeded maximum allowed login attempts and is now locked out.")
                    Throw New Exception("Login failures exceeded maximum allowed attempts - user account is locked out")
                End If
            End If

            Dim newFltIdentity As FltIdentity = fltIdentityDB.AuthenticateUserLogin(LoginName, hashedPassword)
            fltIdentityDB.FetchUserProgramPermissions(newFltIdentity)

            fltUser = FltUserDB.GetItem(LoginName)
            fltUser.IsLockedOut = False
            fltUser.FailedPasswordAttemptCount = 0
            FltUserManager.Update(fltUser, newFltIdentity)

            Logger.Write("INFORMATIONAL - fltUser '" & newFltIdentity.UserName & "' has logged in.")
            Return newFltIdentity

        End Function

        Public Shared Function LogOut(ByVal FltIdentity As FltIdentity) As FltIdentity
            Logger.Write("INFORMATIONAL - fltUser '" & FltIdentity.UserName & "' has logged out.")
            Return New FltIdentity()
        End Function

        Public Shared Sub ChangeCurrentProgram(ByVal FltIdentity As FltIdentity, ByVal NewProgId As Integer)
            If FltIdentity Is Nothing Then
                Throw New Exception("FltIdentity is nothing")
            End If
            FltIdentity.CurrentProgId = NewProgId
            For Each thisUserProgram As FltIdentity.UserProgram In FltIdentity.UserProgramsList
                If thisUserProgram.ProgId = NewProgId Then
                    FltIdentity.ProgDisplayName = thisUserProgram.ProgName
                    Exit For
                End If
            Next
            fltIdentityDB.FetchUserProgramPermissions(FltIdentity)
        End Sub

        Public Shared Function AuthorizeUserAction(ByVal FltIdentity As FltIdentity, ByVal thisModuleName As String, ByVal thisAction As fltUserAction) As Boolean
            If FltIdentity Is Nothing Then
                Throw New Exception("FltIdentity is nothing")
            End If
            Dim canDo As Boolean = False
            If FltIdentity.IsSuperUser Then
                canDo = True
            ElseIf FltIdentity.IsCurrentProgramManager And thisModuleName.ToLower <> "security" And thisModuleName.ToLower <> "appconfig" Then
                canDo = True
            Else
                For Each thisUserModulePermission As FltIdentity.UserModulePermissions In FltIdentity.CurrentModulePermissionsList
                    With thisUserModulePermission
                        If .ModuleName = thisModuleName Then
                            If thisAction = fltUserAction.CanEdit And .CanEdit Then
                                canDo = True
                                Exit For
                            End If
                            If thisAction = fltUserAction.CanView And .CanView Then
                                canDo = True
                                Exit For
                            End If
                        End If
                    End With
                Next
            End If
            Return canDo
        End Function

        Private Sub New()

        End Sub

    End Class

End Namespace
