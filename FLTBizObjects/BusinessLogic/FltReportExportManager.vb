' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.

Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.Dal
Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Configuration

Namespace FLT.BL

    Public Class FltReportExportManager

#Region "Public Methods"

        Public Shared Function GetDailyUsage(ByVal ProgId As Integer, ByVal LocationId As Integer, ByVal FlightDate As Date, ByVal FltIdentity As FltIdentity) As System.IO.Stream
            Try
                If CanView(FltIdentity) Then
                    Return GetTabStream(FltReportExportDB.GetDailyUsage(ProgId, LocationId, FlightDate))
                Else
                    Throw New Exception("Can't View - user is not authorized")
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetPilotComments(ByVal ProgId As Integer, ByVal LocationId As Integer, ByVal AircraftId As Integer, ByVal DateFrom As Date, ByVal DateThru As Date, ByVal FltIdentity As FltIdentity) As System.IO.Stream
            Try
                If CanView(FltIdentity) Then
                    Return GetTabStream(FltReportExportDB.GetPilotComments(ProgId, LocationId, AircraftId, DateFrom, DateThru))
                Else
                    Throw New Exception("Can't View - user is not authorized")
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetFuelBurn(ByVal ProgId As Integer, ByVal AircraftId As Integer, ByVal DateFrom As Date, ByVal DateThru As Date, ByVal LiquidGroupId As Integer, ByVal FltIdentity As FltIdentity) As System.IO.Stream
            Try
                If CanView(FltIdentity) Then
                    Return GetTabStream(FltReportExportDB.GetFuelBurn(ProgId, AircraftId, DateFrom, DateThru, LiquidGroupId))
                Else
                    Throw New Exception("Can't View - user is not authorized")
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetAircraftComments(ByVal ProgId As Integer, ByVal LocationId As Integer, ByVal AircraftId As Integer, ByVal ComponentId As Integer, ByVal RatingLow As Int16, ByVal RatingHigh As Int16, ByVal DateFrom As Date, ByVal DateThru As Date, ByVal FltIdentity As FltIdentity) As System.IO.Stream
            Try
                If CanView(FltIdentity) Then
                    Return GetTabStream(FltReportExportDB.GetAircraftComments(ProgId, LocationId, AircraftId, ComponentId, RatingLow, RatingHigh, DateFrom, DateThru))
                Else
                    Throw New Exception("Can't View - user is not authorized")
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetAircraftData(ByVal ProgId As Integer, ByVal LocationId As Integer, ByVal AircraftId As Integer, ByVal FltIdentity As FltIdentity) As System.IO.Stream
            Try
                If CanView(FltIdentity) Then
                    Return GetTabStream(FltReportExportDB.GetAircraftData(ProgId, LocationId, AircraftId))
                Else
                    Throw New Exception("Can't View - user is not authorized")
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetFlightData(ByVal ProgId As Integer, ByVal LocationId As Integer, ByVal AircraftId As Integer, ByVal DateRangeStart As Date, ByVal DateRangeEnd As Date, ByVal FuelLiquidGroupId As Integer, ByVal OilLiquidGroupId As Integer, ByVal FluidLiquidGroupId As Integer, ByVal FltIdentity As FltIdentity) As System.IO.Stream
            Try
                If CanView(FltIdentity) Then
                    Return GetTabStream(FltReportExportDB.GetFlightData(ProgId, LocationId, AircraftId, DateRangeStart, DateRangeEnd, FuelLiquidGroupId, OilLiquidGroupId, FluidLiquidGroupId))
                Else
                    Throw New Exception("Can't View - user is not authorized")
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetFlightStatistics(ByVal ProgId As Integer, ByVal DateBegin As Date, ByVal DateEnd As Date, ByVal FltIdentity As FltIdentity) As System.IO.Stream
            Try
                If CanView(FltIdentity) Then
                    Return GetTabStream(FltReportExportDB.GetFlightStatistics(ProgId, DateBegin, DateEnd))
                Else
                    Throw New Exception("Can't View - user is not authorized")
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetFlightOutliers(ByVal ProgId As Integer, ByVal DateBegin As Date, ByVal DateEnd As Date, ByVal NbrOfDeviations As Integer, ByVal FltIdentity As FltIdentity) As System.IO.Stream
            Try
                If CanView(FltIdentity) Then
                    Return GetTabStream(FltReportExportDB.GetFlightOutliers(ProgId, DateBegin, DateEnd, NbrOfDeviations))
                Else
                    Throw New Exception("Can't View - user is not authorized")
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetFuelBurnByRefill(ByVal ProgId As Integer, ByVal AircraftId As Integer, ByVal DateFrom As Date, ByVal DateThru As Date, ByVal LiquidGroupId As Integer, ByVal FltIdentity As FltIdentity) As System.IO.Stream
            Try
                If CanView(FltIdentity) Then
                    Return GetTabStream(FltReportExportDB.GetFuelBurnByRefill(ProgId, AircraftId, DateFrom, DateThru, LiquidGroupId))
                Else
                    Throw New Exception("Can't View - user is not authorized")
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function CanView(ByVal FltIdentity As FltIdentity)
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Export", fltUserAction.CanView)
        End Function

#End Region

#Region "Private Procedures"

        Private Sub New()
        End Sub

        Private Shared Function GetTabStream(ByVal thisDataTable As DataTable) As System.IO.MemoryStream
            Dim thisMemoryStream As MemoryStream = Nothing
            Try
                Dim sb As New StringBuilder
                For i As Integer = 0 To thisDataTable.Columns.Count - 1
                    sb.Append(thisDataTable.Columns(i).ColumnName)
                    If i < thisDataTable.Columns.Count - 1 Then
                        sb.Append(vbTab)
                    End If
                Next
                sb.AppendLine()

                For Each thisDataRow As DataRow In thisDataTable.Rows
                    For i As Integer = 0 To thisDataTable.Columns.Count - 1
                        Dim thisType As System.Type = thisDataRow.Item(i).GetType()
                        sb.Append(FormatTabField(thisDataRow.Item(i)))
                        If i < thisDataTable.Columns.Count - 1 Then
                            sb.Append(vbTab)
                        End If
                    Next
                    sb.AppendLine()
                Next

                Dim buffer(sb.Length) As Byte
                buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString)

                thisMemoryStream = New MemoryStream(buffer)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
            Return thisMemoryStream
        End Function

        Private Shared Function FormatTabField(ByVal Item As Object) As String

            Dim outputField As String = String.Empty
            Try

                Select Case Item.GetType.Name
                    Case "Int32"
                        outputField = CInt(Item).ToString

                    Case "Decimal"
                        outputField = Format(CDec(Item), "#,##0.000")

                    Case "DateTime"
                        outputField = Format(CDate(Item), "yyyy-MM-dd hh:mm:ss tt")

                    Case "String"
                        outputField = CleanString(Item.ToString)

                    Case "DBNull"
                        outputField = String.Empty

                    Case "Boolean"
                        outputField = CBool(Item).ToString

                    Case Else
                        outputField = CleanString(Item.ToString)

                End Select

                If IsNumeric(outputField) Then
                    If outputField.Length > 18 Then
                        outputField = "'" & outputField
                    ElseIf CLng(outputField) > 999999999 Then
                        outputField = "'" & outputField
                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Return outputField

        End Function

        Private Shared Function CleanString(ByVal inputString As String) As String
            Dim sb As New StringBuilder

            For i As Integer = 0 To inputString.Length - 1
                If inputString.Substring(i, 1) >= " " Then
                    sb.Append(inputString.Substring(i, 1))
                End If
            Next

            Return sb.ToString
        End Function
#End Region

    End Class

End Namespace
