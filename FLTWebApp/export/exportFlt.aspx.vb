' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports System.IO

Partial Public Class exportFlt
    Inherits System.Web.UI.Page

    Private Const _downloadDirectoryPath As String = "~/download"

    Private _fltIdentity As FltIdentity = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = CType(Session("fltIdentity"), FltIdentity)
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Me.IsPostBack Then
            ShowXmlFiles()
        End If

    End Sub

    Protected Sub Page_PreRender(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Session("_fltIdentity") = _fltIdentity
    End Sub

    Protected Sub CreateExportFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateExportFileButton.Click

        FltExportManager.CreateDownloadFile(GetPhysicalDownloadDirectoryPath, _fltIdentity)
        ShowXmlFiles()

    End Sub

    Private Function GetPhysicalDownloadDirectoryPath() As String
        Return Server.MapPath(_downloadDirectoryPath)
    End Function

    Private Sub ShowXmlFiles()

        Try

            CleanseDownloadFolder(GetPhysicalDownloadDirectoryPath())

            Dim directoryInfo As New IO.DirectoryInfo(GetPhysicalDownloadDirectoryPath)
            Dim fileInfosArray As IO.FileInfo() = directoryInfo.GetFiles("*.*")

            BuildXmlFileList(fileInfosArray)

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

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

    Private Sub BuildXmlFileList(ByVal fileInfosArray As FileInfo())
        Dim tbl As New Table
        tbl.Width = Unit.Percentage(100.0)
        tbl.Rows.Add(BuildHeaderRow())
        If (fileInfosArray.Length > 0) Then
            For Each thisFileInfo As FileInfo In fileInfosArray
                tbl.Rows.Add(BuildXmlRow(thisFileInfo))
            Next
        Else
            tbl.Rows.Add(BuildEmptyRow())
        End If
        Me.fileLinksPH.Controls.Add(tbl)
    End Sub

    Private Function BuildHeaderRow() As TableRow
        Dim newTableRow As New TableRow
        Dim tableCell_0 As New TableCell
        tableCell_0.CssClass = "topleft_header"
        tableCell_0.Text = "Download Link"
        newTableRow.Cells.Add(tableCell_0)
        Dim tableCell_1 As New TableCell
        tableCell_1.CssClass = "topleft_header"
        tableCell_1.Text = "Date Created"
        newTableRow.Cells.Add(tableCell_1)
        Dim tableCell_2 As New TableCell
        tableCell_2.CssClass = "topleft_header"
        tableCell_2.Text = "File Size"
        newTableRow.Cells.Add(tableCell_2)
        Return newTableRow
    End Function

    Private Function BuildXmlRow(ByVal fileInfo As FileInfo) As TableRow
        Dim newTableRow As New TableRow
        Dim tableCell_0 As New TableCell
        Dim tableCell_1 As New TableCell
        Dim tableCell_2 As New TableCell
        Dim hyperLink As New HyperLink
        hyperLink.NavigateUrl = "~\export\getExportFile.aspx?fid=" + fileInfo.Name
        hyperLink.Text = fileInfo.Name
        hyperLink.Target = "_new"
        tableCell_0.Controls.Add(hyperLink)
        tableCell_0.CssClass = "topleft_field"
        newTableRow.Cells.Add(tableCell_0)
        tableCell_1.CssClass = "topleft_field"
        tableCell_1.Text = fileInfo.CreationTimeUtc.ToString
        newTableRow.Cells.Add(tableCell_1)
        tableCell_2.CssClass = "topleft_field"
        tableCell_2.Text = fileInfo.Length.ToString
        newTableRow.Cells.Add(tableCell_2)
        Return newTableRow
    End Function

    Private Function BuildEmptyRow() As TableRow
        Dim newTableRow As New TableRow
        Dim tableCell As New TableCell
        tableCell.CssClass = "error"
        tableCell.ColumnSpan = 3
        tableCell.Text = "There are currently no XML Export files available for download."
        newTableRow.Cells.Add(tableCell)
        Return newTableRow
    End Function

#Region "Private Procedures"

    Private Sub ClearErrorFields()
        Me.ErrorMessageLabel.Text = String.Empty
        Me.ErrorMessageLabel.Visible = False
        Me.SuccessMessageLabel.Text = String.Empty
        Me.SuccessMessageLabel.Visible = False
        Me.BrokenRulesList.Items.Clear()
        Me.BrokenRulesList.Visible = False
    End Sub

    Private Sub ProcessException(ByVal ex As Exception)
        Me.ErrorMessageLabel.Text = ex.Message
        Me.ErrorMessageLabel.Visible = True
    End Sub

#End Region

End Class