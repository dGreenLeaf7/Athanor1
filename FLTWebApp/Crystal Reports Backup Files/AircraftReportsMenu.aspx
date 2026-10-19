<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="AircraftReportsMenu.aspx.vb" Inherits="FLTWebApp.ReportMenu" 
    title="Report Menu" %>
<asp:Content ID="AircraftReportMenuContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Aircraft Reports Menu</h2>
    <ul class="reportMenuList">
        <li><a href="./DailyUsageParameters.aspx">Daily Usage Report</a></li>
        <li><a href="./PilotCommentsParameters.aspx">Pilot Comments Report</a></li>
        <li><a href="./FuelBurnAndAccumHoursParameters.aspx">Fuel Burn and Accumulated Hours Report</a></li>
        <li><a href="./AircraftCommentsParameters.aspx">Aircraft Comments Report</a></li>
        <li><a href="./AircraftDataParameters.aspx">Aircraft Data</a></li>
        <li><a href="./FlightDataParameters.aspx">Flight Data</a></li>
        <li><a href="./FuelBurnByRefillParameters.aspx">Daily Flights / Fuel Burn Report</a></li>
    </ul>
</asp:Content>
