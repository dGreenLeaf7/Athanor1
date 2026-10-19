' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Namespace FltWebApp

    Public Class DailyUsageObject

        Private _progId As Integer
        Private _progName As String
        Private _aircraftLocationId As Integer
        Private _locationId As Integer
        Private _locationName As String
        Private _flightDate As Date
        Private _aircraftId As Integer
        Private _msn As String
        Private _modelName As String
        Private _testName As String
        Private _flightId As Integer
        Private _hoursOnDate As Decimal
        Private _dateLastRun As Date
        Private _lastMeterReading As Decimal
        Private _locationCurrentHours As Decimal
        Private _locationPercentComplete As Double
        Private _locationGoalHours As Decimal
        Private _locationEndTestDate As Date
        Private _aircraftCurrentUsage As Decimal
        Private _programPercentComplete As Double
        Private _programEndTestDate As Date
        Private _totalRequiredUsage As Decimal

        Public Property ProgId() As Integer
            Get
                Return (_progId)
            End Get
            Set(ByVal value As Integer)
                _progId = value
            End Set
        End Property

        Public Property ProgName() As String
            Get
                Return _progName
            End Get
            Set(ByVal value As String)
                _progName = value
            End Set
        End Property

        Public Property AircraftLocationId() As Integer
            Get
                Return _aircraftLocationId
            End Get
            Set(ByVal value As Integer)
                _aircraftLocationId = value
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

        Public Property FlightDate() As Date
            Get
                Return _flightDate
            End Get
            Set(ByVal value As Date)
                _flightDate = value
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

        Public Property ModelName() As String
            Get
                Return _modelName
            End Get
            Set(ByVal value As String)
                _modelName = value
            End Set
        End Property

        Public Property TestName() As String
            Get
                Return _testName
            End Get
            Set(ByVal value As String)
                _testName = value
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

        Public Property HoursOnDate() As Decimal
            Get
                Return _hoursOnDate
            End Get
            Set(ByVal value As Decimal)
                _hoursOnDate = value
            End Set
        End Property

        Public Property DateLastRun() As Date
            Get
                Return _dateLastRun
            End Get
            Set(ByVal value As Date)
                _dateLastRun = value
            End Set
        End Property

        Public Property LastMeterReading() As Decimal
            Get
                Return _lastMeterReading
            End Get
            Set(ByVal value As Decimal)
                _lastMeterReading = value
            End Set
        End Property

        Public Property LocationCurrentHours() As Decimal
            Get
                Return _locationCurrentHours
            End Get
            Set(ByVal value As Decimal)
                _locationCurrentHours = value
            End Set
        End Property

        Public Property LocationPercentComplete() As Double
            Get
                Return _locationPercentComplete
            End Get
            Set(ByVal value As Double)
                _locationPercentComplete = 0
            End Set
        End Property

        Public Property LocationGoalHours() As Decimal
            Get
                Return _locationGoalHours
            End Get
            Set(ByVal value As Decimal)
                _locationGoalHours = value
            End Set
        End Property

        Public Property LocationEndTestDate() As Date
            Get
                Return _locationEndTestDate
            End Get
            Set(ByVal value As Date)
                _locationEndTestDate = value
            End Set
        End Property

        Public Property AircraftCurrentUsage() As Decimal
            Get
                Return _aircraftCurrentUsage
            End Get
            Set(ByVal value As Decimal)
                _aircraftCurrentUsage = value
            End Set
        End Property

        Public Property ProgramPercentComplete() As Double
            Get
                Return _programPercentComplete
            End Get
            Set(ByVal value As Double)
                _programPercentComplete = value
            End Set
        End Property

        Public Property ProgramEndTestDate() As Date
            Get
                Return _programEndTestDate
            End Get
            Set(ByVal value As Date)
                _programEndTestDate = value
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

        Public Sub New()
            _progId = 0
            _progName = String.Empty
            _aircraftLocationId = 0
            _locationId = 0
            _locationName = String.Empty
            _flightDate = Nothing
            _aircraftId = 0
            _msn = String.Empty
            _modelName = String.Empty
            _testName = String.Empty
            _flightId = 0
            _hoursOnDate = 0
            _dateLastRun = Nothing
            _lastMeterReading = 0
            _locationCurrentHours = 0
            _locationPercentComplete = 0
            _locationGoalHours = 0
            _locationEndTestDate = Nothing
            _aircraftCurrentUsage = 0
            _programPercentComplete = 0
            _programEndTestDate = Nothing
            _totalRequiredUsage = 0
        End Sub

    End Class

End Namespace
