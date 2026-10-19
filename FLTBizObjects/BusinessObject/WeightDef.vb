' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

    <Serializable()> _
    Public Class WeightDef

        Private _weightDefId As Integer = 0
        Private _progId As Integer = 0
        Private _weightDefTypeId As Integer = 0
        Private _weightDefTypeDescr As String = String.Empty
        Private _weightDefDescr As String = String.Empty
        Private _weight As Integer = 0
        Private _weightUnitId As Integer = 0
        Private _weightUnit As String = String.Empty
        Private _sortOrder As Integer = 0
        Private _isActive As Boolean = True
        Private _isDeleted As Boolean = False
        Private _weightDefGUID As Guid = Guid.Empty
        Private _utcDateCreated As DateTime
        Private _utcDateModified As DateTime
        Private _StampUserName As String
        Private _concurrencyId As String

        Private _brokenRulesList As BrokenRulesList = New BrokenRulesList
        Private _errorsList As BrokenRulesList = New BrokenRulesList
        Private _warningsList As BrokenRulesList = New BrokenRulesList

        Public Property WeightDefId() As Integer
            Get
                Return _weightDefId
            End Get
            Set(ByVal value As Integer)
                _weightDefId = value
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

        Public Property WeightDefTypeId() As Integer
            Get
                Return _weightDefTypeId
            End Get
            Set(ByVal value As Integer)
                _weightDefTypeId = value
            End Set
        End Property

        Public Property WeightDefTypeDescr() As String
            Get
                Return _weightDefTypeDescr
            End Get
            Set(ByVal value As String)
                _weightDefTypeDescr = value
            End Set
        End Property

        Public Property WeightDefDescr() As String
            Get
                Return _weightDefDescr
            End Get
            Set(ByVal value As String)
                _weightDefDescr = value
            End Set
        End Property

        Public Property Weight() As String
            Get
                Return Format(_weight, "#,##0")
            End Get
            Set(ByVal value As String)
                If IsNumeric(value) Then
                    _weight = CInt(value)
                Else
                    _weight = 0
                End If
            End Set
        End Property

        Public Property WeightUnitId() As Integer
            Get
                Return _weightUnitId
            End Get
            Set(ByVal value As Integer)
                _weightUnitId = value
            End Set
        End Property

        Public Property WeightUnit() As String
            Get
                Return _weightUnit
            End Get
            Set(ByVal value As String)
                _weightUnit = value
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

        Public Property WeightDefGuid() As Guid
            Get
                Return _weightDefGUID
            End Get
            Set(ByVal value As Guid)
                If (value = Nothing) Then
                    _weightDefGUID = Guid.Empty
                Else
                    _weightDefGUID = value
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

        Public ReadOnly Property BrokenRules() As BrokenRulesList
            Get
                Return _brokenRulesList
            End Get
        End Property

        Public ReadOnly Property ErrorsList() As BrokenRulesList
            Get
                Return _errorsList
            End Get
        End Property

        Public ReadOnly Property WarningsList() As BrokenRulesList
            Get
                Return _warningsList
            End Get
        End Property

        Public Sub New()
            Me.WeightDefId = 0
            Me.ProgId = 0
            Me.WeightDefTypeId = 0
            Me.WeightDefTypeDescr = String.Empty
            Me.WeightDefDescr = String.Empty
            Me.Weight = "0"
            Me.WeightUnitId = 0
            Me.WeightUnit = String.Empty
            Me.SortOrder = 0
            Me.WeightDefGuid = Guid.Empty
            Me.IsActive = True
            Me.IsDeleted = False
            Me.UtcDateCreated = Nothing
            Me.UtcDateModified = Nothing
            Me.StampUserName = String.Empty
            _brokenRulesList = New BrokenRulesList
            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList
        End Sub

        Public Function Validate() As Boolean
            Return Validate(_warningsList)
        End Function

        Friend Function Validate(ByVal WarningsList As BrokenRulesList) As Boolean

            Dim prevWarningsList As New BrokenRulesList
            For Each thisBrokenRule As BrokenRule In WarningsList
                prevWarningsList.Add(thisBrokenRule)
            Next
            _brokenRulesList = New BrokenRulesList

            If _progId = 0 Then
                _brokenRulesList.Add(New BrokenRule("ProgId", "ProgId value is required"))
            End If
            If _weightDefTypeId = 0 Then
                _brokenRulesList.Add(New BrokenRule("WeightDefTypeId", "Weight Tracking Type value is required"))
            End If
            If _weightDefDescr = String.Empty Then
                _brokenRulesList.Add(New BrokenRule("WeightDefDescr", "Description value is required"))
            End If
            If ObjectPropertyValidator.ValidateCommonString(_weightDefDescr) = False Then
                _brokenRulesList.Add(New BrokenRule("WeightDefDescr", "Description value" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If
            If _weight = 0 Then
                _brokenRulesList.Add(New BrokenRule("Weight", "Weight value is required"))
            End If
            If _weightUnitId = 0 Then
                _brokenRulesList.Add(New BrokenRule("WeightUnitId", "Weight Unit value is required"))
            End If

            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

        End Function

    End Class

End Namespace
