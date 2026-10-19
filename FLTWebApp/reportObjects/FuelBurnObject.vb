' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Public Class FuelBurnObject

    Private _progName As String
    Private _aircraftId As Integer
    Private _msn As String
    Private _registrationNbr As String
    Private _flightId As Integer
    Private _flightDate As Date
    Private _locationName As String
    Private _meterStart As Decimal
    Private _meterEnd As Decimal
    Private _dailyHours As Decimal
    Private _dailyFuel As Decimal

    Public Property ProgName() As String
        Get
            Return _progName
        End Get
        Set(ByVal value As String)
            _progName = value
        End Set
    End Property

    Public Property AircraftId() As Integer
        Get
            Return _aircraftId
        End Get
        Set(ByVal value As Integer)
            _aircraftId = 0
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

    Public Property RegistrationNbr() As String
        Get
            Return _registrationNbr
        End Get
        Set(ByVal value As String)
            _registrationNbr = value
        End Set
    End Property

    Public Property FlightId() As Integer
        Get
            Return _flightId
        End Get
        Set(ByVal value As Integer)
            _flightId = value
        End Set
    End Property

    Public Property FlightDate() As Date
        Get
            Return _flightDate
        End Get
        Set(ByVal value As Date)
            _flightDate = value
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

    Public Property MeterStart() As Decimal
        Get
            Return _meterStart
        End Get
        Set(ByVal value As Decimal)
            _meterStart = value
        End Set
    End Property

    Public Property MeterEnd() As Decimal
        Get
            Return _meterEnd
        End Get
        Set(ByVal value As Decimal)
            _meterEnd = value
        End Set
    End Property

    Public Property DailyHours() As Decimal
        Get
            Return _dailyHours
        End Get
        Set(ByVal value As Decimal)
            _dailyHours = value
        End Set
    End Property

    Public Property DailyFuel() As Decimal
        Get
            Return _dailyFuel
        End Get
        Set(ByVal value As Decimal)
            _dailyFuel = value
        End Set
    End Property

    Public Sub New()
        ProgName = String.Empty
        AircraftId = 0
        MSN = String.Empty
        RegistrationNbr = String.Empty
        FlightId = 0
        FlightDate = Nothing
        LocationName = String.Empty
        MeterStart = 0
        MeterEnd = 0
        DailyHours = 0
        DailyFuel = 0
    End Sub

End Class
