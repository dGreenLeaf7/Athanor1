<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="userRoleAssignment.aspx.vb" Inherits="FLTWebApp.userRoleAssignment" 
    title="User Role Assignment" %>
<asp:Content ID="UserRoleAssignmentContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>User Role Assignments</h2>
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:MultiView ID="UserRoleAssignmentMultiView" runat="server" ActiveViewIndex="0">
        <asp:View ID="UserRoleAssignmentListView" runat="server">
            <asp:table ID="ListFilterTable" runat="server" GridLines="both">
                <asp:TableRow>
                    <asp:TableHeaderCell>Program</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Is Active</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Is Deleted</asp:TableHeaderCell>
                    <asp:TableHeaderCell RowSpan="2">
                        <asp:Button id="ListButton" runat="server" Text="List" AccessKey="L" />
                        <br />
                        <asp:Button ID="ResetButton" runat="server" Text="Reset" AccessKey="r" /> 
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell RowSpan="2">
                        <asp:Button ID="AddButton" runat="server" Text="Add" AccessKey="A" />
                    </asp:TableHeaderCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:DropDownList ID="ProgramDropDownlist" runat="server"></asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="IsActiveDropDownList" runat="server">
                            <asp:ListItem Selected="true" Value="1">Active Only</asp:ListItem>
                            <asp:ListItem Value="0">Inactive Only</asp:ListItem>
                            <asp:ListItem Value="-1">All</asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="IsDeletedDropDownList" runat="server">
                            <asp:ListItem Selected="true" Value="0">Not Deleted</asp:ListItem>
                            <asp:ListItem Value="1">Deleted</asp:ListItem>
                            <asp:ListItem Value="-1">All</asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:table>
        </asp:View>
        <asp:View ID="UserRoleAssignmentModifyView" runat="server">
        </asp:View>
    </asp:MultiView>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false">
    </asp:BulletedList>
</asp:Content>
