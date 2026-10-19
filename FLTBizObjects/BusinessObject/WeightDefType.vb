' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

    <Serializable()> _
    Public Class WeightDefType

        Private _weightDefTypeId As Integer = 0
        Private _weightDefTypeDescr As String = String.Empty
        Private _sortOrder As Integer = 0
        Private _concurrencyId As String

        Private _brokenRulesList As BrokenRulesList = New BrokenRulesList
        Private _errorsList As BrokenRulesList = New BrokenRulesList
        Private _warningsList As BrokenRulesList = New BrokenRulesList

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

        Public Property SortOrder() As Integer
            Get
                Return _sortOrder
            End Get
            Set(ByVal value As Integer)
                _sortOrder = value
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
            Me.WeightDefTypeId = 0
            Me.WeightDefTypeDescr = String.Empty
            Me.SortOrder = 0
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

            If _weightDefTypeDescr = String.Empty Then
                _brokenRulesList.Add(New BrokenRule("WeightDefTypeDescr", "WeightDefTyepDescr value is required"))
            End If
            If ObjectPropertyValidator.ValidateCommonString(_weightDefTypeDescr) = False Then
                _brokenRulesList.Add(New BrokenRule("WeightDefTypeDescr", "WeightDefTypeDescr" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If


            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

        End Function

    End Class

End Namespace
