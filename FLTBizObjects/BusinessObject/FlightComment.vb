' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data.SqlTypes
Imports System.Diagnostics
Imports System.Text.RegularExpressions
Imports FLTBizObjects.FLT.Dal


Namespace FLT.BO

	<Serializable()> _
	Public Class FlightComment
	
        Private Const TimeRegex As String = "^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]?(\s)?(AM|am|Am|aM|PM|pm|Pm|pM))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]))$"

        Private _flightCommentId As Integer
		Private _flightId As Integer
        Private _componentId As Integer
        Private _componentName As String
        Private _commentTime As String
        Private _commentDateTime_UTC As DateTime
        Private _commentDateTime_Local As DateTime
        Private _issueCodeId As Integer
        Private _issuecode As String
        Private _pilotComment As String
        Private _commentRating As Short
        Private _flightCommentGuid As Guid
		Private _isActive As Boolean
		Private _isDeleted As Boolean
		Private _utcDateCreated As DateTime
		Private _utcDateModified As DateTime
		Private _StampUserName As String
		Private _concurrencyId As String
		
        Private _brokenRulesList As BrokenRulesList = Nothing
        Private _errorsList As BrokenRulesList = Nothing
        Private _warningsList As BrokenRulesList = Nothing

        <DataObjectFieldAttribute(True, True, False)> _
        Public Property FlightCommentId() As Integer
            Get
                Return _flightCommentId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _flightCommentId = 0
                Else
                    _flightCommentId = value
                End If
            End Set
        End Property
		
		Public Property FlightId() as Integer
			Get
				Return _flightId
			End Get
			Set(ByVal value As Integer)
				If (value = Nothing) Then 
					_flightId = 0
				Else
					_flightId = value
				End If
			End Set
		End Property
		
		Public Property ComponentId() as Integer
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

        Public Property ComponentName() As String
            Get
                Return _componentName
            End Get
            Set(ByVal value As String)
                _componentName = value
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

        Friend Property CommentDateTime_UTC() As DateTime
            Get
                Return _commentDateTime_UTC
            End Get
            Set(ByVal value As DateTime)
                If (value = Nothing) Then
                    _commentDateTime_UTC = SqlDateTime.MinValue
                Else
                    _commentDateTime_UTC = value
                End If
            End Set
        End Property

        Friend Property CommentDateTime_Local() As DateTime
            Get
                Return _commentDateTime_Local
            End Get
            Set(ByVal value As DateTime)
                If (value = Nothing) Then
                    _commentDateTime_Local = SqlDateTime.MinValue
                Else
                    _commentDateTime_Local = value
                End If
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

		Public Property PilotComment() as String
			Get
				Return _pilotComment
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_pilotComment = String.Empty
				Else
					_pilotComment = value
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

        Public Property FlightCommentGUID() As Guid
            Get
                Return _flightCommentGuid
            End Get
            Set(ByVal value As Guid)
                If value = Nothing Then
                    _flightCommentGuid = Guid.Empty
                Else
                    _flightCommentGuid = value
                End If
            End Set
        End Property

		Public Property IsActive() as Boolean
			Get
				Return _isActive
			End Get
			Set(ByVal value As Boolean)
				_isActive = value
			End Set
		End Property
		
		Public Property IsDeleted() as Boolean
			Get
				Return _isDeleted
			End Get
			Set(ByVal value As Boolean)
				_isDeleted = value
			End Set
		End Property
		
		Public Property UtcDateCreated() as Date
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
		
		Public Property UtcDateModified() as Date
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
		
		Public Property StampUserName() as String
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
		
		Public Property ConcurrencyId() as String
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
			Me.FlightCommentId = 0
			Me.FlightId = 0
            Me.ComponentId = 0
            Me.ComponentName = String.Empty
            Me.CommentTime = String.Empty
            Me.CommentDateTime_UTC = SqlDateTime.MinValue
            Me.CommentDateTime_Local = SqlDateTime.MinValue
            Me.IssueCodeId = 0
            Me.IssueCode = String.Empty
            Me.PilotComment = String.Empty
            Me.CommentRating = 0
            Me.FlightCommentGUID = Guid.Empty
			Me.IsActive = True
			Me.IsDeleted = False
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

            If (_flightId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("FlightId", "FlightId is required", "02-02-001", False, False))
            Else
                If Len(_flightId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("FlightId", "FlightId is required", "02-02-001", False, False))
                End If
            End If
            If _componentId = Nothing Then
                _brokenRulesList.Add(New BrokenRule("ComponentId", "ComponentId is required", "02-02-008", False, False))
            Else
                If Len(_componentId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("ComponentId", "ComponentId is required", "02-02-008", False, False))
                End If
            End If

            If AppConfiguration.BusinessRuleEnabled("02-02-009") Then
                If (_commentTime = Nothing) Then
                    _brokenRulesList.Add(New BrokenRule("CommentTime", "CommentTime is required", "02-02-009", False, False))
                Else
                    If Len(_commentTime) = 0 Then
                        _brokenRulesList.Add(New BrokenRule("CommentTime", "CommentTime is required", "02-02-009", False, False))
                    End If
                End If
            End If
            If _commentTime.Length <> 0 Then
                If Len(_commentTime) > 8 Then
                    _brokenRulesList.Add(New BrokenRule("CommentTime", "CommentTime cannot be longer than 8", "02-02-003", False, False))
                End If
                If Not Regex.IsMatch(_commentTime, TimeRegex) Then
                    _brokenRulesList.Add(New BrokenRule("CommentTime", "CommentTime format is invalid", "02-02-004", False, False))
                End If
            End If
            If (_pilotComment = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("PilotComment", "PilotComment is required", "02-02-005", False, False))
            Else
                If Len(_pilotComment) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("PilotComment", "PilotComment is required", "02-02-005", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("02-02-006") Then
                If (_pilotComment.Length > 255) Then
                    _brokenRulesList.Add(New BrokenRule("PilotComment", "PilotComment cannot be longer than 255", "02-02-006", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("02-02-00A") Then
                If (_pilotComment.Length > 2048) Then
                    _brokenRulesList.Add(New BrokenRule("PilotComment", "PilotComment cannot be longer than 2048", "02-02-00A", False, False))
                End If
            End If
            If ((_commentRating < 1) Or (_commentRating > 10)) Then
                _brokenRulesList.Add(New BrokenRule("CommentRating", "CommentRating must be between 1 and 10 inclusive", "02-02-007", False, False))
            End If

            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

        End Function

    End Class
		
End Namespace
