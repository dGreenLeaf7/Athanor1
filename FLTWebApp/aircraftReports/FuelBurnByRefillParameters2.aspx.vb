' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL
Imports System
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Configuration
Imports System.Collections
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Security.Application

Partial Public Class FuelBurnByRefillParameters2
    Inherits System.Web.UI.Page
    Private _reportName As String = String.Empty
    Private Const REPORT_DATA_OBJECT_NAME As String = "FuelBurnByRefillObjectList"

    Private _fltIdentity As FltIdentity = Nothing

    Private _reportDocument As ReportDocument = Nothing
    Private _arrayList As ArrayList = Nothing

    Private _connectionInfo As ConnectionInfo = Nothing
    Private _reportPath As String = String.Empty
    Private _reportNbr As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        Server.ScriptTimeout = 600
        ClearErrorFields()
        Me.ProgIdHiddenField.Value = _fltIdentity.CurrentProgId
        If Not Me.IsPostBack Then
            LoadFilterDDLs()
            Me.DateFromTB.Text = Format(Date.Today.Subtract(TimeSpan.FromDays(30)), "MM/dd/yyyy")
            Me.DateThruTB.Text = Format(Date.Today, "MM/dd/yyyy")
        Else
            If Me.ReportMultiView.ActiveViewIndex = 1 Then
                _reportName = "FuelEcononyByRefill2"
                _reportPath = Server.MapPath(_reportName & ".rpt")
                ConfigureCrystalReports()
            Else
                Session(_reportName) = Nothing
            End If

        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Session(_reportName) = _reportDocument
    End Sub

    Protected Sub LoadFilterDDLs()
        Try
            Me.AircraftDDL.Items.Clear()
            Me.AircraftDDL.Items.Add(New ListItem("< All Aircraft >", -1))
            For Each thisAircraft As AircraftInfo In AircraftInfoManager.GetList(Me.ProgIdHiddenField.Value, 0, String.Empty, _fltIdentity)
                Me.AircraftDDL.Items.Add(New ListItem(thisAircraft.MSN, thisAircraft.AircraftId))
            Next
            Me.AircraftDDL.SelectedValue = Me.AircraftDDL.Items(0).Value
            Me.LiquidDDL.Items.Clear()
            For Each thisLiquidGroup As fltLookupBase In fltLookupBaseManager.GetList("LiquidGroup", CInt(Me.ProgIdHiddenField.Value))
                Me.LiquidDDL.Items.Add(New ListItem(thisLiquidGroup.StringValue, thisLiquidGroup.ID))
            Next
            Me.LiquidDDL.SelectedValue = Me.AircraftDDL.Items(0).Value
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub ShowReportButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowReportButton.Click
        Try
            Session(REPORT_DATA_OBJECT_NAME) = Nothing
            Me.ReportMultiView.ActiveViewIndex = 1
            Page.Server.ScriptTimeout = 600
            _connectionInfo = GetConnectionInfo()
            _reportName = "FuelEcononyByRefill2"
            _reportPath = Server.MapPath(_reportName & ".rpt")
            ConfigureCrystalReports()
            Me.ReportMultiView.ActiveViewIndex = 1

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub DownloadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadButton.Click
        Dim sb As New StringBuilder
        sb.Append(".\DownloadData.aspx?rn=10&aid=")
        sb.Append(Encoder.UrlEncode(Me.AircraftDDL.SelectedValue))
        sb.Append("&df=")
        sb.Append(Format(CDate(Me.DateFromTB.Text), "yyyy-MM-dd"))
        sb.Append("&dt=")
        sb.Append(Format(CDate(Me.DateThruTB.Text), "yyyy-MM-dd"))
        sb.Append("&lgid=")
        sb.Append(Encoder.UrlEncode(Me.LiquidDDL.SelectedValue))
        Response.Redirect(sb.ToString, False)
    End Sub

    Protected Sub BackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackButton.Click
        Me.ReportMultiView.ActiveViewIndex = 0
    End Sub

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

    Private Sub ConfigureCrystalReports()

        Try
            PopulateArrayList()

            If Session(_reportName) Is Nothing Then
                _reportDocument = New ReportDocument()
                _reportDocument.Load(_reportPath)
            Else
                _reportDocument = CType(Session(_reportName), ReportDocument)
            End If

            _reportDocument.SetDataSource(_arrayList)

            LoadReportParameters()

            Me.theCrystalReportViewer.ReportSource = _reportDocument
            Me.theCrystalReportViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub PopulateArrayList()
        Try
            If Session(REPORT_DATA_OBJECT_NAME) Is Nothing Then
                _arrayList = FuelBurnByRefillManager.GetArrayList(_fltIdentity.CurrentProgId, Me.AircraftDDL.SelectedValue, CDate(Me.DateFromTB.Text), CDate(Me.DateThruTB.Text), CInt(Me.LiquidDDL.SelectedValue))
                Session(REPORT_DATA_OBJECT_NAME) = _arrayList
            Else
                _arrayList = CType(Session(REPORT_DATA_OBJECT_NAME), ArrayList)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub LoadReportParameters()

        Dim arrayList As New ArrayList

        arrayList.Clear()
        arrayList.Add(_fltIdentity.UserDisplayName)
        SetCurrentValuesForParameterField(_reportDocument, "UserName", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(_fltIdentity.CurrentProgId))
        SetCurrentValuesForParameterField(_reportDocument, "@ProgId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Me.AircraftDDL.SelectedValue))
        SetCurrentValuesForParameterField(_reportDocument, "@AircraftId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Me.LiquidDDL.SelectedValue))
        SetCurrentValuesForParameterField(_reportDocument, "@LiquidGroupId", arrayList)

        arrayList.Clear()
        arrayList.Add(CDate(Me.DateFromTB.Text))
        SetCurrentValuesForParameterField(_reportDocument, "@DateFrom", arrayList)

        arrayList.Clear()
        arrayList.Add(CDate(Me.DateThruTB.Text))
        SetCurrentValuesForParameterField(_reportDocument, "@DateThru", arrayList)

    End Sub

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

    Private Sub ClearErrorFields()
        Me.ErrorMessageLabel.Text = String.Empty
        Me.ErrorMessageLabel.Visible = False
        Me.SuccessMessageLabel.Text = String.Empty
        Me.SuccessMessageLabel.Visible = False
        Me.BrokenRulesList.Items.Clear()
        Me.BrokenRulesList.Visible = False
    End Sub

    Private Sub ProcessException(ByVal ex As Exception)
        Me.ErrorMessageLabel.Text = ex.Message
        Me.ErrorMessageLabel.Visible = True
    End Sub

End Class