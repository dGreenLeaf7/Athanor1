' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

    <DataObjectAttribute()> _
    Public Class AircraftTestXrefManager

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetTestListForAircraft(ByVal AircraftId As Integer, ByVal ProgId As Integer) As fltLookupBaseList
            Try
                Return AircraftTestXrefDB.ListTestsForAircraft(AircraftId, ProgId)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetAircraftInfoListForTest(ByVal TestId As Integer, ByVal ProgId As Integer) As AircraftInfoList
            Try
                Return AircraftTestXrefDB.ListAircraftInfosForTest(TestId, ProgId)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetTestListNotForAircraft(ByVal AircraftId As String, ByVal ProgId As Integer) As fltLookupBaseList
            Try
                Return AircraftTestXrefDB.ListTestsNotForAircraft(AircraftId, ProgId)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetAircraftListNotForTest(ByVal TestId As Integer, ByVal ProgId As Integer) As AircraftInfoList
            Try
                Return AircraftTestXrefDB.ListAircraftInfosNotForTest(TestId, ProgId)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Function

        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Sub Insert(ByVal AircraftId As String, ByVal TestId As Integer, ByVal FltIdentity As FltIdentity)
            Try
                AircraftTestXrefDB.Insert(AircraftId, TestId, FltIdentity.UserName)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Sub

        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal AircraftId As String, ByVal TestId As Integer, ByVal FltIdentity As FltIdentity)
            Try
                AircraftTestXrefDB.Delete(AircraftId, TestId, FltIdentity.UserName)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Sub

    End Class

End Namespace
