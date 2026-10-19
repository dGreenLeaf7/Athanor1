' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System.Configuration

Namespace FLT.Dal

	<Serializable()> _
	Public Class AppConfiguration
		
		Public Shared  ReadOnly Property connectionString() As String
			Get
				Return ConfigurationManager.ConnectionStrings("FLT").connectionString
			End Get
		End Property
		
		Public Shared  ReadOnly Property providerName() As String
			Get
				Return ConfigurationManager.ConnectionStrings("FLT").providerName
			End Get
		End Property

        Public Shared ReadOnly Property BusinessRuleEnabled(ByVal RuleIdentifier) As Boolean
            Get
                Dim retValue As Boolean = True
                If Not (ConfigurationManager.AppSettings(RuleIdentifier) Is Nothing) Then
                    retValue = CBool(ConfigurationManager.AppSettings(RuleIdentifier).ToString.ToUpper <> "N")
                End If
                Return retValue
            End Get
        End Property

        Public Shared ReadOnly Property Lockout_Duration() As Integer
            Get
                Dim lockoutDuration As String

                lockoutDuration = ConfigurationManager.AppSettings.Get("LockoutDuration")

                If IsNumeric(lockoutDuration) Then
                    Return Convert.ToInt32(lockoutDuration)
                Else
                    Return 5
                End If
            End Get
        End Property

        Public Shared ReadOnly Property MaximumLoginFailuresAllowed() As Integer
            Get
                Dim maxLoginFailuresAllowed As String

                maxLoginFailuresAllowed = ConfigurationManager.AppSettings.Get("MaximumLoginFailuresAllowed")

                If IsNumeric(maxLoginFailuresAllowed) Then
                    Return Convert.ToInt32(maxLoginFailuresAllowed)
                Else
                    Return 3
                End If
            End Get
        End Property
    End Class

End Namespace
