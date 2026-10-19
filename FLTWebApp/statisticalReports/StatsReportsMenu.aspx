<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="StatsReportsMenu.aspx.vb" Inherits="FLTWebApp.AircraftReportsMenu2" 
    title="Report Menu" %>
<asp:Content ID="AircraftReportMenuContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Statistical Reports Menu</h2>
    <ul class="reportMenuList">
        <li><a href="./FlightStatisticsParameters2.aspx">Statistics for All Flights</a></li>
        <li><a href="CompletedFlightStatisticsParameters2.aspx">Statistics for Completed Flights</a></li>
        <li><a href="./FlightOutliersParameters2.aspx">Statistical Outliers for All Flights</a></li>
    </ul>
</asp:Content>
