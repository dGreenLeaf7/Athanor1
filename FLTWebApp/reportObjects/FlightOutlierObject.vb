' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Public Class FlightOutlierObject

    Private _progName As String
    Private _locationId As Integer
    Private _locationName As String
    Private _schedRouteId As Integer
    Private _schedRouteName As String
    Private _flownRouteId As Integer
    Private _flownRouteName As String
    Private _aircraftId As Integer
    Private _msn As String
    Private _flightId As Integer
    Private _pilotId As Integer
    Private _pilotName As String
    Private _flightTimeOutlier As Boolean
    Private _flightAirtimeOutlier As Boolean
    Private _flightCount As Integer
    Private _flightDateSort As String
    Private _departureTime As Date
    Private _returnTime As Date
    Private _flightMinutes As Integer
    Private _flightMinutesAvg As Integer
    Private _flightMinutesStDev As Integer
    Private _hobbsStart As Decimal
    Private _hobbsEnd As Decimal
    Private _meterUnitId As Integer
    Private _meterUnit As String
    Private _flightAirtime As Decimal
    Private _flightAirtimeAvg As Decimal
    Private _flightAirtimeStDev As Decimal

    Public Property ProgName() As String
        Get
            Return _progName
        End Get
        Set(ByVal value As String)
            _progName = value
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

    Public Property LocationName() As String
        Get
            Return _locationName
        End Get
        Set(ByVal value As String)
            _locationName = value
        End Set
    End Property

    Public Property SchedRouteId() As Integer
        Get
            Return _schedRouteId
        End Get
        Set(ByVal value As Integer)
            _schedRouteId = value
        End Set
    End Property

    Public Property SchedRouteName() As String
        Get
            Return _schedRouteName
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property FlownRouteId() As Integer
        Get
            Return _flownRouteId
        End Get
        Set(ByVal value As Integer)
            _flownRouteId = value
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

    Public Property FlightId() As Integer
        Get
            Return _flightId
        End Get
        Set(ByVal value As Integer)
            _flightId = value
        End Set
    End Property

    Public Property PilotId() As Integer
        Get
            Return _pilotId
        End Get
        Set(ByVal value As Integer)
            _pilotId = value
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

    Public Property FlightTimeOutlier() As Boolean
        Get
            Return _flightTimeOutlier
        End Get
        Set(ByVal value As Boolean)
            _flightTimeOutlier = value
        End Set
    End Property

    Public Property FlightAirtimeOutlier() As Boolean
        Get
            Return _flightAirtimeOutlier
        End Get
        Set(ByVal value As Boolean)
            _flightAirtimeOutlier = value
        End Set
    End Property

    Public Property FlightCount() As Integer
        Get
            Return _flightCount
        End Get
        Set(ByVal value As Integer)
            _flightCount = value
        End Set
    End Property

    Public Property FlightDateSort() As String
        Get
            Return _flightDateSort
        End Get
        Set(ByVal value As String)
            _flightDateSort = value
        End Set
    End Property

    Public Property DepartureTime() As Date
        Get
            Return _departureTime
        End Get
        Set(ByVal value As Date)
            _departureTime = value
        End Set
    End Property

    Public Property ReturnTime() As Date
        Get
            Return _returnTime
        End Get
        Set(ByVal value As Date)
            _returnTime = value
        End Set
    End Property

    Public Property FlightMinutes() As Integer
        Get
            Return _flightMinutes
        End Get
        Set(ByVal value As Integer)
            _flightMinutes = value
        End Set
    End Property

    Public Property FlightMinutesAvg() As Integer
        Get
            Return _flightMinutesAvg
        End Get
        Set(ByVal value As Integer)
            _flightMinutesAvg = value
        End Set
    End Property

    Public Property FlightMinutesStDev() As Integer
        Get
            Return _flightMinutesStDev
        End Get
        Set(ByVal value As Integer)
            _flightMinutesStDev = value
        End Set
    End Property

    Public Property HobbsStart() As Decimal
        Get
            Return _hobbsStart
        End Get
        Set(ByVal value As Decimal)
            _hobbsEnd = value
        End Set
    End Property

    Public Property HobbsEnd() As Decimal
        Get
            Return _hobbsEnd
        End Get
        Set(ByVal value As Decimal)
            _hobbsEnd = value
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

    Public Property FlightAirtime() As Decimal
        Get
            Return _flightAirtime
        End Get
        Set(ByVal value As Decimal)
            _flightAirtime = value
        End Set
    End Property

    Public Property FlightAirtimeAvg() As Decimal
        Get
            Return _flightAirtimeAvg
        End Get
        Set(ByVal value As Decimal)
            _flightAirtimeAvg = value
        End Set
    End Property

    Public Property FlightAirtimeStDev() As Decimal
        Get
            Return _flightAirtimeStDev
        End Get
        Set(ByVal value As Decimal)
            _flightAirtimeStDev = value
        End Set
    End Property

    Public Sub New()

        _progName = String.Empty
        _locationId = 0
        _locationName = String.Empty
        _schedRouteId = 0
        _schedRouteName = String.Empty
        _flownRouteId = 0
        _flownRouteName = String.Empty
        _aircraftId = 0
        _msn = String.Empty
        _flightId = 0
        _pilotId = 0
        _pilotName = String.Empty
        _flightTimeOutlier = False
        _flightAirtimeOutlier = False
        _flightCount = 0
        _flightDateSort = String.Empty
        _departureTime = Nothing
        _returnTime = Nothing
        _flightMinutes = 0
        _flightMinutesAvg = 0
        _flightMinutesStDev = 0
        _hobbsStart = 0
        _hobbsEnd = 0
        _meterUnitId = 0
        _meterUnit = String.Empty
        _flightAirtime = 0
        _flightAirtimeAvg = 0
        _flightAirtimeStDev = 0

    End Sub

End Class
