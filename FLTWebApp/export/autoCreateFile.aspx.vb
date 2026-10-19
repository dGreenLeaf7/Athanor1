' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports Contoso.Utilities

Partial Public Class autoCreateFile
    Inherits System.Web.UI.Page

    Private _fltIdentity As FltIdentity = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Server.ScriptTimeout = 300
        Dim batchLoginMode As String = ConfigurationManager.AppSettings("BatchLoginMode").ToLower

        _fltIdentity = CType(Session("fltIdentity"), FltIdentity)
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            If AuthenticateUser(Me, Session, Request, Response) Then
                _fltIdentity = Session("fltIdentity")
            End If
        End If

        If Not (FltExportManager.CanView(_fltIdentity)) Then
            GenerateError("User is not authorized", Response)
        Else
            Dim changesSinceDate As String = String.Empty
            If batchLoginMode = "both" Or batchLoginMode = "get" Then
                changesSinceDate = String.Empty & Request.QueryString("date")
            End If
            If (batchLoginMode = "both" And changesSinceDate.Length = 0) Or batchLoginMode = "post" Then
                changesSinceDate = Request.Form("date")
            End If
            If changesSinceDate = Nothing Then
                GenerateError("Date is missing/invalid", Response)
            Else
                If changesSinceDate.Length = 0 Then
                    GenerateError("Date is missing/invalid", Response)
                Else
                    If Not IsDate(changesSinceDate) Then
                        GenerateError("Date is Invalid", Response)
                    Else
                        CreateFile(changesSinceDate)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub CreateFile(ByVal ChangesSinceDate As DateTime)
        FltExportManager.CreateDownloadFile(GetPhysicalDownloadDirectoryPath(Server), ChangesSinceDate, _fltIdentity)
    End Sub

End Class