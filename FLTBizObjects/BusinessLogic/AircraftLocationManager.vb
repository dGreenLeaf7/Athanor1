' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class AircraftLocationManager
		
        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal AircraftId As Integer) As AircraftLocationList
            Dim totRows As Integer = 0
            Return GetList(AircraftId, 0, 1, 0, -1, -1, totRows)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal AircraftId As Integer, ByVal LocationId As Integer) As AircraftLocationList
            Dim totRows As Integer = 0
            Return GetList(AircraftId, LocationId, 1, 0, -1, -1, totRows)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal AircraftId As Integer, ByVal LocationId As Integer, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As AircraftLocationList
            Try
                Return AircraftLocationDB.GetList(AircraftId, LocationId, IsActiveFilter, IsDeletedFilter, RowStart, RowCount, TotalRows)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetItem(ByVal AircraftLocationId As Integer) As AircraftLocation
            Try
                Return AircraftLocationDB.GetItem(AircraftLocationId)
            Catch ex As Exception
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
		
        Public Shared Sub Save(ByVal AircraftLocation As AircraftLocation, ByVal FltIdentity As FltIdentity)
            If ((AircraftLocation.ConcurrencyId = Nothing) Or (AircraftLocation.ConcurrencyId = String.Empty) Or (AircraftLocation.ConcurrencyId = "0")) Then
                Insert(AircraftLocation, FltIdentity)
            Else
                Update(AircraftLocation, FltIdentity)
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal AircraftLocation As AircraftLocation, ByVal FltIdentity As FltIdentity)
            If AircraftLocation.Validate() Then
                AircraftLocation.StampUserName = FltIdentity.UserName
                Try
                    AircraftLocationDB.Update(AircraftLocation)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Function Insert(ByVal AircraftLocation As AircraftLocation, ByVal FltIdentity As FltIdentity) As Integer
            Dim newId As Integer = 0
            If AircraftLocation.Validate() Then
                AircraftLocation.StampUserName = FltIdentity.UserName
                Try
                    newId = AircraftLocationDB.Insert(AircraftLocation)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
            End If
            Return newId
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal AircraftLocation As AircraftLocation, ByVal FltIdentity As FltIdentity)
            Try
                AircraftLocationDB.Delete(AircraftLocation.AircraftLocationId, FltIdentity.UserName)
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
