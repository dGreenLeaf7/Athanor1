' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

    <DataObjectAttribute()> _
    Public Class DocumentTypeManager

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal FltIdentity As FltIdentity) As DocumentTypeList
            Dim totRows As Integer = 0
            Return GetList(-1, -1, totRows, FltIdentity)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal FltIdentity As FltIdentity) As DocumentTypeList
            If CanView(FltIdentity) Then
                Try
                    Return DocumentTypeDB.GetList(RowStart, RowCount, TotalRows)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't list - user is not authorized")
            End If
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetItem(ByVal DocumentTypeId As Integer, ByVal FltIdentity As FltIdentity) As DocumentType
            If CanView(FltIdentity) Then
                Try
                    Return DocumentTypeDB.GetItem(DocumentTypeId)
                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't fetch - user is not authorized")
            End If
        End Function

        Public Shared Sub Save(ByVal DocumentType As DocumentType, ByVal FltIdentity As FltIdentity)
            If ((DocumentType.ConcurrencyId = Nothing) Or (DocumentType.ConcurrencyId = String.Empty) Or (DocumentType.ConcurrencyId = "0")) Then
                Insert(DocumentType, FltIdentity)
            Else
                Update(DocumentType, FltIdentity)
            End If
        End Sub

        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal DocumentType As DocumentType, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                If DocumentType.Validate() Then
                    Try
                        DocumentTypeDB.Update(DocumentType)
                    Catch ex As ApplicationException
                        Throw New ApplicationException(ex.Message)
                    End Try
                Else
                    Throw New ApplicationException("Can't update - record is invalid. Please correct errors and try again.")
                End If
            Else
                Throw New Exception("Can't update - user is not authorized")
            End If
        End Sub

        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Function Insert(ByVal DocumentType As DocumentType, ByVal FltIdentity As FltIdentity) As Integer
            Dim newId As Integer = 0
            If CanEdit(FltIdentity) Then
                If DocumentType.Validate() Then
                    Try
                        newId = DocumentTypeDB.Insert(DocumentType)
                    Catch ex As ApplicationException
                        Throw New ApplicationException(ex.Message)
                    End Try
                Else
                    Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
                End If
            Else
                Throw New Exception("Can't insert - user is not authorized")
            End If
            Return newId
        End Function

        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal DocumentType As DocumentType, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                Try
                    DocumentTypeDB.Delete(DocumentType.DocumentTypeId, FltIdentity.UserName)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't delete - user is not authorized")
            End If
        End Sub

        Public Shared Function CanView(ByVal FltIdentity As FltIdentity) As Boolean
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Documents", fltUserAction.CanView)
        End Function

        Public Shared Function CanEdit(ByVal FltIdentity As FltIdentity) As Boolean
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "System", fltUserAction.CanEdit)
        End Function

    End Class

End Namespace
