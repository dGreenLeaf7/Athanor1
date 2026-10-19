' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Public Class FlightStatisticObject

    Private _progName As String
    Private _locationId As Integer
    Private _locationName As String
    Private _flownRouteId As Integer
    Private _routeName As String
    Private _meterUnitId As Integer
    Private _meterUnit As String
    Private _flightCount As Integer
    Private _flightMinutesMin As Integer
    Private _flightMinutesMax As Integer
    Private _flightMinutesAvg As Integer
    Private _flightMinutesStDev As Integer
    Private _flightAirtimeMin As Decimal
    Private _flightAirtimeMax As Decimal
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

    Public Property FlownRouteId() As Integer
        Get
            Return _flownRouteId
        End Get
        Set(ByVal value As Integer)
            _flownRouteId = value
        End Set
    End Property

    Public Property RouteName() As String
        Get
            Return _routeName
        End Get
        Set(ByVal value As String)
            _routeName = value
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

    Public Property FlightCount() As Integer
        Get
            Return _flightCount
        End Get
        Set(ByVal value As Integer)
            _flightCount = value
        End Set
    End Property

    Public Property FlightMinutesMin() As Integer
        Get
            Return _flightMinutesMin
        End Get
        Set(ByVal value As Integer)
            _flightMinutesMin = value
        End Set
    End Property

    Public Property FlightMinutesMax() As Integer
        Get
            Return _flightMinutesMax
        End Get
        Set(ByVal value As Integer)
            _flightMinutesMax = value
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

    Public Property FlightAirtimeMin() As Decimal
        Get
            Return _flightAirtimeMin
        End Get
        Set(ByVal value As Decimal)
            _flightAirtimeMin = value
        End Set
    End Property

    Public Property FlightAirtimeMax() As Decimal
        Get
            Return _flightAirtimeMax
        End Get
        Set(ByVal value As Decimal)
            _flightAirtimeMax = value
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

    Public Property FlightairtimeStDev() As Decimal
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
        _flownRouteId = 0
        _routeName = String.Empty
        _meterUnitId = 0
        _meterUnit = String.Empty
        _flightCount = 0
        _flightMinutesMin = 0
        _flightMinutesMax = 0
        _flightMinutesAvg = 0
        _flightMinutesStDev = 0
        _flightAirtimeMin = 0
        _flightAirtimeMax = 0
        _flightAirtimeAvg = 0
        _flightAirtimeStDev = 0

    End Sub

End Class
