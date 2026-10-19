' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

    <Serializable()> _
    Public Class LiquidFill

        Private Const TimeRegex As String = "^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]?(\s)?(AM|am|Am|aM|PM|pm|Pm|pM))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]))$"
        Private _meterMask As String = "#,##0.0;-#,##0.0"
        Private _dateMask As String = "MM-dd-yyyy"
        Private _sortDateMask As String = "yyyy-MM-dd"

        Private _liquidFillId As Integer
        Private _flightId As Integer
        Private _liquidGroupId As Integer
        Private _liquidGroupName As String
        Private _liquidTypeId As Integer
        Private _liquidTypeName As String
        Private _liquidUnitId As Integer
        Private _liquidUnitName As String
        Private _liquidAmt As Decimal
        Private _hobbsReading As Decimal
        Private _airframeHoursReading As Decimal
        Private _liquidFillLocation As String
        Private _liquidFillApprover As String
        Private _liquidFillGuid As Guid
        Private _isActive As Boolean
        Private _isDeleted As Boolean
        Private _utcDateCreated As DateTime
        Private _utcDateModified As DateTime
        Private _StampUserName As String
        Private _concurrencyId As String

        Private _brokenRulesList As BrokenRulesList = New BrokenRulesList
        Private _errorsList As BrokenRulesList = New BrokenRulesList
        Private _warningsList As BrokenRulesList = New BrokenRulesList

        <DataObjectFieldAttribute(True, True, False)> _
        Public Property LiquidFillId() As Integer
            Get
                Return _liquidFillId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _liquidFillId = 0
                Else
                    _liquidFillId = value
                End If
            End Set
        End Property

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

        Public Property LiquidGroupId() As Integer
            Get
                Return _liquidGroupId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _liquidGroupId = 0
                Else
                    _liquidGroupId = value
                End If
            End Set
        End Property

        Public Property LiquidGroupName() As String
            Get
                Return _liquidGroupName
            End Get
            Set(ByVal value As String)
                _liquidGroupName = value
            End Set
        End Property

        Public Property LiquidTypeId() As Integer
            Get
                Return _liquidTypeId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _liquidTypeId = 0
                Else
                    _liquidTypeId = value
                End If
            End Set
        End Property

        Public Property LiquidTypeName() As String
            Get
                Return _liquidTypeName
            End Get
            Set(ByVal value As String)
                _liquidTypeName = value
            End Set
        End Property

        Public Property LiquidUnitId() As Integer
            Get
                Return _liquidUnitId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _liquidUnitId = 0
                Else
                    _liquidUnitId = value
                End If
            End Set
        End Property

        Public Property LiquidUnitName() As String
            Get
                Return _liquidUnitName
            End Get
            Set(ByVal value As String)
                _liquidUnitName = value
            End Set
        End Property

        Public Property LiquidAmt() As String
            Get
                Return Format(_liquidAmt, "#,##0.000")
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _liquidAmt = 0
                ElseIf IsNumeric(value) Then
                    _liquidAmt = value
                Else
                    _liquidAmt = 0
                End If
            End Set
        End Property

        Public Property HobbsReading() As String
            Get
                Return Format(_hobbsReading, _meterMask)
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _hobbsReading = 0
                ElseIf IsNumeric(HobbsReading) Then
                    _hobbsReading = CDec(value)
                Else
                    _hobbsReading = 0
                End If
            End Set
        End Property

        Public Property AirframeHoursReading() As String
            Get
                Return Format(_airframeHoursReading, _meterMask)
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _airframeHoursReading = 0
                ElseIf IsNumeric(HobbsReading) Then
                    _airframeHoursReading = CDec(value)
                Else
                    _airframeHoursReading = 0
                End If
            End Set
        End Property

        Public Property LiquidFillLocation() As String
            Get
                Return _liquidFillLocation
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _liquidFillLocation = String.Empty
                Else
                    _liquidFillLocation = value
                End If
            End Set
        End Property

        Public Property LiquidFillApprover() As String
            Get
                Return _liquidFillApprover
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _liquidFillApprover = String.Empty
                Else
                    _liquidFillApprover = value
                End If
            End Set
        End Property

        Public Property LiquidFillGUID() As Guid
            Get
                Return _liquidFillGuid
            End Get
            Set(ByVal value As Guid)
                If value = Nothing Then
                    _liquidFillGuid = Guid.Empty
                Else
                    _liquidFillGuid = value
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
            Me.LiquidFillId = 0
            Me.FlightId = 0
            Me.LiquidGroupId = 0
            Me.LiquidGroupName = String.Empty
            Me.LiquidTypeId = 0
            Me.LiquidTypeName = String.Empty
            Me.LiquidUnitId = 0
            Me.LiquidUnitName = String.Empty
            Me.LiquidAmt = 0
            Me.HobbsReading = "0.0"
            Me.AirframeHoursReading = "0.0"
            Me.LiquidFillLocation = String.Empty
            Me.LiquidFillApprover = String.Empty
            Me.LiquidFillGUID = Guid.Empty
            Me.IsActive = True
            Me.IsDeleted = False
            Me.UtcDateCreated = Nothing
            Me.UtcDateModified = Nothing
            Me.StampUserName = String.Empty
            Me.ConcurrencyId = "0"
            _brokenRulesList = New BrokenRulesList
            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList
        End Sub

        Public Function Validate() As Boolean
            Return Validate(_warningsList)
        End Function

        Public Function Validate(ByVal WarningsList As BrokenRulesList) As Boolean

            Dim prevWarningsList As New BrokenRulesList
            For Each thisBrokenRule As BrokenRule In WarningsList
                prevWarningsList.Add(thisBrokenRule)
            Next
            _brokenRulesList = New BrokenRulesList

            If (_flightId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("FlightId", "FlightId is required", "02-01-001", False, False))
            Else
                If Len(_flightId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("FlightId", "FlightId is required", "02-01-001", False, False))
                End If
            End If
            If (_liquidGroupId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("LiquidGroupId", "LiquidGroupId is required", "02-01-002", False, False))
            Else
                If Len(_liquidGroupId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("LiquidGroupId", "LiquidGroupId is required", "02-01-002", False, False))
                End If
            End If
            If (_liquidTypeId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("LiquidTypeId", "LiquidTypeId is required", "02-01-003", False, False))
            Else
                If Len(_liquidTypeId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("LiquidTypeId", "LiquidTypeId is required", "02-01-003", False, False))
                End If
            End If
            If (_liquidUnitId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("LiquidUnitId", "LiquidUnitId is required", "02-01-004", False, False))
            Else
                If Len(_liquidUnitId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("LiquidUnitId", "LiquidUnitId is required", "02-01-004", False, False))
                End If
            End If
            If (_liquidAmt = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("LiquidAmt", "LiquidAmt is required", "02-01-005", False, False))
            Else
                If Len(_liquidAmt) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("LiquidAmt", "LiquidAmt is required", "02-01-005", False, False))
                ElseIf _liquidAmt < 0 Then
                    _brokenRulesList.Add(New BrokenRule("LiquidAmt", "LiquidAmt cannot be less than Zero", "02-01-006", False, False))
                ElseIf _liquidAmt > 999999.999 Then
                    _brokenRulesList.Add(New BrokenRule("LiquidAmt", "LiquidAmt cannot be greater than 999999.999", "02-01-007", False, False))
                End If
            End If
            If (_hobbsReading = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("MeterReading", "MeterReading is required", "02-01-008", False, False))
            Else
                If Len(_hobbsReading) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("MeterReading", "MeterReading is required", "02-01-008", False, False))
                End If
            End If
            If (_liquidFillLocation = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("LiquidFillLocation", "LiquidFillLocation is required", "02-01-009", False, False))
            Else
                If Len(_liquidFillLocation) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("LiquidFillLocation", "LiquidFillLocation is required", "02-01-009", False, False))
                End If
            End If
            If (_liquidFillLocation.Length > 255) Then
                _brokenRulesList.Add(New BrokenRule("LiquidFillLocation", "LiquidFillLocation cannot be longer than 255", "02-01-00A", False, False))
            End If
            If (_liquidFillApprover = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("LiquidFillApprover", "LiquidFillApprover is required", "02-01-00B", False, False))
            Else
                If Len(_liquidFillApprover) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("LiquidFillApprover", "LiquidFillApprover is required", "02-01-00B", False, False))
                End If
            End If
            If (_liquidFillApprover.Length > 30) Then
                _brokenRulesList.Add(New BrokenRule("LiquidFillApprover", "LiquidFillApprover cannot be longer than 30", "02-01-00C", False, False))
            End If

            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

        End Function

    End Class

End Namespace
