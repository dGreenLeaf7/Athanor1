' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class FlightCommentManager
		
        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal FlightId As Integer) As FlightCommentList
            Dim totRows As Integer = 0
            Return GetList(FlightId, 0, 1, 0, -1, -1, totRows)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal FlightId As Integer, ByVal ComponentId As Integer, ByVal IsActiveFilter As Long, ByVal IsDeletedFilter As Long, ByVal RowStart As Long, ByVal RowCount As Long, ByRef TotalRows As Long) As FlightCommentList
            Try
                Return FlightCommentDB.GetList(FlightId, ComponentId, IsActiveFilter, IsDeletedFilter, RowStart, RowCount, TotalRows)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
		
		<DataObjectMethod(DataObjectMethodType.Select, False)> _
		Public Shared Function GetItem(ByVal FlightCommentId As Long) As FlightComment
			Try
				Return FlightCommentDB.GetItem(FlightCommentId)
			Catch ex As Exception
				Throw New ApplicationException(ex.Message)
			End Try
		End Function
		
        Public Shared Sub Save(ByVal FlightComment As FlightComment, ByVal FltIdentity As FltIdentity)
            If ((FlightComment.ConcurrencyId = Nothing) Or (FlightComment.ConcurrencyId = String.Empty) Or (FlightComment.ConcurrencyId = "0")) Then
                Insert(FlightComment, FltIdentity)
            Else
                Update(FlightComment, FltIdentity)
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal FlightComment As FlightComment, ByVal FltIdentity As FltIdentity)
            If FlightComment.Validate() Then
                FlightComment.StampUserName = FltIdentity.UserName
                Try
                    FlightCommentDB.Update(FlightComment)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Function Insert(ByVal FlightComment As FlightComment, ByVal FltIdentity As FltIdentity) As Long
            Dim newId As Integer = 0
            If FlightComment.Validate() Then
                FlightComment.StampUserName = FltIdentity.UserName
                Try
                    newId = FlightCommentDB.Insert(FlightComment)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
            End If
            Return newId
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal FlightComment As FlightComment, ByVal FltIdentity As FltIdentity)
            Try
                FlightCommentDB.Delete(FlightComment.FlightCommentId, FltIdentity.UserName)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Sub
		
		Public Shared Function CanView(ByVal UserName As String, ByVal ProgId As Long) As Boolean
			Return True
		End Function
		
		Public Shared Function CanEdit(ByVal UserName As String, ByVal ProgId As Long) As Boolean
			Return True
		End Function
		
	End Class

End Namespace
