' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL


Partial Public Class DocumentTypes
    Inherits System.Web.UI.Page
    Private _fltIdentity As FltIdentity = Nothing
    Private _documentType As DocumentType = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        ClearErrorFields()
        If Not Me.IsPostBack Then
            PopulateDropDownlists()
            DocumentTypesGridView_DataBind()
        Else
            _documentType = CType(ViewState("_documentType"), DocumentType)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_documentType") = _documentType
    End Sub

    Protected Sub DocumentTypesGridView_DataBind()
        Dim totRows As Integer = 0
        DocumentTypesGridView.DataSource = DocumentTypeManager.GetList(_fltIdentity)
        DocumentTypesGridView.DataBind()
    End Sub

    Protected Sub DocumentTypeGridView_PageIndexChanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles DocumentTypesGridView.PageIndexChanging
        Dim gv As GridView = CType(sender, GridView)
        gv.PageIndex = e.NewPageIndex
        DocumentTypesGridView_DataBind()
    End Sub

    Protected Sub DocumentTypeGridView_RowEditing(ByVal sender As System.Object, ByVal e As GridViewEditEventArgs) Handles DocumentTypesGridView.RowEditing
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.NewEditIndex)
            Dim DocumentTypeId As Integer = CInt(CType(row.FindControl("DocumentTypeIdLB"), Label).Text)
            _documentType = DocumentTypeManager.GetItem(DocumentTypeId, _fltIdentity)
            gv.EditIndex = e.NewEditIndex
            DocumentTypesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub DocumentTypeGridView_CancelingEdit(ByVal sender As System.Object, ByVal e As GridViewCancelEditEventArgs) Handles DocumentTypesGridView.RowCancelingEdit
        DocumentTypesGridView.EditIndex = -1
        _documentType = Nothing
        DocumentTypesGridView_DataBind()
    End Sub

    Protected Sub DocumentTypeGridView_RowUpdating(ByVal sender As System.Object, ByVal e As GridViewUpdateEventArgs) Handles DocumentTypesGridView.RowUpdating
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            With _documentType
                .FileExtension = Server.HtmlDecode(CType(row.FindControl("FileExtensionTB"), TextBox).Text)
                .MimeType = Server.HtmlDecode(CType(row.FindControl("MimeTypeTB"), TextBox).Text)
            End With
            DocumentTypeManager.Update(_documentType, _fltIdentity)
            _documentType = Nothing
            gv.EditIndex = -1
            DocumentTypesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub DocumentTypeGridView_RowDeleting(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DocumentTypesGridView.RowDeleting
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            Dim DocumentTypeId As Integer = CInt(CType(row.FindControl("DocumentTypeIdLB"), Label).Text)
            _documentType = DocumentTypeManager.GetItem(DocumentTypeId, _fltIdentity)
            DocumentTypeManager.Delete(_documentType, _fltIdentity)
            _documentType = Nothing
            DocumentTypesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        LoadDataToObject()
        Try
            DocumentTypeManager.Save(_documentType, _fltIdentity)
            _documentType = Nothing
            Me.DocumentTypesMultiView.ActiveViewIndex = 0
            DocumentTypesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Me.DocumentTypesMultiView.ActiveViewIndex = 0
        DocumentTypesGridView_DataBind()
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click
        _documentType = New DocumentType
        Me.AddEditTitleLabel.Text = "Adding New Entry"
        LoadDataToForm()
        Me.DocumentTypesMultiView.ActiveViewIndex = 1
    End Sub

    Private Sub LoadDataToForm()

        ClearErrorFields()

        Me.FileExtensionTB.Text = Server.HtmlEncode(_documentType.FileExtension)
        Me.MimeTypeTB.Text = Server.HtmlEncode(_documentType.MimeType)

    End Sub

    Private Sub LoadDataToObject()

        ClearErrorFields()

        Try
            _documentType.DocumentTypeId = -1
            _documentType.FileExtension = Server.HtmlDecode(Me.FileExtensionTB.Text)
            _documentType.MimeType = Server.HtmlDecode(Me.MimeTypeTB.Text)

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Private Sub PopulateDropDownlists()
    End Sub

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
        If _documentType.BrokenRules.Count <> 0 Then
            Me.BrokenRulesList.Visible = True
            For Each thisRule As BrokenRule In _documentType.BrokenRules
                Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
            Next
        End If
    End Sub

End Class