' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

    <DataObjectAttribute()> _
    Public Class WeightDefManager

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal ProgId As Integer, ByVal WeightDefType As String, ByVal FltIdentity As FltIdentity) As WeightDefList
            Dim totRows As Integer = 0
            Return GetList(ProgId, WeightDefType, 0, 1, 0, -1, -1, totRows, FltIdentity)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal ProgId As Integer, ByVal WeightDefTypeId As Integer, ByVal FltIdentity As FltIdentity) As WeightDefList
            Dim totRows As Integer = 0
            Return GetList(ProgId, String.Empty, WeightDefTypeId, 1, 0, -1, -1, totRows, FltIdentity)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal ProgId As Integer, ByVal WeightDefTypeDescr As String, ByVal WeightDefTypeId As Integer, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal FltIdentity As FltIdentity) As WeightDefList
            If CanView(FltIdentity) Then
                Try
                    Return WeightDefDB.GetList(ProgId, WeightDefTypeDescr, WeightDefTypeId, IsActiveFilter, IsDeletedFilter, RowStart, RowCount, TotalRows)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't list - user is not authorized")
            End If
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetItem(ByVal WeightDefId As Integer, ByVal FltIdentity As FltIdentity) As WeightDef
            If CanView(FltIdentity) Then
                Try
                    Return WeightDefDB.GetItem(WeightDefId)
                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't fetch - user is not authorized")
            End If
        End Function

        Public Shared Sub Save(ByVal WeightDef As WeightDef, ByVal FltIdentity As FltIdentity)
            If ((WeightDef.ConcurrencyId = Nothing) Or (WeightDef.ConcurrencyId = String.Empty) Or (WeightDef.ConcurrencyId = "0")) Then
                Insert(WeightDef, FltIdentity)
            Else
                Update(WeightDef, FltIdentity)
            End If
        End Sub

        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal WeightDef As WeightDef, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                If WeightDef.Validate() Then
                    WeightDef.StampUserName = FltIdentity.UserName
                    Try
                        WeightDefDB.Update(WeightDef)
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
        Public Shared Function Insert(ByVal WeightDef As WeightDef, ByVal FltIdentity As FltIdentity) As Integer
            Dim newId As Integer = 0
            If CanEdit(FltIdentity) Then
                If WeightDef.Validate() Then
                    WeightDef.StampUserName = FltIdentity.UserName
                    Try
                        newId = WeightDefDB.Insert(WeightDef)
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
        Public Shared Sub Delete(ByVal WeightDef As WeightDef, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                Try
                    WeightDefDB.Delete(WeightDef.WeightDefId, FltIdentity.UserName)
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
