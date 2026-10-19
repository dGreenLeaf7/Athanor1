' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports Contoso.Utilities
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Configuration

Module autoUtilityProcs

    Private Const _downloadDirectoryPath As String = "~/download"

    Friend Function AuthenticateUser(ByVal WebPage As Page, ByVal Session As HttpSessionState, ByVal Request As HttpRequest, ByVal Response As HttpResponse) As Boolean

        Dim userIsAuthenticated As Boolean = False

        Dim batchLoginMode As String = ConfigurationManager.AppSettings("BatchLoginMode").ToLower

        Session("fltIdentity") = Nothing

        Dim fltIdentity As FltIdentity = Nothing
        Dim userName As String = String.Empty
        Dim password As String = String.Empty

        Select Case batchLoginMode
            Case "get"
                userName = Request.QueryString("username")
                password = Request.QueryString("password")
            Case "post"
                userName = Request.Form("username")
                password = Request.Form("password")
            Case "both"
                userName = String.Empty & Request.QueryString("userName")
                password = String.Empty & Request.QueryString("password")
                If userName = String.Empty Then
                    userName = Request.Form("username")
                    password = Request.Form("password")
                End If
            Case "else"
                InvalidLogin("Incorrect value in server variable BatchLoginMode", Response)
        End Select

        If userName = Nothing Or password = Nothing Then
            InvalidLogin("Missing username/password", Response)
            Return False
        End If

        Try
            fltIdentity = FltIdentityManager.AuthenticateUserLogin(userName, password)
            If fltIdentity Is Nothing Then
                InvalidLogin("Invalid Login for User '" & userName & "'", Response)
            Else
                Session("fltIdentity") = fltIdentity
                userIsAuthenticated = True
            End If
        Catch ex As Exception
            InvalidLogin(ex.Message, Response)
        End Try

        Return userIsAuthenticated

    End Function

    Friend Sub GenerateError(ByVal ErrorMessage As String, ByVal response As HttpResponse)

        Logger.Write(ErrorMessage)

        Response.Clear()
        Dim sb As New StringBuilder
        sb.Append("<?xml version='1.0' encoding='UTF-8'?>")
        sb.Append("<error>")
        sb.Append(ErrorMessage)
        sb.Append("</error>")
        Response.Write(sb.ToString)
        Response.End()

    End Sub

    Friend Function GetPhysicalDownloadDirectoryPath(ByVal Server As HttpServerUtility) As String
        Return Server.MapPath(_downloadDirectoryPath)
    End Function

    Private Sub InvalidLogin(ByVal errorMessage As String, ByVal Response As HttpResponse)

        Logger.Write(errorMessage)

        Response.Clear()
        Dim sb As New StringBuilder
        sb.Append("<?xml version='1.0' encoding='UTF-8'?>")
        sb.Append("<error>Invalid Login</error>")
        Response.Write(sb.ToString)
        Response.End()

    End Sub

End Module
