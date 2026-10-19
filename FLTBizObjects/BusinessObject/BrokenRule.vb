' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System

Namespace FLT.BO

    <Serializable()> _
    Public Class BrokenRule

        Private _ruleIdentifier As String
        Private _propertyName As String = String.Empty
        Private _message As String = String.Empty
        Private _overRideableRule As Boolean = False
        Private _overRideRule As Boolean = False

        Public ReadOnly Property RuleIdentifier() As String
            Get
                Return _ruleIdentifier
            End Get
        End Property

        Public ReadOnly Property PropertyName() As String
            Get
                Return _propertyName
            End Get
        End Property

        Public ReadOnly Property Message() As String
            Get
                Return _message
            End Get
        End Property

        Public ReadOnly Property OverRideableRule() As Boolean
            Get
                Return _overRideableRule
            End Get
        End Property

        Public Property OverRideRule() As Boolean
            Get
                Return _overRideRule
            End Get
            Set(ByVal value As Boolean)
                If _overRideableRule Then
                    _overRideRule = value
                End If
            End Set
        End Property

        Public Sub New(ByVal PropertyName As String, ByVal Message As String)
            _ruleIdentifier = String.Empty
            _propertyName = PropertyName
            _message = Message
            _overRideableRule = False
            _overRideRule = False
        End Sub

        Public Sub New(ByVal PropertyName As String, ByVal Message As String, ByVal RuleIdentifier As String, ByVal OverRideableRule As Boolean, ByVal OverRideRule As Boolean)
            _ruleIdentifier = RuleIdentifier
            _propertyName = PropertyName
            _message = Message
            _overRideableRule = OverRideableRule
            _overRideRule = OverRideRule
        End Sub

    End Class

End Namespace
