<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="PilotCommentsParameters2.aspx.vb" Inherits="FLTWebApp.PilotComments2Parameters" 
    title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <asp:MultiView ID="ReportMultiView" runat="server" ActiveViewIndex="0">
        <asp:View ID="ParametersView" runat="server">
            <h2>Pilot Comments Report</h2>
            <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
            <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
            <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false">
            </asp:BulletedList>    
            <table class="ListFilterTable" cellspacing="0" cellpadding="5">
                <tr>
                    <th>Location:</th>
                    <td>
                        <asp:DropDownList ID="LocationDDL" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>Aircraft:</th>
                    <td>
                        <asp:DropDownList ID="AircraftDDL" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>Date From:</th>
                    <td>
                        <asp:TextBox ID="DateFromTB" CssClass="datePickerTB" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Date Thru:</th>
                    <td>
                        <asp:TextBox ID="DateThruTB" CssClass="datePickerTB" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center;">
                        <asp:Button ID="ShowReportButton" runat="server" Text="Show Report" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                        <asp:Button ID="DownloadButton" runat="server" Text="Download Data" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ReportView" runat="server">
            <asp:Button ID="BackButton" runat="server" Text="&lt; - Back" />
                <table border="1" style="background-color: Silver;">
                    <tr>
                        <td>
                            <asp:Button ID="FirstNavButton" runat="server" Text="|&#x25c0;&nbsp;First"/>
                            <asp:Button ID="PrevNavButton" runat="server" Text="&#x25c0;&nbsp;Prev" />
                            <asp:Label ID="PageLabel" runat="server">&nbsp;No Records&nbsp;</asp:Label>
                            <asp:Button ID="NextNavButton" runat="server" Text="Next&nbsp;&#x25b6;"/>
                            <asp:Button ID="LastNavButton" runat="server" Text="Last&nbsp;&#x25b6;|" />
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: White; text-align: left;">
                            &nbsp;
                            <CR:CrystalReportViewer 
                                ID="theCrystalReportViewer" 
                                runat="server" 
                                HasCrystalLogo="False"
                                PrintMode="PDF"
                                HasPageNavigationButtons="False"
                                HasGotoPageButton="False"
                                AutoDataBind="true" />
                        </td>
                    </tr>
                </table>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="ProgIdHiddenField" runat="server" />
    <script type="text/javascript">
        $(function() {
            $(".datePickerTB").datepicker();
        });
    </script>
</asp:Content>
