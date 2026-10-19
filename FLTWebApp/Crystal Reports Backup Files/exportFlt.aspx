<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="exportFlt.aspx.vb" Inherits="FLTWebApp.exportFlt" 
    title="Export XML File" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style=" text-align: center;">
        <table border="0" cellpadding="0" cellspacing="0" width="600px">
            <tr>
                <td>
                    <table cellpadding="4" cellspacing="0" width="100%" style="border:solid 1px black;">
                        <tr>
                            <th><h2>Export File Generation and Download</h2></th>
                        </tr>
                        <tr>
                            <td><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false"></asp:BulletedList></td>
                        </tr>
                        <tr>
                            <td><asp:Button ID="CreateExportFileButton" runat="server" Text="Create Export File" Height="30px" /></td>
                        </tr>
                        <tr>
                            <td><asp:PlaceHolder ID="fileLinksPH" runat="server"></asp:PlaceHolder></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
