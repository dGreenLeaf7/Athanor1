' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

    <Serializable()> _
    Public Class DocumentType

        Private _documentTypeId As Integer
        Private _fileExtension As String
        Private _mimeType As String
        Private _concurrencyId As String
        Private _brokenRulesList As BrokenRulesList

        Public Property DocumentTypeId() As Integer
            Get
                Return _documentTypeId
            End Get
            Set(ByVal value As Integer)
                _documentTypeId = value
            End Set
        End Property

        Public Property FileExtension() As String
            Get
                Return _fileExtension
            End Get
            Set(ByVal value As String)
                _fileExtension = value
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

        Public Sub New()
            Me.DocumentTypeId = 0
            Me.FileExtension = String.Empty
            Me.MimeType = String.Empty
            Me.ConcurrencyId = "0"
        End Sub

        Public Function Validate() As Boolean
            _brokenRulesList = New BrokenRulesList()
            If _documentTypeId = 0 Then
                _brokenRulesList.Add(New BrokenRule("DocumentTypeId", "DocumentTypeId is required"))
            End If
            If (_fileExtension = Nothing) Or (_fileExtension.Length = 0) Then
                _brokenRulesList.Add(New BrokenRule("FileExtension", "FileExtension is required"))
            End If
            If ObjectPropertyValidator.ValidateFileTypeString(_fileExtension) = False Then
                _brokenRulesList.Add(New BrokenRule("FileExtension", "FileExtension" & ObjectPropertyValidator.FileTypeStringIsInvalidMessage))
            End If
            If (_mimeType = Nothing) Or (_mimeType.Length = 0) Then
                _brokenRulesList.Add(New BrokenRule("MimeType", "MimeType is required"))
            End If
            If ObjectPropertyValidator.ValidateMimeTypeString(_mimeType) = False Then
                _brokenRulesList.Add(New BrokenRule("MimeType", "MimeType" & ObjectPropertyValidator.MimeTypeStringIsInvalidMessage))
            End If

            Return (_brokenRulesList.Count = 0)

        End Function

    End Class

End Namespace
