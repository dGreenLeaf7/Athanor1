' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

	<Serializable()> _
	Public Class AircraftLocation

        Private _aircraftLocationId As Integer
        Private _aircraftId As Integer
        Private _serialNbr As String
        Private _registrationNbr As String
        Private _msn As String
        Private _shipNbr As String
        Private _locationId As Integer
        Private _locationName As String
        Private _arrivalDate As String
        Private _requiredUsage As Decimal
		Private _accumUsage As Decimal
        Private _estEndOfTest As String
        Private _actEndOfTest As String
        Private _lastLocation As Boolean
        Private _aircraftLocationGUID As Guid
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
        Public Property AircraftLocationId() As Integer
            Get
                Return _aircraftLocationId
            End Get
            Set(ByVal value As Integer)
                _aircraftLocationId = value
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

        Public Property SerialNbr() As String
            Get
                Return _serialNbr
            End Get
            Set(ByVal value As String)
                _serialNbr = value
            End Set
        End Property

        Public Property RegistrationNbr() As String
            Get
                Return _registrationNbr
            End Get
            Set(ByVal value As String)
                _registrationNbr = value
            End Set
        End Property

        Public Property MSN() As String
            Get
                Return _msn
            End Get
            Set(ByVal value As String)
                _msn = value
            End Set
        End Property

        Public Property ShipNbr() As String
            Get
                Return _shipNbr
            End Get
            Set(ByVal value As String)
                _shipNbr = value
            End Set
        End Property

        Public Property LocationId() As Integer
            Get
                Return _locationId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _locationId = 0
                Else
                    _locationId = value
                End If
            End Set
        End Property

        Public Property LocationName() As String
            Get
                Return _locationName
            End Get
            Set(ByVal value As String)
                _locationName = value
            End Set
        End Property

        Public Property ArrivalDate() As String
            Get
                Return _arrivalDate
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _arrivalDate = String.Empty
                ElseIf IsDate(value) Then
                    _arrivalDate = Format(CDate(value), "MM-dd-yyyy")
                Else
                    _arrivalDate = value
                End If
            End Set
        End Property

        Public Property RequiredUsage() As String
            Get
                Return _requiredUsage
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _requiredUsage = 0
                ElseIf IsNumeric(value) Then
                    _requiredUsage = CDec(value)
                Else
                    _requiredUsage = 0
                End If
            End Set
        End Property
		
        Public Property AccumUsage() As String
            Get
                Return _accumUsage
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _accumUsage = 0
                ElseIf IsNumeric(value) Then
                    _accumUsage = CDec(value)
                Else
                    _accumUsage = 0
                End If
            End Set
        End Property
		
		Public Property EstEndOfTest() as String
			Get
				Return _estEndOfTest
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_estEndOfTest = String.Empty
                ElseIf IsDate(value) Then
                    _estEndOfTest = Format(CDate(value), "MM-dd-yyyy")
                Else
                    _estEndOfTest = value
                End If
			End Set
		End Property
		
		Public Property ActEndOfTest() as String
			Get
				Return _actEndOfTest
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_actEndOfTest = String.Empty
                ElseIf IsDate(value) Then
                    _actEndOfTest = Format(CDate(value), "MM-dd-yyyy")
                Else
                    _actEndOfTest = value
                End If
			End Set
		End Property
		
		Public Property LastLocation() as Boolean
			Get
				Return _lastLocation
			End Get
			Set(ByVal value As Boolean)
				_lastLocation = value
			End Set
		End Property

        Public Property AircraftLocationGUID() As Guid
            Get
                Return _aircraftLocationGUID
            End Get
            Set(ByVal value As Guid)
                If value = Nothing Then
                    _aircraftLocationGUID = Guid.Empty
                Else
                    _aircraftLocationGUID = value
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
            Me.AircraftLocationId = 0
            Me.AircraftId = 0
            Me.SerialNbr = String.Empty
            Me.RegistrationNbr = String.Empty
            Me.MSN = String.Empty
            Me.ShipNbr = String.Empty
            Me.LocationId = 0
            Me.LocationName = String.Empty
            Me.ArrivalDate = String.Empty
            Me.RequiredUsage = String.Empty
            Me.AccumUsage = String.Empty
            Me.EstEndOfTest = String.Empty
            Me.ActEndOfTest = String.Empty
            Me.LastLocation = False
            Me.AircraftLocationGUID = Guid.Empty
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

            If (_aircraftId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("AircraftId", "AircraftId is required", "01-03-001", False, False))
            Else
                If Len(_aircraftId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("AircraftId", "AircraftId is required", "01-03-001", False, False))
                End If
            End If
            If (_locationId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("LocationId", "LocationId is required", "01-03-002", False, False))
            Else
                If Len(_locationId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("LocationId", "LocationId is required", "01-03-002", False, False))
                End If
            End If
            If (_arrivalDate = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("ArrivalDate", "ArrivalDate is required", "01-03-003", False, False))
            Else
                If Len(_arrivalDate) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("ArrivalDate", "ArrivalDate is required", "01-03-003", False, False))
                Else
                    If Not IsDate(_arrivalDate) Then
                        _brokenRulesList.Add(New BrokenRule("ArrivalDate", "ArrivalDate must be a date value", "01-03-004", False, False))
                    End If
                End If
            End If
            If (_estEndOfTest.Length > 0) Then
                If Not IsDate(EstEndOfTest) Then
                    _brokenRulesList.Add(New BrokenRule("EstEndOfTest", "EstEndOfTest must be a date value", "01-03-005", False, False))
                End If
            End If
            If (_actEndOfTest.Length > 0) Then
                If Not IsDate(_actEndOfTest) Then
                    _brokenRulesList.Add(New BrokenRule("ActEndOfTest", "ActEndOfTest must be a date value", "01-03-006", False, False))
                End If
            End If

            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

        End Function
		
	End Class
		
End Namespace
