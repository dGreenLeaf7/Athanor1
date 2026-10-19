' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL
Imports System
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Configuration
Imports System.Collections
Imports Microsoft.Security.Application

Partial Public Class AircraftDataParameters2
    Inherits System.Web.UI.Page
    Private _reportName As String = String.Empty
    Private Const REPORT_DATA_OBJECT_NAME As String = "AircraftDataObjectList"

    Private _fltIdentity As FltIdentity = Nothing
    Private _reportDocument As ReportDocument = Nothing
    Private _arrayList As ArrayList

    Private _connectionInfo As ConnectionInfo = Nothing
    Private _reportPath As String = String.Empty
    Private _reportNbr As String = String.Empty

    Private _intCurrPage As Int32 = 1
    Private _intLastPage As Int32

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        Page.Server.ScriptTimeout = 600
        ClearErrorFields()
        Me.ProgIdHiddenField.Value = _fltIdentity.CurrentProgId
        If Not Me.IsPostBack Then
            LoadFilterDDLs()
        Else
            If Me.ReportMultiView.ActiveViewIndex = 1 Then
                _reportName = "AircraftData2"
                _reportPath = Server.MapPath(_reportName & ".rpt")
                _connectionInfo = GetConnectionInfo()
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

            Me.LocationDDL.Items.Clear()
            Me.LocationDDL.Items.Add(New ListItem("< All >", 0))
            For Each thisLocation As Location In LocationManager.GetList(Me.ProgIdHiddenField.Value, _fltIdentity)
                Me.LocationDDL.Items.Add(New ListItem(thisLocation.LocationName, thisLocation.LocactionId))
            Next
            Me.LocationDDL.SelectedValue = Me.LocationDDL.Items(0).Value

            Me.AircraftDDL.Items.Clear()
            Me.AircraftDDL.Items.Add(New ListItem("< All >", 0))
            For Each thisAircraft As AircraftInfo In AircraftInfoManager.GetList(Me.ProgIdHiddenField.Value, 0, String.Empty, _fltIdentity)
                Me.AircraftDDL.Items.Add(New ListItem(thisAircraft.MSN, thisAircraft.AircraftId))
            Next
            Me.AircraftDDL.SelectedValue = Me.AircraftDDL.Items(0).Value

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub ValidateParameters()

    End Sub

    Protected Sub ShowReportButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowReportButton.Click
        Try
            Session(REPORT_DATA_OBJECT_NAME) = Nothing
            ValidateParameters()
            Page.Server.ScriptTimeout = 600
            _connectionInfo = GetConnectionInfo()
            _reportName = "AircraftData2"
            _reportPath = Server.MapPath(_reportName & ".rpt")
            ConfigureCrystalReports()
            Me.ReportMultiView.ActiveViewIndex = 1

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub DownloadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadButton.Click
        Try
            ValidateParameters()
            Dim sb As New StringBuilder
            sb.Append(".\DownloadData.aspx?rn=6&loc=")
            sb.Append(Encoder.UrlEncode(Me.LocationDDL.SelectedValue))
            sb.Append("&aid=")
            sb.Append(Encoder.UrlEncode(Me.AircraftDDL.SelectedValue))
            Response.Redirect(sb.ToString, False)

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub BackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackButton.Click
        Me.ReportMultiView.ActiveViewIndex = 0
    End Sub

#Region "Page Buttons for navigating pages in the Crystal Reports Viewere"

    Protected Sub PrevButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrevNavButton.Click
        GetPageNums()
        If _intCurrPage > 1 Then
            _intCurrPage = _intCurrPage - 1
        End If
        SetCurrPageNum(_intCurrPage)
        theCrystalReportViewer.ShowNthPage(_intCurrPage)
        GetPageNums()
        ButtonsCheck()
    End Sub

    Protected Sub FirstButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FirstNavButton.Click
        theCrystalReportViewer.ShowFirstPage()
        SetCurrPageNum(1)
        GetPageNums()
        ButtonsCheck()
    End Sub

    Protected Sub NextButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextNavButton.Click
        GetPageNums()
        If _intCurrPage < _intLastPage Then
            _intCurrPage = _intCurrPage + 1
        End If
        SetCurrPageNum(_intCurrPage)
        theCrystalReportViewer.ShowNthPage(_intCurrPage)
        GetPageNums()
        ButtonsCheck()
    End Sub

    Protected Sub LastButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LastNavButton.Click
        theCrystalReportViewer.ShowLastPage()
        SetCurrPageNum(theCrystalReportViewer.ViewInfo.LastPageNumber)
        GetPageNums()
        ButtonsCheck()
    End Sub

    Private Sub GetPageNums()
        _intCurrPage = GetCRPageNumber()
        _intLastPage = GetCRLastPageNumber()
        Me.PageLabel.Text = "Page " & _intCurrPage.ToString() & " of " & _intLastPage.ToString()
    End Sub

    Private Sub SetCurrPageNum(intPage As Int32)
        ViewState("_pageNbr") = intPage
    End Sub

    Private Function GetCRPageNumber() As Int32
        If String.IsNullOrEmpty(ViewState("_pageNbr")) Then
            ViewState("_pageNbr") = 1
            Return 1
        Else
            Return Convert.ToInt32(ViewState("_pageNbr"))
        End If
    End Function

    Private Function GetCRLastPageNumber() As Int32
        If theCrystalReportViewer.ViewInfo.IsLastPageNumberKnown Then
            Return theCrystalReportViewer.ViewInfo.LastPageNumber
        Else
            Return 9999
        End If
    End Function

    Private Sub ButtonsCheck()
        If _intCurrPage <= 1 Then
            Me.PrevNavButton.Enabled = False
            Me.FirstNavButton.Enabled = False
        Else
            Me.PrevNavButton.Enabled = True
            Me.FirstNavButton.Enabled = True
        End If
        If _intCurrPage >= _intLastPage Then
            Me.NextNavButton.Enabled = False
            Me.LastNavButton.Enabled = False
        Else
            Me.NextNavButton.Enabled = True
            Me.LastNavButton.Enabled = True
        End If
    End Sub

#End Region

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
            Me.theCrystalReportViewer.ShowLastPage()
            GetPageNums()
            Me.theCrystalReportViewer.ShowFirstPage()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Public Sub PopulateArrayList()
        Try
            If Session(REPORT_DATA_OBJECT_NAME) Is Nothing Then
                _arrayList = AircraftDataManager.GetArrayList(_fltIdentity.CurrentProgId, CInt(Me.LocationDDL.SelectedValue), CInt(Me.AircraftDDL.SelectedValue))
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
        arrayList.Add(CInt(_fltIdentity.CurrentProgId))
        SetCurrentValuesForParameterField(_reportDocument, "@ProgId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Me.LocationDDL.SelectedValue))
        SetCurrentValuesForParameterField(_reportDocument, "@LocationId", arrayList)

        arrayList.Clear()
        arrayList.Add(CInt(Me.AircraftDDL.SelectedValue))
        SetCurrentValuesForParameterField(_reportDocument, "@AircraftId", arrayList)

        arrayList.Clear()
        arrayList.Add(Me.LocationDDL.SelectedItem.Text)
        SetCurrentValuesForParameterField(_reportDocument, "@Location", arrayList)

        arrayList.Clear()
        arrayList.Add(Me.AircraftDDL.SelectedItem.Text)
        SetCurrentValuesForParameterField(_reportDocument, "@Aircraft", arrayList)

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