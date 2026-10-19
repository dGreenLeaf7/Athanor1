' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class FltModuleManager
		
        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal FltIdentity As FltIdentity) As FltModuleList
            Dim totRows As Integer = 0
            Return GetList(1, 0, -1, -1, totRows, FltIdentity)
        End Function

        Public Shared Function GetList(ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal FltIdentity As FltIdentity) As FltModuleList
            If CanView(FltIdentity) Then
                Try
                    Return FltModuleDB.GetList(IsActiveFilter, IsDeletedFilter, RowStart, RowCount, TotalRows)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New ApplicationException("Can't list - user is unauthorized")
            End If
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetItem(ByVal ModuleName As String, ByVal FltIdentity As FltIdentity) As FltModule
            If CanView(FltIdentity) Then
                Try
                    Return FltModuleDB.GetItem(ModuleName)
                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't fetch - user is unauthorized")
            End If
        End Function
		
        Public Shared Sub Save(ByVal FltModule As FltModule, ByVal FltIdentity As FltIdentity)
            If ((FltModule.ConcurrencyId = Nothing) Or (FltModule.ConcurrencyId = String.Empty) Or (FltModule.ConcurrencyId = "0")) Then
                Insert(FltModule, FltIdentity)
            Else
                Update(FltModule, FltIdentity)
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal FltModule As FltModule, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                If FltModule.Validate() Then
                    FltModule.StampUserName = FltIdentity.UserName
                    Try
                        FltModuleDB.Update(FltModule)
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
        Public Shared Sub Insert(ByVal FltModule As FltModule, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                If FltModule.Validate() Then
                    FltModule.StampUserName = FltIdentity.UserName
                    Try
                        FltModuleDB.Insert(FltModule)
                    Catch ex As ApplicationException
                        Throw New ApplicationException(ex.Message)
                    End Try
                Else
                    Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
                End If
            Else
                Throw New ApplicationException("Can't insert - user is not authorized")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal FltModule As FltModule, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                Try
                    FltModuleDB.Delete(FltModule.ModuleName, FltIdentity.UserName)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't delete - user is not authorized")
            End If
        End Sub
		
        Public Shared Function CanView(ByVal FltIdentity As FltIdentity) As Boolean
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Security", fltUserAction.CanView)
        End Function
		
        Public Shared Function CanEdit(ByVal FltIdentity As FltIdentity) As Boolean
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Security", fltUserAction.CanEdit)
        End Function
		
	End Class

End Namespace
