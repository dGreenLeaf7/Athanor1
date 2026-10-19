' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

	<Serializable()> _
	Public Class Location
	
        Private _locactionId As Integer
        Private _progId As Integer
        Private _timeZoneId As Integer
        Private _timeZoneCode As String
        Private _timeZoneOffset As Double
        Private _daylightSavingTime As Boolean
        Private _daylightSavingOffset As Double
        Private _daylightSavingBegin As String
        Private _daylightSavingEnd As String
		Private _locationName As String
		Private _isActive As Boolean
		Private _isDeleted As Boolean
		Private _utcDateCreated As DateTime
		Private _utcDateModified As DateTime
		Private _StampUserName As String
		Private _concurrencyId As String
		
		Private _brokenRulesList As BrokenRulesList = New BrokenRulesList()
		
        <DataObjectFieldAttribute(True, True, False)> _
        Public Property LocactionId() As Integer
            Get
                Return _locactionId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _locactionId = 0
                Else
                    _locactionId = value
                End If
            End Set
        End Property
		
		Public Property ProgId() as Integer
			Get
				Return _progId
			End Get
			Set(ByVal value As Integer)
				If (value = Nothing) Then 
					_progId = 0
				Else
					_progId = value
				End If
			End Set
		End Property

        Public Property TimeZoneId() As Integer
            Get
                Return _timeZoneId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _timeZoneId = 0
                Else
                    _timeZoneId = value
                End If
            End Set
        End Property

		Public Property TimeZoneCode() as String
			Get
				Return _timeZoneCode
			End Get
            Friend Set(ByVal value As String)
                If (value = Nothing) Then
                    _timeZoneCode = String.Empty
                Else
                    _timeZoneCode = value
                End If
            End Set
		End Property

        Public Property TimeZoneOffset() As Double
            Get
                Return _timeZoneOffset
            End Get
            Friend Set(ByVal value As Double)
                _timeZoneOffset = value
            End Set
        End Property

        Public Property DaylightSavingTime() As Boolean
            Get
                Return _daylightSavingTime
            End Get
            Friend Set(ByVal value As Boolean)
                _daylightSavingTime = value
            End Set
        End Property

        Public Property DaylightSavingOffset() As Double
            Get
                Return _daylightSavingOffset
            End Get
            Friend Set(ByVal value As Double)
                _daylightSavingOffset = value
            End Set
        End Property

        Public Property DaylightSavingBegin() As String
            Get
                If IsDate(_daylightSavingBegin) Then
                    Return Format(CDate(_daylightSavingBegin), "MM-dd-yyyy hh:mm:ss tt")
                Else
                    Return String.Empty
                End If
            End Get
            Friend Set(ByVal value As String)
                _daylightSavingBegin = value
            End Set
        End Property

        Public Property DaylightSavingEnd() As String
            Get
                If IsDate(_daylightSavingEnd) Then
                    Return Format(CDate(_daylightSavingEnd), "MM-dd-yyyy hh:mm:ss tt")
                Else
                    Return String.Empty
                End If
            End Get
            Friend Set(ByVal value As String)
                _daylightSavingEnd = value
            End Set
        End Property

        Public Property LocationName() As String
            Get
                Return _locationName
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _locationName = String.Empty
                Else
                    _locationName = value
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
			Me.LocactionId = 0
            Me.ProgId = 0
            Me.TimeZoneId = 0
            Me.TimeZoneCode = String.Empty
            Me.TimeZoneOffset = 0
            Me.DaylightSavingTime = False
            Me.DaylightSavingOffset = 0
            Me.DaylightSavingBegin = String.Empty
            Me.DaylightSavingEnd = String.Empty
			Me.LocationName = String.Empty
			Me.IsActive = True
			Me.IsDeleted = False
			Me.UtcDateCreated = Nothing
			Me.UtcDateModified = Nothing
			Me.StampUserName = String.Empty
			Me.ConcurrencyId = "0"
			_brokenRulesList = New BrokenRulesList
		End Sub
	
        Public Sub New(ByVal LocactionId As Integer, ByVal ProgId As Integer, ByVal TimeZoneId As Integer, ByVal TimeZoneCode As String, ByVal TimeZoneOffset As Double, ByVal DaylightSavingTime As Boolean, ByVal DaylightSavingOffset As Double, ByVal DaylightSavingBegin As String, ByVal DaylightSavingEnd As String, ByVal LocationName As String, ByVal IsActive As Boolean, ByVal IsDeleted As Boolean, ByVal UtcDateCreated As DateTime, ByVal UtcDateModified As DateTime, ByVal StampUserName As String, ByVal ConcurrencyId As String)
            Me.LocactionId = LocactionId
            Me.ProgId = ProgId
            Me.TimeZoneId = TimeZoneId
            Me.TimeZoneCode = TimeZoneCode
            Me.TimeZoneOffset = TimeZoneOffset
            Me.DaylightSavingTime = DaylightSavingTime
            Me.DaylightSavingOffset = DaylightSavingOffset
            Me.DaylightSavingBegin = DaylightSavingBegin
            Me.DaylightSavingEnd = DaylightSavingEnd
            Me.LocationName = LocationName
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
			If (_progId = Nothing) Then
				_brokenRulesList.Add(New BrokenRule("ProgId", "ProgId is required"))
			Else
				If Len(_progId) = 0 Then
					_brokenRulesList.Add(New BrokenRule("ProgId", "ProgId is required"))
				End If
			End If
            If (_timeZoneId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("TimeZoneId", "TimeZoneId is required"))
            Else
                If Len(_timeZoneId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("TimeZoneId", "TimeZoneCode is required"))
                End If
            End If
            If (_locationName = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("LocationName", "LocationName is required"))
            Else
                If Len(_locationName) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("LocationName", "LocationName is required"))
                End If
            End If
			If (_locationName.Length > 50) Then
				_brokenRulesList.Add(New BrokenRule("LocationName", "LocationName cannot be longer than 50"))
            End If
            If ObjectPropertyValidator.ValidateCommonString(_locationName) = False Then
                _brokenRulesList.Add(New BrokenRule("LocationName", "LocationName" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If

            Return (_brokenRulesList.Count = 0)

        End Function
		
	End Class
		
End Namespace
