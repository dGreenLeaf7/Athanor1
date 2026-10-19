' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Data.SqlTypes
Imports System.Text.RegularExpressions

Imports FLTBizObjects.FLT.Dal

Namespace FLT.BO

    <Serializable()> _
    Public Class AircraftComment

        Private _aircraftCommentId As Integer
        Private _aircraftId As Integer
        Private _engineerId As Integer
        Private _engineerName As String
        Private _componentId As Integer
        Private _component As String
        Private _commentDate As Date
        Private _commentRating As Short
        Private _issueCodeId As Integer
        Private _issueCode As String
        Private _comments As String
        Private _aircraftCommentGUID As Guid
        Private _isDeleted As Boolean
        Private _utcDateCreated As DateTime
        Private _utcDateModified As DateTime
        Private _StampUserName As String
        Private _concurrencyId As String

        Private _brokenRulesList As BrokenRulesList = Nothing
        Private _errorsList As BrokenRulesList = Nothing
        Private _warningsList As BrokenRulesList = Nothing

        <DataObjectFieldAttribute(True, True, False)> _
        Public Property AircraftCommentId() As Integer
            Get
                Return _aircraftCommentId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _aircraftCommentId = 0
                Else
                    _aircraftCommentId = value
                End If
            End Set
        End Property

        Public Property AircraftId() As Integer
            Get
                Return _aircraftId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _aircraftId = 0
                Else
                    _aircraftId = value
                End If
            End Set
        End Property

        Public Property EngineerId() As Integer
            Get
                Return _engineerId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _engineerId = 0
                Else
                    _engineerId = value
                End If
            End Set
        End Property

        Public Property EngineerName() As String
            Get
                Return _engineerName
            End Get
            Set(ByVal value As String)
                _engineerName = value
            End Set
        End Property

        Public Property ComponentId() As Integer
            Get
                Return _componentId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _componentId = 0
                Else
                    _componentId = value
                End If
            End Set
        End Property

        Public Property Component() As String
            Get
                Return _component
            End Get
            Set(ByVal value As String)
                _component = value
            End Set
        End Property

        Public Property CommentDate() As String
            Get
                Return Format(_commentDate, "MM-dd-yyyy")
            End Get
            Set(ByVal value As String)
                If Not (value = Nothing) Then
                    If IsDate(value) Then
                        _commentDate = CDate(value)
                    Else
                        _commentDate = Nothing
                    End If
                Else
                    _commentDate = Nothing
                End If
            End Set
        End Property

        Public Property CommentRating() As Short
            Get
                Return _commentRating
            End Get
            Set(ByVal value As Short)
                _commentRating = value
            End Set
        End Property

        Public Property IssueCodeId() As Integer
            Get
                Return _issueCodeId
            End Get
            Set(ByVal value As Integer)
                _issueCodeId = value
            End Set
        End Property

        Public Property IssueCode() As String
            Get
                Return _issueCode
            End Get
            Set(ByVal value As String)
                _issueCode = value
            End Set
        End Property

        Public Property Comments() As String
            Get
                Return _comments
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _comments = String.Empty
                Else
                    _comments = value
                End If
            End Set
        End Property

        Public Property AircraftCommentGUID() As Guid
            Get
                Return _aircraftCommentGUID
            End Get
            Set(ByVal value As Guid)
                If value = Nothing Then
                    _aircraftCommentGUID = Guid.Empty
                Else
                    _aircraftCommentGUID = value
                End If
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
            Me.AircraftCommentId = 0
            Me.AircraftId = 0
            Me.EngineerId = 0
            Me.EngineerName = String.Empty
            Me.ComponentId = 0
            Me.Component = String.Empty
            Me.CommentDate = Nothing
            Me.CommentRating = 0
            Me.IssueCodeId = 0
            Me.IssueCode = String.Empty
            Me.Comments = String.Empty
            Me.IsDeleted = False
            Me.AircraftCommentGUID = Guid.Empty
            Me.UtcDateCreated = Nothing
            Me.UtcDateModified = Nothing
            Me.StampUserName = String.Empty
            Me.ConcurrencyId = "0"
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

            If (_aircraftId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("AircraftId", "AircraftId is required", "01-01-001", False, False))
            Else
                If Len(_aircraftId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("AircraftId", "AircraftId is required", "01-01-001", False, False))
                End If
            End If
            If (_engineerId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("EngineerId", "EngineerId is required", "01-01-002", False, False))
            Else
                If Len(_engineerId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("EngineerId", "EngineerId is required", "01-01-002", False, False))
                End If
            End If
            If (_componentId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("ComponentId", "ComponentId is required", "01-01-003", False, False))
            Else
                If Len(_componentId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("ComponentId", "ComponentId is required", "01-01-003", False, False))
                End If
            End If
            If (_commentDate = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("CommentDate", "CommentDate is required", "01-01-004", False, False))
            ElseIf Not IsDate(_commentDate) Then
                _brokenRulesList.Add(New BrokenRule("CommentDate", "CommentDate is required", "01-01-004", False, False))
            End If
            If ((_commentRating < 1) Or (_commentRating > 10)) Then
                _brokenRulesList.Add(New BrokenRule("CommentRating", "CommentRating must be between 1 and 10 inclusive", "01-01-005", False, False))
            End If
            If (_comments = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("Comments", "Comments is required", "01-01-006", False, False))
            Else
                If Len(_comments) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("Comments", "Comments is required", "01-01-007", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("01-01-008") Then
                If (_comments.Length > 255) Then
                    _brokenRulesList.Add(New BrokenRule("Comments", "Comments cannot be longer than 255", "01-01-008", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("01-01-009") Then
                If (_comments.Length > 2048) Then
                    _brokenRulesList.Add(New BrokenRule("Comments", "Comments cannot be longer than 2048", "01-01-009", False, False))
                End If
            End If

            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

        End Function

    End Class

End Namespace

