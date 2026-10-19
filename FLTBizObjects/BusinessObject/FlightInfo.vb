' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

    <Serializable()> _
    Public Class FlightInfo

        Private _flightId As Integer = 0
        Private _locationId As Integer = 0
        Private _locationName As String = String.Empty
        Private _flightDate As String = String.Empty
        Private _workShift As String = String.Empty
        Private _msn As String = String.Empty
        Private _serialNbr As String = String.Empty
        Private _registrationNbr As String = String.Empty
        Private _flightDateSort As String = String.Empty
        Private _departureTime As String = String.Empty
        Private _returnTime As String = String.Empty
        Private _airframeHoursStart As String = String.Empty
        Private _airframeHoursEnd As String = String.Empty
        Private _flightUsage As String = String.Empty
        Private _isApproved As Boolean = True
        Private _isActive As Boolean = True
        Private _isDeleted As Boolean = False
        Private _utcDateCreated As Date
        Private _utcDateModified As Date


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

        Public Property FlightDate() As String
            Get
                Return _flightDate
            End Get
            Set(ByVal value As String)
                _flightDate = value
            End Set
        End Property

        Public Property WorkShift() As String
            Get
                Return _workShift
            End Get
            Set(ByVal value As String)
                _workShift = value
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
                _registrationNbr = value
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

        Public Property DepartureTime() As String
            Get
                Return _departureTime
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _departureTime = String.Empty
                Else
                    _departureTime = value
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
                    _returnTime = value
                End If
            End Set
        End Property

        Public Property AirframeHoursStart() As String
            Get
                Return _airframeHoursStart
            End Get
            Set(ByVal value As String)
                If value = String.Empty Then
                    _airframeHoursStart = "0.0"
                ElseIf IsNumeric(value) Then
                    _airframeHoursStart = Format(CDec(value), "#,##0.0")
                Else
                    _airframeHoursStart = "0.0"
                End If
            End Set
        End Property

        Public Property AirframeHoursEnd() As String
            Get
                Return _airframeHoursEnd
            End Get
            Set(ByVal value As String)
                If value = String.Empty Then
                    _airframeHoursEnd = "0.0"
                ElseIf IsNumeric(value) Then
                    _airframeHoursEnd = Format(CDec(value), "#,##0.0")
                Else
                    _airframeHoursEnd = "0.0"
                End If
            End Set
        End Property

        Public Property FlightUsage() As String
            Get
                Return _flightUsage
            End Get
            Set(ByVal value As String)
                If value = String.Empty Then
                    _flightUsage = "0.0"
                ElseIf IsNumeric(value) Then
                    _flightUsage = Format(CDec(value), "#,##0.0")
                Else
                    _flightUsage = "0.0"
                End If
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
            Me.FlightId = 0
            Me.LocationId = 0
            Me.LocationName = String.Empty
            Me.FlightDate = String.Empty
            Me.WorkShift = String.Empty
            Me.MSN = String.Empty
            Me.SerialNbr = String.Empty
            Me.RegistrationNbr = String.Empty
            Me.FlightDateSort = String.Empty
            Me.DepartureTime = String.Empty
            Me.ReturnTime = String.Empty
            Me.AirframeHoursStart = "0.0"
            Me.AirframeHoursEnd = "0.0"
            Me.FlightUsage = "0.0"
            Me.IsApproved = True
            Me.IsActive = True
            Me.IsDeleted = False
            Me.UtcDateCreated = Nothing
            Me.UtcDateModified = Nothing
        End Sub

    End Class

End Namespace
