' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System.IO
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL
Imports Contoso.Utilities

Partial Public Class autoFetchFile
    Inherits System.Web.UI.Page

    Private _fltIdentity As FltIdentity = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim batchLoginMode As String = ConfigurationManager.AppSettings("BatchLoginMode").ToLower

        _fltIdentity = Session("fltIdentity")

        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            If AuthenticateUser(Me, Session, Request, Response) Then
                _fltIdentity = Session("fltIdentity")
            End If
        End If

        If Not (FltExportManager.CanView(_fltIdentity)) Then
            GenerateError("User is not authorized", Response)
        Else
            Dim fileName As String = String.Empty
            If batchLoginMode = "both" Or batchLoginMode = "get" Then
                fileName = Request.QueryString("filename")
            End If
            If (batchLoginMode = "both" And fileName = Nothing) Or batchLoginMode = "post" Then
                fileName = Request.Form("filename")
            End If
            If fileName = Nothing Or fileName.Length = 0 Then
                GenerateError("No file requested", Response)
            Else
                Dim directoryInfo As New DirectoryInfo(GetPhysicalDownloadDirectoryPath(Server))
                Dim fileInfoArray As IO.FileInfo() = directoryInfo.GetFiles(fileName, SearchOption.TopDirectoryOnly)
                If Not fileInfoArray.Length = 1 Then
                    GenerateError("File not found", Response)
                Else
                    SendFile(fileInfoArray(0))
                End If
            End If
        End If

    End Sub

    Private Sub SendFile(ByVal File As FileInfo)

        Dim bufferSize As Integer = CInt(ConfigurationManager.AppSettings.Get("FileBufferSize"))

        Response.Clear()
        Response.AddHeader("Content-Length", File.Length.ToString())
        Response.ContentType = "text/xml"
        Response.BufferOutput = True

        Using fs As System.IO.FileStream = New System.IO.FileStream(File.FullName, IO.FileMode.Open, IO.FileAccess.Read)
            Dim buffer(bufferSize - 1) As Byte
            Dim offset As Integer = 0
            While (fs.Read(buffer, offset, bufferSize) > 0)
                Response.OutputStream.Write(buffer, offset, bufferSize)
            End While
        End Using

        Response.End()

    End Sub

End Class