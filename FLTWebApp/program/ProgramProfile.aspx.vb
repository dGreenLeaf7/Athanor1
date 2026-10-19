' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO


Partial Public Class ProgramProfile
    Inherits System.Web.UI.Page
    Private _fltIdentity As FltIdentity = Nothing
    Private _program As Program = Nothing

    Private Sub ClearErrorFields()
        Me.ErrorMessageLabel.Text = String.Empty
        Me.ErrorMessageLabel.Visible = False
        Me.SuccessMessageLabel.Text = String.Empty
        Me.SuccessMessageLabel.Visible = False
        Me.BrokenRulesList.Items.Clear()
        Me.BrokenRulesList.Visible = False
    End Sub

    Private Sub LoadPage()

        ClearErrorFields()

        Me.ProgNameTextBox.Text = Server.HtmlEncode(_program.ProgName)
        Me.ProgDescriptionTextBox.Text = Server.HtmlEncode(_program.ProgDescription)
        Me.ProgVersionTextBox.Text = Server.HtmlEncode(_program.ProgVersion)
        Me.ProgramLogoTextBox.Text = Server.HtmlEncode(_program.ProgramLogo)
        Me.ReportLogoTextBox.Text = Server.HtmlEncode(_program.ReportLogo)
        Me.YearLogoTextBox.Text = Server.HtmlEncode(_program.YearLogo)
        Me.ProgStatusDropDownList.SelectedValue = _program.ProgStatus
        Me.DateOpenedTextBox.Text = Server.HtmlEncode(_program.DateOpened)
        Me.DateClosedTextBox.Text = Server.HtmlEncode(_program.DateClosed)
        Me.ActualValueTextBox.Text = _program.ActualValue
        Me.ProjectCodeTextBox.Text = Server.HtmlEncode(_program.ProjectCode)
        Me.ActivityCodeTextBox.Text = Server.HtmlEncode(_program.ActivityCode)
        Me.DeptCodeTextBox.Text = Server.HtmlEncode(_program.DeptCode)
        Me.CompanyNameTextBox.Text = Server.HtmlEncode(_program.CompanyName)
        Me.NearEndUsageTextBox.Text = _program.NearEndUsage
        Me.AvgDailyUsageTextBox.Text = _program.AvgDailyUsage
        Me.WeeklyFlightDaysTextBox.Text = _program.WeeklyFlightDays

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Me.IsPostBack Then
            _program = ProgramManager.GetItem(_fltIdentity.CurrentProgId, _fltIdentity)
            LoadPage()
        Else
            _program = CType(ViewState("_program"), Program)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_program") = _program
    End Sub

    Protected Sub SaveButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveButton.Click

        ClearErrorFields()

        Me.BrokenRulesList.Items.Clear()
        Me.BrokenRulesList.Visible = False

        _program.ProgName = Server.HtmlDecode(Me.ProgNameTextBox.Text)
        _program.ProgDescription = Server.HtmlDecode(Me.ProgDescriptionTextBox.Text)
        _program.ProgVersion = Server.HtmlDecode(Me.ProgVersionTextBox.Text)
        _program.ProgramLogo = Server.HtmlDecode(Me.ProgramLogoTextBox.Text)
        _program.ReportLogo = Server.HtmlDecode(Me.ReportLogoTextBox.Text)
        _program.YearLogo = Server.HtmlDecode(Me.YearLogoTextBox.Text)
        _program.DateOpened = Server.HtmlDecode(Me.DateOpenedTextBox.Text)
        _program.DateClosed = Server.HtmlDecode(Me.DateClosedTextBox.Text)
        _program.ActualValue = Me.ActualValueTextBox.Text
        _program.ProjectCode = Server.HtmlDecode(Me.ProjectCodeTextBox.Text)
        _program.ActivityCode = Server.HtmlDecode(Me.ActivityCodeTextBox.Text)
        _program.DeptCode = Server.HtmlDecode(Me.DeptCodeTextBox.Text)
        _program.CompanyName = Server.HtmlDecode(Me.CompanyNameTextBox.Text)
        _program.NearEndUsage = Me.NearEndUsageTextBox.Text
        _program.AvgDailyUsage = Me.AvgDailyUsageTextBox.Text
        _program.WeeklyFlightDays = Me.WeeklyFlightDaysTextBox.Text

        Try
            ProgramManager.Update(_program, _fltIdentity)
            Me.SuccessMessageLabel.Visible = True
            Me.SuccessMessageLabel.Text = "Record updated successfully!"
        Catch ex As Exception
            Me.ErrorMessageLabel.Text = ex.Message
            Me.ErrorMessageLabel.Visible = True
            If _program.BrokenRules.Count <> 0 Then
                Me.BrokenRulesList.Visible = True
                For Each thisRule As BrokenRule In _program.BrokenRules
                    Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
                Next
            End If
        End Try

        _program = ProgramManager.GetItem(_program.ProgID, _fltIdentity)

    End Sub

End Class

