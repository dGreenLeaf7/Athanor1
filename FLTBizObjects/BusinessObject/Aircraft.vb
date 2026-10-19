' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

	<Serializable()> _
	Public Class Aircraft

        Private Const TimeRegex As String = "^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]?(\s)?(AM|am|Am|aM|PM|pm|Pm|pM))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]))$"
        Private _meterMask As String = "#,##0.0;-#,##0.0"
        Private _dateMask As String = "MM-dd-yyyy"
        Private _sortDateMask As String = "yyyy-MM-dd"

        Private _aircraftId As Integer
        Private _progId As Integer
        Private _currentLocationId As Integer
        Private _currentLocation As String
		Private _mSN As String
		Private _shipNbr As String
		Private _serialNbr As String
        Private _modelId As Integer
        Private _model As String
		Private _description As String
        Private _engineerId As Integer
        Private _engineer As String
        Private _mechanicId As Integer
        Private _mechanic As String
		Private _registrationNbr As String
		Private _liveryColor As String
        Private _aircraftStatusId As Integer
        Private _aircraftStatus As String
        Private _manufacturerId As Integer
        Private _manufacturer As String
        Private _meterUnitId As Integer
        Private _meterUnit As String
        Private _airframeHoursStart As Decimal
        Private _airframeHoursCurrent As Decimal
        Private _airframeHoursOffset As Decimal
        Private _totalRequiredUsage As Decimal
        Private _aircraftEngineHoursCurrent As Decimal
        Private _currentFlightUsage As Decimal
        Private _estEndOfTesting As String
        Private _actEndOfTesting As String
        Private _aircraftGuid As Guid
        Private _isActive As Boolean
        Private _isDeleted As Boolean
        Private _utcDateCreated As DateTime
        Private _utcDateModified As DateTime
        Private _StampUserName As String
        Private _concurrencyId As String

        Private _aircraftConfigurationList As AircraftConfigurationList = Nothing
        Private _aircraftConfigurationDeleteList As AircraftConfigurationList = Nothing
        Private _newAircraftConfigurationId As Integer = -1

        Private _aircraftLocationList As AircraftLocationList = Nothing
        Private _aircraftLocationDeleteList As AircraftLocationList = Nothing
        Private _newAircraftLocationId As Integer = -1

        Private _genericCommentList As GenericCommentList = Nothing
        Private _genericCommentDeleteList As GenericCommentList = Nothing
        Private _newGenericCommentId As Integer = -1

        Private _TestList As fltLookupBaseList = Nothing
        Private _TestDeleteList As fltLookupBaseList = Nothing

        Private _flightInfoList As FlightInfoList = Nothing

        Private _workOrderInfoList As WorkOrderInfoList = Nothing

        Private _brokenRulesList As BrokenRulesList = Nothing
        Private _errorsList As BrokenRulesList = Nothing
        Private _warningsList As BrokenRulesList = Nothing

        <DataObjectFieldAttribute(True, True, False)> _
        Public Property AircraftId() As Integer
            Get
                Return _aircraftId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _aircraftId = 0
                Else
                    _aircraftId = value
                End If
            End Set
        End Property

        Public Property ProgId() As Integer
            Get
                Return _progId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _progId = 0
                Else
                    _progId = value
                End If
            End Set
        End Property

        Public Property CurrentLocationId() As Integer
            Get
                Return _currentLocationId
            End Get
            Friend Set(ByVal value As Integer)
                _currentLocationId = value
            End Set
        End Property

        Public Property CurrentLocation() As String
            Get
                Return _currentLocation
            End Get
            Friend Set(ByVal value As String)
                _currentLocation = value
            End Set
        End Property

        Public Property MSN() As String
            Get
                Return _mSN
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _mSN = String.Empty
                Else
                    _mSN = value
                End If
            End Set
        End Property

        Public Property ShipNbr() As String
            Get
                Return _shipNbr
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _shipNbr = String.Empty
                Else
                    _shipNbr = value
                End If
            End Set
        End Property

        Public Property SerialNbr() As String
            Get
                Return _serialNbr
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _serialNbr = String.Empty
                Else
                    _serialNbr = value
                End If
            End Set
        End Property

        Public Property ModelId() As Integer
            Get
                Return _modelId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _modelId = 0
                Else
                    _modelId = value
                End If
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
                If (value = Nothing) Then
                    _description = String.Empty
                Else
                    _description = value
                End If
            End Set
        End Property

        Public Property EngineerId() As Integer
            Get
                Return _engineerId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _engineerId = 0
                Else
                    _engineerId = value
                End If
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

        Public Property MechanicId() As Integer
            Get
                Return _mechanicId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _mechanicId = 0
                Else
                    _mechanicId = value
                End If
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

        Public Property RegistrationNbr() As String
            Get
                Return _registrationNbr
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _registrationNbr = String.Empty
                Else
                    _registrationNbr = value
                End If
            End Set
        End Property

        Public Property LiveryColor() As String
            Get
                Return _liveryColor
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _liveryColor = String.Empty
                Else
                    _liveryColor = value
                End If
            End Set
        End Property

        Public Property AircraftStatusId() As Integer
            Get
                Return _aircraftStatusId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _aircraftStatusId = 0
                Else
                    _aircraftStatusId = value
                End If
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

        Public Property ManufacturerId() As Integer
            Get
                Return _manufacturerId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _manufacturerId = 0
                Else
                    _manufacturerId = value
                End If
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

        Public Property MeterUnitId() As Integer
            Get
                Return _meterUnitId
            End Get
            Set(ByVal value As Integer)
                If value = Nothing Then
                    _meterUnitId = 0
                Else
                    _meterUnitId = value
                End If
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

        Public Property AirframeHoursStart() As String
            Get
                Return Format(_airframeHoursStart, _meterMask)
            End Get
            Friend Set(ByVal value As String)
                If value = Nothing Then
                    _airframeHoursStart = 0
                ElseIf IsNumeric(value) Then
                    _airframeHoursStart = CDec(value)
                Else
                    _airframeHoursStart = 0
                End If
            End Set
        End Property

        Public Property AirframeHoursCurrent() As String
            Get
                Return Format(_airframeHoursCurrent, _meterMask)
            End Get
            Friend Set(ByVal value As String)
                If value = Nothing Then
                    _airframeHoursCurrent = 0
                ElseIf IsNumeric(value) Then
                    _airframeHoursCurrent = CDec(value)
                Else
                    _airframeHoursCurrent = 0
                End If
            End Set
        End Property

        Public Property AirframeHoursOffset() As String
            Get
                Return Format(_airframeHoursOffset, _meterMask)
            End Get
            Friend Set(ByVal value As String)
                If value = Nothing Then
                    _airframeHoursOffset = 0
                ElseIf IsNumeric(value) Then
                    _airframeHoursOffset = CDec(value)
                Else
                    _airframeHoursOffset = 0
                End If
            End Set
        End Property

        Public Property TotalRequiredUsage() As String
            Get
                Return Format(_totalRequiredUsage, _meterMask)
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _totalRequiredUsage = 0
                ElseIf IsNumeric(value) Then
                    _totalRequiredUsage = CDec(value)
                Else
                    _totalRequiredUsage = 0
                End If
            End Set
        End Property

        Public Property CurrentFlightUsage() As String
            Get
                Return Format(_currentFlightUsage, _meterMask)
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _currentFlightUsage = 0
                ElseIf IsNumeric(value) Then
                    _currentFlightUsage = CDec(value)
                Else
                    _currentFlightUsage = 0
                End If
            End Set
        End Property

        Public Property AircraftEngineHoursCurrent As String
            Get
                Return (Format(_aircraftEngineHoursCurrent, _meterMask))
            End Get
            Set(value As String)
                If String.IsNullOrEmpty(value) Then
                    _aircraftEngineHoursCurrent = 0
                Else
                    If Not Decimal.TryParse(value, _aircraftEngineHoursCurrent) Then
                        _aircraftEngineHoursCurrent = 0
                    End If
                End If
                If _aircraftEngineHoursCurrent < 0 Then
                    _aircraftEngineHoursCurrent = 0
                End If
            End Set
        End Property

        Public Property EstEndOfTesting() As String
            Get
                Return _estEndOfTesting
            End Get
            Set(ByVal value As String)
                If IsDate(value) Then
                    _estEndOfTesting = Format(CDate(value), _dateMask)
                Else
                    _estEndOfTesting = value
                End If
            End Set
        End Property

        Public Property ActEndOfTesting() As String
            Get
                Return _actEndOfTesting
            End Get
            Set(ByVal value As String)
                If IsDate(value) Then
                    _actEndOfTesting = Format(CDate(value), _dateMask)
                Else
                    _actEndOfTesting = value
                End If
            End Set
        End Property

        Public Property AircraftGuid() As Guid
            Get
                Return _aircraftGuid
            End Get
            Set(ByVal value As Guid)
                If (value = Nothing) Then
                    _aircraftGuid = Guid.Empty
                Else
                    _aircraftGuid = value
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

        Public Property AircraftConfigurationList() As AircraftConfigurationList
            Get
                Return _aircraftConfigurationList
            End Get
            Friend Set(ByVal value As AircraftConfigurationList)
                _aircraftConfigurationList = value
            End Set
        End Property

        Public Property AircraftLocationList() As AircraftLocationList
            Get
                Return _aircraftLocationList
            End Get
            Friend Set(ByVal value As AircraftLocationList)
                _aircraftLocationList = value
            End Set
        End Property

        Public Property GenericCommentList() As GenericCommentList
            Get
                Return _genericCommentList
            End Get
            Set(ByVal value As GenericCommentList)
                _genericCommentList = value
            End Set
        End Property

        Public Property TestList() As fltLookupBaseList
            Get
                Return _TestList
            End Get
            Friend Set(ByVal value As fltLookupBaseList)
                _TestList = value
            End Set
        End Property

        Public Property FlightInfoList() As FlightInfoList
            Get
                Return _flightInfoList
            End Get
            Friend Set(ByVal value As FlightInfoList)
                _flightInfoList = value
            End Set
        End Property

        Public Property WorkOrderInfoList() As WorkOrderInfoList
            Get
                Return _workOrderInfoList
            End Get
            Friend Set(ByVal value As WorkOrderInfoList)
                _workOrderInfoList = value
            End Set
        End Property

        Friend Property AircraftConfigurationDeleteList() As AircraftConfigurationList
            Get
                Return _aircraftConfigurationDeleteList
            End Get
            Set(ByVal value As AircraftConfigurationList)
                _aircraftConfigurationDeleteList = value
            End Set
        End Property

        Friend Property AircraftLocationDeleteList() As AircraftLocationList
            Get
                Return _aircraftLocationDeleteList
            End Get
            Set(ByVal value As AircraftLocationList)
                _aircraftLocationDeleteList = value
            End Set
        End Property

        Friend Property GenericCommentDeleteList() As GenericCommentList
            Get
                Return _genericCommentDeleteList
            End Get
            Set(ByVal value As GenericCommentList)
                _genericCommentDeleteList = value
            End Set
        End Property

        Friend Property TestDeleteList() As fltLookupBaseList
            Get
                Return _TestDeleteList
            End Get
            Set(ByVal value As fltLookupBaseList)
                _TestDeleteList = value
            End Set
        End Property

        Friend Property NewAircraftConfigurationId() As Integer
            Get
                Return _newAircraftConfigurationId
            End Get
            Set(ByVal value As Integer)
                _newAircraftConfigurationId = value
            End Set
        End Property

        Friend Property NewAircraftLocationId() As Integer
            Get
                Return _newAircraftLocationId
            End Get
            Set(ByVal value As Integer)
                _newAircraftLocationId = value
            End Set
        End Property

        Friend Property NewGenericCommentId() As Integer
            Get
                Return _newGenericCommentId
            End Get
            Set(ByVal value As Integer)
                _newGenericCommentId = value
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
            Me.AircraftId = -1
            Me.ProgId = 0
            Me.CurrentLocationId = 0
            Me.CurrentLocation = String.Empty
            Me.MSN = String.Empty
            Me.ShipNbr = String.Empty
            Me.SerialNbr = String.Empty
            Me.ModelId = 0
            Me.Model = String.Empty
            Me.Description = String.Empty
            Me.EngineerId = 0
            Me.Engineer = String.Empty
            Me.MechanicId = 0
            Me.Mechanic = String.Empty
            Me.RegistrationNbr = String.Empty
            Me.LiveryColor = String.Empty
            Me.AircraftStatusId = 0
            Me.AircraftStatus = String.Empty
            Me.ManufacturerId = 0
            Me.Manufacturer = String.Empty
            Me.MeterUnitId = 0
            Me.MeterUnit = String.Empty
            Me.AirframeHoursStart = 0
            Me.AirframeHoursCurrent = 0
            Me.AirframeHoursOffset = 0
            Me.TotalRequiredUsage = 0
            Me.CurrentFlightUsage = 0
            Me.EstEndOfTesting = String.Empty
            Me.ActEndOfTesting = String.Empty
            Me.AircraftGuid = Guid.Empty
            Me.IsActive = True
            Me.IsDeleted = False
            Me.UtcDateCreated = Nothing
            Me.UtcDateModified = Nothing
            Me.StampUserName = String.Empty
            Me.ConcurrencyId = "0"

            Me.AircraftConfigurationList = New AircraftConfigurationList
            Me.AircraftConfigurationDeleteList = New AircraftConfigurationList
            Me.NewAircraftConfigurationId = -1

            Me.AircraftLocationList = New AircraftLocationList
            Me.AircraftLocationDeleteList = New AircraftLocationList
            Me.NewAircraftLocationId = -1

            Me.genericCommentList = New GenericCommentList
            Me.genericCommentDeleteList = New GenericCommentList
            Me.NewgenericCommentId = -1

            Me.TestList = New fltLookupBaseList
            Me.TestDeleteList = New fltLookupBaseList

            Me.FlightInfoList = New FlightInfoList

            Me.WorkOrderInfoList = New WorkOrderInfoList

            _brokenRulesList = New BrokenRulesList
            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

        End Sub

        Public Function Validate() As Boolean
            Return Validate(_warningsList)
        End Function

        Friend Function Validate(ByVal WarningsList As BrokenRulesList) As Boolean

            Dim prevWarningsList As New BrokenRulesList
            For Each thisBrokenRule As BrokenRule In WarningsList
                prevWarningsList.Add(thisBrokenRule)
            Next
            _brokenRulesList = New BrokenRulesList

            If (_progId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("ProgId", "ProgId is required", "01-00-001", False, False))
            Else
                If Len(_progId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("ProgId", "ProgId is required", "01-00-001", False, False))
                End If
            End If
            If (_currentLocationId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("CurrentLocationId", "CurrentLocationId is required", "01-00-002", False, False))
            Else
                If Len(_currentLocationId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("CurrentLocationId", "CurrentLocationId is required", "01-00-002", False, False))
                End If
            End If
            If (_mSN = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("MSN", "MSN is required", "01-00-003", False, False))
            Else
                If Len(_mSN) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("MSN", "MSN is required", "01-00-003", False, False))
                End If
            End If
            If (_mSN.Length > 20) Then
                _brokenRulesList.Add(New BrokenRule("MSN", "MSN cannot be longer than 20", "01-00-004", False, False))
            End If
            If ObjectPropertyValidator.ValidateCommonString(_mSN) = False Then
                _brokenRulesList.Add(New BrokenRule("MSN", "MSN" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If
            If (_shipNbr = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("ShipNbr", "ShipNbr is required", "01-00-005", False, False))
            Else
                If Len(_shipNbr) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("ShipNbr", "ShipNbr is required", "01-00-005", False, False))
                End If
            End If
            If (_shipNbr.Length > 15) Then
                _brokenRulesList.Add(New BrokenRule("ShipNbr", "ShipNbr cannot be longer than 15", "01-00-006", False, False))
            End If
            If (_serialNbr = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("SerialNbr", "SerialNbr is required", "01-00-007", False, False))
            Else
                If Len(_serialNbr) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("SerialNbr", "SerialNbr is required", "01-00-007", False, False))
                End If
            End If
            If (_serialNbr.Length > 6) Then
                _brokenRulesList.Add(New BrokenRule("SerialNbr", "SerialNbr cannot be longer than 6", "01-00-008", False, False))
            End If
            If (_modelId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("ModelId", "ModelId is required", "01-00-009", False, False))
            Else
                If Len(_modelId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("ModelId", "ModelId is required", "01-00-009", False, False))
                End If
            End If
            If (_description.Length > 255) Then
                _brokenRulesList.Add(New BrokenRule("Description", "Description cannot be longer than 255", "01-00-00A", False, False))
            End If
            If (_engineerId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("EngineerId", "EngineerId is required", "01-00-00B", False, False))
            Else
                If Len(_engineerId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("EngineerId", "EngineerId is required", "01-00-00B", False, False))
                End If
            End If
            If (_mechanicId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("MechanicId", "MechanicId is required", "01-00-00C", False, False))
            Else
                If Len(_mechanicId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("MechanicId", "MechanicId is required", "01-00-00C", False, False))
                End If
            End If
            If (_registrationNbr.Length > 10) Then
                _brokenRulesList.Add(New BrokenRule("RegistrationNbr", "RegistrationNbr cannot be longer than 10", "01-00-00D", False, False))
            End If
            If (_liveryColor.Length > 10) Then
                _brokenRulesList.Add(New BrokenRule("LiveryColor", "LiveryColor cannot be longer than 10", "01-00-00E", False, False))
            End If
            If (_aircraftStatusId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("AircraftStatusId", "AircraftStatusId is required", "01-00-00F", False, False))
            Else
                If Len(_aircraftStatusId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("AircraftStatusId", "AircraftStatusId is required", "01-00-00F", False, False))
                End If
            End If
            If (_manufacturerId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("ManufacturerId", "ManufacturerId is required", "01-00-010", False, False))
            Else
                If Len(_manufacturerId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("ManufacturerId", "ManufacturerId is required", "01-00-010", False, False))
                End If
            End If
            If (_meterUnitId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("MeterUnitId", "MeterUnitId is required", "01-00-011", False, False))
            Else
                If Len(_meterUnitId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("MeterUnitId", "MeterUnitId is required", "01-00-011", False, False))
                End If
            End If

            For Each thisAircraftConfiguration As AircraftConfiguration In _aircraftConfigurationList
                If Not (thisAircraftConfiguration.Validate(WarningsList)) Then
                    For Each thisBrokenRule As BrokenRule In thisAircraftConfiguration.BrokenRules
                        _brokenRulesList.Add(thisBrokenRule)
                    Next
                    thisAircraftConfiguration.BrokenRules.Clear()
                    For Each thisWarning As BrokenRule In thisAircraftConfiguration.WarningsList
                        _brokenRulesList.Add(thisWarning)
                    Next
                    thisAircraftConfiguration.WarningsList.Clear()
                End If
            Next
            For Each thisAircraftLocation As AircraftLocation In _aircraftLocationList
                If Not (thisAircraftLocation.Validate(WarningsList)) Then
                    For Each thisBrokenRule As BrokenRule In thisAircraftLocation.BrokenRules
                        _brokenRulesList.Add(thisBrokenRule)
                    Next
                    thisAircraftLocation.BrokenRules.Clear()
                    For Each thisWarning As BrokenRule In thisAircraftLocation.WarningsList
                        _brokenRulesList.Add(thisWarning)
                    Next
                    thisAircraftLocation.WarningsList.Clear()
                End If
            Next
            For Each thisComment As GenericComment In _genericCommentList
                If Not (thisComment.Validate(WarningsList)) Then
                    For Each thisBrokenRule As BrokenRule In thisComment.BrokenRules
                        _brokenRulesList.Add(thisBrokenRule)
                    Next
                    thisComment.BrokenRules.Clear()
                    For Each thisWarning As BrokenRule In thisComment.WarningsList
                        _brokenRulesList.Add(thisWarning)
                    Next
                    thisComment.WarningsList.Clear()
                End If
            Next


            If _aircraftConfigurationList.Count = 0 Then
                _brokenRulesList.Add(New BrokenRule("AircraftConfiguration", "There must be at least one Aircraft Configuration entry", "01-00-012", False, False))
            End If

            If _aircraftLocationList.Count = 0 Then
                _brokenRulesList.Add(New BrokenRule("AircraftLocation", "There must be at least one Aircraft Location entry", "01-00-013", False, False))
            End If

            If Not (_aircraftId > 0) Then
                If _aircraftConfigurationList.Count <> 1 Then
                    _brokenRulesList.Add(New BrokenRule("AircraftConfiguration", "When adding a new aircraft, there must be exactly one Aircraft Configuration entry", "01-00-014", False, False))
                End If
                If _aircraftLocationList.Count <> 1 Then
                    _brokenRulesList.Add(New BrokenRule("AircraftLocation", "When adding a new aircraft, there must be exactly one Aircraft Location entry", "01-00-015", False, False))
                End If
            End If

            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

        End Function

    End Class
		
End Namespace
