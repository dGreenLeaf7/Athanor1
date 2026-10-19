' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL


Partial Public Class TimeZones
    Inherits System.Web.UI.Page
    Private _fltIdentity As FltIdentity = Nothing
    Private _utcTimeZone As UTCTimeZone = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        ClearErrorFields()
        If Not Me.IsPostBack Then
            PopulateDropDownlists()
            TimeZonesGridView_DataBind()
        Else
            _utcTimeZone = CType(ViewState("_utcTimeZone"), UTCTimeZone)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_utcTimeZone") = _utcTimeZone
    End Sub

    Protected Sub IsActiveDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsActiveDropDownList.SelectedIndexChanged
        Me.TimeZonesGridView.DataBind()
    End Sub

    Protected Sub TimeZonesGridView_DataBind()
        Dim totRows As Integer = 0
        TimeZonesGridView.DataSource = UTCTimeZoneManager.GetList(Me.IsActiveDropDownList.SelectedValue, -1, -1, totRows, _fltIdentity)
        TimeZonesGridView.DataBind()
    End Sub

    Protected Sub TimeZoneGridView_PageIndexChanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles TimeZonesGridView.PageIndexChanging
        Dim gv As GridView = CType(sender, GridView)
        gv.PageIndex = e.NewPageIndex
        TimeZonesGridView_DataBind()
    End Sub

    Protected Sub TimeZoneGridView_RowEditing(ByVal sender As System.Object, ByVal e As GridViewEditEventArgs) Handles TimeZonesGridView.RowEditing
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.NewEditIndex)
            Dim timeZoneId As Integer = CInt(CType(row.FindControl("TimeZoneIdLB"), Label).Text)
            _utcTimeZone = UTCTimeZoneManager.GetItem(timeZoneId, _fltIdentity)
            gv.EditIndex = e.NewEditIndex
            TimeZonesGridView_Databind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub TimeZoneGridView_CancelingEdit(ByVal sender As System.Object, ByVal e As GridViewCancelEditEventArgs) Handles TimeZonesGridView.RowCancelingEdit
        TimeZonesGridView.EditIndex = -1
        _utcTimeZone = Nothing
        TimeZonesGridView_DataBind()
    End Sub

    Protected Sub TimeZoneGridView_RowUpdating(ByVal sender As System.Object, ByVal e As GridViewUpdateEventArgs) Handles TimeZonesGridView.RowUpdating
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            With _utcTimeZone
                .TimeZoneCode = Server.HtmlDecode(CType(row.FindControl("TimeZoneCodeTB"), TextBox).Text)
                .TimeZoneDescr = Server.HtmlDecode(CType(row.FindControl("TimeZoneDescrTB"), TextBox).Text)
                .TimeZoneOffset = Server.HtmlDecode(CType(row.FindControl("TimeZoneOffsetTB"), TextBox).Text)
                .DaylightSavingTime = CType(row.FindControl("DaylightSavingTimeCB"), CheckBox).Checked
                .DaylightSavingOffset = Server.HtmlDecode(CType(row.FindControl("DaylightSavingOffsetTB"), TextBox).Text)
                .DaylightSavingBegin = Server.HtmlDecode(CType(row.FindControl("DaylightSavingBeginTB"), TextBox).Text)
                .DaylightSavingEnd = Server.HtmlDecode(CType(row.FindControl("DaylightSavingEndTB"), TextBox).Text)
                .IsActive = CType(row.FindControl("IsActiveCB"), CheckBox).Checked
            End With
            UTCTimeZoneManager.Update(_utcTimeZone, _fltIdentity)
            _utcTimeZone = Nothing
            gv.EditIndex = -1
            TimeZonesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub TimeZoneGridView_RowDeleting(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles TimeZonesGridView.RowDeleting
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            Dim timeZoneId As Integer = CInt(CType(row.FindControl("TimeZoneIdLB"), Label).Text)
            _utcTimeZone = UTCTimeZoneManager.GetItem(timeZoneId, _fltIdentity)
            UTCTimeZoneManager.Delete(_utcTimeZone, _fltIdentity)
            _utcTimeZone = Nothing
            TimeZonesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        LoadDataToObject()
        Try
            UTCTimeZoneManager.Save(_utcTimeZone, _fltIdentity)
            _utcTimeZone = Nothing
            Me.TimeZonesMultiView.ActiveViewIndex = 0
            TimeZonesGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Me.TimeZonesMultiView.ActiveViewIndex = 0
        TimeZonesGridView_DataBind()
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click
        _utcTimeZone = New UTCTimeZone
        Me.AddEditTitleLabel.Text = "Adding New Entry"
        LoadDataToForm()
        Me.TimeZonesMultiView.ActiveViewIndex = 1
    End Sub

    Private Sub LoadDataToForm()

        ClearErrorFields()

        Me.TimeZoneCodeTB.Text = Server.HtmlEncode(_utcTimeZone.TimeZoneCode)
        Me.TimeZoneOffsetTB.Text = Server.HtmlEncode(_utcTimeZone.TimeZoneOffset)
        Me.TimeZoneDescrTB.Text = Server.HtmlEncode(_utcTimeZone.TimeZoneDescr)
        Me.DaylightSavingCB.Checked = _utcTimeZone.DaylightSavingTime
        Me.DaylightSavingOffsetTB.Text = Server.HtmlEncode(_utcTimeZone.TimeZoneOffset.ToString)
        Me.DaylightSavingBeginTB.Text = Server.HtmlEncode(_utcTimeZone.DaylightSavingBegin)
        Me.DaylightSavingEndTB.Text = Server.HtmlEncode(_utcTimeZone.DaylightSavingEnd)
        Me.IsActiveCB.Checked = _utcTimeZone.IsActive

    End Sub

    Private Sub LoadDataToObject()

        ClearErrorFields()

        Try
            _utcTimeZone.TimeZoneCode = Server.HtmlDecode(Me.TimeZoneCodeTB.Text)
            _utcTimeZone.TimeZoneOffset = Server.HtmlDecode(Me.TimeZoneOffsetTB.Text)
            _utcTimeZone.TimeZoneDescr = Server.HtmlDecode(Me.TimeZoneDescrTB.Text)
            _utcTimeZone.DaylightSavingTime = Me.DaylightSavingCB.Checked
            _utcTimeZone.DaylightSavingOffset = Server.HtmlDecode(Me.DaylightSavingOffsetTB.Text)
            _utcTimeZone.DaylightSavingBegin = Server.HtmlDecode(Me.DaylightSavingBeginTB.Text)
            _utcTimeZone.DaylightSavingEnd = Server.HtmlDecode(Me.DaylightSavingEndTB.Text)
            _utcTimeZone.IsActive = Me.IsActiveCB.Checked

        Catch ex As Exception
            ProcessException(ex)
        End Try

    End Sub

    Private Sub PopulateDropDownlists()
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
        If _utcTimeZone.BrokenRules.Count <> 0 Then
            Me.BrokenRulesList.Visible = True
            For Each thisRule As BrokenRule In _utcTimeZone.BrokenRules
                Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
            Next
        End If
    End Sub
End Class