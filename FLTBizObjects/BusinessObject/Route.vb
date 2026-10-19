' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

	<Serializable()> _
	Public Class Route
	
        Private _routeId As Integer
		Private _locationId As Integer
		Private _name As String
        Private _description As String
        Private _timeZoneId As Integer
		Private _timeZoneCode As String
		Private _isActive As Boolean
		Private _isDeleted As Boolean
		Private _utcDateCreated As DateTime
		Private _utcDateModified As DateTime
		Private _StampUserName As String
		Private _concurrencyId As String
		
		Private _brokenRulesList As BrokenRulesList = New BrokenRulesList()
		
        <DataObjectFieldAttribute(True, True, False)> _
        Public Property RouteId() As Integer
            Get
                Return _routeId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _routeId = 0
                Else
                    _routeId = value
                End If
            End Set
        End Property
		
		Public Property LocationId() as Integer
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
		
		Public Property Name() as String
			Get
				Return _name
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_name = String.Empty
				Else
					_name = value
				End If
			End Set
		End Property
		
		Public Property Description() as String
			Get
				Return _description
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_description = String.Empty
				Else
					_description = value
				End If
			End Set
		End Property

        Public Property TimeZoneId() As Integer
            Get
                Return _timeZoneId
            End Get
            Set(ByVal value As Integer)
                _timeZoneId = value
            End Set
        End Property

		Public Property TimeZoneCode() as String
			Get
				Return _timeZoneCode
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_timeZoneCode = String.Empty
				Else
					_timeZoneCode = value
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
		
		Public Sub New()
			Me.RouteId = 0
			Me.LocationId = 0
			Me.Name = String.Empty
            Me.Description = String.Empty
            Me.TimeZoneId = 0
			Me.TimeZoneCode = String.Empty
			Me.IsActive = True
			Me.IsDeleted = False
			Me.UtcDateCreated = Nothing
			Me.UtcDateModified = Nothing
			Me.StampUserName = String.Empty
			Me.ConcurrencyId = "0"
			_brokenRulesList = New BrokenRulesList
		End Sub
	
        Public Sub New(ByVal RouteId As Integer, ByVal LocationId As Integer, ByVal Name As String, ByVal Description As String, ByVal TimeZoneId As Integer, ByVal TimeZoneCode As String, ByVal IsActive As Boolean, ByVal IsDeleted As Boolean, ByVal UtcDateCreated As DateTime, ByVal UtcDateModified As DateTime, ByVal StampUserName As String, ByVal ConcurrencyId As String)
            Me.RouteId = RouteId
            Me.LocationId = LocationId
            Me.Name = Name
            Me.Description = Description
            Me.TimeZoneId = TimeZoneId
            Me.TimeZoneCode = TimeZoneCode
            Me.IsActive = IsActive
            Me.IsDeleted = IsDeleted
            Me.UtcDateCreated = UtcDateCreated
            Me.UtcDateModified = UtcDateModified
            Me.StampUserName = StampUserName
            Me.ConcurrencyId = ConcurrencyId
            _brokenRulesList = New BrokenRulesList
        End Sub
	
		Public Function Validate() As Boolean
			_brokenRulesList = new BrokenRulesList()
			If (_locationId = Nothing) Then
				_brokenRulesList.Add(New BrokenRule("LocationId", "LocationId is required"))
			Else
                If _locationId <= 0 Then
                    _brokenRulesList.Add(New BrokenRule("LocationId", "LocationId is required"))
                End If
			End If
			If (_name = Nothing) Then
				_brokenRulesList.Add(New BrokenRule("Name", "Name is required"))
			Else
				If Len(_name) = 0 Then
					_brokenRulesList.Add(New BrokenRule("Name", "Name is required"))
				End If
			End If
			If (_name.Length > 50) Then
				_brokenRulesList.Add(New BrokenRule("Name", "Name cannot be longer than 50"))
            End If
            If ObjectPropertyValidator.ValidateCommonString(_name) = False Then
                _brokenRulesList.Add(New BrokenRule("Name", "Name" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If
			If (_description.Length > 255) Then
				_brokenRulesList.Add(New BrokenRule("Description", "Description cannot be longer than 255"))
            End If
            If ObjectPropertyValidator.ValidateCommonString(_description) = False Then
                _brokenRulesList.Add(New BrokenRule("Description", "Description" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If
            If (_timeZoneId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("TimeZoneId", "TimeZoneId is required"))
            Else
                If _timeZoneId <= 0 Then
                    _brokenRulesList.Add(New BrokenRule("TimeZoneId", "TimeZoneId is required"))
                End If
            End If
			
			Return (_brokenRulesList.Count = 0)
			
		End Function
		
	End Class
		
End Namespace
