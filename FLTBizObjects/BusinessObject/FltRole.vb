' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Text.RegularExpressions

Namespace FLT.BO

    <Serializable()> _
    Public Class FltRole

        Private _progId As Integer
        Private _progName As String
        Private _roleId As Long
        Private _roleName As String
        Private _roleDescription As String
        Private _isProgramManager As Boolean
        Private _isActive As Boolean
        Private _isDeleted As Boolean
        Private _utcDateCreated As DateTime
        Private _utcDateModified As DateTime
        Private _StampUserName As String
        Private _concurrencyId As String

        Private _brokenRulesList As BrokenRulesList = New BrokenRulesList()

        Public Property ProgId() As Integer
            Get
                Return _progId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _progId = 0
                Else
                    _progId = value
                End If
            End Set
        End Property

        Public Property ProgName() As String
            Get
                Return _progName
            End Get
            Set(ByVal value As String)
                _progName = value
            End Set
        End Property

        <DataObjectFieldAttribute(True, True, False)> _
        Public Property RoleId() As Long
            Get
                Return _roleId
            End Get
            Set(ByVal value As Long)
                If (value = Nothing) Then
                    _roleId = 0
                Else
                    _roleId = value
                End If
            End Set
        End Property

        Public Property RoleName() As String
            Get
                Return _roleName
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _roleName = String.Empty
                Else
                    _roleName = value
                End If
            End Set
        End Property

        Public Property RoleDescription() As String
            Get
                Return _roleDescription
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _roleDescription = String.Empty
                Else
                    _roleDescription = value
                End If
            End Set
        End Property

        Public Property IsProgramManager() As Boolean
            Get
                Return _isProgramManager
            End Get
            Set(ByVal value As Boolean)
                _isProgramManager = value
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
            Me.ProgId = 0
            Me.ProgName = String.Empty
            Me.RoleId = 0
            Me.RoleName = String.Empty
            Me.RoleDescription = String.Empty
            Me.IsProgramManager = True
            Me.IsActive = True
            Me.IsDeleted = False
            Me.UtcDateCreated = Nothing
            Me.UtcDateModified = Nothing
            Me.StampUserName = String.Empty
            Me.ConcurrencyId = "0"
            _brokenRulesList = New BrokenRulesList
        End Sub

        Public Function Validate() As Boolean
            _brokenRulesList = New BrokenRulesList()
            If (_progId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("ProgId", "ProgId is required"))
            Else
                If Len(_progId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("ProgId", "ProgId is required"))
                End If
            End If
            If (_roleName = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("RoleName", "RoleName is required"))
            Else
                If Len(_roleName) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("RoleName", "RoleName is required"))
                End If
            End If
            If (_roleName.Length > 50) Then
                _brokenRulesList.Add(New BrokenRule("RoleName", "RoleName cannot be longer than 50"))
            End If
            If ObjectPropertyValidator.ValidateCommonString(_roleName) = False Then
                _brokenRulesList.Add(New BrokenRule("RoleName", "RoleName" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If
            If (_roleDescription <> Nothing) Then
                If (_roleDescription.Length > 255) Then
                    _brokenRulesList.Add(New BrokenRule("RoleDescription", "RoleDescription cannot be longer than 255"))
                End If
            End If
            If ObjectPropertyValidator.ValidateCommonString(_roleDescription) = False Then
                _brokenRulesList.Add(New BrokenRule("RoleDescription", "RoleDescription" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If

            Return (_brokenRulesList.Count = 0)

        End Function

    End Class

End Namespace
