' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.IO
Imports System.Text
Imports System.Collections
Imports System.Configuration

Public Class frmExportClient

    Dim _fltExportManager As New FltExportManager
    Dim _userName As String = String.Empty
    Dim _password As String = String.Empty

    Private Sub CloseButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

    Private Sub GetFileListButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetFileListButton.Click
        Cursor.Current = Cursors.WaitCursor
        Try
            Dim downloadFileList As DownloadFileList = _fltExportManager.GetFileList(_userName, _password)
            Me.FileListView.Items.Clear()
            For Each thisFile As DownloadFile In downloadFileList
                Dim thisItem As New ListViewItem(thisFile.FileName)
                thisItem.SubItems.Add(Format(thisFile.FileSize, "#,##0"))
                thisItem.SubItems.Add(Format(thisFile.UtcDateCreated, "MM/dd/yyyyThh:mm:ss"))
                Me.FileListView.Items.Add(thisItem)
            Next
            Me.ResponseTextBox.Text += vbCrLf
            Me.ResponseTextBox.Text += vbCrLf
            Me.ResponseTextBox.Text += Date.Now.ToString & " // "
            Me.ResponseTextBox.Text += "GetFileList // "
            Me.ResponseTextBox.Text += "File List retrieved"
        Catch ex As Exception
            Me.ResponseTextBox.Text += vbCrLf
            Me.ResponseTextBox.Text += Date.Now.ToString & " // "
            Me.ResponseTextBox.Text += "GetFileList // "
            Me.ResponseTextBox.Text += ex.Message
        End Try
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub CreateFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateFileButton.Click
        Cursor.Current = Cursors.WaitCursor
        Try
            Dim responseData = _fltExportManager.CreateFile(_userName, _password, CDate("01-Jan-2000"))
            Me.ResponseTextBox.Text += vbCrLf
            Me.ResponseTextBox.Text += Date.Now.ToString & " // "
            Me.ResponseTextBox.Text += "CreateFile // "
            Me.ResponseTextBox.Text += responseData
        Catch ex As System.TimeoutException
            Me.ResponseTextBox.Text += vbCrLf
            Me.ResponseTextBox.Text += Date.Now.ToString & " // "
            Me.ResponseTextBox.Text += "CreateFile // "
            Me.ResponseTextBox.Text += ex.Message
        Catch ex As Exception
            Me.ResponseTextBox.Text += vbCrLf
            Me.ResponseTextBox.Text += Date.Now.ToString & " // "
            Me.ResponseTextBox.Text += "CreateFile // "
            Me.ResponseTextBox.Text += ex.Message
        End Try
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub FetchFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FetchFileButton.Click
        Cursor.Current = Cursors.WaitCursor
        Dim filename As String = String.Empty
        Dim strFileContent As String = String.Empty
        Try
            If Me.FileListView.SelectedItems.Count = 0 Then
                Throw New Exception("No file selected for download")
            End If
            filename = Me.FileListView.SelectedItems(0).Text
            strFileContent = _fltExportManager.FetchFile(_userName, _password, filename)
        Catch ex As Exception
            Me.ResponseTextBox.Text += vbCrLf
            Me.ResponseTextBox.Text += Date.Now.ToString & " // "
            Me.ResponseTextBox.Text += "FetchFile // "
            Me.ResponseTextBox.Text += ex.Message
        End Try
        If strFileContent.Length <> 0 Then
            Dim myDocumentsFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            Using outFile As New StreamWriter(myDocumentsFolder & "\" & filename)
                outFile.Write(strFileContent)
            End Using
            Me.ResponseTextBox.Text += vbCrLf
            Me.ResponseTextBox.Text += Date.Now.ToString & " // "
            Me.ResponseTextBox.Text += "FetchFile // "
            Me.ResponseTextBox.Text += "file written to " & myDocumentsFolder & "\" & filename
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub FileListView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles FileListView.DoubleClick
        Cursor.Current = Cursors.WaitCursor
        FetchFileButton_Click(sender, e)
        Cursor.Current = Cursors.Default
    End Sub

    Public Sub New()

        InitializeComponent()

        _userName = ConfigurationManager.AppSettings("userName").ToString
        _password = ConfigurationManager.AppSettings("password").ToString

    End Sub
End Class
