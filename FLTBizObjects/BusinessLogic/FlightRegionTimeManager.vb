' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class FlightRegionTimeManager
		
        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal FlightId As Integer) As FlightRegionTimeList
            Dim totRows As Integer = 0
            Return GetList(FlightId, 0, 1, 0, -1, -1, totRows)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal FlightId As Integer, ByVal RegionId As Integer, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As FlightRegionTimeList
            Try
                Return FlightRegionTimeDB.GetList(FlightId, RegionId, IsActiveFilter, IsDeletedFilter, RowStart, RowCount, TotalRows)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetItem(ByVal FlightRegionTimeId As Integer) As FlightRegionTime
            Try
                Return FlightRegionTimeDB.GetItem(FlightRegionTimeId)
            Catch ex As Exception
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
		
        Public Shared Sub Save(ByVal FlightRegionTime As FlightRegionTime, ByVal FltIdentity As FltIdentity)
            If ((FlightRegionTime.ConcurrencyId = Nothing) Or (FlightRegionTime.ConcurrencyId = String.Empty) Or (FlightRegionTime.ConcurrencyId = "0")) Then
                Insert(FlightRegionTime, FltIdentity)
            Else
                Update(FlightRegionTime, FltIdentity)
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal FlightRegionTime As FlightRegionTime, ByVal FltIdentity As FltIdentity)
            If FlightRegionTime.Validate() Then
                FlightRegionTime.StampUserName = FltIdentity.UserName
                Try
                    FlightRegionTimeDB.Update(FlightRegionTime)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Function Insert(ByVal FlightRegionTime As FlightRegionTime, ByVal FltIdentity As FltIdentity) As Integer
            Dim newId As Integer = 0
            If FlightRegionTime.Validate() Then
                FlightRegionTime.StampUserName = FltIdentity.UserName
                Try
                    newId = FlightRegionTimeDB.Insert(FlightRegionTime)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
            End If
            Return newId
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal FlightRegionTime As FlightRegionTime, ByVal FltIdentity As FltIdentity)
            Try
                FlightRegionTimeDB.Delete(FlightRegionTime.FlightRegionTimeId, FltIdentity.UserName)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Sub
		
        Public Shared Function CanView(ByVal UserName As String, ByVal ProgId As Integer) As Boolean
            Return True
        End Function
		
        Public Shared Function CanEdit(ByVal UserName As String, ByVal ProgId As Integer) As Boolean
            Return True
        End Function
		
	End Class

End Namespace
