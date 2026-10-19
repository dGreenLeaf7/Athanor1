<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="export.aspx.vb" Inherits="FLTWebApp.export" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style=" text-align: center;">
        <table border="0" cellpadding="0" cellspacing="0" width="600px">
            <tr>
                <td>
                    <table cellpadding="4" cellspacing="0" width="100%" style="border:solid 1px black;">
                        <colgroup>
                          <col width="70%" />
                          <col width="30%" />
                        </colgroup>
                        <tr>
                            <th colspan="2"><h2>Export File Generation and Download</h2></th>
                        </tr>
                        <tr>
                            <td colspan="2"><asp:Image ID="BarAnimated" runat="server" ImageUrl="~/images/bar180.gif" style="visibility:hidden;" /></td>
                        </tr>
                        <tr>
                            <td colspan="2"><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2"><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2"><asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false"></asp:BulletedList></td>
                        </tr>
                        <tr>
                            <th style="text-align: left;" >
                                <asp:RadioButtonList ID="FilteringRBL" runat="server" AutoPostBack="true">
                                    <asp:ListItem Text="Only include Flight Data Recorded on or after this Date:" Selected="true"></asp:ListItem>
                                    <asp:ListItem Text="Include All Flight Data"></asp:ListItem>
                                </asp:RadioButtonList>
                            </th>
                            <td style="text-align: left; vertical-align: top;" ><asp:TextBox ID="ChangesSinceDateTB" runat="server" CssClass="datePickerTB" text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2"><asp:Button ID="CreateExportFileButton" runat="server" Text="Create Export File" Height="30px" /></td>
                        </tr>
                        <tr>
                            <td colspan="2"><asp:PlaceHolder ID="fileLinksPH" runat="server"></asp:PlaceHolder></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        $(function() {
            $(".datePickerTB").datepicker();
        });
    </script>
</asp:Content>
