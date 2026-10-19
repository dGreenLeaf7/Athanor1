' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL
Imports System
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Configuration
Imports System.Collections

Partial Public Class AircraftCommentsParameters
    Inherits System.Web.UI.Page

    Private _fltIdentity As FltIdentity = Nothing
    Private _reportDocument As ReportDocument = Nothing
    Private _connectionInfo As ConnectionInfo = Nothing
    Private _reportPath As String = String.Empty
    Private _reportName As String = String.Empty
    Private _reportNbr As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        ClearErrorFields()
        Me.ProgIdHiddenField.Value = _fltIdentity.CurrentProgId
        If Not Me.IsPostBack Then
            LoadFilterDDLs()
            Me.DateFromTB.Text = Format(Date.Today.Subtract(TimeSpan.FromDays(30)), "MM/dd/yyyy")
            Me.DateThruTB.Text = Format(Date.Today, "MM/dd/yyyy")
        Else
            _reportName = GetReportName(Me.SortOrderDDL.SelectedValue)
            If _reportName.Length <> 0 Then
                _reportPath = Server.MapPath(_reportName & ".rpt")
                Page.Server.ScriptTimeout = 600
                _connectionInfo = GetConnectionInfo()
                ConfigureCrystalReports()
            End If
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Session(_reportName) = _reportDocument
    End Sub

    Protected Sub LoadFilterDDLs()
        Try

            Me.LocationDDL.Items.Clear()
            Me.LocationDDL.Items.Add(New ListItem("< All >", -1))
            For Each thisLocation As Location In LocationManager.GetList(Me.ProgIdHiddenField.Value, _fltIdentity)
                Me.LocationDDL.Items.Add(New ListItem(thisLocation.LocationName, thisLocation.LocactionId))
            Next
            Me.LocationDDL.SelectedValue = Me.LocationDDL.Items(0).Value

            Me.AircraftDDL.Items.Clear()
            Me.AircraftDDL.Items.Add(New ListItem("< All >", -1))
            For Each thisAircraft As AircraftInfo In AircraftInfoManager.GetList(Me.ProgIdHiddenField.Value, 0, String.Empty, _fltIdentity)
                Me.AircraftDDL.Items.Add(New ListItem(thisAircraft.MSN, thisAircraft.AircraftId))
            Next
            Me.AircraftDDL.SelectedValue = Me.AircraftDDL.Items(0).Value

            Me.ComponentDDL.Items.Clear()
            Me.ComponentDDL.Items.Add(New ListItem("< All >", -1))
            For Each thisComponent As fltLookupBase In fltLookupBaseManager.GetList("AircraftComponent", Me.ProgIdHiddenField.Value)
                Me.ComponentDDL.Items.Add(New ListItem(thisComponent.StringValue, thisComponent.ID))
            Next
            Me.ComponentDDL.SelectedValue = Me.ComponentDDL.Items(0).Value

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub ValidateParameters()
        Try
            If Me.RatingFromDDL.SelectedValue > Me.RatingThruDDL.SelectedValue Then
                Throw New Exception("From Rating cannot be greater than Thru Rating")
            End If
            If Me.DateFromTB.Text.Length = 0 Then
                Throw New Exception("Date From is required")
            End If
            If Me.DateThruTB.Text.Length = 0 Then
                Throw New Exception("Date Thru is required")
            End If
            If Not IsDate(Me.DateFromTB.Text) Then
                Throw New Exception("Date From is not a valid date")
            End If
            If Not IsDate(Me.DateThruTB.Text) Then
                Throw New Exception("Date Thru is not a valid date")
            End If
            If CDate(Me.DateThruTB.Text) < CDate(Me.DateFromTB.Text) Then
                Throw New Exception("Date thru must be later than Date From")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub ShowReportButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowReportButton.Click
        Try
            ValidateParameters()
            Me.ReportMultiView.ActiveViewIndex = 1
            Page.Server.ScriptTimeout = 600
            _connectionInfo = GetConnectionInfo()
            _reportName = GetReportName(Me.SortOrderDDL.SelectedValue)
            _reportPath = Server.MapPath(_reportName & ".rpt")
            ConfigureCrystalReports()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub DownloadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadButton.Click
        Try
            ValidateParameters()
            Dim sb As New StringBuilder
            sb.Append("DownloadData.aspx?rn=")
            sb.Append(Me.SortOrderDDL.SelectedValue)
            sb.Append("&loc=")
            sb.Append(Me.LocationDDL.SelectedValue)
            sb.Append("&aid=")
            sb.Append(Me.AircraftDDL.SelectedValue)
            sb.Append("&cid=")
            sb.Append(Me.ComponentDDL.SelectedValue)
            sb.Append("&rf=")
            sb.Append(Me.RatingFromDDL.SelectedValue)
            sb.Append("&rh=")
            sb.Append(Me.RatingThruDDL.SelectedValue)
            sb.Append("&df=")
            sb.Append(Format(CDate(Me.DateFromTB.Text), "MM-dd-yyyy"))
            sb.Append("&dt=")
            sb.Append(Format(CDate(Me.DateThruTB.Text), "MM-dd-yyyy"))
            Response.Redirect(sb.ToString, False)

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub BackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackButton.Click
        Me.ReportMultiView.ActiveViewIndex = 0
    End Sub

    Private Function GetReportName(ByVal ReportNumber As String) As String
        Dim reportName As String = String.Empty
        Select Case ReportNumber
            Case 4
                reportName = "AircraftCommentsByDateAndAircraft"
            Case 5
                reportName = "AircraftCommentsByAircraftAndDate"
        End Select
        Return reportName
    End Function

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

        If Session(_reportName) Is Nothing Then
            _reportDocument = New ReportDocument()
            _reportDocument.Load(_reportPath)
            SetDBLogonForSubReports(_connectionInfo, _reportDocument)
            SetDBLogonForReport(_connectionInfo, _reportDocument)
        Else
            _reportDocument = CType(Session(_reportName), ReportDocument)
        End If

        LoadReportParameters()

        Me.myCrystalReportViewer.ReportSource = _reportDocument

    End Sub

    Private Sub LoadReportParameters()

        Dim arrayList As New ArrayList

        arrayList.Clear()
        arrayList.Add(CInt(_fltIdentity.CurrentProgId))
        SetCurrentValuesForParameterField(_reportDocument, "@ProgId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Me.LocationDDL.SelectedValue))
        SetCurrentValuesForParameterField(_reportDocument, "@LocationId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Me.AircraftDDL.SelectedValue))
        SetCurrentValuesForParameterField(_reportDocument, "@AircraftId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Me.ComponentDDL.SelectedValue))
        SetCurrentValuesForParameterField(_reportDocument, "@ComponentId", arrayList)

        arrayList.Clear()
        arrayList.Add(CType(Me.RatingFromDDL.SelectedValue, Int16))
        SetCurrentValuesForParameterField(_reportDocument, "@RatingLow", arrayList)

        arrayList.Clear()
        arrayList.Add(CType(Me.RatingThruDDL.SelectedValue, Int16))
        SetCurrentValuesForParameterField(_reportDocument, "@RatingHigh", arrayList)

        arrayList.Clear()
        arrayList.Add(CDate(Me.DateFromTB.Text))
        SetCurrentValuesForParameterField(_reportDocument, "@DateFrom", arrayList)

        arrayList.Clear()
        arrayList.Add(CDate(Me.DateThruTB.Text))
        SetCurrentValuesForParameterField(_reportDocument, "@DateThru", arrayList)

        arrayList.Clear()
        arrayList.Add(_fltIdentity.UserDisplayName)
        SetCurrentValuesForParameterField(_reportDocument, "UserName", arrayList)

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