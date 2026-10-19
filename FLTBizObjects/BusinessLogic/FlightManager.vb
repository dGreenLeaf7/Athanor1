' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports System.Data.SqlTypes
Imports System.Text.RegularExpressions
Imports Contoso.Utilities
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class FlightManager

#Region "Parent Business Object Methods"

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetItem(ByVal FlightId As Integer, ByVal FltIdentity As FltIdentity) As Flight
            Dim newFlight As Flight = Nothing
            If CanView(FltIdentity) Then
                Try
                    newFlight = FlightDB.GetItem(FlightId)
                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't fetch - user is not authorized")
            End If
            Return newFlight
        End Function

        Public Shared Sub Save(ByVal Flight As Flight, ByVal FltIdentity As FltIdentity)
            Save(Flight, FltIdentity, True)
        End Sub

        Public Shared Sub Save(ByVal Flight As Flight, ByVal FltIdentity As FltIdentity, ByVal EnforceValidation As Boolean)
            If ((Flight.ConcurrencyId = Nothing) Or (Flight.ConcurrencyId = String.Empty) Or (Flight.ConcurrencyId = "0")) Then
                Insert(Flight, FltIdentity, EnforceValidation)
            Else
                Update(Flight, FltIdentity, EnforceValidation)
            End If
        End Sub

        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal Flight As Flight, ByVal FltIdentity As FltIdentity)
            Update(Flight, FltIdentity, True)
        End Sub

        Public Shared Sub Update(ByVal Flight As Flight, ByVal FltIdentity As FltIdentity, ByVal EnforceValidation As Boolean)
            If CanEdit(FltIdentity) Then
                If Flight.Validate(EnforceValidation) Then
                    Try
                        Dim flightLocation As Location = LocationManager.GetItem(Flight.LocationId, FltIdentity)

                        DateTimeAdjustFlightObject(Flight, flightLocation)

                        Flight.AirframeHoursStart = Format(CDec(Flight.HobbsStart) + CDec(Flight.UsedMeterOffset), "#,##0.0")

                        If EnforceValidation Then
                            If Flight.IsApproved = False Then
                                Flight.IsApproved = True
                                Flight.ApprovedBy = FltIdentity.UserName
                                Flight.UtcDateApproved = Date.UtcNow()
                            End If
                        End If

                        Flight.StampUserName = FltIdentity.UserName

                        DateTimeAdjustFlightCommentList(Flight, flightLocation)

                        FlightDB.Update(Flight)

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
        Public Shared Function Insert(ByVal Flight As Flight, ByVal FltIdentity As FltIdentity) As Integer
            Return Insert(Flight, FltIdentity, True)
        End Function

        Public Shared Function Insert(ByVal Flight As Flight, ByVal FltIdentity As FltIdentity, ByVal EnforceValidation As Boolean) As Integer

            Dim newID As Integer = 0

            If CanEdit(FltIdentity) Then

                If Flight.Validate(EnforceValidation) Then

                    Try
                        Dim flightLocation As Location = LocationManager.GetItem(Flight.LocationId, FltIdentity)

                        DateTimeAdjustFlightObject(Flight, flightLocation)

                        Flight.AirframeHoursStart = Format(CDec(Flight.HobbsStart) + CDec(Flight.UsedMeterOffset), "#,##0.0")

                        If EnforceValidation Then
                            If Flight.IsApproved = False Then
                                Flight.IsApproved = True
                                Flight.ApprovedBy = FltIdentity.UserName
                                Flight.UtcDateApproved = Date.UtcNow()
                            End If
                        End If

                        Flight.StampUserName = FltIdentity.UserName
                        DateTimeAdjustFlightCommentList(Flight, flightLocation)
                        DateTimeAdjustFlightEngineRunList(Flight, flightLocation)

                        newID = FlightDB.Insert(Flight)

                        Flight.FlightId = newID

                    Catch ex As ApplicationException
                        Throw New ApplicationException(ex.Message)

                    Catch ex As Exception
                        Logger.Write(ex)
                        Throw New ApplicationException("An application exception occurred")

                    End Try

                Else
                    Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
                End If
            Else
                Throw New Exception("Can't insert - user is not authorized")
            End If
            Return newID

        End Function

        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal Flight As Flight, ByVal FltIdentity As FltIdentity)

            If CanEdit(FltIdentity) Then

                Try
                
                    FlightDB.Delete(Flight.FlightId, FltIdentity.UserName)

                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)

                End Try
                
            Else
                Throw New Exception("Can't delete - user is not authorized")
            End If
        End Sub

#End Region

#Region "Child Business Object Methods"

        Public Shared Function InsertLiquidFill(ByVal Flight As Flight, ByVal LiquidFill As LiquidFill) As Integer
            Dim newId As Integer = 0
            If LiquidFill.Validate() Then
                Try
                    LiquidFill.LiquidFillId = Flight.NewLiquidFillId
                    Flight.NewLiquidFillId = Flight.NewLiquidFillId - 1
                    Flight.LiquidFillList.Add(LiquidFill)
                Catch ex As Exception
                    Logger.Write(ex)
                    Throw New Exception(ex.Message)
                End Try
            Else
                Flight.BrokenRules.Clear()
                For Each thisRule As BrokenRule In LiquidFill.BrokenRules
                    Flight.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't insert - record is invalid. Please correct errors and try again.")
            End If
            Return newId
        End Function

        Public Shared Sub RemoveLiquidFill(ByVal Flight As Flight, ByVal LiquidFillId As Integer)
            Try
                For Each LiquidFill As LiquidFill In Flight.LiquidFillList
                    If LiquidFill.LiquidFillId = LiquidFillId Then
                        Flight.LiquidFillList.Remove(LiquidFill)
                        If LiquidFillId > 0 Then
                            Flight.LiquidFillDeleteList.Add(LiquidFill)
                        End If
                        Exit Sub
                    End If
                Next
                Throw New Exception("Can't remove - record not found in collection!")
            Catch ex As Exception
                Logger.Write(ex)
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub UpdateLiquidFill(ByVal Flight As Flight, ByVal LiquidFill As LiquidFill)
            If LiquidFill.Validate() Then
                Try
                    For Each thisLiquidFill As LiquidFill In Flight.LiquidFillList
                        If LiquidFill.LiquidFillId = thisLiquidFill.LiquidFillId Then
                            LiquidFill.ConcurrencyId = thisLiquidFill.ConcurrencyId
                            Flight.LiquidFillList.Remove(thisLiquidFill)
                            Flight.LiquidFillList.Add(LiquidFill)
                            Exit Sub
                        End If
                    Next
                Catch ex As Exception
                    Logger.Write(ex)
                    Throw New Exception(ex.Message)
                End Try
            Else
                Flight.BrokenRules.Clear()
                For Each thisRule As BrokenRule In LiquidFill.BrokenRules
                    Flight.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Function InsertFlightComment(ByVal Flight As Flight, ByVal FlightComment As FlightComment) As Integer
            Dim newId As Integer = 0
            If FlightComment.Validate() Then
                Try
                    FlightComment.FlightCommentId = Flight.NewFlightCommentId
                    Flight.NewFlightCommentId = Flight.NewFlightCommentId - 1
                    Flight.FlightCommentList.Add(FlightComment)
                    newId = FlightComment.FlightCommentId
                Catch ex As Exception
                    Logger.Write(ex)
                    Throw New Exception(ex.Message)
                End Try
            Else
                Flight.BrokenRules.Clear()
                For Each thisRule As BrokenRule In FlightComment.BrokenRules
                    Flight.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't insert - record is invalid. Please correct errors and try again.")
            End If
            Return newId
        End Function

        Public Shared Sub RemoveFlightComment(ByVal Flight As Flight, ByVal FlightCommentId As Integer)
            Try
                For Each FlightComment As FlightComment In Flight.FlightCommentList
                    If FlightComment.FlightCommentId = FlightCommentId Then
                        Flight.FlightCommentList.Remove(FlightComment)
                        If FlightCommentId > 0 Then
                            Flight.FlightCommentDeleteList.Add(FlightComment)
                        End If
                        Exit Sub
                    End If
                Next
                Throw New Exception("Can't remove - record not found in collection")
            Catch ex As Exception
                Logger.Write(ex)
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub UpdateFlightComment(ByVal Flight As Flight, ByVal FlightComment As FlightComment)
            If FlightComment.Validate() Then
                Try
                    For Each thisFlightComment As FlightComment In Flight.FlightCommentList
                        If FlightComment.FlightCommentId = thisFlightComment.FlightCommentId Then
                            FlightComment.ConcurrencyId = thisFlightComment.ConcurrencyId
                            Flight.FlightCommentList.Remove(thisFlightComment)
                            Flight.FlightCommentList.Add(FlightComment)
                            Exit Sub
                        End If
                    Next
                Catch ex As Exception
                    Logger.Write(ex)
                    Throw New Exception(ex.Message)
                End Try
            Else
                Flight.BrokenRules.Clear()
                For Each thisRule As BrokenRule In FlightComment.BrokenRules
                    Flight.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Function InsertFlightEngineRun(ByVal Flight As Flight, ByVal FlightEngineRun As FlightEngineRun) As Integer
            Dim newId As Integer = 0
            If FlightEngineRun.Validate() Then
                Try
                    FlightEngineRun.FlightEngineRunId = Flight.NewFlightEngineRunId
                    Flight.NewFlightEngineRunId = Flight.NewFlightEngineRunId - 1
                    Flight.FlightEngineRunList.Add(FlightEngineRun)
                    newId = FlightEngineRun.FlightEngineRunId
                Catch ex As Exception
                    Logger.Write(ex)
                    Throw New Exception(ex.Message)
                End Try
            Else
                Flight.BrokenRules.Clear()
                For Each thisRule As BrokenRule In FlightEngineRun.BrokenRules
                    Flight.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't insert - record is invalid. Please correct errors and try again.")
            End If
            Return newId
        End Function

        Public Shared Sub RemoveFlightEngineRun(ByVal Flight As Flight, ByVal FlightEngineRunId As Integer)
            Try
                For Each FlightEngineRun As FlightEngineRun In Flight.FlightEngineRunList
                    If FlightEngineRun.FlightEngineRunId = FlightEngineRunId Then
                        Flight.FlightEngineRunList.Remove(FlightEngineRun)
                        If FlightEngineRunId > 0 Then
                            Flight.FlightEngineRunDeleteList.Add(FlightEngineRun)
                        End If
                        Exit Sub
                    End If
                Next
                Throw New Exception("Can't remove - record not found in collection")
            Catch ex As Exception
                Logger.Write(ex)
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub UpdateFlightEngineRun(ByVal Flight As Flight, ByVal FlightEngineRun As FlightEngineRun)
            If FlightEngineRun.Validate() Then
                Try
                    For Each thisFlightEngineRun As FlightEngineRun In Flight.FlightEngineRunList
                        If FlightEngineRun.FlightEngineRunId = thisFlightEngineRun.FlightEngineRunId Then
                            FlightEngineRun.ConcurrencyId = thisFlightEngineRun.ConcurrencyId
                            Flight.FlightEngineRunList.Remove(thisFlightEngineRun)
                            Flight.FlightEngineRunList.Add(FlightEngineRun)
                            Exit Sub
                        End If
                    Next
                Catch ex As Exception
                    Logger.Write(ex)
                    Throw New Exception(ex.Message)
                End Try
            Else
                Flight.BrokenRules.Clear()
                For Each thisRule As BrokenRule In FlightEngineRun.BrokenRules
                    Flight.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Function InsertFlightRegionTime(ByVal Flight As Flight, ByVal FlightRegionTime As FlightRegionTime) As Integer
            Dim newId As Integer = 0
            If FlightRegionTime.Validate() Then
                Try
                    FlightRegionTime.FlightRegionTimeId = Flight.NewFlightRegionTimeId
                    Flight.NewFlightRegionTimeId = Flight.NewFlightRegionTimeId - 1
                    Flight.FlightRegionTimeList.Add(FlightRegionTime)
                    newId = FlightRegionTime.FlightRegionTimeId
                Catch ex As Exception
                    Logger.Write(ex)
                    Throw New Exception(ex.Message)
                End Try
            Else
                Flight.BrokenRules.Clear()
                For Each thisRule As BrokenRule In FlightRegionTime.BrokenRules
                    Flight.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't insert - record is invalid. Please correct errors and try again.")
            End If
            Return newId
        End Function

        Public Shared Sub RemoveFlightRegionTime(ByVal Flight As Flight, ByVal FlightRegionTimeId As Integer)
            Try
                For Each FlightRegionTime As FlightRegionTime In Flight.FlightRegionTimeList
                    If FlightRegionTime.FlightRegionTimeId = FlightRegionTimeId Then
                        Flight.FlightRegionTimeList.Remove(FlightRegionTime)
                        If FlightRegionTimeId > 0 Then
                            Flight.FlightRegionTimeDeleteList.Add(FlightRegionTime)
                        End If
                        Exit Sub
                    End If
                Next
                Throw New Exception("Can't remove - record not found in collection")
            Catch ex As Exception
                Logger.Write(ex)
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub UpdateFlightRegionTime(ByVal Flight As Flight, ByVal FlightRegionTime As FlightRegionTime)
            If FlightRegionTime.Validate() Then
                Try
                    For Each thisFlightRegionTime As FlightRegionTime In Flight.FlightRegionTimeList
                        If FlightRegionTime.FlightRegionTimeId = thisFlightRegionTime.FlightRegionTimeId Then
                            FlightRegionTime.ConcurrencyId = thisFlightRegionTime.ConcurrencyId
                            Flight.FlightRegionTimeList.Remove(thisFlightRegionTime)
                            Flight.FlightRegionTimeList.Add(FlightRegionTime)
                            Exit Sub
                        End If
                    Next
                Catch ex As Exception
                    Logger.Write(ex)
                    Throw New Exception(ex.Message)
                End Try
            Else
                Flight.BrokenRules.Clear()
                For Each thisRule As BrokenRule In FlightRegionTime.BrokenRules
                    Flight.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub

#End Region

#Region "Business Object Authorization"

        Public Shared Function CanView(ByVal FltIdentity As FltIdentity) As Boolean
            Try
                Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Fleet", fltUserAction.CanView)
            Catch ex As Exception
                Logger.Write(ex)
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function CanEdit(ByVal FltIdentity As FltIdentity) As Boolean
            Try
                Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Fleet", fltUserAction.CanEdit)
            Catch ex As Exception
                Logger.Write(ex)
                Throw New Exception(ex.Message)
            End Try
        End Function

#End Region


#Region "Date Adjusting Procedures "

        Private Shared Sub DateTimeAdjustFlightCommentList(ByVal Flight As Flight, ByVal FlightLocation As Location)
            Try
                For Each thisFlightComment As FlightComment In Flight.FlightCommentList
                    DateTimeAdjustFlightCommentObject(thisFlightComment, Flight, FlightLocation)
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub DateTimeAdjustFlightEngineRunList(ByVal Flight As Flight, ByVal FlightLocation As Location)
            Try
                For Each thisFlightEngineRun As FlightEngineRun In Flight.FlightEngineRunList
                    DateTimeAdjustFlightEngineRunObject(thisFlightEngineRun, Flight, FlightLocation)
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub DateTimeAdjustFlightObject(ByVal Flight As Flight, ByVal FlightLocation As Location)
            Flight.DepartureDateTime_Local = ConstructDateTime(Flight.FlightDate, Flight.DepartureTime)
            Flight.DepartureDateTime_UTC = ComputeUTC(Flight.DepartureDateTime_Local, FlightLocation)
            Flight.ReturnDateTime_Local = ConstructDateTime(Flight.FlightDate, Flight.ReturnTime)
            Flight.ReturnDateTime_UTC = ComputeUTC(Flight.ReturnDateTime_Local, FlightLocation)
            If (Flight.ReturnDateTime_Local.Hour < Flight.DepartureDateTime_Local.Hour) Then
                Flight.ReturnDateTime_Local = Flight.ReturnDateTime_Local.AddDays(1)
                Flight.ReturnDateTime_UTC = Flight.ReturnDateTime_UTC.AddDays(1)
            End If
        End Sub

        Private Shared Sub DateTimeAdjustFlightCommentObject(ByVal FlightComment As FlightComment, ByVal Flight As Flight, ByVal FlightLocation As Location)
            If FlightComment.CommentTime.Length = 0 Then
                FlightComment.CommentDateTime_Local = ConstructDateTime(Flight.DepartureDateTime_Local.ToString, Flight.DepartureTime)
            Else
                FlightComment.CommentDateTime_Local = ConstructDateTime(Flight.DepartureDateTime_Local.ToString, FlightComment.CommentTime)
            End If
            FlightComment.CommentDateTime_UTC = ComputeUTC(FlightComment.CommentDateTime_Local, FlightLocation)
            If Flight.DepartureDateTime_Local.Hour >= 16 Then
                If FlightComment.CommentDateTime_Local.Hour < (Flight.DepartureDateTime_Local.Hour - 1) Then
                    FlightComment.CommentDateTime_Local = FlightComment.CommentDateTime_Local.AddDays(1)
                    FlightComment.CommentDateTime_UTC = FlightComment.CommentDateTime_UTC.AddDays(1)
                End If
            End If
        End Sub

        Private Shared Sub DateTimeAdjustFlightEngineRunObject(ByVal FlightEngineRun As FlightEngineRun, ByVal Flight As Flight, ByVal FlightLocation As Location)
            FlightEngineRun.RunStartDateTime_Local = ConstructDateTime(Flight.DepartureDateTime_Local.ToString, FlightEngineRun.RunStartTime)
            FlightEngineRun.RunEndDateTime_Local = ConstructDateTime(Flight.DepartureDateTime_Local.ToString, FlightEngineRun.RunEndTime)
            FlightEngineRun.RunStartDateTime_UTC = ComputeUTC(FlightEngineRun.RunStartDateTime_Local, FlightLocation)
            FlightEngineRun.RunEndDateTime_UTC = ComputeUTC(FlightEngineRun.RunEndDateTime_Local, FlightLocation)
            If Flight.DepartureDateTime_Local.Hour >= 16 Then
                If FlightEngineRun.RunStartDateTime_Local.Hour < (Flight.DepartureDateTime_Local.Hour - 1) Then
                    FlightEngineRun.RunStartDateTime_Local = FlightEngineRun.RunStartDateTime_Local.AddDays(1)
                    FlightEngineRun.RunStartDateTime_UTC = FlightEngineRun.RunStartDateTime_UTC.AddDays(1)
                    FlightEngineRun.RunEndDateTime_Local = FlightEngineRun.RunEndDateTime_Local.AddDays(1)
                    FlightEngineRun.RunEndDateTime_UTC = FlightEngineRun.RunEndDateTime_UTC.AddDays(1)
                End If
            End If
        End Sub

        Private Shared Function ConstructDateTime(ByVal DateValueString As String, ByVal TimeValueString As String) As DateTime

            Const TimeRegex As String = "^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]?(\s)?(AM|am|Am|aM|PM|pm|Pm|pM))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]))$"

            Dim newDateTime As DateTime = Nothing

            If IsDate(DateValueString) And Regex.IsMatch(TimeValueString, TimeRegex) Then
                newDateTime = CDate(Format(CDate(DateValueString), "yyyy-MM-dd") & " " & TimeValueString)
            End If

            Return newDateTime

        End Function

        Private Shared Function ComputeUTC(ByVal thisLocalDate As DateTime, ByVal thisFlightLocation As Location) As DateTime

            Dim utcOffset As Double = thisFlightLocation.TimeZoneOffset
            Dim utcDateTime As DateTime = Nothing

            If thisFlightLocation.DaylightSavingTime Then
                If ((DateTime.Compare(thisLocalDate, CDate(thisFlightLocation.DaylightSavingBegin)) >= 0) And (DateTime.Compare(thisLocalDate, CDate(thisFlightLocation.DaylightSavingEnd)) <= 0)) Then
                    utcOffset = thisFlightLocation.TimeZoneOffset + thisFlightLocation.DaylightSavingOffset
                End If
            End If

            utcDateTime = thisLocalDate.AddHours(-1.0 * utcOffset)

            Return utcDateTime

        End Function

#End Region

    End Class

End Namespace
