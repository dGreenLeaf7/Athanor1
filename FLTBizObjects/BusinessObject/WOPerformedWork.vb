' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Imports FLTBizObjects.FLT.Dal

Namespace FLT.BO

	<Serializable()> _
	Public Class WOPerformedWork
	
        Private _performedWorkId As Integer
		Private _workOrderId As Integer
        Private _performedWorkLine As String
        Private _performedWorkGUID As Guid
        Private _isDeleted As Boolean
		Private _utcDateCreated As DateTime
		Private _utcDateModified As DateTime
		Private _StampUserName As String
		Private _concurrencyId As String
		
        Private _brokenRulesList As BrokenRulesList = Nothing
        Private _errorsList As BrokenRulesList = Nothing
        Private _warningsList As BrokenRulesList = Nothing

        <DataObjectFieldAttribute(True, True, False)> _
        Public Property PerformedWorkId() As Integer
            Get
                Return _performedWorkId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _performedWorkId = 0
                Else
                    _performedWorkId = value
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

		Public Property PerformedWorkLine() as String
			Get
				Return _performedWorkLine
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_performedWorkLine = String.Empty
				Else
					_performedWorkLine = value
				End If
			End Set
		End Property

        Public Property PerformedWorkGUID() As Guid
            Get
                Return _performedWorkGUID
            End Get
            Set(ByVal value As Guid)
                If value = Nothing Then
                    _performedWorkGUID = Guid.Empty
                Else
                    _performedWorkGUID = value
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
			Me.PerformedWorkId = 0
			Me.WorkOrderId = 0
            Me.PerformedWorkLine = String.Empty
            Me.PerformedWorkGUID = Guid.Empty
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

            If (_workOrderId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("WorkOrderId", "WorkOrderId is required", "03-03-001", False, False))
            Else
                If Len(_workOrderId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("WorkOrderId", "WorkOrderId is required", "03-03-001", False, False))
                End If
            End If
            If (_performedWorkLine = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("PerformedWorkLine", "PerformedWorkLine is required", "03-03-002", False, False))
            Else
                If Len(_performedWorkLine) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("PerformedWorkLine", "PerformedWorkLine is required", "03-03-002", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("03-03-003") Then
                If (_performedWorkLine.Length > 255) Then
                    _brokenRulesList.Add(New BrokenRule("PerformedWorkLine", "PerformedWorkLine cannot be longer than 255", "03-03-003", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("03-03-004") Then
                If (_performedWorkLine.Length > 2048) Then
                    _brokenRulesList.Add(New BrokenRule("PerformedWorkLine", "PerformedWorkLine cannot be longer than 2048", "03-03-004", False, False))
                End If
            End If

            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

        End Function
		
	End Class
		
End Namespace
