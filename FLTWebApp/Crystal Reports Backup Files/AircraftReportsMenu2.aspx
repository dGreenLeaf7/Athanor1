<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="AircraftReportsMenu2.aspx.vb" Inherits="FLTWebApp.ReportMenu2" 
    title="Report Menu" %>
<asp:Content ID="AircraftReportMenuContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Aircraft Reports Menu</h2>
    <ul class="reportMenuList">
        <li><a href="./PilotCommentsParameters2.aspx">Pilot Comments Report</a></li>
        <li><a href="./FuelBurnAndAccumHoursParameters2.aspx">Fuel Burn and Accumulated Hours Report</a></li>
        <li><a href="./AircraftCommentsParameters2.aspx">Aircraft Comments Report</a></li>
        <li><a href="./AircraftDataParameters2.aspx">Aircraft Data</a></li>
        <li><a href="./FlightDataParameters2.aspx">Flight Data</a></li>
    </ul>
</asp:Content>
