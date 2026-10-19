' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO

Partial Public Class UserProfile
    Inherits System.Web.UI.Page
    Private _fltIdentity As FltIdentity = Nothing
    Private _fltUser As FltUser = Nothing

    Private Sub LoadManagerDropDownList()
        Dim TotalRows As Long
        Dim fltUserList As FltUserList = FltUserManager.GetList(1, 0, -1, -1, TotalRows)
        Me.ManagerDropDownList.Items.Clear()
        For Each thisUser As FltUser In fltUserList
            Me.ManagerDropDownList.Items.Add(New ListItem(thisUser.DisplayName, thisUser.UserName))
        Next
    End Sub

    Private Sub LoadProgramDropDownList()
        Dim TotalRows As Long
        Dim programList As ProgramList = ProgramManager.GetList(1, 0, -1, -1, TotalRows)
        Me.DefaultProgramDropDownList.Items.Clear()
        For Each thisProgram As Program In programList
            Me.DefaultProgramDropDownList.Items.Add(New ListItem(thisProgram.ProgName, thisProgram.ProgID))
        Next
    End Sub

    Private Sub ClearErrorFields()
        Me.ErrorMessageLabel.Text = String.Empty
        Me.ErrorMessageLabel.Visible = False
        Me.SuccessMessageLabel.Text = String.Empty
        Me.SuccessMessageLabel.Visible = False
        Me.BrokenRulesList.Items.Clear()
        Me.BrokenRulesList.Visible = False
    End Sub

    Private Sub LoadPage()

        ClearErrorFields()
        LoadManagerDropDownList()
        LoadProgramDropDownList()

        Me.UserNameLabel.Text = Server.HtmlEncode(_fltUser.UserName)
        Me.DisplayNameTextBox.Text = Server.HtmlEncode(_fltUser.DisplayName)
        Me.EmailTextBox.Text = Server.HtmlEncode(_fltUser.Email)
        Me.LocationTextBox.Text = Server.HtmlEncode(_fltUser.Location)
        Me.CountryCodeTextBox.Text = Server.HtmlEncode(_fltUser.CountryCode)
        Me.PhoneNumberTextBox.Text = Server.HtmlEncode(_fltUser.PhoneNumber)
        Me.ManagerDropDownList.SelectedValue = Server.HtmlEncode(_fltUser.MgrUserName)
        Me.DepartmentTextBox.Text = Server.HtmlEncode(_fltUser.Department)
        Me.TitleTextBox.Text = Server.HtmlEncode(_fltUser.Title)
        Me.CompanyTextBox.Text = Server.HtmlEncode(_fltUser.Company)
        Me.DefaultProgramDropDownList.SelectedValue = _fltUser.DefaultProgId

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Me.IsPostBack Then
            _fltUser = FltUserManager.GetItem(_fltIdentity.UserName)
            LoadPage()
        Else
            _fltUser = CType(Me.ViewState("_fltUser"), FltUser)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender
        Me.ViewState("_fltUser") = _fltUser
    End Sub

    Protected Sub SaveButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveButton.Click

        ClearErrorFields()

        Me.BrokenRulesList.Items.Clear()
        Me.BrokenRulesList.Visible = False

        _fltUser.DisplayName = Server.HtmlDecode(Me.DisplayNameTextBox.Text)
        _fltUser.Email = Server.HtmlDecode(Me.EmailTextBox.Text)
        _fltUser.Location = Server.HtmlDecode(Me.LocationTextBox.Text)
        _fltUser.CountryCode = Server.HtmlDecode(Me.CountryCodeTextBox.Text)
        _fltUser.PhoneNumber = Server.HtmlDecode(Me.PhoneNumberTextBox.Text)
        _fltUser.MgrUserName = Server.HtmlDecode(Me.ManagerDropDownList.SelectedValue)
        _fltUser.Department = Server.HtmlDecode(Me.DepartmentTextBox.Text)
        _fltUser.Title = Server.HtmlDecode(Me.TitleTextBox.Text)
        _fltUser.Company = Server.HtmlDecode(Me.CompanyTextBox.Text)
        _fltUser.DefaultProgId = Me.DefaultProgramDropDownList.SelectedValue

        Try
            FltUserManager.Update(_fltUser, _fltIdentity)
            Me.SuccessMessageLabel.Visible = True
            Me.SuccessMessageLabel.Text = "Record updated successfully!"

        Catch ex As Exception
            Me.ErrorMessageLabel.Text = ex.Message
            Me.ErrorMessageLabel.Visible = True
            If _fltUser.BrokenRules.Count <> 0 Then
                Me.BrokenRulesList.Visible = True
                For Each thisRule As BrokenRule In _fltUser.BrokenRules
                    Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
                Next
            End If

        End Try

        _fltUser = FltUserManager.GetItem(_fltUser.UserName)

    End Sub

End Class