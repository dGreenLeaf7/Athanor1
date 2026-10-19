<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EditAircraftStatus.aspx.vb" Inherits="FLTFleet.EditAircraftStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Change an Aircraft Status</title>
    <link href="/Css/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left: 20px;">
        <h2 id="HeaderLabel" runat="server">Change Aircraft Status</h2>
    <asp:Table ID="EditTable" runat="server" GridLines="both" 
        CellPadding="5" CellSpacing="0" BorderColor="Black" Font-Names="Verdana, Arial">
        <asp:TableRow>
            <asp:TableHeaderCell HorizontalAlign="Right">FRC #</asp:TableHeaderCell>
            <asp:TableCell><asp:Label ID="MSNLabel" runat="server"></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell HorizontalAlign="Right">Status</asp:TableHeaderCell>
            <asp:TableCell><asp:DropDownList ID="StatusDropDown" runat="server"></asp:DropDownList></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:tableheadercell HorizontalAlign="Right">Hangar Comment</asp:tableheadercell>
            <asp:TableCell><asp:TextBox ID="EngineerCommentTextBox" MaxLength="100"  Width="300" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:tableheadercell HorizontalAlign="Right">Contoso Comment</asp:tableheadercell>
            <asp:TableCell><asp:TextBox ID="HangarCommentTextBox" MaxLength="100" Width="300" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableFooterRow>
            <asp:TableCell HorizontalAlign="center" ColumnSpan="2">
                <asp:Button ID="SaveButton" runat="server" Text="Save" AccessKey="S" OnClick="SaveButton_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="CancelButton" runat="server" Text="Cancel" AccessKey="C" OnClick="CancelButton_Click" />
            </asp:TableCell>
        </asp:TableFooterRow>
    </asp:Table>
    <br />
    <asp:Label ID="lblWarning" runat="server" style="color: Red;" Width="400px" />
    </div>
    <asp:HiddenField ID="hidAircraftId" runat="server" />
    <asp:HiddenField ID="hidChangeType" runat="server" />
    </form>
</body>
</html>
