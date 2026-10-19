' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System.Net
Imports System.IO
Imports System.Configuration
Imports System.Text
Imports System.Web
Imports System.Xml
Imports System.Collections.Generic

#Region "Data Objects"

Public Class DownloadFile

    Private _fileName As String
    Private _fileSize As Integer
    Private _utcDateCreated As Date

    Public Property FileName() As String
        Get
            Return _fileName
        End Get
        Set(ByVal value As String)
            _fileName = value
        End Set
    End Property

    Public Property FileSize() As Integer
        Get
            Return _fileSize
        End Get
        Set(ByVal value As Integer)
            _fileSize = value
        End Set
    End Property

    Public Property UtcDateCreated() As Date
        Get
            Return _utcDateCreated
        End Get
        Set(ByVal value As Date)
            _utcDateCreated = value
        End Set
    End Property

    Public Sub New()
        _fileName = String.Empty
        _fileSize = 0
    End Sub

End Class

Public Class DownloadFileList
    Inherits List(Of DownloadFile)

    Public Sub New()

    End Sub

End Class

#End Region

Public Class FltExportManager

#Region "Private Variables"

    Private _userName As String = String.Empty
    Private _password As String = String.Empty

#End Region

#Region "Public Properties"

    Public WriteOnly Property UserName() As String
        Set(ByVal value As String)
            _userName = value
        End Set
    End Property

    Public WriteOnly Property Password() As String
        Set(ByVal value As String)
            _password = value
        End Set
    End Property

#End Region

#Region "Public Methods"

    Public Function GetFileList() As DownloadFileList
        Return GetFileList(_userName, _password)
    End Function

    Public Function GetFileList(ByVal UserName As String, ByVal Password As String) As DownloadFileList
        Dim downloadFileList As New DownloadFileList
        Me.UserName = UserName
        Me.Password = Password
        Dim postData As String = "UserName=" & UserName & "&Password=" & Password
        Dim uri As String = ConfigurationManager.AppSettings("getFileListURI").ToString
        Dim responseData As String = String.Empty
        Try
            Dim method As String = ConfigurationManager.AppSettings("method").ToString
            responseData = WRequest(uri, method, postData)
            Dim xmlDoc As New XmlDocument
            xmlDoc.LoadXml(responseData)
            Dim errorElement As XmlElement = xmlDoc.GetElementsByTagName("error").Item(0)
            If Not (errorElement Is Nothing) Then
                Throw New Exception(errorElement.InnerText)
            End If
            Dim xmlFilesElement As XmlElement = xmlDoc.GetElementsByTagName("files").Item(0)
            If Not xmlFilesElement Is Nothing Then
                For Each thisChildElement As XmlElement In xmlFilesElement.ChildNodes
                    If thisChildElement.Name = "file" Then
                        Dim downloadFile As New DownloadFile
                        downloadFile.FileName = thisChildElement.GetElementsByTagName("name").Item(0).InnerText
                        downloadFile.FileSize = CInt(thisChildElement.GetElementsByTagName("size").Item(0).InnerText)
                        downloadFile.UtcDateCreated = CDate(thisChildElement.GetElementsByTagName("utcDateTimeCreated").Item(0).InnerText)
                        downloadFileList.Add(downloadFile)
                    End If
                Next
            Else
                Throw New Exception("No files return")
            End If
        Catch ex As Exception
            Throw New Exception("An error occurred:" & ex.Message)
        End Try
        Return downloadFileList
    End Function

    Public Function CreateFile(ByVal ChangesSinceDate As Date) As String
        Return CreateFile(_userName, _password, ChangesSinceDate)
    End Function

    Public Function CreateFile(ByVal UserName As String, ByVal Password As String, ByVal ChangesSinceDate As Date) As String
        Me.UserName = UserName
        Me.Password = Password
        Dim postData As String = "UserName=" & UserName & "&Password=" & Password & "&Date=" & Format(ChangesSinceDate, "dd-MMM-yyyy")
        Dim uri As String = ConfigurationManager.AppSettings("createFileURI").ToString
        Dim responseData As String = String.Empty
        Try
            Dim method As String = ConfigurationManager.AppSettings("method").ToString
            responseData = WRequest(uri, method, postData)
        Catch ex As System.TimeoutException
            Throw New System.TimeoutException(ex.Message, ex.InnerException)
        Catch ex As Exception
            Throw New Exception("An error occurred:" & ex.Message)
        End Try
        Return responseData
    End Function

    Public Function FetchFile(ByVal FileName As String) As String
        Return FetchFile(_userName, _password, FileName)
    End Function

    Public Function FetchFile(ByVal UserName As String, ByVal Password As String, ByVal FileName As String) As String
        Me.UserName = UserName
        Me.Password = Password
        Dim uri As String = ConfigurationManager.AppSettings("fetchFileURI").ToString
        Dim responseData As String = String.Empty
        Dim postData As String = "UserName=" & UserName & "&Password=" & Password & "&filename=" & FileName
        Try
            Dim method As String = ConfigurationManager.AppSettings("method").ToString
            responseData = WRequest(uri, method, postData)
        Catch ex As Exception
            Throw New Exception("An error occurred:" & ex.Message)
        End Try
        Return responseData
    End Function

    Public Sub New()
        _userName = String.Empty
        _password = String.Empty
    End Sub

#End Region

#Region "Private Functions and Subroutines"

    Private Function WRequest(ByVal URL As String, ByVal Method As String, ByVal PostData As String) As String
        Dim responseData As String = String.Empty
        Try
            If Method.ToUpper = "GET" Then
                URL += "?"
                URL += PostData
            End If
            Dim request As HttpWebRequest = WebRequest.Create(URL)
            request.Accept = "*/*"
            request.AllowAutoRedirect = True
            request.UserAgent = "http_requestor/0.1"
            request.Timeout = 60000
            request.Method = Method.ToUpper
            If Method.ToUpper = "POST" Then
                request.ContentType = "application/x-www-form-urlencoded"
                Dim encoding As New ASCIIEncoding
                Dim postByteArray() As Byte = encoding.GetBytes(PostData)
                request.ContentLength = postByteArray.Length
                Dim postStream As Stream = request.GetRequestStream
                postStream.Write(postByteArray, 0, postByteArray.Length)
                postStream.Close()
            End If
            Dim response As HttpWebResponse = request.GetResponse
            If response.StatusCode = HttpStatusCode.OK Then
                Dim responseReader As StreamReader = New IO.StreamReader(response.GetResponseStream)
                responseData = responseReader.ReadToEnd
            End If
        Catch ex As Exception
            Throw New Exception("An error occurred: " & ex.Message)
        End Try
        Return responseData
    End Function

#End Region

End Class
