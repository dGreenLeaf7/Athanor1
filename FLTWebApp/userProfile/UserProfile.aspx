<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="UserProfile.aspx.vb" Inherits="FLTWebApp.UserProfile" 
    title="User Profile" ViewStateEncryptionMode="Always" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>User Profile</h2>
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:Table ID="EditTable"  runat="server" GridLines="both" CellPadding="5" CellSpacing="0" BorderColor="Black" Font-Names="Verdana, Arial">
        <asp:TableRow>
            <asp:TableHeaderCell>User Name</asp:TableHeaderCell>
            <asp:TableCell><asp:Label ID="UserNameLabel" runat="server"></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Display Name</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="DisplayNameTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:tableheadercell>Email</asp:tableheadercell>
            <asp:TableCell><asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Location</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="LocationTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Country Code</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="CountryCodeTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Phone Number</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="PhoneNumberTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Manager</asp:TableHeaderCell>
            <asp:TableCell><asp:DropDownList ID="ManagerDropDownList" runat="server"></asp:DropDownList></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Department</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="DepartmentTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Title</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="TitleTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Company</asp:TableHeaderCell>
            <asp:TableCell><asp:TextBox ID="CompanyTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>Default Program</asp:TableHeaderCell>
            <asp:TableCell><asp:DropDownList ID="DefaultProgramDropDownList" runat="server"></asp:DropDownList></asp:TableCell>
        </asp:TableRow>
        <asp:TableFooterRow>
            <asp:TableCell HorizontalAlign="center" ColumnSpan="2"><asp:Button ID="SaveButton" runat="server" Text="Save" AccessKey="S" OnClick="SaveButton_Click" /></asp:TableCell>
        </asp:TableFooterRow>
    </asp:Table>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false">
    </asp:BulletedList>
</asp:Content>
