' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Imports FLTBizObjects.FLT.Dal

Namespace FLT.BO

	<Serializable()> _
	Public Class WOMechanicComment
	
		Private _wOMechanicCommentsId As Long
        Private _workOrderId As Integer
        Private _componentId As Integer
        Private _component As String
        Private _commentDate As Date
        Private _commentRating As Short
        Private _issueCodeId As Integer
        Private _issueCode As String
        Private _comments As String
        Private _mechanicCommentGUID As Guid
        Private _isDeleted As Boolean
		Private _utcDateCreated As DateTime
		Private _utcDateModified As DateTime
		Private _StampUserName As String
		Private _concurrencyId As String
		
        Private _brokenRulesList As BrokenRulesList = Nothing
        Private _errorsList As BrokenRulesList = Nothing
        Private _warningsList As BrokenRulesList = Nothing

		<DataObjectFieldAttribute(True, True, False)> _
		Public Property WOMechanicCommentsId() as Long
			Get
				Return _wOMechanicCommentsId
			End Get
			Set(ByVal value As Long)
				If (value = Nothing) Then 
					_wOMechanicCommentsId = 0
				Else
					_wOMechanicCommentsId = value
				End If
			End Set
		End Property
		
		Public Property WorkOrderId() as Integer
			Get
				Return _workOrderId
			End Get
			Set(ByVal value As Integer)
				If (value = Nothing) Then 
					_workOrderId = 0
				Else
					_workOrderId = value
				End If
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

        Public Property MechanicCommentGUID() As Guid
            Get
                Return _mechanicCommentGUID
            End Get
            Set(ByVal value As Guid)
                If value = Nothing Then
                    _mechanicCommentGUID = Guid.Empty
                Else
                    _mechanicCommentGUID = value
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

		Public Property Comments() as String
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
			Me.WOMechanicCommentsId = 0
            Me.WorkOrderId = 0
            Me.ComponentId = 0
            Me.Component = String.Empty
            Me.CommentDate = Nothing
            Me.CommentRating = 0
            Me.IssueCodeId = 0
            Me.IssueCode = String.Empty
            Me.Comments = String.Empty
            Me.MechanicCommentGUID = Guid.Empty
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
            For Each thisWarning As BrokenRule In WarningsList
                prevWarningsList.Add(thisWarning)
            Next
            _brokenRulesList = New BrokenRulesList

            If (_workOrderId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("WorkOrderId", "WorkOrderId is required", "03-06-001", False, False))
            Else
                If Len(_workOrderId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("WorkOrderId", "WorkOrderId is required", "03-06-001", False, False))
                End If
            End If
            If (_componentId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("ComponentId", "ComponentId is required", "03-06-002", False, False))
            Else
                If Len(_componentId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("ComponentId", "ComponentId is required", "03-06-002", False, False))
                End If
            End If
            If (_commentDate = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("CommentDate", "CommentDate is required", "03-06-003", False, False))
            ElseIf Not IsDate(_commentDate) Then
                _brokenRulesList.Add(New BrokenRule("CommentDate", "CommentDate is required", "03-06-003", False, False))
            End If
            If ((_commentRating < 1) Or (_commentRating > 10)) Then
                _brokenRulesList.Add(New BrokenRule("CommentRating", "CommentRating must be between 1 and 10 inclusive", "03-06-004", False, False))
            End If
            If (_comments = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("Comments", "Comments is required", "03-06-005", False, False))
            Else
                If Len(_comments) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("Comments", "Comments is required", "03-06-005", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("03-06-006") Then
                If (_comments.Length > 255) Then
                    _brokenRulesList.Add(New BrokenRule("Comments", "Comments cannot be longer than 255", "03-06-006", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("03-06-007") Then
                If (_comments.Length > 2048) Then
                    _brokenRulesList.Add(New BrokenRule("Comments", "Comments cannot be longer than 2048", "03-06-007", False, False))
                End If
            End If


            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

        End Function
		
	End Class
		
End Namespace
