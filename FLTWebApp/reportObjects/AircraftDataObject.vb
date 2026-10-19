' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Public Class AircraftDataObject

    Private _progName As String
    Private _aircraftId As Integer
    Private _msn As String
    Private _registrationNbr As String
    Private _currentLocation As String
    Private _shipNbr As String
    Private _serialNbr As String
    Private _model As String
    Private _description As String
    Private _liveryColor As String
    Private _engineer As String
    Private _mechanic As String
    Private _aircraftStatus As String
    Private _manufacturer As String
    Private _airframeHoursStart As Decimal
    Private _meterUnit As String
    Private _totalRequiredUsage As Decimal
    Private _estEndOfTesting As Date
    Private _actEndOfTesting As Date
    Private _activeStatus As Boolean

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

    Public Property CurrentLocation() As String
        Get
            Return _currentLocation
        End Get
        Set(ByVal value As String)
            _currentLocation = value
        End Set
    End Property

    Public Property ShipNbr() As String
        Get
            Return _shipNbr
        End Get
        Set(ByVal value As String)
            _shipNbr = value
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

    Public Property Model() As String
        Get
            Return _model
        End Get
        Set(ByVal value As String)
            _model = value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Public Property LiveryColor() As String
        Get
            Return _liveryColor
        End Get
        Set(ByVal value As String)
            _liveryColor = value
        End Set
    End Property

    Public Property Engineer() As String
        Get
            Return _engineer
        End Get
        Set(ByVal value As String)
            _engineer = value
        End Set
    End Property

    Public Property Mechanic() As String
        Get
            Return _mechanic
        End Get
        Set(ByVal value As String)
            _mechanic = value
        End Set
    End Property

    Public Property AircraftStatus() As String
        Get
            Return _aircraftStatus
        End Get
        Set(ByVal value As String)
            _aircraftStatus = value
        End Set
    End Property

    Public Property Manufacturer() As String
        Get
            Return _manufacturer
        End Get
        Set(ByVal value As String)
            _manufacturer = value
        End Set
    End Property

    Public Property AirframeHoursStart() As Decimal
        Get
            Return _airframeHoursStart
        End Get
        Set(ByVal value As Decimal)
            _airframeHoursStart = value
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

    Public Property TotalRequiredUsage() As Decimal
        Get
            Return _totalRequiredUsage
        End Get
        Set(ByVal value As Decimal)
            _totalRequiredUsage = value
        End Set
    End Property

    Public Property EstEndOfTesting() As String
        Get
            If (_estEndOfTesting = Nothing) Then
                Return String.Empty
            Else
                Return Format(_estEndOfTesting, "MM/dd/yyyy")
            End If
        End Get
        Set(ByVal value As String)
            If IsDate(value) Then
                _estEndOfTesting = CDate(value)
            End If
        End Set
    End Property

    Public Property ActEndOfTesting() As String
        Get
            If (_actEndOfTesting = Nothing) Then
                Return String.Empty
            Else
                Return Format(_actEndOfTesting, "MM/dd/yyyy")
            End If
        End Get
        Set(ByVal value As String)
            If IsDate(value) Then
                _actEndOfTesting = CDate(value)
            End If
        End Set
    End Property

    Public Property ActiveStatus() As Boolean
        Get
            Return _activeStatus
        End Get
        Set(ByVal value As Boolean)
            _activeStatus = value
        End Set
    End Property

    Public Sub New()
        ProgName = String.Empty
        AircraftId = 0
        MSN = String.Empty
        RegistrationNbr = String.Empty
        CurrentLocation = String.Empty
        ShipNbr = String.Empty
        SerialNbr = String.Empty
        Model = String.Empty
        Description = String.Empty
        LiveryColor = String.Empty
        Engineer = String.Empty
        Mechanic = String.Empty
        AircraftStatus = String.Empty
        Manufacturer = String.Empty
        AirframeHoursStart = 0
        MeterUnit = String.Empty
        TotalRequiredUsage = 0
        EstEndOfTesting = Nothing
        ActEndOfTesting = Nothing
        ActiveStatus = False
    End Sub

End Class
