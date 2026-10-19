' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class LiquidFillManager
		
        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal FlightId As Integer) As LiquidFillList
            Dim totRows As Integer = 0
            Return GetList(FlightId, 0, 0, 0, 1, 0, -1, -1, 0)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal FlightId As Integer, ByVal LiquidGroupId As Integer, ByVal LiquidTypeId As Integer, ByVal LiquidUnitId As Integer, ByVal IsActiveFilter As Long, ByVal IsDeletedFilter As Long, ByVal RowStart As Long, ByVal RowCount As Long, ByRef TotalRows As Long) As LiquidFillList
            Try
                Return LiquidFillDB.GetList(FlightId, LiquidGroupId, LiquidTypeId, LiquidUnitId, IsActiveFilter, IsDeletedFilter, RowStart, RowCount, TotalRows)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
		
		<DataObjectMethod(DataObjectMethodType.Select, False)> _
		Public Shared Function GetItem(ByVal LiquidFillId As Long) As LiquidFill
			Try
				Return LiquidFillDB.GetItem(LiquidFillId)
			Catch ex As Exception
				Throw New ApplicationException(ex.Message)
			End Try
		End Function
		
        Public Shared Sub Save(ByVal LiquidFill As LiquidFill, ByVal FltIdentity As FltIdentity)
            If ((LiquidFill.ConcurrencyId = Nothing) Or (LiquidFill.ConcurrencyId = String.Empty) Or (LiquidFill.ConcurrencyId = "0")) Then
                Insert(LiquidFill, FltIdentity)
            Else
                Update(LiquidFill, FltIdentity)
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal LiquidFill As LiquidFill, ByVal FltIdentity As FltIdentity)
            If LiquidFill.Validate() Then
                LiquidFill.StampUserName = FltIdentity.UserName
                Try
                    LiquidFillDB.Update(LiquidFill)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Function Insert(ByVal LiquidFill As LiquidFill, ByVal FltIdentity As FltIdentity) As Long
            If LiquidFill.Validate() Then
                LiquidFill.StampUserName = FltIdentity.UserName
                Try
                    Dim newLiquidFillId As Integer = LiquidFillDB.Insert(LiquidFill)
                    Return newLiquidFillId
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
            End If
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal LiquidFill As LiquidFill, ByVal FltIdentity As FltIdentity)
            Try
                LiquidFillDB.Delete(LiquidFill.LiquidFillId, FltIdentity.UserName)
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
