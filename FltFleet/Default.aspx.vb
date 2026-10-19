' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports Microsoft.Security.Application

Partial Public Class _Default
    Inherits System.Web.UI.Page
    Private _fltIdentity As FLTBizObjects.FLT.BO.FltIdentity = Nothing
    Private _SortingColumn As String = "Location"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) OrElse (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If

        Me.UserNameLabel.Text = Server.HtmlEncode(_fltIdentity.UserDisplayName)
        Me.LastLoggedInLabel.Text = Server.HtmlEncode(_fltIdentity.LastLoginDate)

        If Me.IsPostBack Then
            _SortingColumn = SortByHiddenField.Value
        Else
            SortByHiddenField.Value = _SortingColumn
        End If

        FleetGridView_DataBind()

    End Sub

    Protected Sub FleetGridView_DataBind()
        Try
            FleetGridView.DataSource = AircraftStatusManager.GetList(0, _fltIdentity, _SortingColumn, False)
            FleetGridView.DataBind()
        Catch ex As Exception
            Trace.Warn("Error " + ex.Message)
        End Try
    End Sub

    Protected Sub FleetGridView_PageIndexchanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles FleetGridView.PageIndexChanging
        Dim gv As GridView = CType(sender, GridView)
        gv.PageIndex = e.NewPageIndex
        FleetGridView_DataBind()
    End Sub

    Protected Sub FleetGridView_RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles FleetGridView.RowCreated

        Const STATUS_COLUMN_INDEX As Integer = 4
        Dim StatusValue As String = DataBinder.Eval(e.Row.DataItem, "StatusAsString")

        If e.Row.RowType = DataControlRowType.DataRow Then

            If StatusValue = "UP" Then
                e.Row.Cells.Item(STATUS_COLUMN_INDEX).BackColor = Drawing.Color.Green
                e.Row.Cells.Item(STATUS_COLUMN_INDEX).ForeColor = Drawing.Color.White
            End If

            If StatusValue = "DOWN" Then
                e.Row.Cells.Item(STATUS_COLUMN_INDEX).BackColor = Drawing.Color.Red
                e.Row.Cells.Item(STATUS_COLUMN_INDEX).ForeColor = Drawing.Color.Black
            End If

        End If
    End Sub

    Protected Sub FleetGridView_RowCommand(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FleetGridView.RowCommand

        If e.CommandName = "Status_Clicked" Then
            Dim rownum As Integer = Convert.ToInt32(e.CommandArgument)
            Dim key As DataKey = FleetGridView.DataKeys.Item(rownum)

            Response.Redirect("~/EditAircraftStatus.aspx?v=" + Encoder.UrlEncode(Convert.ToString(key.Value)))
        End If

    End Sub

    Protected Sub FleetGridView_Sorting(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles FleetGridView.Sorting

        _SortingColumn = e.SortExpression
        SortByHiddenField.Value = _SortingColumn

        FleetGridView.PageIndex = 0
        FleetGridView_DataBind()

    End Sub

    Protected Sub LogOutButton_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/logout.aspx")
    End Sub

    Protected Sub ShowOffTestBtn_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/OffTestFleet.aspx")
    End Sub
End Class