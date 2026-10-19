<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="roles.aspx.vb" Inherits="FLTWebApp.roles" 
    title="Untitled Page" %>
<asp:Content ID="RolesContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Roles</h2>
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false">
    </asp:BulletedList>
    <asp:MultiView ID="RolesMultiView" runat="server" ActiveViewIndex="0">
        <asp:View ID="RolesListView" runat="server">
            <table class="ListFilterTable" cellspacing="0" cellpadding="5" style="border: solid 1px black;">
                <tr>
                    <td>Program</td>
                    <td>Is Active</td>
                    <td>Is Deleted</td>
                    <td rowspan="2">
                        <asp:ImageButton ID="AddButton" runat="server" AccessKey="A" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add new entry" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList 
                            ID="ProgramDDL" 
                            runat="server" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="IsActiveDDL" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="1" Text="Active Only"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Inactive Only"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="All"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="IsDeletedDDL" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="0" Text="No Deleted"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Only Deleted"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="All"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div class="VerticalSpacer">&nbsp;</div>
            <asp:GridView 
                ID="RolesGridView" 
                runat="server" 
                CssClass="GridList" 
                AutoGenerateColumns="False" 
                DataKeyNames="RoleId,ProgId,ConcurrencyId" 
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
                            <asp:ImageButton ID="SelectButton" runat="server" CommandName="Select" ImageUrl="~/images/table.bmp" ToolTip="Manage users in this role" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <EditItemTemplate>
                            <asp:Label id="RoleIdLB" runat="server" text='<%#: Bind("RoleId") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label id="RoleIdLB" runat="server" text='<%#: Eval("RoleId")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" SortExpression="RoleName" >
                        <EditItemTemplate>
                            <asp:TextBox ID="RoleNameTB" runat="server" Text='<%#: Bind("RoleName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="RoleNameLB" runat="server" Text='<%#: Eval("RoleName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" SortExpression="RoleDescription" >
                        <EditItemTemplate>
                            <asp:TextBox ID="RoleDescriptionTB" runat="server" Text='<%#: Bind("RoleDescription") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="RoleDescriptionLB" runat="server" Text='<%#: Eval("RoleDescription")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Prog Mgr" SortExpression="IsProgramManager" >
                        <ItemStyle HorizontalAlign="Center" />
                        <EditItemTemplate>
                            <asp:CheckBox ID="IsProgramManagerCB" runat="server" Checked='<%# Bind("IsProgramManager") %>' Enabled="true" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="IsProgramManagerCB" runat="server" Checked='<%# Bind("IsProgramManager") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Active" SortExpression="IsActive" >
                        <ItemStyle HorizontalAlign="Center" />
                        <EditItemTemplate>
                            <asp:CheckBox ID="IsActiveCB" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="true" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="IsActiveCB" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Deleted" SortExpression="IsDeleted" >
                        <ItemStyle HorizontalAlign="Center" />
                        <EditItemTemplate>
                            <asp:CheckBox ID="IsDeletedCB" runat="server" Checked='<%# Bind("IsDeleted") %>' Enabled="true" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="IsDeletedCB" runat="server" Checked='<%# Bind("IsDeleted") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="DeleteButton" runat="server" CommandName="Delete" ImageUrl="~/images/x_red.bmp" ToolTip="Delete this entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="Gainsboro" />
            </asp:GridView>
        </asp:View>
        <asp:View ID="RolesModifyView" runat="server">
            <h3><asp:Label ID="AddEditTitleLabel" runat="server"></asp:Label></h3>
            <table class="GridList" style="border: solid 1px black" cellpadding="5" cellspacing="0">
                <tr runat="server">
                    <th runat="server">Name</th>
                    <td runat="server"><asp:TextBox ID="RoleNameTextBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Description</th>
                    <td runat="server"><asp:TextBox ID="RoleDescriptionTextBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Is Program Manager</th>
                    <td runat="server"><asp:CheckBox ID="IsProgramManagerCheckBox" runat="server"></asp:CheckBox></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Is Active</th>
                    <td runat="server"><asp:CheckBox ID="IsActiveCheckBox" runat="server"></asp:CheckBox></td>
                </tr>
                <tr runat="server">
                    <td colspan="2" style="text-align:center;" runat="server">
                        <asp:Button ID="SaveButton" runat="server" text="Save" AccessKey="S" Width="75px" Height="30px" />
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" AccessKey="C"  Width="75px" Height="30px" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="UsersInRoleView" runat="server">
            <h3>Assign Users to Role</h3>
            <div style="height: 20px;">&nbsp;</div>
            <table cellpadding="5" cellspacing="0" border="solid 1px black">
                <tr>
                    <th>Program</th>
                    <th>Role</th>
                    <th rowspan="2"><asp:Button ID="DoneButton" runat="server" text="Done" AccessKey="D" /></th>
                </tr>
                <tr>
                    <td><asp:Label ID="ProgNameLabel" runat="server"></asp:Label></td>
                    <td><asp:Label ID="RoleNameLabel" runat="server"></asp:Label></td>
                </tr>
            </table>
            <h3>Users in Role</h3>
            <asp:GridView 
                ID="UsersInRoleGridView" 
                runat="server" 
                AutoGenerateColumns="False" 
                DataKeyNames="UserName" 
                EmptyDataText="No Users are in this Role" 
                BorderColor="Black" 
                BorderStyle="Solid" 
                BorderWidth="1px">
                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True" SortExpression="UserName" />
                    <asp:BoundField DataField="DisplayName" HeaderText="Display Name" SortExpression="DisplayName" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="RemoveButton" runat="server" CommandName="Select" ImageUrl="~/images/minus_red.bmp" ToolTip="Remove this user from role" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="Gainsboro" />
            </asp:GridView>
            <div style="height: 20px;">&nbsp;</div>
            <h3>Users Not in Role</h3>
            <asp:GridView 
                ID="UsersNotInRoleGridView" 
                runat="server" 
                AutoGenerateColumns="False" 
                DataKeyNames="UserName" 
                BorderColor="Black" 
                BorderStyle="Solid" 
                EmptyDataText="No more roles are available for assignment"
                BorderWidth="1px">
                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True" SortExpression="UserName" />
                    <asp:BoundField DataField="DisplayName" HeaderText="Display Name" SortExpression="DisplayName" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="AssignButton" runat="server" CommandName="Select" ImageUrl="~/images/plus_green.bmp" ToolTip="Assign this user to role" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="Gainsboro" />
            </asp:GridView>
            <asp:HiddenField ID="RoleIdHiddenField" runat="server" />
        </asp:View>
    </asp:MultiView>
    <div style="height: 20px;">&nbsp;</div>
</asp:Content>
