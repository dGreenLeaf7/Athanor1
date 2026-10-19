' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

    <Serializable()> _
    Public Class AircraftInfo

        Private _aircraftId As Integer
        Private _currentLocationName As String
        Private _modelName As String
        Private _mSN As String
        Private _shipNbr As String
        Private _serialNbr As String
        Private _registrationNbr As String
        Private _description As String
        Private _currentLocationId As Integer
        Private _liveryColor As String
        Private _airframeHoursStart As Decimal
        Private _airframeHoursCurrent As Decimal
        Private _airframeHoursOffset As Decimal
        Private _airframeHoursUnitId As Integer
        Private _totalRequiredUsage As Decimal
        Private _currentUsage As Decimal
        Private _estEndOfTesting As String
        Private _actEndOfTesting As String
        Private _aircraftStatus As String
        Private _aircraftEngineHoursCurrent As Decimal
        Private _isActive As Boolean
        Private _isDeleted As Boolean
        Private _utcDateCreated As Date
        Private _utcDateModified As Date

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

        Public Property CurrentLocationName() As String
            Get
                Return _currentLocationName
            End Get
            Set(ByVal value As String)
                _currentLocationName = value
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
                _serialNbr = value
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

        Public Property CurrentLocationId As Integer
            Get
                Return _currentLocationId
            End Get
            Set(value As Integer)
                _currentLocationId = value
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

        Public Property AirframeHoursStart() As String
            Get
                Return Format(_airframeHoursStart, "#,##0.0")
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

        Public Property AirframeHoursCurrent() As String
            Get
                Return Format(_airframeHoursCurrent, "#,##0.0")
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
                Return Format(_airframeHoursOffset, "#,##0.0")
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

        Public Property AirframeHoursUnitId() As Integer
            Get
                Return _airframeHoursUnitId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _airframeHoursUnitId = 0
                Else
                    _airframeHoursUnitId = value
                End If
            End Set
        End Property

        Public Property TotalRequiredUsage() As String
            Get
                Return Format(_totalRequiredUsage, "#,##0.0")
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

        Public Property CurrentUsage() As String
            Get
                Return Format(_currentUsage, "#,##0.0")
            End Get
            Friend Set(ByVal value As String)
                If value = Nothing Then
                    _currentUsage = 0
                ElseIf IsNumeric(value) Then
                    _currentUsage = CDec(value)
                Else
                    _currentUsage = 0
                End If
            End Set
        End Property

        Public Property EstEndOfTesting() As String
            Get
                Return _estEndOfTesting
            End Get
            Set(ByVal value As String)
                If IsDate(value) Then
                    _estEndOfTesting = Format(CDate(value), "MM-dd-yyyy")
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
                    _actEndOfTesting = Format(CDate(value), "MM-dd-yyyy")
                Else
                    _actEndOfTesting = value
                End If
            End Set
        End Property

        Public Property AircraftStatus() As String
            Get
                Return _aircraftStatus
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _aircraftStatus = String.Empty
                Else
                    _aircraftStatus = value
                End If
            End Set
        End Property

        Public Property AircraftEngineHoursCurrent As Decimal
            Get
                Return _aircraftEngineHoursCurrent
            End Get
            Set(value As Decimal)
                _aircraftEngineHoursCurrent = value
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

        Public Sub New()
            Me.AircraftId = 0
            Me.CurrentLocationName = String.Empty
            Me.ModelName = String.Empty
            Me.MSN = String.Empty
            Me.ShipNbr = String.Empty
            Me.SerialNbr = String.Empty
            Me.RegistrationNbr = String.Empty
            Me.Description = String.Empty
            Me.CurrentLocationId = 0
            Me.LiveryColor = String.Empty
            Me.AirframeHoursStart = 0
            Me.AirframeHoursCurrent = 0
            Me.AirframeHoursOffset = 0
            Me.AirframeHoursUnitId = 0
            Me.TotalRequiredUsage = 0
            Me.CurrentUsage = 0
            Me.EstEndOfTesting = String.Empty
            Me.ActEndOfTesting = String.Empty
            Me.AircraftStatus = String.Empty
            Me.AircraftEngineHoursCurrent = 0.0
            Me.IsActive = True
            Me.IsDeleted = False
            Me.UtcDateCreated = Nothing
            Me.UtcDateModified = Nothing
        End Sub

    End Class

End Namespace
