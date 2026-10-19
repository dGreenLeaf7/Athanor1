' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports Microsoft.Security.Application

Partial Public Class AircraftModify
    Inherits System.Web.UI.Page
#Region "Page Constants"

    Private Const _aircraftListUrl As String = "~/fleet/FleetList.aspx"
    Private Const _flightModifyUrl As String = "~/fleet/FlightModify.aspx"
    Private Const _workOrderModifyUrl As String = "~/fleet/WorkOrderModify.aspx"

#End Region

#Region "Private Variables"

    Private _fltIdentity As FltIdentity = Nothing

    Private _aircraft As Aircraft = Nothing
    Private _testsNotAssigned As fltLookupBaseList = Nothing
    Private _pageMode As String = String.Empty

#End Region

#Region "Page Procedures"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        Me.ProgIdHiddenField.Value = _fltIdentity.CurrentProgId

        ClearErrorFields()

        If Not Me.IsPostBack Then

            _pageMode = LCase(Request.QueryString("mode"))
            If _pageMode = "edit" Then
                Me.AddEditTitleLabel.Text = "Editing Aircraft"
                Dim aircraftId As Integer = Request.QueryString("aircraftid")
                _aircraft = AircraftManager.GetItem(aircraftId, _fltIdentity)
            Else
                Me.AddEditTitleLabel.Text = "Adding New Aircraft"
                _aircraft = New Aircraft
            End If

            _testsNotAssigned = AircraftTestXrefManager.GetTestListNotForAircraft(_aircraft.AircraftId, Me.ProgIdHiddenField.Value)

            PopulateDropDownLists()
            LoadDataToForm()
            BindData_AircraftLocationDG()
            BindData_AircraftFlightsGridView()
            BindData_AircraftWorkOrdersGridView()
            BindData_AircraftConfigGV()
            BindData_TestsAssignedDG()
            BindData_TestsNotAssignedDG()
            BindData_AircraftCommentsDG()

        Else

            _pageMode = CType(ViewState("_pageMode"), String)
            _aircraft = CType(ViewState("_aircraft"), Aircraft)
            _testsNotAssigned = CType(ViewState("_testsNotAssigned"), fltLookupBaseList)

        End If

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_aircraft") = _aircraft
        ViewState("_testsNotAssigned") = _testsNotAssigned
        ViewState("_pageMode") = _pageMode
    End Sub

    Protected Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click

        Try
            If Not SaveAllPendingChildDataGridAdds() Then Exit Sub

            LoadDataToObject()
            ProcessWarnings()
            ClearErrorFields()
            _aircraft.ProgId = Me.ProgIdHiddenField.Value
            AircraftManager.Save(_aircraft, _fltIdentity)
            _aircraft = Nothing
            ClearErrorFields()
            Response.Redirect(_aircraftListUrl, False)

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Response.Redirect(_aircraftListUrl, False)
    End Sub

    Private Sub PopulateDropDownLists()
        Dim totRows As Long = 0
        Dim progId As Long = Me.ProgIdHiddenField.Value
        LoadDDL(Me.ManufacturerDDL, progId, "Manufacturer")
        LoadDDL(Me.ModelDDL, progId, "Model")
        LoadDDL(Me.AircraftStatusDDL, progId, "AircraftStatusDef")
        LoadDDL(Me.EngineerDDL, progId, "Engineer")
        LoadDDL(Me.MechanicDDL, progId, "Mechanic")
        LoadDDL(Me.EngineTypeDDL, progId, "EngineType")
        LoadDDL(Me.EngineMaturityDDL, progId, "MaturityDef")
        LoadDDL(Me.EngineEmissionDDL, progId, "Emission")
        LoadDDL(Me.PropellerTypeDDL, progId, "PropellerType")
        LoadDDL(Me.APUTypeDDL, progId, "APUType")
        LoadDDL(Me.APUMaturityDDL, progId, "MaturityDef")
        LoadDDL(Me.APUEmissionDDL, progId, "Emission")
        LoadDDL(Me.ConfigModelDDL, progId, "Model")
        LoadDDL(Me.ConfigMeterUnits, progId, "LengthUnit")

    End Sub

    Private Sub LoadDDL(ByVal DDL As DropDownList, ByVal ProgId As Long, ByVal LookupName As String)
        Dim totRows As Long = 0
        DDL.Items.Clear()
        DDL.Items.Add(New ListItem("< Select >", 0))
        Dim thisLookupList As fltLookupBaseList = fltLookupBaseManager.GetList(LookupName, ProgId, 1, -1, -1, totRows)
        If Not (thisLookupList Is Nothing) Then
            For Each thisLookup As fltLookupBase In thisLookupList
                DDL.Items.Add(New ListItem(thisLookup.StringValue, thisLookup.ID))
            Next
        Else
            Throw New Exception("Why is the list empty?")
        End If
    End Sub

    Private Sub LoadDataToForm()
        ClearErrorFields()
        PopulateDropDownLists()

        Me.MSNTB.Text = Server.HtmlEncode(_aircraft.MSN)
        Me.ShipNbrTB.Text = Server.HtmlEncode(_aircraft.ShipNbr)
        Me.SerialNbrTB.Text = Server.HtmlEncode(_aircraft.SerialNbr)
        Me.RegistrationTB.Text = Server.HtmlEncode(_aircraft.RegistrationNbr)

        Me.ManufacturerDDL.SelectedValue = _aircraft.ManufacturerId
        Me.ModelDDL.SelectedValue = _aircraft.ModelId
        Me.LiveryColorTB.Text = Server.HtmlEncode(_aircraft.LiveryColor)
        Me.AircraftStatusDDL.SelectedValue = _aircraft.AircraftStatusId

        Me.DescriptionTB.Text = Server.HtmlEncode(_aircraft.Description)
        Me.AircraftEngineHoursCurrentTB.Text = Server.HtmlEncode(_aircraft.AircraftEngineHoursCurrent)

        Me.CurrentLocationLB.Text = Server.HtmlEncode(_aircraft.CurrentLocation)
        Me.EngineerDDL.SelectedValue = _aircraft.EngineerId
        Me.MechanicDDL.SelectedValue = _aircraft.MechanicId
        Me.MeterUnitsLB.Text = Server.HtmlEncode(_aircraft.MeterUnit)

        Me.MeterStartLB.Text = Server.HtmlEncode(_aircraft.AirframeHoursStart)
        Me.CurrentMeterOffsetLB.Text = Server.HtmlEncode(_aircraft.AirframeHoursOffset)
        Me.MeterCurrentLB.Text = Server.HtmlEncode(_aircraft.AirframeHoursCurrent)

        Me.EstEndOfTestingTB.Text = Server.HtmlEncode(_aircraft.EstEndOfTesting)
        Me.ActEndOfTestingTB.Text = Server.HtmlEncode(_aircraft.ActEndOfTesting)
        Me.TotalRequiredHoursTB.Text = Server.HtmlEncode(_aircraft.TotalRequiredUsage)
        Me.CurrentHoursLB.Text = Server.HtmlEncode(_aircraft.CurrentFlightUsage)

    End Sub

    Private Sub LoadDataToObject()
        ClearErrorFields()

        _aircraft.MSN = Server.HtmlDecode(Me.MSNTB.Text)
        _aircraft.ShipNbr = Server.HtmlDecode(Me.ShipNbrTB.Text)
        _aircraft.SerialNbr = Server.HtmlDecode(Me.SerialNbrTB.Text)
        _aircraft.RegistrationNbr = Server.HtmlDecode(Me.RegistrationTB.Text)

        _aircraft.ManufacturerId = Me.ManufacturerDDL.SelectedValue
        _aircraft.ModelId = Me.ModelDDL.SelectedValue
        _aircraft.LiveryColor = Server.HtmlDecode(Me.LiveryColorTB.Text)
        _aircraft.AircraftStatusId = Me.AircraftStatusDDL.SelectedValue

        _aircraft.Description = Server.HtmlDecode(Me.DescriptionTB.Text)
        _aircraft.AircraftEngineHoursCurrent = Server.HtmlDecode(Me.AircraftEngineHoursCurrentTB.Text)

        _aircraft.EngineerId = Me.EngineerDDL.SelectedValue
        _aircraft.MechanicId = Me.MechanicDDL.SelectedValue

        _aircraft.EstEndOfTesting = Server.HtmlDecode(Me.EstEndOfTestingTB.Text)
        _aircraft.ActEndOfTesting = Server.HtmlDecode(Me.ActEndOfTestingTB.Text)
        _aircraft.TotalRequiredUsage = Server.HtmlDecode(Me.TotalRequiredHoursTB.Text)

    End Sub

#End Region

#Region "AircraftLocation DataGrid"

    Protected Sub BindData_AircraftLocationDG()
        Me.AircraftLocationsDG.DataSource = _aircraft.AircraftLocationList
        Me.AircraftLocationsDG.DataBind()
    End Sub

    Protected Sub AircraftLocationDG_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles AircraftLocationsDG.ItemCommand

        Dim aircraftLocation As New AircraftLocation

        Try
            Dim dg As DataGrid = CType(sender, DataGrid)

            Select Case LCase(e.CommandName)

                Case "add"
                    Dim locationId As String = CType(e.Item.FindControl("LocationDDL"), DropDownList).SelectedValue
                    Dim locationName As String = CType(e.Item.FindControl("LocationDDL"), DropDownList).SelectedItem.Text
                    Dim arrivalDate As String = CType(e.Item.FindControl("ArrivalDateTB"), TextBox).Text
                    Dim requiredUsage As String = CType(e.Item.FindControl("RequiredUsageTB"), TextBox).Text
                    Dim estEndOfTest As String = CType(e.Item.FindControl("EstEndOfTestTB"), TextBox).Text
                    Dim actEndOfTest As String = CType(e.Item.FindControl("ActEndOfTestTB"), TextBox).Text
                    Dim lastLocation As Boolean = CType(e.Item.FindControl("CurrentLocationCB"), CheckBox).Checked

                    If locationId <> "0" Or arrivalDate <> String.Empty Or requiredUsage <> String.Empty _
                    Or estEndOfTest <> String.Empty Or actEndOfTest <> String.Empty Then
                        With aircraftLocation
                            .AircraftId = _aircraft.AircraftId
                            .LocationId = locationId
                            .LocationName = locationName
                            .ArrivalDate = Server.HtmlDecode(arrivalDate)
                            .RequiredUsage = Server.HtmlDecode(requiredUsage)
                            .EstEndOfTest = Server.HtmlDecode(estEndOfTest)
                            .ActEndOfTest = Server.HtmlDecode(actEndOfTest)
                            .LastLocation = lastLocation
                        End With
                        AircraftManager.InsertAircraftLocation(_aircraft, aircraftLocation)
                        BindData_AircraftLocationDG()
                    End If

                Case "edit"
                    dg.EditItemIndex = CInt(e.Item.ItemIndex)
                    BindData_AircraftLocationDG()
                    Me.SaveButton.Enabled = False

                Case "save"
                    With aircraftLocation
                        .AircraftLocationId = CType(e.Item.FindControl("AircraftLocationIdLB"), Label).Text
                        .AircraftId = _aircraft.AircraftId
                        .LocationId = CType(e.Item.FindControl("LocationDDL"), DropDownList).SelectedValue
                        .LocationName = CType(e.Item.FindControl("LocationDDL"), DropDownList).SelectedItem.Text
                        .ArrivalDate = Server.HtmlDecode(CType(e.Item.FindControl("ArrivalDateTB"), TextBox).Text)
                        .RequiredUsage = Server.HtmlDecode(CType(e.Item.FindControl("RequiredUsageTB"), TextBox).Text)
                        .EstEndOfTest = Server.HtmlDecode(CType(e.Item.FindControl("EstEndOfTestTB"), TextBox).Text)
                        .ActEndOfTest = Server.HtmlDecode(CType(e.Item.FindControl("ActEndOfTestTB"), TextBox).Text)
                        .LastLocation = CType(e.Item.FindControl("CurrentLocationCB"), CheckBox).Checked
                    End With
                    AircraftManager.UpdateAircraftLocation(_aircraft, aircraftLocation)
                    dg.EditItemIndex = -1
                    BindData_AircraftLocationDG()
                    Me.SaveButton.Enabled = True

                Case "cancel"
                    dg.EditItemIndex = -1
                    BindData_AircraftLocationDG()
                    Me.SaveButton.Enabled = True

                Case "delete"
                    Dim aircraftLocationId As Integer = CType(e.Item.FindControl("AircraftLocationIdLB"), Label).Text
                    AircraftManager.RemoveAircraftLocation(_aircraft, aircraftLocationId)
                    BindData_AircraftLocationDG()

            End Select

        Catch ex As Exception
            ProcessException(ex, aircraftLocation.ErrorsList, aircraftLocation.WarningsList)
            If e.CommandArgument = "OnSave" Then
                Throw New Exception(ex.Message)
            End If
        End Try

    End Sub

    Protected Function GetLocations(Optional ByVal LoadEmptySelectItem As Boolean = False) As LocationList
        Dim totRows As Long = 0
        Dim newLocationList As New LocationList
        Try
            If LoadEmptySelectItem Then
                newLocationList.Add(New Location(0, Me.ProgIdHiddenField.Value, 0, String.Empty, 0, False, 0, String.Empty, String.Empty, "< Select >", 1, 0, Date.Now, Date.Now, String.Empty, String.Empty))
            End If
            Dim oldLocationList As LocationList = LocationManager.GetList(Me.ProgIdHiddenField.Value, _fltIdentity)
            For Each thisLocation As Location In oldLocationList
                newLocationList.Add(thisLocation)
            Next
        Catch ex As Exception
            ProcessException(ex)
        End Try
        Return newLocationList
    End Function

#End Region

#Region "AircraftFlights GridView"

    Protected Sub BindData_AircraftFlightsGridView()
        Me.AircraftFlightsGridView.DataSource = _aircraft.FlightInfoList
        Me.AircraftFlightsGridView.DataBind()
    End Sub

    Protected Sub AircraftFlightsGridView_PageIndexChanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles AircraftFlightsGridView.PageIndexChanging
        Dim gv As GridView = CType(sender, GridView)
        gv.PageIndex = e.NewPageIndex
        BindData_AircraftFlightsGridView()
    End Sub

    Protected Sub AircraftFlightsGridView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AircraftFlightsGridView.SelectedIndexChanged

        Dim stringBuilder As New StringBuilder

        With stringBuilder
            .Append(_flightModifyUrl)
            .Append("?mode=edit")
            .Append("&flightid=")
            .Append(Encoder.UrlEncode(Me.AircraftFlightsGridView.SelectedValue.ToString))
            .Append("&aircraftid=")
            .Append(_aircraft.AircraftId)
        End With

        Response.Redirect(stringBuilder.ToString, False)

    End Sub

#End Region

#Region "AircraftWorkOrders GridView"

    Protected Sub BindData_AircraftWorkOrdersGridView()
        Me.AircraftWorkOrdersGridView.DataSource = _aircraft.WorkOrderInfoList
        Me.AircraftWorkOrdersGridView.DataBind()
    End Sub

    Protected Sub AircraftWorkOrdersGridView_PageIndexChanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles AircraftWorkOrdersGridView.PageIndexChanging
        Dim gv As GridView = CType(sender, GridView)
        gv.PageIndex = e.NewPageIndex
        BindData_AircraftWorkOrdersGridView()
    End Sub

    Protected Sub AircraftWorkOrdersGridView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AircraftWorkOrdersGridView.SelectedIndexChanged

        Dim stringBuilder As New StringBuilder

        With stringBuilder
            .Append(_workOrderModifyUrl)
            .Append("?mode=edit")
            .Append("&workorderid=")
            .Append(Encoder.UrlEncode(Me.AircraftWorkOrdersGridView.SelectedValue.ToString))
            .Append("&aircraftid=")
            .Append(Encoder.UrlEncode(_aircraft.AircraftId.ToString))
        End With

        Response.Redirect(stringBuilder.ToString, False)

    End Sub

#End Region

#Region "Tests DataGrid"

    Protected Sub BindData_TestsAssignedDG()
        Me.TestsAssignedDG.DataSource = _aircraft.TestList
        Me.TestsAssignedDG.DataBind()
    End Sub

    Protected Sub BindData_TestsNotAssignedDG()
        Me.TestsNotAssignedDG.DataSource = _testsNotAssigned
        Me.TestsNotAssignedDG.DataBind()
    End Sub

    Protected Sub TestsAssignedDG_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles TestsAssignedDG.ItemCommand

        Try

            Dim dg As DataGrid = CType(sender, DataGrid)

            Select Case LCase(e.CommandName)

                Case "remove"
                    Dim newTest As New fltLookupBase
                    With newTest
                        .ID = CType(e.Item.FindControl("TestIdLB"), Label).Text
                        .ProgId = Me.ProgIdHiddenField.Value
                        .StringValue = CType(e.Item.FindControl("TestDescriptionLB"), Label).Text
                        .SortOrder = CType(e.Item.FindControl("SortOrderLB"), Label).Text
                    End With
                    AircraftManager.RemoveTest(_aircraft, newTest)
                    _testsNotAssigned.Add(newTest)
                    BindData_TestsAssignedDG()
                    BindData_TestsNotAssignedDG()

            End Select

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Protected Sub TestsNotAssignedDG_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles TestsNotAssignedDG.ItemCommand

        Try

            Dim dg As DataGrid = CType(sender, DataGrid)

            Select Case LCase(e.CommandName)

                Case "assign"
                    Dim newTest As New fltLookupBase
                    With newTest
                        .ID = CType(e.Item.FindControl("TestIdLB"), Label).Text
                        .ProgId = Me.ProgIdHiddenField.Value
                        .StringValue = CType(e.Item.FindControl("TestDescriptionLB"), Label).Text
                        .SortOrder = CType(e.Item.FindControl("SortOrderLB"), Label).Text
                    End With
                    AircraftManager.InsertTest(_aircraft, newTest)
                    For Each thisTest As fltLookupBase In _testsNotAssigned
                        If thisTest.ID = newTest.ID Then
                            _testsNotAssigned.Remove(thisTest)
                            Exit For
                        End If
                    Next
                    BindData_TestsAssignedDG()
                    BindData_TestsNotAssignedDG()

            End Select

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

#End Region

#Region "AircraftConfiguration GridView"

    Protected Sub BindData_AircraftConfigGV()
        AircraftConfigGV.DataSource = _aircraft.AircraftConfigurationList
        AircraftConfigGV.DataBind()
    End Sub

    Protected Sub AircraftConfigGV_PageIndexChanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles AircraftConfigGV.PageIndexChanging
        Dim gv As GridView = CType(sender, GridView)
        gv.PageIndex = e.NewPageIndex
        BindData_AircraftConfigGV()
    End Sub

    Protected Sub AircraftConfigGV_OnRowDeleting(ByVal sender As System.Object, ByVal e As GridViewDeleteEventArgs) Handles AircraftConfigGV.RowDeleting
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            Dim AircraftConfigurationId As Integer = CInt(CType(row.FindControl("AircraftConfigurationIdLB"), Label).Text)
            AircraftManager.RemoveAircraftConfiguration(_aircraft, AircraftConfigurationId)
            BindData_AircraftConfigGV()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub AircraftConfigVG_SelectedIndexChange(ByVal sender As System.Object, ByVal e As GridViewSelectEventArgs) Handles AircraftConfigGV.SelectedIndexChanging
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim aircraftConfigId As Integer = CInt(CType(gv.Rows.Item(e.NewSelectedIndex).FindControl("AircraftConfigurationIdLB"), Label).Text)

            For Each thisAircraftConfiguration As AircraftConfiguration In _aircraft.AircraftConfigurationList
                If thisAircraftConfiguration.AircraftConfigurationId = aircraftConfigId Then
                    Me.AircraftConfigurationsMultiView.ActiveViewIndex = 1
                    LoadAircraftConfigDataToForm(thisAircraftConfiguration)
                    Me.SaveButton.Enabled = False
                    Exit Sub
                End If
            Next
            Throw New Exception("Aircraft Configuration not found in Aircraft object")
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub AddAircraftConfigButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddAircraftConfigButton.Click
        Try
            Me.AircraftConfigurationsMultiView.ActiveViewIndex = 1
            Dim latestAircraftConfiguration As AircraftConfiguration = AircraftManager.RetrieveLatestAircraftConfiguration(_aircraft, _fltIdentity)
            Dim thisAircraftConfiguration As AircraftConfiguration = AircraftManager.CloneAircraftConfiguration(latestAircraftConfiguration, True)
            LoadAircraftConfigDataToForm(thisAircraftConfiguration)
            Me.SaveButton.Enabled = False
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub CancelAircraftConfigButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CancelAircraftConfigButton.Click
        Me.AircraftConfigurationsMultiView.ActiveViewIndex = 0
        Me.SaveButton.Enabled = True
        BindData_AircraftConfigGV()
    End Sub

    Protected Sub SaveAircraftConfigButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles SaveAircraftConfigButton.Click
        Dim thisAircraftConfiguration As New AircraftConfiguration
        Try
            LoadAircraftConfigDataToObject(thisAircraftConfiguration)
            If thisAircraftConfiguration.AircraftConfigurationId = 0 Then
                AircraftManager.InsertAircraftConfiguration(_aircraft, thisAircraftConfiguration)
            Else
                AircraftManager.UpdateAircraftConfiguration(_aircraft, thisAircraftConfiguration)
            End If
            BindData_AircraftConfigGV()
            Me.AircraftConfigurationsMultiView.ActiveViewIndex = 0
            Me.SaveButton.Enabled = True
            Me.CurrentMeterOffsetLB.Text = _aircraft.AirframeHoursOffset
        Catch ex As Exception
            ProcessException(ex, thisAircraftConfiguration.ErrorsList, thisAircraftConfiguration.WarningsList)
        End Try
    End Sub

    Private Sub LoadAircraftConfigDataToObject(ByVal thisAircraftConfiguration As AircraftConfiguration)
        Try
            With thisAircraftConfiguration
                .AircraftConfigurationId = Me.AircraftConfigurationIdHF.Value
                .AircraftId = _aircraft.AircraftId
                .ConfigurationStartDate = Server.HtmlDecode(Me.ConfigurationStartDateTB.Text)
                .ConfigurationEndDate = Server.HtmlDecode(Me.ConfigurationEndDateTB.Text)
                .ConfigurationMeterStart = Server.HtmlDecode(Me.ConfigurationMeterStartTB.Text)
                .ConfigurationMeterOffset = Server.HtmlDecode(Me.ConfigurationMeterOffsetTB.Text)
                .ConfigurationDescription = Server.HtmlDecode(Me.ConfigurationDescriptionTB.Text)
                .ModelId = Me.ConfigModelDDL.SelectedValue
                .MeterUnitId = Me.ConfigMeterUnits.SelectedValue
                .EngineTypeId = Me.EngineTypeDDL.SelectedValue
                .EngineType = Me.EngineTypeDDL.SelectedItem.Text
                .EngineMaturityId = Me.EngineMaturityDDL.SelectedValue
                .EngineMaturity = Me.EngineMaturityDDL.SelectedItem.Text
                .EngineEmissionId = Me.EngineEmissionDDL.SelectedValue
                .EngineEmission = Me.EngineEmissionDDL.SelectedItem.Text
                .EngineSerialNbr = Server.HtmlDecode(Me.EngineSerialNbrTB.Text)
                .APUTypeId = Me.APUTypeDDL.SelectedValue
                .APUType = Me.APUTypeDDL.SelectedItem.Text
                .APUEmissionId = Me.APUEmissionDDL.SelectedValue
                .APUEmission = Me.APUEmissionDDL.SelectedItem.Text
                .APUMaturityId = Me.APUMaturityDDL.SelectedValue
                .APUMaturity = Me.APUMaturityDDL.SelectedItem.Text
                .APUSerialNbr = Server.HtmlDecode(Me.APUSerialNbrTB.Text)
                .PropellerTypeId = Me.PropellerTypeDDL.SelectedValue
                .PropellerType = Server.HtmlDecode(Me.PropellerTypeDDL.SelectedItem.Text)
                .PropellerModelNbr = Server.HtmlDecode(Me.PropellerModelNbrTB.Text)
                .PropellerSerialNo = Server.HtmlDecode(Me.PropellerSerialNbrTB.Text)
                .NoseGear = Server.HtmlDecode(Me.NoseGearTB.Text)
                .MainGear = Server.HtmlDecode(Me.MainGearTB.Text)
                .AvionicsVersion = Server.HtmlDecode(Me.AvionicsVersionTB.Text)
                .EmissionLevel = Server.HtmlDecode(Me.EmissionLevelTB.Text)
            End With
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub LoadAircraftConfigDataToForm(ByVal thisAircraftConfiguration As AircraftConfiguration)
        Try
            With thisAircraftConfiguration
                Me.AircraftConfigurationIdHF.Value = .AircraftConfigurationId
                Me.ConfigurationStartDateTB.Text = Server.HtmlEncode(.ConfigurationStartDate)
                Me.ConfigurationEndDateTB.Text = Server.HtmlEncode(.ConfigurationEndDate)
                If thisAircraftConfiguration.AircraftConfigurationId > 0 Then
                    Me.ConfigurationStartDateTB.Enabled = False
                    Me.ConfigurationMeterStartTB.Enabled = False
                    Me.ConfigurationMeterStartTB.Text = Server.HtmlEncode(.ConfigurationMeterStart)
                    Me.ConfigurationMeterOffsetTB.Enabled = False
                    Me.ConfigurationMeterOffsetTB.Text = Server.HtmlEncode(.ConfigurationMeterOffset)
                Else
                    Me.ConfigurationStartDateTB.Enabled = True
                    Me.ConfigurationStartDateTB.Text = Format(Date.Now(), "MM/dd/yyyy")
                    If _pageMode = "edit" Then
                        Me.ConfigurationMeterStartTB.Enabled = False
                        Me.ConfigurationMeterStartTB.Text = Server.HtmlEncode(_aircraft.AirframeHoursCurrent)
                    Else
                        Me.ConfigurationMeterStartTB.Enabled = True
                        Me.ConfigurationMeterStartTB.Text = Server.HtmlEncode(.ConfigurationMeterStart)
                    End If
                    Me.ConfigurationMeterOffsetTB.Enabled = True
                    Me.ConfigurationMeterOffsetTB.Text = Server.HtmlEncode(.ConfigurationMeterOffset)
                End If
                Me.ConfigurationMeterEndTB.Enabled = False
                Me.ConfigurationMeterEndTB.Text = Server.HtmlEncode(.ConfigurationMeterEnd)

                Me.ConfigurationDescriptionTB.Text = Server.HtmlEncode(.ConfigurationDescription)
                Me.ConfigModelDDL.SelectedValue = .ModelId
                Me.ConfigMeterUnits.SelectedValue = .MeterUnitId
                Me.EngineTypeDDL.SelectedValue = .EngineTypeId
                Me.EngineMaturityDDL.SelectedValue = .EngineMaturityId
                Me.EngineEmissionDDL.SelectedValue = .EngineEmissionId
                Me.EngineSerialNbrTB.Text = Server.HtmlEncode(.EngineSerialNbr)
                Me.APUTypeDDL.SelectedValue = .APUTypeId
                Me.APUEmissionDDL.SelectedValue = .APUEmissionId
                Me.APUMaturityDDL.SelectedValue = .APUMaturityId
                Me.APUSerialNbrTB.Text = Server.HtmlEncode(.APUSerialNbr)
                Me.PropellerTypeDDL.SelectedValue = .PropellerTypeId
                Me.PropellerModelNbrTB.Text = Server.HtmlEncode(.PropellerModelNbr)
                Me.PropellerSerialNbrTB.Text = Server.HtmlEncode(.PropellerSerialNo)
                Me.NoseGearTB.Text = Server.HtmlEncode(.NoseGear)
                Me.MainGearTB.Text = Server.HtmlEncode(.MainGear)
                Me.AvionicsVersionTB.Text = Server.HtmlEncode(.AvionicsVersion)
                Me.EmissionLevelTB.Text = Server.HtmlEncode(.EmissionLevel)
            End With
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub ProcessWarnings()
        If _aircraft.WarningsList.Count <> 0 Then
            For Each thisItem As ListItem In Me.WarningsCheckList.Items
                If thisItem.Selected Then
                    For Each thisWarning As BrokenRule In _aircraft.WarningsList
                        If thisItem.Value = thisWarning.RuleIdentifier Then
                            thisWarning.OverRideRule = True
                        End If
                    Next
                End If
            Next
        End If
    End Sub

#End Region


#Region "AircraftComments DataGrid"

    Protected Sub BindData_AircraftCommentsDG()
        Me.AircraftCommentsDG.DataSource = _aircraft.GenericCommentList
        Me.AircraftCommentsDG.DataBind()
    End Sub

    Protected Sub AircraftCommentsDG_PageChanged(ByVal sender As System.Object, ByVal e As DataGridPageChangedEventArgs) Handles AircraftCommentsDG.PageIndexChanged
        Me.AircraftCommentsDG.CurrentPageIndex = e.NewPageIndex
        BindData_AircraftCommentsDG()
    End Sub

    Protected Sub AircraftCommentsDG_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles AircraftCommentsDG.ItemCommand

        Dim genericComment As New GenericComment
        Try
            Dim dg As DataGrid = CType(sender, DataGrid)

            Select Case LCase(e.CommandName)

                Case "add"

                    Dim authorId As String = CType(e.Item.FindControl("CommentAuthorDDL"), DropDownList).SelectedValue
                    Dim authorName As String = CType(e.Item.FindControl("CommentAuthorDDL"), DropDownList).SelectedItem.Text
                    Dim componentId As String = CType(e.Item.FindControl("ComponentDDL"), DropDownList).SelectedValue
                    Dim componentName As String = CType(e.Item.FindControl("ComponentDDL"), DropDownList).SelectedItem.Text
                    Dim issueCodeid As String = CType(e.Item.FindControl("IssueCodeDDL"), DropDownList).SelectedValue
                    Dim issueCode As String = CType(e.Item.FindControl("IssueCodeDDL"), DropDownList).SelectedItem.Text
                    Dim commentDate As String = CType(e.Item.FindControl("CommentDateTB"), TextBox).Text
                    Dim commentRating As String = CShort(CType(e.Item.FindControl("CommentRatingDDL"), DropDownList).SelectedItem.Text)
                    Dim comments As String = CType(e.Item.FindControl("CommentsTB"), TextBox).Text

                    If Not String.IsNullOrEmpty(commentDate.Trim()) Or Not String.IsNullOrEmpty(comments.Trim()) Then
                        With genericComment
                            .SourceTable = "AircraftComment"
                            .ParentId = _aircraft.AircraftId
                            .AuthorID = authorId
                            .AuthorName = authorName
                            .ComponentId = componentId
                            .ComponentName = componentName
                            .IssueCodeId = issueCodeid
                            .IssueCode = issueCode
                            .CommentDate = Server.HtmlDecode(commentDate)
                            .CommentRating = commentRating
                            .Comments = Server.HtmlDecode(comments)
                        End With
                        AircraftManager.InsertGenericComment(_aircraft, genericComment)
                        BindData_AircraftCommentsDG()
                    End If

                Case "edit"
                    dg.EditItemIndex = CInt(e.Item.ItemIndex)
                    Me.SaveButton.Enabled = False
                    BindData_AircraftCommentsDG()

                Case "save"
                    With genericComment
                        .SourceTable = Server.HtmlDecode(CType(e.Item.FindControl("SourceTableLB"), Label).Text)
                        .SourceID = CType(e.Item.FindControl("SourceIdLB"), Label).Text
                        .ParentId = CType(e.Item.FindControl("ParentIdLB"), Label).Text
                        .AuthorID = CType(e.Item.FindControl("AuthorIdLB"), Label).Text
                        .AuthorName = Server.HtmlDecode(CType(e.Item.FindControl("AuthorNameLB"), Label).Text)
                        .ComponentId = CType(e.Item.FindControl("ComponentDDL"), DropDownList).SelectedValue
                        .ComponentName = CType(e.Item.FindControl("ComponentDDL"), DropDownList).SelectedItem.Text
                        .IssueCodeId = CType(e.Item.FindControl("IssueCodeDDL"), DropDownList).SelectedValue
                        .IssueCode = CType(e.Item.FindControl("IssueCodeDDL"), DropDownList).SelectedItem.Text
                        .CommentDate = Server.HtmlDecode(CType(e.Item.FindControl("CommentDateLB"), Label).Text)
                        .CommentTime = Server.HtmlDecode(CType(e.Item.FindControl("CommentTimeLB"), Label).Text)
                        .CommentDateTime_UTC = Server.HtmlDecode(CType(e.Item.FindControl("CommentDateTime_UTCLB"), Label).Text)
                        .CommentDateTime_Local = Server.HtmlDecode(CType(e.Item.FindControl("CommentDateTime_LocalLB"), Label).Text)
                        .CommentRating = CShort(CType(e.Item.FindControl("CommentRatingDDL"), DropDownList).SelectedItem.Text)
                        .Comments = Server.HtmlDecode(CType(e.Item.FindControl("CommentsTB"), TextBox).Text)
                    End With
                    AircraftManager.UpdateGenericComment(_aircraft, genericComment)
                    dg.EditItemIndex = -1
                    BindData_AircraftCommentsDG()
                    Me.SaveButton.Enabled = True

                Case "cancel"
                    dg.EditItemIndex = -1
                    BindData_AircraftCommentsDG()
                    Me.SaveButton.Enabled = True

                Case "delete"
                    Dim sourceTable As String = CType(e.Item.FindControl("SourceTableLB"), Label).Text
                    Dim sourceId As Integer = CType(e.Item.FindControl("SourceIdLB"), Label).Text
                    AircraftManager.RemoveGenericComment(_aircraft, sourceTable, sourceId)
                    BindData_AircraftCommentsDG()

                Case "select"
                    Dim sourceTable As String = CType(e.Item.FindControl("SourceTableLB"), Label).Text
                    Select Case sourceTable
                        Case "AircraftComment"
                            Throw New Exception("You are already looking at Aircraft content!")
                        Case "WOEngineerComment", "WOMechanicComment"
                            Dim stringBuilder As New StringBuilder
                            With stringBuilder
                                .Append(_workOrderModifyUrl)
                                .Append("?mode=edit")
                                .Append("&workorderid=")
                                .Append(Encoder.UrlEncode(CType(e.Item.FindControl("ParentIdLB"), Label).Text))
                                .Append("&aircraftid=")
                                .Append(Encoder.UrlEncode(_aircraft.AircraftId.ToString))
                            End With
                            Response.Redirect(stringBuilder.ToString, False)
                        Case "FlightComment"
                            Dim stringBuilder As New StringBuilder
                            With stringBuilder
                                .Append(_flightModifyUrl)
                                .Append("?mode=edit")
                                .Append("&flightid=")
                                .Append(Encoder.UrlEncode(CType(e.Item.FindControl("ParentIdLB"), Label).Text))
                                .Append("&aircraftid=")
                                .Append(_aircraft.AircraftId)
                            End With
                            Response.Redirect(stringBuilder.ToString, False)
                        Case Else
                            Throw New Exception("Unable to determine Content Source Table")
                    End Select
            End Select

        Catch ex As Exception
            ProcessException(ex, genericComment.BrokenRules, genericComment.WarningsList)
            If e.CommandArgument = "OnSave" Then
                Throw New Exception(ex.Message)
            End If
        End Try

    End Sub

    Protected Function GetEngineers() As fltLookupBaseList
        Dim fltLookupBaseList As New fltLookupBaseList
        Try
            fltLookupBaseList = fltLookupBaseManager.GetList("Engineer", Me.ProgIdHiddenField.Value)
        Catch ex As Exception
            ProcessException(ex)
        End Try
        Return fltLookupBaseList
    End Function

    Protected Function GetComponents() As fltLookupBaseList
        Dim fltLookupBaseList As New fltLookupBaseList
        Try
            fltLookupBaseList = fltLookupBaseManager.GetList("AircraftComponent", Me.ProgIdHiddenField.Value)
        Catch ex As Exception
            ProcessException(ex)
        End Try
        Return fltLookupBaseList
    End Function

    Protected Function GetIssueCodes() As fltLookupBaseList
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
        ProcessException(ex, _aircraft.ErrorsList, _aircraft.WarningsList)
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

    Private Function SaveAllPendingChildDataGridAdds() As Boolean

        Dim retValue As Boolean = True

        Dim dgi As DataGridItem
        Dim orgArgs As CommandEventArgs
        Dim e As DataGridCommandEventArgs

        Try

            dgi = Me.AircraftLocationsDG.Controls(0).Controls(Me.AircraftLocationsDG.Controls(0).Controls.Count - 1)
            orgArgs = New CommandEventArgs("Add", "OnSave")
            e = New DataGridCommandEventArgs(dgi, Me.AircraftLocationsDG, orgArgs)
            AircraftLocationDG_ItemCommand(Me.AircraftLocationsDG, e)

            dgi = Me.AircraftCommentsDG.Controls(0).Controls(Me.AircraftCommentsDG.Controls(0).Controls.Count - 2)
            orgArgs = New CommandEventArgs("Add", "OnSave")
            e = New DataGridCommandEventArgs(dgi, Me.AircraftCommentsDG, orgArgs)
            AircraftCommentsDG_ItemCommand(Me.AircraftCommentsDG, e)

        Catch ex As Exception
            retValue = False
        End Try

        Return retValue

    End Function

#End Region

End Class