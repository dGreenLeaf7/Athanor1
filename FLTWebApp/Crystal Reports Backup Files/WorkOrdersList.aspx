<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="WorkOrdersList.aspx.vb" Inherits="FLTWebApp.WorkOrdersList" 
    title="Work Orders List" %>
<asp:Content ID="WorkOrderListContent" ContentPlaceHolderID="MainContent" runat="server">
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
            <td>Aircraft</td>
            <td>Location</td>
            <td>Is Active</td>
            <td>Is Deleted</td>
            <td rowspan="2">
                <asp:ImageButton ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox id="AircraftFilterTB" runat="server" AutoPostBack="True"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="LocationFilterDDL" runat="server" AutoPostBack="True"></asp:DropDownList>
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
        ID="WorkOrdersGV" 
        runat="server" 
        CssClass="GridList" 
        AutoGenerateColumns="False" 
        DataKeyNames="WorkOrderId"
        EmptyDataText="No Records Retrieved" 
        AllowPaging="True" 
        BorderColor="Black" 
        BorderStyle="Solid" 
        BorderWidth="1px" 
        CellPadding="5">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="SelectButton" runat="server" ImageUrl="~/images/pencil_yellow.bmp" CommandName="Select" ToolTip="Edit this entry" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="WorkOrderId" HeaderText="WorkOrderId" InsertVisible="False"
                ReadOnly="True" SortExpression="WorkOrderId" Visible="False" />
            <asp:BoundField DataField="WorkOrderNbr" HeaderText="Work Order Nbr" SortExpression="WorkOrderNbr" />
            <asp:BoundField DataField="MSN" HeaderText="MSN" SortExpression="MSN" />
            <asp:BoundField DataField="RegistrationNbr" HeaderText="Tail" SortExpression="Tail" />
            <asp:BoundField DataField="RequestDate" HeaderText="Req. Date" SortExpression="RequestDate" />
            <asp:BoundField DataField="WorkOrderStatus" HeaderText="Status" SortExpression="WorkOrderStatus" />
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" SortExpression="IsDeleted" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="DeleteButton" runat="server" ImageUrl="~/images/x_red.bmp" CommandName="Delete" ToolTip="Delete this entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:HiddenField ID="ProgIdHiddenField" runat="server" />
</asp:Content>
