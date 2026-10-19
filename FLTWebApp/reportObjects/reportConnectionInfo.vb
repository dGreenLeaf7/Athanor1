' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Public Class ReportConnectionInfo

    Public Property ServerName As String
    Public Property DatabaseName As String
    Public Property UserID As String
    Public Property Password As String
    Public Property IntegratedSecurity As Boolean

    Public Sub New()
        Me.ServerName = ConfigurationManager.AppSettings("ReportDBServerName").ToLower
        Me.DatabaseName = ConfigurationManager.AppSettings("ReportDBName").ToLower
        Me.UserID = ConfigurationManager.AppSettings("ReportUserId").ToLower
        Me.Password = ConfigurationManager.AppSettings("ReportPassword")
        If String.IsNullOrEmpty(ConfigurationManager.AppSettings("ReportIntegratedSecurity")) Then
            Me.IntegratedSecurity = False
        Else
            If ConfigurationManager.AppSettings("ReportIntegratedSecurity").ToLower = "true" Then
                Me.IntegratedSecurity = True
            Else
                Me.IntegratedSecurity = False
            End If
        End If
    End Sub

End Class
