' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

    <DataObjectAttribute()> _
    Public Class WorkOrderInfoManager

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal ProgId As Integer, ByVal AircraftId As Integer, ByVal LocationId As Integer, ByRef FltIdentity As FltIdentity) As WorkOrderInfoList
            Dim totRows As Integer = 0
            Return GetList(ProgId, AircraftId, String.Empty, LocationId, 0, String.Empty, 0, -1, -1, totRows, FltIdentity)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal ProgId As Integer, ByVal AircraftId As Integer, ByVal AircraftTextMatch As String, ByVal LocationId As Integer, ByVal MechanicId As Integer, ByVal WorkOrderNbr As String, ByRef FltIdentity As FltIdentity) As WorkOrderInfoList
            Dim totRows As Integer = 0
            Return GetList(ProgId, AircraftId, AircraftTextMatch, LocationId, MechanicId, WorkOrderNbr, 0, -1, -1, totRows, FltIdentity)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal ProgId As Integer, ByVal AircraftId As Integer, ByVal AircraftTextMatch As String, ByVal LocationId As Integer, ByVal MechanicId As Integer, ByVal WorkOrderNbr As String, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByRef FltIdentity As FltIdentity) As WorkOrderInfoList
            If CanView(FltIdentity) Then
                Try
                    Return WorkOrderInfoDB.GetList(ProgId, AircraftId, AircraftTextMatch, LocationId, MechanicId, WorkOrderNbr, IsDeletedFilter, RowStart, RowCount, TotalRows)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't list - user is not authorized")
            End If
        End Function

        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal WorkOrderInfo As WorkOrderInfo, ByVal FltIdentity As FltIdentity)
            Try
                WorkOrderDB.Delete(WorkOrderInfo.WorkOrderId, FltIdentity.UserName)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Sub

        Private Shared Function CanView(ByVal FltIdentity As FltIdentity) As Boolean
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Fleet", fltUserAction.CanView)
        End Function

        Private Shared Function CanEdit(ByVal FltIdentity As FltIdentity) As Boolean
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Fleet", fltUserAction.CanEdit)
        End Function

    End Class

End Namespace