' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Public Class AircraftCommentsObject

    Private _progName As String
    Private _locationName As String
    Private _locationId As Integer
    Private _msn As String
    Private _aircraftId As Integer
    Private _commentDate As Date
    Private _sourceId As Integer
    Private _source As String
    Private _commentAuthor As String
    Private _commentRating As Integer
    Private _component As String
    Private _componentId As Integer
    Private _issueCode As String
    Private _issueCodeId As Integer
    Private _comment As String

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

    Public Property CommentDate() As Date
        Get
            Return _commentDate
        End Get
        Set(ByVal value As Date)
            _commentDate = value
        End Set
    End Property

    Public Property SourceId() As Integer
        Get
            Return _sourceId
        End Get
        Set(ByVal value As Integer)
            _sourceId = value
        End Set
    End Property

    Public Property Source() As String
        Get
            Return _source
        End Get
        Set(ByVal value As String)
            _source = value
        End Set
    End Property

    Public Property CommentAuthor() As String
        Get
            Return _commentAuthor
        End Get
        Set(ByVal value As String)
            _commentAuthor = value
        End Set
    End Property

    Public Property CommentRating() As Integer
        Get
            Return _commentRating
        End Get
        Set(ByVal value As Integer)
            _commentRating = value
        End Set
    End Property

    Public Property Component() As String
        Get
            Return _component
        End Get
        Set(ByVal value As String)
            _component = value
        End Set
    End Property

    Public Property ComponentId() As Integer
        Get
            Return _componentId
        End Get
        Set(ByVal value As Integer)
            _componentId = value
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

    Public Property Comment() As String
        Get
            Return _comment
        End Get
        Set(ByVal value As String)
            _comment = value
        End Set
    End Property

    Public Sub New()

        _progName = String.Empty
        _locationName = String.Empty
        _locationId = 0
        _msn = String.Empty
        _aircraftId = 0
        _commentDate = Nothing
        _sourceId = 0
        _source = String.Empty
        _commentAuthor = String.Empty
        _commentRating = 0
        _component = String.Empty
        _componentId = 0
        _issueCode = String.Empty
        _issueCodeId = 0
        _comment = String.Empty

    End Sub

End Class
