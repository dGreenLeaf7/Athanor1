' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class ProgramManager
		
        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetList() As ProgramList
            Dim totRows As Integer = 0
            Return GetList(1, 0, -1, -1, totRows)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetList(ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As ProgramList
            Try
                Return ProgramDB.GetList(IsActiveFilter, IsDeletedFilter, RowStart, RowCount, TotalRows)
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
		
        <DataObjectMethod(DataObjectMethodType.Select, False)> _
        Public Shared Function GetItem(ByVal ProgID As Integer, ByVal FltIdentity As FltIdentity) As Program
            If CanView(FltIdentity) Then
                Try
                    Return ProgramDB.GetItem(ProgID)
                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't fetch - user is not authorized")
            End If
        End Function
		
        Public Shared Sub Save(ByVal Program As Program, ByVal FltIdentity As FltIdentity)
            If ((Program.ConcurrencyId = Nothing) Or (Program.ConcurrencyId = String.Empty) Or (Program.ConcurrencyId = "0")) Then
                Insert(Program, FltIdentity)
            Else
                Update(Program, FltIdentity)
            End If
        End Sub
		
        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal Program As Program, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                If Program.Validate() Then
                    Program.StampUserName = FltIdentity.UserName
                    Try
                        ProgramDB.Update(Program)
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
        Public Shared Function Insert(ByVal Program As Program, byval FltIdentity as FltIdentity) As Integer
            Dim newId As Integer = 0
            If CanEdit(FltIdentity) Then
                If Program.Validate() Then
                    Program.StampUserName = FltIdentity.UserName
                    Try
                        newId = ProgramDB.Insert(Program)
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
        Public Shared Sub Delete(ByVal Program As Program, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                Try
                    ProgramDB.Delete(Program.ProgID, FltIdentity.UserName)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't delete - user is not authorized")
            End If
        End Sub

        Public Shared Sub Clone(ByVal SourceProgId As Integer, ByVal Program As Program, ByVal FltIdentity As FltIdentity)

            Dim totRows As Integer

            Try
                If CanEdit(FltIdentity) Then
                    Program.StampUserName = FltIdentity.UserName
                    Dim newProgId As Integer = ProgramDB.Insert(Program)

                    For Each thisRole As FltRole In FltRoleDB.GetList(SourceProgId, 1, 0, -1, -1, totRows)
                        thisRole.ProgId = newProgId
                        thisRole.StampUserName = FltIdentity.UserName
                        Dim sourceRoleId As Integer = thisRole.RoleId
                        Dim newRoleId As Integer = FltRoleDB.Insert(thisRole)
                        For Each thisRoleModulePermission As FltRoleModulePermissions In FltRoleModulePermissionsDB.GetList(sourceRoleId, String.Empty, 1, 0, -1, -1, totRows)
                            thisRoleModulePermission.RoleId = newRoleId
                            thisRoleModulePermission.StampUserName = FltIdentity.UserName
                            FltRoleModulePermissionsDB.Insert(thisRoleModulePermission)
                        Next
                        For Each thisUser As FltUser In FltUserInRoleDB.ListFltUsersForFltRole(sourceRoleId)
                            FltUserInRoleDB.Insert(thisUser.UserName, newRoleId, FltIdentity.UserName)
                        Next
                    Next

                    For Each thisRegion As Region In RegionDB.GetList(SourceProgId, 1, -1, -1, totRows)
                        thisRegion.ProgId = newProgId
                        RegionDB.Insert(thisRegion)
                    Next

                    For Each thisLocation As Location In LocationDB.GetList(SourceProgId, 0, 1, 0, -1, -1, totRows)
                        thisLocation.ProgId = newProgId
                        thisLocation.StampUserName = FltIdentity.UserName
                        Dim sourceLocationId As Integer = thisLocation.LocactionId
                        Dim thisLocationId As Integer = LocationDB.Insert(thisLocation)
                        For Each thisRoute As Route In RouteDB.GetList(SourceProgId, sourceLocationId, 0, 1, 0, -1, -1, totRows)
                            thisRoute.LocationId = thisLocationId
                            thisRoute.StampUserName = FltIdentity.UserName
                            RouteDB.Insert(thisRoute)
                        Next
                    Next

                    For Each thisWeightDef As WeightDef In WeightDefDB.GetList(SourceProgId, String.Empty, 0, 1, 0, -1, -1, totRows)
                        thisWeightDef.ProgId = newProgId
                        thisWeightDef.StampUserName = FltIdentity.UserName
                        Dim thisWeightDefId As Integer = WeightDefDB.Insert(thisWeightDef)
                    Next

                    For Each thisLookUpName As String In New fltLookupNameList
                        For Each thisLookupBase As fltLookupBase In fltLookupBaseDB.GetList(thisLookUpName, SourceProgId, 1, -1, -1, totRows)
                            thisLookupBase.ProgId = newProgId
                            fltLookupBaseDB.Insert(thisLookupBase)
                        Next
                    Next
                Else
                    Throw New Exception("Can't copy - user is not authorized")
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Function CanView(ByVal FltIdentity As FltIdentity) As Boolean
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Program", fltUserAction.CanView)
        End Function
		
        Public Shared Function CanEdit(ByVal FltIdentity As FltIdentity) As Boolean
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Program", fltUserAction.CanEdit)
        End Function
		
	End Class

End Namespace
