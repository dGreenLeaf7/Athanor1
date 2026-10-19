<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="locations.aspx.vb" Inherits="FLTWebApp.locations" 
    title="Locations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Locations</h2>
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false"></asp:BulletedList>
    <asp:MultiView ID="LocationMultiView" runat="server" ActiveViewIndex="0">
    
        <asp:View ID="LocationListView" runat="server">
            <table class="ListFilterTable" cellpadding="5" cellspacing="0" style="border: solid 1px black">
                <tr>
                    <td>Is Active</td>
                    <td>Is Deleted</td>
                    <td rowspan="2"><asp:ImageButton ID="AddButton" runat="server" imageurl="~/images/plus_blue.bmp" ToolTip="Add new entry" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="IsActiveDropDownList" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="1">Active Only</asp:ListItem>
                            <asp:ListItem Value="0">Inactive Only</asp:ListItem>
                            <asp:ListItem Value="-1">All</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="IsDeletedDropDownlist" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="0">Not Deleted</asp:ListItem>
                            <asp:ListItem Value="1">Only Deleted</asp:ListItem>
                            <asp:ListItem Value="-1">All</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div class="VerticalSpacer">&nbsp;</div>
            <asp:GridView 
                ID="LocationsGridView" 
                runat="server" 
                CssClass="GridList"
                AutoGenerateColumns="False" 
                DataKeyNames="LocactionId,ConcurrencyId,ProgId" 
                EmptyDataText="No Information Retrieved" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
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
                    <asp:TemplateField Visible="false">
                        <EditItemTemplate>
                            <asp:Label ID="LocationIdLB" runat="server" Text='<%#: Bind("LocactionId") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LocationIdLB" runat="server" Text='<%#: Eval("LocactionId")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Location Name" SortExpression="LocationName">
                        <EditItemTemplate>
                            <asp:TextBox ID="LocationNameTB" runat="server" Text='<%#: Bind("LocationName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LocationLB" runat="server" Text='<%#: Eval("LocationName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Time Zone Code" SortExpression="TimeZoneCode">
                        <EditItemTemplate>
                            <asp:DropDownList ID="TimeZoneDDL" runat="server" DataSource="<%# GetTimeZones() %>" DataTextField="TimeZoneCode" DataValueField="TimeZoneId" SelectedValue='<%# Bind("TimeZoneId") %>'>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%#: Eval("TimeZoneCode")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Active" SortExpression="IsActive">
                        <EditItemTemplate>
                            <asp:CheckBox ID="IsActiveCB" runat="server" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="IsActiveCB" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="False" />                        
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Deleted" SortExpression="IsDeleted">
                        <EditItemTemplate>
                            <asp:CheckBox ID="IsDeletedCB" runat="server" Checked='<%# Bind("IsDeleted") %>' Enabled="False" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="IsDeletedCB" runat="server" Checked='<%# Bind("IsDeleted") %>' Enabled="False" />                        
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
    
        <asp:View ID="LocationAddView" runat="server">
            <h3>Add New Item</h3>
            <asp:Table ID="LocationAddTable" runat="server" GridLines="both">
                <asp:Tablerow> 
                    <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:TextBox ID="LocationNameTextBox" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:Tablerow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Time Zone</asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="TimeZoneDropDownList2" runat="server" >
                        </asp:DropDownList>                    
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" style="text-align:center">
                        <asp:Button ID="SaveButton" runat="server" Text="Save" Width="75px" Height="30px" AccessKey="S" />
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" Width="75px" Height="30px" accesskey="C" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:View>
        
    </asp:MultiView>

    <asp:HiddenField ID="ProgIdHiddenField" runat="server" />
</asp:Content>
