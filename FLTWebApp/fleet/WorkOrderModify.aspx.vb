' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports System.Transactions
Imports Microsoft.Security.Application

Partial Public Class WorkOrderModify
    Inherits System.Web.UI.Page

#Region "Private Variables"

    Private _workOrderListUrl As String = "~/fleet/workOrdersList.aspx"

    Private _fltIdentity As FltIdentity = Nothing

    Private _workOrderId As Integer = 0
    Private _aircraftId As Integer = 0
    Private _workOrder As WorkOrder = Nothing
    Private _pageMode As String = String.Empty
    Private _returnAddress As String = String.Empty

#End Region

#Region "Page Procedures"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If _fltIdentity Is Nothing Then
            Response.Redirect("~/Login.aspx", False)
        End If

        Me.ProgIdHiddenField.Value = _fltIdentity.CurrentProgId
        ClearErrorFields()

        If Not Me.IsPostBack Then

            _workOrderId = Request.QueryString("workorderid")
            _aircraftId = Request.QueryString("aircraftid")
            _pageMode = Request.QueryString("mode")
            _returnAddress = Request.UrlReferrer.ToString

            If (_pageMode = "add") Then

                _workOrder = New WorkOrder()
                Me.AircraftDDL.Enabled = True
                Me.LocationDDL.Enabled = True

            ElseIf (_pageMode = "edit") Then

                _workOrder = WorkOrderManager.GetItem(_workOrderId, _fltIdentity)
                Me.AircraftDDL.Enabled = False
                Me.LocationDDL.Enabled = False

            End If

            PopulateDDLs(_fltIdentity.CurrentProgId)
            LoadDataToForm()
            BindData_WOReasonsDG()
            BindData_WOInstructionsDG()
            BindData_WOPerformedWorkDG()
            BindData_WOEngineerCommentsDG()
            BindData_WOTechCommentsDG()
            BindData_WOPartsDG()
            BindData_WODocumentInfosGV()

        Else

            _workOrder = CType(ViewState("_workOrder"), WorkOrder)
            _workOrderId = ViewState("_workOrderId")
            _aircraftId = ViewState("_aircraftId")
            _returnAddress = ViewState("_returnAddress")
            _pageMode = ViewState("_pageMode")

        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_workOrderId") = _workOrderId
        ViewState("_aircraftId") = _aircraftId
        ViewState("_workOrder") = _workOrder
        ViewState("_returnAddress") = _returnAddress
        ViewState("_pageMode") = _pageMode
    End Sub

    Protected Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        Try
            If Not SaveAllPendingChildDataGridAdds() Then Exit Sub

            LoadDataToObject()
            ProcessWarnings()
            ClearErrorFields()

            WorkOrderManager.Save(_workOrder, _fltIdentity)
            _workOrder = Nothing
            ClearErrorFields()
            Response.Redirect(_returnAddress, False)

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Response.Redirect(_returnAddress, False)
    End Sub


#End Region

#Region "WorkOrder Modify Procedures"

    Private Sub PopulateDDLs(ByVal ProgId As Long)
        Try
            Dim totRows As Long = 0
            Me.AircraftDDL.Items.Clear()
            If _pageMode = "add" Then
                Me.AircraftDDL.Items.Add(New ListItem("< Select >", 0))
                Me.AircraftDDL.Items(0).Selected = True
            End If
            Dim aircraftInfoList As AircraftInfoList = AircraftInfoManager.GetList(ProgId, 0, String.Empty, _fltIdentity)
            If Not (aircraftInfoList Is Nothing) Then
                For Each thisAircraftInfo As AircraftInfo In aircraftInfoList
                    Me.AircraftDDL.Items.Add(New ListItem(thisAircraftInfo.MSN, thisAircraftInfo.AircraftId))
                Next
            End If
            Me.LocationDDL.Items.Clear()
            If _pageMode = "add" Then
                Me.LocationDDL.Items.Add(New ListItem("< Select >", 0))
                Me.LocationDDL.Items(0).Selected = True
            End If
            Dim locationList As LocationList = LocationManager.GetList(ProgId, 0, 1, 0, -1, -1, totRows, _fltIdentity)
            If Not (locationList Is Nothing) Then
                For Each thisLocation As Location In locationList
                    LocationDDL.Items.Add(New ListItem(thisLocation.LocationName, thisLocation.LocactionId))
                Next
            End If

            LoadDDL(Me.EngineerDDL, ProgId, "Engineer")
            LoadDDL(Me.MechanicDDL, ProgId, "Mechanic")
            LoadDDL(Me.WorkOrderTypeDDL, ProgId, "WorkOrderType")
            LoadDDL(Me.WorkOrderStatusDDL, ProgId, "WorkOrderStatus")
            LoadDDL(Me.EngineeringActionDDL, ProgId, "EngineeringAction")

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Private Sub LoadDDL(ByVal DDL As DropDownList, ByVal ProgId As Long, ByVal LookupName As String)
        Try
            Dim totRows As Long = 0
            DDL.Items.Clear()
            If _pageMode = "add" Then
                DDL.Items.Add(New ListItem("< Select >", 0))
                DDL.Items(0).Selected = True
            End If
            Dim thisLookupList As fltLookupBaseList = fltLookupBaseManager.GetList(LookupName, ProgId, 1, -1, -1, totRows)
            If Not (thisLookupList Is Nothing) Then
                For Each thisLookup As fltLookupBase In thisLookupList
                    DDL.Items.Add(New ListItem(thisLookup.StringValue, thisLookup.ID))
                Next
            Else
                Throw New Exception("Why is the list empty?")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

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

    Private Sub LoadDataToForm()
        Try
            Me.AircraftDDL.SelectedValue = _workOrder.AircraftId
            Me.LocationDDL.SelectedValue = _workOrder.LocationId
            Me.WorkOrderTypeDDL.SelectedValue = _workOrder.WorkOrderTypeId
            Me.WorkOrderStatusDDL.SelectedValue = _workOrder.WorkOrderStatusId
            Me.WorkOrderNbrLB.Text = Server.HtmlEncode(_workOrder.WorkOrderNbr)
            Me.RequestDateTB.Text = Server.HtmlEncode(_workOrder.RequestDate)
            Me.EngineerDDL.SelectedValue = _workOrder.EngineerId
            Me.EngineeringActionDDL.SelectedValue = _workOrder.EngineeringActionId
            Me.FaultOccurredDateTB.Text = Server.HtmlEncode(_workOrder.FaultOccurredDate)
            Me.FaultOccurredMeterTB.Text = Server.HtmlEncode(_workOrder.FaultOccurredMeter)
            Me.DueDateTB.Text = Server.HtmlEncode(_workOrder.DueDate)
            Me.DueTimeTB.Text = Server.HtmlEncode(_workOrder.DueTime)
            Me.StartMeterTB.Text = Server.HtmlEncode(_workOrder.MeterStartWork)
            Me.MechanicDDL.SelectedValue = _workOrder.MechanicId
            Me.FinishDateTB.Text = Server.HtmlEncode(_workOrder.FinishDate)
            Me.FinishTimeTB.Text = Server.HtmlEncode(_workOrder.FinishTime)
            Me.EndMeterTB.Text = Server.HtmlEncode(_workOrder.MeterEndWork)
            Me.EngineeringActionDDL.SelectedValue = _workOrder.EngineeringActionId
            Me.FISComplaintNbrTB.Text = Server.HtmlEncode(_workOrder.FISComplaintNbr)
            Me.SquawkNbrTB.Text = Server.HtmlEncode(_workOrder.SquawkNbr)
            Me.AircraftDispositionTB.Text = Server.HtmlEncode(_workOrder.AircraftDispostion)
            Me.AircraftIssueAlertCB.Checked = _workOrder.AircraftIssueAlert
            Me.MaintIssueAlertCB.Checked = _workOrder.MaintIssueAlert
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Private Sub LoadDataToObject()
        Try
            _workOrder.ProgId = Me.ProgIdHiddenField.Value
            _workOrder.AircraftId = Me.AircraftDDL.SelectedValue
            _workOrder.LocationId = Me.LocationDDL.SelectedValue
            _workOrder.AircraftIssueAlert = Me.AircraftIssueAlertCB.Checked
            _workOrder.MaintIssueAlert = Me.MaintIssueAlertCB.Checked
            _workOrder.EngineerId = Me.EngineerDDL.SelectedValue
            _workOrder.MechanicId = Me.MechanicDDL.SelectedValue
            _workOrder.WorkOrderTypeId = Me.WorkOrderTypeDDL.SelectedValue
            _workOrder.WorkOrderStatusId = Me.WorkOrderStatusDDL.SelectedValue
            _workOrder.FaultOccurredDate = Server.HtmlDecode(Me.FaultOccurredDateTB.Text)
            _workOrder.FaultOccurredMeter = Server.HtmlDecode(Me.FaultOccurredMeterTB.Text)
            _workOrder.FISComplaintNbr = Server.HtmlDecode(Me.FISComplaintNbrTB.Text)
            _workOrder.SquawkNbr = Server.HtmlDecode(Me.SquawkNbrTB.Text)
            _workOrder.RequestDate = Server.HtmlDecode(Me.RequestDateTB.Text)
            _workOrder.DueDate = Server.HtmlDecode(Me.DueDateTB.Text)
            _workOrder.DueTime = Server.HtmlDecode(Me.DueTimeTB.Text)
            _workOrder.FinishDate = Server.HtmlDecode(Me.FinishDateTB.Text)
            _workOrder.FinishTime = Server.HtmlDecode(Me.FinishTimeTB.Text)
            _workOrder.MeterStartWork = Server.HtmlDecode(Me.StartMeterTB.Text)
            _workOrder.MeterEndWork = Server.HtmlDecode(Me.EndMeterTB.Text)
            _workOrder.EngineeringActionId = Me.EngineeringActionDDL.SelectedValue
            _workOrder.AircraftDispostion = Server.HtmlDecode(Me.AircraftDispositionTB.Text)
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Private Sub ProcessWarnings()
        If _workOrder.WarningsList.Count <> 0 Then
            For Each thisItem As ListItem In Me.WarningsCheckList.Items
                If thisItem.Selected Then
                    For Each thisWarning As BrokenRule In _workOrder.WarningsList
                        If thisItem.Value = thisWarning.RuleIdentifier Then
                            thisWarning.OverRideRule = True
                        End If
                    Next
                End If
            Next
        End If
    End Sub

#End Region

#Region "WOReasonsDG Procedures"

    Protected Sub BindData_WOReasonsDG()
        Try
            Me.WOReasonsDG.DataSource = _workOrder.WOReasonList
            WOReasonsDG.DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub WOReasonsDG_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles WOReasonsDG.ItemCommand
        Dim newWOReason As New WOReason
        Try
            Dim dg As DataGrid = CType(sender, DataGrid)

            Select Case LCase(e.CommandName)

                Case "add"

                    Dim reasonLine As String = CType(e.Item.FindControl("ReasonLineTB"), TextBox).Text
                    If reasonLine <> String.Empty Then
                        With newWOReason
                            .WorkOrderId = _workOrder.WorkOrderId
                            .ReasonLine = Server.HtmlDecode(reasonLine)
                        End With
                        WorkOrderManager.InsertWOReason(_workOrder, newWOReason)
                        BindData_WOReasonsDG()
                    End If

                Case "edit"
                    dg.EditItemIndex = CInt(e.Item.ItemIndex)
                    Me.SaveButton.Enabled = False
                    BindData_WOReasonsDG()

                Case "update"
                    With newWOReason
                        .WOReasonId = CType(e.Item.FindControl("WOReasonsIdLB"), Label).Text
                        .WorkOrderId = _workOrder.WorkOrderId
                        .ReasonLine = Server.HtmlDecode(CType(e.Item.FindControl("ReasonLineTB"), TextBox).Text)
                    End With
                    WorkOrderManager.UpdateWOReason(_workOrder, newWOReason)
                    dg.EditItemIndex = -1
                    If Me.WOInstructionsDG.EditItemIndex = -1 _
                    And Me.WOReasonsDG.EditItemIndex = -1 _
                    And Me.WOPerformedWorkDG.EditItemIndex = -1 _
                    And Me.WOEngineerCommentsDG.EditItemIndex = -1 _
                    And Me.WOTechCommentsDG.EditItemIndex = -1 _
                    And Me.WOPartsDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If
                    BindData_WOReasonsDG()

                Case "cancel"
                    dg.EditItemIndex = -1
                    If Me.WOInstructionsDG.EditItemIndex = -1 _
                    And Me.WOReasonsDG.EditItemIndex = -1 _
                    And Me.WOPerformedWorkDG.EditItemIndex = -1 _
                    And Me.WOEngineerCommentsDG.EditItemIndex = -1 _
                    And Me.WOTechCommentsDG.EditItemIndex = -1 _
                    And Me.WOPartsDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If
                    BindData_WOReasonsDG()

                Case "delete"
                    Dim woReasonsId As Integer = CType(e.Item.FindControl("WOReasonsIdLB"), Label).Text
                    WorkOrderManager.RemoveWOReason(_workOrder, woReasonsId)
                    BindData_WOReasonsDG()

            End Select

        Catch ex As Exception
            ProcessException(ex, newWOReason.ErrorsList, newWOReason.WarningsList)
            If e.CommandArgument = "OnSave" Then
                Throw New Exception(ex.Message)
            End If
        End Try
    End Sub

#End Region

#Region "WOInstructionsDG Procedures"

    Protected Sub BindData_WOInstructionsDG()
        Try
            Me.WOInstructionsDG.DataSource = _workOrder.WOInstructionList
            WOInstructionsDG.DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub WOInstructionsDG_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles WOInstructionsDG.ItemCommand
        Dim newWOInstruction As New WOInstruction
        Try
            Dim dg As DataGrid = CType(sender, DataGrid)

            Select Case LCase(e.CommandName)

                Case "add"

                    Dim instructionLine As String = CType(e.Item.FindControl("InstructionLineTB"), TextBox).Text
                    If instructionLine <> String.Empty Then
                        With newWOInstruction
                            .WorkOrderId = _workOrder.WorkOrderId
                            .InstructionLine = Server.HtmlDecode(instructionLine)
                        End With
                        WorkOrderManager.InsertWOInstruction(_workOrder, newWOInstruction)
                        BindData_WOInstructionsDG()
                    End If

                Case "edit"
                    dg.EditItemIndex = CInt(e.Item.ItemIndex)
                    Me.SaveButton.Enabled = False
                    BindData_WOInstructionsDG()

                Case "update"
                    With newWOInstruction
                        .WOInstructionsId = CType(e.Item.FindControl("WOInstructionsIdLB"), Label).Text
                        .WorkOrderId = _workOrder.WorkOrderId
                        .InstructionLine = Server.HtmlDecode(CType(e.Item.FindControl("InstructionLineTB"), TextBox).Text)
                    End With
                    WorkOrderManager.UpdateWOInstruction(_workOrder, newWOInstruction)
                    dg.EditItemIndex = -1
                    If Me.WOInstructionsDG.EditItemIndex = -1 _
                    And Me.WOReasonsDG.EditItemIndex = -1 _
                    And Me.WOPerformedWorkDG.EditItemIndex = -1 _
                    And Me.WOEngineerCommentsDG.EditItemIndex = -1 _
                    And Me.WOTechCommentsDG.EditItemIndex = -1 _
                    And Me.WOPartsDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If
                    BindData_WOInstructionsDG()

                Case "cancel"
                    dg.EditItemIndex = -1
                    If Me.WOInstructionsDG.EditItemIndex = -1 _
                    And Me.WOReasonsDG.EditItemIndex = -1 _
                    And Me.WOPerformedWorkDG.EditItemIndex = -1 _
                    And Me.WOEngineerCommentsDG.EditItemIndex = -1 _
                    And Me.WOTechCommentsDG.EditItemIndex = -1 _
                    And Me.WOPartsDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If
                    BindData_WOInstructionsDG()

                Case "delete"
                    Dim woInstructionsId As Integer = CType(e.Item.FindControl("WOInstructionsIdLB"), Label).Text
                    WorkOrderManager.RemoveWOInstruction(_workOrder, woInstructionsId)
                    BindData_WOInstructionsDG()

            End Select

        Catch ex As Exception
            ProcessException(ex, newWOInstruction.ErrorsList, newWOInstruction.WarningsList)
            If e.CommandArgument = "OnSave" Then
                Throw New Exception(ex.Message)
            End If
        End Try
    End Sub

#End Region

#Region "WOPerformedWorkDG Procedures"

    Protected Sub BindData_WOPerformedWorkDG()
        Try
            Me.WOPerformedWorkDG.DataSource = _workOrder.WOPerformedWorkList
            WOPerformedWorkDG.DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub WOPerformedWorkDG_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles WOPerformedWorkDG.ItemCommand
        Dim newWOPerformedWork As New WOPerformedWork
        Try
            Dim dg As DataGrid = CType(sender, DataGrid)

            Select Case LCase(e.CommandName)

                Case "add"
                    Dim performedWorkLine As String = CType(e.Item.FindControl("PerformedWorkLineTB"), TextBox).Text
                    If performedWorkLine.Length <> 0 Then
                        With newWOPerformedWork
                            .WorkOrderId = _workOrder.WorkOrderId
                            .PerformedWorkLine = Server.HtmlDecode(performedWorkLine)
                        End With
                        WorkOrderManager.InsertWOPerformedWork(_workOrder, newWOPerformedWork)
                        BindData_WOPerformedWorkDG()
                    End If

                Case "edit"
                    dg.EditItemIndex = CInt(e.Item.ItemIndex)
                    Me.SaveButton.Enabled = False
                    BindData_WOPerformedWorkDG()

                Case "update"
                    With newWOPerformedWork
                        .PerformedWorkId = CType(e.Item.FindControl("PerformedWorkIdLB"), Label).Text
                        .WorkOrderId = _workOrder.WorkOrderId
                        .PerformedWorkLine = Server.HtmlDecode(CType(e.Item.FindControl("PerformedWorkLineTB"), TextBox).Text)
                    End With
                    WorkOrderManager.UpdateWOPerformedWork(_workOrder, newWOPerformedWork)
                    dg.EditItemIndex = -1
                    If Me.WOInstructionsDG.EditItemIndex = -1 _
                    And Me.WOReasonsDG.EditItemIndex = -1 _
                    And Me.WOPerformedWorkDG.EditItemIndex = -1 _
                    And Me.WOEngineerCommentsDG.EditItemIndex = -1 _
                    And Me.WOTechCommentsDG.EditItemIndex = -1 _
                    And Me.WOPartsDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If
                    BindData_WOPerformedWorkDG()

                Case "cancel"
                    dg.EditItemIndex = -1
                    If Me.WOInstructionsDG.EditItemIndex = -1 _
                    And Me.WOReasonsDG.EditItemIndex = -1 _
                    And Me.WOPerformedWorkDG.EditItemIndex = -1 _
                    And Me.WOEngineerCommentsDG.EditItemIndex = -1 _
                    And Me.WOTechCommentsDG.EditItemIndex = -1 _
                    And Me.WOPartsDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If
                    BindData_WOPerformedWorkDG()

                Case "delete"
                    Dim woPerformedWorkId As Integer = CType(e.Item.FindControl("PerformedWorkIdLB"), Label).Text
                    WorkOrderManager.RemoveWOPerformedWork(_workOrder, woPerformedWorkId)
                    BindData_WOPerformedWorkDG()

            End Select

        Catch ex As Exception
            ProcessException(ex, newWOPerformedWork.ErrorsList, newWOPerformedWork.WarningsList)
        End Try
    End Sub

#End Region

#Region "WOEngineeringCommentDG Procedures"

    Protected Sub BindData_WOEngineerCommentsDG()
        Try
            Me.WOEngineerCommentsDG.DataSource = _workOrder.WOEngineerCommentList
            WOEngineerCommentsDG.DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub WOEngineerCommentsDG_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles WOEngineerCommentsDG.ItemCommand
        Dim newWOEngineerComment As New WOEngineerComment
        Try
            Dim dg As DataGrid = CType(sender, DataGrid)

            Select Case LCase(e.CommandName)

                Case "add"
                    Dim componentId As Integer = CType(e.Item.FindControl("ComponentDDL"), DropDownList).SelectedItem.Value
                    Dim component As String = CType(e.Item.FindControl("ComponentDDL"), DropDownList).SelectedItem.Text
                    Dim commentDate As String = CType(e.Item.FindControl("CommentDateTB"), TextBox).Text
                    Dim issueCodeId As Integer = CType(e.Item.FindControl("IssueCodeDDL"), DropDownList).SelectedValue
                    Dim issueCode As String = CType(e.Item.FindControl("IssueCodeDDL"), DropDownList).SelectedItem.Text
                    Dim commentRating As Integer = CType(e.Item.FindControl("CommentRatingDDL"), DropDownList).SelectedValue
                    Dim comments As String = CType(e.Item.FindControl("CommentsTB"), TextBox).Text
                    If componentId <> 0 Or commentDate <> String.Empty Or issueCodeId <> 0 Or commentRating <> 0 Or comments <> String.Empty Then
                        With newWOEngineerComment
                            .WorkOrderId = _workOrder.WorkOrderId
                            .ComponentId = componentId
                            .Component = component
                            .IssueCodeId = issueCodeId
                            .IssueCode = issueCode
                            .CommentDate = Server.HtmlDecode(commentDate)
                            .CommentRating = commentRating
                            .Comments = Server.HtmlDecode(comments)
                        End With
                        WorkOrderManager.InsertWOEngineerComment(_workOrder, newWOEngineerComment)
                        BindData_WOEngineerCommentsDG()
                    End If

                Case "edit"
                    dg.EditItemIndex = CInt(e.Item.ItemIndex)
                    Me.SaveButton.Enabled = False
                    BindData_WOEngineerCommentsDG()

                Case "update"
                    With newWOEngineerComment
                        .EngineerCommentsId = CType(e.Item.FindControl("EngineerCommentsIdLB"), Label).Text
                        .ComponentId = CType(e.Item.FindControl("ComponentDDL"), DropDownList).SelectedItem.Value
                        .Component = CType(e.Item.FindControl("ComponentDDL"), DropDownList).SelectedItem.Text
                        .IssueCodeId = CType(e.Item.FindControl("IssueCodeDDL"), DropDownList).SelectedValue
                        .IssueCode = CType(e.Item.FindControl("IssueCodeDDL"), DropDownList).SelectedItem.Text
                        .CommentDate = Server.HtmlDecode(CType(e.Item.FindControl("CommentDateTB"), TextBox).Text)
                        .CommentRating = CType(e.Item.FindControl("CommentRatingDDL"), DropDownList).SelectedValue
                        .WorkOrderId = _workOrder.WorkOrderId
                        .Comments = Server.HtmlDecode(CType(e.Item.FindControl("CommentsTB"), TextBox).Text)
                    End With
                    WorkOrderManager.UpdateWOEngineerComment(_workOrder, newWOEngineerComment)
                    dg.EditItemIndex = -1
                    If Me.WOInstructionsDG.EditItemIndex = -1 _
                    And Me.WOReasonsDG.EditItemIndex = -1 _
                    And Me.WOPerformedWorkDG.EditItemIndex = -1 _
                    And Me.WOEngineerCommentsDG.EditItemIndex = -1 _
                    And Me.WOTechCommentsDG.EditItemIndex = -1 _
                    And Me.WOPartsDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If
                    BindData_WOEngineerCommentsDG()

                Case "cancel"
                    dg.EditItemIndex = -1
                    If Me.WOInstructionsDG.EditItemIndex = -1 _
                    And Me.WOReasonsDG.EditItemIndex = -1 _
                    And Me.WOPerformedWorkDG.EditItemIndex = -1 _
                    And Me.WOEngineerCommentsDG.EditItemIndex = -1 _
                    And Me.WOTechCommentsDG.EditItemIndex = -1 _
                    And Me.WOPartsDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If
                    BindData_WOEngineerCommentsDG()

                Case "delete"
                    Dim woEngineerCommentsId As Integer = CType(e.Item.FindControl("EngineerCommentsIdLB"), Label).Text
                    WorkOrderManager.RemoveWOEngineerComment(_workOrder, woEngineerCommentsId)
                    BindData_WOEngineerCommentsDG()

            End Select

        Catch ex As Exception
            ProcessException(ex, newWOEngineerComment.ErrorsList, newWOEngineerComment.WarningsList)
            If e.CommandArgument = "OnSave" Then
                Throw New Exception(ex.Message)
            End If
        End Try
    End Sub

#End Region

#Region "WOTechCommentsDG Procedures"

    Protected Sub BindData_WOTechCommentsDG()
        Try
            Me.WOTechCommentsDG.DataSource = _workOrder.WOMechanicCommentList
            WOTechCommentsDG.DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub WOTechCommentsDG_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles WOTechCommentsDG.ItemCommand
        Dim newWOMechanicComment As New WOMechanicComment
        Try
            Dim dg As DataGrid = CType(sender, DataGrid)

            Select Case LCase(e.CommandName)

                Case "add"
                    Dim componentId As Integer = CType(e.Item.FindControl("ComponentDDL"), DropDownList).SelectedItem.Value
                    Dim component As String = CType(e.Item.FindControl("ComponentDDL"), DropDownList).SelectedItem.Text
                    Dim issueCodeId As Integer = CType(e.Item.FindControl("IssueCodeDDL"), DropDownList).SelectedValue
                    Dim issueCode As String = CType(e.Item.FindControl("IssueCodeDDL"), DropDownList).SelectedItem.Text
                    Dim commentDate As String = CType(e.Item.FindControl("CommentDateTB"), TextBox).Text
                    Dim commentRating As Integer = CType(e.Item.FindControl("CommentRatingDDL"), DropDownList).SelectedValue
                    Dim comments As String = CType(e.Item.FindControl("CommentsTB"), TextBox).Text

                    If componentId <> 0 Or commentDate <> String.Empty Or commentRating <> 0 Or comments <> String.Empty Then
                        With newWOMechanicComment
                            .WorkOrderId = _workOrder.WorkOrderId
                            .ComponentId = componentId
                            .Component = component
                            .IssueCodeId = issueCodeId
                            .IssueCode = issueCode
                            .CommentDate = Server.HtmlDecode(commentDate)
                            .CommentRating = commentRating
                            .Comments = Server.HtmlDecode(comments)
                        End With
                        WorkOrderManager.InsertWOMechanicComment(_workOrder, newWOMechanicComment)
                        BindData_WOTechCommentsDG()
                    End If

                Case "edit"
                    dg.EditItemIndex = CInt(e.Item.ItemIndex)
                    Me.SaveButton.Enabled = False
                    BindData_WOTechCommentsDG()

                Case "update"
                    With newWOMechanicComment
                        .WOMechanicCommentsId = CType(e.Item.FindControl("WOMechanicCommentsIdLB"), Label).Text
                        .WorkOrderId = _workOrder.WorkOrderId
                        .ComponentId = CType(e.Item.FindControl("ComponentDDL"), DropDownList).SelectedItem.Value
                        .Component = CType(e.Item.FindControl("ComponentDDL"), DropDownList).SelectedItem.Text
                        .IssueCodeId = CType(e.Item.FindControl("IssueCodeDDL"), DropDownList).SelectedValue
                        .IssueCode = CType(e.Item.FindControl("IssueCodeDDL"), DropDownList).SelectedItem.Text
                        .CommentDate = Server.HtmlDecode(CType(e.Item.FindControl("CommentDateTB"), TextBox).Text)
                        .CommentRating = CType(e.Item.FindControl("CommentRatingDDL"), DropDownList).SelectedValue
                        .Comments = Server.HtmlDecode(CType(e.Item.FindControl("CommentsTB"), TextBox).Text)
                    End With
                    WorkOrderManager.UpdateWOMechanicComment(_workOrder, newWOMechanicComment)
                    dg.EditItemIndex = -1
                    If Me.WOInstructionsDG.EditItemIndex = -1 _
                    And Me.WOReasonsDG.EditItemIndex = -1 _
                    And Me.WOPerformedWorkDG.EditItemIndex = -1 _
                    And Me.WOEngineerCommentsDG.EditItemIndex = -1 _
                    And Me.WOTechCommentsDG.EditItemIndex = -1 _
                    And Me.WOPartsDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If
                    BindData_WOTechCommentsDG()

                Case "cancel"
                    dg.EditItemIndex = -1
                    If Me.WOInstructionsDG.EditItemIndex = -1 _
                    And Me.WOReasonsDG.EditItemIndex = -1 _
                    And Me.WOPerformedWorkDG.EditItemIndex = -1 _
                    And Me.WOEngineerCommentsDG.EditItemIndex = -1 _
                    And Me.WOTechCommentsDG.EditItemIndex = -1 _
                    And Me.WOPartsDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If
                    BindData_WOTechCommentsDG()

                Case "delete"
                    Dim woMechanicCommentsId As Integer = CType(e.Item.FindControl("WOMechanicCommentsIdLB"), Label).Text
                    WorkOrderManager.RemoveWOMechanicComment(_workOrder, woMechanicCommentsId)
                    BindData_WOTechCommentsDG()

            End Select

        Catch ex As Exception
            ProcessException(ex, newWOMechanicComment.ErrorsList, newWOMechanicComment.WarningsList)
            If e.CommandArgument = "OnSave" Then
                Throw New Exception(ex.Message)
            End If
        End Try
    End Sub

#End Region

#Region "WOPartsDG Procedures"

    Protected Sub BindData_WOPartsDG()
        Try
            Me.WOPartsDG.DataSource = _workOrder.WOPartList
            WOPartsDG.DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub WOPartsDG_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles WOPartsDG.ItemCommand
        Dim newWorkOrderPart As New WorkOrderPart
        Try
            Dim dg As DataGrid = CType(sender, DataGrid)

            Select Case LCase(e.CommandName)

                Case "add"
                    Dim partName As String = CType(e.Item.FindControl("PartNameTB"), TextBox).Text
                    Dim partDescription As String = CType(e.Item.FindControl("PartDescriptionTB"), TextBox).Text
                    Dim partModule As String = CType(e.Item.FindControl("PartModuleTB"), TextBox).Text
                    Dim fisIdRemoved As String = CType(e.Item.FindControl("FisIdRemovedTB"), TextBox).Text
                    Dim fisIdAdded As String = CType(e.Item.FindControl("FisIdAddedTB"), TextBox).Text
                    Dim partNbrFailed As String = CType(e.Item.FindControl("PartNbrFailedTB"), TextBox).Text
                    Dim partNbrRemoved As String = CType(e.Item.FindControl("PartNbrRemovedTB"), TextBox).Text
                    Dim partNbrAdded As String = CType(e.Item.FindControl("PartNbrAddedTB"), TextBox).Text
                    If partName <> String.Empty Or partDescription <> String.Empty Or partModule <> String.Empty _
                    Or fisIdRemoved <> String.Empty Or fisIdAdded <> String.Empty Or partNbrFailed <> String.Empty Or partNbrRemoved <> String.Empty Or partNbrAdded <> String.Empty Then
                        With newWorkOrderPart
                            .WorkOrderId = _workOrder.WorkOrderId
                            .PartName = Server.HtmlDecode(CType(e.Item.FindControl("PartNameTB"), TextBox).Text)
                            .PartDescription = Server.HtmlDecode(CType(e.Item.FindControl("PartDescriptionTB"), TextBox).Text)
                            .PartModule = Server.HtmlDecode(CType(e.Item.FindControl("PartModuleTB"), TextBox).Text)
                            .FisIDRemoved = Server.HtmlDecode(CType(e.Item.FindControl("FisIdRemovedTB"), TextBox).Text)
                            .FisIDAdded = Server.HtmlDecode(CType(e.Item.FindControl("FisIdAddedTB"), TextBox).Text)
                            .PartNbrFailed = Server.HtmlDecode(CType(e.Item.FindControl("PartNbrFailedTB"), TextBox).Text)
                            .PartNbrRemoved = Server.HtmlDecode(CType(e.Item.FindControl("PartNbrRemovedTB"), TextBox).Text)
                            .PartNbrAdded = Server.HtmlDecode(CType(e.Item.FindControl("PartNbrAddedTB"), TextBox).Text)
                        End With
                        WorkOrderManager.InsertWorkOrderPart(_workOrder, newWorkOrderPart)
                        BindData_WOPartsDG()
                    End If

                Case "edit"
                    dg.EditItemIndex = CInt(e.Item.ItemIndex)
                    Me.SaveButton.Enabled = False
                    BindData_WOPartsDG()

                Case "update"
                    With newWorkOrderPart
                        .WorkOrderPartId = CType(e.Item.FindControl("WorkOrderPartIdLB"), Label).Text
                        .WorkOrderId = _workOrder.WorkOrderId
                        .PartName = Server.HtmlDecode(CType(e.Item.FindControl("PartNameTB"), TextBox).Text)
                        .PartDescription = Server.HtmlDecode(CType(e.Item.FindControl("PartDescriptionTB"), TextBox).Text)
                        .PartModule = Server.HtmlDecode(CType(e.Item.FindControl("PartModuleTB"), TextBox).Text)
                        .FisIDRemoved = Server.HtmlDecode(CType(e.Item.FindControl("FisIdRemovedTB"), TextBox).Text)
                        .FisIDAdded = Server.HtmlDecode(CType(e.Item.FindControl("FisIdAddedTB"), TextBox).Text)
                        .PartNbrFailed = Server.HtmlDecode(CType(e.Item.FindControl("PartNbrFailedTB"), TextBox).Text)
                        .PartNbrRemoved = Server.HtmlDecode(CType(e.Item.FindControl("PartNbrRemovedTB"), TextBox).Text)
                        .PartNbrAdded = Server.HtmlDecode(CType(e.Item.FindControl("PartNbrAddedTB"), TextBox).Text)
                    End With
                    WorkOrderManager.UpdateWorkOrderPart(_workOrder, newWorkOrderPart)
                    dg.EditItemIndex = -1
                    If Me.WOInstructionsDG.EditItemIndex = -1 _
                    And Me.WOReasonsDG.EditItemIndex = -1 _
                    And Me.WOPerformedWorkDG.EditItemIndex = -1 _
                    And Me.WOEngineerCommentsDG.EditItemIndex = -1 _
                    And Me.WOTechCommentsDG.EditItemIndex = -1 _
                    And Me.WOPartsDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If
                    BindData_WOPartsDG()

                Case "cancel"
                    dg.EditItemIndex = -1
                    If Me.WOInstructionsDG.EditItemIndex = -1 _
                    And Me.WOReasonsDG.EditItemIndex = -1 _
                    And Me.WOPerformedWorkDG.EditItemIndex = -1 _
                    And Me.WOEngineerCommentsDG.EditItemIndex = -1 _
                    And Me.WOTechCommentsDG.EditItemIndex = -1 _
                    And Me.WOPartsDG.EditItemIndex = -1 Then
                        Me.SaveButton.Enabled = True
                    End If
                    BindData_WOPartsDG()

                Case "delete"
                    Dim workOrderPartId As Integer = CType(e.Item.FindControl("WorkOrderPartIdLB"), Label).Text
                    WorkOrderManager.RemoveWorkOrderPart(_workOrder, workOrderPartId)
                    BindData_WOPartsDG()

            End Select

        Catch ex As Exception
            ProcessException(ex, newWorkOrderPart.ErrorsList, newWorkOrderPart.WarningsList)
            If e.CommandArgument = "OnSave" Then
                Throw New Exception(ex.Message)
            End If
        End Try
    End Sub

#End Region

#Region "WODocuments Procedures"

    Protected Sub BindData_WODocumentInfosGV()
        Try
            Me.WODocumentsGV.DataSource = _workOrder.WODocumentInfosList
            WODocumentsGV.DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub DocumentsGridView_PageIndexchanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles WODocumentsGV.PageIndexChanging
        Dim gv As GridView = CType(sender, GridView)
        gv.PageIndex = e.NewPageIndex
        BindData_WODocumentInfosGV()
    End Sub

    Protected Sub AddDocumentButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddDocumentButton.Click
        Me.DocumentsMV.ActiveViewIndex = 1
    End Sub

    Protected Sub DocumentsGridView_Command(ByVal sender As System.Object, ByVal e As GridViewCommandEventArgs) Handles WODocumentsGV.RowCommand
    End Sub

    Protected Sub DocumentsGridView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WODocumentsGV.SelectedIndexChanged
        Dim stringBuilder As New StringBuilder

        With stringBuilder
            .Append("~/documents/documentShow.aspx?id=")
            .Append(Encoder.UrlEncode(Me.WODocumentsGV.SelectedValue.ToString))
        End With

        Response.Redirect(stringBuilder.ToString, False)
    End Sub

    Protected Sub DocumentsGridView_RowEditing(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles WODocumentsGV.RowEditing
        Try
            Dim gv As GridView = CType(sender, GridView)
            gv.EditIndex = e.NewEditIndex
            BindData_WODocumentInfosGV()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub DocumentsGridView_CancelingEdit(ByVal sender As System.Object, ByVal e As GridViewCancelEditEventArgs) Handles WODocumentsGV.RowCancelingEdit
        WODocumentsGV.EditIndex = -1
        BindData_WODocumentInfosGV()
    End Sub

    Protected Sub DocumentsGridView_RowUpdating(ByVal sender As System.Object, ByVal e As GridViewUpdateEventArgs) Handles WODocumentsGV.RowUpdating
        Dim documentInfo As New DocumentInfo
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            With documentInfo
                .DocumentId = CInt(CType(row.FindControl("DocumentIdLB"), Label).Text)
                .ProgId = CInt(Me.ProgIdHiddenField.Value)
                .DocumentTypeId = CInt(CType(row.FindControl("DocumentTypeIdLB"), Label).Text)
                .DocumentSize = CInt(CType(row.FindControl("DocumentSizeLB"), Label).Text)
                .DocumentName = Server.HtmlDecode(CType(row.FindControl("DocumentNameTB"), TextBox).Text)
                .DocumentDescription = Server.HtmlDecode(CType(row.FindControl("DocumentDescriptionTB"), TextBox).Text)
            End With
            WorkOrderManager.UpdateDocument(_workOrder, documentInfo)
            gv.EditIndex = -1
            BindData_WODocumentInfosGV()
        Catch ex As Exception
            ProcessException(ex, documentInfo.BrokenRules)
        End Try
    End Sub

    Protected Sub DocumentsGridView_RowDeleting(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles WODocumentsGV.RowDeleting
        Dim documentInfo As New DocumentInfo
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            With documentInfo
                .DocumentId = CInt(CType(row.FindControl("DocumentIdLB"), Label).Text)
                .DocumentName = CType(row.FindControl("DocumentNameLB"), Label).Text
                .DocumentDescription = CType(row.FindControl("DocumentDescriptionLB"), Label).Text
            End With
            WorkOrderManager.RemoveDocument(_workOrder, documentInfo)
            BindData_WODocumentInfosGV()
        Catch ex As Exception
            ProcessException(ex, documentInfo.BrokenRules)
        End Try
    End Sub

    Protected Sub UploadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadButton.Click
        Dim newDocumentInfo As New DocumentInfo

        Try
            If Me.FileUpoad1.FileName.ToString = String.Empty Then
                Throw New Exception("Please select a document to upload")
            End If

            Dim maxDocSize As Integer = CInt(ConfigurationManager.AppSettings.Get("MaxFileUploadSize"))

            If Me.FileUpoad1.FileBytes.Length > maxDocSize Then
                Throw New Exception("Document size exceeds maximum limit of " & Format((maxDocSize / 1000000), "#,##0") & "MB")
            End If

            With newDocumentInfo
                .ProgId = Me.ProgIdHiddenField.Value
                .DocumentName = Server.HtmlDecode(Me.FileUpoad1.FileName.ToString)
                .FileExtension = .DocumentName.Substring(.DocumentName.LastIndexOf("."))
                .DocumentTypeId = GetDocumentType(.FileExtension)
                .DocumentSize = Me.FileUpoad1.FileBytes.Length
                .DocumentDescription = Server.HtmlDecode(Me.DocumentDescriptionTB.Text)
            End With

            WorkOrderManager.InsertDocument(_workOrder, newDocumentInfo, Me.FileUpoad1.PostedFile.InputStream, _fltIdentity)

            Me.DocumentsMV.ActiveViewIndex = 0
            BindData_WODocumentInfosGV()

        Catch ex As Exception
            ProcessException(ex, newDocumentInfo.BrokenRules)
        End Try
    End Sub

    Protected Sub UploadCancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Me.DocumentsMV.ActiveViewIndex = 0
        BindData_WODocumentInfosGV()
    End Sub

    Private Function GetDocumentType(ByVal thisFileExtension As String) As Integer
        Dim newDocumentTypeId As Integer = 0
        Try
            For Each thisDocumentType As DocumentType In DocumentTypeManager.GetList(_fltIdentity)
                If thisDocumentType.FileExtension = thisFileExtension Then
                    newDocumentTypeId = thisDocumentType.DocumentTypeId
                    Exit For
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return newDocumentTypeId
    End Function

#End Region

#Region "Private Procedures"

    Protected Function GetComponents() As fltLookupBaseList
        Dim fltLookupBaseList As New fltLookupBaseList
        Dim fltLookupBase As New fltLookupBase
        With fltLookupBase
            .ID = 0
            .LookupName = "AircraftComponent"
            .StringValue = "< Select >"
        End With
        fltLookupBaseList.Add(fltLookupBase)
        Try
            For Each thisComponent As fltLookupBase In fltLookupBaseManager.GetList("AircraftComponent", Me.ProgIdHiddenField.Value)
                fltLookupBaseList.Add(thisComponent)
            Next
            Return fltLookupBaseList
        Catch ex As Exception
            ProcessException(ex)
        End Try
        Return fltLookupBaseList
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
        ProcessException(ex, _workOrder.ErrorsList, _workOrder.WarningsList)
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
    Private Function SaveAllPendingChildDataGridAdds() As Boolean

        Dim retValue As Boolean = True

        Dim dgi As DataGridItem
        Dim orgArgs As CommandEventArgs
        Dim e As DataGridCommandEventArgs

        Try

            dgi = Me.WOReasonsDG.Controls(0).Controls(Me.WOReasonsDG.Controls(0).Controls.Count - 1)
            orgArgs = New CommandEventArgs("Add", "OnSave")
            e = New DataGridCommandEventArgs(dgi, Me.WOReasonsDG, orgArgs)
            WOReasonsDG_ItemCommand(Me.WOReasonsDG, e)

            dgi = Me.WOInstructionsDG.Controls(0).Controls(Me.WOInstructionsDG.Controls(0).Controls.Count - 1)
            orgArgs = New CommandEventArgs("Add", "OnSave")
            e = New DataGridCommandEventArgs(dgi, Me.WOInstructionsDG, orgArgs)
            WOInstructionsDG_ItemCommand(Me.WOInstructionsDG, e)

            dgi = Me.WOPerformedWorkDG.Controls(0).Controls(Me.WOPerformedWorkDG.Controls(0).Controls.Count - 1)
            orgArgs = New CommandEventArgs("Add", "OnSave")
            e = New DataGridCommandEventArgs(dgi, Me.WOPerformedWorkDG, orgArgs)
            WOPerformedWorkDG_ItemCommand(Me.WOPerformedWorkDG, e)

            dgi = Me.WOEngineerCommentsDG.Controls(0).Controls(Me.WOEngineerCommentsDG.Controls(0).Controls.Count - 1)
            orgArgs = New CommandEventArgs("Add", "OnSave")
            e = New DataGridCommandEventArgs(dgi, Me.WOEngineerCommentsDG, orgArgs)
            WOEngineerCommentsDG_ItemCommand(Me.WOEngineerCommentsDG, e)

            dgi = Me.WOTechCommentsDG.Controls(0).Controls(Me.WOTechCommentsDG.Controls(0).Controls.Count - 1)
            orgArgs = New CommandEventArgs("Add", "OnSave")
            e = New DataGridCommandEventArgs(dgi, Me.WOTechCommentsDG, orgArgs)
            WOTechCommentsDG_ItemCommand(Me.WOTechCommentsDG, e)

            dgi = Me.WOPartsDG.Controls(0).Controls(Me.WOPartsDG.Controls(0).Controls.Count - 1)
            orgArgs = New CommandEventArgs("Add", "OnSave")
            e = New DataGridCommandEventArgs(dgi, Me.WOPartsDG, orgArgs)
            WOPartsDG_ItemCommand(Me.WOPartsDG, e)


        Catch ex As Exception
            retValue = False
        End Try

        Return retValue

    End Function

#End Region

End Class