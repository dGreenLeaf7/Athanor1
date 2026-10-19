' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class WOEngineerCommentManager
		
        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal WorkOrderId As Integer) As WOEngineerCommentList
            Dim totRows As Integer = 0
            Return GetList(WorkOrderId, 0, -1, -1, totRows)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal WorkOrderId As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As WOEngineerCommentList
            Try
                Return WOEngineerCommentDB.GetList(WorkOrderId, IsDeletedFilter, RowStart, RowCount, TotalRows)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetItem(ByVal EngineerCommentsId As Integer) As WOEngineerComment
            Try
                Return WOEngineerCommentDB.GetItem(EngineerCommentsId)
            Catch ex As Exception
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
		
        Public Shared Sub Save(ByVal WOEngineerComment As WOEngineerComment, ByVal FltIdentity As FltIdentity)
            If ((WOEngineerComment.ConcurrencyId = Nothing) Or (WOEngineerComment.ConcurrencyId = String.Empty) Or (WOEngineerComment.ConcurrencyId = "0")) Then
                Insert(WOEngineerComment, FltIdentity)
            Else
                Update(WOEngineerComment, FltIdentity)
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal WOEngineerComment As WOEngineerComment, ByVal FltIdentity As FltIdentity)
            If WOEngineerComment.Validate() Then
                WOEngineerComment.StampUserName = FltIdentity.UserName
                Try
                    WOEngineerCommentDB.Update(WOEngineerComment)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Sub Insert(ByVal WOEngineerComment As WOEngineerComment, ByVal FltIdentity As FltIdentity)
            If WOEngineerComment.Validate() Then
                WOEngineerComment.StampUserName = FltIdentity.UserName
                Try
                    WOEngineerCommentDB.Insert(WOEngineerComment)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal WOEngineerComment As WOEngineerComment, ByVal FltIdentity As FltIdentity)
            Try
                WOEngineerCommentDB.Delete(WOEngineerComment.EngineerCommentsId, FltIdentity.UserName)
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
