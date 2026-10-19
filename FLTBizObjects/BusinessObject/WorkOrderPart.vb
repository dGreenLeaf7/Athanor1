' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Text.RegularExpressions
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BO

	<Serializable()> _
	Public Class WorkOrderPart

        Private Const WholeNumberRegExMask As String = "^\d{1,3}$"

        Private _workOrderPartId As Integer
		Private _workOrderId As Integer
		Private _partName As String
		Private _partDescription As String
		Private _fisIDRemoved As String
        Private _partNbrFailed As String
		Private _fisIDAdded As String
		Private _partModule As String
        Private _partNbrRemoved As String
        Private _partNbrAdded As String
        Private _isDeleted As Boolean
		Private _utcDateCreated As DateTime
		Private _utcDateModified As DateTime
		Private _StampUserName As String
		Private _concurrencyId As String
		
        Private _brokenRulesList As BrokenRulesList = Nothing
        Private _errorsList As BrokenRulesList = Nothing
        Private _warningsList As BrokenRulesList = Nothing

        <DataObjectFieldAttribute(True, True, False)> _
        Public Property WorkOrderPartId() As Integer
            Get
                Return _workOrderPartId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _workOrderPartId = 0
                Else
                    _workOrderPartId = value
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
		
		Public Property PartName() as String
			Get
				Return _partName
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_partName = String.Empty
				Else
					_partName = value
				End If
			End Set
		End Property
		
		Public Property PartDescription() as String
			Get
				Return _partDescription
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_partDescription = String.Empty
				Else
					_partDescription = value
				End If
			End Set
		End Property
		
		Public Property FisIDRemoved() as String
			Get
				Return _fisIDRemoved
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_fisIDRemoved = String.Empty
				Else
					_fisIDRemoved = value
				End If
			End Set
		End Property
		
        Public Property PartNbrFailed() As String
            Get
                Return _partNbrFailed
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _partNbrFailed = "0"
                Else
                    _partNbrFailed = value
                End If
            End Set
        End Property
		
		Public Property FisIDAdded() as String
			Get
				Return _fisIDAdded
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_fisIDAdded = String.Empty
				Else
					_fisIDAdded = value
				End If
			End Set
		End Property
		
		Public Property PartModule() as String
			Get
				Return _partModule
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_partModule = String.Empty
				Else
					_partModule = value
				End If
			End Set
		End Property
		
        Public Property PartNbrRemoved() As String
            Get
                Return _partNbrRemoved
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _partNbrRemoved = "0"
                Else
                    _partNbrRemoved = value
                End If
            End Set
        End Property
		
        Public Property PartNbrAdded() As String
            Get
                Return _partNbrAdded
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _partNbrAdded = "0"
                Else
                    _partNbrAdded = value
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
			Me.WorkOrderPartId = 0
			Me.WorkOrderId = 0
			Me.PartName = String.Empty
			Me.PartDescription = String.Empty
			Me.FisIDRemoved = String.Empty
            Me.PartNbrFailed = "0"
			Me.FisIDAdded = String.Empty
			Me.PartModule = String.Empty
            Me.PartNbrRemoved = "0"
            Me.PartNbrAdded = "0"
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
                _brokenRulesList.Add(New BrokenRule("WorkOrderId", "WorkOrderId is required", "03-05-001", False, False))
            Else
                If Len(_workOrderId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("WorkOrderId", "WorkOrderId is required", "03-05-001", False, False))
                End If
            End If
            If (_partName = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("PartName", "PartName is required", "03-05-002", False, False))
            Else
                If Len(_partName) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("PartName", "PartName is required", "03-05-002", False, False))
                End If
            End If
            If (_partName.Length > 20) Then
                _brokenRulesList.Add(New BrokenRule("PartName", "PartName cannot be longer than 20", "03-05-003", False, False))
            End If
            If (_partDescription.Length > 255) Then
                _brokenRulesList.Add(New BrokenRule("PartDescription", "PartDescription cannot be longer than 255", "03-05-003", False, False))
            End If
            If AppConfiguration.BusinessRuleEnabled("03-05-004") Then
                If (_fisIDRemoved = Nothing) Then
                    _brokenRulesList.Add(New BrokenRule("FisIDRemoved", "FisIDRemoved is required", "03-05-004", False, False))
                Else
                    If Len(_fisIDRemoved) = 0 Then
                        _brokenRulesList.Add(New BrokenRule("FisIDRemoved", "FisIDRemoved is required", "03-05-004", False, False))
                    End If
                End If
            End If
            If (_fisIDRemoved.Length > 20) Then
                _brokenRulesList.Add(New BrokenRule("FisIDRemoved", "FisIDRemoved cannot be longer than 20", "03-05-005", False, False))
            End If
            If AppConfiguration.BusinessRuleEnabled("03-05-00C") Then
                If (_partNbrFailed = Nothing) Then
                    _brokenRulesList.Add(New BrokenRule("PartNbrFailed", "PartNbrFailed is required", "03-05-006", False, False))
                Else
                    If Not Regex.IsMatch(_partNbrFailed, WholeNumberRegExMask) Then
                        _brokenRulesList.Add(New BrokenRule("PartNbrFailed", "PartNbrFailed must be a number", "03-05-00C", False, False))
                    End If
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("03-05-007") Then
                If (_fisIDAdded = Nothing) Then
                    _brokenRulesList.Add(New BrokenRule("FisIDAdded", "FisIDAdded is required", "03-05-007", False, False))
                Else
                    If Len(_fisIDAdded) = 0 Then
                        _brokenRulesList.Add(New BrokenRule("FisIDAdded", "FisIDAdded is required", "03-05-007", False, False))
                    End If
                End If
            End If
            If (_fisIDAdded.Length > 20) Then
                _brokenRulesList.Add(New BrokenRule("FisIDAdded", "FisIDAdded cannot be longer than 20", "03-05-008", False, False))
            End If
            If (_partModule.Length > 20) Then
                _brokenRulesList.Add(New BrokenRule("PartModule", "PartModule cannot be longer than 20", "03-05-009", False, False))
            End If
            If AppConfiguration.BusinessRuleEnabled("03-05-00D") Then
                If (_partNbrRemoved = Nothing) Then
                    _brokenRulesList.Add(New BrokenRule("PartNbrRemoved", "PartNbrRemoved is required", "03-05-00A", False, False))
                Else
                    If Not Regex.IsMatch(_partNbrRemoved, WholeNumberRegExMask) Then
                        _brokenRulesList.Add(New BrokenRule("PartNbrRemoved", "PartNbrRemoved must be a number", "03-05-00D", False, False))
                    End If
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("03-05-00E") Then
                If (_partNbrAdded = Nothing) Then
                    _brokenRulesList.Add(New BrokenRule("PartNbrAdded", "PartNbrAdded is required", "03-05-00B", False, False))
                Else
                    If Not Regex.IsMatch(_partNbrAdded, WholeNumberRegExMask) Then
                        _brokenRulesList.Add(New BrokenRule("PartNbrAdded", "PartNbrAdded must be a number", "03-05-00E", False, False))
                    End If
                End If
            End If


            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

        End Function
		
	End Class
		
End Namespace
