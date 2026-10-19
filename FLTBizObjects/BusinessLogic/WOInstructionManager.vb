' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class WOInstructionManager
		
        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal WorkOrderId As Integer)
            Dim totRows As Integer = 0
            Return GetList(WorkOrderId, 0, -1, -1, totRows)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal WorkOrderId As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As WOInstructionList
            Try
                Return WOInstructionDB.GetList(WorkOrderId, IsDeletedFilter, RowStart, RowCount, TotalRows)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetItem(ByVal WOInstructionsId As Integer) As WOInstruction
            Try
                Return WOInstructionDB.GetItem(WOInstructionsId)
            Catch ex As Exception
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
		
        Public Shared Sub Save(ByVal WOInstruction As WOInstruction, ByVal FltIdentity As FltIdentity)
            If ((WOInstruction.ConcurrencyId = Nothing) Or (WOInstruction.ConcurrencyId = String.Empty) Or (WOInstruction.ConcurrencyId = "0")) Then
                Insert(WOInstruction, FltIdentity)
            Else
                Update(WOInstruction, FltIdentity)
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal WOInstruction As WOInstruction, ByVal FltIdentity As FltIdentity)
            If WOInstruction.Validate() Then
                WOInstruction.StampUserName = FltIdentity.UserName
                Try
                    WOInstructionDB.Update(WOInstruction)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Sub Insert(ByVal WOInstruction As WOInstruction, ByVal FltIdentity As FltIdentity)
            If WOInstruction.Validate() Then
                WOInstruction.StampUserName = FltIdentity.UserName
                Try
                    WOInstructionDB.Insert(WOInstruction)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal WOInstruction As WOInstruction, ByVal FltIdentity As FltIdentity)
            Try
                WOInstructionDB.Delete(WOInstruction.WOInstructionsId, FltIdentity.UserName)
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
