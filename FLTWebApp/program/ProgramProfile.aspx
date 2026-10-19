<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="ProgramProfile.aspx.vb" Inherits="FLTWebApp.ProgramProfile" 
    title="Program Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Program Profile</h2>
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:Table ID="EditTable"  runat="server" GridLines="both" CellPadding="5" CellSpacing="0" BorderColor="Black" Font-Names="Verdana, Arial">
        <asp:TableRow>
            <asp:TableHeaderCell>Name</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="ProgNameTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Description</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="ProgDescriptionTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:tableheadercell>Version</asp:tableheadercell>
            <asp:TableCell><asp:TextBox ID="ProgVersionTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Program Logo</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="ProgramLogoTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Report Logo</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="ReportLogoTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Year Logo</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="YearLogoTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Program Status</asp:TableHeaderCell>
            <asp:TableCell><asp:DropDownList ID="ProgStatusDropDownList" runat="server"></asp:DropDownList></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Date Opened</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="DateOpenedTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Date Closed</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="DateClosedTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Actual Value</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="ActualValueTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Contoso Project Nbr</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="ProjectCodeTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Contoso Activity Nbr</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="ActivityCodeTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Contoso Department Nbr</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="DeptCodeTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Company Name</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="CompanyNameTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Near End Usage</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="NearEndUsageTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Avg Daily Usage</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="AvgDailyUsageTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Weekly Test Days</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="WeeklyFlightDaysTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableFooterRow>
            <asp:TableCell HorizontalAlign="center" ColumnSpan="2"><asp:Button ID="SaveButton" runat="server" Text="Save" AccessKey="S" OnClick="SaveButton_Click" Width="75px" Height="30px" /></asp:TableCell>
        </asp:TableFooterRow>
    </asp:Table>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false">
    </asp:BulletedList>
</asp:Content>
