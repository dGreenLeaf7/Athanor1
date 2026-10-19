' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports System.Configuration

Partial Public Class FLT
    Inherits System.Web.UI.MasterPage
    Private _fltIdentity As FltIdentity = Nothing

    Protected ReadOnly Property ProgId() As Long
        Get
            Return Me.UserProgramsDropDownList.SelectedValue
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        Me.Page.Title = System.Configuration.ConfigurationManager.AppSettings("AppTitle")
        Me.TitleLB.Text = System.Configuration.ConfigurationManager.AppSettings("AppTitle")
        Me.UserNameLabel.Text = _fltIdentity.UserDisplayName
        Me.LastLoggedInLabel.Text = _fltIdentity.LastLoginDate
        If Not Me.IsPostBack Then
            BuildUserProgramList(_fltIdentity)
            BuildMenu(_fltIdentity)
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Session("fltIdentity") = _fltIdentity
    End Sub

    Private Sub BuildUserProgramList(ByVal FltIdentity As FltIdentity)
        Me.UserProgramsDropDownList.Items.Clear()
        Dim thisListItem As ListItem
        For Each thisUserProgram As FltIdentity.UserProgram In FltIdentity.UserProgramsList
            thisListItem = New ListItem(thisUserProgram.ProgName, thisUserProgram.ProgId)
            If thisUserProgram.ProgId = FltIdentity.CurrentProgId Then
                thisListItem.Selected = "True"
            End If
            Me.UserProgramsDropDownList.Items.Add(thisListItem)
        Next
    End Sub

    Private Sub BuildMenu(ByVal FltIdentity As FltIdentity)

        Dim homeMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Home", "/default.aspx")
        Me.menu1.Items.Add(homeMenuItem)

        If FltIdentityManager.AuthorizeUserAction(_fltIdentity, "Fleet", fltUserAction.CanView) Then
            Dim fleetMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Fleet Module")
            Me.menu1.Items.Add(fleetMenuItem)
            Dim fleetSubMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Fleet", "/fleet/FleetList.aspx")
            fleetMenuItem.SubItems.Add(fleetSubMenuItem)
            Dim flightsSubMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Flights", "/fleet/FlightsList.aspx")
            fleetMenuItem.SubItems.Add(flightsSubMenuItem)
            Dim workordersSubMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Work Orders", "/fleet/WorkOrdersList.aspx")
            fleetMenuItem.SubItems.Add(workordersSubMenuItem)
        End If


        If FltIdentityManager.AuthorizeUserAction(_fltIdentity, "Documents", fltUserAction.CanView) Then
            Dim documentsMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Documents", "/documents/DocumentsList.aspx")
            Me.menu1.Items.Add(documentsMenuItem)
        End If

        If FltIdentityManager.AuthorizeUserAction(_fltIdentity, "Export", fltUserAction.CanView) Then
            Dim exportMenuItem As New skmMenu.MenuItem("Export Data", "/export/export.aspx")
            Me.menu1.Items.Add(exportMenuItem)
        End If

        Dim userProfileMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("User Profile")
        Me.menu1.Items.Add(userProfileMenuItem)
        Dim editProfileMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Edit Profile", "/userProfile/UserProfile.aspx")
        userProfileMenuItem.SubItems.Add(editProfileMenuItem)
        Dim changePasswordMenuItem As New skmMenu.MenuItem("Change Password", "/userProfile/ChangePassword.aspx")
        userProfileMenuItem.SubItems.Add(changePasswordMenuItem)

        If FltIdentityManager.AuthorizeUserAction(_fltIdentity, "Program", fltUserAction.CanView) Then
            Dim programMenuItem As New skmMenu.MenuItem("Program")
            menu1.Items.Add(programMenuItem)
            Dim programProfileMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Program Profile", "/program/ProgramProfile.aspx")
            programMenuItem.SubItems.Add(programProfileMenuItem)
            Dim programConfigMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Program Configuration")
            programMenuItem.SubItems.Add(programConfigMenuItem)
            Dim locationsListMenuItem As New skmMenu.MenuItem("Locations", "/program/locations.aspx")
            programConfigMenuItem.SubItems.Add(locationsListMenuItem)
            Dim regionsListMenuItem As New skmMenu.MenuItem("Regions", "/program/regions.aspx")
            programConfigMenuItem.SubItems.Add(regionsListMenuItem)
            Dim routesListMenuItem As New skmMenu.MenuItem("Routes", "/program/routes.aspx")
            programConfigMenuItem.SubItems.Add(routesListMenuItem)
            Dim weightsListMenuItem As New skmMenu.MenuItem("Weight Tracking", "/program/weightDefs.aspx")
            programConfigMenuItem.SubItems.Add(weightsListMenuItem)
            Dim configValuesListMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Configuration Values", "/program/ConfigValues.aspx")
            programConfigMenuItem.SubItems.Add(configValuesListMenuItem)
        End If

        If FltIdentityManager.AuthorizeUserAction(_fltIdentity, "Security", fltUserAction.CanView) Then
            Dim securityMenuItem As New skmMenu.MenuItem("Security")
            menu1.Items.Add(securityMenuItem)
            Dim modulesMenuItem As New skmMenu.MenuItem("Modules", "/security/modules.aspx")
            securityMenuItem.SubItems.Add(modulesMenuItem)
            Dim rolesMenuItem As New skmMenu.MenuItem("Roles", "/security/roles.aspx")
            securityMenuItem.SubItems.Add(rolesMenuItem)
            Dim roleModulePermissionsMenuItem As New skmMenu.MenuItem("Role Module Permissions", "/security/roleModulePermissions.aspx")
            securityMenuItem.SubItems.Add(roleModulePermissionsMenuItem)
            Dim usersMenuItem As New skmMenu.MenuItem("Users", "/security/users.aspx")
            securityMenuItem.SubItems.Add(usersMenuItem)
        End If

        If FltIdentityManager.AuthorizeUserAction(_fltIdentity, "AppConfig", fltUserAction.CanView) Then
            Dim appConfigMenuItem As New skmMenu.MenuItem("App Config")
            menu1.Items.Add(appConfigMenuItem)
            Dim programListMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Programs", "/appConfiguration/Programs.aspx")
            appConfigMenuItem.SubItems.Add(programListMenuItem)
            Dim timeZoneListMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Time Zones", "/appConfiguration/TimeZones.aspx")
            appConfigMenuItem.SubItems.Add(timeZoneListMenuItem)
            Dim docTypesListMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Doc Types", "/appConfiguration/DocumentTypes.aspx")
            appConfigMenuItem.SubItems.Add(docTypesListMenuItem)
            Dim weightDefTypesListMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Weight Types", "/appConfiguration/WeightDefTypes.aspx")
            appConfigMenuItem.SubItems.Add(weightDefTypesListMenuItem)
        End If

        If FltIdentityManager.AuthorizeUserAction(_fltIdentity, "Program", fltUserAction.CanView) Then

            Dim reportsMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Program Reports")
            Me.menu1.Items.Add(reportsMenuItem)

            Dim rptFleetMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Fleet", "/reports/rptFleet.aspx")
            reportsMenuItem.SubItems.Add(rptFleetMenuItem)

            Dim rptLocationsMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Locations", "/reports/rptLocations.aspx")
            reportsMenuItem.SubItems.Add(rptLocationsMenuItem)

            Dim rptRegionsMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Regions", "/reports/rptRegions.aspx")
            reportsMenuItem.SubItems.Add(rptRegionsMenuItem)

            Dim rptRoutesMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Routes", "/reports/rptRoutes.aspx")
            reportsMenuItem.SubItems.Add(rptRoutesMenuItem)

            Dim rptWeightTrackingMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Weight Tracking", "/reports/rptWeightTracking.aspx")
            reportsMenuItem.SubItems.Add(rptWeightTrackingMenuItem)

            Dim rptConfigValuesMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Config Values", "/reports/rptConfigValues.aspx")
            reportsMenuItem.SubItems.Add(rptConfigValuesMenuItem)

        End If

        Dim logoutMenuItem As skmMenu.MenuItem = New skmMenu.MenuItem("Log Out", "/logout.aspx")
        menu1.Items.Add(logoutMenuItem)

        menu1.CssClass = "menustyle"
        menu1.HighlightTopMenu = True
        menu1.zIndex = 100
        menu1.Cursor = skmMenu.MouseCursor.Pointer

    End Sub

    Protected Sub UserProgramsDropDownList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserProgramsDropDownList.SelectedIndexChanged
        FltIdentityManager.ChangeCurrentProgram(_fltIdentity, Me.UserProgramsDropDownList.SelectedValue)
        Response.Redirect("~/Default.aspx")
    End Sub

End Class