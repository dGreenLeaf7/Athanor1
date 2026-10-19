' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports System.Configuration

Partial Public Class GetExportFile
    Inherits System.Web.UI.Page

#Region "Private Variables"

    Private _fltIdentity As FltIdentity = Nothing

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _fltIdentity = Session("fltIdentity")
            If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
                Throw New Exception("User is not authorized")
            End If
            Dim fileName As String = CStr(Request.QueryString("fid"))
            Dim downloadMethod As String = CStr(Request.QueryString("dm"))
            DownloadDocument(fileName, downloadMethod)
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Private Sub DownloadDocument(ByVal fileName As String, ByVal downLoadMethod As String)

        Try
            If fileName.Length = 0 Then
                Throw New Exception("File Name not provided")
            End If

            Dim path As String = Server.MapPath("~\download\" & fileName)
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)

            If file.Exists Then
                Select Case downLoadMethod
                    Case "w"
                        WriteFileToClient(file)
                    Case "s2"
                        BinaryWriteFileToClient(file)
                    Case Else
                        StreamFileToClient(file)
                End Select
            Else
                Throw New Exception("File '" & fileName & "' does not exist")
            End If

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Private Sub StreamFileToClient(ByVal file As System.IO.FileInfo)

        Dim bufferSize As Integer = CInt(ConfigurationManager.AppSettings.Get("FileBufferSize"))

        Response.Clear()
        Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
        Response.AddHeader("Content-Length", file.Length.ToString())
        Response.ContentType = "text.xml"
        Response.BufferOutput = True

        Using fs As System.IO.FileStream = New System.IO.FileStream(file.FullName, IO.FileMode.Open, IO.FileAccess.Read)
            Dim buffer(bufferSize - 1) As Byte
            Dim offset As Integer = 0
            While (fs.Read(buffer, offset, bufferSize) > 0)
                Response.OutputStream.Write(buffer, offset, bufferSize)
            End While
        End Using

        Response.End()

    End Sub

    Private Sub BinaryWriteFileToClient(ByVal file As System.IO.FileInfo)
        Dim iStream As System.IO.Stream
        Dim length As Integer
        Dim bufferSize As Integer = CInt(ConfigurationManager.AppSettings.Get("FileBufferSize"))
        Dim buffer(bufferSize) As Byte
        Dim dataToRead As Long = 0
        iStream = New System.IO.FileStream(file.FullName, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)

        Try

            dataToRead = iStream.Length

            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            Response.ContentType = "text/xml"

            While dataToRead > 0
                If Response.IsClientConnected = True Then
                    length = iStream.Read(buffer, 0, bufferSize)
                    Response.OutputStream.Write(buffer, 0, length)

                    Response.Flush()

                    ReDim buffer(bufferSize)

                    dataToRead = dataToRead - length

                Else
                    dataToRead = -1
                End If
            End While

        Catch ex As Exception
            ProcessException(ex)
        Finally
            If IsNothing(iStream) = False Then
                iStream.Close()
            End If
            Response.Close()
        End Try
    End Sub

    Private Sub WriteFileToClient(ByVal file As System.IO.FileInfo)
        Response.Clear()
        Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
        Response.AddHeader("Content-Length", file.Length.ToString())
        Response.ContentType = "text/xml"
        Response.WriteFile(file.FullName)
        Response.Flush()
        Response.End()
    End Sub

    Private Sub ClearErrorFields()
        Me.ErrorMessageLabel.Text = String.Empty
        Me.ErrorMessageLabel.Visible = False
        Me.SuccessMessageLabel.Text = String.Empty
        Me.SuccessMessageLabel.Visible = False
    End Sub

    Private Sub ProcessException(ByVal ex As Exception)
        Me.ErrorMessageLabel.Text = ex.Message
        Me.ErrorMessageLabel.Visible = True
    End Sub

End Class