' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class AircraftConfigurationManager
		
        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal AircraftId As Integer) As AircraftConfigurationList
            Dim totRows As Integer = 0
            Return GetList(AircraftId, 0, -1, -1, totRows)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal AircraftId As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As AircraftConfigurationList
            Try
                Return AircraftConfigurationDB.GetList(AircraftId, IsDeletedFilter, RowStart, RowCount, TotalRows)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetItem(ByVal AircraftConfigurationId As Integer) As AircraftConfiguration
            Try
                Return AircraftConfigurationDB.GetItem(AircraftConfigurationId)
            Catch ex As Exception
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
		
        Public Shared Sub Save(ByVal AircraftConfiguration As AircraftConfiguration, ByVal FltIdentity As FltIdentity)
            If ((AircraftConfiguration.ConcurrencyId = Nothing) Or (AircraftConfiguration.ConcurrencyId = String.Empty) Or (AircraftConfiguration.ConcurrencyId = "0")) Then
                Insert(AircraftConfiguration, FltIdentity)
            Else
                Update(AircraftConfiguration, FltIdentity)
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal AircraftConfiguration As AircraftConfiguration, ByVal FltIdentity As FltIdentity)
            If AircraftConfiguration.Validate() Then
                AircraftConfiguration.StampUserName = FltIdentity.UserName
                Try
                    AircraftConfigurationDB.Update(AircraftConfiguration)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Function Insert(ByVal AircraftConfiguration As AircraftConfiguration, ByVal FltIdentity As FltIdentity) As Integer
            Dim newId As Long = 0
            If AircraftConfiguration.Validate() Then
                AircraftConfiguration.StampUserName = FltIdentity.UserName
                Try
                    newId = AircraftConfigurationDB.Insert(AircraftConfiguration)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
            End If
            Return newId
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal AircraftConfiguration As AircraftConfiguration, ByVal FltIdentity As FltIdentity)
            Try
                AircraftConfigurationDB.Delete(AircraftConfiguration.AircraftConfigurationId, FltIdentity.UserName)
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
