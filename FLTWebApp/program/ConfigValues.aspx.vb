' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL


Partial Public Class configurations
    Inherits System.Web.UI.Page
    Private _lookupBase As fltLookupBase = Nothing
    Private _fltIdentity As FltIdentity = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = CType(Session("fltIdentity"), FltIdentity)
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        Me.ProgIdHiddenField.Value = _fltIdentity.CurrentProgId
        ClearErrorFields()
        If Not Me.IsPostBack Then
            PopulateDropDownlists()
            ConfigurationGridView_DataBind()
        Else
            _lookupBase = CType(ViewState("_lookupBase"), fltLookupBase)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_lookupBase") = _lookupBase
    End Sub

    Protected Sub ConfigurationGridView_DataBind()
        Dim totRows As Integer = 0
        ConfigurationGridView.DataSource = fltLookupBaseManager.GetList(Me.TypeDropDownList.SelectedItem.Text, ProgIdHiddenField.Value, Me.IsActiveDropDownList.SelectedValue, -1, -1, totRows)
        ConfigurationGridView.DataBind()
    End Sub

    Protected Sub ConfigurationGridView_PageIndexChanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles ConfigurationGridView.PageIndexChanging
        ConfigurationGridView.PageIndex = e.NewPageIndex
        ConfigurationGridView_DataBind()
    End Sub

    Protected Sub ConfigurationGridView_RowEditing(ByVal sender As System.Object, ByVal e As GridViewEditEventArgs) Handles ConfigurationGridView.RowEditing
        Dim row As GridViewRow = ConfigurationGridView.Rows(e.NewEditIndex)
        Dim id As Integer = CInt(CType(row.FindControl("IDLB"), Label).Text)
        _lookupBase = fltLookupBaseManager.GetItem(Me.TypeDropDownList.SelectedItem.Text, id)
        ConfigurationGridView.EditIndex = e.NewEditIndex
        ConfigurationGridView_DataBind()
    End Sub

    Protected Sub ConfigurationGridView_RowCancelingEdit(ByVal sender As System.Object, ByVal e As GridViewCancelEditEventArgs) Handles ConfigurationGridView.RowCancelingEdit
        _lookupBase = Nothing
        ConfigurationGridView.EditIndex = -1
        ConfigurationGridView_DataBind()
    End Sub

    Protected Sub ConfigurationGridView_RowUpdating(ByVal sender As System.Object, ByVal e As GridViewUpdateEventArgs) Handles ConfigurationGridView.RowUpdating
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            With _lookupBase
                .StringValue = Server.HtmlDecode(CType(row.FindControl("StringValueTB"), TextBox).Text)
                .SortOrder = CType(row.FindControl("SortOrderTB"), TextBox).Text
                .IsActive = CType(row.FindControl("IsActiveCB"), CheckBox).Checked
            End With
            fltLookupBaseManager.Update(_lookupBase, _fltIdentity)
            _lookupBase = Nothing
            gv.EditIndex = -1
            ConfigurationGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub ConfigurationGridView_RowDeleting(ByVal sender As System.Object, ByVal e As GridViewDeleteEventArgs) Handles ConfigurationGridView.RowDeleting
        Try
            Dim row As GridViewRow = ConfigurationGridView.Rows(e.RowIndex)
            Dim id As Integer = CInt(CType(row.FindControl("IDLB"), Label).Text)
            Dim fltLookupBase As fltLookupBase = fltLookupBaseManager.GetItem(TypeDropDownList.SelectedItem.Text, id)
            fltLookupBaseManager.Delete(fltLookupBase, _fltIdentity)
            ConfigurationGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click

        Try
            LoadDataToObject()
            fltLookupBaseManager.Save(_lookupBase, _fltIdentity)
            _lookupBase = Nothing
            Me.ConfigurationMultiView.ActiveViewIndex = 0
            ConfigurationGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Me.ConfigurationMultiView.ActiveViewIndex = 0
        ConfigurationGridView_DataBind()
    End Sub

    Protected Sub TypeDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TypeDropDownList.SelectedIndexChanged
        ConfigurationGridView_DataBind()
    End Sub

    Protected Sub IsActiveDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsActiveDropDownList.SelectedIndexChanged
        ConfigurationGridView_DataBind()
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click
        _lookupBase = New fltLookupBase
        _lookupBase.LookupName = Me.TypeDropDownList.SelectedValue
        _lookupBase.ProgId = Me.ProgIdHiddenField.Value
        LoadDataToForm()
        Me.ConfigurationMultiView.ActiveViewIndex = 1
    End Sub

    Private Sub ClearErrorFields()
        Me.ErrorMessageLabel.Text = String.Empty
        Me.ErrorMessageLabel.Visible = False
        Me.SuccessMessageLabel.Text = String.Empty
        Me.SuccessMessageLabel.Visible = False
        Me.BrokenRulesList.Items.Clear()
        Me.BrokenRulesList.Visible = False
    End Sub

    Private Sub LoadDataToForm()

        Me.StringValueTextBox.Text = Server.HtmlEncode(_lookupBase.StringValue)
        Me.SortOrderTextBox.Text = _lookupBase.SortOrder
        Me.IsActiveCheckBox.Checked = _lookupBase.IsActive

    End Sub

    Private Sub LoadDataToObject()
        _lookupBase.StringValue = Server.HtmlDecode(Me.StringValueTextBox.Text)
        _lookupBase.SortOrder = Me.SortOrderTextBox.Text
        _lookupBase.IsActive = Me.IsActiveCheckBox.Checked
    End Sub

    Private Sub PopulateDropDownlists()
        Dim FltLookupNameList As fltLookupNameList = New fltLookupNameList
        For Each thisLookupName As String In FltLookupNameList
            Me.TypeDropDownList.Items.Add(New ListItem(thisLookupName, thisLookupName))
        Next
    End Sub

    Private Sub ProcessException(ByVal ex As Exception)
        Me.ErrorMessageLabel.Text = ex.Message
        Me.ErrorMessageLabel.Visible = True
        If _lookupBase.BrokenRules.Count <> 0 Then
            Me.BrokenRulesList.Visible = True
            For Each thisRule As BrokenRule In _lookupBase.BrokenRules
                Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
            Next
        End If
    End Sub

End Class