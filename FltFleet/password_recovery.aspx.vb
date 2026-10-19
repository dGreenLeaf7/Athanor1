' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO

Partial Public Class password_recovery
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Page.Title = System.Configuration.ConfigurationManager.AppSettings("AppTitle")
        Me.ErrorMessageLB.Text = String.Empty
    End Sub

    Protected Sub SendButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendButton.Click
        Try
            Dim fltUser As FltUser = FltUserManager.GetItem(Me.UserNameTB.Text)
            If Not fltUser Is Nothing Then
                Dim fltIdentity As New FltIdentity
                FltUserManager.GenerateNewPassword(fltUser, fltIdentity)
            End If
        Finally
            Me.PR_Multiview.ActiveViewIndex = 1
        End Try
    End Sub

End Class