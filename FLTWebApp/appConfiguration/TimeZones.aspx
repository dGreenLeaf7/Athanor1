<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="TimeZones.aspx.vb" Inherits="FLTWebApp.TimeZones" 
    title="Time Zones" %>
<asp:Content ID="TimeZonesContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Time Zones</h2> 
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false"></asp:BulletedList>
    <asp:MultiView ID="TimeZonesMultiView" runat="server" ActiveViewIndex="0">
        <asp:View ID="TimeZonesListView" runat="server">
            <table class="ListFilterTable" cellpadding="5" cellspacing="0" style="border: solid 1px black">
                <tr>
                    <th>Is Active</th>
                    <td rowspan="2">
                        <asp:ImageButton ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add New Entry" />
                    </td>
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
                ID="TimeZonesGridView" 
                runat="server" 
                CssClass="GridList" 
                AutoGenerateColumns="False" 
                DataKeyNames="TimeZoneID,ConcurrencyId" 
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
                    <asp:TemplateField HeaderText="TimeZoneID" SortExpression="TimeZoneID" Visible="False" >
                        <EditItemTemplate>
                            <asp:Label ID="TimeZoneIdLB" runat="server" Text='<%#: Bind("TimeZoneId") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="TimeZoneIdLB" runat="server" Text='<%#: Eval("TimeZoneId")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Time Zone Code" SortExpression="TimeZoneCode" >
                        <EditItemTemplate>
                            <asp:TextBox ID="TimeZoneCodeTB" runat="server" Text='<%#: Bind("TimeZoneCode") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="TimeZoneCodeLB" runat="server" Text='<%#: Eval("TimeZoneCode")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Time Zone Offset" SortExpression="TimeZoneOffset" >
                        <EditItemTemplate>
                            <asp:TextBox ID="TimeZoneOffsetTB" runat="server" Text='<%#: Bind("TimeZoneOffset") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="TimeZoneOffsetLB" runat="server" Text='<%#: Eval("TimeZoneOffset")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" SortExpression="TimeZoneDescr" >
                        <EditItemTemplate>
                            <asp:TextBox ID="TimeZoneDescrTB" runat="server" Text='<%#: Bind("TimeZoneDescr") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="TimeZoneDescrLB" runat="server" Text='<%#: Eval("TimeZoneDescr")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Daylight Saving Time" SortExpression="DaylightSavingTime" >
                        <EditItemTemplate>
                            <asp:CheckBox ID="DaylightSavingTimeCB" runat="server" Checked='<%# Bind("DaylightSavingTime") %>' Enabled="true" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="DaylightSavingTimeCB" runat="server" Checked='<%# Bind("DaylightSavingTime") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Daylight Saving Offset" SortExpression="DaylightSavingOffset" >
                        <EditItemTemplate>
                            <asp:TextBox ID="DaylightSavingOffsetTB" runat="server" Text='<%#: Bind("DaylightSavingOffset") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="DaylightSavingOffsetLB" runat="server" Text='<%#: Eval("DaylightSavingOffset")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Daylight Saving Begin" SortExpression="DaylightSavingBegin" >
                        <EditItemTemplate>
                            <asp:TextBox ID="DaylightSavingBeginTB" runat="server" Text='<%#: Bind("DaylightSavingBegin") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="DaylightSavingBeginLB" runat="server" Text='<%#: Eval("DaylightSavingBegin")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Daylight Saving End" SortExpression="DaylightSavingEnd" >
                        <EditItemTemplate>
                            <asp:TextBox ID="DaylightSavingEndTB" runat="server" Text='<%#: Bind("DaylightSavingEnd") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="DaylightSavingEndLB" runat="server" Text='<%#: Eval("DaylightSavingEnd")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IsActive" SortExpression="IsActive" >
                        <EditItemTemplate>
                            <asp:CheckBox ID="IsActiveCB" runat="server" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="IsActiveCB" runat="server" Checked='<%# Bind("IsActive") %>' />
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
        <asp:View ID="TimesZonesModifyView" runat="server">
            <h3><asp:label ID="AddEditTitleLabel" runat="server"></asp:label></h3>
            <table class="GridList" style="border: solid 1px black;" CellPadding="5" CellSpacing="0" >
                <tr>
                    <th>Time Zone Code</th>
                    <td><asp:TextBox ID="TimeZoneCodeTB" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>Offset (in Hours)</th>
                    <td><asp:TextBox ID="TimeZoneOffsetTB" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>Description</th>
                    <td><asp:TextBox ID="TimeZoneDescrTB" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>Daylight Savings Zone</th>
                    <td><asp:CheckBox ID="DaylightSavingCB" runat="server"></asp:CheckBox></td>
                </tr>
                <tr>
                    <th>Daylight Savings Offset</th>
                    <td><asp:TextBox ID="DaylightSavingOffsetTB" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>Daylight Saving Begin Date/Time</th>
                    <td><asp:TextBox ID="DaylightSavingBeginTB" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>Daylight Saving End Date/Time</th>
                    <td><asp:TextBox ID="DaylightSavingEndTB" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>Is Active</th>
                    <td><asp:CheckBox ID="IsActiveCB" runat="server" /></td>
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
