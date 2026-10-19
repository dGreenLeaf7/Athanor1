' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

    <DataObjectAttribute()> _
    Public Class DocumentInfoManager

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetItem(ByVal DocumentId As Integer, ByVal FltIdentity As FltIdentity) As DocumentInfo
            If CanView(FltIdentity) Then
                Try
                    Return DocumentInfoDB.GetItem(DocumentId)
                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't fetch - user is not authorized")
            End If
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal Progid As Integer, ByVal FltIdentity As FltIdentity) As DocumentInfoList
            Dim totRows As Integer = 0
            Return GetList(Progid, 0, 0, -1, -1, totRows, FltIdentity)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal ProgId As Integer, ByVal DocumentTypeId As Long, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal FltIdentity As FltIdentity) As DocumentInfoList
            If CanView(FltIdentity) Then
                Try
                    Return DocumentInfoDB.GetList(ProgId, DocumentTypeId, IsDeletedFilter, RowStart, RowCount, TotalRows)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't list - user is not authorized")
            End If
        End Function

        Public Shared Sub Save(ByVal DocumentInfo As DocumentInfo, ByVal FltIdentity As FltIdentity)
            If ((DocumentInfo.ConcurrencyId = Nothing) Or (DocumentInfo.ConcurrencyId = String.Empty) Or (DocumentInfo.ConcurrencyId = "0")) Then
                Insert(DocumentInfo, FltIdentity)
            Else
                Update(DocumentInfo, FltIdentity)
            End If
        End Sub

        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal DocumentInfo As DocumentInfo, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                If DocumentInfo.Validate() Then
                    DocumentInfo.StampUserName = FltIdentity.UserName
                    Try
                        DocumentInfoDB.Update(DocumentInfo)
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
        Public Shared Function Insert(ByVal DocumentInfo As DocumentInfo, ByVal FltIdentity As FltIdentity) As Integer
            Dim newId As Integer = 0
            If CanEdit(FltIdentity) Then
                If DocumentInfo.Validate() Then
                    DocumentInfo.StampUserName = FltIdentity.UserName
                    Try
                        newId = DocumentInfoDB.Insert(DocumentInfo)
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
        Public Shared Sub Delete(ByVal DocumentInfo As DocumentInfo, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                Try
                    DocumentInfoDB.Delete(DocumentInfo.DocumentId, FltIdentity.UserName)
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
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Documents", fltUserAction.CanEdit)
        End Function

    End Class

End Namespace