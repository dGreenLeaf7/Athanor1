' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO

Partial Public Class FlightsList
    Inherits System.Web.UI.Page
    Private _fltIdentity As FltIdentity = Nothing

#Region "Procedures (Page Wide)"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        Me.ProgIdHiddenField.Value = _fltIdentity.CurrentProgId
        ClearErrorFields()
        If Not Me.IsPostBack Then
            LoadLocationFilterDDL()
            FlightsGridView_DataBind()
        End If
    End Sub

#End Region

#Region "Procedures (Flight List View)"

    Protected Sub LocationFilterDropdownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LocationFilterDropdownList.SelectedIndexChanged
        FlightsGridView_DataBind()
    End Sub

    Protected Sub AircraftTextFilter_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AircraftFilterTextBox.TextChanged
        FlightsGridView_DataBind()
    End Sub

    Protected Sub FilterDateFrom_Changed(ByVal sender As System.Object, ByVal e As EventArgs) Handles FilterDateFromTB.TextChanged
        Try
            If Me.FilterDateFromTB.Text.Length > 0 Then
                If Not IsDate(Me.FilterDateFromTB.Text) Then
                    Throw New Exception("Date From value must either be a valid date or be empty")
                End If
            End If
            FlightsGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub FilterDateThru_Changed(ByVal sender As System.Object, ByVal e As EventArgs) Handles FilterDateThruTB.TextChanged
        Try
            If Me.FilterDateThruTB.Text.Length > 0 Then
                If Not IsDate(Me.FilterDateThruTB.Text) Then
                    Throw New Exception("Date Thru value must either be a valid date or be empty")
                End If
            End If
            FlightsGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub IsApprovedDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsApprovedDropDownList.SelectedIndexChanged
        FlightsGridView_DataBind()
    End Sub

    Protected Sub IsActiveDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsActiveDropDownList.SelectedIndexChanged
        FlightsGridView_DataBind()
    End Sub

    Protected Sub IsDeletedDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsDeletedDropDownList.SelectedIndexChanged
        FlightsGridView_DataBind()
    End Sub

    Protected Sub FlightsGridView_DataBind()
        Dim totRows As Integer = 0
        FlightsGridView.DataSource = FlightInfoManager.GetList(0, Me.AircraftFilterTextBox.Text, CInt(Me.LocationFilterDropdownList.SelectedValue), 0, 0, 0, Me.FilterDateFromTB.Text, Me.FilterDateThruTB.Text, Me.IsApprovedDropDownList.SelectedValue, Me.IsActiveDropDownList.SelectedValue, Me.IsDeletedDropDownList.SelectedValue, -1, -1, totRows, _fltIdentity)
        FlightsGridView.DataBind()
    End Sub

    Protected Sub FlightsGridView_PageIndexChanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles FlightsGridView.PageIndexChanging
        Dim gv As GridView = CType(sender, GridView)
        gv.PageIndex = e.NewPageIndex
        FlightsGridView_DataBind()
    End Sub

    Protected Sub FlightsGridView_RowCommand(ByVal sender As System.Object, ByVal e As GridViewCommandEventArgs) Handles FlightsGridView.RowCommand
    End Sub

    Protected Sub FlightsGridView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FlightsGridView.SelectedIndexChanged
        Dim flight As New Flight
        Try
            flight = FlightManager.GetItem(FlightsGridView.SelectedDataKey.Value, _fltIdentity)
            Response.Redirect("~/Fleet/FlightModify.aspx?mode=edit&flightid=" & flight.FlightId.ToString)
        Catch ex As Exception
            ProcessException(ex, flight.ErrorsList, flight.WarningsList)
        End Try
    End Sub

    Protected Sub FlightsGridView_RowDeleting(ByVal sender As System.Object, ByVal e As GridViewDeleteEventArgs) Handles FlightsGridView.RowDeleting
        Dim gv As GridView = CType(sender, GridView)
        Dim FlightId As Integer = CInt(gv.DataKeys(e.RowIndex).Value.ToString)
        Dim flight As Flight = Nothing
        Try
            flight = FlightManager.GetItem(FlightId, _fltIdentity)
            FlightManager.Delete(flight, _fltIdentity)
            FlightsGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex, flight.ErrorsList, flight.WarningsList)
        End Try
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click
        Response.Redirect("~/Fleet/FlightModify.aspx?mode=add")
    End Sub

    Protected Sub LoadLocationFilterDDL()
        For Each thisLoc As Location In LocationManager.GetList(Me.ProgIdHiddenField.Value, _fltIdentity)
            Me.LocationFilterDropdownList.Items.Add(New ListItem(thisLoc.LocationName, thisLoc.LocactionId))
        Next
        Me.LocationFilterDropdownList.SelectedValue = Me.LocationFilterDropdownList.Items(0).Value
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