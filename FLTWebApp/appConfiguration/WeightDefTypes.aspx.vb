' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL

Partial Public Class WeightDefTypes
    Inherits System.Web.UI.Page

    Private _fltIdentity As FltIdentity = Nothing
    Private _WeightDefType As WeightDefType = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        ClearErrorFields()
        If Not Me.IsPostBack Then
            PopulateDropDownlists()
            WeightDefTypesGridView_DataBind()
        Else
            _WeightDefType = CType(ViewState("_WeightDefType"), WeightDefType)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_WeightDefType") = _WeightDefType
    End Sub

    Protected Sub WeightDefTypesGridView_DataBind()
        Dim totRows As Integer = 0
        WeightDefTypesGridView.DataSource = WeightDefTypeManager.GetList(_fltIdentity)
        WeightDefTypesGridView.DataBind()
    End Sub

    Protected Sub WeightDefTypeGridView_PageIndexChanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles WeightDefTypesGridView.PageIndexChanging
        Dim gv As GridView = CType(sender, GridView)
        gv.PageIndex = e.NewPageIndex
        WeightDefTypesGridView_DataBind()
    End Sub

    Protected Sub WeightDefTypeGridView_RowEditing(ByVal sender As System.Object, ByVal e As GridViewEditEventArgs) Handles WeightDefTypesGridView.RowEditing
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.NewEditIndex)
            Dim WeightDefTypeId As Integer = CInt(CType(row.FindControl("WeightDefTypeIdLB"), Label).Text)
            _WeightDefType = WeightDefTypeManager.GetItem(WeightDefTypeId, _fltIdentity)
            gv.EditIndex = e.NewEditIndex
            WeightDefTypesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub WeightDefTypeGridView_CancelingEdit(ByVal sender As System.Object, ByVal e As GridViewCancelEditEventArgs) Handles WeightDefTypesGridView.RowCancelingEdit
        WeightDefTypesGridView.EditIndex = -1
        _WeightDefType = Nothing
        WeightDefTypesGridView_DataBind()
    End Sub

    Protected Sub WeightDefTypeGridView_RowUpdating(ByVal sender As System.Object, ByVal e As GridViewUpdateEventArgs) Handles WeightDefTypesGridView.RowUpdating
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            With _WeightDefType
                .WeightDefTypeDescr = Server.HtmlDecode(CType(row.FindControl("WeightDefTypeDescrTB"), TextBox).Text)
                .SortOrder = Server.HtmlDecode(CType(row.FindControl("SortOrderTB"), TextBox).Text)
            End With
            WeightDefTypeManager.Update(_WeightDefType, _fltIdentity)
            _WeightDefType = Nothing
            gv.EditIndex = -1
            WeightDefTypesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub WeightDefTypeGridView_RowDeleting(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles WeightDefTypesGridView.RowDeleting
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            Dim WeightDefTypeId As Integer = CInt(CType(row.FindControl("WeightDefTypeIdLB"), Label).Text)
            _WeightDefType = WeightDefTypeManager.GetItem(WeightDefTypeId, _fltIdentity)
            WeightDefTypeManager.Delete(_WeightDefType, _fltIdentity)
            _WeightDefType = Nothing
            WeightDefTypesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        LoadDataToObject()
        Try
            WeightDefTypeManager.Save(_WeightDefType, _fltIdentity)
            _WeightDefType = Nothing
            Me.WeightDefTypesMultiView.ActiveViewIndex = 0
            WeightDefTypesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Me.WeightDefTypesMultiView.ActiveViewIndex = 0
        WeightDefTypesGridView_DataBind()
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click
        _WeightDefType = New WeightDefType
        Me.AddEditTitleLabel.Text = "Adding New Entry"
        LoadDataToForm()
        Me.WeightDefTypesMultiView.ActiveViewIndex = 1
    End Sub

    Private Sub LoadDataToForm()

        ClearErrorFields()

        Me.WeightDefTypeDescrTB.Text = Server.HtmlEncode(_WeightDefType.WeightDefTypeDescr)
        Me.SortOrderTB.Text = Server.HtmlEncode(_WeightDefType.SortOrder)

    End Sub

    Private Sub LoadDataToObject()

        ClearErrorFields()

        Try
            _WeightDefType.WeightDefTypeId = -1
            _WeightDefType.WeightDefTypeDescr = Server.HtmlDecode(Me.WeightDefTypeDescrTB.Text)
            _WeightDefType.SortOrder = Server.HtmlDecode(Me.SortOrderTB.Text)

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
        If _WeightDefType.BrokenRules.Count <> 0 Then
            Me.BrokenRulesList.Visible = True
            For Each thisRule As BrokenRule In _WeightDefType.BrokenRules
                Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
            Next
        End If
    End Sub

End Class