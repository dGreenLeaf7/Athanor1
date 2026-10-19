' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class UTCTimeZoneManager
		
        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal FltIdentity As FltIdentity) As UTCTimeZoneList
            Dim totRows As Integer = 0
            Return GetList(1, -1, -1, totRows, FltIdentity)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal IsActiveFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal FltIdentity As FltIdentity) As UTCTimeZoneList
            If CanView(FltIdentity) Then
                Try
                    Return UTCTimeZoneDB.GetList(IsActiveFilter, RowStart, RowCount, TotalRows)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't list - user is not authorized")
            End If
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetItem(ByVal TimeZoneId As Integer, ByVal FltIdentity As FltIdentity) As UTCTimeZone
            If CanView(FltIdentity) Then
                Try
                    Return UTCTimeZoneDB.GetItem(TimeZoneId)
                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't fetch - user is not authorized")
            End If
        End Function
		
        Public Shared Sub Save(ByVal UTCTimeZone As UTCTimeZone, ByVal FltIdentity As FltIdentity)
            If ((UTCTimeZone.ConcurrencyId = Nothing) Or (UTCTimeZone.ConcurrencyId = String.Empty) Or (UTCTimeZone.ConcurrencyId = "0")) Then
                Insert(UTCTimeZone, FltIdentity)
            Else
                Update(UTCTimeZone, FltIdentity)
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal UTCTimeZone As UTCTimeZone, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                If UTCTimeZone.Validate() Then
                    Try
                        UTCTimeZoneDB.Update(UTCTimeZone)
                    Catch ex As ApplicationException
                        Throw New ApplicationException(ex.Message)
                    End Try
                Else
                    Throw New ApplicationException("Can't update - record is invalid. Please correct errors and try again.")
                End If
            Else
                Throw New Exception("Can't update - user is not authorized")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Sub Insert(ByVal UTCTimeZone As UTCTimeZone, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                If UTCTimeZone.Validate() Then
                    Try
                        UTCTimeZoneDB.Insert(UTCTimeZone)
                    Catch ex As ApplicationException
                        Throw New ApplicationException(ex.Message)
                    End Try
                Else
                    Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
                End If
            Else
                Throw New Exception("Can't insert - user is not authorized")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal UTCTimeZone As UTCTimeZone, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                Try
                    UTCTimeZoneDB.Delete(UTCTimeZone.TimeZoneID)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't delete - user is not authorized")
            End If
        End Sub
		
        Public Shared Function CanView(ByVal FltIdentity As FltIdentity) As Boolean
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "System", fltUserAction.CanView)
        End Function
		
        Public Shared Function CanEdit(ByVal FltIdentity As FltIdentity) As Boolean
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "System", fltUserAction.CanEdit)
        End Function
		
	End Class

End Namespace
