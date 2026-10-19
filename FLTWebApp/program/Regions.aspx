<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="Regions.aspx.vb" Inherits="FLTWebApp.Regions" 
    title="Regions" %>
<asp:Content ID="RegionContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Regions</h2>
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false"></asp:BulletedList>
    <asp:MultiView ID="RegionMultiView" runat="server" ActiveViewIndex="0">
    
        <asp:View ID="RegionListView" runat="server">
            <table class="ListFilterTable" cellpadding="5" cellspacing="0" style="border: solid 1px black">
                <tr>
                    <td>Is Active</td>
                    <td rowspan="2"><asp:ImageButton ID="AddButton" runat="server" imageurl="~/images/plus_blue.bmp" ToolTip="Add new entry" AccessKey="A" /></td>
                </tr>
                <tr>
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
                ID="RegionsGridView" 
                runat="server"
                CssClass="GridList"
                AutoGenerateColumns="False" 
                DataKeyNames="Id,ProgId,ConcurrencyId" 
                ShowFooter="False" EmptyDataText="No Information Retrieved" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                <Columns>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="SaveButton" runat="server" CommandName="Update" ImageUrl="~/images/save.bmp" ToolTip="Save pending changes" />
                            <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" ImageUrl="~/images/cancel.bmp" ToolTip="Cancel pending changes" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton id="EditButton" runat="server" commandName="Edit" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit this entry" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <EditItemTemplate>
                            <asp:Label ID="IDLB" runat="server" Text='<%#: Bind("Id") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="IDLB" runat="server" Text='<%#: Eval("Id")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Region Code" SortExpression="Code" >
                        <EditItemTemplate>
                            <asp:TextBox ID="CodeTB" runat="server" text='<%#: Bind("Code") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="CodeLB" runat="server" text='<%#: Eval("Code") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Region Name" SortExpression="Name" >
                        <EditItemTemplate>
                            <asp:TextBox ID="NameTB" runat="server" text='<%#: Bind("Name") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="NameLB" runat="server" text='<%#: Eval("Name")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Active" SortExpression="IsActive" >
                        <EditItemTemplate>
                            <asp:CheckBox ID="IsActiveCB" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="true" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="IsActiveCB" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="False" />
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
    
        <asp:View ID="RegionAddView" runat="server">
            <h3>Add New Item</h3>
            <asp:Table ID="RegionAddTable" runat="server" GridLines="both">
                <asp:Tablerow> 
                    <asp:TableHeaderCell>Region Code</asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:TextBox ID="RegionCodeTextBox" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:Tablerow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Region Name</asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:TextBox ID="RegionNameTextBox" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" style="text-align:center">
                        <asp:Button ID="SaveButton" runat="server" Text="Save" AccessKey="S" Width="75px" Height="30px" />
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" accesskey="C" Width="75px" Height="30px" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:View>
        
    </asp:MultiView>

    <asp:HiddenField ID="ProgIdHiddenField" runat="server" />

</asp:Content>
