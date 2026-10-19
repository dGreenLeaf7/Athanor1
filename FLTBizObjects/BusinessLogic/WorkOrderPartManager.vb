' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class WorkOrderPartManager
		
        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal WorkOrderId As Integer) As WorkOrderPartList
            Dim totRows As Integer = 0
            Return GetList(WorkOrderId, 0, -1, -1, totRows)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal WorkOrderId As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As WorkOrderPartList
            Try
                Return WorkOrderPartDB.GetList(WorkOrderId, IsDeletedFilter, RowStart, RowCount, TotalRows)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetItem(ByVal WorkOrderPartId As Integer) As WorkOrderPart
            Try
                Return WorkOrderPartDB.GetItem(WorkOrderPartId)
            Catch ex As Exception
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
		
        Public Shared Sub Save(ByVal WorkOrderPart As WorkOrderPart, ByVal FltIdentity As FltIdentity)
            If ((WorkOrderPart.ConcurrencyId = Nothing) Or (WorkOrderPart.ConcurrencyId = String.Empty) Or (WorkOrderPart.ConcurrencyId = "0")) Then
                Insert(WorkOrderPart, FltIdentity)
            Else
                Update(WorkOrderPart, FltIdentity)
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal WorkOrderPart As WorkOrderPart, ByVal FltIdentity As FltIdentity)
            If WorkOrderPart.Validate() Then
                WorkOrderPart.StampUserName = FltIdentity.UserName
                Try
                    WorkOrderPartDB.Update(WorkOrderPart)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Sub Insert(ByVal WorkOrderPart As WorkOrderPart, ByVal FltIdentity As FltIdentity)
            If WorkOrderPart.Validate() Then
                WorkOrderPart.StampUserName = FltIdentity.UserName
                Try
                    WorkOrderPartDB.Insert(WorkOrderPart)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal WorkOrderPart As WorkOrderPart, ByVal FltIdentity As FltIdentity)
            Try
                WorkOrderPartDB.Delete(WorkOrderPart.WorkOrderPartId, FltIdentity.UserName)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Sub
		
        Public Shared Function CanView(ByVal FltIdentity As FltIdentity) As Boolean
            Return True
        End Function
		
        Public Shared Function CanEdit(ByVal FltIdentity As FltIdentity) As Boolean
            Return True
        End Function
		
	End Class

End Namespace
