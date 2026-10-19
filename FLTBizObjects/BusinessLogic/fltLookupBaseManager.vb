' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports System.Transactions
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

    <DataObjectAttribute()> _
    Public Class fltLookupBaseManager

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal LookupName As String, ByVal Progid As Integer) As fltLookupBaseList
            Dim totRows As Integer = 0
            Return GetList(LookupName, Progid, 1, -1, -1, totRows)
        End Function

        Public Shared Function GetList(ByVal LookupName As String, ByVal ProgId As Integer, ByVal IsActiveFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As fltLookupBaseList
            Try
                Return fltLookupBaseDB.GetList(LookupName, ProgId, IsActiveFilter, RowStart, RowCount, TotalRows)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetItem(ByVal DefName As String, ByVal Id As Integer) As fltLookupBase
            Try
                Return fltLookupBaseDB.GetItem(DefName, Id)
            Catch ex As Exception
                Throw New ApplicationException(ex.Message)
            End Try
        End Function

        Public Shared Sub Save(ByVal DefObject As fltLookupBase, ByVal FltIdentity As FltIdentity)
            If ((DefObject.ConcurrencyId = Nothing) Or (DefObject.ConcurrencyId = String.Empty) Or (DefObject.ConcurrencyId = "0")) Then
                Insert(DefObject, FltIdentity)
            Else
                Update(DefObject, FltIdentity)
            End If
        End Sub

        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal DefObject As fltLookupBase, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                If DefObject.Validate() Then
                    Using myTransactionScope As TransactionScope = New TransactionScope
                        Try
                            fltLookupBaseDB.Update(DefObject)
                            myTransactionScope.Complete()
                        Catch ex As ApplicationException
                            Throw New ApplicationException(ex.Message)
                        End Try
                    End Using
                Else
                    Throw New ApplicationException("Can't update - record is invalid. Please correct errors and try again.")
                End If
            Else
                Throw New Exception("Can't update - user is not authorized")
            End If
        End Sub

        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Function Insert(ByVal DefObject As fltLookupBase, ByVal FltIdentity As FltIdentity) As Integer
            Dim newId As Integer = 0
            If CanEdit(FltIdentity) Then
                If DefObject.Validate() Then
                    Using myTransactionScope As TransactionScope = New TransactionScope
                        Try
                            newId = fltLookupBaseDB.Insert(DefObject)
                            myTransactionScope.Complete()
                        Catch ex As ApplicationException
                            Throw New ApplicationException(ex.Message)
                        End Try
                    End Using
                Else
                    Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
                End If
            Else
                Throw New Exception("Can't insert - user is not authorized")
            End If
            Return newId
        End Function

        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal LookupBase As fltLookupBase, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                Using myTransactionScope As TransactionScope = New TransactionScope
                    Try
                        fltLookupBaseDB.Delete(LookupBase.LookupName, LookupBase.ID)
                    Catch ex As ApplicationException
                        Throw New ApplicationException(ex.Message)
                    End Try
                    myTransactionScope.Complete()
                End Using
            Else
                Throw New Exception("Can't delete - user is not authorized")
            End If
        End Sub

        Public Shared Function CanView(ByVal FltIdentity As FltIdentity) As Boolean
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "ConfigTables", fltUserAction.CanView)
        End Function

        Public Shared Function CanEdit(ByVal FltIdentity As FltIdentity) As Boolean
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "ConfigTables", fltUserAction.CanEdit)
        End Function

    End Class

End Namespace
