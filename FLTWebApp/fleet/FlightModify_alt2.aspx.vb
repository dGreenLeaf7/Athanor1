' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports System.Transactions

Partial Public Class FlightModify_alt2
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
                Me.LocationAddDDL.Enabled = True
                Me.AircraftAddDDL.Enabled = True

            ElseIf (_pageMode = "edit") Then

                Me.PageTitleLB.Text = "Editing an Existing Flight"
                _flight = FlightManager.GetItem(_flightId, _fltIdentity)
                _flightKeyInfo = FlightKeyInfoManager.GetItem(_flight.AircraftId, _flight.LocationId, _flight.FlightDateSort, _flight.WorkShiftIndicatorId, _fltIdentity)
                Me.ParentChildMultiview.ActiveViewIndex = 1

            End If

            PopulateDDLs(_fltIdentity.CurrentProgId, _flightKeyInfo.LocationId)
            LoadDataToForm()
            BindData_LiquidFillDG()
            BindData_FlightCommentDG()
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

    Protected Sub LocationAddDDL_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LocationAddDDL.SelectedIndexChanged
        Dim totRows As Long = 0
        Me.AircraftAddDDL.Items.Clear()
        If _pageMode = "add" Then
            Me.AircraftAddDDL.Items.Add(New ListItem("< Select >", 0))
        End If
        Dim aircraftInfoList As AircraftInfoList = AircraftInfoManager.GetList(Me.ProgIdHiddenField.Value, LocationAddDDL.SelectedValue, String.Empty, 1, 0, -1, -1, totRows, _fltIdentity)
        If Not (aircraftInfoList Is Nothing) Then
            For Each thisAircraftInfo As AircraftInfo In aircraftInfoList
                Me.AircraftAddDDL.Items.Add(New ListItem(thisAircraftInfo.ShipNbr, thisAircraftInfo.AircraftId))
            Next
        End If
        Me.SchedRouteDDL.Items.Clear()
        Me.FlownRouteDDL.Items.Clear()
        Me.SchedRouteDDL.Items.Add(New ListItem("< Select >", 0))
        Me.FlownRouteDDL.Items.Add(New ListItem("< Select >", 0))
        Dim routeList As RouteList = RouteManager.GetList(Me.ProgIdHiddenField.Value, LocationAddDDL.SelectedValue, 0, 1, 0, -1, -1, totRows, _fltIdentity)
        If Not (routeList Is Nothing) Then
            For Each thisRoute As Route In routeList
                Me.SchedRouteDDL.Items.Add(New ListItem(thisRoute.Name, thisRoute.RouteId))
                Me.FlownRouteDDL.Items.Add(New ListItem(thisRoute.Name, thisRoute.RouteId))
            Next
        End If
        Me.AircraftAddDDL.Focus()
    End Sub

    Protected Sub NextButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextButton.Click

        Try

            Dim AircraftId As Integer = CInt(Me.AircraftAddDDL.SelectedValue)
            Dim LocationId As Integer = CInt(Me.LocationAddDDL.SelectedValue)
            Dim FlightDateSort As String = Me.FlightDateAddTB.Text
            Dim WorkShiftId As Integer = CInt(Me.WorkShiftAddDDL.SelectedValue)

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
            BindData_FlightRegionTimeDG()

        Catch ex As Exception
            ProcessException(ex, _flightKeyInfo.ErrorsList, _flightKeyInfo.WarningsList)
        End Try

    End Sub

    Protected Sub EditButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditButton.Click
        Try
            Me.PageTitleLB.Text = "Editing an Existing Flight"
            _flight = FlightManager.GetItem(_flightKeyInfo.FlightId, _fltIdentity)

            Me.ParentChildMultiview.ActiveViewIndex = 1
            PopulateDDLs(_fltIdentity.CurrentProgId, _flightKeyInfo.LocationId)
            LoadDataToForm()
            BindData_LiquidFillDG()
            BindData_FlightCommentDG()
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
        Me.LocationAddDDL.Focus()
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
        ComputeTotalHours()
    End Sub

    Protected Sub HobbsEndTB_Change(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HobbsEndTB.TextChanged
        ComputeTotalHours()
    End Sub

    Private Sub ComputeTotalHours()
        If IsNumeric(Me.HobbsStartTB.Text) And IsNumeric(Me.HobbsEndTB.Text) Then
            Me.TotalHoursLB.Text = Format(CDec(Me.HobbsEndTB.Text) - CDec(Me.HobbsStartTB.Text), _meterMask)
            Me.HobbsStartTB.Text = Format(CDec(Me.HobbsStartTB.Text), _meterMask)
            Me.HobbsEndTB.Text = Format(CDec(Me.HobbsEndTB.Text), _meterMask)
        End If
    End Sub

    Private Sub PopulateDDLs(ByVal ProgId As Integer, ByVal LocationId As Integer)
        Dim totRows As Long = 0
        Me.LocationAddDDL.Items.Clear()
        If _pageMode = "add" Then
            Me.LocationAddDDL.Items.Add(New ListItem("< Select >", 0))
        End If
        Dim locationList As LocationList = LocationManager.GetList(ProgId, 0, 1, 0, -1, -1, totRows, _fltIdentity)
        If Not (locationList Is Nothing) Then
            For Each thisLocation As Location In locationList
                LocationAddDDL.Items.Add(New ListItem(thisLocation.LocationName, thisLocation.LocactionId))
            Next
        End If
        Me.LocationEditDDL.Items.Clear()
        If Not (locationList Is Nothing) Then
            For Each thisLocation As Location In locationList
                LocationEditDDL.Items.Add(New ListItem(thisLocation.LocationName, thisLocation.LocactionId))
            Next
        End If

        Me.AircraftAddDDL.Items.Clear()
        If _pageMode = "add" Then
            Me.AircraftAddDDL.Items.Add(New ListItem("< Select >", 0))
        End If
        Dim aircraftInfoList As AircraftInfoList = AircraftInfoManager.GetList(ProgId, 0, String.Empty, 1, 0, -1, -1, totRows, _fltIdentity)
        If Not (aircraftInfoList Is Nothing) Then
            For Each thisAircraftInfo As AircraftInfo In aircraftInfoList
                Me.AircraftAddDDL.Items.Add(New ListItem(thisAircraftInfo.ShipNbr, thisAircraftInfo.AircraftId))
            Next
        End If

        LoadDDL(Me.WorkShiftAddDDL, ProgId, "WorkShift")
        LoadDDL(Me.WorkShiftEditDDL, ProgId, "WorkShift")
        LoadDDL(Me.PilotDDL, ProgId, "Pilot")
        Me.SchedRouteDDL.Items.Clear()
        Me.FlownRouteDDL.Items.Clear()
        If _pageMode = "add" Then
            Me.SchedRouteDDL.Items.Add(New ListItem("< Select >", 0))
            Me.FlownRouteDDL.Items.Add(New ListItem("< Select >", 0))
        End If
        Dim routeList As RouteList = RouteManager.GetList(ProgId, LocationId, 0, 1, 0, -1, -1, totRows, _fltIdentity)
        If Not (routeList Is Nothing) Then
            For Each thisRoute As Route In routeList
                Me.SchedRouteDDL.Items.Add(New ListItem(thisRoute.Name, thisRoute.RouteId))
                Me.FlownRouteDDL.Items.Add(New ListItem(thisRoute.Name, thisRoute.RouteId))
            Next
        End If

    End Sub

    Private Sub LoadDDL(ByVal DDL As DropDownList, ByVal ProgId As Long, ByVal LookupName As String)
        Try
            Dim totRows As Long = 0
            DDL.Items.Clear()
            DDL.Items.Add(New ListItem("< Select >", 0))
            Dim thisLookupList As fltLookupBaseList = fltLookupBaseManager.GetList(LookupName, ProgId, 1, -1, -1, totRows)
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
            Me.MSNLB.Text = _flightKeyInfo.MSN
            Me.LocationEditDDL.SelectedValue = _flightKeyInfo.LocationId
            Me.SchedRouteDDL.SelectedValue = _flight.SchedRouteId
            Me.FlownRouteDDL.SelectedValue = _flight.FlownRouteId
            Me.FlightDateEditTB.Text = _flightKeyInfo.FlightDateSort
            Me.WorkShiftEditDDL.SelectedValue = _flightKeyInfo.WorkShift
            Me.PilotDDL.SelectedValue = _flight.PilotId
            Me.PreFlightFtiOKDDL.SelectedValue = _flight.PreFlightFtiOK
            Me.OilLevelDDL.SelectedValue = _flight.OilLevelIndicatorStart

            If _pageMode = "add" Then
                Me.HobbsStartTB.Text = Format(CDec(_flightKeyInfo.AirframeHoursCurrent) - CDec(_flightKeyInfo.AirframeHoursOffset), _meterMask)
                Me.HobbsStartTB.Enabled = True
            Else
                Me.HobbsStartTB.Text = _flight.HobbsStart
                Me.HobbsStartTB.Enabled = False
                Me.HobbsEndTB.Enabled = False
            End If

            Me.DepartTimeTB.Text = _flight.DepartureTime
            Me.ReturnTimeTB.Text = _flight.ReturnTime
            Me.HobbsEndTB.Text = _flight.HobbsEnd
            Me.PostFlightFtiOKDDL.SelectedValue = _flight.PostFlightFtiOK

        Catch ex As Exception
            ProcessException(ex)

        End Try

    End Sub

    Private Sub LoadDataToObject()

        Try
            _flight.FlightId = Me.FlightIdHiddenField.Value
            _flight.AircraftId = _flightKeyInfo.AircraftId
            _flight.LocationId = _flightKeyInfo.LocationId
            _flight.SchedRouteId = Me.SchedRouteDDL.SelectedValue
            _flight.FlownRouteId = Me.FlownRouteDDL.SelectedValue
            _flight.FlightDate = _flightKeyInfo.FlightDateSort
            _flight.WorkShiftIndicatorId = _flightKeyInfo.WorkShiftId
            _flight.PilotId = Me.PilotDDL.SelectedValue
            _flight.PreFlightFtiOK = Me.PreFlightFtiOKDDL.SelectedValue
            _flight.OilLevelIndicatorStart = Me.OilLevelDDL.SelectedValue
            _flight.UsedMeterOffset = _flightKeyInfo.AirframeHoursOffset
            _flight.DepartureTime = Me.DepartTimeTB.Text
            _flight.HobbsStart = Me.HobbsStartTB.Text
            _flight.ReturnTime = Me.ReturnTimeTB.Text
            _flight.HobbsEnd = Me.HobbsEndTB.Text
            _flight.MeterUnitId = _flightKeyInfo.MeterUnitId
            _flight.PostFlightFtiOK = Me.PostFlightFtiOKDDL.SelectedValue

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
                            .LiquidAmt = liquidAmt
                            If Not IsNumeric(hobbsReading) Then
                                .HobbsReading = Format((_flightKeyInfo.AirframeHoursCurrent - _flightKeyInfo.AirframeHoursOffset), _meterMask)
                            Else
                                .HobbsReading = hobbsReading
                            End If
                            .AirframeHoursReading = Format(airframeHoursReading, _meterMask)
                            .LiquidFillLocation = liquidFillLocation
                            .LiquidFillApprover = liquidFillApprover
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
                        .LiquidAmt = CType(e.Item.FindControl("LiquidAmtTB"), TextBox).Text
                        If Not IsNumeric(CType(e.Item.FindControl("LiquidAmtTB"), TextBox).Text) Then
                            .HobbsReading = Format(_flightKeyInfo.AirframeHoursCurrent, _meterMask)
                        End If
                        .HobbsReading = CType(e.Item.FindControl("HobbsTB"), TextBox).Text
                        .AirframeHoursReading = CDec(.HobbsReading) + _flightKeyInfo.AirframeHoursOffset
                        .LiquidFillLocation = CType(e.Item.FindControl("LocationTB"), TextBox).Text
                        .LiquidFillApprover = CType(e.Item.FindControl("ApproverTB"), TextBox).Text
                    End With
                    FlightManager.UpdateLiquidFill(_flight, liquidFill)
                    dg.EditItemIndex = -1
                    BindData_LiquidFillDG()
                    If Me.LiquidFillDG.EditItemIndex = -1 _
                    And Me.FlightCommentDG.EditItemIndex = -1 _
                    And Me.FlightRegionTimeDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If

                Case "Cancel"
                    dg.EditItemIndex = -1
                    BindData_LiquidFillDG()
                    If Me.LiquidFillDG.EditItemIndex = -1 _
                    And Me.FlightCommentDG.EditItemIndex = -1 _
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
                    Dim pilotComment As String = CType(e.Item.FindControl("PilotCommentTB"), TextBox).Text
                    Dim commentRating As String = CType(e.Item.FindControl("CommentRatingDDL"), DropDownList).SelectedValue

                    If componentId <> "0" Or commentTime <> String.Empty _
                    Or pilotComment <> String.Empty Or commentRating <> "0" Then
                        With _flightComment
                            .FlightId = _flight.FlightId
                            .ComponentId = CInt(componentId)
                            .ComponentName = componentName
                            .IssueCodeId = issueCodeId
                            .IssueCode = issueCode
                            .CommentTime = commentTime
                            .PilotComment = pilotComment
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
                        .CommentTime = CType(e.Item.FindControl("CommentTimeTB"), TextBox).Text
                        .PilotComment = CType(e.Item.FindControl("PilotCommentTB"), TextBox).Text
                        .CommentRating = CType(e.Item.FindControl("CommentRatingDDL"), DropDownList).SelectedValue
                    End With
                    FlightManager.UpdateFlightComment(_flight, _flightComment)
                    dg.EditItemIndex = -1
                    _flightComment = New FlightComment
                    ClearErrorFields()
                    BindData_FlightCommentDG()
                    If Me.LiquidFillDG.EditItemIndex = -1 _
                    And Me.FlightCommentDG.EditItemIndex = -1 _
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
                    Dim regionId As String = CType(e.Item.FindControl("RegionDDL"), DropDownList).SelectedValue
                    Dim regionCode As String = CType(e.Item.FindControl("RegionDDL"), DropDownList).SelectedItem.ToString
                    Dim hours As String = CType(e.Item.FindControl("HoursTB"), TextBox).Text

                    If regionId <> "0" Or hours <> String.Empty Then
                        flightRegionTime = New FlightRegionTime
                        With flightRegionTime
                            .FlightId = _flight.FlightId
                            .RegionId = CInt(regionId)
                            .RegionCode = regionCode
                            .Hours = hours
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
                        .Hours = CType(e.Item.FindControl("HoursTB"), TextBox).Text
                    End With
                    FlightManager.UpdateFlightRegionTime(_flight, flightRegionTime)
                    dg.EditItemIndex = -1
                    BindData_FlightRegionTimeDG()
                    If Me.LiquidFillDG.EditItemIndex = -1 _
                    And Me.FlightCommentDG.EditItemIndex = -1 _
                    And Me.FlightRegionTimeDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If

                Case "Cancel"
                    dg.EditItemIndex = -1
                    BindData_FlightRegionTimeDG()
                    If Me.LiquidFillDG.EditItemIndex = -1 _
                    And Me.FlightCommentDG.EditItemIndex = -1 _
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
                .StringValue = Me.PilotNameTB.Text
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