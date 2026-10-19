' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO


Partial Public Class locations
    Inherits System.Web.UI.Page
#Region "Private Variables"

    Private _fltIdentity As FltIdentity = Nothing
    Private _location As Location

#End Region

#Region "Protected Subs (Location List View)"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = CType(Session("fltIdentity"), FltIdentity)
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        Me.ProgIdHiddenField.Value = _fltIdentity.CurrentProgId
        ClearErrorFields()
        If Not Me.IsPostBack Then
            LocationsGridView_DataBind()
        Else
            _location = CType(ViewState("_location"), Location)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_location") = _location
    End Sub

    Protected Sub LocationsGridView_DataBind()
        Dim totRows As Integer = 0
        Me.LocationsGridView.DataSource = LocationManager.GetList(Me.ProgIdHiddenField.Value, 0, Me.IsActiveDropDownList.SelectedValue, Me.IsDeletedDropDownlist.SelectedValue, -1, -1, totRows, _fltIdentity)
        Me.LocationsGridView.DataBind()
    End Sub

    Protected Sub LocationsGridView_PageIndexChanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles LocationsGridView.PageIndexChanging
        LocationsGridView.PageIndex = e.NewPageIndex
        LocationsGridView_DataBind()
    End Sub

    Protected Sub LocationsGridView_RowEditing(ByVal sender As System.Object, ByVal e As GridViewEditEventArgs) Handles LocationsGridView.RowEditing
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.NewEditIndex)
            Dim locationId As Integer = CInt(CType(row.FindControl("LocationIdLB"), Label).Text)
            _location = LocationManager.GetItem(locationId, _fltIdentity)
            gv.EditIndex = e.NewEditIndex
            LocationsGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub LocationsGridView_RowCancelingEdit(ByVal sender As System.Object, ByVal e As GridViewCancelEditEventArgs) Handles LocationsGridView.RowCancelingEdit
        _location = Nothing
        LocationsGridView.EditIndex = -1
        LocationsGridView_DataBind()
    End Sub

    Protected Sub LocationsGridView_RowUpdating(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles LocationsGridView.RowUpdating
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            With _location
                .LocationName = Server.HtmlDecode(CType(row.FindControl("LocationNameTB"), TextBox).Text)
                .TimeZoneId = CType(row.FindControl("TimeZoneDDL"), DropDownList).SelectedValue
                .IsActive = CType(row.FindControl("IsActiveCB"), CheckBox).Checked
            End With
            LocationManager.Update(_location, _fltIdentity)
            gv.EditIndex = -1
            LocationsGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub LocationsGridView_RowDeleting(ByVal sender As System.Object, ByVal e As GridViewDeleteEventArgs) Handles LocationsGridView.RowDeleting
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            Dim locationId As Integer = CInt(CType(row.FindControl("LocationIdLB"), Label).Text)
            _location = LocationManager.GetItem(locationId, _fltIdentity)
            LocationManager.Delete(_location, _fltIdentity)
            _location = Nothing
            LocationsGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click
        _location = New Location
        LoadDataToForm()
        Me.LocationMultiView.ActiveViewIndex = 1
    End Sub

    Protected Sub IsActiveDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsActiveDropDownList.SelectedIndexChanged
        LocationsGridView_DataBind()
    End Sub

    Protected Sub IsDeletedDropDownlist_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsDeletedDropDownlist.SelectedIndexChanged
        LocationsGridView_DataBind()
    End Sub

#End Region

#Region "Protected Procedures (Location Modify View)"

    Protected Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        LoadDataToObject()
        Try
            LocationManager.Save(_location, _fltIdentity)
            Me.LocationMultiView.ActiveViewIndex = 0
            LocationsGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Me.LocationMultiView.ActiveViewIndex = 0
        LocationsGridView_DataBind()
    End Sub

#End Region

    Protected Function GetTimeZones() As UTCTimeZoneList
        Return UTCTimeZoneManager.GetList(_fltIdentity)
    End Function

    Private Sub PopulateDropDownLists()
        Dim totRows As Long = 0
        Me.TimeZoneDropDownList2.Items.Clear()
        Me.TimeZoneDropDownList2.Items.Add(New ListItem("< Select Time Zone >", String.Empty))
        For Each thisTimeZone As UTCTimeZone In UTCTimeZoneManager.GetList(1, -1, -1, totRows, _fltIdentity)
            Me.TimeZoneDropDownList2.Items.Add(New ListItem(thisTimeZone.TimeZoneCode, thisTimeZone.TimeZoneID.ToString))
        Next
    End Sub

    Private Sub LoadDataToForm()
        ClearErrorFields()
        PopulateDropDownLists()
        Me.LocationNameTextBox.Text = Server.HtmlEncode(_location.LocationName)
        Me.TimeZoneDropDownList2.SelectedValue = _location.TimeZoneCode
    End Sub

    Private Sub LoadDataToObject()
        ClearErrorFields()
        Try
            _location.ProgId = Me.ProgIdHiddenField.Value
            _location.LocationName = Server.HtmlDecode(Me.LocationNameTextBox.Text)
            _location.TimeZoneId = Me.TimeZoneDropDownList2.SelectedValue
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

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
        If _location.BrokenRules.Count <> 0 Then
            Me.BrokenRulesList.Visible = True
            For Each thisRule As BrokenRule In _location.BrokenRules
                Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
            Next
        End If
    End Sub
End Class