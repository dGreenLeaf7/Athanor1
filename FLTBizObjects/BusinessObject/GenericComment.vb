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
    Public Class GenericComment

        Private _sourceTable As String
        Private _sourceID As Integer
        Private _parentId As Integer
        Private _authorName As String
        Private _authorId As Integer
        Private _componentName As String
        Private _componentId As Integer
        Private _commentDate As Date
        Private _commentTime As String
        Private _commentDateTime_UTC As Date
        Private _commentDateTime_Local As Date
        Private _commentRating As Short
        Private _issueCodeId As Integer
        Private _issuecode As String
        Private _comments As String
        Private _commentGUID As Guid
        Private _isDeleted As Boolean
        Private _utcDateCreated As DateTime
        Private _utcDateModified As DateTime
        Private _StampUserName As String
        Private _concurrencyId As String

        Private _isDirty As Boolean

        Private _brokenRulesList As BrokenRulesList = New BrokenRulesList
        Private _errorsList As BrokenRulesList = New BrokenRulesList
        Private _warningsList As BrokenRulesList = New BrokenRulesList

        Public ReadOnly Property GenericCommentId() As String
            Get
                Return _sourceTable & Format(_sourceID, "000000000;-000000000")
            End Get
        End Property

        Public Property SourceTable() As String
            Get
                Return _sourceTable
            End Get
            Set(ByVal value As String)
                _sourceTable = value
            End Set
        End Property

        Public ReadOnly Property CommentType() As String
            Get
                Select Case LCase(_sourceTable)
                    Case "woengineercomment"
                        Return "WO Engineer"
                    Case "womechaniccomment"
                        Return "WO Svc Tech"
                    Case "flightcomment"
                        Return "Pilot"
                    Case "aircraftcomment"
                        Return "Aircraft"
                    Case Else
                        Return "Unknown"
                End Select
            End Get
        End Property

        Public Property SourceID() As Integer
            Get
                Return _sourceID
            End Get
            Set(ByVal value As Integer)
                _sourceID = value
            End Set
        End Property

        Public Property ParentId() As Integer
            Get
                Return _parentId
            End Get
            Set(ByVal value As Integer)
                _parentId = value
            End Set
        End Property

        Public Property AuthorName() As String
            Get
                Return _authorName
            End Get
            Set(ByVal value As String)
                _authorName = value
            End Set
        End Property

        Public Property AuthorID() As Integer
            Get
                Return _authorId
            End Get
            Set(ByVal value As Integer)
                _authorId = value
            End Set
        End Property

        Public Property ComponentName() As String
            Get
                Return _componentName
            End Get
            Set(ByVal value As String)
                _componentName = value
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

        Public Property CommentTime() As String
            Get
                Return _commentTime
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _commentTime = String.Empty
                Else
                    _commentTime = value
                End If
            End Set
        End Property

        Public Property CommentDateTime_UTC() As String
            Get
                Return Format(_commentDateTime_UTC, "MM/dd/yyyy HH:mm:ss.fffff")
            End Get
            Set(ByVal value As String)
                If (value <> Nothing) Then
                    If IsDate(value) Then
                        _commentDateTime_UTC = CDate(value)
                    Else
                        _commentDateTime_UTC = Nothing
                    End If
                Else
                    _commentDateTime_UTC = Nothing
                End If
            End Set
        End Property

        Public Property CommentDateTime_Local() As String
            Get
                Return Format(_commentDateTime_Local, "MM/dd/yyyy HH:mm:ss.fffff")
            End Get
            Set(ByVal value As String)
                If (value <> Nothing) Then
                    If IsDate(value) Then
                        _commentDateTime_Local = CDate(value)
                    Else
                        _commentDateTime_Local = Nothing
                    End If
                Else
                    _commentDateTime_Local = Nothing
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

        Public Property CommentGUID() As Guid
            Get
                Return _commentGUID
            End Get
            Set(ByVal value As Guid)
                If value = Nothing Then
                    _commentGUID = Guid.Empty
                Else
                    _commentGUID = value
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

        Public Property IsDirty() As Boolean
            Get
                Return _isDirty
            End Get
            Set(ByVal value As Boolean)
                _isDirty = value
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
            Me.SourceTable = "AircraftComment"
            Me.SourceID = 0
            Me.ParentId = 0
            Me.AuthorName = String.Empty
            Me.AuthorID = 0
            Me.ComponentId = 0
            Me.ComponentName = String.Empty
            Me.CommentDate = Nothing
            Me.CommentTime = String.Empty
            Me.CommentDateTime_UTC = Nothing
            Me.CommentDateTime_Local = Nothing
            Me.CommentRating = 0
            Me.IssueCodeId = 0
            Me.IssueCode = String.Empty
            Me.Comments = String.Empty
            Me.CommentGUID = Guid.Empty
            Me.IsDeleted = False
            Me.UtcDateCreated = Nothing
            Me.UtcDateModified = Nothing
            Me.StampUserName = String.Empty
            Me.ConcurrencyId = "0"

            Me.IsDirty = True

            _brokenRulesList = New BrokenRulesList
            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList
        End Sub

        Public Function Validate() As Boolean
            Return Validate(_warningsList)
        End Function

        Public Function Validate(ByVal WarningsList As BrokenRulesList) As Boolean

            Dim prevWarningsList As New BrokenRulesList
            For Each thisBrokenRule As BrokenRule In WarningsList
                prevWarningsList.Add(thisBrokenRule)
            Next
            _brokenRulesList = New BrokenRulesList

            If (_parentId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("ParentId", "ParentId is required", "01-04-001", False, False))
            Else
                If Len(_parentId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("ParentId", "ParentId is required", "01-04-001", False, False))
                End If
            End If
            If (_authorId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("AuthorId", "AuthorId is required", "01-04-002", False, False))
            Else
                If Len(_authorId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("AuthorId", "AuthorId is required", "01-04-002", False, False))
                End If
            End If
            If (_componentId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("ComponentId", "ComponentId is required", "01-04-003", False, False))
            Else
                If Len(_componentId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("ComponentId", "ComponentId is required", "01-04-003", False, False))
                End If
            End If
            If (_commentDate = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("CommentDate", "CommentDate is required", "01-04-004", False, False))
            ElseIf Not IsDate(_commentDate) Then
                _brokenRulesList.Add(New BrokenRule("CommentDate", "CommentDate is required", "01-04-004", False, False))
            End If
            If ((_commentRating < 1) Or (_commentRating > 10)) Then
                _brokenRulesList.Add(New BrokenRule("CommentRating", "CommentRating must be between 1 and 10 inclusive", "01-04-005", False, False))
            End If
            If (_comments = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("Comments", "Comments is required", "01-04-006", False, False))
            Else
                If Len(_comments) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("Comments", "Comments is required", "01-04-007", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("01-04-008") Then
                If (_comments.Length > 255) Then
                    _brokenRulesList.Add(New BrokenRule("Comments", "Comments cannot be longer than 255", "01-04-008", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("01-04-009") Then
                If (_comments.Length > 2048) Then
                    _brokenRulesList.Add(New BrokenRule("Comments", "Comments cannot be longer than 2048", "01-04-009", False, False))
                End If
            End If

            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

        End Function

    End Class

End Namespace
