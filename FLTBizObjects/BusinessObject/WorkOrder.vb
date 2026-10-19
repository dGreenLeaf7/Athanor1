' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data.SqlTypes
Imports System.Diagnostics
Imports System.Text.RegularExpressions

Namespace FLT.BO


    <Serializable()> _
    Public Class WorkOrder

        Private Const TimeRegex As String = "^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]?(\s)?(AM|am|Am|aM|PM|pm|Pm|pM))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]))$"

        Private _workOrderId As Integer
        Private _workOrderNbr As String
        Private _progId As Integer
        Private _aircraftId As Integer
        Private _msn As String
        Private _tailNbr As String
        Private _locationId As Integer
        Private _locationName As String
        Private _workOrderTypeId As Integer
        Private _workOrderType As String
        Private _workOrderStatusId As Integer
        Private _workOrderStatus As String
        Private _engineerId As Integer
        Private _engineerName As String
        Private _engineeringActionId As Integer
        Private _engineeringAction As String
        Private _mechanicId As Long
        Private _mechanicName As String
        Private _requestDate As String
        Private _faultOccurredDate As DateTime
        Private _faultOccurredMeter As Decimal
        Private _fISComplaintNbr As String
        Private _squawkNbr As String
        Private _dueDate As String
        Private _dueTime As String
        Private _finishDate As String
        Private _finishtime As String
        Private _aircraftIssueAlert As Boolean
        Private _maintIssueAlert As Boolean
        Private _meterStartWork As Decimal
        Private _meterEndWork As Decimal
        Private _aircraftDispostion As String
        Private _workOrderGuid As Guid
        Private _isDeleted As Boolean
        Private _utcDateCreated As DateTime
        Private _utcDateModified As DateTime
        Private _StampUserName As String
        Private _concurrencyId As String

        Private _woReasonList As WOReasonList = Nothing
        Private _woReasonDeleteList As WOReasonList = Nothing
        Private _newWoReasonId As Integer = -1
        Private _nextWOReasonLineNbr As Integer = -1

        Private _woInstructionList As WOInstructionList = Nothing
        Private _woInstructionDeleteList As WOInstructionList = Nothing
        Private _newWoInstructionId As Integer = -1
        Private _nextWOInstructionLineNbr As Integer = -1

        Private _woPerformedWorkList As WOPerformedWorkList = Nothing
        Private _woPerformedWorkDeleteList As WOPerformedWorkList = Nothing
        Private _newWoPerformedWorkId As Integer = -1
        Private _nextWOPerformedWorkLineNbr As Integer = -1

        Private _woPartList As WorkOrderPartList = Nothing
        Private _woPartDeleteList As WorkOrderPartList = Nothing
        Private _newWoPartId As Integer = -1

        Private _woEngineerCommentList As WOEngineerCommentList = Nothing
        Private _woEngineerCommentDeleteList As WOEngineerCommentList = Nothing
        Private _newWoEngineerCommentId As Integer = -1
        Private _nextWOEngineerCommentLineNbr As Integer = -1

        Private _woMechanicCommentList As WOMechanicCommentList = Nothing
        Private _woMechanicCommentDeleteList As WOMechanicCommentList = Nothing
        Private _newWoMechanicCommentId As Integer = -1
        Private _nextWOMechanicCommentLineNbr As Integer = -1

        Private _woDocumentInfosList As DocumentInfoList = Nothing
        Private _woDocumentInfosDeleteList As DocumentInfoList = Nothing
        Private _newWoDocumentId As Integer = -1

        Private _brokenRulesList As BrokenRulesList = Nothing
        Private _errorsList As BrokenRulesList = Nothing
        Private _warningsList As BrokenRulesList = Nothing

        <DataObjectFieldAttribute(True, True, False)> _
        Public Property WorkOrderId() As Long
            Get
                Return _workOrderId
            End Get
            Set(ByVal value As Long)
                If (value = Nothing) Then
                    _workOrderId = 0
                Else
                    _workOrderId = value
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

        Public Property MSN() As String
            Get
                Return _msn
            End Get
            Set(ByVal value As String)
                _msn = value
            End Set
        End Property

        Public Property TailNbr() As String
            Get
                Return _tailNbr
            End Get
            Set(ByVal value As String)
                _tailNbr = value
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

        Public Property LocationName() As String
            Get
                Return _locationName
            End Get
            Set(ByVal value As String)
                _locationName = value
            End Set
        End Property

        Public Property WorkOrderStatusId() As Integer
            Get
                Return _workOrderStatusId
            End Get
            Set(ByVal value As Integer)
                _workOrderStatusId = value
            End Set
        End Property

        Public Property WorkOrderStatus() As String
            Get
                Return _workOrderStatus
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _workOrderStatus = String.Empty
                Else
                    _workOrderStatus = value
                End If
            End Set
        End Property

        Public Property FaultOccurredDate() As String
            Get
                If _faultOccurredDate < CDate("1-Jan-1950") Then
                    Return String.Empty
                Else
                    Return Format(_faultOccurredDate, "MM-dd-yyyy")
                End If
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _faultOccurredDate = Nothing
                ElseIf IsDate(value) Then
                    _faultOccurredDate = CDate(value)
                Else
                    _faultOccurredDate = Nothing
                End If
            End Set
        End Property

        Public Property FaultOccurredMeter() As String
            Get
                Return Format(_faultOccurredMeter, "#,##0.0")
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _faultOccurredMeter = 0
                ElseIf IsNumeric(value) Then
                    _faultOccurredMeter = CDec(value)
                Else
                    _faultOccurredMeter = 0
                End If
            End Set
        End Property

        Public Property FISComplaintNbr() As String
            Get
                Return _fISComplaintNbr
            End Get
            Set(ByVal value As String)
                _fISComplaintNbr = value
            End Set
        End Property

        Public Property SquawkNbr() As String
            Get
                Return _squawkNbr
            End Get
            Set(ByVal value As String)
                _squawkNbr = value
            End Set
        End Property

        Public Property RequestDate() As String
            Get
                Return _requestDate
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _requestDate = Nothing
                Else
                    _requestDate = Format(CDate(value), "MM-dd-yyyy")
                End If
            End Set
        End Property

        Public Property DueDate() As String
            Get
                Return _dueDate
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _dueDate = String.Empty
                ElseIf IsDate(value) Then
                    _dueDate = Format(CDate(value), "MM-dd-yyyy")
                Else
                    _dueDate = Trim(value)
                End If
            End Set
        End Property

        Public Property DueTime() As String
            Get
                Return _dueTime
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _dueTime = String.Empty
                Else
                    _dueTime = Trim(value)
                End If
            End Set
        End Property

        Public Property FinishDate() As String
            Get
                Return _finishDate
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _finishDate = String.Empty
                ElseIf IsDate(value) Then
                    _finishDate = Format(CDate(value), "MM-dd-yyyy")
                Else
                    _finishDate = Trim(value)
                End If
            End Set
        End Property

        Public Property FinishTime() As String
            Get
                Return _finishtime
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _finishtime = String.Empty
                Else
                    _finishtime = Trim(value)
                End If
            End Set
        End Property

        Public Property AircraftIssueAlert() As Boolean
            Get
                Return _aircraftIssueAlert
            End Get
            Set(ByVal value As Boolean)
                _aircraftIssueAlert = value
            End Set
        End Property

        Public Property MaintIssueAlert() As Boolean
            Get
                Return _maintIssueAlert
            End Get
            Set(ByVal value As Boolean)
                _maintIssueAlert = value
            End Set
        End Property

        Public Property WorkOrderTypeId() As Integer
            Get
                Return _workOrderTypeId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _workOrderTypeId = 0
                Else
                    _workOrderTypeId = value
                End If
            End Set
        End Property

        Public Property WorkOrderType() As String
            Get
                Return _workOrderType
            End Get
            Set(ByVal value As String)
                _workOrderType = value
            End Set
        End Property

        Public Property WorkOrderNbr() As String
            Get
                Return _workOrderNbr
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _workOrderNbr = String.Empty
                Else
                    _workOrderNbr = value
                End If
            End Set
        End Property

        Public Property MeterStartWork() As String
            Get
                Return Format(_meterStartWork, "#,##0.0")
            End Get
            Set(ByVal value As String)
                If Not (value = Nothing) Then
                    If IsNumeric(value) Then
                        _meterStartWork = CDec(value)
                    End If
                End If
            End Set
        End Property

        Public Property MeterEndWork() As String
            Get
                Return Format(_meterEndWork, "#,##0.0")
            End Get
            Set(ByVal value As String)
                If Not (value = Nothing) Then
                    If IsNumeric(value) Then
                        _meterEndWork = CDec(value)
                    End If
                End If
            End Set
        End Property

        Public Property EngineerId() As Integer
            Get
                Return _engineerId
            End Get
            Set(ByVal value As Integer)
                _engineerId = value
            End Set
        End Property

        Public Property EngineerName() As String
            Get
                Return _engineerName
            End Get
            Set(ByVal value As String)
                _engineerName = value
            End Set
        End Property

        Public Property MechanicId() As Long
            Get
                Return _mechanicId
            End Get
            Set(ByVal value As Long)
                If (value = Nothing) Then
                    _mechanicId = 0
                Else
                    _mechanicId = value
                End If
            End Set
        End Property

        Public Property MechanicName() As String
            Get
                Return _mechanicName
            End Get
            Set(ByVal value As String)
                _mechanicName = value
            End Set
        End Property

        Public Property EngineeringActionId() As Integer
            Get
                Return _engineeringActionId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _engineeringActionId = 0
                Else
                    _engineeringActionId = value
                End If
            End Set
        End Property

        Public Property EngineeringAction() As String
            Get
                Return _engineeringAction
            End Get
            Set(ByVal value As String)
                _engineeringAction = value
            End Set
        End Property

        Public Property AircraftDispostion() As String
            Get
                Return _aircraftDispostion
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _aircraftDispostion = String.Empty
                Else
                    _aircraftDispostion = value
                End If
            End Set
        End Property

        Public Property WorkOrderGuid() As Guid
            Get
                Return _workOrderGuid
            End Get
            Set(ByVal value As Guid)
                If (value = Nothing) Then
                    _workOrderGuid = Guid.Empty
                Else
                    _workOrderGuid = value
                End If
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

        Public Property WOReasonList() As WOReasonList
            Get
                Return _woReasonList
            End Get
            Set(ByVal value As WOReasonList)
                _woReasonList = value
            End Set
        End Property

        Public Property WOReasonDeleteList() As WOReasonList
            Get
                Return _woReasonDeleteList
            End Get
            Set(ByVal value As WOReasonList)
                _woReasonDeleteList = value
            End Set
        End Property

        Public Property NewWoReasonId() As Integer
            Get
                Return _newWoReasonId
            End Get
            Set(ByVal value As Integer)
                _newWoReasonId = value
            End Set
        End Property

        Public Property WOInstructionList() As WOInstructionList
            Get
                Return _woInstructionList
            End Get
            Set(ByVal value As WOInstructionList)
                _woInstructionList = value
            End Set
        End Property

        Public Property WOInstructionDeleteList() As WOInstructionList
            Get
                Return _woInstructionDeleteList
            End Get
            Set(ByVal value As WOInstructionList)
                _woInstructionDeleteList = value
            End Set
        End Property

        Public Property NewWoInstructionId() As Integer
            Get
                Return _newWoInstructionId
            End Get
            Set(ByVal value As Integer)
                _newWoInstructionId = value
            End Set
        End Property

        Public Property WOPerformedWorkList() As WOPerformedWorkList
            Get
                Return _woPerformedWorkList
            End Get
            Set(ByVal value As WOPerformedWorkList)
                _woPerformedWorkList = value
            End Set
        End Property

        Public Property WOPerformedWorkDeleteList() As WOPerformedWorkList
            Get
                Return _woPerformedWorkDeleteList
            End Get
            Set(ByVal value As WOPerformedWorkList)
                _woPerformedWorkDeleteList = value
            End Set
        End Property

        Public Property NewWoPerformedWorkId() As Integer
            Get
                Return _newWoPerformedWorkId
            End Get
            Set(ByVal value As Integer)
                _newWoPerformedWorkId = value
            End Set
        End Property

        Public Property WOPartList() As WorkOrderPartList
            Get
                Return _woPartList
            End Get
            Set(ByVal value As WorkOrderPartList)
                _woPartList = value
            End Set
        End Property

        Public Property WOPartDeleteList() As WorkOrderPartList
            Get
                Return _woPartDeleteList
            End Get
            Set(ByVal value As WorkOrderPartList)
                _woPartDeleteList = value
            End Set
        End Property

        Public Property NewWoPartId() As Integer
            Get
                Return _newWoPartId
            End Get
            Set(ByVal value As Integer)
                _newWoPartId = value
            End Set
        End Property

        Public Property WOEngineerCommentList() As WOEngineerCommentList
            Get
                Return _woEngineerCommentList
            End Get
            Set(ByVal value As WOEngineerCommentList)
                _woEngineerCommentList = value
            End Set
        End Property

        Public Property WOEngineerCommentDeleteList() As WOEngineerCommentList
            Get
                Return _woEngineerCommentDeleteList
            End Get
            Set(ByVal value As WOEngineerCommentList)
                _woEngineerCommentDeleteList = value
            End Set
        End Property

        Public Property NewWoEngineerCommentId() As Integer
            Get
                Return _newWoEngineerCommentId
            End Get
            Set(ByVal value As Integer)
                _newWoEngineerCommentId = value
            End Set
        End Property

        Public Property WOMechanicCommentList() As WOMechanicCommentList
            Get
                Return _woMechanicCommentList
            End Get
            Set(ByVal value As WOMechanicCommentList)
                _woMechanicCommentList = value
            End Set
        End Property

        Public Property WOMechanicCommentDeleteList() As WOMechanicCommentList
            Get
                Return _woMechanicCommentDeleteList
            End Get
            Set(ByVal value As WOMechanicCommentList)
                _woMechanicCommentDeleteList = value
            End Set
        End Property

        Public Property NewWoMechanicCommentId() As Integer
            Get
                Return _newWoMechanicCommentId
            End Get
            Set(ByVal value As Integer)
                _newWoMechanicCommentId = value
            End Set
        End Property

        Public Property WODocumentInfosList() As DocumentInfoList
            Get
                Return _woDocumentInfosList
            End Get
            Set(ByVal value As DocumentInfoList)
                _woDocumentInfosList = value
            End Set
        End Property

        Public Property WODocumentInfosDeleteList() As DocumentInfoList
            Get
                Return _woDocumentInfosDeleteList
            End Get
            Set(ByVal value As DocumentInfoList)
                _woDocumentInfosDeleteList = value
            End Set
        End Property

        Public Property NewWoDocumentId() As Integer
            Get
                Return _newWoDocumentId
            End Get
            Set(ByVal value As Integer)
                _newWoDocumentId = value
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
            Me.WorkOrderId = -1
            Me.ProgId = 0
            Me.AircraftId = 0
            Me.MSN = String.Empty
            Me.TailNbr = String.Empty
            Me.LocationId = 0
            Me.LocationName = String.Empty
            Me.WorkOrderStatusId = 0
            Me.WorkOrderStatus = String.Empty
            Me.FaultOccurredDate = String.Empty
            Me.FaultOccurredMeter = "0.0"
            Me.FISComplaintNbr = String.Empty
            Me.SquawkNbr = String.Empty
            Me.RequestDate = String.Empty
            Me.DueDate = String.Empty
            Me.DueTime = String.Empty
            Me.FinishDate = String.Empty
            Me.FinishTime = String.Empty
            Me.AircraftIssueAlert = 0
            Me.MaintIssueAlert = 0
            Me.WorkOrderTypeId = 0
            Me.WorkOrderType = String.Empty
            Me.WorkOrderNbr = String.Empty
            Me.MeterStartWork = "0.0"
            Me.MeterEndWork = "0.0"
            Me.EngineerId = 0
            Me.EngineerName = String.Empty
            Me.MechanicId = 0
            Me.MechanicName = String.Empty
            Me.EngineeringActionId = 0
            Me.AircraftDispostion = String.Empty
            Me.WorkOrderGuid = Guid.Empty
            Me.IsDeleted = False
            Me.UtcDateCreated = Nothing
            Me.UtcDateModified = Nothing
            Me.StampUserName = String.Empty
            Me.ConcurrencyId = "0"

            Me.WOReasonList = New WOReasonList
            Me.WOReasonDeleteList = New WOReasonList
            Me.NewWoReasonId = -1

            Me.WOInstructionList = New WOInstructionList
            Me.WOInstructionDeleteList = New WOInstructionList
            Me.NewWoInstructionId = -1

            Me.WOPerformedWorkList = New WOPerformedWorkList
            Me.WOPerformedWorkDeleteList = New WOPerformedWorkList
            Me.NewWoPerformedWorkId = -1

            Me.WOPartList = New WorkOrderPartList
            Me.WOPartDeleteList = New WorkOrderPartList
            Me.NewWoPartId = -1

            Me.WOEngineerCommentList = New WOEngineerCommentList
            Me.WOEngineerCommentDeleteList = New WOEngineerCommentList
            Me.NewWoEngineerCommentId = -1

            Me.WOMechanicCommentList = New WOMechanicCommentList
            Me.WOMechanicCommentDeleteList = New WOMechanicCommentList
            Me.NewWoMechanicCommentId = -1

            Me.WODocumentInfosList = New DocumentInfoList
            Me.WODocumentInfosDeleteList = New DocumentInfoList
            Me.NewWoDocumentId = -1

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
                _brokenRulesList.Add(New BrokenRule("ProgId", "ProgId is required", "03-00-001", False, False))
            Else
                If Len(_progId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("ProgId", "ProgId is required", "03-00-001", False, False))
                End If
            End If
            If (_aircraftId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("AircraftId", "AircraftId is required", "03-00-002", False, False))
            Else
                If Len(_aircraftId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("AircraftId", "AircraftId is required", "03-00-002", False, False))
                End If
            End If
            If (_locationId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("LocationId", "LocationId is required", "03-00-003", False, False))
            Else
                If Len(_locationId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("LocationId", "LocationId is required", "03-00-003", False, False))
                End If
            End If
            If (_workOrderTypeId = 0) Then
                _brokenRulesList.Add(New BrokenRule("WorkOrderTypeId", "WorkOrderTypeId is required", "03-00-004", False, False))
            Else
                If Len(_workOrderTypeId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("WorkOrderTypeId", "WorkOrderTypeId is required", "03-00-004", False, False))
                End If
            End If
            If (_workOrderStatusId = 0) Then
                _brokenRulesList.Add(New BrokenRule("WorkOrderStatusId", "WorkOrderStatusId is required", "03-00-005", False, False))
            Else
                If Len(_workOrderStatusId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("WorkOrderStatusId", "WorkOrderStatusId is required", "03-00-005", False, False))
                End If
            End If
            If (_fISComplaintNbr.Length > 8) Then
                _brokenRulesList.Add(New BrokenRule("FISComplaintNbr", "FISComplaintNbr cannot be longer than 8", "03-00-006", False, False))
            End If
            If (_squawkNbr.Length > 20) Then
                _brokenRulesList.Add(New BrokenRule("SquawkNbr", "SquawkNbr cannot be longer than 20", "03-00-007", False, False))
            End If
            If (_requestDate = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("RequestDate", "RequestDate is required", "03-00-008", False, False))
            Else
                If Not IsDate(_requestDate) Then
                    _brokenRulesList.Add(New BrokenRule("RequestDate", "RequestDate must be a valid date", "03-00-009", False, False))
                End If
            End If
            If Not (_dueDate = Nothing) Then
                If Not IsDate(_dueDate) Then
                    _brokenRulesList.Add(New BrokenRule("DueDate", "DueDate must be a valid date", "03-00-00A", False, False))
                End If
            End If
            If Not (_dueTime = Nothing) Then
                If Len(_dueTime) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("DueTime", "DueTime is required", "03-00-00B", False, False))
                Else
                    If Not Regex.IsMatch(_dueTime, TimeRegex) Then
                        _brokenRulesList.Add(New BrokenRule("DueTime", "DueTime time format is invalid", "03-00-00C", False, False))
                    End If
                End If
            End If
            If (_dueDate = Nothing And _dueTime <> Nothing) Or (_dueDate <> Nothing And _dueTime = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("DueDate", "Please provide both Due Date and Due Time", "03-00-00D", False, False))
            End If
            If Not (_finishDate = Nothing) Then
                If Not IsDate(_finishDate) Then
                    _brokenRulesList.Add(New BrokenRule("FinishDate", "FinishDate must be a valid date", "03-00-00E", False, False))
                End If
            End If
            If Not (_finishtime = Nothing) Then
                If Len(_finishtime) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("FinishTime", "FinishTime is required", "03-00-00F", False, False))
                Else
                    If Not Regex.IsMatch(_finishtime, TimeRegex) Then
                        _brokenRulesList.Add(New BrokenRule("FinishTime", "FinishTime time format is invalid", "03-00-010", False, False))
                    End If
                End If
            End If
            If (_engineerId = 0) Then
                _brokenRulesList.Add(New BrokenRule("EngineerId", "EngineerId is required", "03-00-011", False, False))
            End If
            If (_engineeringActionId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("EngineeringActionId", "EngineeringActionId is required", "03-00-012", False, False))
            Else
                If Len(_engineeringActionId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("EngineeringActionId", "EngineeringActionId is required", "03-00-012", False, False))
                End If
            End If
            If (_aircraftDispostion.Length > 50) Then
                _brokenRulesList.Add(New BrokenRule("AircraftDispostion", "AircraftDispostion cannot be longer than 50", "03-00-013", False, False))
            End If

            For Each thisWoReason As WOReason In Me.WOReasonList
                If Not (thisWoReason.Validate(_warningsList)) Then
                    For Each thisBrokenRule As BrokenRule In thisWoReason.BrokenRules
                        Me.BrokenRules.Add(thisBrokenRule)
                    Next
                End If
            Next
            For Each thisWoInstruction As WOInstruction In Me.WOInstructionList
                If Not (thisWoInstruction.Validate(_warningsList)) Then
                    For Each thisBrokenRule As BrokenRule In thisWoInstruction.BrokenRules
                        Me.BrokenRules.Add(thisBrokenRule)
                    Next
                End If
            Next
            For Each thisWoPerformedWork As WOPerformedWork In Me.WOPerformedWorkList
                If Not (thisWoPerformedWork.Validate(_warningsList)) Then
                    For Each thisBrokenRule As BrokenRule In thisWoPerformedWork.BrokenRules
                        Me.BrokenRules.Add(thisBrokenRule)
                    Next
                End If
            Next
            For Each thisWoPartList As WorkOrderPart In Me.WOPartList
                If Not (thisWoPartList.Validate(_warningsList)) Then
                    For Each thisBrokenRule As BrokenRule In thisWoPartList.BrokenRules
                        Me.BrokenRules.Add(thisBrokenRule)
                    Next
                End If
            Next
            For Each thisWoEngineerComment As WOEngineerComment In Me.WOEngineerCommentList
                If Not (thisWoEngineerComment.Validate(_warningsList)) Then
                    For Each thisBrokenRule As BrokenRule In thisWoEngineerComment.BrokenRules
                        Me.BrokenRules.Add(thisBrokenRule)
                    Next
                End If
            Next
            For Each thisWoMechanicComment As WOMechanicComment In Me.WOMechanicCommentList
                If Not (thisWoMechanicComment.Validate(_warningsList)) Then
                    For Each thisBrokenRule As BrokenRule In thisWoMechanicComment.BrokenRules
                        Me.BrokenRules.Add(thisBrokenRule)
                    Next
                End If
            Next

            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

        End Function

    End Class

End Namespace
