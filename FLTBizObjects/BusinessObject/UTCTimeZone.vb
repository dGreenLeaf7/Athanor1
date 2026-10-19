' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

	<Serializable()> _
	Public Class UTCTimeZone

        Private _timeZoneId As String
		Private _timeZoneCode As String
        Private _timeZoneOffset As Double
		Private _timeZoneDescr As String
        Private _daylightSavingTime As Boolean
        Private _daylightSavingOffset As Double
        Private _daylightSavingBegin As String
        Private _daylightSavingEnd As String
        Private _isActive As Boolean
		Private _concurrencyId As String
		
		Private _brokenRulesList As BrokenRulesList = New BrokenRulesList()
		
        <DataObjectFieldAttribute(True, True, False)> _
        Public Property TimeZoneID() As Integer
            Get
                Return _timeZoneId
            End Get
            Set(ByVal value As Integer)
                _timeZoneId = value
            End Set
        End Property

        Public Property TimeZoneCode() As String
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
		
        Public Property TimeZoneOffset() As Double
            Get
                Return _timeZoneOffset
            End Get
            Set(ByVal value As Double)
                If (value = Nothing) Then
                    _timeZoneOffset = 0
                Else
                    _timeZoneOffset = value
                End If
            End Set
        End Property
		
		Public Property TimeZoneDescr() as String
			Get
				Return _timeZoneDescr
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_timeZoneDescr = String.Empty
				Else
					_timeZoneDescr = value
				End If
			End Set
		End Property

        Public Property DaylightSavingTime() As Boolean
            Get
                Return _daylightSavingTime
            End Get
            Set(ByVal value As Boolean)
                _daylightSavingTime = value
            End Set
        End Property

        Public Property DaylightSavingOffset() As Double
            Get
                Return _daylightSavingOffset
            End Get
            Set(ByVal value As Double)
                _daylightSavingOffset = value
            End Set
        End Property

        Public Property DaylightSavingBegin() As String
            Get
                If IsDate(_daylightSavingBegin) Then
                    Return Format(CDate(_daylightSavingBegin), "MM-dd-yyyy hh:mm:ss tt")
                Else
                    Return _daylightSavingBegin
                End If
            End Get
            Set(ByVal value As String)
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
            Set(ByVal value As String)
                _daylightSavingEnd = value
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
            Me.TimeZoneID = 0
            Me.TimeZoneCode = String.Empty
            Me.TimeZoneOffset = 0
            Me.DaylightSavingTime = False
            Me.DaylightSavingOffset = 0
            Me.DaylightSavingBegin = String.Empty
            Me.DaylightSavingEnd = String.Empty
            Me.TimeZoneDescr = String.Empty
            Me.IsActive = True
            Me.ConcurrencyId = "0"
            _brokenRulesList = New BrokenRulesList
        End Sub
	
        Public Sub New(ByVal TimeZoneId As Integer, ByVal TimeZoneCode As String, ByVal TimeZoneOffset As Double, ByVal TimeZoneDescr As String, ByVal DaylightSavingTime As Boolean, ByVal DaylightSavingOffset As Double, ByVal DaylightSavingBegin As String, ByVal DaylightSavingEnd As String, ByVal IsActive As Boolean, ByVal ConcurrencyId As String)
            Me.TimeZoneID = TimeZoneId
            Me.TimeZoneCode = TimeZoneCode
            Me.TimeZoneOffset = TimeZoneOffset
            Me.TimeZoneDescr = TimeZoneDescr
            Me.DaylightSavingTime = DaylightSavingTime
            Me.DaylightSavingOffset = DaylightSavingOffset
            Me.DaylightSavingBegin = DaylightSavingBegin
            Me.DaylightSavingEnd = DaylightSavingEnd
            Me.IsActive = IsActive
            Me.ConcurrencyId = ConcurrencyId
            _brokenRulesList = New BrokenRulesList
        End Sub
	
		Public Function Validate() As Boolean
            _brokenRulesList = New BrokenRulesList()
            If (_timeZoneCode.Length = 0) Then
                _brokenRulesList.Add(New BrokenRule("TimeZoneCode", "TimeZoneCode is required"))
            End If
            If (_timeZoneCode.Length > 10) Then
                _brokenRulesList.Add(New BrokenRule("TimeZoneCode", "TimeZoneCode cannot be longer than 10"))
            End If
            If ObjectPropertyValidator.ValidateCommonString(_timeZoneCode) = False Then
                _brokenRulesList.Add(New BrokenRule("TimeZoneCode", "TimeZoneCode" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If
            If (_timeZoneDescr = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("TimeZoneDescr", "TimeZoneDescr is required"))
            Else
                If Len(_timeZoneDescr) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("TimeZoneDescr", "TimeZoneDescr is required"))
                End If
            End If
            If (_timeZoneDescr.Length > 255) Then
                _brokenRulesList.Add(New BrokenRule("TimeZoneDescr", "TimeZoneDescr cannot be longer than 255"))
            End If
            If (_daylightSavingTime = True) Then
                If _daylightSavingOffset > 12 Or _daylightSavingOffset < -12 Then
                    _brokenRulesList.Add(New BrokenRule("DaylightSavingOffset", "DaylightSavingOffset must be between -12 and +12"))
                End If
                If Not IsDate(_daylightSavingBegin) Then
                    _brokenRulesList.Add(New BrokenRule("DaylightSavingBegin", "DaylightSavingBegin is a required date"))
                End If
                If Not IsDate(_daylightSavingEnd) Then
                    _brokenRulesList.Add(New BrokenRule("DaylightSavingEnd", "DaylightSavingEnd is a required date"))
                End If
                If IsDate(_daylightSavingBegin) And IsDate(DaylightSavingEnd) Then
                    If (DateTime.Compare(CDate(_daylightSavingEnd), CDate(_daylightSavingBegin)) < 0) Then
                        _brokenRulesList.Add(New BrokenRule("DaylightSavingEnd", "DaylightSavingEnd must not be earlier than DaylightSavingBegin"))
                    End If
                End If
            End If

            Return (_brokenRulesList.Count = 0)

        End Function
		
	End Class
		
End Namespace
