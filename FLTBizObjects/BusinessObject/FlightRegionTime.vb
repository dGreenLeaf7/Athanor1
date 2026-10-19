' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

	<Serializable()> _
	Public Class FlightRegionTime
	
        Private _flightRegionTimeId As Integer
        Private _flightId As Integer
        Private _regionId As Integer
        Private _regionCode As String
        Private _hours As Decimal
        Private _flightRegionTimeGUID As Guid
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
        Public Property FlightRegionTimeId() As Integer
            Get
                Return _flightRegionTimeId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _flightRegionTimeId = 0
                Else
                    _flightRegionTimeId = value
                End If
            End Set
        End Property
		
        Public Property FlightId() As Integer
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

        Public Property RegionId() As Integer
            Get
                Return _regionId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _regionId = 0
                Else
                    _regionId = value
                End If
            End Set
        End Property

        Public Property RegionCode() As String
            Get
                Return _regionCode
            End Get
            Set(ByVal value As String)
                _regionCode = value
            End Set
        End Property

        Public Property Hours() As String
            Get
                Return Format(_hours, "#,##0.0")
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _hours = 0
                ElseIf IsNumeric(value) Then
                    _hours = CDec(value)
                Else
                    _hours = 0
                End If
            End Set
        End Property

        Public Property FlightRegionTimeGUID() As Guid
            Get
                Return _flightRegionTimeGUID
            End Get
            Set(ByVal value As Guid)
                If value = Nothing Then
                    _flightRegionTimeGUID = Guid.Empty
                Else
                    _flightRegionTimeGUID = value
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
			Me.FlightRegionTimeId = 0
			Me.FlightId = 0
            Me.RegionId = 0
            Me.RegionCode = String.Empty
            Me.Hours = "0.0"
            Me.FlightRegionTimeGUID = Guid.Empty
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

        Public Function Validate(ByVal WarningsList As BrokenRulesList) As Boolean

            Dim prevWarningsList As New BrokenRulesList
            For Each thisBrokenRule As BrokenRule In WarningsList
                prevWarningsList.Add(thisBrokenRule)
            Next
            _brokenRulesList = New BrokenRulesList

            If (_flightId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("FlightId", "FlightId is required", "02-04-001", False, False))
            Else
                If Len(_flightId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("FlightId", "FlightId is required", "02-04-001", False, False))
                End If
            End If
            If (_regionId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("RegionId", "RegionId is required", "02-04-002", False, False))
            Else
                If Len(_regionId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("RegionId", "RegionId is required", "02-04-002", False, False))
                End If
            End If
            If (_hours = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("Hours", "Hours is required", "02-04-003", False, False))
            Else
                If Len(_hours) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("Hours", "Hours is required", "02-04-003", False, False))
                End If
            End If

            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

        End Function
		
	End Class
		
End Namespace
