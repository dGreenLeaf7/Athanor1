' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

<Serializable()> _
Public Class AircraftStatus
    Private _aircraftId As Integer
    Private _aircraftStatusId As Integer
    Private _LocationName As String
    Private _MSN As String
    Private _Status As Integer
    Private _HangarStatus As Integer
    Private _EngineerComment As String
    Private _HangarComment As String
    Private _UTCDateModified As Date

    Public Property AircraftId() As Long
        Get
            Return _aircraftId
        End Get
        Set(ByVal value As Long)
            _aircraftId = value
        End Set
    End Property

    Public Property AircraftStatusId() As Long
        Get
            Return _aircraftStatusId
        End Get
        Set(ByVal value As Long)
            _aircraftStatusId = value
        End Set
    End Property

    Public Property LocationName() As String
        Get
            Return _LocationName
        End Get
        Set(ByVal value As String)
            _LocationName = value
        End Set
    End Property

    Public Property MSN() As String
        Get
            Return _MSN
        End Get
        Set(ByVal value As String)
            _MSN = value
        End Set
    End Property

    Public Property Status() As Integer
        Get
            Return _Status
        End Get
        Set(ByVal value As Integer)
            _Status = value
        End Set
    End Property

    Public ReadOnly Property StatusAsString() As String
        Get
            If Status = 1 Then Return "UP"
            If Status = 2 Then Return "DOWN"
            Return "OFF TEST"
        End Get
    End Property


    Public Property EngineerComment() As String
        Get
            Return _EngineerComment
        End Get
        Set(ByVal value As String)
            _EngineerComment = value
        End Set
    End Property

    Public Property HangarComment() As String
        Get
            Return _HangarComment
        End Get
        Set(ByVal value As String)
            _HangarComment = value
        End Set
    End Property

    Public Property UTCDateModified() As Date
        Get
            Return _UTCDateModified
        End Get
        Set(ByVal value As Date)
            If value = Nothing Then
                _UTCDateModified = Nothing
            Else
                _UTCDateModified = value
            End If
        End Set
    End Property

    Public ReadOnly Property DateModifiedInPT() As String
        Get
            Try
                Dim Offset As Integer = -8
                Dim localZone As TimeZone = TimeZone.CurrentTimeZone
                If localZone.IsDaylightSavingTime(Date.Now) Then Offset = -9

                If _UTCDateModified = Date.MinValue Then Return ""

                Dim DisplayDate As Date = _UTCDateModified.AddHours(Offset)
                Return DisplayDate.ToShortDateString() + " " + DisplayDate.ToShortTimeString()
            Catch myException As Exception
                Return ""
            End Try
        End Get
    End Property
End Class
