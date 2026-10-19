<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="modules.aspx.vb" Inherits="FLTWebApp.modules" 
    title="Modules" %>
<asp:Content ID="ModulesContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Modules</h2>
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false">
    </asp:BulletedList>
    <asp:MultiView ID="ModulesMultiView" runat="server" ActiveViewIndex="0">
        <asp:View ID="ModulesListView" runat="server">
            <table class="ListFilterTable" cellspacing="0" cellpadding="5" style="border: solid 1px black;">
                <tr>
                    <td>
                        <asp:Label ID="IsActiveLabel" runat="server" Text="Is Active"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="IsDeletedLabel" runat="server" Text="Is Deleted"></asp:Label>
                    </td>
                    <td rowspan="2">
                        <asp:ImageButton ID="AddButton" runat="server" AccessKey="A" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add new entry" /><br />
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
                ID="ModulesGridView" 
                runat="server" 
                CssClass="GridList" 
                AutoGenerateColumns="False" 
                DataKeyNames="ModuleName" 
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
                    <asp:TemplateField HeaderText="Name" SortExpression="ModuleName" >
                        <EditItemTemplate>
                            <asp:Label ID="ModuleNameLB" runat="server" Text='<%#: Bind("ModuleName") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="ModuleNameLB" runat="server" Text='<%#: Eval("ModuleName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" SortExpression="ModuleDescription" >
                        <EditItemTemplate>
                            <asp:TextBox ID="ModuleDescriptionTB" runat="server" text='<%#: Bind("ModuleDescription") %>' ></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="ModuleDescriptionLB" runat="server" text='<%#: Eval("ModuleDescription")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Active" SortExpression="IsActive" >
                        <EditItemTemplate>
                            <asp:CheckBox ID="IsActiveCB" runat="server" checked='<%# Bind("IsActive") %>' Enabled="true" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="IsActiveCB" runat="server" checked='<%# Bind("IsActive") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Deleted" SortExpression="IsDeleted"  >
                        <EditItemTemplate>
                            <asp:CheckBox ID="IsDeletedCB" runat="server" checked='<%# Bind("IsDeleted") %>' Enabled="false" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="IsDeletedCB" runat="server" checked='<%# Bind("IsDeleted") %>' Enabled="false" />
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
        <asp:View ID="ModulesModifyView" runat="server">
            <h3><asp:Label ID="AddEditTitleLabel" runat="server"></asp:Label></h3>
            <table class="GridList" cellpadding="5" cellspacing="0" style="border: solid 1px black;">
                <tr runat="server">
                    <th runat="server">Name</th>
                    <td runat="server"><asp:TextBox ID="ModuleNameTextBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Description</th>
                    <td runat="server"><asp:TextBox ID="ModuleDescriptionTextBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr runat="server">
                    <th runat="server">Is Active</th>
                    <td runat="server"><asp:CheckBox ID="IsActiveCheckBox" runat="server"></asp:CheckBox></td>
                </tr>
                <tr runat="server">
                    <td colspan="2" style="text-align:center;" runat="server">
                        <asp:Button ID="SaveButton" runat="server" text="Save" AccessKey="S" Width="75px" height="30px"/>
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" AccessKey="C" Width="75px" height="30px" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
