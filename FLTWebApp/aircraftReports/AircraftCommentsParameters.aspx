<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="AircraftCommentsParameters.aspx.vb" Inherits="FLTWebApp.AircraftCommentsParameters" 
    title="Aircraft Comments Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:MultiView ID="ReportMultiView" runat="server" ActiveViewIndex="0">
        <asp:View ID="ParametersView" runat="server">
    <h2>Aircraft Comments Report</h2>
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
            <th>Component:</th>
            <td>
                <asp:DropDownList ID="ComponentDDL" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>Rating From:</th>
            <td>
                <asp:DropDownList ID="RatingFromDDL" runat="server">
                    <asp:ListItem Selected="true">1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>Rating Thru:</th>
            <td>
                <asp:DropDownList ID="RatingThruDDL" runat="server">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                    <asp:ListItem Selected="true">10</asp:ListItem>                
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
            <th>Sort Order:</th>
            <td>
                <asp:DropDownList ID="SortOrderDDL" runat="server">
                    <asp:ListItem Value="4" Selected="True">Sorted by Date and MSN</asp:ListItem>
                    <asp:ListItem Value="5">Sorted by MSN and Date</asp:ListItem>
                </asp:DropDownList>
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
                        <td style="background-color: White; text-align: left;">
                            &nbsp;
                            <CR:CrystalReportViewer 
                                ID="myCrystalReportViewer" 
                                runat="server" 
                                DisplayGroupTree="false"
                                HasCrystalLogo="False"
                                PrintMode="PDF"
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