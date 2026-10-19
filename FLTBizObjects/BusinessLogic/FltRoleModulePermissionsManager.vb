' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class FltRoleModulePermissionsManager
		
        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList(ByVal RoleId As Integer, ByVal ModuleName As String, ByVal FltIdentity As FltIdentity) As FltRoleModulePermissionsList
            Dim totRows As Integer = 0
            Return GetList(RoleId, ModuleName, 1, 0, -1, -1, totRows, FltIdentity)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal RoleId As Integer, ByVal FltIdentity As FltIdentity) As FltRoleModulePermissionsList
            Dim totRows As Integer = 0
            Return GetList(RoleId, String.Empty, 1, 0, -1, -1, totRows, FltIdentity)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal ModuleName As String, ByVal FltIdentity As FltIdentity) As FltRoleModulePermissionsList
            Dim totRows As Integer = 0
            Return GetList(0, ModuleName, 1, 0, -1, -1, totRows, FltIdentity)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal RoleId As Integer, ByVal ModuleName As String, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal FltIdentity As FltIdentity) As FltRoleModulePermissionsList
            If CanView(FltIdentity) Then
                Try
                    Return FltRoleModulePermissionsDB.GetList(RoleId, ModuleName, IsActiveFilter, IsDeletedFilter, RowStart, RowCount, TotalRows)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't list - user is not authorized")
            End If
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetItem(ByVal RoleId As Integer, ByVal ModuleName As String, ByVal FltIdentity As FltIdentity) As FltRoleModulePermissions
            If CanView(FltIdentity) Then
                Try
                    Return FltRoleModulePermissionsDB.GetItem(RoleId, ModuleName)
                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't fetch - user is not authorized")
            End If
        End Function
		
        Public Shared Sub Save(ByVal FltRoleModulePermissions As FltRoleModulePermissions, ByVal FltIdentity As FltIdentity)
            If ((FltRoleModulePermissions.ConcurrencyId = Nothing) Or (FltRoleModulePermissions.ConcurrencyId = String.Empty) Or (FltRoleModulePermissions.ConcurrencyId = "0")) Then
                Insert(FltRoleModulePermissions, FltIdentity)
            Else
                Update(FltRoleModulePermissions, FltIdentity)
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal FltRoleModulePermissions As FltRoleModulePermissions, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                If FltRoleModulePermissions.Validate() Then
                    FltRoleModulePermissions.StampUserName = FltIdentity.UserName
                    Try
                        FltRoleModulePermissionsDB.Update(FltRoleModulePermissions)
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
        Public Shared Sub Insert(ByVal FltRoleModulePermissions As FltRoleModulePermissions, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                If FltRoleModulePermissions.Validate() Then
                    FltRoleModulePermissions.StampUserName = FltIdentity.UserName
                    Try
                        FltRoleModulePermissionsDB.Insert(FltRoleModulePermissions)
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
        Public Shared Sub Delete(ByVal FltRoleModulePermissions As FltRoleModulePermissions, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                Try
                    FltRoleModulePermissionsDB.Delete(FltRoleModulePermissions.RoleId, FltRoleModulePermissions.ModuleName, FltIdentity.UserName)
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
