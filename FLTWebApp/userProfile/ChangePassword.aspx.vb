' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO

Partial Public Class ChangePassword
    Inherits System.Web.UI.Page
    Private _fltIdentity As FltIdentity = Nothing
    Private _fltUser As FltUser = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Me.IsPostBack Then
            _fltUser = FltUserManager.GetItem(_fltIdentity.UserName)
            Me.EmailTextBox.Text = Server.HtmlEncode(_fltUser.Email)
        Else
            _fltUser = CType(Session("_fltUser"), FltUser)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Session("_fltUser") = _fltUser
    End Sub

    Protected Sub ChangeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeButton.Click
        Me.ErrorMessageLabel.Text = String.Empty
        Me.BrokenRulesList.Items.Clear()
        Me.BrokenRulesTable.Visible = False
        Try
            _fltUser.Email = Server.HtmlDecode(Me.EmailTextBox.Text)
            FltUserManager.ChangePassword(_fltUser, Me.OldPasswordTextBox.Text, Me.NewPassword1TextBox.Text, Me.NewPassword2TextBox.Text, _fltIdentity)
            Me.ChangePasswordMultiview.ActiveViewIndex = 1
        Catch ex As Exception
            Me.ErrorMessageLabel.Text = ex.Message
            If _fltUser.BrokenRules.Count <> 0 Then
                Me.BrokenRulesTable.Visible = True
                For Each thisRule As BrokenRule In _fltUser.BrokenRules
                    Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
                Next
            End If
        End Try
    End Sub

End Class