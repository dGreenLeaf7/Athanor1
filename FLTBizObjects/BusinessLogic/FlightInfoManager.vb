' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

    <DataObjectAttribute()> _
    Public Class FlightInfoManager

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal AircraftId As Integer, ByVal AircraftTextMatch As String, ByVal LocationId As Integer, ByVal FltIdentity As FltIdentity) As FlightInfoList
            Dim totRows As Integer = 0
            Return GetList(AircraftId, AircraftTextMatch, LocationId, 0, 0, 0, String.Empty, String.Empty, 1, 1, 0, -1, -1, totRows, FltIdentity)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal AircraftId As Integer, ByVal AircraftTextMatch As String, ByVal LocationId As Integer, ByVal SchedRouteId As Integer, ByVal FlownRouteId As Integer, ByVal WorkshiftIndicatorId As Integer, ByVal FltIdentity As FltIdentity) As FlightInfoList
            Dim totRows As Integer = 0
            Return GetList(AircraftId, AircraftTextMatch, LocationId, SchedRouteId, FlownRouteId, WorkshiftIndicatorId, String.Empty, String.Empty, 1, 1, 0, -1, -1, totRows, FltIdentity)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal AircraftId As Integer, ByVal AircraftTextMatch As String, ByVal LocationId As Integer, ByVal SchedRouteId As Integer, ByVal FlownRouteId As Integer, ByVal WorkShiftIndicatorId As Integer, ByVal FilterDateBegin As String, ByVal FilterDateEnd As String, ByVal IsApprovedFilter As Integer, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal fltIdentity As FltIdentity) As FlightInfoList
            If CanView(fltIdentity) Then
                Try
                    Return FlightInfoDB.GetList(AircraftId, AircraftTextMatch, LocationId, SchedRouteId, FlownRouteId, WorkShiftIndicatorId, FilterDateBegin, FilterDateEnd, IsApprovedFilter, IsActiveFilter, IsDeletedFilter, RowStart, RowCount, TotalRows)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't list - user is not authorized")
            End If
        End Function

        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal FlightInfo As FlightInfo, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                Try
                    FlightDB.Delete(FlightInfo.FlightId, FltIdentity.UserName)
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