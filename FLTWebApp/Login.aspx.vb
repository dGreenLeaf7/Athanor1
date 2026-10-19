' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports Contoso.Utilities

Partial Public Class Login
    Inherits System.Web.UI.Page
    Private _loginTrys As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Page.Title = System.Configuration.ConfigurationManager.AppSettings("AppTitle")
        Me.Login1.Focus()
        CType(Login1.FindControl("Password"), TextBox).Attributes.Add("autocomplete", "off")
    End Sub

    Protected Sub Login1_Authenticate(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles Login1.Authenticate
        Dim fltIdentity As FLTIdentity = Nothing
        Try
            Me.Login1.PasswordRecoveryText = "Forgot Password?"
            fltIdentity = FltIdentityManager.AuthenticateUserLogin(Login1.UserName, Login1.Password)
            If fltIdentity Is Nothing Then
                Logger.Write("Login.aspx.vb\Login1_Authenticate: fltIdentity is nothing")
                Me.Login1.FailureText = "Failed Login Attempt - either Username or Password is incorrect"
            Else
                Session("fltIdentity") = fltIdentity
                Response.Redirect("~/default.aspx")
            End If
        Catch ex As Exception
            Logger.Write(ex)
            Me.Login1.FailureText = "Failed Login Attempt - either Username or Password is incorrect"
        End Try

    End Sub

End Class