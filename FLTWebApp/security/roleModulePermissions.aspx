<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="roleModulePermissions.aspx.vb" Inherits="FLTWebApp.roleModulePermissions" 
    title="Role Module Permissions" %>
<asp:Content ID="RoleModulePermissionsContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Role Module Permissions</h2>
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false">
    </asp:BulletedList>
    <asp:MultiView ID="RoleModulePermissionsMultiView" runat="server" ActiveViewIndex="0">
        <asp:View ID="RoleModulePermissionsListView" runat="server">
            <table class="ListFilterTable" cellpadding="5" cellspacing="0" >
                <tr>
                    <th>Program</th>
                    <th>Role</th>
                    <th>Is Active</th>
                    <th>Is Deleted</th>
                    <th rowspan="2">
                        <asp:ImageButton ID="AddButton" runat="server" AccessKey="A" imageurl="~/images/plus_blue.bmp" ToolTip="Add new entry" />
                    </th>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ProgramDDL" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="RoleDDL" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="IsActiveDropDownList" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="1">Active Only</asp:ListItem>
                            <asp:ListItem Value="0">Inactive Only</asp:ListItem>
                            <asp:ListItem Value="-1">All</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="IsDeletedDropDownList" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="0">Not Deleted</asp:ListItem>
                            <asp:ListItem Value="1">Deleted</asp:ListItem>
                            <asp:ListItem Value="-1">All</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div class="VerticalSpacer">&nbsp;</div>
            <asp:GridView 
                ID="RoleModulePermissionsGridView" 
                runat="server" 
                CssClass="GridList" 
                AutoGenerateColumns="False" 
                DataKeyNames="RoleId,ModuleName" 
                BorderColor="Black" 
                BorderStyle="Solid" 
                BorderWidth="1px" 
                AllowPaging="True">
                <Columns>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="SaveButton" runat="server" CommandName="Update" ImageUrl="~/images/save.bmp" ToolTip="Save pending changes" />
                            <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" ImageUrl="~/images/cancel.bmp" ToolTip="Cancel pending changes" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit this entry" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="RoleIdLB" runat="server" Text='<%#: Bind("RoleId") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="RoleIdLB" runat="server" Text='<%#: Eval("RoleId")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Module Name" SortExpression="ModuleName" >
                        <EditItemTemplate>
                            <asp:Label ID="ModuleNameLB" runat="server" Text='<%#: Bind("ModuleName") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="ModuleNameLB" runat="server" Text='<%#: Eval("ModuleName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Can View" SortExpression="CanView">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CanViewCB" runat="server" Checked='<%# Bind("CanView") %>' Enabled="True" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CanViewCB" runat="server" Checked='<%# Bind("CanView") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Can Edit" SortExpression="CanEdit">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CanEditCB" runat="server" Checked='<%# Bind("CanEdit") %>' Enabled="True" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CanEditCB" runat="server" Checked='<%# Bind("CanEdit") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Active" SortExpression="CanView">
                        <EditItemTemplate>
                            <asp:CheckBox ID="IsActiveCB" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="True" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="IsActiveCB" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Deleted" SortExpression="CanEdit">
                        <EditItemTemplate>
                            <asp:CheckBox ID="IsDeleted" runat="server" Checked='<%# Bind("IsDeleted") %>' Enabled="false" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="IsDeleted" runat="server" Checked='<%# Bind("IsDeleted") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="Deletebutton" runat="server" CommandName="Delete" ImageUrl="~/images/x_red.bmp" ToolTip="Delete this entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="Gainsboro" />
            </asp:GridView>
        </asp:View>
        <asp:View ID="RoleModulePermissionsModifyView" runat="server">
            <h3><asp:Label ID="AddEditTitleLabel" runat="server"></asp:Label></h3>
            <table class="GridList" style="border: solid 1px black;" cellpadding="5" cellspacing="0">
                <tr runat="server">
                    <th runat="server">Role</th>
                    <td runat="server"><asp:Label ID="RoleLabel" runat="server"></asp:Label></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Module</th>
                    <td runat="server"><asp:DropDownList ID="ModuleDropDownlist" runat="server"></asp:DropDownList></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Can View</th>
                    <td runat="server"><asp:CheckBox ID="CanViewCheckBox" runat="server" /></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Can Edit</th>
                    <td runat="server"><asp:CheckBox ID="CanEditCheckBox" runat="server" /></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Is Active</th>
                    <td runat="server"><asp:CheckBox ID="IsActiveCheckBox" runat="server" /></td>
                </tr>
                <tr runat="server">
                    <td style="text-align: center;" colspan="2">
                        <asp:Button ID="SaveButton" runat="server" Text="Save" AccessKey="S" width="75px" Height="30px" />
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" AccessKey="C" width="75px" Height="30px" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
