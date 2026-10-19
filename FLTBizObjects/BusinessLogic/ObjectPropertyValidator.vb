' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports FLTBizObjects.FLT.BO
Imports System.Configuration
Imports System.Text.RegularExpressions

Friend Class ObjectPropertyValidator

    Public Shared Function UserNameStringIsInvalidMessage() As String
        Dim messageString As String = " should start with a letter and only contain letters, numbers, dashes, underscores, and periods.  Spaces, periods, dashes, and underscores can't appear twice in a row."
        If Not ConfigurationManager.AppSettings("UserNameRegExMessage") = Nothing Then
            messageString = ConfigurationManager.AppSettings("UserNameRegExMessage").ToString()
        End If
        Return messageString
    End Function

    Public Shared Function FileTypeStringIsInvalidMessage() As String
        Dim messageString As String = " should start with a period and only contain letters and numbers."
        If Not ConfigurationManager.AppSettings("FileTypeRegExMessage") = Nothing Then
            messageString = ConfigurationManager.AppSettings("FileTypeRegExMessage").ToString()
        End If
        Return messageString
    End Function

    Public Shared Function MimeTypeStringIsInvalidMessage() As String
        Dim messageString As String = " should only contain letters, numbers, spaces, dashes, underscores, and slashes."
        If Not ConfigurationManager.AppSettings("MimeTypeRegExMessage") = Nothing Then
            messageString = ConfigurationManager.AppSettings("MimeTypeRegExMessage").ToString()
        End If
        Return messageString
    End Function

    Public Shared Function DocumentNameStringIsInvalidMessage() As String
        Dim messageString As String = " cannot contain any of the following characters: \ / : * ? "" < > |"
        If Not ConfigurationManager.AppSettings("DocumentNameRegExMessage") = Nothing Then
            messageString = ConfigurationManager.AppSettings("DocumentNameRegExMessage").ToString()
        End If
        Return messageString
    End Function

    Public Shared Function CommonStringIsInvalidMessage() As String
        Dim messageString As String = " should only contain letters, numbers, spaces, periods, dashes, underscores, and slashes."
        If Not ConfigurationManager.AppSettings("CommonRegExMessage") = Nothing Then
            messageString = ConfigurationManager.AppSettings("CommonRegExMessage").ToString()
        End If
        Return messageString
    End Function

    Public Shared Function ValidateUserNameString(userString As String) As Boolean

        Dim regExString As String = "^[A-Za-z]{1}([A-Za-z0-9 ._-](?:(?![ ]{2}|[.]{2}|[_]{2}|[-]{2})))*$"

        If Not ConfigurationManager.AppSettings("UserNameRegEx") = Nothing Then
            regExString = ConfigurationManager.AppSettings("UserNameRegEx").ToString()
        End If

        Dim regEx As New Regex(regExString)

        Return regEx.IsMatch(userString)

    End Function

    Public Shared Function ValidateFileTypeString(fileTypeString As String) As Boolean

        Dim regExString As String = "^\.([A-Za-z0-9])*$"

        If Not ConfigurationManager.AppSettings("FileTypeRegEx") = Nothing Then
            regExString = ConfigurationManager.AppSettings("FileTypeRegEx").ToString()
        End If

        Dim regEx As New Regex(regExString)

        Return regEx.IsMatch(fileTypeString)

    End Function

    Public Shared Function ValidateMimeTypeString(mimeTypeString As String) As Boolean

        Dim regExString As String = "^([A-Za-z0-9 .\/_-])*$"

        If Not ConfigurationManager.AppSettings("MimeTypeString") = Nothing Then
            regExString = ConfigurationManager.AppSettings("MimeTypeString").ToString()
        End If

        Dim regEx As New Regex(regExString)

        Return regEx.IsMatch(mimeTypeString)

    End Function

    Public Shared Function ValidateDocumentNameString(documentNameString As String) As Boolean
        If (documentNameString.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) <> -1) Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Shared Function ValidateCommonString(commonString As String) As Boolean

        Dim regExString As String = "^([A-Za-z0-9 .#\/()_-])*$"

        If Not ConfigurationManager.AppSettings("CommonRegEx") = Nothing Then
            regExString = ConfigurationManager.AppSettings("CommonRegEx").ToString()
        End If

        Dim regEx As New Regex(regExString)

        Return regEx.IsMatch(commonString)

    End Function

End Class
