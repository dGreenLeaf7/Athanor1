<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="ConfigValues.aspx.vb" Inherits="FLTWebApp.configurations" 
    title="Configuration Values" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Configuration Values</h2> 
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false"></asp:BulletedList>
    <asp:MultiView ID="ConfigurationMultiView" runat="server" ActiveViewIndex="0">
        <asp:View ID="ConfigurationListView" runat="server">
            <table class="ListFilterTable" cellpadding="5" cellspacing="0" style="border: solid 1px black">
                <tr>
                    <th>Type</th>
                    <th>Is Active</th>
                    <td rowspan="2">
                        <asp:ImageButton ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add New Entry" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="TypeDropDownList" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="IsActiveDropDownList" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="1">Active Only</asp:ListItem>
                            <asp:ListItem Value="0">Inactive Only</asp:ListItem>
                            <asp:ListItem Value="-1">All</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div class="VerticalSpacer">&nbsp;</div>
            <asp:GridView 
                ID="ConfigurationGridView" 
                runat="server" 
                CssClass="GridList" 
                AllowPaging="True" 
                AutoGenerateColumns="False" 
                DataKeyNames="ID,ProgId,LookupName,ConcurrencyId" 
                EmptyDataText="No information was retrieved"
                BorderColor="Black" 
                BorderStyle="Solid" 
                BorderWidth="1px">
                <Columns>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="SaveButton" runat="server" CommandName="Update" ImageUrl="~/images/save.bmp" ToolTip="Save pending changes" />
                            <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" imageurl="~/images/cancel.bmp" ToolTip="Cancel pending changes" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit this entry" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField visible="false">
                        <EditItemTemplate>
                            <asp:Label ID="IDLB" runat="server" Text='<%#: Bind("ID") %>'></asp:Label>
                            <asp:Label ID="ConcurrencyIdLB" runat="server" Text='<%#: Bind("ID") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="IDLB" runat="server" Text='<%#: Eval("ID")%>'></asp:Label>
                            <asp:Label ID="ConcurrencyIdLB" runat="server" Text='<%#: Eval("ID")%>'></asp:Label>                        
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="StringValue" SortExpression="StringValue" >
                        <EditItemTemplate>
                            <asp:TextBox ID="StringValueTB" runat="server" Text='<%#: Bind("StringValue") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="StringValueLB" runat="server" Text='<%#: Eval("StringValue")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:TextBox ID="SortOrderTB" runat="server" Text='<%#: Bind("SortOrder") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="SortOrderLB" runat="server" Text='<%#: Eval("SortOrder")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IsActive" SortExpression="IsActive" >
                        <EditItemTemplate>
                            <asp:CheckBox ID="IsActiveCB" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="true" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="IsActiveCB" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <asp:ItemTemplate>
                            <asp:ImageButton id="DeleteButton" runat="server" CommandName="Delete" ImageUrl="~/images/x_red.bmp" ToolTip="Delete this entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                        </asp:ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="Gainsboro" />
            </asp:GridView>
        </asp:View>
        <asp:View ID="ConfigModifyView" runat="server">
            <h2>Modify View</h2>
            <asp:Table ID="EditTable"  runat="server" GridLines="both" CellPadding="5" CellSpacing="0" BorderColor="Black" Font-Names="Verdana, Arial">
                <asp:TableRow>
                    <asp:TableHeaderCell>String Value</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="StringValueTextBox" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Sort Order</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="SortOrderTextBox" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Is Active</asp:TableHeaderCell>
                    <asp:TableCell><asp:CheckBox ID="IsActiveCheckBox" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableFooterRow>
                    <asp:TableCell HorizontalAlign="center" ColumnSpan="2">
                        <asp:Button ID="SaveButton" runat="server" Text="Save" AccessKey="S" OnClick="SaveButton_Click" Width="75px" Height="30px" />
                        &nbsp;
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" AccessKey="C" OnClick="CancelButton_Click" Width="75px" Height="30px"/>
                    </asp:TableCell>
                </asp:TableFooterRow>
            </asp:Table>
        </asp:View>
    </asp:MultiView>   
    <asp:HiddenField ID="ProgIdHiddenField" runat="server" />
</asp:Content>
