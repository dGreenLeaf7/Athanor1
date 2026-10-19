' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

    <Serializable()> _
    Public Class fltLookupBase

        Private _id As Integer
        Private _lookupName As String
        Private _progId As Integer
        Private _stringValue As String
        Private _sortOrder As Integer
        Private _isActive As Boolean
        Private _concurrencyId As String

        Private _brokenRulesList As BrokenRulesList = New BrokenRulesList()

        Friend Sub InitializeObject()
            Me.ID = 0
            Me.LookupName = "Unknown"
            Me.ProgId = 0
            Me.StringValue = String.Empty
            Me.SortOrder = 0
            Me.IsActive = True
            Me.ConcurrencyId = "0"
            Me.BrokenRules = New BrokenRulesList
        End Sub

        <DataObjectFieldAttribute(True, True, False)> _
        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property

        Public Property LookupName() As String
            Get
                Return _lookupName
            End Get
            Set(ByVal value As String)
                _lookupName = value
            End Set
        End Property

        Public Property ProgId() As Integer
            Get
                Return _progId
            End Get
            Set(ByVal value As Integer)
                _progId = value
            End Set
        End Property

        Public Property StringValue() As String
            Get
                Return _stringValue
            End Get
            Set(ByVal value As String)
                _stringValue = value
            End Set
        End Property

        Public Property SortOrder() As Integer
            Get
                Return _sortOrder
            End Get
            Set(ByVal value As Integer)
                _sortOrder = value
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

        Public Property BrokenRules() As BrokenRulesList
            Get
                Return _brokenRulesList
            End Get
            Set(ByVal value As BrokenRulesList)
                _brokenRulesList = value
            End Set
        End Property

        Public Function Validate() As Boolean
            _brokenRulesList = New BrokenRulesList()
            If (_progId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("ProgId", "ProgId is required", "01-05-001", False, False))
            Else
                If Len(_progId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("ProgId", "ProgId is required", "01-05-001", False, False))
                End If
            End If
            If ObjectPropertyValidator.ValidateCommonString(_stringValue) = False Then
                _brokenRulesList.Add(New BrokenRule("StringValue", "StringValue" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If
            If (_stringValue = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("StringValue", "StringValue is required", "01-05-001", False, False))
            Else
                If Len(_stringValue) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("StringValue", "StringValue is required", "01-05-001", False, False))
                End If
            End If
            If (_stringValue.Length > 255) Then
                _brokenRulesList.Add(New BrokenRule("StringValue", "StringValue cannot be longer than 255", "01-05-001", False, False))
            End If
            If IsNumeric(_sortOrder) = False Then
                _brokenRulesList.Add(New BrokenRule("SortOrder", "SortOrder must be numeric."))
            End If

            AddExtraBusinessRules()

            Return (_brokenRulesList.Count = 0)

        End Function

        Friend Overridable Sub AddExtraBusinessRules()

        End Sub

        Public Sub New()
            InitializeObject()
        End Sub

        Public Sub New(ByVal Id As Integer, ByVal LookupName As String, ByVal ProgId As Integer, ByVal StringValue As String, ByVal SortOrder As Integer, ByVal IsActive As Boolean, ByVal ConcurrencyId As String)
            Me.ID = Id
            Me.LookupName = LookupName
            Me.ProgId = ProgId
            Me.StringValue = StringValue
            Me.SortOrder = SortOrder
            Me.IsActive = IsActive
            Me.ConcurrencyId = ConcurrencyId
            Me.BrokenRules = New BrokenRulesList
        End Sub

    End Class

End Namespace
