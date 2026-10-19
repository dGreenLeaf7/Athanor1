<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="programs.aspx.vb" Inherits="FLTWebApp.programs" 
    title="Untitled Page" %>
<asp:Content ID="ProgramContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Programs</h2>
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false">
    </asp:BulletedList>
    <asp:MultiView ID="ProgramMultiView" runat="server" ActiveViewIndex="0">
        <asp:View ID="ProgramListView" runat="server" EnableViewState="true">
            <table class="ListFilterTable" cellspacing="0" cellpadding="5" style="border: solid 1px black;">
                <tr>
                    <td>
                        <asp:Label ID="IsActiveLabel" runat="server" Text="Is Active"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="IsDeletedLabel" runat="server" Text="Is Deleted"></asp:Label>
                    </td>
                    <td rowspan="2">
                        <asp:ImageButton ID="Addbutton" runat="server" AccessKey="A" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add New Entry" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="IsActiveDropDownList" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="true" Value="1" Text="Active Only"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Inactive Only"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="All"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="IsDeletedDropDownList" runat="server" AutoPostBack="true">
                            <asp:ListItem Selected="True" Value="0" Text="No Deleted"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Only Deleted"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="All"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div class="VerticalSpacer">&nbsp;</div>
            <asp:GridView 
                ID="ProgramGridView" 
                runat="server" 
                CssClass="GridList" 
                AllowPaging="True" 
                AutoGenerateColumns="False"
                DataKeyNames="ProgID" 
                Caption="Program List" 
                CaptionAlign="Top" 
                EmptyDataText="No Information Retrieved" 
                BorderColor="Black" 
                BorderStyle="Solid" 
                BorderWidth="1px" >
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="SelectButton" runat="server" CommandName="Select" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit This Entry" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="ProgIdLB" runat="server" Text='<%#: Eval("ProgId")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ProgName" HeaderText="Name" SortExpression="ProgName" />
                    <asp:BoundField DataField="ProgDescription" HeaderText="Description" SortExpression="ProgDescription" />
                    <asp:BoundField DataField="ProgVersion" HeaderText="Version" SortExpression="ProgVersion" />
                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" />
                    <asp:CheckBoxField DataField="IsDeleted" HeaderText="IsDeleted" SortExpression="IsDeleted" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="DeleteButton" runat="server" CommandName="Delete" ImageUrl="~/images/x_red.bmp" ToolTip="Delete This Entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="Gainsboro" />
            </asp:GridView>
        </asp:View>
        <asp:View ID="ProgramEditView" runat="server">
            <h3><asp:label ID="AddEditTitleLabel" runat="server"></asp:label></h3>
            <asp:Table ID="EditTable" runat="server" cssclass="GridList" style="border: solid 1px black" cellpadding="5" cellspacing="0" >
                <asp:TableRow>
                    <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="ProgNameTextBox" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Description</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="ProgDescriptionTextBox" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Version</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="ProgVersionTextBox" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Program Logo</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="ProgramLogoTextBox" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Report Logo</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="ReportLogoTextBox" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Year Logo</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="YearLogoTextBox" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Program Status</asp:TableHeaderCell>
                    <asp:TableCell><asp:DropDownList ID="ProgStatusDropDownList" runat="server"></asp:DropDownList></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Date Opened</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="DateOpenedTextBox" runat="server" CssClass="datePickerTB"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Date Closed</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="DateClosedTextBox" runat="server" CssClass="datePickerTB"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Actual Value</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="ActualValueTextBox" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Contoso Project Nbr</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="ProjectCodeTextBox" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Contoso Activity Nbr</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="ActivityCodeTextBox" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Contoso Department Nbr</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="DeptCodeTextBox" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Company Name</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="CompanyNameTextBox" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Near End Usage</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="NearEndUsageTextBox" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Avg Daily Usage</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="AvgDailyUsageTextBox" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell>Weekly Test Days</asp:TableHeaderCell>
                    <asp:TableCell><asp:TextBox ID="WeeklyFlightDaysTextBox" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="CloneTableRow" runat="server" >
                    <asp:TableHeaderCell>Clone from Program</asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="CloneProgDDL" runat="server"></asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell style="text-align: center" ColumnSpan="2">
                        <asp:Button ID="SaveButton" runat="server" Text="Save" AccessKey="S" Width="75px" Height="30px" OnClick="SaveButton_Click" ToolTip="Save Pending Changes" />
                        &nbsp;
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" Width="75px" Height="30px" AccessKey="C" ToolTip="Cancel Pending Changes" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:View>
    </asp:MultiView>
    <script type="text/javascript">
        $(function() {
            $(".datePickerTB").datepicker();
        });
    </script>
</asp:Content>
