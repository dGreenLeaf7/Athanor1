' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data.SqlTypes
Imports System.Diagnostics
Imports System.Text.RegularExpressions
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BO

    <Serializable()> _
    Public Class Flight

        Private Const TimeRegex As String = "^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]?(\s)?(AM|am|Am|aM|PM|pm|Pm|pM))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]))$"
        Private _meterMask As String = "#,##0.0;-#,##0.0"
        Private _dateMask As String = "MM-dd-yyyy"
        Private _sortDateMask As String = "yyyy-MM-dd"

        Private _flightId As Integer
        Private _AircraftId As Integer
        Private _locationId As Integer
        Private _schedRouteId As Integer
        Private _schedRouteName As String
        Private _flownRouteId As Integer
        Private _flownRouteName As String
        Private _flightDate As String
        Private _flightDateSort As String
        Private _workShiftIndicatorId As Integer
        Private _workShiftName As String

        Private _takeoffWeight As Integer

        Private _airframeWeightId As Integer
        Private _airframeWeightDescr As String
        Private _airframeWeight As Integer
        Private _airframeWeightUnit As String
        Private _airframeWeightGuid As Guid

        Private _cargo1WeightId As Integer
        Private _cargo1WeightDescr As String
        Private _cargo1Weight As Integer
        Private _cargo1WeightUnit As String
        Private _cargo1WeightGuid As Guid

        Private _cargo2WeightId As Integer
        Private _cargo2WeightDescr As String
        Private _cargo2Weight As Integer
        Private _cargo2WeightUnit As String
        Private _cargo2WeightGuid As Guid

        Private _ballastWeightId As Integer
        Private _ballastWeightDescr As String
        Private _ballastWeight As Integer
        Private _ballastWeightUnit As String
        Private _ballastWeightGuid As Guid

        Private _pilotId As Integer
        Private _pilotName As String
        Private _preFlightFtiOK As Short
        Private _preFlightTelemetryOK As Short
        Private _preFlightTelemetryCheckComplete As Short
        Private _oilLevelIndicatorStart As Short
        Private _hydraulicLevelIndicatorStart As Short
        Private _usedMeterOffset As Decimal
        Private _meterUnitId As Integer
        Private _departureTime As String
        Private _departureDateTime_UTC As DateTime
        Private _departureDateTime_Local As DateTime
        Private _engineHoursStart As Decimal
        Private _hobbsStart As Decimal
        Private _airframeHoursStart As Decimal
        Private _returnTime As String
        Private _returnDateTime_UTC As DateTime
        Private _returnDateTime_Local As DateTime
        Private _hobbsEnd As Decimal
        Private _airframeHoursEnd As Decimal
        Private _engineHoursEnd As Decimal
        Private _aircraftEngineHoursCurrent As Decimal
        Private _postFlightFtiOK As Short
        Private _postFlightTelemetryCheckComplete As Short
        Private _nacelleCheckBuildUp As Short
        Private _nacelleCheckDiscoloration As Short

        Private _isApproved As Boolean
        Private _approvedBy As String
        Private _utcDateApproved As DateTime

        Private _flightGuid As Guid
        Private _isActive As Boolean
        Private _isDeleted As Boolean
        Private _utcDateCreated As DateTime
        Private _utcDateModified As DateTime
        Private _StampUserName As String
        Private _concurrencyId As String

        Private _liquidFillList As LiquidFillList = Nothing
        Private _liquidFillDeleteList As LiquidFillList = Nothing
        Private _newLiquidFillId As Integer = -1

        Private _flightCommentList As FlightCommentList = Nothing
        Private _flightCommentDeleteList As FlightCommentList = Nothing
        Private _newFlightCommentId As Integer = -1

        Private _flightEngineRunList As FlightEngineRunList = Nothing
        Private _flightEngineRunDeleteList As FlightEngineRunList = Nothing
        Private _newFlightEngineRunId As Integer = -1

        Private _flightRegionTimeList As FlightRegionTimeList = Nothing
        Private _flightRegionTimeDeleteList As FlightRegionTimeList = Nothing
        Private _newFlightRegionTimeId As Integer = -1

        Private _brokenRulesList As BrokenRulesList = New BrokenRulesList
        Private _errorsList As BrokenRulesList = New BrokenRulesList
        Private _warningsList As BrokenRulesList = New BrokenRulesList

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
                Return _AircraftId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _AircraftId = 0
                Else
                    _AircraftId = value
                End If
            End Set
        End Property

        Public Property LocationId() As Integer
            Get
                Return _locationId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _locationId = 0
                Else
                    _locationId = value
                End If
            End Set
        End Property

        Public Property SchedRouteId() As Integer
            Get
                Return _schedRouteId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _schedRouteId = 0
                Else
                    _schedRouteId = value
                End If
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

        Public Property FlownRouteId() As Integer
            Get
                Return _flownRouteId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _flownRouteId = 0
                Else
                    _flownRouteId = value
                End If
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

        Public Property FlightDate() As String
            Get
                Return _flightDate
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _flightDate = String.Empty
                ElseIf IsDate(value) Then
                    _flightDate = Format(CDate(value), _dateMask)
                    _flightDateSort = Format(CDate(value), _sortDateMask)
                Else
                    _flightDate = Trim(value)
                End If
            End Set
        End Property

        Public Property FlightDateSort() As String
            Get
                Return _flightDateSort
            End Get
            Friend Set(ByVal value As String)
                If (value = Nothing) Then
                    _flightDate = String.Empty
                ElseIf IsDate(value) Then
                    _flightDateSort = Format(CDate(value), _sortDateMask)
                Else
                    _flightDateSort = value
                End If
            End Set
        End Property

        Public Property WorkShiftIndicatorId() As Integer
            Get
                Return _workShiftIndicatorId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _workShiftIndicatorId = 0
                Else
                    _workShiftIndicatorId = value
                End If
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

        Public Property TakeoffWeight() As Integer
            Get
                Return (_takeoffWeight)
            End Get
            Friend Set(ByVal value As Integer)
                If Not value = Nothing Then
                    _takeoffWeight = value
                Else
                    _takeoffWeight = 0
                End If
            End Set
        End Property

        Public Property AirframeWeightId() As Integer
            Get
                Return _airframeWeightId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _airframeWeightId = 0
                Else
                    _airframeWeightId = value
                End If
            End Set
        End Property

        Public Property AirframeWeightDescr() As String
            Get
                Return _airframeWeightDescr
            End Get
            Set(ByVal value As String)
                _airframeWeightDescr = value
            End Set
        End Property

        Public Property AirframeWeight() As Integer
            Get
                Return _airframeWeight
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _airframeWeight = 0
                Else
                    _airframeWeight = value
                End If
            End Set
        End Property

        Public Property AirframeWeightUnit() As String
            Get
                Return _airframeWeightUnit
            End Get
            Set(ByVal value As String)
                _airframeWeightUnit = value
            End Set
        End Property

        Public Property AirframeWeightGUID() As Guid
            Get
                Return _airframeWeightGuid
            End Get
            Set(ByVal value As Guid)
                If (value = Nothing) Then
                    _airframeWeightGuid = Guid.Empty
                Else
                    _airframeWeightGuid = value
                End If
            End Set
        End Property

        Public Property Cargo1WeightId() As Integer
            Get
                Return _cargo1WeightId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _cargo1WeightId = 0
                Else
                    _cargo1WeightId = value
                End If
            End Set
        End Property

        Public Property Cargo1WeightDescr() As String
            Get
                Return _cargo1WeightDescr
            End Get
            Set(ByVal value As String)
                _cargo1WeightDescr = value
            End Set
        End Property

        Public Property Cargo1Weight() As Integer
            Get
                Return _cargo1Weight
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _cargo1Weight = 0
                Else
                    _cargo1Weight = value
                End If
            End Set
        End Property

        Public Property Cargo1WeightUnit() As String
            Get
                Return _cargo1WeightUnit
            End Get
            Set(ByVal value As String)
                _cargo1WeightUnit = value
            End Set
        End Property

        Public Property Cargo1WeightGUID() As Guid
            Get
                Return _cargo1WeightGuid
            End Get
            Set(ByVal value As Guid)
                If (value = Nothing) Then
                    _cargo1WeightGuid = Guid.Empty
                Else
                    _cargo1WeightGuid = value
                End If
            End Set
        End Property

        Public Property Cargo2WeightId() As Integer
            Get
                Return _cargo2WeightId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _cargo2WeightId = 0
                Else
                    _cargo2WeightId = value
                End If
            End Set
        End Property

        Public Property Cargo2WeightDescr() As String
            Get
                Return _cargo2WeightDescr
            End Get
            Set(ByVal value As String)
                _cargo2WeightDescr = value
            End Set
        End Property

        Public Property Cargo2Weight() As Integer
            Get
                Return _cargo2Weight
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _cargo2Weight = 0
                Else
                    _cargo2Weight = value
                End If
            End Set
        End Property

        Public Property Cargo2WeightUnit() As String
            Get
                Return _cargo2WeightUnit
            End Get
            Set(ByVal value As String)
                _cargo2WeightUnit = value
            End Set
        End Property

        Public Property Cargo2WeightGUID() As Guid
            Get
                Return _cargo2WeightGuid
            End Get
            Set(ByVal value As Guid)
                If (value = Nothing) Then
                    _cargo2WeightGuid = Guid.Empty
                Else
                    _cargo2WeightGuid = value
                End If
            End Set
        End Property

        Public Property BallastWeightId() As Integer
            Get
                Return _ballastWeightId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _ballastWeightId = 0
                Else
                    _ballastWeightId = value
                End If
            End Set
        End Property

        Public Property BallastWeightDescr() As String
            Get
                Return _ballastWeightDescr
            End Get
            Set(ByVal value As String)
                _ballastWeightDescr = value
            End Set
        End Property

        Public Property BallastWeight() As Integer
            Get
                Return _ballastWeight
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _ballastWeight = 0
                Else
                    _ballastWeight = value
                End If
            End Set
        End Property

        Public Property BallastWeightUnit() As String
            Get
                Return _ballastWeightUnit
            End Get
            Set(ByVal value As String)
                _ballastWeightUnit = value
            End Set
        End Property

        Public Property BallastWeightGUID() As Guid
            Get
                Return _ballastWeightGuid
            End Get
            Set(ByVal value As Guid)
                If (value = Nothing) Then
                    _ballastWeightGuid = Guid.Empty
                Else
                    _ballastWeightGuid = value
                End If
            End Set
        End Property

        Public Property PilotId() As Integer
            Get
                Return _pilotId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _pilotId = 0
                Else
                    _pilotId = value
                End If
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

        Public Property PreFlightFtiOK() As Short
            Get
                Return _preFlightFtiOK
            End Get
            Set(ByVal value As Short)
                _preFlightFtiOK = value
            End Set
        End Property

        Public Property PreFlightTelemetryOK() As Short
            Get
                Return _preFlightTelemetryOK
            End Get
            Set(ByVal value As Short)
                _preFlightTelemetryOK = value
            End Set
        End Property

        Public Property PreFlightTelemetryCheckComplete() As Short
            Get
                Return _preFlightTelemetryCheckComplete
            End Get
            Set(ByVal value As Short)
                _preFlightTelemetryCheckComplete = value
            End Set
        End Property

        Public Property OilLevelIndicatorStart() As Short
            Get
                Return _oilLevelIndicatorStart
            End Get
            Set(ByVal value As Short)
                If (value = Nothing) Then
                    _oilLevelIndicatorStart = 0
                Else
                    _oilLevelIndicatorStart = value
                End If
            End Set
        End Property

        Public Property HydraulicLevelIndicatorStart() As Short
            Get
                Return _hydraulicLevelIndicatorStart
            End Get
            Set(ByVal value As Short)
                If (value = Nothing) Then
                    _hydraulicLevelIndicatorStart = 0
                Else
                    _hydraulicLevelIndicatorStart = value
                End If
            End Set
        End Property

        Public Property UsedMeterOffset() As String
            Get
                Return Format(_usedMeterOffset, _meterMask)
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _usedMeterOffset = 0
                ElseIf IsNumeric(value) Then
                    _usedMeterOffset = CDec(value)
                Else
                    _usedMeterOffset = 0
                End If
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

        Public Property DepartureTime() As String
            Get
                Return _departureTime
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _departureTime = String.Empty
                Else
                    _departureTime = Trim(value)
                End If
            End Set
        End Property

        Friend Property DepartureDateTime_UTC() As DateTime
            Get
                Return _departureDateTime_UTC
            End Get
            Set(ByVal value As DateTime)
                If Not (value = Nothing) Then
                    _departureDateTime_UTC = value
                Else
                    _departureDateTime_UTC = SqlDateTime.MinValue
                End If
            End Set
        End Property

        Friend Property DepartureDateTime_Local() As DateTime
            Get
                Return _departureDateTime_Local
            End Get
            Set(ByVal value As DateTime)
                If Not (value = Nothing) Then
                    _departureDateTime_Local = value
                Else
                    _departureDateTime_Local = SqlDateTime.MinValue
                End If
            End Set
        End Property

        Public Property EngineHoursStart() As String
            Get
                Return Format(_engineHoursStart, _meterMask)
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _engineHoursStart = 0
                ElseIf IsNumeric(value) Then
                    _engineHoursStart = CDec(value)
                Else
                    _engineHoursStart = 0
                End If
            End Set
        End Property

        Public Property HobbsStart() As String
            Get
                Return Format(_hobbsStart, _meterMask)
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _hobbsStart = 0
                ElseIf IsNumeric(value) Then
                    _hobbsStart = CDec(value)
                Else
                    _hobbsStart = 0
                End If
            End Set
        End Property

        Public Property AirframeHoursStart() As String
            Get
                Return Format(_airframeHoursStart, _meterMask)
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _airframeHoursStart = 0
                ElseIf IsNumeric(value) Then
                    _airframeHoursStart = CDec(value)
                Else
                    _airframeHoursStart = 0
                End If
            End Set
        End Property

        Public Property ReturnTime() As String
            Get
                Return _returnTime
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _returnTime = String.Empty
                Else
                    _returnTime = Trim(value)
                End If
            End Set
        End Property

        Friend Property ReturnDateTime_UTC() As DateTime
            Get
                Return _returnDateTime_UTC
            End Get
            Set(ByVal value As DateTime)
                If Not (value = Nothing) Then
                    _returnDateTime_UTC = value
                Else
                    _returnDateTime_UTC = SqlDateTime.MinValue
                End If
            End Set
        End Property

        Friend Property ReturnDateTime_Local() As DateTime
            Get
                Return _returnDateTime_Local
            End Get
            Set(ByVal value As DateTime)
                If Not (value = Nothing) Then
                    _returnDateTime_Local = value
                Else
                    _returnDateTime_Local = SqlDateTime.MinValue
                End If
            End Set
        End Property

        Public Property HobbsEnd() As String
            Get
                Return Format(_hobbsEnd, _meterMask)
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _hobbsEnd = 0
                ElseIf IsNumeric(value) Then
                    _hobbsEnd = CDec(value)
                Else
                    _hobbsEnd = 0
                End If
            End Set
        End Property

        Public Property AirframeHoursEnd() As String
            Get
                Return Format(_airframeHoursEnd, _meterMask)
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _airframeHoursEnd = 0
                ElseIf IsNumeric(value) Then
                    _airframeHoursEnd = CDec(value)
                Else
                    _airframeHoursEnd = 0
                End If
            End Set
        End Property

        Public Property EngineHoursEnd() As String
            Get
                Return Format(_engineHoursEnd, _meterMask)
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _engineHoursEnd = 0
                ElseIf IsNumeric(value) Then
                    _engineHoursEnd = CDec(value)
                Else
                    _engineHoursEnd = 0
                End If
            End Set
        End Property

        Public Property AircraftEngineHoursCurrent As String
            Get
                Return Format(_aircraftEngineHoursCurrent, _meterMask)
            End Get
            Set(value As String)
                If String.IsNullOrEmpty(value) Then
                    _aircraftEngineHoursCurrent = 0
                ElseIf IsNumeric(value) Then
                    _aircraftEngineHoursCurrent = CDec(value)
                Else
                    _aircraftEngineHoursCurrent = 0
                End If
            End Set
        End Property

        Public Property PostFlightFtiOK() As Short
            Get
                Return _postFlightFtiOK
            End Get
            Set(ByVal value As Short)
                _postFlightFtiOK = value
            End Set
        End Property

        Public Property PostFlightTelemetryCheckComplete() As Short
            Get
                Return _postFlightTelemetryCheckComplete
            End Get
            Set(ByVal value As Short)
                _postFlightTelemetryCheckComplete = value
            End Set
        End Property

        Public Property NacelleCheckBuildUp() As Short
            Get
                Return _nacelleCheckBuildUp
            End Get
            Set(ByVal value As Short)
                _nacelleCheckBuildUp = value
            End Set
        End Property

        Public Property NacelleCheckDiscoloration() As Short
            Get
                Return _nacelleCheckDiscoloration
            End Get
            Set(ByVal value As Short)
                _nacelleCheckDiscoloration = value
            End Set
        End Property

        Public Property IsApproved() As Boolean
            Get
                Return _isApproved
            End Get
            Set(ByVal value As Boolean)
                _isApproved = value
            End Set
        End Property

        Public Property ApprovedBy() As String
            Get
                Return _approvedBy
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _approvedBy = String.Empty
                Else
                    _approvedBy = value
                End If
            End Set
        End Property

        Public Property UtcDateApproved() As Date
            Get
                Return _utcDateApproved
            End Get
            Set(ByVal value As Date)
                If (value = Nothing) Then
                    _utcDateApproved = Nothing
                Else
                    _utcDateApproved = value
                End If
            End Set
        End Property

        Public Property FlightGuid() As Guid
            Get
                Return _flightGuid
            End Get
            Set(ByVal value As Guid)
                If (value = Nothing) Then
                    _flightGuid = Guid.Empty
                Else
                    _flightGuid = value
                End If
            End Set
        End Property

        Public Property IsActive() As Boolean
            Get
                Return _isActive
            End Get
            Set(ByVal value As Boolean)
                _isActive = value
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

        Public Property LiquidFillList() As LiquidFillList
            Get
                Return _liquidFillList
            End Get
            Friend Set(ByVal value As LiquidFillList)
                _liquidFillList = value
            End Set
        End Property

        Public Property FlightCommentList() As FlightCommentList
            Get
                Return _flightCommentList
            End Get
            Friend Set(ByVal value As FlightCommentList)
                _flightCommentList = value
            End Set
        End Property

        Public Property FlightEngineRunList() As FlightEngineRunList
            Get
                Return _flightEngineRunList
            End Get
            Set(ByVal value As FlightEngineRunList)
                _flightEngineRunList = value
            End Set
        End Property

        Public Property FlightRegionTimeList() As FlightRegionTimeList
            Get
                Return _flightRegionTimeList
            End Get
            Friend Set(ByVal value As FlightRegionTimeList)
                _flightRegionTimeList = value
            End Set
        End Property

        Friend Property LiquidFillDeleteList() As LiquidFillList
            Get
                Return _liquidFillDeleteList
            End Get
            Set(ByVal value As LiquidFillList)
                _liquidFillDeleteList = value
            End Set
        End Property

        Friend Property FlightCommentDeleteList() As FlightCommentList
            Get
                Return _flightCommentDeleteList
            End Get
            Set(ByVal value As FlightCommentList)
                _flightCommentDeleteList = value
            End Set
        End Property

        Friend Property FlightEngineRunDeleteList() As FlightEngineRunList
            Get
                Return _flightEngineRunDeleteList
            End Get
            Set(ByVal value As FlightEngineRunList)

            End Set
        End Property

        Friend Property FlightRegionTimeDeleteList() As FlightRegionTimeList
            Get
                Return _flightRegionTimeDeleteList
            End Get
            Set(ByVal value As FlightRegionTimeList)
                _flightRegionTimeDeleteList = value
            End Set
        End Property

        Friend Property NewLiquidFillId() As Integer
            Get
                Return _newLiquidFillId
            End Get
            Set(ByVal value As Integer)
                _newLiquidFillId = value
            End Set
        End Property

        Friend Property NewFlightCommentId() As Integer
            Get
                Return _newFlightCommentId
            End Get
            Set(ByVal value As Integer)
                _newFlightCommentId = value
            End Set
        End Property

        Friend Property NewFlightEngineRunId() As Integer
            Get
                Return _newFlightEngineRunId
            End Get
            Set(ByVal value As Integer)
                _newFlightEngineRunId = value
            End Set
        End Property

        Friend Property NewFlightRegionTimeId() As Integer
            Get
                Return _newFlightRegionTimeId
            End Get
            Set(ByVal value As Integer)
                _newFlightRegionTimeId = value
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
            Me.FlightId = -1
            Me.AircraftId = 0
            Me.LocationId = 0
            Me.SchedRouteId = 0
            Me.SchedRouteName = String.Empty
            Me.FlownRouteId = 0
            Me.FlownRouteName = String.Empty
            Me.FlightDate = String.Empty
            Me.WorkShiftIndicatorId = 0
            Me.WorkShiftName = String.Empty

            Me.AirframeWeightId = 0
            Me.AirframeWeightDescr = String.Empty
            Me.AirframeWeight = 0
            Me.AirframeWeightUnit = String.Empty
            Me.AirframeWeightGUID = Guid.Empty

            Me.Cargo1WeightId = 0
            Me.Cargo1WeightDescr = String.Empty
            Me.Cargo1Weight = 0
            Me.Cargo1WeightUnit = String.Empty
            Me.Cargo1WeightGUID = Guid.Empty

            Me.Cargo2WeightId = 0
            Me.Cargo2WeightDescr = String.Empty
            Me.Cargo2Weight = 0
            Me.Cargo2WeightUnit = String.Empty
            Me.Cargo2WeightGUID = Guid.Empty

            Me.BallastWeightId = 0
            Me.BallastWeightDescr = String.Empty
            Me.BallastWeight = 0
            Me.BallastWeightUnit = String.Empty
            Me.BallastWeightGUID = Guid.Empty

            Me.PilotId = 0
            Me.PilotName = String.Empty
            Me.PreFlightFtiOK = 0
            Me.PreFlightTelemetryOK = 0
            Me.PreFlightTelemetryCheckComplete = 0
            Me.OilLevelIndicatorStart = 0
            Me.HydraulicLevelIndicatorStart = 0
            Me.UsedMeterOffset = 0
            Me.MeterUnitId = 0
            Me.DepartureTime = String.Empty
            Me.DepartureDateTime_UTC = SqlDateTime.MinValue
            Me.DepartureDateTime_Local = SqlDateTime.MinValue
            Me.EngineHoursStart = 0.0
            Me.HobbsStart = 0
            Me.AirframeHoursStart = 0
            Me.ReturnTime = String.Empty
            Me.ReturnDateTime_UTC = SqlDateTime.MinValue
            Me.ReturnDateTime_Local = SqlDateTime.MinValue
            Me.HobbsEnd = 0
            Me.AirframeHoursEnd = 0
            Me.EngineHoursEnd = 0.0
            Me.AircraftEngineHoursCurrent = 0.0
            Me.PostFlightTelemetryCheckComplete = 0
            Me.NacelleCheckBuildUp = 0
            Me.NacelleCheckDiscoloration = 0
            Me.IsApproved = False
            Me.ApprovedBy = String.Empty
            Me.UtcDateApproved = SqlDateTime.MinValue
            Me.FlightGuid = Guid.Empty
            Me.IsActive = True
            Me.IsDeleted = False
            Me.UtcDateCreated = Nothing
            Me.UtcDateModified = Nothing
            Me.StampUserName = String.Empty
            Me.ConcurrencyId = "0"

            _liquidFillList = New LiquidFillList
            _liquidFillDeleteList = New LiquidFillList
            _newLiquidFillId = -1

            _flightCommentList = New FlightCommentList
            _flightCommentDeleteList = New FlightCommentList
            _newFlightCommentId = -1

            _flightEngineRunList = New FlightEngineRunList
            _flightEngineRunDeleteList = New FlightEngineRunList
            _newFlightEngineRunId = -1

            _flightRegionTimeList = New FlightRegionTimeList
            _flightRegionTimeDeleteList = New FlightRegionTimeList
            _newFlightRegionTimeId = -1

            _brokenRulesList = New BrokenRulesList
            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

        End Sub

        Public Function Validate() As Boolean
            Return Validate(_warningsList)
        End Function

        Public Function Validate(ByVal EnforceValidation As Boolean) As Boolean
            If EnforceValidation Then
                Return Validate(_warningsList)
            Else
                _warningsList = New BrokenRulesList
                Return True
            End If
        End Function

        Friend Function Validate(ByVal WarningsList As BrokenRulesList) As Boolean

            Dim prevWarningsList As New BrokenRulesList
            For Each thisBrokenRule As BrokenRule In WarningsList
                prevWarningsList.Add(thisBrokenRule)
            Next
            _brokenRulesList = New BrokenRulesList

            If (_AircraftId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("AircraftId", "AircraftId is required", "02-00-001", False, False))
            Else
                If Len(_AircraftId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("AircraftId", "AircraftId is required", "02-00-001", False, False))
                End If
            End If
            If (_locationId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("LocationId", "LocationId is required", "02-00-002", False, False))
            Else
                If Len(_locationId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("LocationId", "LocationId is required", "02-00-002", False, False))
                End If
            End If
            If (_schedRouteId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("SchedRouteId", "SchedRouteId is required", "02-00-003", False, False))
            Else
                If Len(_schedRouteId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("SchedRouteId", "SchedRouteId is required", "02-00-003", False, False))
                End If
            End If
            If (_flownRouteId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("FlownRouteId", "FlownRouteId is required", "02-00-004", False, False))
            Else
                If Len(_flownRouteId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("FlownRouteId", "FlownRouteId is required", "02-00-004", False, False))
                End If
            End If
            If (_flightDate = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("FlightDate", "FlightDate is required", "02-00-005", False, False))
            Else
                If Len(_flightDate) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("FlightDate", "FlightDate is required", "02-00-005", False, False))
                Else
                    If Not IsDate(_flightDate) Then
                        _brokenRulesList.Add(New BrokenRule("FlightDate", "FlightDate must be a date value", "02-00-006", False, False))
                    End If
                End If
            End If
            If (_flightDate.Length > 10) Then
                _brokenRulesList.Add(New BrokenRule("FlightDate", "FlightDate cannot be longer than 10", "02-00-007", False, False))
            End If
            If (_workShiftIndicatorId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("WorkShiftIndicatorId", "WorkShiftIndicatorId is required", "02-00-008", False, False))
            Else
                If Len(_workShiftIndicatorId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("WorkShiftIndicatorId", "WorkShiftIndicatorId is required", "02-00-008", False, False))
                End If
            End If
            If (_meterUnitId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("MeterUnitId", "MeterUnitId is required", "02-00-009", False, False))
            Else
                If Len(_meterUnitId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("MeterUnitId", "MeterUnitId is required", "02-00-009", False, False))
                End If
            End If
            If (_pilotId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("PilotId", "PilotId is required", "02-00-00A", False, False))
            Else
                If _pilotId = 0 Then
                    _brokenRulesList.Add(New BrokenRule("PilotId", "PilotId is required", "02-00-00A", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("02-00-00B") Then
                If (_preFlightFtiOK = 0) Then
                    _brokenRulesList.Add(New BrokenRule("PreFlightFti", "PreFlightFti is required", "02-00-00B", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("02-00-00C") Then
                If (_preFlightTelemetryOK = 0) Then
                    _brokenRulesList.Add(New BrokenRule("PreFlightTelemetryOK", "PreFlightTelemetryOK is required", "02-00-00C", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("02-00-00D") Then
                If (_preFlightTelemetryCheckComplete = 0) Then
                    _brokenRulesList.Add(New BrokenRule("PreFlightTelemetryCheckComplete", "PreFlightTelemetryCheckComplete is required", "02-00-00D", False, False))
                End If
            End If
            If (_departureTime = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("DepartureTime", "DepartureTime is required", "02-00-00E", False, False))
            Else
                If Len(_departureTime) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("DepartureTime", "DepartureTime is required", "02-00-00E", False, False))
                Else
                    If Not Regex.IsMatch(_departureTime, TimeRegex) Then
                        _brokenRulesList.Add(New BrokenRule("DepartureTime", "DepartureTime time format is invalid", "02-00-00F", False, False))
                    End If
                End If
            End If
            If (_departureTime.Length > 8) Then
                _brokenRulesList.Add(New BrokenRule("DepartureTime", "DepartureTime cannot be longer than 8", "02-00-010", False, False))
            End If
            If _hobbsStart = 0 Then
                _brokenRulesList.Add(New BrokenRule("HobbsStart", "HobbsStart is usually a non-zero value", "02-00-011", True, False))
            End If
            If (_returnTime = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("ReturnTime", "ReturnTime is required", "02-00-012", False, False))
            Else
                If Len(_returnTime) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("ReturnTime", "ReturnTime is required", "02-00-012", False, False))
                Else
                    If Not Regex.IsMatch(_returnTime, TimeRegex) Then
                        _brokenRulesList.Add(New BrokenRule("ReturnTime", "ReturnTime time format is invalid", "02-00-013", False, False))
                    End If
                End If
            End If
            If (_returnTime.Length > 8) Then
                _brokenRulesList.Add(New BrokenRule("ReturnTime", "ReturnTime cannot be longer than 8", "02-00-014", False, False))
            End If
            If (_hobbsEnd = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("HobbsEnd", "HobbsEnd is required", "02-00-015", False, False))
            Else
                If _hobbsEnd = 0 Then
                    _brokenRulesList.Add(New BrokenRule("HobbsEnd", "HobbsEnd is required", "02-00-015", False, False))
                End If
            End If
            If (_airframeHoursEnd = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("AirframeHoursEnd", "AirframeHoursEnd is required", "02-00-0A0", False, False))
            Else
                If _airframeHoursEnd = 0 Then
                    _brokenRulesList.Add(New BrokenRule("AirframeHoursEnd", "AirframeHoursEnd is required", "02-00-0A0", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("02-00-016") Then
                If (_postFlightTelemetryCheckComplete = 0) Then
                    _brokenRulesList.Add(New BrokenRule("PostFlightTelemetryCheckComplete", "PostFlightTelemetryCheckComplete is required", "02-00-016", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("02-00-017") Then
                If (_nacelleCheckBuildUp = 0) Then
                    _brokenRulesList.Add(New BrokenRule("NacelleCheckBuildUp", "NacelleCheckBuildUp is required", "02-00-017", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("02-00-018") Then
                If (_nacelleCheckDiscoloration = 0) Then
                    _brokenRulesList.Add(New BrokenRule("NacelleCheckDiscoloration", "NacelleCheckDiscoloration is required", "02-00-018", False, False))
                End If
            End If
            For Each thisLiquidFill As LiquidFill In Me.LiquidFillList
                If Not thisLiquidFill.Validate(WarningsList) Then
                    For Each thisError As BrokenRule In thisLiquidFill.ErrorsList
                        _brokenRulesList.Add(thisError)
                    Next
                    thisLiquidFill.BrokenRules.Clear()
                    For Each thisWarning As BrokenRule In thisLiquidFill.WarningsList
                        _brokenRulesList.Add(thisWarning)
                    Next
                    thisLiquidFill.WarningsList.Clear()
                End If
            Next
            For Each thisFlightComment As FlightComment In Me.FlightCommentList
                If Not thisFlightComment.Validate(WarningsList) Then
                    For Each thisError As BrokenRule In thisFlightComment.ErrorsList
                        _brokenRulesList.Add(thisError)
                    Next
                    thisFlightComment.BrokenRules.Clear()
                    For Each thisWarning As BrokenRule In thisFlightComment.WarningsList
                        _brokenRulesList.Add(thisWarning)
                    Next
                    thisFlightComment.WarningsList.Clear()
                End If
            Next
            For Each thisFlightEngineRun As FlightEngineRun In Me.FlightEngineRunList
                If Not thisFlightEngineRun.Validate(WarningsList) Then
                    For Each thisBrokenRule As BrokenRule In thisFlightEngineRun.ErrorsList
                        _brokenRulesList.Add(thisBrokenRule)
                    Next
                    thisFlightEngineRun.BrokenRules.Clear()
                    For Each thisWarning As BrokenRule In thisFlightEngineRun.WarningsList
                        _brokenRulesList.Add(thisWarning)
                    Next
                    thisFlightEngineRun.WarningsList.Clear()
                End If
            Next
            For Each thisFlightRegionTime As FlightRegionTime In Me.FlightRegionTimeList
                If Not thisFlightRegionTime.Validate(WarningsList) Then
                    For Each thisBrokenRule As BrokenRule In thisFlightRegionTime.ErrorsList
                        _brokenRulesList.Add(thisBrokenRule)
                    Next
                    thisFlightRegionTime.BrokenRules.Clear()
                    For Each thisWarning As BrokenRule In thisFlightRegionTime.WarningsList
                        _brokenRulesList.Add(thisWarning)
                    Next
                    thisFlightRegionTime.WarningsList.Clear()
                End If
            Next

            If _hobbsStart > _hobbsEnd Then
                _brokenRulesList.Add(New BrokenRule("HobbsEnd", "Ending Hobbs cannot be less than Starting Hobbs", "02-00-019", False, False))
            End If

            If _engineHoursStart > _engineHoursEnd Then
                _brokenRulesList.Add(New BrokenRule("EngineHoursEnd", "End Engine Hours of " & Format(_engineHoursEnd, "#,##0.0") & " cannot be less than Start Engine Hours of " & Format(_engineHoursStart, "#,##0.0"), "02-00-01A", False, False))
            End If

            If _engineHoursStart > _aircraftEngineHoursCurrent And _aircraftEngineHoursCurrent <> 0 Then
                _brokenRulesList.Add(New BrokenRule("EngineHoursStart", "Start Engine Hours of " & Format(_engineHoursStart, "#,##0.0") & " is greater than aircraft's current engine hourss of " & Format(_aircraftEngineHoursCurrent, "#,##0.0"), "02-00-01X", True, False))
            End If
            If _engineHoursStart < _aircraftEngineHoursCurrent Then
                _brokenRulesList.Add(New BrokenRule("EngineHoursStart", "Start Engine Hours of " & Format(_engineHoursStart, "#,##0.0") & " is less than aircraft's current engine hours of " & Format(_aircraftEngineHoursCurrent, "#,##0.0"), "02-00-01Y", True, False))
            End If

            If AppConfiguration.BusinessRuleEnabled("02-00-01B") Then
                Dim sumRegionHours As Decimal = 0.0
                For Each thisRegionTime As FlightRegionTime In Me.FlightRegionTimeList
                    sumRegionHours = sumRegionHours + thisRegionTime.Hours
                Next
                If (sumRegionHours <> (_hobbsEnd - _hobbsStart)) Then
                    _brokenRulesList.Add(New BrokenRule("MeterEnd", "Total Region Usage of " & Format(sumRegionHours, _meterMask) & " doesn't match Flight Usage of " & Format((_hobbsEnd - _hobbsStart), _meterMask), "02-00-01B", False, False))
                End If
            End If

            If _hobbsStart + _usedMeterOffset > _airframeHoursStart Then
                _brokenRulesList.Add(New BrokenRule("HobbsStart", "Hobbs Start is greater than aircraft's current meter", "02-00-01B", True, False))
            End If
            If _hobbsStart + _usedMeterOffset < _airframeHoursStart Then
                _brokenRulesList.Add(New BrokenRule("HobbsStart", "Hobbs Start is less than aircraft's current meter", "02-00-1C", True, False))
            End If

            If ((_hobbsEnd - _hobbsStart) > 2000) Then
                _brokenRulesList.Add(New BrokenRule("TotalFlightAirtimeWarning", "The total flight airtime exceeds 2000", "02-00-1E", True, False))
            End If

            If ((_engineHoursEnd - _engineHoursStart) > 24) Then
                _brokenRulesList.Add(New BrokenRule("TotalEngineHoursWarning", "The total engine hours (" & _engineHoursEnd.ToString() & " ending hours - " & _engineHoursStart.ToString() & "starting hours) exceeds 24 hours", "02-00-1F", True, False))
            End If

            For Each thisLiquidFill As LiquidFill In _liquidFillList
                If ((CDec(thisLiquidFill.HobbsReading) < _hobbsStart) Or (CDec(thisLiquidFill.HobbsReading) > _hobbsEnd)) Then
                    _brokenRulesList.Add(New BrokenRule("HobbsReading", "The Hobbs Reading for a Liquid Fill falls outside of the Flight's Hobbs range", "02-00-1D", False, False))
                End If
                If thisLiquidFill.LiquidGroupName.ToLower() = "jeta" Then
                    If ((CDec(thisLiquidFill.LiquidAmt) > 150) And (thisLiquidFill.LiquidUnitName.ToLower() = "gallon")) Then
                        _brokenRulesList.Add(New BrokenRule("Jet-AFillAmtWarning", "The Jet-A fill amount of " & thisLiquidFill.LiquidAmt.ToString() & " gallons is greater than 150 gallons", "02-00-20", True, False))
                    End If
                    If ((CDec(thisLiquidFill.LiquidAmt) > 600) And (thisLiquidFill.LiquidUnitName.ToLower() = "liter")) Then
                        _brokenRulesList.Add(New BrokenRule("Jet-AFillAmtWarning", "The Jet-A fill amount of " & thisLiquidFill.LiquidAmt.ToString() & " liters is greater than 600 liters", "02-00-21", True, False))
                    End If
                    If ((CDec(thisLiquidFill.LiquidAmt) > 600) And (thisLiquidFill.LiquidUnitName.ToLower() = "quart")) Then
                        _brokenRulesList.Add(New BrokenRule("Jet-AFillAmtWarning", "The Jet-A fill amount of " & thisLiquidFill.LiquidAmt.ToString() & " quarts is greater than 600 quarts", "02-00-22", True, False))
                    End If
                End If
                If thisLiquidFill.LiquidGroupName.ToLower() = "coolant" Then
                    If ((CDec(thisLiquidFill.LiquidAmt) > 5) And (thisLiquidFill.LiquidUnitName.ToLower() = "gallon")) Then
                        _brokenRulesList.Add(New BrokenRule("CoolantFillAmtWarning", "The Coolant fill amount of " & thisLiquidFill.LiquidAmt.ToString() & " gallons is greater than 5 gallons", "02-00-20", True, False))
                    End If
                    If ((CDec(thisLiquidFill.LiquidAmt) > 20) And (thisLiquidFill.LiquidUnitName.ToLower() = "liter")) Then
                        _brokenRulesList.Add(New BrokenRule("CoolantFillAmtWarning", "The Coolant fill amount of " & thisLiquidFill.LiquidAmt.ToString() & " liters is greater than 20 liters", "02-00-21", True, False))
                    End If
                    If ((CDec(thisLiquidFill.LiquidAmt) > 20) And (thisLiquidFill.LiquidUnitName.ToLower() = "quart")) Then
                        _brokenRulesList.Add(New BrokenRule("CoolantFillAmtWarning", "The Coolant fill amount of " & thisLiquidFill.LiquidAmt.ToString() & " quarts is greater than 20 quarts", "02-00-22", True, False))
                    End If
                End If
                If thisLiquidFill.LiquidGroupName.ToLower() = "hydraulic" Then
                    If ((CDec(thisLiquidFill.LiquidAmt) > 7) And (thisLiquidFill.LiquidUnitName.ToLower() = "gallon")) Then
                        _brokenRulesList.Add(New BrokenRule("HydraulicFillAmtWarning", "The Hydraulic fill amount of " & thisLiquidFill.LiquidAmt.ToString() & " gallons is greater than 7 gallon", "02-00-20", True, False))
                    End If
                    If ((CDec(thisLiquidFill.LiquidAmt) > 30) And (thisLiquidFill.LiquidUnitName.ToLower() = "liter")) Then
                        _brokenRulesList.Add(New BrokenRule("HydraulicFillAmtWarning", "The Hydraulic fill amount of " & thisLiquidFill.LiquidAmt.ToString() & " liters is greater than 30 liters", "02-00-21", True, False))
                    End If
                    If ((CDec(thisLiquidFill.LiquidAmt) > 30) And (thisLiquidFill.LiquidUnitName.ToLower() = "quart")) Then
                        _brokenRulesList.Add(New BrokenRule("HydraulicFillAmtWarning", "The Hydraulic fill amount of " & thisLiquidFill.LiquidAmt.ToString() & " quarts is greater than 30 quarts", "02-00-22", True, False))
                    End If
                End If
            Next

            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

        End Function

    End Class

End Namespace
