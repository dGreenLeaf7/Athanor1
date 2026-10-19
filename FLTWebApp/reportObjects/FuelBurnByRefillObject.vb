' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Public Class FuelBurnByRefillObject

    Dim _progName As String
    Dim _aircraftId As Integer
    Dim _msn As String
    Dim _registrationNbr As String
    Dim _flightDate As Date
    Dim _locationName As String
    Dim _workShiftName As String
    Dim _schedRouteName As String
    Dim _flownRouteName As String
    Dim _flightMeterSort As Decimal
    Dim _flightMeterStart As Decimal
    Dim _flightMeterEnd As Decimal
    Dim _flightAirtime As Decimal
    Dim _totalFlightAirtime As Decimal
    Dim _meterUnit As String
    Dim _liquidFillId As Integer
    Dim _refillMeterLast As Decimal
    Dim _refillMeterCurrent As Decimal
    Dim _refillAirtime As Decimal
    Dim _liquidGroup As String
    Dim _liquidAmt As Decimal
    Dim _liquidUnit As String
    Dim _usage As Decimal
    Dim _totalRefillAirtime As Decimal
    Dim _totalLiquidAmt As Decimal
    Dim _avgUsage As Decimal

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

    Public Property WorkShiftName() As String
        Get
            Return _workShiftName
        End Get
        Set(ByVal value As String)
            _workShiftName = value
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

    Public Property FlownRouteName()
        Get
            Return _flownRouteName
        End Get
        Set(ByVal value)
            _flownRouteName = value
        End Set
    End Property

    Public Property FlightMeterSort() As Decimal
        Get
            Return _flightMeterSort
        End Get
        Set(ByVal value As Decimal)
            _flightMeterSort = value
        End Set
    End Property

    Public Property FlightMeterStart() As Decimal
        Get
            Return _flightMeterStart
        End Get
        Set(ByVal value As Decimal)
            _flightMeterStart = value
        End Set
    End Property

    Public Property FlightMeterEnd() As Decimal
        Get
            Return _flightMeterEnd
        End Get
        Set(ByVal value As Decimal)
            _flightMeterEnd = value
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

    Public Property TotalFlightAirtime() As Decimal
        Get
            Return _totalFlightAirtime
        End Get
        Set(ByVal value As Decimal)
            _totalFlightAirtime = value
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

    Public Property LiquidFillId() As Integer
        Get
            Return _liquidFillId
        End Get
        Set(ByVal value As Integer)
            _liquidFillId = value
        End Set
    End Property

    Public Property RefillMeterLast() As Decimal
        Get
            Return _refillMeterLast
        End Get
        Set(ByVal value As Decimal)
            _refillMeterLast = value
        End Set
    End Property

    Public Property RefillMeterCurrent() As Decimal
        Get
            Return _refillmetercurrent
        End Get
        Set(ByVal value As Decimal)
            _refillMeterCurrent = value
        End Set
    End Property

    Public Property RefillAirtime() As Decimal
        Get
            Return _refillAirtime
        End Get
        Set(ByVal value As Decimal)
            _refillAirtime = value
        End Set
    End Property

    Public Property LiquidGroup() As String
        Get
            Return _liquidGroup
        End Get
        Set(ByVal value As String)
            _liquidGroup = value
        End Set
    End Property

    Public Property LiquidAmt() As Decimal
        Get
            Return _liquidAmt
        End Get
        Set(ByVal value As Decimal)
            _liquidAmt = value
        End Set
    End Property

    Public Property LiquidUnit() As String
        Get
            Return _liquidUnit
        End Get
        Set(ByVal value As String)
            _liquidUnit = value
        End Set
    End Property

    Public Property Usage() As Decimal
        Get
            Return _usage
        End Get
        Set(ByVal value As Decimal)
            _usage = value
        End Set
    End Property

    Public Property TotalRefillAirtime() As Decimal
        Get
            Return _totalRefillAirtime
        End Get
        Set(ByVal value As Decimal)
            _totalRefillAirtime = value
        End Set
    End Property
    Public Property TotalLiquidAmt() As Decimal
        Get
            Return _totalLiquidAmt
        End Get
        Set(ByVal value As Decimal)
            _totalLiquidAmt = value
        End Set
    End Property

    Public Property AvgUsage() As Decimal
        Get
            Return _avgUsage
        End Get
        Set(ByVal value As Decimal)
            _avgUsage = value
        End Set
    End Property

    Public Sub New()

        _progName = String.Empty
        _aircraftId = 0
        _msn = String.Empty
        _registrationNbr = String.Empty
        _flightDate = Nothing
        _locationName = String.Empty
        _workShiftName = String.Empty
        _schedRouteName = String.Empty
        _flownRouteName = String.Empty
        _flightMeterSort = 0
        _flightMeterStart = 0
        _flightMeterEnd = 0
        _flightAirtime = 0
        _totalFlightAirtime = 0
        _meterUnit = String.Empty
        _liquidFillId = Nothing
        _refillMeterLast = Nothing
        _refillMeterCurrent = Nothing
        _refillAirtime = Nothing
        _liquidGroup = String.Empty
        _liquidAmt = Nothing
        _liquidUnit = String.Empty
        _usage = Nothing
        _totalRefillAirtime = Nothing
        _totalLiquidAmt = Nothing
        _avgUsage = Nothing

    End Sub

End Class
