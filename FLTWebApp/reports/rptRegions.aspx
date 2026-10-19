<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Title="Program Reports: Regions" Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" 
    CodeBehind="rptRegions.aspx.vb" Inherits="FLTWebApp.rptRegions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Program Reports: Regions</h2>
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false">
    </asp:BulletedList>    
    <table class="ListFilterTable" cellspacing="0" cellpadding="5">
        <tr>
            <td colspan="2" style="text-align:center;">
                <asp:Button ID="DownloadDataButton" runat="server" Text="Download Data" OnClick="DownloadDataButton_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="ProgIdHiddenField" runat="server" />
    <script type="text/javascript">
        $(function() {
            $(".datePickerTB").datepicker();
        });
    </script>
</asp:Content>
