' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class FltUserInRoleManager
		
        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetFltRoleListForFltUser(ByVal UserName As String, ByVal FltIdentity As FltIdentity) As FltRoleList
            If CanView(FltIdentity) Then
                Try
                    Return FltUserInRoleDB.ListFltRolesForFltUser(UserName)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't list - user is not authorized")
            End If
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetFltUserListForFltRole(ByVal RoleId As Integer, ByVal FltIdentity As FltIdentity) As FltUserList
            If CanView(FltIdentity) Then
                Try
                    Return FltUserInRoleDB.ListFltUsersForFltRole(RoleId)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't list - user is not authorized")
            End If
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetFltRoleListNotForFltUser(ByVal UserName As String, ByVal FltIdentity As FltIdentity) As FltRoleList
            If CanView(FltIdentity) Then
                Try
                    Return FltUserInRoleDB.ListFltRolesNotForFltUser(UserName)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't list - user is not authorized")
            End If
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetFltUserListNotForFltRole(ByVal RoleId As Integer, ByVal FltIdentity As FltIdentity) As FltUserList
            If CanView(FltIdentity) Then
                Try
                    Return FltUserInRoleDB.ListFltUsersNotForFltRole(RoleId)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't list - user is not authorized")
            End If
        End Function

        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Sub Insert(ByVal UserName As String, ByVal RoleId As Integer, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                Try
                    FltUserInRoleDB.Insert(UserName, RoleId, FltIdentity.UserName)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't insert - user is not authorized")
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal UserName As String, ByVal RoleId As Integer, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                Try
                    FltUserInRoleDB.Delete(UserName, RoleId, FltIdentity.UserName)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't deleted - user is not authorized")
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
