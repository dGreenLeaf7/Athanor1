' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class RegionManager
		
        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal ProgId As Integer, ByVal FltIdentity As FltIdentity) As RegionList
            Dim totRows As Integer = 0
            Return GetList(ProgId, 1, -1, -1, totRows, FltIdentity)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal ProgId As Integer, ByVal IsActiveFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal FltIdentity As FltIdentity) As RegionList
            If CanView(FltIdentity) Then
                Try
                    Return RegionDB.GetList(ProgId, IsActiveFilter, RowStart, RowCount, TotalRows)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't list - user is not authorized")
            End If
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetItem(ByVal Id As Integer, ByVal FltIdentity As FltIdentity) As Region
            If CanView(FltIdentity) Then
                Try
                    Return RegionDB.GetItem(Id)
                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't fetch - user is not authorized")
            End If
        End Function
		
        Public Shared Sub Save(ByVal Region As Region, ByVal FltIdentity As FltIdentity)
            If ((Region.ConcurrencyId = Nothing) Or (Region.ConcurrencyId = String.Empty) Or (Region.ConcurrencyId = "0")) Then
                Insert(Region, FltIdentity)
            Else
                Update(Region, FltIdentity)
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal Region As Region, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                If Region.Validate() Then
                    Try
                        RegionDB.Update(Region)
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
        Public Shared Function Insert(ByVal Region As Region, ByVal FltIdentity As FltIdentity) As Integer
            Dim newId As Integer = 0
            If CanEdit(FltIdentity) Then
                If Region.Validate() Then
                    Try
                        newId = RegionDB.Insert(Region)
                    Catch ex As ApplicationException
                        Throw New ApplicationException(ex.Message)
                    End Try
                Else
                    Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
                End If
            Else
                Throw New Exception("Can't insert - user is not authorized")
            End If
            Return newId
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal Region As Region, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                Try
                    RegionDB.Delete(Region.Id)
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
