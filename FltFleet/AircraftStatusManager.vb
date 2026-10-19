' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports System.Configuration
Imports FLTBizObjects
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal
Imports FLTBizObjects.FLT.BL

Public Class AircraftStatusManager

    <DataObjectMethod(DataObjectMethodType.Select, False)>
    Public Shared Function GetList(ByVal ProgId As Integer, ByVal FltIdentity As FLTBizObjects.FLT.BO.FltIdentity, ByVal SortingColumn As String, ByVal ShowOffTest As Boolean) As AircraftStatusList
        If CanView(FltIdentity) Then
            Return AircraftStatusDB.GetList(ProgId, SortingColumn, ShowOffTest)
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function CanView(ByVal FltIdentity As FltIdentity) As Boolean
        Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Fleet", fltUserAction.CanView)
    End Function

    Public Shared Function CanEdit(ByVal FltIdentity As FltIdentity) As Boolean

        Dim EditAllowed As Boolean = FltIdentityManager.AuthorizeUserAction(FltIdentity, "Fleet", fltUserAction.CanEdit)
        If EditAllowed = False Then Return False

        Return IsTimeOKForEdit(FltIdentity)

    End Function

    Public Shared Function IsTimeOKForEdit(ByVal FltIdentity As FltIdentity) As Boolean

        If FltIdentity.IsSuperUser Then
            Return True
        End If

        Dim TimeInPT As Date = GetCurrentTimeInPT()
        Dim CodedTimeInPT As Integer = TimeInPT.Hour * 100 + TimeInPT.Minute

        Dim MyRoleList As FltRoleList = FltUserInRoleDB.ListFltRolesForFltUser(FltIdentity.UserName)
        For Each MyRole As FltRole In MyRoleList
            Dim RoleNumber As Integer = MyRole.RoleId

            Dim StartHour As Integer, StartMinute As Integer, EndHour As Integer, EndMinute As Integer
            If GetEditTimeRangeForRole(RoleNumber, StartHour, StartMinute, EndHour, EndMinute) Then
                If CodedTimeInPT >= (StartHour * 100 + StartMinute) And _
                    CodedTimeInPT <= (EndHour * 100 + EndMinute) Then
                    Return True
                End If
            End If
        Next

        Return False

    End Function

    Public Shared Function GetEditTimeRangeForRole(ByVal RoleNumber As Integer, _
        ByRef StartHour As Integer, ByRef StartMinute As Integer, _
        ByRef EndHour As Integer, ByRef EndMinute As Integer) As Boolean

        Dim AppSettingValue As String = ConfigurationManager.AppSettings("ChangeTimeStartHourFor" + Convert.ToString(RoleNumber))
        If AppSettingValue = "" Then Return False
        StartHour = Convert.ToInt32(AppSettingValue)

        AppSettingValue = ConfigurationManager.AppSettings("ChangeTimeStartMinuteFor" + Convert.ToString(RoleNumber))
        If AppSettingValue = "" Then Return False
        StartMinute = Convert.ToInt32(AppSettingValue)

        AppSettingValue = ConfigurationManager.AppSettings("ChangeTimeStopHourFor" + Convert.ToString(RoleNumber))
        If AppSettingValue = "" Then Return False
        EndHour = Convert.ToInt32(AppSettingValue)

        AppSettingValue = ConfigurationManager.AppSettings("ChangeTimeStopMinuteFor" + Convert.ToString(RoleNumber))
        If AppSettingValue = "" Then Return False
        EndMinute = Convert.ToInt32(AppSettingValue)

        Return True

    End Function

    Private Shared Function GetCurrentTimeInPT() As Date

        Dim HoursToSubtract As Integer
        Dim CurrentTimeZoneName As String = LCase(TimeZone.CurrentTimeZone.StandardName)

        If CurrentTimeZoneName.Contains("eastern") Then HoursToSubtract = 3
        If CurrentTimeZoneName.Contains("central") Then HoursToSubtract = 2
        If CurrentTimeZoneName.Contains("mountain") Then HoursToSubtract = 1
        If CurrentTimeZoneName.Contains("central") Then HoursToSubtract = 0

        Return Date.Now.AddHours(0 - HoursToSubtract)

    End Function
End Class
