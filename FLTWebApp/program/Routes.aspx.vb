' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO

Partial Public Class Routes
    Inherits System.Web.UI.Page

#Region "Private Variables"

    Private _fltIdentity As FltIdentity = Nothing
    Private _Route As Route

#End Region

#Region "Protected Subs (Route List View)"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        Me.ProgIdHiddenField.Value = _fltIdentity.CurrentProgId
        ClearErrorFields()
        If Not Me.IsPostBack Then
            LocationFilterDropDownList_DataBind()
            RoutesGridView_DataBind()
        Else
            _Route = CType(ViewState("_Route"), Route)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_Route") = _Route
    End Sub

    Protected Sub LocationFilterDropDownList_DataBind()
        For Each thisLocation As Location In LocationManager.GetList(Me.ProgIdHiddenField.Value, _fltIdentity)
            Dim newListItem As New ListItem(thisLocation.LocationName, thisLocation.LocactionId)
            Me.LocationFilterDropDownList.Items.Add(newListItem)
        Next
        Me.LocationFilterDropDownList.SelectedValue = Me.LocationFilterDropDownList.Items(0).Value
    End Sub

    Protected Sub RoutesGridView_DataBind()
        Dim totRows As Integer = 0
        RoutesGridView.DataSource = RouteManager.GetList(Me.ProgIdHiddenField.Value, LocationFilterDropDownList.SelectedValue, 0, Me.IsActiveDropDownList.SelectedValue, Me.IsDeletedDropDownlist.SelectedValue, -1, -1, totRows, _fltIdentity)
        RoutesGridView.DataBind()
    End Sub

    Protected Sub RoutesGridView_PageIndexChanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles RoutesGridView.PageIndexChanging
        RoutesGridView.PageIndex = e.NewPageIndex
        RoutesGridView_DataBind()
    End Sub

    Protected Sub RoutesGridView_RowEditing(ByVal sender As System.Object, ByVal e As GridViewEditEventArgs) Handles RoutesGridView.RowEditing
        Dim gv As GridView = CType(sender, GridView)
        Dim row As GridViewRow = gv.Rows(e.NewEditIndex)
        Dim RouteId As Integer = CInt(CType(row.FindControl("RouteIdLB"), Label).Text)
        _Route = RouteManager.GetItem(RouteId, _fltIdentity)
        gv.EditIndex = e.NewEditIndex
        RoutesGridView_DataBind()
    End Sub

    Protected Sub RoutesGridView_RowCancelingEdit(ByVal sender As System.Object, ByVal e As GridViewCancelEditEventArgs) Handles RoutesGridView.RowCancelingEdit
        _Route = Nothing
        RoutesGridView.EditIndex = -1
        RoutesGridView_DataBind()
    End Sub

    Protected Sub RoutesGridView_RowUpdating(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles RoutesGridView.RowUpdating
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            With _Route
                .Name = Server.HtmlDecode(CType(row.FindControl("NameTB"), TextBox).Text)
                .Description = Server.HtmlDecode(CType(row.FindControl("DescriptionTB"), TextBox).Text)
                .TimeZoneId = CType(row.FindControl("TimeZoneDDL"), DropDownList).SelectedValue
                .IsActive = CType(row.FindControl("IsActiveCB"), CheckBox).Checked
            End With
            RouteManager.Update(_Route, _fltIdentity)
            _Route = Nothing
            gv.EditIndex = -1
            RoutesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub RoutesGridView_RowDeleting(ByVal sender As System.Object, ByVal e As GridViewDeleteEventArgs) Handles RoutesGridView.RowDeleting
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            Dim RouteId As Integer = CInt(CType(row.FindControl("RouteIdLB"), Label).Text)
            _Route = RouteManager.GetItem(RouteId, _fltIdentity)
            RouteManager.Delete(_Route, _fltIdentity)
            _Route = Nothing
            RoutesGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click
        _Route = New Route
        LoadDataToForm()
        Me.RouteMultiView.ActiveViewIndex = 1
    End Sub

    Protected Sub IsActiveDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsActiveDropDownList.SelectedIndexChanged
        RoutesGridView_DataBind()
    End Sub

    Protected Sub IsDeletedDropDownlist_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsDeletedDropDownlist.SelectedIndexChanged
        RoutesGridView_DataBind()
    End Sub

#End Region

#Region "Protected Procedures (Route Add View)"

    Protected Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        LoadDataToObject()
        Try
            RouteManager.Save(_Route, _fltIdentity)
            Me.RouteMultiView.ActiveViewIndex = 0
            LocationFilterDropDownList_DataBind()
            RoutesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Me.RouteMultiView.ActiveViewIndex = 0
        LocationFilterDropDownList_DataBind()
        RoutesGridView_DataBind()
    End Sub

#End Region

    Protected Function GetTimeZones() As UTCTimeZoneList
        Return UTCTimeZoneManager.GetList(_fltIdentity)
    End Function

    Protected Function GetLocations() As LocationList
        Return LocationManager.GetList(Me.ProgIdHiddenField.Value, _fltIdentity)
    End Function

    Private Sub PopulateModifyDropDownLists()
        Try
            Dim totRows As Long = 0
            Me.LocationDropDownList.Items.Clear()
            Me.LocationDropDownList.Items.Add(New ListItem("< Select Location> ", 0))
            For Each thisLocation As Location In LocationManager.GetList(Me.ProgIdHiddenField.Value, 0, 1, 0, -1, -1, totRows, _fltIdentity)
                Me.LocationDropDownList.Items.Add(New ListItem(thisLocation.LocationName, thisLocation.LocactionId))
            Next
            Me.TimeZoneDropDownList.Items.Clear()
            Me.TimeZoneDropDownList.Items.Add(New ListItem("< Select Time Zone >", 0))
            For Each thisTimeZone As UTCTimeZone In UTCTimeZoneManager.GetList(1, -1, -1, totRows, _fltIdentity)
                Me.TimeZoneDropDownList.Items.Add(New ListItem(thisTimeZone.TimeZoneCode, thisTimeZone.TimeZoneID))
            Next
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Private Sub LoadDataToForm()
        Try
            PopulateModifyDropDownLists()
            Me.LocationDropDownList.SelectedValue = _Route.LocationId
            Me.RouteNameTextBox.Text = Server.HtmlEncode(_Route.Name)
            Me.RouteDescrTextBox.Text = Server.HtmlEncode(_Route.Description)
            Me.TimeZoneDropDownList.SelectedValue = _Route.TimeZoneId
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Private Sub LoadDataToObject()
        Try
            _Route.LocationId = Me.LocationDropDownList.SelectedValue
            _Route.Name = Server.HtmlDecode(Me.RouteNameTextBox.Text)
            _Route.Description = Server.HtmlDecode(Me.RouteDescrTextBox.Text)
            _Route.TimeZoneId = Me.TimeZoneDropDownList.SelectedValue
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub LocationFilterDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LocationFilterDropDownList.SelectedIndexChanged
        RoutesGridView_DataBind()
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
        If _Route.BrokenRules.Count <> 0 Then
            Me.BrokenRulesList.Visible = True
            For Each thisRule As BrokenRule In _Route.BrokenRules
                Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
            Next
        End If
    End Sub

End Class