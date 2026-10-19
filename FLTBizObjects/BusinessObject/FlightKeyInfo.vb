' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

    <Serializable()> _
    Public Class FlightKeyInfo

        Private _flightId As Integer = 0

        Private _aircraftId As Integer = 0
        Private _locationId As Integer = 0
        Private _flightDateSort As String = String.Empty
        Private _workShiftId As Integer = 0

        Private _workShift As String = String.Empty
        Private _locationName As String = String.Empty
        Private _msn As String = String.Empty
        Private _serialNbr As String = String.Empty
        Private _registrationNbr As String = String.Empty
        Private _airframeHoursCurrent As Decimal = 0
        Private _airframeHoursOffset As Decimal = 0
        Private _meterUnitId As Integer = 0
        Private _meterUnit As String = String.Empty

        Private _validFlight As Boolean = False
        Private _validAircraft As Boolean = False
        Private _validLocation As Boolean = False
        Private _validAircraftLocation As Boolean = False
        Private _validWorkShift As Boolean = False

        Private _errorsList As New BrokenRulesList
        Private _warningsList As New BrokenRulesList

        <DataObjectFieldAttribute(True, True, False)> _
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

        Public Property AircraftId() As Integer
            Get
                Return _aircraftId
            End Get
            Set(ByVal value As Integer)
                _aircraftId = value
            End Set
        End Property

        Public Property LocationId() As Integer
            Get
                Return _locationId
            End Get
            Set(ByVal value As Integer)
                _locationId = value
            End Set
        End Property

        Public Property FlightDateSort() As String
            Get
                Return _flightDateSort
            End Get
            Set(ByVal value As String)
                If value = String.Empty Then
                    _flightDateSort = String.Empty
                ElseIf IsDate(value) Then
                    _flightDateSort = Format(CDate(value), "yyyy-MM-dd")
                Else
                    _flightDateSort = String.Empty
                End If
            End Set
        End Property

        Public Property WorkShiftId() As Integer
            Get
                Return _workShiftId
            End Get
            Set(ByVal value As Integer)
                _workShiftId = value
            End Set
        End Property

        Public Property WorkShift() As String
            Get
                Return _workShift
            End Get
            Set(ByVal value As String)
                _workShift = value
            End Set
        End Property

        Public Property MeterUnitId() As Integer
            Get
                Return _meterUnitId
            End Get
            Set(ByVal value As Integer)
                _meterUnitId = value
            End Set
        End Property

        Public Property MeterUnit() As String
            Get
                Return _meterUnit
            End Get
            Set(ByVal value As String)
                _meterUnit = value
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

        Public Property MSN() As String
            Get
                Return _msn
            End Get
            Set(ByVal value As String)
                _msn = value
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

        Public Property AirframeHoursCurrent() As Decimal
            Get
                Return _airframeHoursCurrent
            End Get
            Set(ByVal value As Decimal)
                _airframeHoursCurrent = value
            End Set
        End Property

        Public Property AirframeHoursOffset() As Decimal
            Get
                Return _airframeHoursOffset
            End Get
            Set(ByVal value As Decimal)
                _airframeHoursOffset = value
            End Set
        End Property

        Public Property ValidFlight() As Boolean
            Get
                Return _validFlight
            End Get
            Set(ByVal value As Boolean)
                _validFlight = value
            End Set
        End Property

        Public Property ValidAircraft() As Boolean
            Get
                Return _validAircraft
            End Get
            Set(ByVal value As Boolean)
                _validAircraft = value
            End Set
        End Property

        Public Property ValidLocation() As Boolean
            Get
                Return _validLocation
            End Get
            Set(ByVal value As Boolean)
                _validLocation = value
            End Set
        End Property

        Public Property ValidAircraftLocation() As Boolean
            Get
                Return _validAircraftLocation
            End Get
            Set(ByVal value As Boolean)
                _validAircraftLocation = value
            End Set
        End Property

        Public Property ValidWorkShift() As Boolean
            Get
                Return _validWorkShift
            End Get
            Set(ByVal value As Boolean)
                _validWorkShift = value
            End Set
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
            Me.FlightId = 0
            Me.AircraftId = 0
            Me.LocationId = 0
            Me.FlightDateSort = Nothing
            Me.WorkShiftId = 0
            Me.LocationName = String.Empty
            Me.WorkShift = String.Empty
            Me.MSN = String.Empty
            Me.SerialNbr = String.Empty
            Me.RegistrationNbr = String.Empty
            Me.AirframeHoursCurrent = 0
            Me.AirframeHoursOffset = 0
            Me.ValidFlight = False
            Me.ValidAircraft = False
            Me.ValidLocation = False
            Me.ValidAircraftLocation = False
            Me.ValidWorkShift = False

            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

        End Sub

    End Class

End Namespace
