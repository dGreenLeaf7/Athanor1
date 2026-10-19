' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System.Web.SessionState
Imports System.ComponentModel
Imports System.Configuration
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports Contoso.Utilities

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Public Sub New()
        InitializeComponent()
    End Sub

    Private _components As System.ComponentModel.IContainer = Nothing

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        Logger.Initialize()
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        Dim fltIdentity As FLTIdentity = New FLTIdentity()
        Session("fltIdentity") = fltIdentity
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Private Sub InitializeComponent()
        Me._components = New System.ComponentModel.Container
    End Sub

End Class