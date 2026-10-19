' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System.Web.UI.HtmlControls
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO

Partial Public Class users
    Inherits System.Web.UI.Page
#Region "Private Variables"

    Private _fltIdentity As FltIdentity = Nothing
    Private _FltUser As FltUser = Nothing

#End Region

#Region "Protected Methods (Page)"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        ClearErrorFields()
        If Not Me.IsPostBack Then
            UsersGridView_DataBind()
        Else
            _FltUser = CType(ViewState("_FltUser"), FltUser)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_FltUser") = _FltUser
    End Sub

#End Region

#Region "User List View'"

    Protected Sub UsersGridView_DataBind()
        Dim totRows As Integer = 0
        UsersGridView.DataSource = FltUserManager.GetList(Me.IsActiveDropDownList.SelectedValue, Me.IsDeletedDropDownList.SelectedValue, -1, -1, totRows)
        UsersGridView.DataBind()
    End Sub

    Protected Sub UsersGridView_PageIndexchanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles UsersGridView.PageIndexChanging
        Dim gv As GridView = CType(sender, GridView)
        gv.PageIndex = e.NewPageIndex
        UsersGridView_DataBind()
    End Sub

    Protected Sub UserGridView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsersGridView.SelectedIndexChanged
        Try
            _FltUser = FltUserManager.GetItem(UsersGridView.SelectedDataKey.Value)
            Me.UsersMultiView.ActiveViewIndex = 2
            Me.UserNameHiddenField.Value = Server.HtmlEncode(_FltUser.UserName)
            Me.UserNameLabel2.Text = Server.HtmlEncode(_FltUser.UserName)
            Me.DisplayNameLabel.Text = Server.HtmlEncode(_FltUser.DisplayName)

            RolesForUserGridView_DataBind()
            RolesNotForUserGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub UserGridView_RowDeleting(ByVal sender As System.Object, ByVal e As GridViewDeleteEventArgs) Handles UsersGridView.RowDeleting
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            Dim userName As String = CType(row.FindControl("UserNameLB"), Label).Text
            Dim fltUser As FltUser = FltUserManager.GetItem(userName)
            FltUserManager.Delete(fltUser, _fltIdentity)
            UsersGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub UsersGridView_RowEditing(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles UsersGridView.RowEditing
        Dim rIndex As Integer = e.NewEditIndex
        e.Cancel = True

        Dim thisUserName As String = Me.UsersGridView.DataKeys(rIndex).Value.ToString
        Me.AddEditTitleLabel.Text = "Editing Existing Item"
        Me.UserNameLabel.Visible = True
        Me.UserNameTextBox.Visible = False
        Try
            _FltUser = FltUserManager.GetItem(thisUserName)
            LoadDataToForm()
            Me.UsersMultiView.ActiveViewIndex = 1
            Me.ChangePasswordButton.Enabled = True
        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Protected Sub IsActiveDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsActiveDropDownList.SelectedIndexChanged
        UsersGridView_DataBind()
    End Sub

    Protected Sub IsDeletedDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsDeletedDropDownList.SelectedIndexChanged
        UsersGridView_DataBind()
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click
        Me.AddEditTitleLabel.Text = "Adding New Item"
        Me.UserNameLabel.Visible = False
        Me.UserNameTextBox.Visible = True
        Try
            _FltUser = New FltUser()
            LoadDataToForm()
            Me.UsersMultiView.ActiveViewIndex = 1
            Me.ChangePasswordButton.Enabled = False
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

#End Region

#Region "Protected Methods (User Modify View)"

    Protected Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        LoadDataToObject()
        Try
            FltUserManager.Save(_FltUser, _fltIdentity)
            _FltUser = Nothing
            Me.UsersMultiView.ActiveViewIndex = 0
            UsersGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        _FltUser = Nothing
        ClearErrorFields()
        UsersMultiView.ActiveViewIndex = 0
        usersgridview_DataBind()
    End Sub

    Protected Sub ChangePassword(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangePasswordButton.Click
        If _FltUser.ConcurrencyId <> "0" Then
            Try
                FltUserManager.GenerateNewPassword(_FltUser, _fltIdentity)
                Me.SuccessMessageLabel.Visible = True
                Me.SuccessMessageLabel.Text = "Password has been changed, Email has been sent."
                _FltUser = FltUserManager.GetItem(_FltUser.UserName)
                LoadDataToForm()
            Catch ex As Exception
                Me.ErrorMessageLabel.Visible = True
                Me.ErrorMessageLabel.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub UnlockUserAccount(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnlockUserAccountButton.Click
        If _FltUser.IsLockedOut = True Then
            Try
                _FltUser.IsLockedOut = False
                _FltUser.FailedPasswordAttemptCount = 0
                FltUserManager.Update(_FltUser, _fltIdentity)
                _FltUser = FltUserManager.GetItem(_FltUser.UserName)
                LoadDataToForm()
            Catch ex As Exception
                Me.ErrorMessageLabel.Visible = True
                Me.ErrorMessageLabel.Text = ex.Message
            End Try
        End If
    End Sub

#End Region

#Region "Protected Methods (Assign/Remove Roles)"

    Protected Sub RolesForUserGridView_DataBind()
        RolesForUserGridView.DataSource = FltUserInRoleManager.GetFltRoleListForFltUser(_FltUser.UserName, _fltIdentity)
        RolesForUserGridView.DataBind()
    End Sub

    Protected Sub RolesNotForUserGridView_DataBind()
        RolesNotForUserGridView.DataSource = FltUserInRoleManager.GetFltRoleListNotForFltUser(_FltUser.UserName, _fltIdentity)
        RolesNotForUserGridView.DataBind()
    End Sub

    Protected Sub DoneButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DoneButton.Click
        Me.UsersMultiView.ActiveViewIndex = 0
        UsersGridView_DataBind()
    End Sub

    Protected Sub RolesForUserGridView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RolesForUserGridView.SelectedIndexChanged
        Dim UserNameToUnassign As String = _FltUser.UserName
        Me.UserNameHiddenField.Value = _FltUser.UserName
        Dim RoleIdToUnassign As Long = Me.RolesForUserGridView.SelectedValue
        Try
            FltUserInRoleManager.Delete(UserNameToUnassign, RoleIdToUnassign, _fltIdentity)
            RolesForUserGridView_DataBind()
            RolesNotForUserGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub RolesNotForUserGridView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RolesNotForUserGridView.SelectedIndexChanged
        Dim UserNameToAssign As String = _FltUser.UserName
        Me.UserNameHiddenField.Value = _FltUser.UserName
        Dim RoleIdToAssign = Me.RolesNotForUserGridView.SelectedValue
        Try
            FltUserInRoleManager.Insert(UserNameToAssign, RoleIdToAssign, _fltIdentity)
            RolesForUserGridView_DataBind()
            RolesNotForUserGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

#End Region

    Private Sub PopulateDropDownLists()
        Dim totRows As Long
        Me.DefaultProgramDropDownList.Items.Clear()
        Me.DefaultProgramDropDownList.Items.Add(New ListItem("<Select a Program>", "0"))
        For Each thisProg As Program In ProgramManager.GetList(1, 0, -1, -1, totRows)
            Me.DefaultProgramDropDownList.Items.Add(New ListItem(thisProg.ProgName, thisProg.ProgID))
        Next
    End Sub

    Private Sub LoadDataToObject()

        _FltUser.UserName = Server.HtmlDecode(Me.UserNameTextBox.Text)
        _FltUser.DisplayName = Server.HtmlDecode(Me.DisplayNameTextBox.Text)
        _FltUser.Email = Server.HtmlDecode(Me.EmailTextBox.Text)
        _FltUser.Location = Server.HtmlDecode(Me.LocationTextBox.Text)
        _FltUser.CountryCode = Server.HtmlDecode(Me.CountryCodeTextBox.Text)
        _FltUser.PhoneNumber = Server.HtmlDecode(Me.PhoneNumberTextBox.Text)
        _FltUser.Department = Server.HtmlDecode(Me.DepartmentTextBox.Text)
        _FltUser.Title = Server.HtmlDecode(Me.TitleTextBox.Text)
        _FltUser.Company = Server.HtmlDecode(Me.CompanyTextBox.Text)
        _FltUser.DefaultProgId = Me.DefaultProgramDropDownList.SelectedValue
        _FltUser.IsSuperUser = Me.IsSuperUserCheckBox.Checked
        _FltUser.IsActive = Me.IsActiveCheckBox.Checked
        _FltUser.IsLockedOut = Me.IsLockedOutCheckBox.Checked

        Me.IsLockedOutCheckBox.Enabled = False

    End Sub

    Private Sub LoadDataToForm()

        PopulateDropDownLists()

        Me.UserNameLabel.Text = Server.HtmlEncode(_FltUser.UserName)
        Me.UserNameTextBox.Text = Server.HtmlEncode(_FltUser.UserName)
        Me.DisplayNameTextBox.Text = Server.HtmlEncode(_FltUser.DisplayName)
        Me.EmailTextBox.Text = Server.HtmlEncode(_FltUser.Email)
        Me.LocationTextBox.Text = Server.HtmlEncode(_FltUser.Location)
        Me.CountryCodeTextBox.Text = Server.HtmlEncode(_FltUser.CountryCode)
        Me.PhoneNumberTextBox.Text = Server.HtmlEncode(_FltUser.PhoneNumber)
        Me.DepartmentTextBox.Text = Server.HtmlEncode(_FltUser.Department)
        Me.TitleTextBox.Text = Server.HtmlEncode(_FltUser.Title)
        Me.CompanyTextBox.Text = Server.HtmlEncode(_FltUser.Company)
        Me.DefaultProgramDropDownList.SelectedValue = _FltUser.DefaultProgId
        Me.IsSuperUserCheckBox.Checked = _FltUser.IsSuperUser
        Me.IsActiveCheckBox.Checked = _FltUser.IsActive
        Me.IsLockedOutCheckBox.Checked = _FltUser.IsLockedOut

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
        If _FltUser.BrokenRules.Count <> 0 Then
            Me.BrokenRulesList.Visible = True
            For Each thisRule As BrokenRule In _FltUser.BrokenRules
                Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
            Next
        End If
    End Sub

    Protected Function GetProgName(ByVal ProgId As Long) As String
        Return CStr(ProgId)
    End Function

End Class