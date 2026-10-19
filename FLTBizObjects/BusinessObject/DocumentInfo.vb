' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

    <Serializable()> _
    Public Class DocumentInfo

        Private _documentId As Long
        Private _progId As Integer
        Private _documentTypeId As Integer
        Private _fileExtension As String
        Private _mimeType As String
        Private _documentName As String
        Private _documentDescription As String
        Private _documentSize As Integer
        Private _isDeleted As Boolean
        Private _utcDateCreated As DateTime
        Private _utcDateModified As DateTime
        Private _StampUserName As String
        Private _concurrencyId As String

        Private _brokenRulesList As BrokenRulesList = New BrokenRulesList()
        Private _errorsList As BrokenRulesList = New BrokenRulesList()
        Private _warningsList As BrokenRulesList = New BrokenRulesList()


        <DataObjectFieldAttribute(True, True, False)> _
        Public Property DocumentId() As Long
            Get
                Return _documentId
            End Get
            Set(ByVal value As Long)
                If (value = Nothing) Then
                    _documentId = 0
                Else
                    _documentId = value
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

        Public Property DocumentTypeId() As Integer
            Get
                Return _documentTypeId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _documentTypeId = 0
                Else
                    _documentTypeId = value
                End If
            End Set
        End Property

        Public Property FileExtension() As String
            Get
                Return _fileextension
            End Get
            Set(ByVal value As String)
                _fileextension = value
            End Set
        End Property

        Public Property MimeType() As String
            Get
                Return _mimeType
            End Get
            Set(ByVal value As String)
                _mimeType = value
            End Set
        End Property

        Public Property DocumentName() As String
            Get
                Return _documentName
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _documentName = String.Empty
                Else
                    _documentName = value
                End If
            End Set
        End Property

        Public Property DocumentDescription() As String
            Get
                Return _documentDescription
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _documentDescription = String.Empty
                Else
                    _documentDescription = value
                End If
            End Set
        End Property

        Public Property DocumentSize() As Integer
            Get
                Return _documentSize
            End Get
            Set(ByVal value As Integer)
                _documentSize = value
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
            Me.DocumentId = 0
            Me.ProgId = 0
            Me.DocumentTypeId = 0
            Me.FileExtension = String.Empty
            Me.MimeType = String.Empty
            Me.DocumentName = String.Empty
            Me.DocumentDescription = String.Empty
            Me.DocumentSize = 0
            Me.IsDeleted = False
            Me.UtcDateCreated = Nothing
            Me.UtcDateModified = Nothing
            Me.StampUserName = String.Empty
            Me.ConcurrencyId = "0"
            _brokenRulesList = New BrokenRulesList
        End Sub

        Public Function Validate() As Boolean

            Dim prevWarningsList As New BrokenRulesList
            For Each thisBrokenRule As BrokenRule In WarningsList
                prevWarningsList.Add(thisBrokenRule)
            Next

            _brokenRulesList = New BrokenRulesList()
            If (_progId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("ProgId", "ProgId is required"))
            Else
                If Len(_progId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("ProgId", "ProgId is required"))
                End If
            End If
            If (_documentTypeId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("DocTypeId", "DocTypeId is required"))
            Else
                If Len(_documentTypeId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("DocTypeId", "DocTypeId is required"))
                End If
            End If
            If (_documentName = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("DocFileName", "DocFileName is required"))
            Else
                If Len(_documentName) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("DocFileName", "DocFileName is required"))
                End If
            End If
            If ObjectPropertyValidator.ValidateDocumentNameString(_documentName) = False Then
                _brokenRulesList.Add(New BrokenRule("DocFileName", "DocFileName" & ObjectPropertyValidator.DocumentNameStringIsInvalidMessage))
            End If
            If (_documentName.Length > 255) Then
                _brokenRulesList.Add(New BrokenRule("DocFileName", "DocFileName cannot be longer than 255"))
            End If
            If (_documentName.LastIndexOf(".") = 0) Then
                _brokenRulesList.Add(New BrokenRule("DocuFileName", "DocFileName is missing, must be more than just a file extension"))
            End If

            If (_documentDescription <> Nothing) Then
                If (_documentDescription.Length > 255) Then
                    _brokenRulesList.Add(New BrokenRule("DocDescription", "DocDescription cannot be longer than 255"))
                End If
            End If
            If (_documentSize <= 0) Then
                _brokenRulesList.Add(New BrokenRule("DocumentSize", "DocumentSize is required"))
            End If


            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

            Return (_brokenRulesList.Count = 0)

        End Function

    End Class

End Namespace
