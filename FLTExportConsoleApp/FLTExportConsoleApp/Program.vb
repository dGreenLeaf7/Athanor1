' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System.Configuration
Imports FLTBizObjects
Imports Contoso.Utilities


Module Program

    Sub Main()

        Console.Title = "FLT Export Console"
        Console.WriteLine("+------------------------------+")
        Console.WriteLine("| Begin FLT Export Console App |")
        Console.WriteLine("+------------------------------+")
        Console.WriteLine(String.Empty)

        Try
            Logger.Initialize()
            Logger.Write("Begin FLT Export Console App")

            FLTBizObjects.FLT.BL.FltExportManager.CreateDownloadFile(GetDownloadPath(), GetChangesSinceDate(), GetFLTIdentity())

        Catch ex As Exception
            Console.WriteLine("Error in FLT Export Console App")
            Console.WriteLine("Error Message: " & ex.Message)
            Logger.Write("Error in FLT Export Console App")
            Logger.Write(ex)
        End Try
        Console.WriteLine("+----------------------------+")
        Console.WriteLine("| End FLT Export Console App |")
        Console.WriteLine("+----------------------------+")
        Logger.Write("End FLT Export Console App")

    End Sub

    Function GetFLTIdentity() As FLT.BO.FltIdentity

        Dim result = CredentialHelper.GetCredential("FLT.Export.Auth")

        Dim fltIdentity As New FLT.BO.FltIdentity()

        fltIdentity = FLTBizObjects.FLT.BL.FltIdentityManager.AuthenticateUserLogin(result.Username, result.Password)

        Return fltIdentity

    End Function

    Private Function GetDownloadPath() As String

        Dim downloadPath As String = ConfigurationManager.AppSettings("DownloadPath").ToString()

        Return downloadPath

    End Function

    Private Function GetChangesSinceDate() As String

        Dim nbrOfDaysHistory As String = ConfigurationManager.AppSettings("NbrOfDaysHistory").ToString()

        Dim nbr As Integer

        If Integer.TryParse(nbrOfDaysHistory, nbr) Then

        End If

        Dim today As DateTime = DateTime.Now()

        Dim ChangesSinceDate As DateTime = today.AddDays(-1 * nbr)

        Return ChangesSinceDate

    End Function

End Module
