' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO

Partial Public Class WorkOrdersList
    Inherits System.Web.UI.Page
    Private _fltIdentity As FltIdentity = Nothing

    Private _workOrderModifyUrl As String = "~/fleet/workOrderModify.aspx"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        ClearErrorFields()
        Me.ProgIdHiddenField.Value = _fltIdentity.CurrentProgId
        If Not Me.IsPostBack Then
            Me.AircraftFilterTB.Text = String.Empty
            LoadFilterDDLs(Me.ProgIdHiddenField.Value)
            WorkOrdersGV_DataBind()
        End If

    End Sub

    Private Sub LoadFilterDDLs(ByVal ProgId As Integer)
        Dim totRows As Long = 0
        Me.LocationFilterDDL.Items.Clear()
        Me.LocationFilterDDL.Items.Add(New ListItem("< All >", 0))
        Dim locationList As LocationList = LocationManager.GetList(ProgId, 0, 1, 0, -1, -1, totRows, _fltIdentity)
        If Not (locationList Is Nothing) Then
            For Each thisLocation As Location In locationList
                Me.LocationFilterDDL.Items.Add(New ListItem(thisLocation.LocationName, thisLocation.LocactionId))
            Next
        End If
        Me.LocationFilterDDL.SelectedValue = Me.LocationFilterDDL.Items(0).Value
    End Sub

    Private Sub WorkOrdersGV_DataBind()
        WorkOrdersGV.DataSource = WorkOrderInfoManager.GetList(Me.ProgIdHiddenField.Value, 0, Me.AircraftFilterTB.Text, 0, 0, String.Empty, _fltIdentity)
        WorkOrdersGV.DataBind()
    End Sub

    Protected Sub WorkOrdersGV_PageIndexchanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles WorkOrdersGV.PageIndexChanging
        Dim gv As GridView = CType(sender, GridView)
        gv.PageIndex = e.NewPageIndex
        WorkOrdersGV_DataBind()
    End Sub

    Protected Sub WorkOrdersGV_RowCommand(ByVal sender As System.Object, ByVal e As GridViewCommandEventArgs) Handles WorkOrdersGV.RowCommand
    End Sub

    Protected Sub WorkOrdersGV_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WorkOrdersGV.SelectedIndexChanged
        Try
            Dim aircraftId As Integer = WorkOrdersGV.SelectedDataKey.Value
            Dim stringBuilder As New StringBuilder

            With stringBuilder
                .Append(_workOrderModifyUrl)
                .Append("?mode=edit")
                .Append("&workorderid=")
                .Append(CStr(aircraftId))
            End With

            Response.Redirect(stringBuilder.ToString, False)

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub WorkOrdersGV_RowDeleting(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles WorkOrdersGV.RowDeleting
        Dim gv As GridView = CType(sender, GridView)
        Dim WorkOrderId As Integer = CInt(gv.DataKeys(e.RowIndex).Value.ToString)
        Try
            Dim workOrder As WorkOrder = WorkOrderManager.GetItem(WorkOrderId, _fltIdentity)
            WorkOrderManager.Delete(workOrder, _fltIdentity)
            WorkOrdersGV_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click

        Dim stringBuilder As New StringBuilder

        With stringBuilder
            .Append(_workOrderModifyUrl)
            .Append("?mode=add")
        End With
        Response.Redirect(stringBuilder.ToString, False)

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
        ProcessException(ex, New BrokenRulesList, New BrokenRulesList)
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
            Me.WarningsCheckList.DataSource = WarningsList
            Me.WarningsCheckList.DataBind()
        End If
    End Sub

#End Region

End Class