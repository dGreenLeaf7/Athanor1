' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports Contoso.Utilities
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

    Public Class FlightKeyInfoManager

        Public Shared Function GetItem(ByVal AircraftId As Integer, ByVal LocationId As Integer, ByVal FlightDateSort As String, ByVal WorkShiftId As Integer, ByVal FltIdentity As FltIdentity) As FlightKeyInfo
            Dim newFlightKeyInfo As New FlightKeyInfo
            If CanView(FltIdentity) Then
                Try

                    If LocationId = 0 Then
                        newFlightKeyInfo.ErrorsList.Add(New BrokenRule("LocationId", "Please choose a location."))
                    End If
                    If AircraftId = 0 Then
                        newFlightKeyInfo.ErrorsList.Add(New BrokenRule("AircraftId", "Please choose an aircraft."))
                    End If
                    If FlightDateSort = String.Empty Then
                        newFlightKeyInfo.ErrorsList.Add(New BrokenRule("FlightDate", "Please provide a flight date."))
                    Else
                        If Not IsDate(FlightDateSort) Then
                            newFlightKeyInfo.ErrorsList.Add(New BrokenRule("FlightDate", "Please provide a valid flight date."))
                        End If
                    End If
                    If WorkShiftId = 0 Then
                        newFlightKeyInfo.ErrorsList.Add(New BrokenRule("WorkShiftId", "Please choose a work shift."))
                    End If

                    If newFlightKeyInfo.ErrorsList.Count = 0 Then

                        newFlightKeyInfo = FlightKeyInfoDB.GetItem(AircraftId, LocationId, Format(CDate(FlightDateSort), "yyyy-MM-dd"), WorkShiftId)

                        If Not newFlightKeyInfo.ValidAircraft Then
                            newFlightKeyInfo.ErrorsList.Add(New BrokenRule("AircraftId", "Aircraft selected does not exist"))
                        End If
                        If Not newFlightKeyInfo.ValidLocation Then
                            newFlightKeyInfo.ErrorsList.Add(New BrokenRule("LocationId", "Location selected does not exist"))
                        End If
                        If Not newFlightKeyInfo.ValidAircraftLocation Then
                            newFlightKeyInfo.ErrorsList.Add(New BrokenRule("AircraftLocationId", "Aircraft is not assigned to selected Location"))
                        End If
                        If Not newFlightKeyInfo.ValidWorkShift Then
                            newFlightKeyInfo.ErrorsList.Add(New BrokenRule("WorkShiftId", "Work Shift selected does not exist"))
                        End If

                    End If

                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't fetch - user is not authorized")
            End If
            Return newFlightKeyInfo
        End Function

        Private Shared Function CanView(ByVal FltIdentity As FltIdentity) As Boolean
            Try
                Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Fleet", fltUserAction.CanView)
            Catch ex As Exception
                Logger.Write(ex)
                Throw New Exception(ex.Message)
            End Try
        End Function

    End Class

End Namespace
