<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="FleetList.aspx.vb" Inherits="FLTWebApp.Fleet" 
    title="Fleet List" %>
<asp:Content ID="AircraftListContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Fleet</h2>
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
            <th>Aircraft</th>
            <th>Location</th>
            <th>Is Active</th>
            <th>Is Deleted</th>
            <th rowspan="2" style="width: 45px">
                <asp:ImageButton ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add New Entry" style="margin: auto;" />
            </th>
        </tr>
        <tr>
            <td>
                <asp:TextBox id="AircraftFilterTextBox" runat="server" AutoPostBack="True" Text=""></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="LocationFilterDropdownList" runat="server" AutoPostBack="True">
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
        ID="FleetGridView" 
        runat="server" 
        CssClass="GridList" 
        AutoGenerateColumns="False" 
        DataKeyNames="AircraftId" 
        EmptyDataText="No Records Retrieved" 
        AllowPaging="True" 
        BorderColor="Black" 
        BorderStyle="Solid" 
        BorderWidth="1px" 
        CellPadding="5">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="SelectButton" runat="server" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit Aircraft" CommandName="Select" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="MSN" HeaderText="MSN" SortExpression="MSN" />
            <asp:BoundField DataField="ModelName" HeaderText="Model" SortExpression="ModelName" />
            <asp:BoundField DataField="CurrentLocationName" HeaderText="Current Location" SortExpression="CurrentLocationName" />
            <asp:BoundField DataField="AirframeHoursCurrent" HeaderText="Current Hours" SortExpression="AirframeHoursCurrent" />
            <asp:BoundField DataField="CurrentUsage" HeaderText="Curr Hours" SortExpression="TotalRequiredHours" />
            <asp:BoundField DataField="TotalRequiredUsage" HeaderText = "Tot Req'd Hours" SortExpression="TotalRequiredUsage" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="DeleteButton" runat="server" ImageUrl="~/images/x_red.bmp" ToolTip="Delete Aircraft" CommandName="Delete" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle BackColor="Gainsboro" />
    </asp:GridView>
    <asp:HiddenField ID="ProgIdHiddenField" runat="server" />
    
</asp:Content>
