' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO

Partial Public Class Logout
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("fltIdentity") = FltIdentityManager.LogOut(Session("fltIdentity"))

        Session.Clear()
        Session.Abandon()
        Response.Cookies("ASP.NET_SessionId").Value = String.Empty
        Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-20)

        Response.Redirect("~/Login.aspx")
    End Sub

End Class