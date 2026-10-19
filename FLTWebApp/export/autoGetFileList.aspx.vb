' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System.IO
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL
Imports Contoso.Utilities

Partial Public Class autoGetFileList
    Inherits System.Web.UI.Page

    Private _fltIdentity As FltIdentity = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        _fltIdentity = Session("fltIdentity")

        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            If AuthenticateUser(Me, Session, Request, Response) Then
                _fltIdentity = Session("fltIdentity")
            End If
        End If
        If Not (FltExportManager.CanView(_fltIdentity)) Then
            GenerateError("User is not authorized", Response)
        Else
            SendFileList()
        End If

    End Sub

    Private Sub SendFileList()

        Dim fileElementString As String = String.Empty

        Try
            CleanseDownloadFolder(GetPhysicalDownloadDirectoryPath(Server))
            fileElementString = GetDownloadFileElements()

        Catch ex As Exception
            GenerateError(ex.Message, Response)
            Exit Sub
        End Try

        Response.Clear()
        Dim sb As New StringBuilder
        sb.Append("<?xml version='1.0' encoding='UTF-8'?>")
        sb.Append("<files>")
        sb.Append(fileElementString)
        sb.Append("</files>")
        Response.Write(sb.ToString)
        Response.End()

    End Sub

    Private Function GetDownloadFileElements() As String

        Dim sb As New StringBuilder

        Try

            Dim directoryInfo As New IO.DirectoryInfo(GetPhysicalDownloadDirectoryPath(Server))
            Dim fileInfosArray As IO.FileInfo() = directoryInfo.GetFiles("*.*")
            If (fileInfosArray.Length > 0) Then
                For Each thisFileInfo As FileInfo In fileInfosArray
                    sb.Append("<file>")
                    sb.Append("<name>")
                    sb.Append(thisFileInfo.Name)
                    sb.Append("</name>")
                    sb.Append("<size>")
                    sb.Append(thisFileInfo.Length)
                    sb.Append("</size>")
                    sb.Append("<utcDateTimeCreated>")
                    sb.Append(thisFileInfo.CreationTimeUtc)
                    sb.Append("</utcDateTimeCreated>")
                    sb.Append("</file>")
                Next
            End If

        Catch ex As Exception
            Logger.Write(ex.Message)
            Throw New Exception("Application error occurred")
        End Try

        Return sb.ToString

    End Function

    Private Shared Sub CleanseDownloadFolder(ByVal directoryPath As String)
        Const ageInHours As Integer = 24
        Dim directoryInfo As New DirectoryInfo(directoryPath)
        Dim fileInfosArray As IO.FileInfo() = directoryInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly)
        For Each thisFileInfo As IO.FileInfo In fileInfosArray
            If (thisFileInfo.CreationTimeUtc.AddHours(ageInHours) < Date.UtcNow) Then
                thisFileInfo.Delete()
            End If
        Next
    End Sub

End Class