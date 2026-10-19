' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

    <Serializable()> _
    Public Class WorkOrderInfo

        Private _workOrderId As Integer
        Private _aircraftId As Integer
        Private _msn As String
        Private _shipNbr As String
        Private _serialNbr As String
        Private _registrationNbr As String
        Private _locationId As Integer
        Private _locationName As String
        Private _workOrderNbr As String
        Private _fISComplaintNbr As String
        Private _squawkNbr As String
        Private _requestDate As String
        Private _dueDate As String
        Private _finishDate As String
        Private _workOrderStatusId As Integer
        Private _workOrderStatus As String
        Private _isActive As Boolean
        Private _isDeleted As Boolean

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

        Public Property AircraftId() As Long
            Get
                Return _aircraftId
            End Get
            Set(ByVal value As Long)
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

        Public Property RegistrationNbr() As String
            Get
                Return _registrationNbr
            End Get
            Set(ByVal value As String)
                _registrationNbr = value
            End Set
        End Property

        Public Property LocationId() As Long
            Get
                Return _locationId
            End Get
            Set(ByVal value As Long)
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

        Public Property FISComplaintNbr() As String
            Get
                Return _fISComplaintNbr
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _fISComplaintNbr = String.Empty
                Else
                    _fISComplaintNbr = value
                End If
            End Set
        End Property

        Public Property SquawkNbr() As String
            Get
                Return _squawkNbr
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _squawkNbr = String.Empty
                Else
                    _squawkNbr = value
                End If
            End Set
        End Property

        Public Property RequestDate() As String
            Get
                Return _requestDate
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _requestDate = String.Empty
                Else
                    _requestDate = value
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
                Else
                    _dueDate = value
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
                Else
                    _finishDate = value
                End If
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

        Public Sub New()
            Me.WorkOrderId = 0
            Me.AircraftId = 0
            Me.MSN = String.Empty
            Me.ShipNbr = String.Empty
            Me.SerialNbr = String.Empty
            Me.RegistrationNbr = String.Empty
            Me.LocationId = 0
            Me.LocationName = String.Empty
            Me.WorkOrderNbr = String.Empty
            Me.FISComplaintNbr = String.Empty
            Me.SquawkNbr = String.Empty
            Me.RequestDate = String.Empty
            Me.DueDate = String.Empty
            Me.FinishDate = String.Empty
            Me.WorkOrderStatusId = 0
            Me.WorkOrderStatus = String.Empty
            Me.IsActive = True
            Me.IsDeleted = False
        End Sub

    End Class

End Namespace
