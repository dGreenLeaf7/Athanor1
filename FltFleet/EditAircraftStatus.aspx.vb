' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO

Partial Public Class EditAircraftStatus
    Inherits System.Web.UI.Page
    Private _fltIdentity As FLTBizObjects.FLT.BO.FltIdentity = Nothing


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) OrElse (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If

        If Not Me.IsPostBack Then

            StatusDropDown.Items.Clear()
            StatusDropDown.Items.Add("OFF TEST")
            StatusDropDown.Items.Add("UP")
            StatusDropDown.Items.Add("DOWN")

            Dim aircraftId As Integer = CInt(Server.UrlDecode(Request.QueryString.Item("v")))

            Dim CurrentAircraft As AircraftStatus = AircraftStatusDB.GetItem(aircraftId)

            hidAircraftId.Value = aircraftId
            MSNLabel.Text = Server.HtmlEncode(CurrentAircraft.MSN)
            EngineerCommentTextBox.Text = Server.HtmlEncode(CurrentAircraft.EngineerComment)
            HangarCommentTextBox.Text = Server.HtmlEncode(CurrentAircraft.HangarComment)
            StatusDropDown.SelectedIndex = CurrentAircraft.Status

            If AircraftStatusManager.CanEdit(_fltIdentity) = False Then
                Me.lblWarning.Text = "You cannot edit the aircraft status at this time.<br/>"
                Me.SaveButton.Enabled = False
                Me.StatusDropDown.Enabled = False
                Me.EngineerCommentTextBox.Enabled = False
                Me.HangarCommentTextBox.Enabled = False
            End If
        End If

    End Sub

    Protected Sub SaveButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        AircraftStatusDB.SaveStatusUpdate(Convert.ToInt32(hidAircraftId.Value), StatusDropDown.SelectedIndex, Server.HtmlDecode(EngineerCommentTextBox.Text), Server.HtmlDecode(HangarCommentTextBox.Text))
        Response.Redirect("~/Default.aspx")
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Response.Redirect("~/Default.aspx")
    End Sub


End Class