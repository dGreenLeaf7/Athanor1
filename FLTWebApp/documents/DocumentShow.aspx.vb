' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports System.Configuration
Imports Microsoft.Security.Application

Partial Public Class DocumentShow
    Inherits System.Web.UI.Page
#Region "Private Variables"
    Private _fltIdentity As FltIdentity = Nothing

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/NotAuthorized.aspx")
        End If
        Dim documentId As Integer = CInt(Request.QueryString("id"))
        DownloadDocument(documentId)
    End Sub

    Private Sub DownloadDocument(ByVal DocumentId As Integer)

        Try
            Dim documentInfo As DocumentInfo = DocumentInfoManager.GetItem(DocumentId, _fltIdentity)
            Dim bufferSize As Integer = CInt(ConfigurationManager.AppSettings.Get("FileBufferSize"))
            Dim outputBuffer(bufferSize - 1) As Byte
            Dim index As Integer = 0

            Using ms As System.IO.MemoryStream = DocumentManager.GetStream(DocumentId, _fltIdentity)
                Using bReader As New System.IO.BinaryReader(ms)

                    Response.ContentType = "application\octet"
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" & Encoder.HtmlAttributeEncode(documentInfo.DocumentName))

                    Dim retVal As Integer

                    Do While True
                        retVal = bReader.Read(outputBuffer, index, bufferSize)
                        If retVal = 0 Then
                            Exit Do
                        End If
                        If retVal < bufferSize Then
                            ReDim Preserve outputBuffer(retVal - 1)
                            Response.BinaryWrite(outputBuffer)
                            Exit Do
                        End If
                        Response.BinaryWrite(outputBuffer)
                    Loop

                End Using
            End Using
            
            Response.Flush()
            Response.End()

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Private Sub ProcessException(ByVal ex As Exception)

    End Sub

End Class