<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OffTestFleet.aspx.vb" Inherits="FLTFleet.OffTestFleet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Off Test Fleet Page</title>
    <link href="/Css/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table id="MainHolder" style="width: 100%;" cellpadding="0" cellspacing="0">
        <tr>
            <td id="header">
                <asp:Label ID="TitleLB" runat="server" CssClass="title">Contoso Flight Log Tracking System v2.0</asp:Label>
            </td>
        </tr>
        <tr>
            <td id="programBar">
                <asp:Button ID="ShowOffTestBtn" runat="server" Text="Show ON TEST Fleet" AutoPostBack="true" OnClick="ShowOffTestBtn_Click" />
            </td>
        </tr>
    </table>
    <div>
    <asp:GridView 
        ID="FleetGridView" 
        runat="server" 
        CssClass="GridList" 
        AutoGenerateColumns="False" 
        DataKeyNames="AircraftId" 
        EmptyDataText="No Records Retrieved" 
        AllowPaging="False" 
        BorderColor="Black" 
        BorderStyle="Solid" 
        BorderWidth="1px" 
        Width="100%"
        PageSize="16"
        CellPadding="5" AllowSorting="True">
        <Columns>
            <asp:BoundField DataField="AircraftId" Visible="false" />
            <asp:BoundField DataField="AircraftStatusID" Visible="false" />
            <asp:BoundField DataField="LocationName"  HeaderText="Location" SortExpression="Location" >
                <HeaderStyle Width="70px" />
            </asp:BoundField>
            <asp:BoundField DataField="MSN" HeaderText="FRC #" SortExpression="MSN" >
                <HeaderStyle Width="120px" />
            </asp:BoundField>
            <asp:ButtonField DataTextField="StatusAsString" HeaderText="Status" CommandName="Status_Clicked"  >
                <HeaderStyle Width="80px" />
            </asp:ButtonField>
            <asp:BoundField DataField="EngineerComment" HeaderText="Hangar Comments"  />
            <asp:BoundField DataField="HangarComment" HeaderText = "Contoso Comments"  />
            <asp:BoundField DataField="DateModifiedInPT" HeaderText="Last Update (PT)" SortExpression="Date" >
                <HeaderStyle Width="130px" />
            </asp:BoundField>
        </Columns>
        <AlternatingRowStyle BackColor="Gainsboro" />
    </asp:GridView>
    <asp:HiddenField ID="SortByHiddenField" runat="server" />
    </div>
    <table  style="width: 100%;" cellpadding="0" cellspacing="0">    
        <tr>
            <td id="footer" style="width: 100%">
                <div style="width:auto;">
                    <div style="width: 600px; float: left;">&nbsp;&nbsp;
                    User:&nbsp;<asp:Label ID="UserNameLabel" runat="server"></asp:Label>&nbsp;&nbsp;
                    Last Logged In:&nbsp;<asp:Label ID="LastLoggedInLabel" runat="server"></asp:Label></div>
                    <div style="float: right; text-align: right; padding: 0px 10px">
                        <asp:LinkButton ID="LogOutButton" runat="server" Text="Logout" style="color: white;" OnClick="LogOutButton_Click" />
                    </div>
                </div>
            </td>
        </tr>
    </table>

    </form>
</body>
</html>
