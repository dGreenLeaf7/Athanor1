<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="WeightDefTypes.aspx.vb" Inherits="FLTWebApp.WeightDefTypes" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Weight Definition Types</h2> 
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false"></asp:BulletedList>
    <asp:MultiView ID="WeightDefTypesMultiView" runat="server" ActiveViewIndex="0">
        <asp:View ID="WeightDefTypesListView" runat="server">
            <asp:ImageButton ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add new entry" />
            <div class="VerticalSpacer">&nbsp;</div>
            <asp:GridView 
                ID="WeightDefTypesGridView" 
                runat="server" 
                CssClass="GridList" 
                AutoGenerateColumns="False" 
                DataKeyNames="WeightDefTypeID,ConcurrencyId" 
                AllowPaging="True" 
                BorderStyle="Solid" 
                BorderWidth="1px" 
                EmptyDataText="No Data Retrieved">
                <Columns>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="SaveButton" runat="server" CommandName="Update" ImageUrl="~/images/save.bmp" ToolTip="Save pending changes" />
                            <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" ImageUrl="~/images/cancel.bmp" ToolTip="Cancel pending changes" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit This Entry" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="WeightDefTypeID" SortExpression="WeightDefTypeID" Visible="False" >
                        <EditItemTemplate>
                            <asp:Label ID="WeightDefTypeIdLB" runat="server" Text='<%#: Bind("WeightDefTypeId") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="WeightDefTypeIdLB" runat="server" Text='<%#: Eval("WeightDefTypeId")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" SortExpression="WeightDefTypeDescr" >
                        <EditItemTemplate>
                            <asp:TextBox ID="WeightDefTypeDescrTB" runat="server" Text='<%#: Eval("WeightDefTypeDescr")%>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="WeightDefTypeDescrLB" runat="server" Text='<%#: Eval("WeightDefTypeDescr")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sort" SortExpression="SortOrder" >
                        <EditItemTemplate>
                            <asp:TextBox ID="SortOrderTB" runat="server" Text='<%#: Bind("SortOrder") %>' style="width: 400px;"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="SortOrderTB" runat="server" Text='<%#: Eval("SortOrder")%>' style="width: 400px;"></asp:Label>
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
        <asp:View ID="WeightDefTypeAddView" runat="server">
            <h3><asp:label ID="AddEditTitleLabel" runat="server"></asp:label></h3>
            <table class="GridList" style="border: solid 1px black;" cellpadding="5" cellspacing="0" >
                <tr>
                    <th>Description</th>
                    <td><asp:TextBox ID="WeightDefTypeDescrTB" runat="server" ></asp:TextBox></td>
                </tr>
                <tr>
                    <th>Sort Order</th>
                    <td><asp:TextBox ID="SortOrderTB" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="2">
                        <asp:Button ID="SaveButton" runat="server" Text="Save" AccessKey="S" OnClick="SaveButton_Click" Width="75px" Height="30px" />
                        &nbsp;
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" AccessKey="C" OnClick="CancelButton_Click" Width="75px" Height="30px" />
                    </td>
                </tr>
            </table>        
        </asp:View>
    </asp:MultiView>
</asp:Content>
