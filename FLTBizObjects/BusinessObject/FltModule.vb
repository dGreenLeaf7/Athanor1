' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Text.RegularExpressions

Namespace FLT.BO

    <Serializable()> _
    Public Class FltModule

        Private _moduleName As String
        Private _moduleDescription As String
        Private _isActive As Boolean
        Private _isDeleted As Boolean
        Private _utcDateCreated As DateTime
        Private _utcDateModified As DateTime
        Private _StampUserName As String
        Private _concurrencyId As String

        Private _brokenRulesList As BrokenRulesList = New BrokenRulesList()

        <DataObjectFieldAttribute(True, True, False)> _
        Public Property ModuleName() As String
            Get
                Return _moduleName
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _moduleName = String.Empty
                Else
                    _moduleName = value
                End If
            End Set
        End Property

        Public Property ModuleDescription() As String
            Get
                Return _moduleDescription
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _moduleDescription = String.Empty
                Else
                    _moduleDescription = value
                End If
            End Set
        End Property

        Public Property IsActive() As Boolean
            Get
                Return _isActive
            End Get
            Set(ByVal value As Boolean)
                _isActive = value
            End Set
        End Property

        Public Property IsDeleted() As Boolean
            Get
                Return _isDeleted
            End Get
            Set(ByVal value As Boolean)
                _isDeleted = value
            End Set
        End Property

        Public Property UtcDateCreated() As Date
            Get
                Return _utcDateCreated
            End Get
            Set(ByVal value As Date)
                If (value = Nothing) Then
                    _utcDateCreated = Nothing
                Else
                    _utcDateCreated = value
                End If
            End Set
        End Property

        Public Property UtcDateModified() As Date
            Get
                Return _utcDateModified
            End Get
            Set(ByVal value As Date)
                If (value = Nothing) Then
                    _utcDateModified = Nothing
                Else
                    _utcDateModified = value
                End If
            End Set
        End Property

        Public Property StampUserName() As String
            Get
                Return _StampUserName
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _StampUserName = String.Empty
                Else
                    _StampUserName = value
                End If
            End Set
        End Property

        Public Property ConcurrencyId() As String
            Get
                Return _concurrencyId
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _concurrencyId = String.Empty
                Else
                    _concurrencyId = value
                End If
            End Set
        End Property

        Public ReadOnly Property BrokenRules As BrokenRulesList
            Get
                Return _brokenRulesList
            End Get
        End Property

        Public Sub New()
            Me.ModuleName = String.Empty
            Me.ModuleDescription = String.Empty
            Me.IsActive = True
            Me.IsDeleted = False
            Me.UtcDateCreated = Nothing
            Me.UtcDateModified = Nothing
            Me.StampUserName = String.Empty
            Me.ConcurrencyId = "0"
            _brokenRulesList = New BrokenRulesList
        End Sub

        Public Sub New(ByVal ModuleName As String, ByVal ModuleDescription As String, ByVal IsActive As Boolean, ByVal IsDeleted As Boolean, ByVal UtcDateCreated As DateTime, ByVal UtcDateModified As DateTime, ByVal StampUserName As String, ByVal ConcurrencyId As String)
            Me.ModuleName = ModuleName
            Me.ModuleDescription = ModuleDescription
            Me.IsActive = IsActive
            Me.IsDeleted = IsDeleted
            Me.UtcDateCreated = UtcDateCreated
            Me.UtcDateModified = UtcDateModified
            Me.StampUserName = StampUserName
            Me.ConcurrencyId = ConcurrencyId
            _brokenRulesList = New BrokenRulesList
        End Sub

        Public Function Validate() As Boolean
            _brokenRulesList = New BrokenRulesList()
            If (_moduleName.Length > 50) Then
                _brokenRulesList.Add(New BrokenRule("ModuleName", "ModuleName cannot be longer than 50"))
            End If
            If ObjectPropertyValidator.ValidateCommonString(_moduleName) = False Then
                _brokenRulesList.Add(New BrokenRule("ModuleName", "ModuleName" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If
            If (_moduleDescription <> Nothing) Then
            If (_moduleDescription.Length > 255) Then
                _brokenRulesList.Add(New BrokenRule("ModuleDescription", "ModuleDescription cannot be longer than 255"))
            End If
            End If
            If ObjectPropertyValidator.ValidateCommonString(_moduleDescription) = False Then
                _brokenRulesList.Add(New BrokenRule("ModuleDescription", "ModuleDescription" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If

            Return (_brokenRulesList.Count = 0)

        End Function

    End Class

End Namespace
