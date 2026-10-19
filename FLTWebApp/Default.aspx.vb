' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO


Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim fltIdentity As FltIdentity = Nothing
        fltIdentity = Session("fltIdentity")
        If ((fltIdentity Is Nothing) Or (fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        Me.TitleLB.Text = System.Configuration.ConfigurationManager.AppSettings("AppTitle")
    End Sub

End Class