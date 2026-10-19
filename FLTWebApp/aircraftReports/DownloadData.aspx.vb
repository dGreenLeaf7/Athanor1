' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports System.Configuration

Partial Public Class DownloadData
    Inherits System.Web.UI.Page
#Region "Private Variables"

    Private _fltIdentity As FltIdentity = Nothing

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Server.ScriptTimeout = 600
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/NotAuthorized.aspx")
        End If
        GenerateDownloadFile()
    End Sub

    Private Sub GenerateDownloadFile()
        Dim reportNumber As String = Request.QueryString("rn")
        Try
            Select Case reportNumber
                Case "1"
                    DownloadDailyUsage(GetReportName(reportNumber))
                Case "2"
                    DownloadFuelBurn(GetReportName(reportNumber))
                Case "3"
                    DownloadPilotComments(GetReportName(reportNumber))
                Case "4", "5"
                    DownloadAircraftComments(GetReportName(reportNumber))
                Case "6"
                    DownloadAircraftData(GetReportName(reportNumber))
                Case "7"
                    DownloadFlightData(GetReportName(reportNumber))
                Case "8"
                    DownloadFlightStatistics(GetReportName(reportNumber))
                Case "9"
                    DownloadFlightOutliers(GetReportName(reportNumber))
                Case "10"
                    DownloadFuelBurnByRefill(GetReportName(reportNumber))
            End Select
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Private Sub DownloadDailyUsage(ByVal reportname As String)

        Dim progId As Integer = _fltIdentity.CurrentProgId
        Dim locationId As Integer = CInt(Request.QueryString("loc"))
        Dim flightDate As Date = CDate(Request.QueryString("td"))

        Try
            Using ms As System.IO.MemoryStream = FltReportExportManager.GetDailyUsage(progId, locationId, flightDate, _fltIdentity)
                DownloadData(ms, reportname)
            End Using

            Response.Flush()
            Response.End()

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Private Sub DownloadPilotComments(ByVal reportname As String)

        Dim progId As Integer = _fltIdentity.CurrentProgId
        Dim locationId As Integer = CInt(Request.QueryString("loc"))
        Dim aircraftId As Integer = CInt(Request.QueryString("aid"))
        Dim dateFrom As Date = CDate(Request.QueryString("df"))
        Dim dateThru As Date = CDate(Request.QueryString("dt"))

        Try
            Using ms As System.IO.MemoryStream = FltReportExportManager.GetPilotComments(progId, locationId, aircraftId, dateFrom, dateThru, _fltIdentity)
                DownloadData(ms, reportname)
            End Using

            Response.Flush()
            Response.End()

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Private Sub DownloadFuelBurn(ByVal reportname As String)

        Dim progId As Integer = _fltIdentity.CurrentProgId
        Dim aircraftId As Integer = CInt(Request.QueryString("aid"))
        Dim dateFrom As Date = CDate(Request.QueryString("df"))
        Dim dateThru As Date = CDate(Request.QueryString("dt"))
        Dim liquidGroupId As Integer = CInt(Request.QueryString("lgid"))

        Try
            Using ms As System.IO.MemoryStream = FltReportExportManager.GetFuelBurn(progId, aircraftId, dateFrom, dateThru, liquidGroupId, _fltIdentity)
                DownloadData(ms, reportname)
            End Using

            Response.Flush()
            Response.End()

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Private Sub DownloadAircraftComments(ByVal reportname As String)

        Dim progId As Integer = _fltIdentity.CurrentProgId
        Dim locationId As Integer = CInt(Request.QueryString("loc"))
        Dim aircraftId As Integer = CInt(Request.QueryString("aid"))
        Dim componentId As Integer = CInt(Request.QueryString("cid"))
        Dim ratingLow As Int16 = CType(Request.QueryString("rl"), Int16)
        Dim ratingHigh As Int16 = CType(Request.QueryString("rh"), Int16)
        Dim dateFrom As Date = CDate(Request.QueryString("df"))
        Dim dateThru As Date = CDate(Request.QueryString("dt"))

        Try
            Using ms As System.IO.MemoryStream = FltReportExportManager.GetAircraftComments(progId, locationId, aircraftId, componentId, ratingLow, ratingHigh, dateFrom, dateThru, _fltIdentity)
                DownloadData(ms, reportname)
            End Using

            Response.Flush()
            Response.End()

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Private Sub DownloadAircraftData(ByVal reportname As String)

        Dim progId As Integer = _fltIdentity.CurrentProgId
        Dim locationId As Integer = CInt(Request.QueryString("loc"))
        Dim aircraftId As Integer = CInt(Request.QueryString("aid"))

        Try

            Using ms As System.IO.MemoryStream = FltReportExportManager.GetAircraftData(progId, locationId, aircraftId, _fltIdentity)
                DownloadData(ms, reportname)
            End Using

            Response.Flush()
            Response.End()

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Private Sub DownloadFlightData(ByVal reportName As String)

        Dim progId As Integer = _fltIdentity.CurrentProgId
        Dim reportNumber As String = Request.QueryString("rn")
        Dim locationId As Integer = CInt(Request.QueryString("loc"))
        Dim aircraftId As Integer = CInt(Request.QueryString("aid"))
        Dim dateRangeFrom As Date = CDate(Request.QueryString("df"))
        Dim dateRangeThru As Date = CDate(Request.QueryString("dt"))
        Dim fuelLiquidGroupId As Integer = CInt(Request.QueryString("fuid"))
        Dim oilLiquidGroupId As Integer = CInt(Request.QueryString("oiid"))
        Dim fluidLiquidGroupId As Integer = CInt(Request.QueryString("flid"))

        Try

            Using ms As System.IO.MemoryStream = FltReportExportManager.GetFlightData(progId, locationId, aircraftId, dateRangeFrom, dateRangeThru, fuelLiquidGroupId, oilLiquidGroupId, fluidLiquidGroupId, _fltIdentity)
                DownloadData(ms, reportName)
            End Using

            Response.Flush()
            Response.End()

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Private Sub DownloadFlightStatistics(ByVal reportName As String)

        Dim progId As Integer = _fltIdentity.CurrentProgId
        Dim reportNumber As String = Request.QueryString("rn")
        Dim dateRangeFrom As Date = CDate(Request.QueryString("df"))
        Dim dateRangeThru As Date = CDate(Request.QueryString("dt"))

        Try
            Using ms As System.IO.MemoryStream = FltReportExportManager.GetFlightStatistics(progId, dateRangeFrom, dateRangeThru, _fltIdentity)
                DownloadData(ms, reportName)
            End Using

            Response.Flush()
            Response.End()

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Private Sub DownloadFlightOutliers(ByVal reportName As String)

        Dim progId As Integer = _fltIdentity.CurrentProgId
        Dim reportNumber As String = Request.QueryString("rn")
        Dim dateRangeFrom As Date = CDate(Request.QueryString("df"))
        Dim dateRangeThru As Date = CDate(Request.QueryString("dt"))
        Dim NbrOfStDevs As Integer = CInt(Request.QueryString("stdev"))

        Try
            Using ms As System.IO.MemoryStream = FltReportExportManager.GetFlightOutliers(progId, dateRangeFrom, dateRangeThru, NbrOfStDevs, _fltIdentity)
                DownloadData(ms, reportName)
            End Using

            Response.Flush()
            Response.End()

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Private Sub DownloadFuelBurnByRefill(ByVal reportname As String)

        Dim progId As Integer = _fltIdentity.CurrentProgId
        Dim aircraftId As Integer = CInt(Request.QueryString("aid"))
        Dim dateFrom As Date = CDate(Request.QueryString("df"))
        Dim dateThru As Date = CDate(Request.QueryString("dt"))
        Dim liquidGroupId As Integer = CInt(Request.QueryString("lgid"))

        Try
            Using ms As System.IO.MemoryStream = FltReportExportManager.GetFuelBurnByRefill(progId, aircraftId, dateFrom, dateThru, liquidGroupId, _fltIdentity)
                DownloadData(ms, reportname)
            End Using

            Response.Flush()
            Response.End()

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Private Sub DownloadData(ByVal ms As System.IO.MemoryStream, ByVal ReportName As String)

        Dim bufferSize As Integer = CInt(ConfigurationManager.AppSettings.Get("FileBufferSize"))
        Dim outputBuffer(bufferSize - 1) As Byte
        Dim index As Integer = 0

        Using bReader As New System.IO.BinaryReader(ms)

            Response.ContentType = "application\octet"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & ReportName & ".txt")

            Dim retVal As Integer

            Do While True
                retVal = bReader.Read(outputBuffer, index, bufferSize)
                If retVal = 0 Then
                    Exit Do
                End If
                If retVal < bufferSize Then
                    ReDim Preserve outputBuffer(retVal)
                    Response.BinaryWrite(outputBuffer)
                    Exit Do
                End If
                Response.BinaryWrite(outputBuffer)
            Loop

        End Using

    End Sub

    Private Function GetReportName(ByVal ReportNumber As String) As String
        Dim reportName As String = String.Empty
        Select Case reportNumber
            Case "1"
                reportName = "DailyUsage"
            Case "2"
                reportName = "FuelBurnAndAccumHours"
            Case "3"
                reportName = "PilotComments"
            Case "4"
                reportName = "AircraftCommentsByDateAndAircraft"
            Case "5"
                reportName = "AircraftCommentsByAircraftAndDate"
            Case "6"
                reportName = "AircraftData"
            Case "7"
                reportName = "FlightData"
            Case "8"
                reportName = "FlightStatistics"
            Case "9"
                reportName = "FlightOutliers"
            Case "10"
                reportName = "DailyFlightsAndFuelBurn"
        End Select
        Return reportName
    End Function


    Private Sub ClearErrorFields()
        Me.ErrorMessageLabel.Text = String.Empty
        Me.ErrorMessageLabel.Visible = False
        Me.SuccessMessageLabel.Text = String.Empty
        Me.SuccessMessageLabel.Visible = False
        Me.ErrorsBulletList.Items.Clear()
        Me.ErrorsBulletList.Visible = False
        Me.WarningMessageLabel.Text = String.Empty
        Me.WarningMessageLabel.Visible = False
        Me.WarningsCheckList.Items.Clear()
        Me.WarningsCheckList.Visible = False
    End Sub

    Private Sub ProcessException(ByVal ex As Exception)
        ProcessException(ex, New BrokenRulesList, New BrokenRulesList)
    End Sub

    Private Sub ProcessException(ByVal ex As Exception, ByVal BrokenRulesList As BrokenRulesList)
        ProcessException(ex, BrokenRulesList, New BrokenRulesList)
    End Sub

    Private Sub ProcessException(ByVal ex As Exception, ByVal ErrorsList As BrokenRulesList, ByVal WarningsList As BrokenRulesList)
        Me.ErrorMessageLabel.Text = ex.Message
        Me.ErrorMessageLabel.Visible = True
        If ErrorsList.Count <> 0 Then
            Me.ErrorsBulletList.Visible = True
            Me.ErrorsBulletList.DataSource = ErrorsList
            Me.ErrorsBulletList.DataBind()
        End If
        If WarningsList.Count <> 0 Then
            Me.WarningMessageLabel.Text = "Either correct the errors listed below or place a checkmark next to those errors that can be ignored."
            Me.WarningMessageLabel.Visible = True
            Me.WarningsCheckList.Visible = True
            For Each thisWarning As BrokenRule In WarningsList
                Dim listItem As New ListItem(thisWarning.Message, thisWarning.RuleIdentifier)
                If thisWarning.OverRideRule Then
                    listItem.Selected = True
                End If
                Me.WarningsCheckList.Items.Add(listItem)
            Next
        End If
    End Sub

End Class