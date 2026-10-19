' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports Microsoft.Security.Application

Partial Public Class Fleet
    Inherits System.Web.UI.Page
#Region "Private Variables"

    Private _fltIdentity As FltIdentity = Nothing

    Private _aircraftModifyUrl As String = "~/fleet/AircraftModify.aspx"

#End Region

#Region "Procedures (Page Wide)"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        Me.ProgIdHiddenField.Value = _fltIdentity.CurrentProgId
        If Not Me.IsPostBack Then
            Me.AircraftFilterTextBox.Text = String.Empty
            LoadFilterDDLs(_fltIdentity.CurrentProgId)
            FleetGridView_DataBind()
        End If
    End Sub

    Private Sub LoadFilterDDLs(ByVal ProgId As Long)
        Try
            Dim totRows As Long = 0
            Me.LocationFilterDropdownList.Items.Clear()
            Me.LocationFilterDropdownList.Items.Add(New ListItem("< All >", 0))
            Dim locationList As LocationList = LocationManager.GetList(ProgId, 0, 1, 0, -1, -1, totRows, _fltIdentity)
            If Not (locationList Is Nothing) Then
                For Each thisLocation As Location In locationList
                    Me.LocationFilterDropdownList.Items.Add(New ListItem(thisLocation.LocationName, thisLocation.LocactionId))
                Next
            End If
            Me.LocationFilterDropdownList.SelectedValue = Me.LocationFilterDropdownList.Items(0).Value
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

#End Region

#Region "Procedures (List View)"

    Protected Sub LocationFilterDropdownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LocationFilterDropdownList.SelectedIndexChanged
        FleetGridView_DataBind()
    End Sub

    Protected Sub AircraftFilterTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AircraftFilterTextBox.TextChanged
        FleetGridView_DataBind()
    End Sub

    Protected Sub IsActiveDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsActiveDropDownList.SelectedIndexChanged
        FleetGridView_DataBind()
    End Sub

    Protected Sub IsDeletedDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsDeletedDropDownList.SelectedIndexChanged
        FleetGridView_DataBind()
    End Sub

    Protected Sub FleetGridView_DataBind()
        Dim totRows As Integer = 0
        Try
            FleetGridView.DataSource = AircraftInfoManager.GetList(Me.ProgIdHiddenField.Value, Me.LocationFilterDropdownList.SelectedValue, Me.AircraftFilterTextBox.Text, Me.IsActiveDropDownList.SelectedValue, Me.IsDeletedDropDownList.SelectedValue, -1, -1, totRows, _fltIdentity)
            FleetGridView.DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub FleetGridView_PageIndexchanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles FleetGridView.PageIndexChanging
        Dim gv As GridView = CType(sender, GridView)
        gv.PageIndex = e.NewPageIndex
        FleetGridView_DataBind()
    End Sub

    Protected Sub FleetGridView_Something(ByVal sender As System.Object, ByVal e As GridViewCommandEventArgs) Handles FleetGridView.RowCommand
    End Sub

    Protected Sub FleetGridView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FleetGridView.SelectedIndexChanged
        Try
            Dim aircraftId As Integer = FleetGridView.SelectedDataKey.Value
            Dim stringBuilder As New StringBuilder

            With stringBuilder
                .Append(_aircraftModifyUrl)
                .Append("?mode=edit")
                .Append("&aircraftid=")
                .Append(Encoder.UrlEncode(CStr(aircraftId)))
            End With

            Response.Redirect(stringBuilder.ToString, False)

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub FleetGridView_RowDeleting(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles FleetGridView.RowDeleting
        Dim gv As GridView = CType(sender, GridView)
        Dim aircraftId As Integer = CInt(gv.DataKeys(e.RowIndex).Value.ToString)
        Dim aircraft As Aircraft = Nothing
        Try
            aircraft = AircraftManager.GetItem(aircraftId, _fltIdentity)
            AircraftManager.Delete(aircraft, _fltIdentity)
            FleetGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex, aircraft.ErrorsList, aircraft.WarningsList)
        End Try
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click

        Dim stringBuilder As New StringBuilder

        With stringBuilder
            .Append(_aircraftModifyUrl)
            .Append("?mode=add")
        End With
        Response.Redirect("~/fleet/aircraftModify.aspx?mode=add", False)

    End Sub

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