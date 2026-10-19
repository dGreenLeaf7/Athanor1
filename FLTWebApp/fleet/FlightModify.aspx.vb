' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports System.Transactions

Partial Public Class FlightModify
    Inherits System.Web.UI.Page
    Private _fltIdentity As FltIdentity = Nothing
    Private _flightKeyInfo As FlightKeyInfo = Nothing
    Private _flight As Flight = Nothing

    Private _flightId As Long = 0
    Private _aircraftId As Long = 0
    Private _pageMode As String = String.Empty
    Private _returnAddress As String = String.Empty

    Private _flightComment As New FlightComment

    Private _meterMask As String = "#,##0.0;-#,##0.0"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx", False)
            Exit Sub
        End If

        Me.ProgIdHiddenField.Value = _fltIdentity.CurrentProgId

        If Not Me.IsPostBack Then

            _flightId = Request.QueryString("flightid")
            _aircraftId = Request.QueryString("aircraftid")
            _pageMode = Request.QueryString("mode")
            _returnAddress = Request.UrlReferrer.ToString

            ClearErrorFields()

            If (_pageMode = "add") Then

                Me.PageTitleLB.Text = "Adding a New Flight"
                _flight = New Flight()
                _flightKeyInfo = New FlightKeyInfo
                Me.ParentChildMultiview.ActiveViewIndex = 0
                Me.LocationIdDDL.Enabled = True
                Me.AircraftIdDDL.Enabled = True

            ElseIf (_pageMode = "edit") Then

                Me.PageTitleLB.Text = "Editing an Existing Flight"
                _flight = FlightManager.GetItem(_flightId, _fltIdentity)
                _flightKeyInfo = FlightKeyInfoManager.GetItem(_flight.AircraftId, _flight.LocationId, _flight.FlightDateSort, _flight.WorkShiftIndicatorId, _fltIdentity)
                Me.ParentChildMultiview.ActiveViewIndex = 1
                Me.LocationIdDDL.Enabled = False
                Me.AircraftIdDDL.Enabled = False

            End If

            PopulateDDLs(_fltIdentity.CurrentProgId, _flightKeyInfo.LocationId)
            LoadDataToForm()
            BindData_LiquidFillDG()
            BindData_FlightCommentDG()
            BindData_FlightEngineRunDG()
            BindData_FlightRegionTimeDG()

        Else

            _flight = CType(ViewState("_flight"), Flight)
            _flightKeyInfo = CType(ViewState("_flightKeyInfo"), FlightKeyInfo)
            _flightId = ViewState("_flightId")
            _aircraftId = ViewState("_aircraftId")
            _returnAddress = ViewState("_returnAddress")
            _pageMode = ViewState("_pageMode")

            _flightComment = CType(ViewState("_flightComment"), FlightComment)

        End If

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_flight") = _flight
        ViewState("_flightKeyInfo") = _flightKeyInfo
        ViewState("_flightId") = _flightId
        ViewState("_aircraftId") = _aircraftId
        ViewState("_returnAddress") = _returnAddress
        ViewState("_pageMode") = _pageMode
        ViewState("_flightComment") = _flightComment
    End Sub

#Region "Flight Modify Procedures"

    Protected Sub LocationIdDDL_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LocationIdDDL.SelectedIndexChanged
        Dim totRows As Long = 0
        Me.AircraftIdDDL.Items.Clear()
        If _pageMode = "add" Then
            Me.AircraftIdDDL.Items.Add(New ListItem("< Select >", 0))
        End If
        Dim aircraftInfoList As AircraftInfoList = AircraftInfoManager.GetList(Me.ProgIdHiddenField.Value, LocationIdDDL.SelectedValue, String.Empty, 1, 0, -1, -1, totRows, _fltIdentity)
        If Not (aircraftInfoList Is Nothing) Then
            For Each thisAircraftInfo As AircraftInfo In aircraftInfoList
                Me.AircraftIdDDL.Items.Add(New ListItem(thisAircraftInfo.MSN, thisAircraftInfo.AircraftId))
            Next
        End If
        Me.SchedRouteDDL.Items.Clear()
        Me.SchedRouteDDL.Items.Add(New ListItem("< Select >", 0))
        Dim routeList As RouteList = RouteManager.GetList(Me.ProgIdHiddenField.Value, LocationIdDDL.SelectedValue, 0, 1, 0, -1, -1, totRows, _fltIdentity)
        If Not (routeList Is Nothing) Then
            For Each thisRoute As Route In routeList
                Me.SchedRouteDDL.Items.Add(New ListItem(thisRoute.Name, thisRoute.RouteId))
            Next
        End If
        Me.AircraftIdDDL.Focus()
    End Sub

    Protected Sub LocationEditDDL_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LocationEditDDL.SelectedIndexChanged
        Dim totRows As Long = 0
        Me.AircraftEditDDL.Items.Clear()
        If _pageMode = "add" Then
            Me.AircraftEditDDL.Items.Add(New ListItem("< Select >", 0))
        End If
        Dim aircraftInfoList As AircraftInfoList = AircraftInfoManager.GetList(Me.ProgIdHiddenField.Value, LocationIdDDL.SelectedValue, String.Empty, 1, 0, -1, -1, totRows, _fltIdentity)
        If Not (aircraftInfoList Is Nothing) Then
            For Each thisAircraftInfo As AircraftInfo In aircraftInfoList
                Me.AircraftEditDDL.Items.Add(New ListItem(thisAircraftInfo.MSN, thisAircraftInfo.AircraftId))
            Next
        End If
        Me.SchedRouteDDL.Items.Clear()
        Me.SchedRouteDDL.Items.Add(New ListItem("< Select >", 0))
        Dim routeList As RouteList = RouteManager.GetList(Me.ProgIdHiddenField.Value, LocationIdDDL.SelectedValue, 0, 1, 0, -1, -1, totRows, _fltIdentity)
        If Not (routeList Is Nothing) Then
            For Each thisRoute As Route In routeList
                Me.SchedRouteDDL.Items.Add(New ListItem(thisRoute.Name, thisRoute.RouteId))
            Next
        End If
        Me.AircraftEditDDL.Focus()
    End Sub

    Protected Sub NextButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextButton.Click

        Try

            Dim AircraftId As Integer = CInt(Me.AircraftIdDDL.SelectedValue)
            Dim LocationId As Integer = CInt(Me.LocationIdDDL.SelectedValue)
            Dim FlightDateSort As String = Me.FlightDateTB.Text
            Dim WorkShiftId As Integer = CInt(Me.WorkShiftDDL.SelectedValue)

            _flightKeyInfo = FlightKeyInfoManager.GetItem(AircraftId, LocationId, FlightDateSort, WorkShiftId, _fltIdentity)

            If _flightKeyInfo.ErrorsList.Count <> 0 Then
                Throw New Exception("Unable to retrieve information. Please correct errors and try again.")
            End If

            If _flightKeyInfo.ValidFlight Then
                Me.NextButton.Visible = False
                Me.EditButton.Visible = True
                Me.RetryButton.Visible = True
                With _flightKeyInfo.ErrorsList
                    .Add(New BrokenRule(String.Empty, "Click 'Edit' to edit this flight."))
                    .Add(New BrokenRule(String.Empty, "Click 'Retry' to change Flight key information."))
                    .Add(New BrokenRule(String.Empty, "Click 'Cancel' to return to the Flight List page."))
                End With
                Throw New Exception("This flight already exists!")
            End If

            Me.ParentChildMultiview.ActiveViewIndex = 1
            PopulateDDLs(_fltIdentity.CurrentProgId, _flightKeyInfo.LocationId)
            LoadDataToForm()
            BindData_LiquidFillDG()
            BindData_FlightCommentDG()
            BindData_FlightEngineRunDG()
            BindData_FlightRegionTimeDG()

        Catch ex As Exception
            ProcessException(ex, _flightKeyInfo.ErrorsList, _flightKeyInfo.WarningsList)
        End Try

    End Sub

    Protected Sub EditButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditButton.Click
        Try
            Me.PageTitleLB.Text = "Editing an Existing Flight"
            _pageMode = "edit"
            _flight = FlightManager.GetItem(_flightKeyInfo.FlightId, _fltIdentity)

            Me.ParentChildMultiview.ActiveViewIndex = 1
            PopulateDDLs(_fltIdentity.CurrentProgId, _flightKeyInfo.LocationId)
            LoadDataToForm()
            BindData_LiquidFillDG()
            BindData_FlightCommentDG()
            BindData_FlightEngineRunDG()
            BindData_FlightRegionTimeDG()


        Catch ex As Exception
            ProcessException(ex, _flight.ErrorsList, _flight.WarningsList)
        End Try
    End Sub

    Protected Sub RetryButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RetryButton.Click
        Me.NextButton.Visible = True
        Me.EditButton.Visible = False
        Me.RetryButton.Visible = False
        ClearErrorFields()
        Me.LocationIdDDL.Focus()
    End Sub

    Protected Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click

        Try
            If Not SaveAllPendingChildDataGridAdds() Then Exit Sub

            LoadDataToObject()
            ProcessWarnings(_flight.WarningsList)
            ClearErrorFields()

            FlightManager.Save(_flight, _fltIdentity)

            Response.Redirect(_returnAddress, False)

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Response.Redirect(_returnAddress, False)
    End Sub

    Protected Sub HobbsStartTB_Change(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HobbsStartTB.TextChanged
        If IsNumeric(Me.HobbsStartTB.Text) Then
            Me.HobbsStartLB.Text = Format(CDec(Me.HobbsStartTB.Text), _meterMask)
        End If
        ComputeTotalHours()
        Me.EngineHoursStartTB.Focus()
    End Sub

    Protected Sub HobbsEndTB_Change(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HobbsEndTB.TextChanged
        If IsNumeric(Me.HobbsEndTB.Text) Then
            Me.HobbsEndLB.Text = Format(CDec(Me.HobbsEndTB.Text), _meterMask)
        End If
        ComputeTotalHours()
        Me.EngineHoursEndTB.Focus()
    End Sub

    Private Sub ComputeTotalHours()
        If IsNumeric(Me.HobbsStartTB.Text) And IsNumeric(Me.HobbsEndTB.Text) Then
            Me.TotalHoursLabel.Text = Format(CDec(Me.HobbsEndTB.Text) - CDec(Me.HobbsStartTB.Text), _meterMask)
            Me.AirframeHoursEndLB.Text = Format(CDec(Me.HobbsEndTB.Text) + CDec(Me.UsedMeterOffsetLB.Text), _meterMask)
            Me.HobbsStartTB.Text = Format(CDec(Me.HobbsStartTB.Text), _meterMask)
            Me.HobbsEndTB.Text = Format(CDec(Me.HobbsEndTB.Text), _meterMask)
        End If
    End Sub

    Private Sub PopulateDDLs(ByVal ProgId As Integer, ByVal LocationId As Integer)
        Dim totRows As Long = 0
        Me.LocationIdDDL.Items.Clear()
        If _pageMode = "add" Then
            Me.LocationIdDDL.Items.Add(New ListItem("< Select >", 0))
        End If
        Dim locationList As LocationList = LocationManager.GetList(ProgId, 0, 1, 0, -1, -1, totRows, _fltIdentity)
        If Not (locationList Is Nothing) Then
            For Each thisLocation As Location In locationList
                LocationIdDDL.Items.Add(New ListItem(thisLocation.LocationName, thisLocation.LocactionId))
            Next
        End If
        Me.LocationEditDDL.Items.Clear()
        If _pageMode = "add" Then
            Me.LocationEditDDL.Items.Add(New ListItem("< Select >", 0))
        End If
        locationList = LocationManager.GetList(ProgId, 0, 1, 0, -1, -1, totRows, _fltIdentity)
        If Not (locationList Is Nothing) Then
            For Each thisLocation As Location In locationList
                LocationEditDDL.Items.Add(New ListItem(thisLocation.LocationName, thisLocation.LocactionId))
            Next
        End If
        Me.AircraftIdDDL.Items.Clear()
        If _pageMode = "add" Then
            Me.AircraftIdDDL.Items.Add(New ListItem("< Select >", 0))
        End If
        Dim aircraftInfoList As AircraftInfoList = AircraftInfoManager.GetList(ProgId, 0, String.Empty, 1, 0, -1, -1, totRows, _fltIdentity)
        If Not (aircraftInfoList Is Nothing) Then
            For Each thisAircraftInfo As AircraftInfo In aircraftInfoList
                Me.AircraftIdDDL.Items.Add(New ListItem(thisAircraftInfo.MSN, thisAircraftInfo.AircraftId))
            Next
        End If
        Me.AircraftEditDDL.Items.Clear()
        If _pageMode = "add" Then
            Me.AircraftEditDDL.Items.Add(New ListItem("< Select >", 0))
        End If
        aircraftInfoList = AircraftInfoManager.GetList(ProgId, 0, String.Empty, 1, 0, -1, -1, totRows, _fltIdentity)
        If Not (aircraftInfoList Is Nothing) Then
            For Each thisAircraftInfo As AircraftInfo In aircraftInfoList
                Me.AircraftEditDDL.Items.Add(New ListItem(thisAircraftInfo.MSN, thisAircraftInfo.AircraftId))
            Next
        End If
        LoadDDL(Me.WorkShiftDDL, ProgId, "WorkShift")
        LoadDDL(Me.WorkShiftEditDDL, ProgId, "WorkShift")
        Me.PilotDDL.Items.Clear()
        Me.PilotDDL.Items.Add(New ListItem("< Select >", 0))
        Dim pilotList As fltLookupBaseList = fltLookupBaseManager.GetList("Pilot", ProgId)
        If (Not pilotList Is Nothing) Then
            For Each thisPilot As fltLookupBase In pilotList
                Me.PilotDDL.Items.Add(New ListItem(thisPilot.StringValue, thisPilot.ID))
            Next
        End If
        Me.SchedRouteDDL.Items.Clear()
        If _pageMode = "add" Then
            Me.SchedRouteDDL.Items.Add(New ListItem("< Select >", 0))
        End If
        Dim routeList As RouteList = RouteManager.GetList(ProgId, LocationId, 0, 1, 0, -1, -1, totRows, _fltIdentity)
        If Not (routeList Is Nothing) Then
            For Each thisRoute As Route In routeList
                Me.SchedRouteDDL.Items.Add(New ListItem(thisRoute.Name, thisRoute.RouteId))
            Next
        End If

        Me.AirframeDDL.Items.Clear()
        Me.AirframeDDL.Items.Add(New ListItem("N/A", 0))
        Dim airframeWeightList As WeightDefList = WeightDefManager.GetList(ProgId, "Airframe", _fltIdentity)
        For Each thisAirframeWeight As WeightDef In airframeWeightList
            Me.AirframeDDL.Items.Add(New ListItem(thisAirframeWeight.WeightDefDescr, thisAirframeWeight.WeightDefId))
        Next

        Me.Cargo1DDL.Items.Clear()
        Me.Cargo1DDL.Items.Add(New ListItem("N/A", 0))
        Dim cargo1WeightList As WeightDefList = WeightDefManager.GetList(ProgId, "Cargo", _fltIdentity)
        For Each thisCargo1Weight As WeightDef In cargo1WeightList
            Me.Cargo1DDL.Items.Add(New ListItem(thisCargo1Weight.WeightDefDescr, thisCargo1Weight.WeightDefId))
        Next

        Me.Cargo2DDL.Items.Clear()
        Me.Cargo2DDL.Items.Add(New ListItem("N/A", 0))
        Dim cargo2WeightList As WeightDefList = WeightDefManager.GetList(ProgId, "Cargo", _fltIdentity)
        For Each thisCargo2Weight As WeightDef In cargo2WeightList
            Me.Cargo2DDL.Items.Add(New ListItem(thisCargo2Weight.WeightDefDescr, thisCargo2Weight.WeightDefId))
        Next

        Me.BallastDDL.Items.Clear()
        Me.BallastDDL.Items.Add(New ListItem("N/A", 0))
        Dim ballastWeightList As WeightDefList = WeightDefManager.GetList(ProgId, "Ballast", _fltIdentity)
        For Each thisBallastWeight As WeightDef In ballastWeightList
            Me.BallastDDL.Items.Add(New ListItem(thisBallastWeight.WeightDefDescr, thisBallastWeight.WeightDefId))
        Next

    End Sub

    Private Sub LoadDDL(ByVal DDL As DropDownList, ByVal ProgId As Long, ByVal LookupName As String)
        Try
            Dim totRows As Long = 0
            Dim IsActiveFilter As Long = 1
            If LookupName.ToUpper = "PILOT" Then
                IsActiveFilter = 2
            End If
            DDL.Items.Clear()
            If _pageMode = "add" Then
                DDL.Items.Add(New ListItem("< Select >", 0))
            End If
            Dim thisLookupList As fltLookupBaseList = fltLookupBaseManager.GetList(LookupName, ProgId, IsActiveFilter, -1, -1, totRows)
            If Not (thisLookupList Is Nothing) Then
                For Each thisLookup As fltLookupBase In thisLookupList
                    DDL.Items.Add(New ListItem(thisLookup.StringValue, thisLookup.ID))
                Next
            Else
                Throw New Exception("Why is the drop-down list for " & LookupName & " empty?")
            End If
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Private Sub LoadDataToForm()

        Try
            Me.FlightIdHiddenField.Value = _flight.FlightId
            Me.AircraftEditDDL.SelectedValue = _flightKeyInfo.AircraftId
            Me.LocationEditDDL.SelectedValue = _flightKeyInfo.LocationId
            Me.SchedRouteDDL.SelectedValue = _flight.SchedRouteId
            If IsDate(_flightKeyInfo.FlightDateSort) Then
                Me.FlightDateEditTB.Text = Format(CDate(_flightKeyInfo.FlightDateSort), "MM/dd/yyyy")
            End If
            Me.WorkShiftEditDDL.SelectedValue = _flightKeyInfo.WorkShiftId
            Me.PilotDDL.SelectedValue = _flight.PilotId
            Me.PreFlightFtiOKDDL.SelectedValue = _flight.PreFlightFtiOK
            Me.PreFlightTelemetryOKDDL.SelectedValue = _flight.PreFlightTelemetryOK
            Me.PreFlightTelemetryCheckCompleteDDL.SelectedValue = _flight.PreFlightTelemetryCheckComplete
            Me.OilLevelIndicatorStartDDL.SelectedValue = _flight.OilLevelIndicatorStart
            Me.HydraulicLevelIndicatorStart.SelectedValue = _flight.HydraulicLevelIndicatorStart

            Me.TakeoffWeightLB.Text = Format(_flight.TakeoffWeight, "#,##0")
            Me.AirframeDDL.SelectedValue = _flight.AirframeWeightId
            Me.Cargo1DDL.SelectedValue = _flight.Cargo1WeightId
            Me.Cargo2DDL.SelectedValue = _flight.Cargo2WeightId
            Me.BallastDDL.SelectedValue = _flight.BallastWeightId

            If _pageMode = "add" Then
                Me.AirframeHoursStartLB.Text = Format(_flightKeyInfo.AirframeHoursCurrent, _meterMask)
                Me.UsedMeterOffsetLB.Text = Format(_flightKeyInfo.AirframeHoursOffset, _meterMask)
                Me.HobbsStartTB.Text = Format(CDec(_flightKeyInfo.AirframeHoursCurrent) - CDec(_flightKeyInfo.AirframeHoursOffset), _meterMask)
                Me.HobbsStartLB.Text = Me.HobbsStartTB.Text

                If _flightKeyInfo.AircraftId <> 0 Then
                    Me.AircraftEngineHoursCurrentHF.Value = AircraftManager.GetItem(_flightKeyInfo.AircraftId, _fltIdentity).AircraftEngineHoursCurrent
                    Me.EngineHoursStartTB.Text = Format(CDec(Me.AircraftEngineHoursCurrentHF.Value), _meterMask)
                End If

                Me.HobbsStartTB.Enabled = True
            Else
                Me.AirframeHoursStartLB.Text = Server.HtmlEncode(_flight.AirframeHoursStart)
                Me.UsedMeterOffsetLB.Text = Server.HtmlEncode(_flight.UsedMeterOffset)
                Me.HobbsStartTB.Text = Server.HtmlEncode(_flight.HobbsStart)
                Me.HobbsStartLB.Text = Server.HtmlEncode(Me.HobbsStartTB.Text)
                Me.AirframeHoursEndLB.Text = Server.HtmlEncode(_flight.AirframeHoursEnd)
                Me.TotalHoursLabel.Text = Format(CDec(_flight.AirframeHoursEnd) - CDec(_flight.AirframeHoursStart), _meterMask)

                Me.AircraftEngineHoursCurrentHF.Value = Format(CDec(_flight.AircraftEngineHoursCurrent), _meterMask)
                Me.EngineHoursStartTB.Text = _flight.EngineHoursStart

                If _flight.IsApproved Then
                    Me.HobbsStartTB.Enabled = False
                    Me.HobbsEndTB.Enabled = False
                End If

            End If

            Me.DepartDateTimeTB.Text = Server.HtmlEncode(_flight.DepartureTime)
            Me.ReturnDateTimeTB.Text = Server.HtmlEncode(_flight.ReturnTime)
            Me.HobbsEndTB.Text = Server.HtmlEncode(_flight.HobbsEnd)
            Me.HobbsEndLB.Text = Me.HobbsEndTB.Text
            Me.EngineHoursEndTB.Text = Server.HtmlEncode(_flight.EngineHoursEnd)
            Me.MeterUnitLB.Text = Server.HtmlEncode(_flightKeyInfo.MeterUnit)
            Me.PostFlightFtiOKDDL.SelectedValue = _flight.PostFlightFtiOK
            Me.PostFlightTelemetryCheckCompleteDDL.SelectedValue = _flight.PostFlightTelemetryCheckComplete
            Me.CarbonSootBuildupDDL.SelectedValue = _flight.NacelleCheckBuildUp
            Me.LeaksOrDiscolorationDDL.SelectedValue = _flight.NacelleCheckDiscoloration

            If _fltIdentity.IsSuperUser Or _fltIdentity.IsCurrentProgramManager Then
                Me.HobbsStartTB.Enabled = True
                Me.HobbsEndTB.Enabled = True
                LocationEditDDL.Enabled = True
                AircraftEditDDL.Enabled = True
                WorkShiftEditDDL.Enabled = True
                FlightDateEditTB.Enabled = True
            Else
                AircraftEditDDL.Enabled = False
                LocationEditDDL.Enabled = False
                WorkShiftEditDDL.Enabled = False
                FlightDateEditTB.Enabled = False
            End If

        Catch ex As Exception
            ProcessException(ex)

        End Try

    End Sub

    Private Sub LoadDataToObject()

        Try
            _flight.FlightId = Me.FlightIdHiddenField.Value
            _flight.AircraftId = Me.AircraftEditDDL.SelectedValue
            _flight.LocationId = Me.LocationEditDDL.SelectedValue
            _flight.SchedRouteId = Me.SchedRouteDDL.SelectedValue
            _flight.FlownRouteId = Me.SchedRouteDDL.SelectedValue
            _flight.FlightDate = Server.HtmlDecode(Me.FlightDateEditTB.Text)
            _flight.WorkShiftIndicatorId = Me.WorkShiftEditDDL.SelectedValue
            _flight.PilotId = Me.PilotDDL.SelectedValue
            _flight.PreFlightFtiOK = Me.PreFlightFtiOKDDL.SelectedValue
            _flight.PreFlightTelemetryOK = Me.PreFlightTelemetryOKDDL.SelectedValue
            _flight.PreFlightTelemetryCheckComplete = Me.PreFlightTelemetryCheckCompleteDDL.SelectedValue
            _flight.OilLevelIndicatorStart = Me.OilLevelIndicatorStartDDL.SelectedValue
            _flight.HydraulicLevelIndicatorStart = Me.HydraulicLevelIndicatorStart.SelectedValue
            If _pageMode = "add" Then
                _flight.UsedMeterOffset = _flightKeyInfo.AirframeHoursOffset
            Else
                _flight.UsedMeterOffset = Server.HtmlDecode(Me.UsedMeterOffsetLB.Text)
            End If
            _flight.DepartureTime = Server.HtmlDecode(Me.DepartDateTimeTB.Text)

            _flight.AircraftEngineHoursCurrent = Server.HtmlDecode(Me.AircraftEngineHoursCurrentHF.Value)
            
            _flight.EngineHoursStart = Server.HtmlDecode(Me.EngineHoursStartTB.Text)
            _flight.HobbsStart = Server.HtmlDecode(Me.HobbsStartTB.Text)
            _flight.AirframeHoursStart = Server.HtmlDecode(Me.AirframeHoursStartLB.Text)
            _flight.AirframeWeightId = Me.AirframeDDL.SelectedValue
            _flight.Cargo1WeightId = Me.Cargo1DDL.SelectedValue
            _flight.Cargo2WeightId = Me.Cargo2DDL.SelectedValue
            _flight.BallastWeightId = Me.BallastDDL.SelectedValue
            _flight.ReturnTime = Server.HtmlDecode(Me.ReturnDateTimeTB.Text)
            _flight.HobbsEnd = Server.HtmlDecode(Me.HobbsEndTB.Text)
            _flight.AirframeHoursEnd = Server.HtmlDecode(Me.AirframeHoursEndLB.Text)
            _flight.MeterUnitId = _flightKeyInfo.MeterUnitId
            _flight.EngineHoursEnd = Server.HtmlDecode(Me.EngineHoursEndTB.Text)
            _flight.PostFlightFtiOK = Me.PostFlightFtiOKDDL.SelectedValue
            _flight.PostFlightTelemetryCheckComplete = Me.PostFlightTelemetryCheckCompleteDDL.SelectedValue
            _flight.NacelleCheckBuildUp = Me.CarbonSootBuildupDDL.SelectedValue
            _flight.NacelleCheckDiscoloration = Me.LeaksOrDiscolorationDDL.SelectedValue

        Catch ex As Exception
            ProcessException(ex)

        End Try

    End Sub

    Public Sub AddPilotButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddPilotButton.Click
        Me.ParentChildMultiview.ActiveViewIndex = 2
        Me.PilotNameTB.Text = String.Empty
        Me.PilotSortOrderTB.Text = String.Empty
        Me.PilotIsActiveCheckBox.Checked = True
    End Sub

#End Region

#Region "LiquidFill Procedures"

    Private Sub BindData_LiquidFillDG()
        Me.LiquidFillDG.DataSource = _flight.LiquidFillList
        Me.LiquidFillDG.DataBind()
    End Sub

    Protected Sub LiquidFillDG_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles LiquidFillDG.ItemCommand

        Dim liquidFill As New LiquidFill

        Try
            Dim dg As DataGrid = CType(sender, DataGrid)

            Select Case e.CommandName

                Case "Add"
                    Dim liquidGroupId As String = CType(e.Item.FindControl("LiquidGroupDDL"), DropDownList).SelectedValue
                    Dim liquidGroupName As String = CType(e.Item.FindControl("LiquidGroupDDL"), DropDownList).SelectedItem.ToString
                    Dim liquidTypeId As String = CType(e.Item.FindControl("LiquidTypeDDL"), DropDownList).SelectedValue
                    Dim liquidTypeName As String = CType(e.Item.FindControl("LiquidTypeDDL"), DropDownList).SelectedItem.ToString
                    Dim liquidUnitId As String = CType(e.Item.FindControl("LiquidUnitDDL"), DropDownList).SelectedValue
                    Dim liquidUnitName As String = CType(e.Item.FindControl("LiquidUnitDDL"), DropDownList).SelectedItem.ToString
                    Dim liquidAmt As String = CType(e.Item.FindControl("LiquidAmtTB"), TextBox).Text
                    Dim hobbsReading As String = CType(e.Item.FindControl("HobbsTB"), TextBox).Text
                    Dim airframeHoursReading As Decimal
                    If hobbsReading <> String.Empty Then
                        airframeHoursReading = CDec(hobbsReading) + _flightKeyInfo.AirframeHoursOffset
                    End If
                    Dim liquidFillLocation As String = CType(e.Item.FindControl("LocationTB"), TextBox).Text
                    Dim liquidFillApprover As String = CType(e.Item.FindControl("ApproverTB"), TextBox).Text

                    If liquidGroupId <> "0" Or liquidTypeId <> "0" Or liquidUnitId <> "0" Or liquidAmt <> String.Empty _
                    Or hobbsReading <> String.Empty Or liquidFillLocation <> String.Empty Or liquidFillApprover <> String.Empty Then

                        liquidFill = New LiquidFill
                        With liquidFill
                            .FlightId = _flight.FlightId
                            .LiquidGroupId = CInt(liquidGroupId)
                            .LiquidGroupName = liquidGroupName
                            .LiquidTypeId = CInt(liquidTypeId)
                            .LiquidTypeName = liquidTypeName
                            .LiquidUnitId = CInt(liquidUnitId)
                            .LiquidUnitName = liquidUnitName
                            .LiquidAmt = Server.HtmlDecode(liquidAmt)
                            If Not IsNumeric(hobbsReading) Then
                                .HobbsReading = Format((_flightKeyInfo.AirframeHoursCurrent - _flightKeyInfo.AirframeHoursOffset), _meterMask)
                            Else
                                .HobbsReading = hobbsReading
                            End If
                            .AirframeHoursReading = Format(airframeHoursReading, _meterMask)
                            .LiquidFillLocation = Server.HtmlDecode(liquidFillLocation)
                            .LiquidFillApprover = Server.HtmlDecode(liquidFillApprover)
                        End With
                        FlightManager.InsertLiquidFill(_flight, liquidFill)
                        BindData_LiquidFillDG()

                    End If

                Case "Edit"
                    dg.EditItemIndex = CInt(e.Item.ItemIndex)
                    BindData_LiquidFillDG()
                    Me.SaveButton.Enabled = False

                Case "Save"
                    liquidFill = New LiquidFill
                    With liquidFill
                        .LiquidFillId = CInt(CType(e.Item.FindControl("LiquidFillIdLB"), Label).Text)
                        .FlightId = _flight.FlightId
                        .LiquidGroupId = CType(e.Item.FindControl("LiquidGroupDDL"), DropDownList).SelectedValue
                        .LiquidGroupName = CType(e.Item.FindControl("LiquidGroupDDL"), DropDownList).SelectedItem.ToString
                        .LiquidTypeId = CType(e.Item.FindControl("LiquidTypeDDL"), DropDownList).SelectedValue
                        .LiquidTypeName = CType(e.Item.FindControl("LiquidTypeDDL"), DropDownList).SelectedItem.ToString
                        .LiquidUnitId = CType(e.Item.FindControl("LiquidUnitDDL"), DropDownList).SelectedValue
                        .LiquidUnitName = CType(e.Item.FindControl("LiquidUnitDDL"), DropDownList).SelectedItem.ToString
                        .LiquidAmt = Server.HtmlDecode(CType(e.Item.FindControl("LiquidAmtTB"), TextBox).Text)
                        If Not IsNumeric(CType(e.Item.FindControl("LiquidAmtTB"), TextBox).Text) Then
                            .HobbsReading = Format(_flightKeyInfo.AirframeHoursCurrent, _meterMask)
                        End If
                        .HobbsReading = CType(e.Item.FindControl("HobbsTB"), TextBox).Text
                        .AirframeHoursReading = CDec(.HobbsReading) + _flightKeyInfo.AirframeHoursOffset
                        .LiquidFillLocation = Server.HtmlDecode(CType(e.Item.FindControl("LocationTB"), TextBox).Text)
                        .LiquidFillApprover = Server.HtmlDecode(CType(e.Item.FindControl("ApproverTB"), TextBox).Text)
                    End With
                    FlightManager.UpdateLiquidFill(_flight, liquidFill)
                    dg.EditItemIndex = -1
                    BindData_LiquidFillDG()
                    If Me.LiquidFillDG.EditItemIndex = -1 _
                    And Me.FlightCommentDG.EditItemIndex = -1 _
                    And Me.FlightEngineRunDG.EditItemIndex = -1 _
                    And Me.FlightRegionTimeDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If

                Case "Cancel"
                    dg.EditItemIndex = -1
                    BindData_LiquidFillDG()
                    If Me.LiquidFillDG.EditItemIndex = -1 _
                    And Me.FlightCommentDG.EditItemIndex = -1 _
                    And Me.FlightEngineRunDG.EditItemIndex = -1 _
                    And Me.FlightRegionTimeDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If

                Case "Delete"
                    Dim thisFlightCommentId = CInt(dg.DataKeys(e.Item.ItemIndex))
                    FlightManager.RemoveLiquidFill(_flight, thisFlightCommentId)
                    BindData_LiquidFillDG()

            End Select

        Catch ex As Exception
            ProcessException(ex, liquidFill.ErrorsList, liquidFill.WarningsList)
            If e.CommandArgument = "OnSave" Then
                Throw New Exception(ex.Message)
            End If
        End Try
    End Sub

    Protected Function GetLiquidGroupList() As fltLookupBaseList
        Dim totRows As Long = 0
        Dim newLiquidGroupList As New fltLookupBaseList
        Dim newLiquidGroup As New fltLookupBase
        With newLiquidGroup
            .ID = 0
            .LookupName = "LiquidGroup"
            .StringValue = "< Select >"
        End With
        newLiquidGroupList.Add(newLiquidGroup)
        Dim oldLiquidGroupList As fltLookupBaseList = fltLookupBaseManager.GetList("LiquidGroup", Me.ProgIdHiddenField.Value, 1, -1, -1, totRows)
        For Each thisLiquidGroup As fltLookupBase In oldLiquidGroupList
            newLiquidGroupList.Add(thisLiquidGroup)
        Next
        Return newLiquidGroupList
    End Function

    Protected Function GetLiquidTypeList() As fltLookupBaseList
        Dim totRows As Long = 0
        Dim newLiquidTypeList As New fltLookupBaseList
        Dim newLiquidType As New fltLookupBase
        With newLiquidType
            .ID = 0
            .LookupName = "LiquidType"
            .StringValue = "< Select >"
        End With
        newLiquidTypeList.Add(newLiquidType)
        Dim oldLiquidTypeList As fltLookupBaseList = fltLookupBaseManager.GetList("LiquidType", Me.ProgIdHiddenField.Value, 1, -1, -1, totRows)
        For Each thisLiquidType As fltLookupBase In oldLiquidTypeList
            newLiquidTypeList.Add(thisLiquidType)
        Next
        Return newLiquidTypeList
    End Function

    Protected Function GetLiquidUnitList() As fltLookupBaseList
        Dim totRows As Long = 0
        Dim newLiquidUnitList As New fltLookupBaseList
        Dim newLiquidUnit As New fltLookupBase
        With newLiquidUnit
            .ID = 0
            .LookupName = "LiquidUnit"
            .StringValue = "--"
        End With
        newLiquidUnitList.Add(newLiquidUnit)
        Dim oldLiquidUnitList As fltLookupBaseList = fltLookupBaseManager.GetList("LiquidUnit", Me.ProgIdHiddenField.Value, 1, -1, -1, totRows)
        For Each thisLiquidUnit As fltLookupBase In oldLiquidUnitList
            newLiquidUnitList.Add(thisLiquidUnit)
        Next
        Return newLiquidUnitList
    End Function

#End Region

#Region "FlightComment Procedures"

    Private Sub BindData_FlightCommentDG()
        Me.FlightCommentDG.DataSource = _flight.FlightCommentList
        Me.FlightCommentDG.DataBind()
    End Sub

    Protected Sub FlightCommentDG_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles FlightCommentDG.ItemCommand

        Try
            Dim dg As DataGrid = CType(sender, DataGrid)

            Select Case e.CommandName

                Case "Add"

                    ProcessWarnings(_flightComment.WarningsList)
                    _flightComment.ErrorsList.Clear()

                    Dim componentId As String = CType(e.Item.FindControl("ComponentDDL"), DropDownList).SelectedValue
                    Dim componentName As String = CType(e.Item.FindControl("componentDDL"), DropDownList).SelectedItem.ToString
                    Dim issueCodeId As String = CType(e.Item.FindControl("IssueCodeDDL"), DropDownList).SelectedValue
                    Dim issueCode As String = CType(e.Item.FindControl("IssueCodeDDL"), DropDownList).SelectedItem.Text
                    Dim commentTime As String = CType(e.Item.FindControl("CommentTimeTB"), TextBox).Text
                    Dim pilotComment As String = CType(e.Item.FindControl("PilotCommentTB"), TextBox).Text.Trim()
                    Dim commentRating As String = CType(e.Item.FindControl("CommentRatingDDL"), DropDownList).SelectedValue

                    If componentId <> "0" Or commentTime <> String.Empty _
                    Or pilotComment <> String.Empty Or commentRating <> "0" Then
                        With _flightComment
                            .FlightId = _flight.FlightId
                            .ComponentId = CInt(componentId)
                            .ComponentName = componentName
                            .IssueCodeId = issueCodeId
                            .IssueCode = issueCode
                            .CommentTime = Server.HtmlDecode(commentTime)
                            .PilotComment = Server.HtmlDecode(pilotComment)
                            .CommentRating = CType(commentRating, Short)
                        End With
                        FlightManager.InsertFlightComment(_flight, _flightComment)
                        BindData_FlightCommentDG()
                        _flightComment = New FlightComment
                        ClearErrorFields()
                    End If

                Case "Edit"
                    dg.EditItemIndex = CInt(e.Item.ItemIndex)
                    _flightComment = New FlightComment
                    BindData_FlightCommentDG()
                    Me.SaveButton.Enabled = False

                Case "Save"
                    ProcessWarnings(_flightComment.WarningsList)
                    _flightComment.ErrorsList.Clear()
                    With _flightComment
                        .FlightCommentId = CInt(CType(e.Item.FindControl("FlightCommentIdLB"), Label).Text)
                        .FlightId = _flight.FlightId
                        .ComponentId = CType(e.Item.FindControl("ComponentDDL"), DropDownList).SelectedValue
                        .ComponentName = CType(e.Item.FindControl("componentDDL"), DropDownList).SelectedItem.ToString
                        .IssueCodeId = CType(e.Item.FindControl("IssueCodeDDL"), DropDownList).SelectedValue
                        .IssueCode = CType(e.Item.FindControl("IssueCodeDDL"), DropDownList).SelectedItem.Text
                        .CommentTime = Server.HtmlDecode(CType(e.Item.FindControl("CommentTimeTB"), TextBox).Text)
                        .PilotComment = Server.HtmlDecode(CType(e.Item.FindControl("PilotCommentTB"), TextBox).Text)
                        .CommentRating = CType(e.Item.FindControl("CommentRatingDDL"), DropDownList).SelectedValue
                    End With
                    FlightManager.UpdateFlightComment(_flight, _flightComment)
                    dg.EditItemIndex = -1
                    _flightComment = New FlightComment
                    ClearErrorFields()
                    BindData_FlightCommentDG()
                    If Me.LiquidFillDG.EditItemIndex = -1 _
                    And Me.FlightCommentDG.EditItemIndex = -1 _
                    And Me.FlightEngineRunDG.EditItemIndex = -1 _
                    And Me.FlightRegionTimeDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If

                Case "Cancel"
                    dg.EditItemIndex = -1
                    _flightComment = New FlightComment
                    BindData_FlightCommentDG()
                    ClearErrorFields()
                    If Me.LiquidFillDG.EditItemIndex = -1 _
                    And Me.FlightCommentDG.EditItemIndex = -1 _
                    And Me.FlightEngineRunDG.EditItemIndex = -1 _
                    And Me.FlightRegionTimeDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If

                Case "Delete"
                    Dim thisFlightCommentId As Integer = CInt(dg.DataKeys(e.Item.ItemIndex))
                    FlightManager.RemoveFlightComment(_flight, thisFlightCommentId)
                    BindData_FlightCommentDG()

            End Select

        Catch ex As Exception
            ProcessException(ex, _flightComment.ErrorsList, _flightComment.WarningsList)
            If e.CommandArgument = "OnSave" Then
                Throw New Exception(ex.Message)
            End If
        End Try
    End Sub

    Protected Function GetAircraftComponentList() As fltLookupBaseList
        Dim totRows As Long = 0
        Dim newAircraftComponentList As New fltLookupBaseList
        Dim newAircraftComponent As New fltLookupBase
        With newAircraftComponent
            .ID = 0
            .LookupName = "AircraftComponent"
            .StringValue = "< Select >"
        End With
        newAircraftComponentList.Add(newAircraftComponent)
        Dim oldAircraftComponentList As fltLookupBaseList = fltLookupBaseManager.GetList("AircraftComponent", Me.ProgIdHiddenField.Value, 1, -1, -1, totRows)
        For Each thisAircraftComponent As fltLookupBase In oldAircraftComponentList
            newAircraftComponentList.Add(thisAircraftComponent)
        Next
        Return newAircraftComponentList
    End Function

    Protected Function GetIssueCodeList() As fltLookupBaseList
        Dim totRows As Long = 0
        Dim newIssueCodeList As New fltLookupBaseList
        Dim newIssueCode As New fltLookupBase
        With newIssueCode
            .ID = 0
            .LookupName = "IssueCode"
            .StringValue = "N/A"
        End With
        newIssueCodeList.Add(newIssueCode)
        Dim oldIssueCodeList As fltLookupBaseList = fltLookupBaseManager.GetList("IssueCode", Me.ProgIdHiddenField.Value, 1, -1, -1, totRows)
        For Each thisAircraftComponent As fltLookupBase In oldIssueCodeList
            newIssueCodeList.Add(thisAircraftComponent)
        Next
        Return newIssueCodeList
    End Function

#End Region

#Region "FlightEngineRun Procedures"

    Private Sub BindData_FlightEngineRunDG()
        Me.FlightEngineRunDG.DataSource = _flight.FlightEngineRunList
        Me.FlightEngineRunDG.DataBind()
    End Sub

    Protected Sub FlightEngineRunDG_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles FlightEngineRunDG.ItemCommand

        Dim flightEngineRun As New FlightEngineRun

        Try
            Dim dg As DataGrid = CType(sender, DataGrid)

            Select Case e.CommandName

                Case "Add"

                    Dim runStartTime As String = CType(e.Item.FindControl("RunStartTimeTB"), TextBox).Text
                    Dim runEndTime As String = CType(e.Item.FindControl("RunEndTimeTB"), TextBox).Text
                    Dim engineSpeed As String = CType(e.Item.FindControl("EngineSpeedTB"), TextBox).Text

                    If runStartTime <> String.Empty Or runEndTime <> String.Empty Or engineSpeed <> String.Empty Then
                        flightEngineRun = New FlightEngineRun
                        With flightEngineRun
                            .FlightId = _flight.FlightId
                            .RunStartTime = Server.HtmlDecode(runStartTime)
                            .RunEndTime = Server.HtmlDecode(runEndTime)
                            .EngineSpeed = Server.HtmlDecode(engineSpeed)
                        End With
                        FlightManager.InsertFlightEngineRun(_flight, flightEngineRun)
                    End If

                Case "Edit"
                    dg.EditItemIndex = CInt(e.Item.ItemIndex)
                    Me.SaveButton.Enabled = False

                Case "Save"
                    flightEngineRun = New FlightEngineRun
                    With flightEngineRun
                        .FlightEngineRunId = CInt(CType(e.Item.FindControl("FlightEngineRunIdLB"), Label).Text)
                        .FlightId = _flight.FlightId
                        .RunStartTime = Server.HtmlDecode(CType(e.Item.FindControl("RunStartTimeTB"), TextBox).Text)
                        .RunEndTime = Server.HtmlDecode(CType(e.Item.FindControl("RunEndTimeTB"), TextBox).Text)
                        .EngineSpeed = Server.HtmlDecode(CType(e.Item.FindControl("EngineSpeedTB"), TextBox).Text)
                    End With
                    FlightManager.UpdateFlightEngineRun(_flight, flightEngineRun)
                    dg.EditItemIndex = -1
                    If Me.LiquidFillDG.EditItemIndex = -1 _
                    And Me.FlightCommentDG.EditItemIndex = -1 _
                    And Me.FlightEngineRunDG.EditItemIndex = -1 _
                    And Me.FlightRegionTimeDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If

                Case "Cancel"
                    dg.EditItemIndex = -1
                    If Me.LiquidFillDG.EditItemIndex = -1 _
                    And Me.FlightCommentDG.EditItemIndex = -1 _
                    And Me.FlightEngineRunDG.EditItemIndex = -1 _
                    And Me.FlightRegionTimeDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If

                Case "Delete"
                    Dim thisFlightEngineRunId = CInt(dg.DataKeys(e.Item.ItemIndex))
                    FlightManager.RemoveFlightEngineRun(_flight, thisFlightEngineRunId)

            End Select

            BindData_FlightEngineRunDG()

        Catch ex As Exception
            ProcessException(ex, flightEngineRun.ErrorsList, flightEngineRun.WarningsList)
            If e.CommandArgument = "OnSave" Then
                Throw New Exception(ex.Message)
            End If
        End Try

    End Sub

#End Region

#Region "FlightRegionTime Procedures"

    Private Sub BindData_FlightRegionTimeDG()
        Me.FlightRegionTimeDG.DataSource = _flight.FlightRegionTimeList
        Me.FlightRegionTimeDG.DataBind()
    End Sub

    Protected Sub FlightRegionTimeDG_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles FlightRegionTimeDG.ItemCommand

        Dim flightRegionTime As New FlightRegionTime

        Try
            Dim dg As DataGrid = CType(sender, DataGrid)

            Select Case e.CommandName

                Case "Add"
                    Dim regionId As String = Server.HtmlDecode(CType(e.Item.FindControl("RegionDDL"), DropDownList).SelectedValue)
                    Dim regionCode As String = CType(e.Item.FindControl("RegionDDL"), DropDownList).SelectedItem.ToString
                    Dim hours As String = CType(e.Item.FindControl("HoursTB"), TextBox).Text

                    If regionId <> "0" Or hours <> String.Empty Then
                        flightRegionTime = New FlightRegionTime
                        With flightRegionTime
                            .FlightId = _flight.FlightId
                            .RegionId = CInt(regionId)
                            .RegionCode = regionCode
                            .Hours = Server.HtmlDecode(hours)
                        End With
                        FlightManager.InsertFlightRegionTime(_flight, flightRegionTime)
                        BindData_FlightRegionTimeDG()
                    End If

                Case "Edit"
                    dg.EditItemIndex = CInt(e.Item.ItemIndex)
                    BindData_FlightRegionTimeDG()
                    Me.SaveButton.Enabled = False

                Case "Save"
                    flightRegionTime = New FlightRegionTime
                    With flightRegionTime
                        .FlightRegionTimeId = CInt(CType(e.Item.FindControl("FlightRegionTimeIdLB"), Label).Text)
                        .FlightId = _flight.FlightId
                        .RegionId = CType(e.Item.FindControl("RegionDDL"), DropDownList).SelectedValue
                        .RegionCode = CType(e.Item.FindControl("RegionDDL"), DropDownList).SelectedItem.ToString
                        .Hours = Server.HtmlDecode(CType(e.Item.FindControl("HoursTB"), TextBox).Text)
                    End With
                    FlightManager.UpdateFlightRegionTime(_flight, flightRegionTime)
                    dg.EditItemIndex = -1
                    BindData_FlightRegionTimeDG()
                    If Me.LiquidFillDG.EditItemIndex = -1 _
                    And Me.FlightCommentDG.EditItemIndex = -1 _
                    And Me.FlightEngineRunDG.EditItemIndex = -1 _
                    And Me.FlightRegionTimeDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If

                Case "Cancel"
                    dg.EditItemIndex = -1
                    BindData_FlightRegionTimeDG()
                    If Me.LiquidFillDG.EditItemIndex = -1 _
                    And Me.FlightCommentDG.EditItemIndex = -1 _
                    And Me.FlightEngineRunDG.EditItemIndex = -1 _
                    And Me.FlightRegionTimeDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If

                Case "Delete"
                    Dim thisFlightRegionTimeId = CInt(dg.DataKeys(e.Item.ItemIndex))
                    FlightManager.RemoveFlightRegionTime(_flight, thisFlightRegionTimeId)
                    BindData_FlightRegionTimeDG()

            End Select

        Catch ex As Exception
            ProcessException(ex, flightRegionTime.ErrorsList, flightRegionTime.WarningsList)
            If e.CommandArgument = "OnSave" Then
                Throw New Exception(ex.Message)
            End If
        End Try
    End Sub

    Protected Function GetRegionList() As RegionList
        Dim totRows As Long = 0
        Dim newRegionList As New RegionList
        newRegionList.Add(New Region(0, Me.ProgIdHiddenField.Value, "--", String.Empty, True, 0))
        Dim oldRegionList As RegionList = RegionManager.GetList(Me.ProgIdHiddenField.Value, 1, -1, -1, totRows, _fltIdentity)
        For Each thisRegion As Region In oldRegionList
            newRegionList.Add(thisRegion)
        Next
        Return newRegionList
    End Function

#End Region

    Protected Sub PilotSaveButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles PilotSaveButton.Click
        Dim newPilot As New fltLookupBase
        Try
            With newPilot
                .ProgId = Me.ProgIdHiddenField.Value
                .LookupName = "Pilot"
                .StringValue = Server.HtmlDecode(Me.PilotNameTB.Text)
                .SortOrder = Me.PilotSortOrderTB.Text
            End With
            Dim newPilotId As Integer = fltLookupBaseManager.Insert(newPilot, _fltIdentity)
            LoadDDL(Me.PilotDDL, Me.ProgIdHiddenField.Value, "Pilot")
            For Each thisItem As ListItem In Me.PilotDDL.Items
                If newPilotId = thisItem.Value Then
                    thisItem.Selected = True
                    Exit For
                End If
            Next
            Me.ParentChildMultiview.ActiveViewIndex = 1
        Catch ex As Exception
            ProcessException(ex, newPilot.BrokenRules, New BrokenRulesList)
        End Try
    End Sub

    Protected Sub PilotCancelButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles PilotCancelButton.Click
        Me.ParentChildMultiview.ActiveViewIndex = 1
    End Sub

#Region "Private Procedures"

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
        ProcessException(ex, _flight.ErrorsList, _flight.WarningsList)
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
            Me.WarningsCheckList.Items.Clear()
            For Each thisWarning As BrokenRule In WarningsList
                Dim listItem As New ListItem(thisWarning.Message, thisWarning.RuleIdentifier)
                If thisWarning.OverRideRule Then
                    listItem.Selected = True
                End If
                Me.WarningsCheckList.Items.Add(listItem)
            Next
        End If
    End Sub

    Private Sub ProcessWarnings(ByVal WarningsList As BrokenRulesList)
        If WarningsList.Count <> 0 Then
            For Each thisItem As ListItem In Me.WarningsCheckList.Items
                If thisItem.Selected Then
                    For Each thisWarning As BrokenRule In WarningsList
                        If thisItem.Value = thisWarning.RuleIdentifier Then
                            thisWarning.OverRideRule = True
                        End If
                    Next
                End If
            Next
        End If
    End Sub

    Private Function SaveAllPendingChildDataGridAdds() As Boolean

        Dim retValue As Boolean = True

        Dim dgi As DataGridItem
        Dim orgArgs As CommandEventArgs
        Dim e As DataGridCommandEventArgs

        Try

            dgi = Me.LiquidFillDG.Controls(0).Controls(Me.LiquidFillDG.Controls(0).Controls.Count - 1)
            orgArgs = New CommandEventArgs("Add", "OnSave")
            e = New DataGridCommandEventArgs(dgi, Me.LiquidFillDG, orgArgs)
            LiquidFillDG_ItemCommand(Me.LiquidFillDG, e)

            dgi = Me.FlightCommentDG.Controls(0).Controls(Me.FlightCommentDG.Controls(0).Controls.Count - 1)
            orgArgs = New CommandEventArgs("Add", "OnSave")
            e = New DataGridCommandEventArgs(dgi, Me.FlightCommentDG, orgArgs)
            FlightCommentDG_ItemCommand(Me.FlightCommentDG, e)

            dgi = Me.FlightEngineRunDG.Controls(0).Controls(Me.FlightEngineRunDG.Controls(0).Controls.Count - 1)
            orgArgs = New CommandEventArgs("Add", "OnSave")
            e = New DataGridCommandEventArgs(dgi, Me.FlightEngineRunDG, orgArgs)
            FlightEngineRunDG_ItemCommand(Me.FlightEngineRunDG, e)

            dgi = Me.FlightRegionTimeDG.Controls(0).Controls(Me.FlightRegionTimeDG.Controls(0).Controls.Count - 1)
            orgArgs = New CommandEventArgs("Add", "OnSave")
            e = New DataGridCommandEventArgs(dgi, Me.FlightRegionTimeDG, orgArgs)
            FlightRegionTimeDG_ItemCommand(Me.FlightRegionTimeDG, e)

        Catch ex As Exception
            retValue = False
        End Try

        Return retValue

    End Function

#End Region

End Class