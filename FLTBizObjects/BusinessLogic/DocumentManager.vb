' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

    <DataObjectAttribute()> _
    Public Class DocumentManager

        Public Shared Function GetStream(ByVal DocumentId As Integer, ByVal FltIdentity As FltIdentity) As System.IO.Stream
            Try
                Return DocumentDB.GetStream(DocumentId)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Sub PutStream(ByVal DocumentId As Integer, ByVal DocumentStream As System.IO.Stream, ByVal FltIdentity As FltIdentity)
            Try
                DocumentDB.PutStream(DocumentId, DocumentStream)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Function CanView(ByVal FltIdentity As FltIdentity) As Boolean
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Documents", fltUserAction.CanView)
        End Function

        Public Shared Function CanEdit(ByVal FltIdentity As FltIdentity) As Boolean
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Documents", fltUserAction.CanEdit)
        End Function

    End Class

End Namespace
