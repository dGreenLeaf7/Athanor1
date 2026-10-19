' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data.SqlTypes
Imports System.Diagnostics
Imports System.Text.RegularExpressions

Namespace FLT.BO

    <Serializable()> _
    Public Class FlightEngineRun

        Const TimeRegex As String = "^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]?(\s)?(AM|am|Am|aM|PM|pm|Pm|pM))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]))$"

        Private _FlightEngineRunId As Integer
        Private _flightId As Integer
        Private _runStartTime As String
        Private _runEndTime As String
        Private _engineSpeed As Integer
        Private _runStartDateTime_Local As DateTime
        Private _runStartDateTime_UTC As DateTime
        Private _runEndDateTime_Local As DateTime
        Private _runEndDateTime_UTC As DateTime
        Private _FlightEngineRunGUID As Guid
        Private _isDeleted As Boolean
        Private _utcDateCreated As DateTime
        Private _utcDateModified As DateTime
        Private _StampUserName As String
        Private _concurrencyId As String

        Private _brokenRulesList As BrokenRulesList = Nothing
        Private _errorsList As BrokenRulesList = Nothing
        Private _warningsList As BrokenRulesList = Nothing

        <DataObjectFieldAttribute(True, True, False)> _
        Public Property FlightEngineRunId() As Integer
            Get
                Return _FlightEngineRunId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _FlightEngineRunId = 0
                Else
                    _FlightEngineRunId = value
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

        Public Property RunStartTime() As String
            Get
                Return _runStartTime
            End Get
            Set(ByVal value As String)
                _runStartTime = value
            End Set
        End Property

        Public Property RunEndTime() As String
            Get
                Return _runEndTime
            End Get
            Set(ByVal value As String)
                _runEndTime = value
            End Set
        End Property

        Public Property EngineSpeed() As String
            Get
                Return Format(_engineSpeed, "#,##0")
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _engineSpeed = 0
                ElseIf IsNumeric(value) Then
                    _engineSpeed = CInt(value)
                Else
                    _engineSpeed = 0
                End If
            End Set
        End Property

        Friend Property RunStartDateTime_Local() As DateTime
            Get
                Return _runStartDateTime_Local
            End Get
            Set(ByVal value As DateTime)
                _runStartDateTime_Local = value
            End Set
        End Property

        Friend Property RunStartDateTime_UTC() As DateTime
            Get
                Return _runStartDateTime_UTC
            End Get
            Set(ByVal value As DateTime)
                _runStartDateTime_UTC = value
            End Set
        End Property

        Friend Property RunEndDateTime_Local() As DateTime
            Get
                Return _runEndDateTime_Local
            End Get
            Set(ByVal value As DateTime)
                _runEndDateTime_Local = value
            End Set
        End Property

        Friend Property RunEndDateTime_UTC() As DateTime
            Get
                Return _runEndDateTime_UTC
            End Get
            Set(ByVal value As DateTime)
                _runEndDateTime_UTC = value
            End Set
        End Property

        Public Property FlightEngineRunGUID() As Guid
            Get
                Return _FlightEngineRunGUID
            End Get
            Set(ByVal value As Guid)
                If value = Nothing Then
                    _FlightEngineRunGUID = Guid.Empty
                Else
                    _FlightEngineRunGUID = value
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
            Me.FlightEngineRunId = 0
            Me.FlightId = 0
            Me.RunStartTime = String.Empty
            Me.RunEndTime = String.Empty
            Me.EngineSpeed = "0"
            Me.RunStartDateTime_Local = SqlDateTime.MinValue
            Me.RunStartDateTime_UTC = SqlDateTime.MinValue
            Me.RunEndDateTime_Local = SqlDateTime.MinValue
            Me.RunEndDateTime_UTC = SqlDateTime.MinValue
            Me.FlightEngineRunGUID = Guid.Empty
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
                _brokenRulesList.Add(New BrokenRule("FlightId", "FlightId is required", "02-03-001", False, False))
            Else
                If Len(_flightId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("FlightId", "FlightId is required", "02-03-001", False, False))
                End If
            End If
            If _runStartTime = String.Empty Then
                _brokenRulesList.Add(New BrokenRule("RunStartTime", "RunStartTime is required", "02-03-002", False, False))
            ElseIf Not (Regex.IsMatch(_runStartTime, TimeRegex)) Then
                _brokenRulesList.Add(New BrokenRule("RunStartTime", "RunStartTime must be in a valid time format", "02-03-003", False, False))
            End If
            If _runEndTime = String.Empty Then
                _brokenRulesList.Add(New BrokenRule("RunEndTime", "RunEndTime is required", "02-03-004", False, False))
            ElseIf Not (Regex.IsMatch(_runEndTime, TimeRegex)) Then
                _brokenRulesList.Add(New BrokenRule("RunEndTime", "RunEndTime must be in a valid time format", "02-03-005", False, False))
            End If
            If _runStartTime <> String.Empty And _runEndTime <> String.Empty Then
                If DateTime.Compare(CDate(_runStartTime), CDate(RunEndTime)) > 0 Then
                    _brokenRulesList.Add(New BrokenRule("RegEndTime", "RegEndTime cannot be earlier than RegBeginTime", "02-03-006", False, False))
                End If
            End If
            If (_engineSpeed = Nothing) Or (_engineSpeed = 0) Then
                _brokenRulesList.Add(New BrokenRule("EngineSpeed", "EngineSpeed is required", "02-03-007", False, False))
            End If

            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

        End Function

    End Class

End Namespace
