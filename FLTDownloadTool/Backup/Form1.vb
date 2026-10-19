' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Net
Imports System.IO
Imports System.Text

Public Class Form1

    Private strLocalPath As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim xmlString As String = "<request><loginName>devuser</loginName><password>REDACTED</password><action>getFile</action></request>"
        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(xmlString)

        Dim loginUrl As String = "https://test.download.flt.contoso.example"

        Dim webReq As WebRequest = WebRequest.Create(loginUrl)
        webReq.Credentials = CredentialCache.DefaultCredentials
        webReq.Method = "POST"
        webReq.ContentLength = byteArray.Length
        webReq.ContentType = "application/x-www-form-urlencoded"
        Dim requestStream As Stream = webReq.GetRequestStream()
        requestStream.Write(byteArray, 0, byteArray.Length)
        requestStream.Close()

        Dim webResponse As WebResponse = webReq.GetResponse()

        Dim responseStream As Stream = webResponse.GetResponseStream
        Dim reader As New StreamReader(responseStream)

        Dim file As System.IO.FileStream = New FileStream(strLocalPath, FileMode.OpenOrCreate, FileAccess.Write)
        Dim writer As New StreamWriter(file)

        Dim bufferSize As Integer = 4096
        Dim buffer(bufferSize - 1) As Char
        Dim index = 0

        While (reader.Read(buffer, index, bufferSize) > 0)
            writer.Write(buffer, index, bufferSize)
        End While

        reader.Close()
        writer.Close()

        file.Close()

    End Sub

End Class
