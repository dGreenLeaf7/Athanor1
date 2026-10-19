' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System.Web.UI.HtmlControls
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports Microsoft.Security.Application


Partial Public Class programs
    Inherits System.Web.UI.Page

    Private _fltIdentity As FltIdentity = Nothing
    Private _program As Program = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        ClearErrorFields()
        If Not Me.IsPostBack Then
            ProgramGridView_DataBind()
        Else
            _program = CType(ViewState("_program"), Program)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_program") = _program
    End Sub

    Protected Sub ProgramGridView_DataBind()
        Dim totRows As Integer = 0
        ProgramGridView.DataSource = ProgramManager.GetList(Me.IsActiveDropDownList.SelectedValue, Me.IsDeletedDropDownList.SelectedValue, -1, -1, totRows)
        ProgramGridView.DataBind()
    End Sub

    Protected Sub ProgramGridView_PageIndexChanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles ProgramGridView.PageIndexChanging
        ProgramGridView.PageIndex = e.NewPageIndex
        ProgramGridView_DataBind()
    End Sub

    Protected Sub ProgramGridView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProgramGridView.SelectedIndexChanged
        Try
            _program = ProgramManager.GetItem(CLng(ProgramGridView.SelectedDataKey.Value), _fltIdentity)
            LoadDataToForm()
            Me.ProgramMultiView.ActiveViewIndex = 1
            Me.CloneTableRow.Visible = False
            PopulateCloneProgDDL()
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
    End Sub

    Protected Sub ProgramGridView_RowDeleting(ByVal sender As System.Object, ByVal e As GridViewDeleteEventArgs) Handles ProgramGridView.RowDeleting
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            Dim progId As Integer = CInt(CType(row.FindControl("ProgIdLB"), Label).Text)
            _program = ProgramManager.GetItem(progId, _fltIdentity)
            ProgramManager.Delete(_program, _fltIdentity)
            _program = Nothing
            ProgramGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Addbutton.Click
        Try
            _program = New Program()
            Me.AddEditTitleLabel.Text = "Adding New Program"
            LoadDataToForm()
            PopulateCloneProgDDL()
            Me.ProgramMultiView.ActiveViewIndex = 1
            Me.CloneTableRow.Visible = True
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
    End Sub

    Protected Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click

        Try
            LoadDataToObject()
            If Me.CloneProgDDL.SelectedValue = 0 Then
                ProgramManager.Save(_program, _fltIdentity)
            Else
                ProgramManager.Clone(CInt(Me.CloneProgDDL.SelectedValue), _program, _fltIdentity)
            End If
            _program = Nothing
            Me.ProgramMultiView.ActiveViewIndex = 0
            ProgramGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        _program = Nothing
        Me.ProgramMultiView.ActiveViewIndex = 0
        ProgramGridView_DataBind()
    End Sub

    Protected Sub IsActiveDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsActiveDropDownList.SelectedIndexChanged
        ProgramGridView_DataBind()
    End Sub

    Protected Sub IsDeletedDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsDeletedDropDownList.SelectedIndexChanged
        ProgramGridView_DataBind()
    End Sub

    Private Sub ClearErrorFields()
        Me.ErrorMessageLabel.Text = String.Empty
        Me.ErrorMessageLabel.Visible = False
        Me.SuccessMessageLabel.Text = String.Empty
        Me.SuccessMessageLabel.Visible = False
        Me.BrokenRulesList.Items.Clear()
        Me.BrokenRulesList.Visible = False
    End Sub

    Private Sub LoadDataToObject()

        _program.ProgName = Server.HtmlDecode(Me.ProgNameTextBox.Text)
        _program.ProgDescription = Server.HtmlDecode(Me.ProgDescriptionTextBox.Text)
        _program.ProgVersion = Server.HtmlDecode(Me.ProgVersionTextBox.Text)
        _program.ProgramLogo = Server.HtmlDecode(Me.ProgramLogoTextBox.Text)
        _program.ReportLogo = Server.HtmlDecode(Me.ReportLogoTextBox.Text)
        _program.YearLogo = Server.HtmlDecode(Me.YearLogoTextBox.Text)
        _program.DateOpened = Server.HtmlDecode(Me.DateOpenedTextBox.Text)
        _program.DateClosed = Server.HtmlDecode(Me.DateClosedTextBox.Text)

        Dim tryDecimal As Decimal
        If Decimal.TryParse(Me.ActualValueTextBox.Text, tryDecimal) Then
            _program.ActualValue = tryDecimal
        Else
            Throw New ApplicationException("Value for 'Actual Value' must be a decimal number")
        End If

        _program.ProjectCode = Server.HtmlDecode(Me.ProjectCodeTextBox.Text)
        _program.ActivityCode = Server.HtmlDecode(Me.ActivityCodeTextBox.Text)
        _program.DeptCode = Server.HtmlDecode(Me.DeptCodeTextBox.Text)
        _program.CompanyName = Server.HtmlDecode(Me.CompanyNameTextBox.Text)

        Dim tryInt As Int32
        If Int32.TryParse(Me.NearEndUsageTextBox.Text, tryInt) Then
            _program.NearEndUsage = tryInt
        Else
            Throw New ApplicationException("Value for 'Near End Usage' must be a whole number")
        End If
        If Int32.TryParse(Me.AvgDailyUsageTextBox.Text, tryInt) Then
            _program.AvgDailyUsage = tryInt
        Else
            Throw New ApplicationException("Value for 'Avg Daily Usage' must be a whole number")
        End If
        If Int32.TryParse(Me.WeeklyFlightDaysTextBox.Text, tryInt) Then
            _program.WeeklyFlightDays = tryInt
        Else
            Throw New ApplicationException("Value for 'Weekly Test Days' must be a whole number")
        End If

    End Sub

    Private Sub LoadDataToForm()

        Me.ProgNameTextBox.Text = Server.HtmlEncode(_program.ProgName)
        Me.ProgDescriptionTextBox.Text = Server.HtmlEncode(_program.ProgDescription)
        Me.ProgVersionTextBox.Text = Server.HtmlEncode(_program.ProgVersion)
        Me.ProgramLogoTextBox.Text = Server.HtmlEncode(_program.ProgramLogo)
        Me.ReportLogoTextBox.Text = Server.HtmlEncode(_program.ReportLogo)
        Me.YearLogoTextBox.Text = Server.HtmlEncode(_program.YearLogo)
        Me.DateOpenedTextBox.Text = Server.HtmlEncode(_program.DateOpened)
        Me.DateClosedTextBox.Text = Server.HtmlEncode(_program.DateClosed)
        Me.ActualValueTextBox.Text = Server.HtmlEncode(_program.ActualValue)
        Me.ProjectCodeTextBox.Text = Server.HtmlEncode(_program.ProjectCode)
        Me.ActivityCodeTextBox.Text = Server.HtmlEncode(_program.ActivityCode)
        Me.DeptCodeTextBox.Text = Server.HtmlEncode(_program.DeptCode)
        Me.CompanyNameTextBox.Text = Server.HtmlEncode(_program.CompanyName)
        Me.NearEndUsageTextBox.Text = Server.HtmlEncode(_program.NearEndUsage)
        Me.AvgDailyUsageTextBox.Text = Server.HtmlEncode(_program.AvgDailyUsage)
        Me.WeeklyFlightDaysTextBox.Text = Server.HtmlEncode(_program.WeeklyFlightDays)
        Me.CloneProgDDL.SelectedValue = Server.HtmlEncode(0)
    End Sub

    Private Sub PopulateCloneProgDDL()
        Dim totRows As Integer = 0
        Me.CloneProgDDL.Items.Clear()
        Me.CloneProgDDL.Items.Add(New ListItem("< Do Not Clone >", 0))
        For Each thisProgram As Program In ProgramManager.GetList(1, 0, -1, -1, totRows)
            Me.CloneProgDDL.Items.Add(New ListItem(Server.HtmlEncode(thisProgram.ProgName), thisProgram.ProgID))
        Next
    End Sub

    Private Sub ProcessException(ByVal ex As Exception)
        Me.ErrorMessageLabel.Text = ex.Message
        Me.ErrorMessageLabel.Visible = True
        If _program.BrokenRules.Count <> 0 Then
            Me.BrokenRulesList.Visible = True
            For Each thisRule As BrokenRule In _program.BrokenRules
                Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
            Next
        End If
    End Sub

End Class