<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="WeightDefs.aspx.vb" Inherits="FLTWebApp.WeightDefs" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Weight Tracking</h2>
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false"></asp:BulletedList>
    <asp:MultiView ID="WeightDefMultiView" runat="server" ActiveViewIndex="0">
    
        <asp:View ID="WeightDefListView" runat="server">
            <table class="ListFilterTable" cellpadding="5" cellspacing="0" style="border: solid 1px black">
                <tr>
                    <td>Weight Tracking Type</td>
                    <td>Is Active</td>
                    <td>Is Deleted</td>
                    <td rowspan="2"><asp:ImageButton ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add new entry" AccessKey="A" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="WeightDefTypeFilterDropDownList" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>                
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
                            <asp:ListItem Value="0">Only Deleted</asp:ListItem>
                            <asp:ListItem Value="-1">All</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div class="VerticalSpacer">&nbsp;</div>
            <asp:GridView 
                ID="WeightDefsGridView" 
                runat="server" 
                CssClass="GridList"
                AutoGenerateColumns="False" 
                DataKeyNames="WeightDefId,ConcurrencyId" 
                AllowPaging="true"
                EmptyDataText="No Information Retrieved" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                <Columns>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="SaveButton" runat="server" CommandName="Update" ImageUrl="~/images/save.bmp" ToolTip="Save pending changes" />
                            <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" ImageUrl="~/images/cancel.bmp" ToolTip="Cancel pending changes" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="EditButton" runat="Server" CommandName="Edit" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit this entry" />
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <EditItemTemplate>
                            <asp:Label id="WeightDefIdLB" runat="server" Text='<%#: Bind("WeightDefId") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label id="WeightDefIdLB" runat="server" Text='<%#: Eval("WeightDefId")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
                        <EditItemTemplate>
                            <asp:DropDownList ID="WeightDefTypeDDL" runat="server" DataSource="<%# GetWeightDefTypes() %>" AppendDataBoundItems="true" DataTextField="WeightDefTypeDescr" DataValueField="WeightDefTypeId" SelectedValue='<%# Bind("WeightDefTypeId") %>'></asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="WeightDefTypeLB" runat="server" Text='<%#: Eval("WeightDefTypeDescr")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" SortExpression="WeightDefDescr">
                        <EditItemTemplate>
                            <asp:TextBox ID="WeightDefDescrTB" runat="server" Text='<%#: Bind("WeightDefDescr") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="WeightDefDescrTB" runat="server" Text='<%#: Eval("WeightDefDescr")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Weight" SortExpression="Description" >
                        <EditItemTemplate>
                            <asp:TextBox ID="WeightTB" runat="server" Text='<%#: Bind("Weight") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="WeightLB" runat="server" Text='<%#: Eval("Weight")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="Unit">
                        <EditItemTemplate>
                            <asp:DropDownList ID="WeightUnitDDL" runat="server" DataSource="<%# GetWeightUnits() %>" AppendDataBoundItems="true" DataTextField="StringValue" DataValueField="Id" SelectedValue='<%# Bind("WeightUnitId") %>'></asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="WeightUnitLB" runat="server" Text='<%#: Eval("WeightUnit")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="Sort">
                        <EditItemTemplate>
                            <asp:TextBox ID="SortOrderTB" runat="server" Text='<%#: Bind("SortOrder") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="SortOrderLB" runat="server" Text='<%#: Eval("SortOrder")%>'></asp:Label>
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
                            <asp:CheckBox ID="IsDeletedCB" runat="server" Checked='<%# Bind("IsDeleted") %>' />
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
    
        <asp:View ID="WeightDefAddView" runat="server">
            <h3>Add New Item</h3>
            <asp:Table ID="WeightDefAddTable" runat="server" GridLines="both">
                <asp:TableRow>
                    <asp:TableHeaderCell>Type</asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="WeightDefTypeDropDownList" runat="server"></asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:Tablerow> 
                    <asp:TableHeaderCell>Description</asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:TextBox ID="WeightDefDescrTextBox" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:Tablerow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Weight</asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:TextBox ID="WeightTextBox" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Weight Unit</asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="WeightUnitDropDownList" runat="server"></asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Sort Order</asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:TextBox ID="SortOrderTextBox" runat="server"></asp:TextBox>
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
