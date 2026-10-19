' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.

Public Class PilotCommentObject

    Private _progName As String
    Private _locationName As String
    Private _locationId As Integer
    Private _msn As String
    Private _aircraftId As Integer
    Private _flightDateSort As String
    Private _flightDate As Date
    Private _flightId As Integer
    Private _airframeHoursEnd As Decimal
    Private _pilotName As String
    Private _componentName As String
    Private _commentRating As String
    Private _issueCode As String
    Private _issueCodeId As Integer
    Private _flightCommentId As Integer
    Private _pilotComment As String

    Public Property ProgName() As String
        Get
            Return _progName
        End Get
        Set(ByVal value As String)
            _progName = value
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

    Public Property LocationId() As Integer
        Get
            Return _locationId
        End Get
        Set(ByVal value As Integer)
            _locationId = value
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

    Public Property AircraftId() As Integer
        Get
            Return _aircraftId
        End Get
        Set(ByVal value As Integer)
            _aircraftId = value
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

    Public Property FlightDate() As Date
        Get
            Return _flightDate
        End Get
        Set(ByVal value As Date)
            _flightDate = value
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

    Public Property AirframeHoursEnd() As Decimal
        Get
            Return _airframeHoursEnd
        End Get
        Set(ByVal value As Decimal)
            _airframeHoursEnd = value
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

    Public Property ComponentName() As String
        Get
            Return _componentName
        End Get
        Set(ByVal value As String)
            _componentName = value
        End Set
    End Property

    Public Property CommentRating() As String
        Get
            Return _commentRating
        End Get
        Set(ByVal value As String)
            _commentRating = value
        End Set
    End Property

    Public Property IssueCode() As String
        Get
            Return _issueCode
        End Get
        Set(ByVal value As String)
            _issueCode = value
        End Set
    End Property

    Public Property IssueCodeId() As Integer
        Get
            Return _issueCodeId
        End Get
        Set(ByVal value As Integer)
            _issueCodeId = value
        End Set
    End Property

    Public Property FlightCommentId() As Integer
        Get
            Return _flightCommentId
        End Get
        Set(ByVal value As Integer)
            _flightCommentId = value
        End Set
    End Property

    Public Property PilotComment() As String
        Get
            Return _pilotComment
        End Get
        Set(ByVal value As String)
            _pilotComment = value
        End Set
    End Property

    Public Sub New()

        _progName = String.Empty
        _locationName = String.Empty
        _locationId = 0
        _msn = String.Empty
        _aircraftId = 0
        _flightDateSort = String.Empty
        _flightDate = Nothing
        _flightId = 0
        _airframeHoursEnd = 0
        _pilotName = String.Empty
        _componentName = String.Empty
        _commentRating = String.Empty
        _issueCode = String.Empty
        _issueCodeId = 0
        _flightCommentId = 0
        _pilotComment = String.Empty

    End Sub

End Class
