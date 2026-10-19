' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports Microsoft.Security.Application

Partial Public Class documentsList
    Inherits System.Web.UI.Page

#Region "Private Variables"

    Private _fltIdentity As FltIdentity = Nothing
    Private _documentInfo As DocumentInfo = Nothing
    Private _documentAddUrl As String = "~/Documents/documentAdd.aspx"

#End Region

#Region "Procedures (Page Wide)"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ClearErrorFields()
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        Me.ProgIdHiddenField.Value = _fltIdentity.CurrentProgId
        If Not Me.IsPostBack Then
            LoadFilterDDLs(_fltIdentity.CurrentProgId)
            DocumentsGridView_DataBind()
        Else
            _documentInfo = CType(ViewState("_documentInfo"), DocumentInfo)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_documentInfo") = _documentInfo
    End Sub

    Private Sub LoadFilterDDLs(ByVal ProgId As Long)
        Try
            Me.DocumentTypeDDL.Items.Add(New ListItem("< All >", 0))
            For Each thisDocType As DocumentType In DocumentTypeManager.GetList(_fltIdentity)
                Me.DocumentTypeDDL.Items.Add(New ListItem(thisDocType.FileExtension, thisDocType.DocumentTypeId))
            Next
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub DocumentTypeDDL_Change(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DocumentTypeDDL.SelectedIndexChanged
        DocumentsGridView_DataBind()
    End Sub

    Protected Sub IsDeletedFilterDDL_Change(ByVal senders As System.Object, ByVal e As System.EventArgs) Handles IsDeletedDDL.SelectedIndexChanged
        DocumentsGridView_DataBind()
    End Sub

#End Region

#Region "Procedures (List View)"

    Protected Sub DocumentsGridView_DataBind()
        Dim totRows As Integer = 0
        Try
            DocumentsGridView.DataSource = DocumentInfoManager.GetList(Me.ProgIdHiddenField.Value, Me.DocumentTypeDDL.SelectedValue, IsDeletedDDL.SelectedValue, -1, -1, totRows, _fltIdentity)
            DocumentsGridView.DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub DocumentsGridView_PageIndexchanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles DocumentsGridView.PageIndexChanging
        Dim gv As GridView = CType(sender, GridView)
        gv.PageIndex = e.NewPageIndex
        DocumentsGridView_DataBind()
    End Sub

    Protected Sub DocumentsGridView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DocumentsGridView.SelectedIndexChanged
        Dim stringBuilder As New StringBuilder

        With stringBuilder
            .Append("~/documents/documentShow.aspx?id=")
            .Append(Encoder.UrlEncode(Me.DocumentsGridView.SelectedValue.ToString))
        End With

        Response.Redirect(stringBuilder.ToString, False)
    End Sub

    Protected Sub DocumentsGridView_RowEditing(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles DocumentsGridView.RowEditing
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.NewEditIndex)
            Dim documentId As Integer = CInt(CType(row.FindControl("DocumentIdLB"), Label).Text)
            _documentInfo = DocumentInfoManager.GetItem(documentId, _fltIdentity)
            gv.EditIndex = e.NewEditIndex
            DocumentsGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub DocumentsGridView_CancelingEdit(ByVal sender As System.Object, ByVal e As GridViewCancelEditEventArgs) Handles DocumentsGridView.RowCancelingEdit
        DocumentsGridView.EditIndex = -1
        _documentInfo = Nothing
        DocumentsGridView_DataBind()
    End Sub

    Protected Sub DocumentsGridView_RowUpdating(ByVal sender As System.Object, ByVal e As GridViewUpdateEventArgs) Handles DocumentsGridView.RowUpdating
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            With _documentInfo
                .DocumentName = Server.HtmlDecode(CType(row.FindControl("DocumentNameTB"), TextBox).Text)
                .DocumentDescription = Server.HtmlDecode(CType(row.FindControl("DocumentDescriptionTB"), TextBox).Text)
                .IsDeleted = CType(row.FindControl("IsDeletedCB"), CheckBox).Checked
            End With
            DocumentInfoManager.Update(_documentInfo, _fltIdentity)
            _documentInfo = Nothing
            gv.EditIndex = -1
            DocumentsGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub DocumentsGridView_RowDeleting(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DocumentsGridView.RowDeleting
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            Dim documentId As Integer = CInt(CType(row.FindControl("DocumentIdLB"), Label).Text)
            _documentInfo = DocumentInfoManager.GetItem(documentId, _fltIdentity)
            DocumentInfoManager.Delete(_documentInfo, _fltIdentity)
            _documentInfo = Nothing
            DocumentsGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click

        Dim stringBuilder As New StringBuilder

        With stringBuilder
            .Append(Encoder.UrlEncode(_documentAddUrl))
            .Append("?mode=add")
        End With
        Response.Redirect("~/Documents/documentAdd.aspx?mode=add", False)

    End Sub

#End Region

#Region "Private Procedures"

    Private Sub ClearErrorFields()
        Me.ErrorMessageLabel.Text = String.Empty
        Me.ErrorMessageLabel.Visible = False
        Me.SuccessMessageLabel.Text = String.Empty
        Me.SuccessMessageLabel.Visible = False
        Me.ErrorsBulletList.Items.Clear()
        Me.ErrorsBulletList.Visible = False
        Me.WarningMessageLabel.Text = String.Empty
        Me.WarningMessageLabel.Visible = False
        Me.WarningsCheckList.Items.Clear()
        Me.WarningsCheckList.Visible = False
    End Sub

    Private Sub ProcessException(ByVal ex As Exception)
        ProcessException(ex, _documentInfo.ErrorsList, _documentInfo.WarningsList)
    End Sub

    Private Sub ProcessException(ByVal ex As Exception, ByVal ErrorsList As BrokenRulesList, ByVal WarningsList As BrokenRulesList)
        Me.ErrorMessageLabel.Text = ex.Message
        Me.ErrorMessageLabel.Visible = True
        If ErrorsList.Count <> 0 Then
            Me.ErrorsBulletList.Visible = True
            Me.ErrorsBulletList.DataSource = ErrorsList
            Me.ErrorsBulletList.DataBind()
        End If
        If WarningsList.Count <> 0 Then
            Me.WarningMessageLabel.Text = "Either correct the errors listed below or place a checkmark next to those errors that can be ignored."
            Me.WarningMessageLabel.Visible = True
            Me.WarningsCheckList.Visible = True
            Me.WarningsCheckList.Items.Clear()
            For Each thisWarning As BrokenRule In WarningsList
                Dim listItem As New ListItem(thisWarning.Message, thisWarning.RuleIdentifier)
                If thisWarning.OverRideRule Then
                    listItem.Selected = True
                End If
                Me.WarningsCheckList.Items.Add(listItem)
            Next
        End If
    End Sub

    Private Sub ProcessWarnings(ByVal WarningsList As BrokenRulesList)
        If WarningsList.Count <> 0 Then
            For Each thisItem As ListItem In Me.WarningsCheckList.Items
                If thisItem.Selected Then
                    For Each thisWarning As BrokenRule In WarningsList
                        If thisItem.Value = thisWarning.RuleIdentifier Then
                            thisWarning.OverRideRule = True
                        End If
                    Next
                End If
            Next
        End If
    End Sub

#End Region

End Class