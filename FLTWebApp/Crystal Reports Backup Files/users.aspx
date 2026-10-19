<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="users.aspx.vb" Inherits="FLTWebApp.users" 
    title="Untitled Page" %>
<asp:Content ID="UsersContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Users</h2>
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false">    </asp:BulletedList>
    <asp:MultiView ID="UsersMultiView" runat="server" ActiveViewIndex="0">
        <asp:View ID="UsersListView" runat="server">
            <table class="ListFilterTable" cellspacing="0" cellpadding="5" style="border: solid 1px black;">
                <tr>
                    <td>Is Active</td>
                    <td>Is Deleted</td>
                    <td rowspan="2">
                        <asp:ImageButton ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" AccessKey="A"/><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="IsActiveDropDownList" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="1" Text="Active Only"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Inactive Only"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="All"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="IsDeletedDropDownList" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="0" Text="No Deleted"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Only Deleted"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="All"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div class="VerticalSpacer">&nbsp;</div>
            <asp:GridView 
                ID="UsersGridView" 
                runat="server" 
                CssClass="GridList" 
                AutoGenerateColumns="False" 
                DataKeyNames="UserName" 
                BorderColor="Black" 
                BorderStyle="Solid" 
                BorderWidth="1px" 
                AllowPaging="True"
                EmptyDataText="No information retrieved">
                <Columns>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <nobr>
                                <asp:ImageButton ID="SaveButton" runat="server" CommandName="Update" ImageUrl="~/images/save.bmp" ToolTip="Save pending changes" />
                                <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" ImageUrl="~/images/cancel.bmp" ToolTip="Cancel pending changes" />
                            </nobr>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <nobr>
                                <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit this entry" />
                                <asp:ImageButton ID="SelectButton" runat="server" CommandName="Select" ImageUrl="~/images/table.bmp" ToolTip="Manage this user's roles" />
                            </nobr>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Name" SortExpression="UserName" >
                        <EditItemTemplate>
                            <asp:Label ID="UserNameLB" runat="server" Text='<%#: Bind("UserName") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="UserNameLB" runat="server" Text='<%#: Eval("UserName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DisplayName" HeaderText="Display Name" SortExpression="DisplayName" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:CheckBoxField DataField="IsSuperUser" HeaderText="Is Super User" SortExpression="IsSuperUser"  />
                    <asp:CheckBoxField DataField="IsApproved" HeaderText="Is Approved" SortExpression="IsApproved" />
                    <asp:CheckBoxField DataField="IsLockedOut" HeaderText="Is Locked Out" SortExpression="IsLockedOut" />
                    <asp:CheckBoxField DataField="IsActive" HeaderText="Is Active" SortExpression="IsActive" />
                    <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" SortExpression="IsDeleted" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="DeleteButton" runat="server" CommandName="Delete" ImageUrl="~/images/x_red.bmp" ToolTip="Delete this entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="Gainsboro" />
            </asp:GridView>
        </asp:View>
        <asp:View ID="UsersModifyView" runat="server">
            <h3><asp:Label ID="AddEditTitleLabel" runat="server"></asp:Label></h3>
            <table class="GridList" style="border: solid 1px black;" cellpadding="5" cellspacing="0">
                <tr runat="server">
                    <th runat="server">User Name</th>
                    <td runat="server">
                        <asp:Label ID="UserNameLabel" runat="server"></asp:Label>
                        <asp:TextBox ID="UserNameTextBox" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server">
                    <th runat="server">Display Name</th>
                    <td runat="server"><asp:TextBox ID="DisplayNameTextBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Email</th>
                    <td runat="server"><asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Location</th>
                    <td runat="server"><asp:TextBox ID="LocationTextBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Country Code</th>
                    <td runat="server"><asp:TextBox ID="CountryCodeTextBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Phone Number</th>
                    <td runat="server"><asp:TextBox ID="PhoneNumberTextBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Manager</th>
                    <td runat="server"><asp:TextBox ID="ManagerTextBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Department</th>
                    <td runat="server"><asp:TextBox ID="DepartmentTextBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Title</th>
                    <td runat="server"><asp:TextBox ID="TitleTextBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Company</th>
                    <td runat="server"><asp:TextBox ID="CompanyTextBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Default Program</th>
                    <td runat="server">
                        <asp:DropDownList ID="DefaultProgramDropDownList" runat="server">
                            <asp:ListItem Value="0">Choose a Program</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr runat="server">
                    <th runat="server">Is Super User</th>
                    <td runat="server"><asp:CheckBox ID="IsSuperUserCheckBox" runat="server" /></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Is Active</th>
                    <td runat="server"><asp:CheckBox ID="IsActiveCheckBox" runat="server" /></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Is Locked Out</th>
                    <td runat="server"><asp:CheckBox ID="IsLockedOutCheckBox" runat="server" style="" /></td>
                </tr>
                <tr runat="server">
                    <td colspan="2" style="text-align:center;" runat="server">
                        <asp:Button ID="SaveButton" runat="server" text="Save" AccessKey="S"/>
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" AccessKey="C" />
                    </td>
                </tr>
                <tr runat="server">
                    <td colspan="2" style="text-align: center;" runat="server">
                        <asp:Button ID="ChangePasswordButton" runat="server" Text="Change Password" AccessKey="P" />
                    </td>
                </tr>
                <tr runat="server">
                    <td colspan="2" style="text-align: center;" runat="server">
                        <asp:Button ID="UnlockUserAccountButton" runat="server" Text="Unlock User Account" AccessKey="U" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="RolesForUserView" runat="server">
            <h3>Assign Roles to User</h3>
            <div style="height: 20px;">&nbsp;</div>
            <table cellpadding="5" cellspacing="0" border="solid 1px black">
                <tr>
                    <th>User Name</th>
                    <th>Display Name</th>
                    <th rowspan="2"><asp:Button ID="DoneButton" runat="server" text="Done" AccessKey="D" /></th>
                </tr>
                <tr>
                    <td><asp:Label ID="UserNameLabel2" runat="server"></asp:Label></td>
                    <td><asp:Label ID="DisplayNameLabel" runat="server"></asp:Label></td>
                </tr>
            </table>
            <h3>Roles Assigned to User</h3>
            <asp:GridView 
                ID="RolesForUserGridView" 
                runat="server" 
                AutoGenerateColumns="False" 
                DataKeyNames="RoleId" 
                BorderColor="Black" 
                BorderStyle="Solid" 
                BorderWidth="1px" 
                EmptyDataText="No Roles Assigned to User">
                <Columns>
                    <asp:TemplateField HeaderText="Program" SortExpression="ProgName">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%#: Eval("ProgName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RoleId" HeaderText="RoleId" InsertVisible="False" ReadOnly="True"
                        SortExpression="RoleId" Visible="False" />
                    <asp:BoundField DataField="RoleName" HeaderText="Role" SortExpression="RoleName" />
                    <asp:BoundField DataField="RoleDescription" HeaderText="Description" SortExpression="RoleDescription" />
                    <asp:CheckBoxField DataField="IsProgramManager" HeaderText="Is Prog Mgr" SortExpression="IsProgramManager" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton id="RemoveButton" runat="server" CommandName="Select" ImageUrl="~/images/minus_red.bmp" ToolTip="Remove this role from user" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="Gainsboro" />
            </asp:GridView>
            
            <h3>Roles Not Assigned to User</h3>
            <asp:GridView 
                ID="RolesNotForUserGridView" 
                runat="server" 
                AutoGenerateColumns="False" 
                DataKeyNames="RoleId" 
                BorderColor="Black" 
                BorderStyle="Solid" 
                BorderWidth="1px">
                <Columns>
                    <asp:TemplateField HeaderText="Program" SortExpression="ProgName">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%#: Eval("ProgName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RoleId" HeaderText="RoleId" InsertVisible="False" ReadOnly="True"
                        SortExpression="RoleId" Visible="False" />
                    <asp:BoundField DataField="RoleName" HeaderText="Role" SortExpression="RoleName" />
                    <asp:BoundField DataField="RoleDescription" HeaderText="Description" SortExpression="RoleDescription" />
                    <asp:CheckBoxField DataField="IsProgramManager" HeaderText="Is Prog Mgr" SortExpression="IsProgramManager" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton id="AssignButton" runat="server" CommandName="Select" ImageUrl="~/images/plus_green.bmp" ToolTip="Assign this role to user" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="Gainsboro" />
            </asp:GridView>
            <asp:HiddenField ID="UserNameHiddenField" runat="server" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
