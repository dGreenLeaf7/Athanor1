' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

    <DataObjectAttribute()> _
    Public Class WeightDefTypeManager

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal FltIdentity As FltIdentity) As WeightDefTypeList
            Dim totRows As Integer = 0
            Return GetList(-1, -1, totRows, FltIdentity)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal FltIdentity As FltIdentity) As WeightDefTypeList
            If CanView(FltIdentity) Then
                Try
                    Return WeightDefTypeDB.GetList(RowStart, RowCount, TotalRows)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't list - user is not authorized")
            End If
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetItem(ByVal WeightDefTypeId As Integer, ByVal FltIdentity As FltIdentity) As WeightDefType
            If CanView(FltIdentity) Then
                Try
                    Return WeightDefTypeDB.GetItem(WeightDefTypeId)
                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't fetch - user is not authorized")
            End If
        End Function

        Public Shared Sub Save(ByVal WeightDefType As WeightDefType, ByVal FltIdentity As FltIdentity)
            If ((WeightDefType.ConcurrencyId = Nothing) Or (WeightDefType.ConcurrencyId = String.Empty) Or (WeightDefType.ConcurrencyId = "0")) Then
                Insert(WeightDefType, FltIdentity)
            Else
                Update(WeightDefType, FltIdentity)
            End If
        End Sub

        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal WeightDefType As WeightDefType, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                If WeightDefType.Validate() Then
                    Try
                        WeightDefTypeDB.Update(WeightDefType)
                    Catch ex As ApplicationException
                        Throw New ApplicationException(ex.Message)
                    End Try
                Else
                    Throw New ApplicationException("Can't update - record is invalid. Please correct errors and try again.")
                End If
            Else
                Throw New Exception("Can't insert - user is not authorized")
            End If
        End Sub

        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Function Insert(ByVal WeightDefType As WeightDefType, ByVal FltIdentity As FltIdentity) As Integer
            Dim newId As Integer = 0
            If CanEdit(FltIdentity) Then
                If WeightDefType.Validate() Then
                    Try
                        newId = WeightDefTypeDB.Insert(WeightDefType)
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
        Public Shared Sub Delete(ByVal WeightDefType As WeightDefType, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                Try
                    WeightDefTypeDB.Delete(WeightDefType.WeightDefTypeId, FltIdentity.UserName)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't delete - user is not authorized")
            End If
        End Sub

        Public Shared Function CanView(ByVal FltIdentity As FltIdentity) As Boolean
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Fleet", fltUserAction.CanView)
        End Function

        Public Shared Function CanEdit(ByVal FltIdentity As FltIdentity) As Boolean
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Fleet", fltUserAction.CanEdit)
        End Function

    End Class

End Namespace