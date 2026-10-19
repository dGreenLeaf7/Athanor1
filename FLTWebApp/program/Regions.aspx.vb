' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO

Partial Public Class Regions
    Inherits System.Web.UI.Page

#Region "Private Variables"

    Private _fltIdentity As FltIdentity
    Private _Region As Region

#End Region

#Region "Protected Subs (Region List View)"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        Me.ProgIdHiddenField.Value = _fltIdentity.CurrentProgId
        ClearErrorFields()
        If Not Me.IsPostBack Then
            RegionsGridView_DataBind()
        Else
            _Region = CType(ViewState("_Region"), Region)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ViewState("_Region") = _Region
    End Sub

    Protected Sub RegionsGridView_DataBind()
        Dim totRows As Integer = 0
        RegionsGridView.DataSource = RegionManager.GetList(Me.ProgIdHiddenField.Value, Me.IsActiveDropDownList.SelectedValue, -1, -1, totRows, _fltIdentity)
        RegionsGridView.DataBind()
    End Sub

    Protected Sub RegionsGridView_PageIndexChanging(ByVal sender As System.Object, ByVal e As GridViewPageEventArgs) Handles RegionsGridView.PageIndexChanging
        RegionsGridView.PageIndex = e.NewPageIndex
        RegionsGridView_DataBind()
    End Sub

    Protected Sub RegionsGridView_RowEditing(ByVal sender As System.Object, ByVal e As GridViewEditEventArgs) Handles RegionsGridView.RowEditing
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.NewEditIndex)
            Dim id As Integer = CInt(CType(row.FindControl("IDLB"), Label).Text)
            _Region = RegionManager.GetItem(id, _fltIdentity)
            gv.EditIndex = e.NewEditIndex
            RegionsGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub RegionsGridView_RowCancelingEdit(ByVal sender As System.Object, ByVal e As GridViewCancelEditEventArgs) Handles RegionsGridView.RowCancelingEdit
        _Region = Nothing
        RegionsGridView.EditIndex = -1
        RegionsGridView_DataBind()
    End Sub

    Protected Sub RegionsGridView_RowUpdating(ByVal Sender As System.Object, ByVal e As GridViewUpdateEventArgs) Handles RegionsGridView.RowUpdating
        Try
            Dim gv As GridView = CType(Sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            Dim regionId As Integer = CInt(CType(row.FindControl("IDLB"), Label).Text)
            _Region = RegionManager.GetItem(regionId, _fltIdentity)
            With _Region
                .Code = Server.HtmlDecode(CType(row.FindControl("CodeTB"), TextBox).Text)
                .Name = Server.HtmlDecode(CType(row.FindControl("NameTB"), TextBox).Text)
                .IsActive = CType(row.FindControl("IsActiveCB"), CheckBox).Checked
            End With
            RegionManager.Update(_Region, _fltIdentity)
            gv.EditIndex = -1
            RegionsGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub RegionsGridView_RowDeleting(ByVal sender As System.Object, ByVal e As GridViewDeleteEventArgs) Handles RegionsGridView.RowDeleting
        Try
            Dim gv As GridView = CType(sender, GridView)
            Dim row As GridViewRow = gv.Rows(e.RowIndex)
            Dim routeId As Integer = CInt(CType(row.FindControl("IDLB"), Label).Text)
            _Region = RegionManager.GetItem(routeId, _fltIdentity)
            RegionManager.Delete(_Region, _fltIdentity)
            RegionsGridView_DataBind()

        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click
        _Region = New Region
        LoadDataToForm()
        Me.RegionMultiView.ActiveViewIndex = 1
    End Sub

    Protected Sub IsActiveDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsActiveDropDownList.SelectedIndexChanged
        RegionsGridView_DataBind()
    End Sub

#End Region

#Region "Protected Procedures (Region Modify View)"

    Protected Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        LoadDataToObject()
        Try
            RegionManager.Save(_Region, _fltIdentity)
            Me.RegionMultiView.ActiveViewIndex = 0
            RegionsGridView_DataBind()
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Me.RegionMultiView.ActiveViewIndex = 0
        RegionsGridView_DataBind()
    End Sub

#End Region

    Private Sub PopulateDropDownLists()

    End Sub

    Private Sub LoadDataToForm()
        PopulateDropDownLists()
        Me.RegionCodeTextBox.Text = Server.HtmlEncode(_Region.Code)
        Me.RegionNameTextBox.Text = Server.HtmlEncode(_Region.Name)
    End Sub

    Private Sub LoadDataToObject()
        _Region.ProgId = Me.ProgIdHiddenField.Value
        _Region.Code = Server.HtmlDecode(Me.RegionCodeTextBox.Text)
        _Region.Name = Server.HtmlDecode(Me.RegionNameTextBox.Text)
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
        If _Region.BrokenRules.Count <> 0 Then
            Me.BrokenRulesList.Visible = True
            For Each thisRule As BrokenRule In _Region.BrokenRules
                Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
            Next
        End If
    End Sub

End Class