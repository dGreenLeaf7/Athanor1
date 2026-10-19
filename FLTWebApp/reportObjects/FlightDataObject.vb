' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Public Class FlightDataObject

    Private _progName As String
    Private _aircraftId As Integer
    Private _msn As String
    Private _registrationNbr As String
    Private _flightId As Integer
    Private _flightDate As Date
    Private _shift As String
    Private _departureTime As String
    Private _returnTime As String
    Private _locationName As String
    Private _schedRouteName As String
    Private _flownRouteName As String
    Private _pilotName As String
    Private _meterStart As Decimal
    Private _meterEnd As Decimal
    Private _dailyHours As Decimal
    Private _fuel As Decimal
    Private _oil As Decimal
    Private _fluids As Decimal
    Private _component As String
    Private _pilotComments As String
    Private _rating As String

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
            _aircraftId = value
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

    Public Property Shift() As String
        Get
            Return _shift
        End Get
        Set(ByVal value As String)
            _shift = value
        End Set
    End Property

    Public Property DepartureTime() As String
        Get
            Return _departureTime
        End Get
        Set(ByVal value As String)
            _departureTime = value
        End Set
    End Property

    Public Property ReturnTime() As String
        Get
            Return _returnTime
        End Get
        Set(ByVal value As String)
            _returnTime = value
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

    Public Property SchedRouteName() As String
        Get
            Return _schedRouteName
        End Get
        Set(ByVal value As String)
            _schedRouteName = value
        End Set
    End Property

    Public Property FlownRouteName() As String
        Get
            Return _flownRouteName
        End Get
        Set(ByVal value As String)
            _flownRouteName = value
        End Set
    End Property

    Public Property PilotName() As String
        Get
            Return _pilotName
        End Get
        Set(ByVal value As String)
            _pilotName = value
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

    Public Property Fuel() As Decimal
        Get
            Return _fuel
        End Get
        Set(ByVal value As Decimal)
            _fuel = value
        End Set
    End Property

    Public Property Oil() As Decimal
        Get
            Return _oil
        End Get
        Set(ByVal value As Decimal)
            _oil = value
        End Set
    End Property

    Public Property Fluids() As Decimal
        Get
            Return _fluids
        End Get
        Set(ByVal value As Decimal)
            _fluids = value
        End Set
    End Property

    Public Property Component() As String
        Get
            Return _component
        End Get
        Set(ByVal value As String)
            _component = value
        End Set
    End Property

    Public Property PilotComments() As String
        Get
            Return _pilotComments
        End Get
        Set(ByVal value As String)
            _pilotComments = value
        End Set
    End Property

    Public Property Rating() As String
        Get
            Return _rating
        End Get
        Set(ByVal value As String)
            _rating = value
        End Set
    End Property

    Public Sub New()

        _progName = String.Empty
        _aircraftId = 0
        _msn = String.Empty
        _registrationNbr = String.Empty
        _flightId = 0
        _flightDate = Nothing
        _shift = String.Empty
        _departureTime = String.Empty
        _returnTime = String.Empty
        _locationName = String.Empty
        _schedRouteName = String.Empty
        _flownRouteName = String.Empty
        _pilotName = String.Empty
        _meterStart = 0
        _meterEnd = 0
        _dailyHours = 0
        _fuel = 0
        _oil = 0
        _fluids = 0
        _component = String.Empty
        _pilotComments = String.Empty
        _rating = String.Empty

    End Sub

End Class
