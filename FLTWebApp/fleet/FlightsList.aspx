<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="FlightsList.aspx.vb" Inherits="FLTWebApp.FlightsList" 
    title="Flights List Page" %>
<asp:Content ID="FlightsContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Flights List</h2>
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
    
    <table class="ListFilterTable" cellpadding="5" cellspacing="0">
        <tr>
            <th>Location</th>
            <th>Aircraft</th>
            <th>Date From</th>
            <th>Date Thru</th>
            <th>Is Approved</th>
            <th>Is Active</th>
            <th>Is Deleted</th>
            <th rowspan="2">
                <asp:ImageButton ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add New Entry" />
            </th>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="LocationFilterDropdownList" runat="server" AutoPostBack="True" ></asp:DropDownList>
            </td>
            <td>
                <asp:TextBox id="AircraftFilterTextBox" runat="server" AutoPostBack="True"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="FilterDateFromTB" runat="server" AutoPostBack="True" CssClass="datePickerTB"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="FilterDateThruTB" runat="server" AutoPostBack="True" CssClass="datePickerTB"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="IsApprovedDropDownList" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="IsActiveDropDownList" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                    <asp:ListItem Value="-1">All</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="IsDeletedDropDownList" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="-1">All</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div class="VerticalSpacer">&nbsp;</div>
    <asp:GridView 
        ID="FlightsGridView" 
        runat="server" 
        CssClass="GridList" 
        AutoGenerateColumns="False" 
        DataKeyNames="FlightId" 
        AllowPaging="True" 
        BorderColor="Black" 
        BorderStyle="Solid" 
        BorderWidth="1px" 
        CellPadding="5" 
        EmptyDataText="No information retrieved">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="EditFlightButton" runat="server" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit Flight" CommandName="Select" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FlightId" HeaderText="FlightId" InsertVisible="False" ReadOnly="True"
                SortExpression="FlightId" Visible="False" />
            <asp:BoundField DataField="LocationName" HeaderText="Location Name" SortExpression="LocationName" />
            <asp:BoundField DataField="FlightDateSort" HeaderText="Flight Date" SortExpression="FlightDateSort" />
            <asp:BoundField DataField="WorkShift" HeaderText="Work Shift" SortExpression="WorkShift" />
            <asp:BoundField DataField="MSN" HeaderText="MSN" SortExpression="MSN" />
            <asp:BoundField DataField="AirframeHoursStart" HeaderText="Start Hours" />
            <asp:BoundField DataField="AirframeHoursEnd" HeaderText="End Hours" />
            <asp:BoundField DataField="FlightUsage" HeaderText="Usage" SortExpression="Usage" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="DeleteFlightButton" runat="server" ImageUrl="~/images/x_red.bmp" ToolTip="Delete Flight" CommandName="Delete" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle BackColor="Gainsboro" />
    </asp:GridView>
    <asp:HiddenField ID="ProgIdHiddenField" runat="server" />
    <script type="text/javascript">
        $(function() {
            $(".datePickerTB").datepicker();
        });
    </script>
</asp:Content>
