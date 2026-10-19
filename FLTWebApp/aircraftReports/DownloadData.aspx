<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DownloadData.aspx.vb" Inherits="FLTWebApp.DownloadData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Download Data</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
        <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
        <asp:BulletedList 
            ID="ErrorsBulletList" 
            runat="server"
            DataValuefield="RuleIdentifier"
            DataTextField="Message"
            Visible="false">
        </asp:BulletedList>
        <div><asp:Label id="WarningMessageLabel" runat="server" style="color: #C35617; font-weight: bold;"></asp:Label></div>
        <asp:CheckBoxList 
            ID="WarningsCheckList" 
            runat="server" 
            DataValueField="RuleIdentifier"
            DataTextField="Message"
            Visible="false" 
            style="left: 1em; position: relative;" 
            CellPadding="5" 
            CellSpacing="0">
        </asp:CheckBoxList>    
    </div>
    </form>
</body>
</html>
