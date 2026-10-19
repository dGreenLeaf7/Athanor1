' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System.Web.UI.HtmlControls
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO

Partial Public Class modules
    Inherits System.Web.UI.Page
    Private _fltIdentity As FltIdentity = Nothing
    Private _FltModule As FltModule = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        ClearErrorFields()
        If Not Me.IsPostBack Then
            modulesgridview_databind()
        Else
            _FltModule = CType(ViewState("_FltModule"), FltModule)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_FltModule") = _FltModule
    End Sub

    Protected Sub ModulesGridView_DataBind()
        Dim totRows As Integer = 0
        ModulesGridView.DataSource = FltModuleManager.GetList(Me.IsActiveDropDownList.SelectedValue, Me.IsDeletedDropDownList.SelectedValue, -1, -1, totRows, _fltIdentity)
        ModulesGridView.DataBind()
    End Sub

    Protected Sub ModulesGridView_PageIndexChanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles ModulesGridView.PageIndexChanging
        ModulesGridView.PageIndex = e.NewPageIndex
        ModulesGridView_DataBind()
    End Sub

    Protected Sub ModulesGridView_RowCommand(ByVal sender As System.Object, ByVal e As GridViewCommandEventArgs) Handles ModulesGridView.RowCommand

    End Sub

    Protected Sub ModulesGridView_RowEditing(ByVal sender As System.Object, ByVal e As GridViewEditEventArgs) Handles ModulesGridView.RowEditing
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.NewEditIndex)
            Dim moduleName As String = CType(row.FindControl("ModuleNameLB"), Label).Text
            _FltModule = FltModuleManager.GetItem(moduleName, _fltIdentity)
            gv.EditIndex = e.NewEditIndex
            ModulesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub ModulesGridView_RowCancelingEdit(ByVal sender As System.Object, ByVal e As GridViewCancelEditEventArgs) Handles ModulesGridView.RowCancelingEdit
        _FltModule = Nothing
        ModulesGridView.EditIndex = -1
        ModulesGridView_DataBind()
    End Sub

    Protected Sub ModulesGridView_RowUpdating(ByVal sender As System.Object, ByVal e As GridViewUpdateEventArgs) Handles ModulesGridView.RowUpdating

        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            Dim moduleName As String = Server.HtmlDecode(CType(row.FindControl("ModuleNameLB"), Label).Text)
            With _FltModule
                .ModuleDescription = Server.HtmlDecode(CType(row.FindControl("ModuleDescriptionTB"), TextBox).Text)
                .IsActive = CBool(CType(row.FindControl("IsActiveCB"), CheckBox).Checked)
            End With
            FltModuleManager.Update(_FltModule, _fltIdentity)
            _FltModule = Nothing
            gv.EditIndex = -1
            ModulesGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub ModulesGridView_RowDeleting(ByVal sender As System.Object, ByVal e As GridViewDeleteEventArgs) Handles ModulesGridView.RowDeleting
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim moduleName As String = gv.DataKeys(e.RowIndex).Value
            _FltModule = FltModuleManager.GetItem(moduleName, _fltIdentity)
            FltModuleManager.Delete(_FltModule, _fltIdentity)
            ModulesGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click
        Me.AddEditTitleLabel.Text = "Adding New Item"
        Try
            _FltModule = New FltModule()
            LoadDataToForm()
            Me.ModulesMultiView.ActiveViewIndex = 1
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        LoadDataToObject()
        Try
            FltModuleManager.Save(_FltModule, _fltIdentity)
            _FltModule = Nothing
            Me.ModulesMultiView.ActiveViewIndex = 0
            ModulesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        _FltModule = Nothing
        Me.ModulesMultiView.ActiveViewIndex = 0
        ModulesGridView_DataBind()
    End Sub

    Protected Sub IsActiveDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsActiveDropDownList.SelectedIndexChanged
        ModulesGridView_DataBind()
    End Sub

    Protected Sub IsDeletedDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsDeletedDropDownList.SelectedIndexChanged
        ModulesGridView_DataBind()
    End Sub

    Private Sub ClearErrorFields()
        Me.ErrorMessageLabel.Text = String.Empty
        Me.ErrorMessageLabel.Visible = False
        Me.SuccessMessageLabel.Text = String.Empty
        Me.SuccessMessageLabel.Visible = False
        Me.BrokenRulesList.Items.Clear()
        Me.BrokenRulesList.Visible = False
    End Sub

    Private Sub LoadDataToObject()

        _FltModule.ModuleName = Server.HtmlDecode(Me.ModuleNameTextBox.Text)
        _FltModule.ModuleDescription = Server.HtmlDecode(Me.ModuleDescriptionTextBox.Text)
        _FltModule.IsActive = Me.IsActiveCheckBox.Checked

    End Sub

    Private Sub LoadDataToForm()

        Me.ModuleNameTextBox.Text = Server.HtmlEncode(_FltModule.ModuleName)
        Me.ModuleDescriptionTextBox.Text = Server.HtmlEncode(_FltModule.ModuleDescription)
        Me.IsActiveCheckBox.Checked = _FltModule.IsActive

    End Sub

    Private Sub ProcessException(ByVal ex As Exception)
        Me.ErrorMessageLabel.Text = ex.Message
        Me.ErrorMessageLabel.Visible = True
        If _FltModule.BrokenRules.Count <> 0 Then
            Me.BrokenRulesList.Visible = True
            For Each thisRule As BrokenRule In _FltModule.BrokenRules
                Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
            Next
        End If
    End Sub

End Class