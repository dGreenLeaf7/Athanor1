<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="documentsList.aspx.vb" Inherits="FLTWebApp.documentsList" 
    title="Document List Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Documents List</h2>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green; font-weight: bold;"></asp:Label></div>
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red; font-weight: bold; width: 100%;" ></asp:Label></div>
    <asp:BulletedList 
        ID="ErrorsBulletList" 
        runat="server"
        DataValuefield="RuleIdentifier"
        DataTextField="Message"
        Visible="false">
    </asp:BulletedList>
    <div><asp:Label id="WarningMessageLabel" runat="server" style="color: #C35617; font-weight: bold;"></asp:Label></div>
    <asp:CheckBoxList 
        ID="WarningsCheckList" 
        runat="server" 
        DataValueField="RuleIdentifier"
        DataTextField="Message"
        Visible="false" 
        style="left: 1em; position: relative;" 
        CellPadding="5" 
        CellSpacing="0">
    </asp:CheckBoxList>
    
    <asp:MultiView ID="DocumentMultiView" runat="server" ActiveViewIndex="0">
    
        <asp:View ID="DocumentListView" runat="server">
            <table class="ListFilterTable" cellpadding="5" cellspacing="0">
                <tr>
                    <th>Doc Type</th>
                    <th>Is Deleted</th>
                    <th rowspan="2" style="width: 45px">
                        <asp:ImageButton ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add New Entry" style="margin: auto;" />
                    </th>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownlist ID="DocumentTypeDDL" runat="server" AutoPostBack="true" >
                        </asp:DropDownlist>
                    </td>
                    <td>
                        <asp:DropDownList ID="IsDeletedDDL" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="-1">All</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div class="VerticalSpacer">&nbsp;</div>
            <asp:GridView 
                ID="DocumentsGridView" 
                runat="server" 
                CssClass="GridList" 
                AutoGenerateColumns="False" 
                DataKeyNames="DocumentId, ProgId, ConcurrencyId" 
                EmptyDataText="No Records Retrieved" 
                AllowPaging="True" 
                BorderColor="Black" 
                BorderStyle="Solid" 
                BorderWidth="1px" 
                CellPadding="5">
                <Columns>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="UpdateButton" runat="server" ImageUrl="~/images/save.bmp" ToolTip="Save changes" CommandName="Update" />
                            <asp:ImageButton ID="CancelButton" runat="server" ImageUrl="~/images/cancel.bmp" ToolTip="Cancel changes" CommandName="Cancel" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="EditButton" runat="server" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit Document Metadata" CommandName="Edit" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>&nbsp;</EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="SelectButton" runat="server" ImageUrl="~/images/arrowDown_blue.bmp" ToolTip="Download document" CommandName="Select" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="DocumentIdLB" runat="server" Visible="false" Text='<%# Bind("DocumentId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DocumentId" HeaderText="DocumentId" 
                        ReadOnly="True" SortExpression="AircraftId" Visible="False" />
                    <asp:TemplateField HeaderText="Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="DocumentNameTB" runat="Server" text='<%#: Bind("DocumentName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="DocumentNameLB" runat="server" text='<%#: Eval("DocumentName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mime Type">
                        <ItemTemplate>
                            <asp:Label ID="MimeType" runat="server" text='<%#: Eval("MimeType")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Size">
                        <ItemTemplate>
                            <asp:Label ID="SizeLB" runat="server" Text='<%# Bind("DocumentSize") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <EditItemTemplate>
                            <asp:TextBox ID="DocumentDescriptionTB" runat="server" Text='<%#: Bind("DocumentDescription") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="DocumentDescriptionLB" runat="server" Text='<%#: Eval("DocumentDescription")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Deleted">
                        <EditItemTemplate>
                            <asp:CheckBox ID="IsDeletedCB" runat="server" Checked='<%# Bind("IsDeleted") %>' enabled="true" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="IsDeletedCB" runat="server" Checked='<%# Bind("IsDeleted") %>' enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="DeleteButton" runat="server" ImageUrl="~/images/x_red.bmp" ToolTip="Delete Aircraft" CommandName="Delete" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="Gainsboro" />
            </asp:GridView>
        </asp:View>
    
        <asp:View ID="DocumentAddView" runat="server">
        
        </asp:View>
        
    </asp:MultiView> 
    <asp:HiddenField ID="ProgIdHiddenField" runat="server" />

</asp:Content>
