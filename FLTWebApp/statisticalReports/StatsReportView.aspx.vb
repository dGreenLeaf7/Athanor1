' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Configuration
Imports System.Collections

Partial Public Class StatsReportView
    Inherits System.Web.UI.Page

#Region "Private Variables"

    Private _fltIdentity As FltIdentity = Nothing
    Private _reportDocument As ReportDocument = Nothing
    Private _connectionInfo As ConnectionInfo = Nothing
    Private _reportPath As String = String.Empty
    Private _reportName As String = String.Empty
    Private _reportNbr As String = String.Empty

#End Region

#Region "Private Procedures"

    Private Function GetConnectionInfo() As ConnectionInfo
        Dim connectionInfo As New ConnectionInfo
        With connectionInfo
            .ServerName = ConfigurationManager.AppSettings("ReportDBServerName").ToLower
            .DatabaseName = ConfigurationManager.AppSettings("ReportDBName").ToLower
            .UserID = ConfigurationManager.AppSettings("ReportUserId")
            .Password = ConfigurationManager.AppSettings("ReportPassword")
        End With
        Return connectionInfo
    End Function

    Private Function GetReportName(ByVal ReportNumber As String) As String
        Dim reportName As String = String.Empty
        Select Case ReportNumber
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
        End Select
        Return reportName
    End Function

    Private Sub ConfigureCrystalReports(ByVal ReportNbr As String)

        If Session(_reportName) Is Nothing Then
            _reportDocument = New ReportDocument()
            _reportDocument.Load(_reportPath)
            SetDBLogonForSubReports(_connectionInfo, _reportDocument)
            SetDBLogonForReport(_connectionInfo, _reportDocument)
        Else
            _reportDocument = CType(Session(_reportName), ReportDocument)
        End If

        Select Case ReportNbr
            Case "1"
                LoadDailyUsageReportParameters()
            Case "2"
                LoadFuelBurnAndAccumHoursReportParameters()
            Case "3"
                LoadPilotCommentsReportParameters()
            Case "4", "5"
                LoadAircraftCommentsReportParameters()
            Case "6"
                LoadAircraftDataReportParameters()
            Case "7"
                LoadFlightDataReportParameters()
        End Select

        Me.myCrystalReportViewer.ReportSource = _reportDocument
    End Sub

    Private Sub LoadDailyUsageReportParameters()

        Dim arrayList As New ArrayList

        arrayList.Clear()
        arrayList.Add(CInt(_fltIdentity.CurrentProgId))
        SetCurrentValuesForParameterField(_reportDocument, "@ProgId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("loc")))
        SetCurrentValuesForParameterField(_reportDocument, "@LocationId", arrayList)

        arrayList.Clear()
        arrayList.Add(CDate(Request.QueryString("td")))
        SetCurrentValuesForParameterField(_reportDocument, "@FlightDate", arrayList)

        arrayList.Clear()
        arrayList.Add(_fltIdentity.UserDisplayName)
        SetCurrentValuesForParameterField(_reportDocument, "UserName", arrayList)

    End Sub

    Private Sub LoadFuelBurnAndAccumHoursReportParameters()

        Dim arrayList As New ArrayList

        arrayList.Clear()
        arrayList.Add(CInt(_fltIdentity.CurrentProgId))
        SetCurrentValuesForParameterField(_reportDocument, "@ProgId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("aid")))
        SetCurrentValuesForParameterField(_reportDocument, "@AircraftId", arrayList)

        arrayList.Clear()
        arrayList.Add(CDate(Request.QueryString("df")))
        SetCurrentValuesForParameterField(_reportDocument, "@DateFrom", arrayList)

        arrayList.Clear()
        arrayList.Add(CDate(Request.QueryString("dt")))
        SetCurrentValuesForParameterField(_reportDocument, "@DateThru", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("lgid")))
        SetCurrentValuesForParameterField(_reportDocument, "@LiquidGroupId", arrayList)

        arrayList.Clear()
        arrayList.Add(_fltIdentity.UserDisplayName)
        SetCurrentValuesForParameterField(_reportDocument, "UserName", arrayList)

    End Sub

    Private Sub LoadPilotCommentsReportParameters()

        Dim arrayList As New ArrayList

        arrayList.Clear()
        arrayList.Add(CInt(_fltIdentity.CurrentProgId))
        SetCurrentValuesForParameterField(_reportDocument, "@ProgId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("loc")))
        SetCurrentValuesForParameterField(_reportDocument, "@LocationId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("aid")))
        SetCurrentValuesForParameterField(_reportDocument, "@AircraftId", arrayList)

        arrayList.Clear()
        arrayList.Add(CDate(Request.QueryString("df")))
        SetCurrentValuesForParameterField(_reportDocument, "@DateFrom", arrayList)

        arrayList.Clear()
        arrayList.Add(CDate(Request.QueryString("dt")))
        SetCurrentValuesForParameterField(_reportDocument, "@DateThru", arrayList)

        arrayList.Clear()
        arrayList.Add(_fltIdentity.UserDisplayName)
        SetCurrentValuesForParameterField(_reportDocument, "UserName", arrayList)

    End Sub

    Private Sub LoadAircraftCommentsReportParameters()

        Dim arrayList As New ArrayList

        arrayList.Clear()
        arrayList.Add(CInt(_fltIdentity.CurrentProgId))
        SetCurrentValuesForParameterField(_reportDocument, "@ProgId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("loc")))
        SetCurrentValuesForParameterField(_reportDocument, "@LocationId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("aid")))
        SetCurrentValuesForParameterField(_reportDocument, "@AircraftId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("cid")))
        SetCurrentValuesForParameterField(_reportDocument, "@ComponentId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("rf")))
        SetCurrentValuesForParameterField(_reportDocument, "@RatingLow", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("rt")))
        SetCurrentValuesForParameterField(_reportDocument, "@RatingHigh", arrayList)

        arrayList.Clear()
        arrayList.Add(CDate(Request.QueryString("df")))
        SetCurrentValuesForParameterField(_reportDocument, "@DateFrom", arrayList)

        arrayList.Clear()
        arrayList.Add(CDate(Request.QueryString("dt")))
        SetCurrentValuesForParameterField(_reportDocument, "@DateThru", arrayList)

        arrayList.Clear()
        arrayList.Add(_fltIdentity.UserDisplayName)
        SetCurrentValuesForParameterField(_reportDocument, "UserName", arrayList)

    End Sub

    Private Sub LoadAircraftDataReportParameters()

        Dim arrayList As New ArrayList

        arrayList.Clear()
        arrayList.Add(CInt(_fltIdentity.CurrentProgId))
        SetCurrentValuesForParameterField(_reportDocument, "@ProgId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("loc")))
        SetCurrentValuesForParameterField(_reportDocument, "@LocationId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("aid")))
        SetCurrentValuesForParameterField(_reportDocument, "@AircraftId", arrayList)

        arrayList.Clear()
        arrayList.Add(_fltIdentity.UserDisplayName)
        SetCurrentValuesForParameterField(_reportDocument, "UserName", arrayList)

    End Sub

    Private Sub LoadFlightDataReportParameters()

        Dim arrayList As New ArrayList

        arrayList.Clear()
        arrayList.Add(CInt(_fltIdentity.CurrentProgId))
        SetCurrentValuesForParameterField(_reportDocument, "@ProgId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("loc")))
        SetCurrentValuesForParameterField(_reportDocument, "@LocationId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("aid")))
        SetCurrentValuesForParameterField(_reportDocument, "@AircraftId", arrayList)

        arrayList.Clear()
        arrayList.Add(_fltIdentity.UserDisplayName)
        SetCurrentValuesForParameterField(_reportDocument, "UserName", arrayList)

        arrayList.Clear()
        arrayList.Add(CDate(Request.QueryString("df")))
        SetCurrentValuesForParameterField(_reportDocument, "@DateRangeStart", arrayList)

        arrayList.Clear()
        arrayList.Add(CDate(Request.QueryString("dt")))
        SetCurrentValuesForParameterField(_reportDocument, "@DateRangeEnd", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("lg1")))
        SetCurrentValuesForParameterField(_reportDocument, "@FuelLiquidGroupId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("lg2")))
        SetCurrentValuesForParameterField(_reportDocument, "@OilLiquidGroupId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Request.QueryString("lg3")))
        SetCurrentValuesForParameterField(_reportDocument, "@FluidsLiquidGroupId", arrayList)

    End Sub

#End Region

#Region "Protected Procedures"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        Page.Server.ScriptTimeout = 6000
        _connectionInfo = GetConnectionInfo()
        _reportNbr = CType(Request.QueryString("rn"), String)
        _reportName = GetReportName(_reportNbr)
        _reportPath = Server.MapPath(_reportName & ".rpt")
        ConfigureCrystalReports(_reportNbr)
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Session(_reportName) = _reportDocument
    End Sub

    Protected Sub BackButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BackButton.Click
        Dim parameterPageUrl As String = String.Empty
        Select Case CType(Request.QueryString("rn"), String)
            Case "1"
                parameterPageUrl = ".\DailyUsageParameters.aspx"
            Case "2"
                parameterPageUrl = ".\FuelBurnAndAccumHoursParameters.aspx"
            Case "3"
                parameterPageUrl = ".\PilotCommentsParameters.aspx"
            Case "4"
                parameterPageUrl = ".\AircraftCommentsParameters.aspx"
            Case "5"
                parameterPageUrl = ".\AircraftCommentsParameters.aspx"
            Case "6"
                parameterPageUrl = ".\AircraftDataParameters.aspx"
            Case "7"
                parameterPageUrl = ".\FlightDataParameters.aspx"
        End Select
        Response.Redirect(parameterPageUrl)
    End Sub

#End Region

#Region "Crystal Decisions Procedures"

    Private Sub SetDBLogonForReport(ByVal ConnectionInfo As ConnectionInfo, ByVal ReportDocument As ReportDocument)
        Dim tables As Tables = ReportDocument.Database.Tables
        For Each table As Table In tables
            Dim tableLogonInfo As New TableLogOnInfo
            tableLogonInfo.ConnectionInfo = ConnectionInfo
            table.ApplyLogOnInfo(tableLogonInfo)
            table.Location = ConnectionInfo.DatabaseName & ".dbo." & table.Location
        Next
    End Sub

    Private Sub SetDBLogonForSubReports(ByVal ConnectionInfo As ConnectionInfo, ByVal ReportDocument As ReportDocument)
        Dim sections As Sections = ReportDocument.ReportDefinition.Sections
        For Each section As Section In sections
            Dim reportObjects As ReportObjects = section.ReportObjects
            For Each reportObject As ReportObject In reportObjects
                If reportObject.Kind = ReportObjectKind.SubreportObject Then
                    Dim subreportObject As SubreportObject = CType(reportObject, SubreportObject)
                    Dim subReportDocument As ReportDocument = subreportObject.OpenSubreport(subreportObject.SubreportName)
                    SetDBLogonForReport(ConnectionInfo, subReportDocument)
                End If
            Next
        Next
    End Sub

    Private Sub SetCurrentValuesForParameterField(ByVal ReportDocument As ReportDocument, ByVal ParameterFieldName As String, ByVal ArrayList As ArrayList)
        Dim currentParameterValues As New ParameterValues
        For Each submittedValue As Object In ArrayList
            Dim parameterDiscreteValue As New ParameterDiscreteValue
            parameterDiscreteValue.Value = submittedValue
            currentParameterValues.Add(parameterDiscreteValue)
        Next
        Dim parameterFieldDefinitions As ParameterFieldDefinitions = ReportDocument.DataDefinition.ParameterFields
        Dim parameterFieldDefinition As ParameterFieldDefinition = parameterFieldDefinitions(ParameterFieldName)
        parameterFieldDefinition.ApplyCurrentValues(currentParameterValues)
    End Sub

    Private Function GetDefaultValuesFromParameterField(ByVal ReportDocument As ReportDocument, ByVal ParameterFieldName As String) As ArrayList
        Dim parameterFieldDefinitions As ParameterFieldDefinitions = ReportDocument.DataDefinition.ParameterFields
        Dim parameterFieldDefinition As ParameterFieldDefinition = parameterFieldDefinitions(ParameterFieldName)
        Dim defaultParameterValues As ParameterValues = parameterFieldDefinition.DefaultValues
        Dim arrayList As New ArrayList
        For Each parameterValue As ParameterValue In defaultParameterValues
            Dim parameterDiscreteValue As ParameterDiscreteValue = CType(parameterValue, ParameterDiscreteValue)
            arrayList.Add(parameterDiscreteValue.ToString)
        Next
        Return arrayList
    End Function

#End Region

End Class