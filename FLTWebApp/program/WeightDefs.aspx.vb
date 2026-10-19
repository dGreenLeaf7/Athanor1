' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO

Partial Public Class WeightDefs
    Inherits System.Web.UI.Page
#Region "Private Variables"

    Private _fltIdentity As FltIdentity = Nothing
    Private _weightDef As WeightDef

#End Region

#Region "Protected Subs (WeightDef List View)"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        Me.ProgIdHiddenField.Value = _fltIdentity.CurrentProgId
        ClearErrorFields()
        If Not Me.IsPostBack Then
            WeightDefTypeFilterDropDownList_DataBind()
            WeightDefsGridView_DataBind()
        Else
            _weightDef = CType(ViewState("_weightDef"), WeightDef)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_weightDef") = _weightDef
    End Sub

    Protected Sub WeightDefTypeFilterDropDownList_DataBind()
        Me.WeightDefTypeFilterDropDownList.Items.Clear()
        Me.WeightDefTypeFilterDropDownList.Items.Add(New ListItem("< All >", 0))
        For Each thisWeightDefType As WeightDefType In WeightDefTypeManager.GetList(_fltIdentity)
            Dim newListItem As New ListItem(thisWeightDefType.WeightDefTypeDescr, thisWeightDefType.WeightDefTypeId)
            Me.WeightDefTypeFilterDropDownList.Items.Add(newListItem)
        Next
        Me.WeightDefTypeFilterDropDownList.SelectedValue = Me.WeightDefTypeFilterDropDownList.Items(0).Value
    End Sub

    Protected Sub WeightDefsGridView_DataBind()
        Dim totRows As Integer = 0
        WeightDefsGridView.DataSource = WeightDefManager.GetList(CInt(Me.ProgIdHiddenField.Value), String.Empty, CInt(WeightDefTypeFilterDropDownList.SelectedValue), Me.IsActiveDropDownList.SelectedValue, Me.IsDeletedDropDownlist.SelectedValue, -1, -1, totRows, _fltIdentity)
        WeightDefsGridView.DataBind()
    End Sub

    Protected Sub WeightDefsGridView_PageIndexChanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles WeightDefsGridView.PageIndexChanging
        WeightDefsGridView.PageIndex = e.NewPageIndex
        WeightDefsGridView_DataBind()
    End Sub

    Protected Sub WeightDefsGridView_RowEditing(ByVal sender As System.Object, ByVal e As GridViewEditEventArgs) Handles WeightDefsGridView.RowEditing
        Dim gv As GridView = CType(sender, GridView)
        Dim row As GridViewRow = gv.Rows(e.NewEditIndex)
        Dim WeightDefId As Integer = CInt(CType(row.FindControl("WeightDefIdLB"), Label).Text)
        _weightDef = WeightDefManager.GetItem(WeightDefId, _fltIdentity)
        gv.EditIndex = e.NewEditIndex
        WeightDefsGridView_DataBind()
    End Sub

    Protected Sub WeightDefsGridView_RowCancelingEdit(ByVal sender As System.Object, ByVal e As GridViewCancelEditEventArgs) Handles WeightDefsGridView.RowCancelingEdit
        _weightDef = Nothing
        WeightDefsGridView.EditIndex = -1
        WeightDefsGridView_DataBind()
    End Sub

    Protected Sub WeightDefsGridView_RowUpdating(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles WeightDefsGridView.RowUpdating
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            With _weightDef
                .WeightDefTypeId = CInt(CType(row.FindControl("WeightDefTypeDDL"), DropDownList).SelectedValue)
                .WeightDefDescr = Server.HtmlDecode(CType(row.FindControl("WeightDefDescrTB"), TextBox).Text)
                .Weight = Server.HtmlDecode(CType(row.FindControl("WeightTB"), TextBox).Text)
                .WeightUnitId = CInt(CType(row.FindControl("WeightUnitDDL"), DropDownList).SelectedValue)
                .SortOrder = CInt(CType(row.FindControl("SortOrderTB"), TextBox).Text)
                .IsActive = CType(row.FindControl("IsActiveCB"), CheckBox).Checked
                .IsDeleted = CType(row.FindControl("IsDeletedCB"), CheckBox).Checked
            End With
            WeightDefManager.Update(_weightDef, _fltIdentity)
            _weightDef = Nothing
            gv.EditIndex = -1
            WeightDefsGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub WeightDefsGridView_RowDeleting(ByVal sender As System.Object, ByVal e As GridViewDeleteEventArgs) Handles WeightDefsGridView.RowDeleting
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            Dim WeightDefId As Integer = CInt(CType(row.FindControl("WeightDefIdLB"), Label).Text)
            _weightDef = WeightDefManager.GetItem(WeightDefId, _fltIdentity)
            WeightDefManager.Delete(_weightDef, _fltIdentity)
            _weightDef = Nothing
            WeightDefsGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click
        _weightDef = New WeightDef
        LoadDataToForm()
        Me.WeightDefMultiView.ActiveViewIndex = 1
    End Sub

    Protected Sub IsActiveDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsActiveDropDownList.SelectedIndexChanged
        WeightDefsGridView_DataBind()
    End Sub

    Protected Sub IsDeletedDropDownlist_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsDeletedDropDownlist.SelectedIndexChanged
        WeightDefsGridView_DataBind()
    End Sub

#End Region

#Region "Protected Procedures (WeightDef Add View)"

    Protected Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        LoadDataToObject()
        Try
            WeightDefManager.Save(_weightDef, _fltIdentity)
            Me.WeightDefMultiView.ActiveViewIndex = 0
            WeightDefTypeFilterDropDownList_DataBind()
            WeightDefsGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Me.WeightDefMultiView.ActiveViewIndex = 0
        WeightDefTypeFilterDropDownList_DataBind()
        WeightDefsGridView_DataBind()
    End Sub

#End Region

    Protected Function GetWeightUnits() As fltLookupBaseList
        Return fltLookupBaseManager.GetList("WeightUnit", Me.ProgIdHiddenField.Value)
    End Function

    Protected Function GetWeightDefTypes() As WeightDefTypeList
        Return WeightDefTypeManager.GetList(_fltIdentity)
    End Function

    Private Sub PopulateModifyDropDownLists()
        Try
            Dim totRows As Long = 0
            Me.WeightDefTypeDropDownList.Items.Clear()
            Me.WeightDefTypeDropDownList.Items.Add(New ListItem("< Select > ", 0))
            For Each thisWeightDefType As WeightDefType In WeightDefTypeManager.GetList(-1, -1, totRows, _fltIdentity)
                Me.WeightDefTypeDropDownList.Items.Add(New ListItem(thisWeightDefType.WeightDefTypeDescr, thisWeightDefType.WeightDefTypeId))
            Next
            Me.WeightUnitDropDownList.Items.Clear()
            Me.WeightUnitDropDownList.Items.Add(New ListItem("--", 0))
            For Each thisWeightUnit As fltLookupBase In fltLookupBaseManager.GetList("WeightUnit", Me.ProgIdHiddenField.Value)
                Me.WeightUnitDropDownList.Items.Add(New ListItem(thisWeightUnit.StringValue, thisWeightUnit.ID))
            Next
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Private Sub LoadDataToForm()
        Try
            PopulateModifyDropDownLists()
            Me.WeightDefTypeDropDownList.SelectedValue = _weightDef.WeightDefTypeId
            Me.WeightDefDescrTextBox.Text = Server.HtmlEncode(_weightDef.WeightDefDescr)
            Me.WeightTextBox.Text = Server.HtmlEncode(_weightDef.Weight)
            Me.SortOrderTextBox.Text = _weightDef.SortOrder
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Private Sub LoadDataToObject()
        Try
            _weightDef.ProgId = Me.ProgIdHiddenField.Value
            _weightDef.WeightDefTypeId = Me.WeightDefTypeDropDownList.SelectedValue
            _weightDef.WeightDefDescr = Server.HtmlDecode(Me.WeightDefDescrTextBox.Text)
            _weightDef.Weight = Server.HtmlDecode(Me.WeightTextBox.Text)
            _weightDef.WeightUnitId = Me.WeightUnitDropDownList.SelectedValue
            _weightDef.SortOrder = Me.SortOrderTextBox.Text
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub WeightDefTypeFilterDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WeightDefTypeFilterDropDownList.SelectedIndexChanged
        WeightDefsGridView_DataBind()
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
        If _weightDef.BrokenRules.Count <> 0 Then
            Me.BrokenRulesList.Visible = True
            For Each thisRule As BrokenRule In _weightDef.BrokenRules
                Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
            Next
        End If
    End Sub

End Class