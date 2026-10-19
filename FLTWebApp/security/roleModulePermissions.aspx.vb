' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System.Web.UI.HtmlControls
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO

Partial Public Class roleModulePermissions
    Inherits System.Web.UI.Page
    Private _fltIdentity As FltIdentity = Nothing
    Private _fltRoleModulePermissions As FltRoleModulePermissions = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Me.IsPostBack Then
            PopulateFilterDropDownLists()
            RoleModulePermissionsGridView_DataBind()
        Else
            _fltRoleModulePermissions = CType(ViewState("_fltRoleModulePermissions"), FltRoleModulePermissions)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_fltRoleModulePermissions") = _fltRoleModulePermissions
    End Sub

    Protected Sub RoleModulePermissionsGridView_DataBind()
        RoleModulePermissionsGridView.DataSource = FltRoleModulePermissionsManager.GetList(CInt(Me.RoleDDL.SelectedValue), _fltIdentity)
        RoleModulePermissionsGridView.DataBind()
    End Sub

    Protected Sub ModulesGridView_PageIndexChanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles RoleModulePermissionsGridView.PageIndexChanging
        RoleModulePermissionsGridView.PageIndex = e.NewPageIndex
        RoleModulePermissionsGridView_DataBind()
    End Sub

    Protected Sub RoleModulePermissionsGridView_RowEditing(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles RoleModulePermissionsGridView.RowEditing
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.NewEditIndex)
            Dim roleId As Integer = CInt(CType(row.FindControl("RoleIdLB"), Label).Text)
            Dim moduleName As String = CType(row.FindControl("moduleNameLB"), Label).Text
            _fltRoleModulePermissions = FltRoleModulePermissionsManager.GetItem(roleId, moduleName, _fltIdentity)
            gv.EditIndex = e.NewEditIndex
            RoleModulePermissionsGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub RoleModulePermissionsGridView_RowCancelingEdit(ByVal sender As System.Object, ByVal e As GridViewCancelEditEventArgs) Handles RoleModulePermissionsGridView.RowCancelingEdit
        _fltRoleModulePermissions = Nothing
        RoleModulePermissionsGridView.EditIndex = -1
        RoleModulePermissionsGridView_DataBind()
    End Sub

    Protected Sub RoleModulePermissionsGridView_RowUpdating(ByVal sender As System.Object, ByVal e As GridViewUpdateEventArgs) Handles RoleModulePermissionsGridView.RowUpdating

        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            Dim moduleName As String = Server.HtmlDecode(CType(row.FindControl("moduleNameLB"), Label).Text)
            With _fltRoleModulePermissions
                .CanView = CBool(CType(row.FindControl("CanViewCB"), CheckBox).Checked)
                .CanEdit = CBool(CType(row.FindControl("CanEditCB"), CheckBox).Checked)
                .IsActive = CBool(CType(row.FindControl("IsActiveCB"), CheckBox).Checked)
            End With
            FltRoleModulePermissionsManager.Update(_fltRoleModulePermissions, _fltIdentity)
            _fltRoleModulePermissions = Nothing
            gv.EditIndex = -1
            RoleModulePermissionsGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub RoleModulePermissionsGridView_RowDeleting(ByVal sender As System.Object, ByVal e As GridViewDeleteEventArgs) Handles RoleModulePermissionsGridView.RowDeleting
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            Dim roleId As Integer = CInt(CType(row.FindControl("RoleIdLB"), Label).Text)
            Dim moduleName As String = CType(row.FindControl("moduleNameLB"), Label).Text
            _fltRoleModulePermissions = FltRoleModulePermissionsManager.GetItem(roleId, moduleName, _fltIdentity)
            FltRoleModulePermissionsManager.Delete(_fltRoleModulePermissions, _fltIdentity)
            RoleModulePermissionsGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub ProgramDropDownlist_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProgramDDL.SelectedIndexChanged
        Dim totRows As Long
        Me.RoleDDL.Items.Clear()
        For Each thisRole As FltRole In FltRoleManager.GetList(Me.ProgramDDL.SelectedValue, 1, 0, -1, -1, totRows, _fltIdentity)
            Me.RoleDDL.Items.Add(New ListItem(thisRole.RoleName, thisRole.RoleId))
        Next
        RoleModulePermissionsGridView_DataBind()
    End Sub

    Protected Sub RoleDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RoleDDL.SelectedIndexChanged
        RoleModulePermissionsGridView_DataBind()
    End Sub

    Protected Sub IsActiveDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsActiveDropDownList.SelectedIndexChanged
        RoleModulePermissionsGridView_DataBind()
    End Sub

    Protected Sub IsDeletedDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsDeletedDropDownList.SelectedIndexChanged
        RoleModulePermissionsGridView_DataBind()
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click
        Me.AddEditTitleLabel.Text = "Adding New Item"
        Try
            _fltRoleModulePermissions = New FltRoleModulePermissions()
            LoadDataToForm()
            Me.RoleModulePermissionsMultiView.ActiveViewIndex = 1
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        LoadDataToObject()
        Try
            FltRoleModulePermissionsManager.Save(_fltRoleModulePermissions, _fltIdentity)
            _fltRoleModulePermissions = Nothing
            Me.RoleModulePermissionsMultiView.ActiveViewIndex = 0
            RoleModulePermissionsGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        _fltRoleModulePermissions = Nothing
        ClearErrorFields()
        RoleModulePermissionsMultiView.ActiveViewIndex = 0
        RoleModulePermissionsGridView_DataBind()
    End Sub

    Private Sub ClearErrorFields()
        Me.ErrorMessageLabel.Text = String.Empty
        Me.ErrorMessageLabel.Visible = False
        Me.SuccessMessageLabel.Text = String.Empty
        Me.SuccessMessageLabel.Visible = False
        Me.BrokenRulesList.Items.Clear()
        Me.BrokenRulesList.Visible = False
    End Sub

    Private Sub PopulateFilterDropDownLists()
        Dim totRows As Long
        Me.ProgramDDL.Items.Clear()
        For Each thisProg As Program In ProgramManager.GetList(1, 0, -1, -1, totRows)
            Me.ProgramDDL.Items.Add(New ListItem(thisProg.ProgName, thisProg.ProgID))
        Next
        Me.RoleDDL.Items.Clear()
        For Each thisRole As FltRole In FltRoleManager.GetList(ProgramDDL.Items(0).Value, 1, 0, -1, -1, totRows, _fltIdentity)
            Me.RoleDDL.Items.Add(New ListItem(thisRole.RoleName, thisRole.RoleId))
        Next
    End Sub

    Private Sub PopulateDropDownLists()
        Dim totRows As Long
        Me.ModuleDropDownlist.Items.Clear()
        Me.ModuleDropDownlist.Items.Add(New ListItem("<Select a Module>", String.Empty))
        For Each thisModule As FltModule In FltModuleManager.GetList(1, 0, -1, -1, totRows, _fltIdentity)
            Me.ModuleDropDownlist.Items.Add(New ListItem(thisModule.ModuleName))
        Next
    End Sub

    Private Sub LoadDataToObject()

        ClearErrorFields()

        _fltRoleModulePermissions.RoleId = Me.RoleDDL.SelectedValue
        _fltRoleModulePermissions.ModuleName = Me.ModuleDropDownlist.SelectedValue
        _fltRoleModulePermissions.CanView = Me.CanViewCheckBox.Checked
        _fltRoleModulePermissions.CanEdit = Me.CanEditCheckBox.Checked
        _fltRoleModulePermissions.IsActive = Me.IsActiveCheckBox.Checked

    End Sub

    Private Sub LoadDataToForm()

        ClearErrorFields()
        PopulateDropDownLists()

        Me.RoleLabel.Text = Me.RoleDDL.SelectedItem.Text
        Me.ModuleDropDownlist.SelectedValue = _fltRoleModulePermissions.ModuleName
        Me.CanViewCheckBox.Checked = _fltRoleModulePermissions.CanView
        Me.CanEditCheckBox.Checked = _fltRoleModulePermissions.CanEdit
        Me.IsActiveCheckBox.Checked = _fltRoleModulePermissions.IsActive

    End Sub

    Private Sub ProcessException(ByVal ex As Exception)
        ProcessException(ex, _fltRoleModulePermissions.BrokenRules, String.Empty)
    End Sub

    Private Sub ProcessException(ByVal ex As Exception, ByVal AdditionalMessage As String)
        ProcessException(ex, New BrokenRulesList, AdditionalMessage)
    End Sub

    Private Sub ProcessException(ByVal ex As Exception, ByVal BrokenRulesList As BrokenRulesList)
        ProcessException(ex, BrokenRulesList, String.Empty)
    End Sub

    Private Sub ProcessException(ByVal ex As Exception, ByVal BrokenRulesList As BrokenRulesList, ByVal AdditionalMessage As String)
        Me.ErrorMessageLabel.Text = ex.Message & "  " & AdditionalMessage
        Me.ErrorMessageLabel.Visible = True
        If _fltRoleModulePermissions.BrokenRules.Count <> 0 Then
            Me.BrokenRulesList.Visible = True
            For Each thisRule As BrokenRule In BrokenRulesList
                Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
            Next
        End If
    End Sub

End Class