' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System.Web.UI.HtmlControls
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO

Partial Public Class roles
    Inherits System.Web.UI.Page
#Region "Private Variables"

    Private _fltIdentity As FltIdentity = Nothing
    Private _FltRole As FltRole = Nothing

#End Region

#Region "Protected Methods (For All Views)"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        ClearErrorFields()
        If Not Me.IsPostBack Then
            LoadProgramDDL()
            RolesGridView_DataBind()
        Else
            _FltRole = CType(ViewState("_FltRole"), FltRole)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_FltRole") = _FltRole
    End Sub

    Protected Sub LoadProgramDDL()
        Me.ProgramDDL.Items.Clear()
        For Each thisProgram As Program In ProgramManager.GetList()
            Me.ProgramDDL.Items.Add(New ListItem(thisProgram.ProgName, thisProgram.ProgID))
        Next
        Me.ProgramDDL.SelectedValue = Me.ProgramDDL.Items(0).Value
    End Sub

#End Region

#Region "Protected Methods (For Roles List View)"
    Protected Sub RolesGridView_DataBind()
        Dim totRows As Integer = 0
        RolesGridView.DataSource = FltRoleManager.GetList(Me.ProgramDDL.SelectedValue, Me.IsActiveDDL.SelectedValue, Me.IsDeletedDDL.SelectedValue, -1, -1, totRows, _fltIdentity)
        RolesGridView.DataBind()
    End Sub

    Protected Sub RolesGridView_PageIndexChanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles RolesGridView.PageIndexChanging
        RolesGridView.PageIndex = e.NewPageIndex
        RolesGridView_DataBind()
    End Sub

    Protected Sub RolesGridView_RowEditing(ByVal sender As System.Object, ByVal e As GridViewEditEventArgs) Handles RolesGridView.RowEditing
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.NewEditIndex)
            Dim roleId As Integer = CInt(CType(row.FindControl("RoleIdLB"), Label).Text)
            _FltRole = FltRoleManager.GetItem(roleId, _fltIdentity)
            gv.EditIndex = e.NewEditIndex
            RolesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub RolesGridView_RowCancelingEdit(ByVal sender As System.Object, ByVal e As GridViewCancelEditEventArgs) Handles RolesGridView.RowCancelingEdit
        _FltRole = Nothing
        RolesGridView.EditIndex = -1
        RolesGridView_DataBind()
    End Sub

    Protected Sub RolesGridView_RowUpdating(ByVal sender As System.Object, ByVal e As GridViewUpdateEventArgs) Handles RolesGridView.RowUpdating
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            With _FltRole
                .RoleName = Server.HtmlDecode(CType(row.FindControl("RoleNameTB"), TextBox).Text)
                .RoleDescription = Server.HtmlDecode(CType(row.FindControl("RoleDescriptionTB"), TextBox).Text)
                .IsProgramManager = CBool(CType(row.FindControl("IsProgramManagerCB"), CheckBox).Checked)
                .IsActive = CBool(CType(row.FindControl("IsActiveCB"), CheckBox).Checked)
                .IsDeleted = CBool(CType(row.FindControl("IsDeletedCB"), CheckBox).Checked)
            End With
            FltRoleManager.Update(_FltRole, _fltIdentity)
            _FltRole = Nothing
            gv.EditIndex = -1
            RolesGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub RolesGridView_RowDeleting(ByVal sender As System.Object, ByVal e As GridViewDeleteEventArgs) Handles RolesGridView.RowDeleting
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            Dim RoleId As String = CType(row.FindControl("RoleIdLB"), Label).Text
            _FltRole = FltRoleManager.GetItem(RoleId, _fltIdentity)
            FltRoleManager.Delete(_FltRole, _fltIdentity)
            RolesGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub
    Protected Sub RolesGridView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RolesGridView.SelectedIndexChanged
        Try
            _FltRole = FltRoleManager.GetItem(CInt(Me.RolesGridView.SelectedValue), _fltIdentity)
            Me.RolesMultiView.ActiveViewIndex = 2
            Me.ProgNameLabel.Text = Me.ProgramDDL.SelectedItem.Text
            Me.RoleIdHiddenField.Value = _FltRole.RoleId
            Me.RoleNameLabel.Text = _FltRole.RoleName
            UsersInRoleGridView_DataBind()
            UsersNotInRoleGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub ProgramDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProgramDDL.SelectedIndexChanged
        RolesGridView_DataBind()
    End Sub

    Protected Sub IsActiveDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsActiveDDL.SelectedIndexChanged
        RolesGridView_DataBind()
    End Sub

    Protected Sub IsDeletedDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsDeletedDDL.SelectedIndexChanged
        RolesGridView_DataBind()
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click
        Me.AddEditTitleLabel.Text = "Adding New Item"
        Try
            _FltRole = New FltRole()
            LoadDataToForm()
            Me.RolesMultiView.ActiveViewIndex = 1
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

#End Region

#Region "Protected Methods (For Roles Modify View)"

    Protected Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        LoadDataToObject()
        Try
            FltRoleManager.Save(_FltRole, _fltIdentity)
            _FltRole = Nothing
            Me.RolesMultiView.ActiveViewIndex = 0
            RolesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        _FltRole = Nothing
        ClearErrorFields()
        Me.RolesMultiView.ActiveViewIndex = 0
        RolesGridView_DataBind()
    End Sub

#End Region

#Region "Protected Methods (For Assign/Unassign Users to Roles)"

    Protected Sub UsersNotInRoleGridView_DataBind()
        UsersNotInRoleGridView.DataSource = FltUserInRoleManager.GetFltUserListNotForFltRole(Me.RoleIdHiddenField.Value, _fltIdentity)
        UsersNotInRoleGridView.DataBind()
    End Sub

    Protected Sub UsersInRoleGridView_DataBind()
        UsersInRoleGridView.DataSource = FltUserInRoleManager.GetFltUserListForFltRole(Me.RoleIdHiddenField.Value, _fltIdentity)
        UsersInRoleGridView.DataBind()
    End Sub

    Protected Sub UsersNotInRoleGridView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsersNotInRoleGridView.SelectedIndexChanged
        Dim UserNameToAssign As String = Me.UsersNotInRoleGridView.SelectedValue
        Dim RoleIdToAssign As Long = _FltRole.RoleId
        Me.RoleIdHiddenField.Value = _FltRole.RoleId
        Try
            FltUserInRoleManager.Insert(UserNameToAssign, RoleIdToAssign, _fltIdentity)
            UsersInRoleGridView_DataBind()
            UsersNotInRoleGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Protected Sub UsersInRoleGridView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsersInRoleGridView.SelectedIndexChanged
        Dim UserNameToUnassign As String = Me.UsersInRoleGridView.SelectedValue
        Dim RoleIdToUnassign As Long = _FltRole.RoleId
        Me.RoleIdHiddenField.Value = _FltRole.RoleId
        Try
            FltUserInRoleManager.Delete(UserNameToUnassign, RoleIdToUnassign, _fltIdentity)
            UsersInRoleGridView_DataBind()
            UsersNotInRoleGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub DoneButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DoneButton.Click
        Me.RolesMultiView.ActiveViewIndex = 0
    End Sub

#End Region

#Region "Private Procedures"

    Private Sub LoadDataToObject()

        _FltRole.ProgId = Me.ProgramDDL.SelectedValue
        _FltRole.RoleName = Server.HtmlDecode(Me.RoleNameTextBox.Text)
        _FltRole.RoleDescription = Server.HtmlDecode(Me.RoleDescriptionTextBox.Text)
        _FltRole.IsProgramManager = Me.IsProgramManagerCheckBox.Checked
        _FltRole.IsActive = Me.IsActiveCheckBox.Checked

    End Sub

    Private Sub LoadDataToForm()

        Me.RoleNameTextBox.Text = Server.HtmlEncode(_FltRole.RoleName)
        Me.RoleDescriptionTextBox.Text = Server.HtmlEncode(_FltRole.RoleDescription)
        Me.IsProgramManagerCheckBox.Checked = _FltRole.IsProgramManager
        Me.IsActiveCheckBox.Checked = _FltRole.IsActive

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
        If _FltRole.BrokenRules.Count <> 0 Then
            Me.BrokenRulesList.Visible = True
            For Each thisRule As BrokenRule In _FltRole.BrokenRules
                Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
            Next
        End If
    End Sub

#End Region

End Class