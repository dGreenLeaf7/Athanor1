<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="DocumentTypes.aspx.vb" Inherits="FLTWebApp.DocumentTypes"
    title="Document Types" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Document Types</h2> 
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false"></asp:BulletedList>
    <asp:MultiView ID="DocumentTypesMultiView" runat="server" ActiveViewIndex="0">
        <asp:View ID="DocumentTypesListView" runat="server">
            <asp:ImageButton ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add new entry" />
            <div class="VerticalSpacer">&nbsp;</div>
            <asp:GridView 
                ID="DocumentTypesGridView" 
                runat="server" 
                CssClass="GridList" 
                AutoGenerateColumns="False" 
                DataKeyNames="DocumentTypeID,ConcurrencyId" 
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
                    <asp:TemplateField HeaderText="DocumentTypeID" SortExpression="DocumentTypeID" Visible="False" >
                        <EditItemTemplate>
                            <asp:Label ID="DocumentTypeIdLB" runat="server" Text='<%#: Bind("DocumentTypeId") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="DocumentTypeIdLB" runat="server" Text='<%#: Eval("DocumentTypeId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="File Ext." SortExpression="FileExtension" >
                        <EditItemTemplate>
                            <asp:TextBox ID="FileExtensionTB" runat="server" Text='<%#: Bind("FileExtension") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="FileExtensionLB" runat="server" Text='<%#: Eval("FileExtension") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mime Type" SortExpression="MimeType" >
                        <EditItemTemplate>
                            <asp:TextBox ID="MimeTypeTB" runat="server" Text='<%#: Bind("MimeType") %>' style="width: 400px;"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="MimeTypeLB" runat="server" Text='<%#: Eval("MimeType") %>' style="width: 400px;"></asp:Label>
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
        <asp:View ID="DocumentTypeAddView" runat="server">
            <h3><asp:label ID="AddEditTitleLabel" runat="server"></asp:label></h3>
            <table class="GridList" style="border: solid 1px black;" cellpadding="5" cellspacing="0" >
                <tr>
                    <th>File Extension</th>
                    <td><asp:TextBox ID="FileExtensionTB" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>Mime Type</th>
                    <td><asp:TextBox ID="MimeTypeTB" runat="server" style="width: 400px;"></asp:TextBox></td>
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
