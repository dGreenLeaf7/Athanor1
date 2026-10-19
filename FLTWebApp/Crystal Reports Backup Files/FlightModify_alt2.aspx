<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="FlightModify_alt2.aspx.vb" Inherits="FLTWebApp.FlightModify_alt2" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="DataEntryContainer">
    
        <div style="text-align: left;">
            <h2><asp:label ID="PageTitleLB" runat="server">Flight Modify</asp:label></h2>
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
        </div>
            
        <asp:MultiView ID="ParentChildMultiview" runat="server" ActiveViewIndex="0">
        
            <asp:View ID="AddView" runat="server">

                <div class="DataEntryTitle">Flight Key Information</div>
                <table class="DataEntryTable" cellpadding="10" cellspacing="0" width="100%; font-family: Tahoma, Verdana, Arial;">
                    <colgroup>
                        <col width="16%" />
                        <col width="16%" />
                        <col width="17%" />
                        <col width="17%" />
                        <col width="16%" />
                        <col width="16%" />
                    </colgroup>
                    <tr>
                        <th>Location</th>
                        <td><asp:DropDownList ID="LocationAddDDL" runat="server" AutoPostBack="true"></asp:DropDownList></td>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
                    <tr>
                        <th>Date</th>
                        <td><asp:TextBox ID="FlightDateAddTB" runat="server" CssClass="datePickerTB"></asp:TextBox></td>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
                    <tr>
                        <th>Tail #</th>
                        <td><asp:DropDownList ID="AircraftAddDDL" runat="server"></asp:DropDownList></td>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>                            
                    </tr>
                    <tr>
                        <th>Shift</th>
                        <td><asp:DropDownList ID="WorkShiftAddDDL" runat="server"></asp:DropDownList></td>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>                            
                    </tr>
                    <tr>
                        <th colspan="6" style="text-align:left">
                            <asp:Button ID="NextButton" runat="server" text="Next" Width="75px" />
                            <asp:Button ID="EditButton" runat="server" Text="Edit" Width="75px" Visible="false" />
                            <asp:Button ID="RetryButton" runat="server" Text="Retry" Width="75px" Visible="false" />
                            <asp:Button ID="CancelButton1" runat="server" Text="Cancel" Width="75px" OnClick="CancelButton_Click" />
                        </th>
                    </tr>
                </table>
                
            </asp:View>
            
            <asp:View ID="View1" runat="server">
                
                <div class="DataEntryTitle">Flight Data Entry</div>
                <table class="DataEntryTable" cellpadding="10" cellspacing="0">
                    <colgroup>
                        <col width="16%" />
                        <col width="16%" />
                        <col width="17%" />
                        <col width="17%" />
                        <col width="16%" />
                        <col width="16%" />
                    </colgroup>
                    <tr>
                        <th>Location</th>
                        <td><asp:DropDownList ID="LocationEditDDL" runat="server"></asp:DropDownList></td>
                        <th>Start Usage</th>
                        <td><asp:TextBox ID="HobbsStartTB" runat="server"></asp:TextBox></td>
                        <th>End Usage</th>
                        <td><asp:TextBox ID="HobbsEndTB" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>Date</th>
                        <td><asp:TextBox ID="FlightDateEditTB" runat="server"></asp:TextBox></td>
                        <th>Oil Level</th>
                        <td>
                            <asp:DropDownList ID="OilLevelDDL" runat="server">
                                <asp:ListItem Selected="true" Value="0">ADD</asp:ListItem>
                                <asp:ListItem Value="1">1/4</asp:ListItem>
                                <asp:ListItem Value="2">1/2</asp:ListItem>
                                <asp:ListItem Value="3">3/4</asp:ListItem>
                                <asp:ListItem Value="4">FULL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <th>Data Log OK</th>
                        <td>
                            <asp:DropDownList ID="PostFlightFtiOKDDL" runat="server">
                                <asp:ListItem Selected="true" Value="0">--</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="-1">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>Tail #</th>
                        <td><asp:Label ID="MSNLB" runat="server"></asp:Label></td>
                        <th>Data Log OK?</th>
                        <td>
                            <asp:DropDownList ID="PreFlightFtiOKDDL" runat="server">
                                <asp:ListItem Selected="true" Value="0">--</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="-1">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <th colspan="2">&nbsp;</th>
                    </tr>
                    <tr>
                        <th>Shift</th>
                        <td><asp:DropDownList ID="WorkShiftEditDDL" runat="server" /></td>
                        <th>Sched. Route</th>
                        <td><asp:DropDownList ID="SchedRouteDDL" runat="server"></asp:DropDownList></td>
                        <th>Flown Route</th>
                        <td><asp:DropDownList ID="FlownRouteDDL" runat="server"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <th>Pilot</th>
                        <td><asp:DropDownList ID="PilotDDL" runat="server"></asp:DropDownList><asp:ImageButton ID="AddPilotButton" runat="server" style="position: relative; left: 7px; top: 3px;" ImageUrl="~/images/plus_blue.bmp" /></td>
                        <th>Depart Time</th>
                        <td><asp:TextBox ID="DepartTimeTB" runat="server"></asp:TextBox></td>
                        <th>Return Time</th>
                        <td><asp:TextBox ID="ReturnTimeTB" runat="server"></asp:TextBox></td>    
                    </tr>
                    <tr>
                        <th>Outside Temp</th>
                        <td><asp:TextBox ID="OutsideTempTB" runat="server"></asp:TextBox></td>
                        <th>Weather</th>
                        <td><asp:TextBox ID="WeatherTB" runat="server"></asp:TextBox></td>
                        <th>Total Accum. Hours</th>
                        <td><asp:Label ID="TotalHoursLB" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4" rowspan="6" style="padding:0; margin: 0; vertical-align: top;">
                            <div class="DataEntrySubTitle" >Liquid Fills</div>
                            <asp:DataGrid 
                                ID="LiquidFillDG"
                                runat="server"
                                DataKeyField="LiquidFillId"
                                BorderStyle = "solid"
                                bordercolor="Black"
                                font-names="Verdana, Arial"
                                Font-size="10pt"
                                cellpadding="5"
                                cellspacing="0"
                                ShowHeader="True"
                                ShowFooter="True"
                                GridLines="Both"
                                width="100%"
                                allowPaging="true"
                                PageSize="2"
                                AlternatingItemStyle-BackColor="Gainsboro"
                                OnItemCommand="LiquidFillDG_ItemCommand"
                                AutoGenerateColumns="False">

                                <HeaderStyle Font-Bold="True" Font-Size="10pt" BackColor="DarkGray" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <nobr>
                                                <asp:ImageButton CommandName="Edit" ID="EditButton" runat="server" ImageUrl="~/images/pencil_yellow.bmp" AlternateText="Edit" ToolTip="Edit Existing Entry" />
                                            </nobr>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <nobr>
                                                <asp:ImageButton CommandName="Save" ID="SaveButton" runat="server" ImageUrl="~/images/save.bmp" AlternateText="Save" ToolTip="Save Pending Changes" />
                                                <asp:ImageButton CommandName="Cancel" ID="CancelButton" runat="server" ImageUrl="~/images/cancel.bmp" AlternateText="Cancel" ToolTip="Cancel Pending Changes" />
                                            </nobr>
                                        </EditItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="LiquidFillIdLB" runat="server" Text='<%# Bind("LiquidFillId") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="LiquidFillIdLB" runat="server" Text='<%# Bind("LiquidFillId") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="LiquidGroupNameLB" runat="server" Text='<%# Bind("LiquidGroupName") %>' Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="LiquidGroupDDL" runat="server" DataTextField="StringValue" DataValueField="Id" SelectedValue='<%# Bind("LiquidGroupId") %>' DataSource="<%# GetLiquidGroupList() %>"  Width="100px"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="LiquidGroupDDL" runat="server" DataTextField="StringValue" DataValueField="Id" DataSource="<%# GetLiquidGroupList() %>"  Width="100px"></asp:DropDownList>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Sub-Type">
                                        <ItemTemplate>
                                            <asp:Label ID="LiquidTypeNameLB" runat="server" Text='<%# Bind("LiquidTypeName") %>' Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="LiquidTypeDDL" runat="server" DataTextField="StringValue" DataValueField="Id" SelectedValue='<%# Bind("LiquidTypeId") %>' DataSource="<%# GetLiquidTypeList() %>" Width="100px"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="LiquidTypeDDL" runat="server" DataTextField="StringValue" DataValueField="Id" DataSource="<%# GetLiquidTypeList() %>" Width="100px"></asp:DropDownList>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="LiquidAmtLB" runat="Server" Text='<%# Bind("LiquidAmt") %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="LiquidAmtTB" runat="server" Text='<%# Bind("LiquidAmt") %>' Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="LiquidAmtTB" runat="server" Text="" Width="50px"></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="LiquidUnitNameLB" runat="server" Text='<%# Bind("LiquidUnitName") %>' width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="LiquidUnitDDL" runat="server" DataTextField="StringValue" DataValueField="Id" SelectedValue='<%# Bind("LiquidUnitId") %>' DataSource="<%# GetLiquidUnitList() %>" Width="50px"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="LiquidUnitDDL" runat="server" DataTextField="StringValue" DataValueField="Id" DataSource="<%# GetLiquidUnitList() %>" Width="50px"></asp:DropDownList>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Hobbs">
                                        <ItemTemplate>
                                            <asp:Label ID="HobbsLB" runat="server" Text='<%# Bind("HobbsReading") %>' Width="67px"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="HobbsTB" runat="server" Text='<%# Bind("HobbsReading") %>' Width="67px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="HobbsTB" runat="server" Text="" Width="67px"></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                                <asp:ImageButton CommandName="Delete" ID="DeleteButton" runat="server" ImageUrl="~/images/x_red.bmp" AlternateText="Delete" ToolTip="Delete Existing Entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton CommandName="Add" ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" AlternateText="Add" ToolTip="Add New Entry" />
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                </Columns>
                                <AlternatingItemStyle BackColor="Gainsboro" />
                            </asp:DataGrid>
                        </td>
                        <td colspan="2" rowspan="6" style="text-align: center; padding: 0; margin: 0;">
                            <div class="DataEntrySubTitle" >Region Usage</div>
                            <asp:DataGrid
                                id="FlightRegionTimeDG"
                                runat="Server"
                                AutoGenerateColumns="False"
                                BorderStyle="Solid"
                                BorderColor="Black"
                                Font-Names="Verdana, Arial"
                                Font-Size="10pt"
                                CellPadding="5"
                                Cellspacing="0"
                                DataKeyField="FlightRegionTimeId"
                                ShowHeader="True"
                                ShowFooter="True"
                                width="100%"
                                AllowPaging="true"
                                PageSize="2"
                                AlternatingItemStyle-BackColor="Gainsboro"
                                OnItemCommand="FlightRegionTimeDG_ItemCommand">
                                <HeaderStyle Font-Bold="True" Font-Size="10pt" BackColor="DarkGray" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <nobr>
                                                <asp:ImageButton CommandName="Edit" ID="EditButton" runat="server" ImageUrl="~/images/pencil_yellow.bmp" AlternateText="Edit" ToolTip="Edit Existing Entry" />
                                            </nobr>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <nobr>
                                                <asp:ImageButton CommandName="Save" ID="SaveButton" runat="server" ImageUrl="~/images/save.bmp" AlternateText="Save" ToolTip="Save Pending Changes" />
                                                <asp:ImageButton CommandName="Cancel" ID="CancelButton" runat="server" ImageUrl="~/images/cancel.bmp" AlternateText="Cancel" ToolTip="Cancel Pending Changes" />
                                            </nobr>
                                        </EditItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" Height="20px" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="FlightRegionTimeIdLB" runat="server" Text='<%# Bind("FlightRegionTimeId") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="FlightRegionTimeIdLB" runat="server" Text='<%# Bind("FlightRegionTimeId") %>'></asp:Label>                                        
                                        </EditItemTemplate>
                                        <FooterTemplate>&nbsp;
                                        </FooterTemplate>
                                        <HeaderStyle Width="0px" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Region">
                                        <ItemTemplate>
                                            <asp:Label ID="RegionCodeLB" runat="server" Text='<%# Bind("RegionCode") %>' Width="50px" ></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="RegionDDL" runat="server" DataTextField="Code" DataValueField="Id" SelectedValue='<%# Bind("RegionId") %>' DataSource='<%# GetRegionList() %>' Width="50px"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="RegionDDL" runat="server" DataTextField="Code" DataValueField="Id" DataSource='<%# GetRegionList() %>' Width="50px"></asp:DropDownList>                                        
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="75px" />
                                        <ItemStyle HorizontalAlign="Center" Height="20px" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Hours">
                                        <ItemTemplate>
                                            <asp:Label ID="HoursLB" runat="server" Text='<%# Bind("Hours") %>' width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="HoursTB" runat="server" Text='<%# Bind("Hours") %>' Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="HoursTB" runat="server" Text="" Width="50px"></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="75px" />
                                        <ItemStyle HorizontalAlign="Center" Height="20px" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                                <asp:ImageButton CommandName="Delete" ID="DeleteButton" runat="server" ImageUrl="~/images/x_red.bmp" AlternateText="Delete" ToolTip="Delete Existing Entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton CommandName="Add" ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" AlternateText="Add" ToolTip="Add New Entry" />
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" Height="20px" />
                                    </asp:TemplateColumn>
                                </Columns>    
                                <AlternatingItemStyle BackColor="Gainsboro" />
                            </asp:DataGrid>            
                        </td>
                    </tr>
                </table>
                <div class="DataEntrySubTitle" >Pilot Comments</div>
                <div style="width: 100%">
                    <asp:DataGrid
                        id="FlightCommentDG"
                        runat="Server"
                        AutoGenerateColumns="False"
                        BorderStyle="Solid"
                        BorderColor="Black"
                        Font-Names="Verdana, Arial"
                        Font-Size="10pt"
                        CellPadding="5"
                        Cellspacing="0"
                        DataKeyField="FlightCommentId"
                        ShowHeader="True"
                        ShowFooter="True"
                        width="100%"
                        AllowPaging="true"
                        PageSize="2"
                        AlternatingItemStyle-BackColor="Gainsboro"
                        OnItemCommand="FlightCommentDG_ItemCommand">
                        <HeaderStyle Font-Bold="True" Font-Size="10pt" BackColor="DarkGray" ForeColor="White" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <nobr>
                                        <asp:ImageButton CommandName="Edit" ID="EditButton" runat="server" ImageUrl="~/images/pencil_yellow.bmp" AlternateText="Edit" ToolTip="Edit Existing Entry" />
                                    </nobr>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <nobr>
                                        <asp:ImageButton CommandName="Save" ID="SaveButton" runat="server" ImageUrl="~/images/save.bmp" AlternateText="Save" ToolTip="Save Pending Changes" />
                                        <asp:ImageButton CommandName="Cancel" ID="CancelButton" runat="server" ImageUrl="~/images/cancel.bmp" AlternateText="Cancel" ToolTip="Cancel Pending Changes" />
                                    </nobr>
                                </EditItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="FlightCommentIdLB" runat="server" Text='<%# Bind("FlightCommentId") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="FlightCommentIdLB" runat="server" Text='<%# Bind("FlightCommentId") %>'></asp:Label>                            
                                </EditItemTemplate>
                                <HeaderStyle Width="0px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Component">
                                <ItemTemplate>
                                    <asp:Label ID="ComponentNameLB" runat="server" Text='<%# Bind("ComponentName") %>' width="100px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ComponentDDL" runat="server" DataTextField="StringValue" DataValueField="Id" SelectedValue='<%# Bind("ComponentId") %>' DataSource='<%# GetAircraftComponentList() %>' Width="100px"></asp:DropDownList>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ComponentDDL" runat="server" DataTextField="StringValue" DataValueField="Id" DataSource='<%# GetAircraftComponentList() %>' Width="100px"></asp:DropDownList>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Issue Code">
                                <ItemTemplate>
                                    <asp:Label ID="IssueCodeLB" runat="server" Text='<%# Bind("IssueCode") %>' Width="100px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="IssueCodeDDL" runat="server" DataTextField="StringValue" DataValueField="Id" SelectedValue='<%# Bind("IssueCodeId") %>' DataSource='<%# GetIssueCodeList() %>' Width="100px"></asp:DropDownList>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="IssueCodeDDL" runat="server" DataTextField="StringValue" DataValueField="Id" DataSource='<%# GetIssueCodeList() %>' Width="100px"></asp:DropDownList>
                                </FooterTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Time">
                                <ItemTemplate>
                                    <asp:Label ID="CommentTimeLB" runat="server" Text='<%# Bind("CommentTime") %>' Width="100px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="CommentTimeTB" runat="server" Text='<%# Bind("CommentTime") %>' Width="100px"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="CommentTimeTB" runat="server" Text="" Width="100px"></asp:TextBox>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Test Observations and Comments">
                                <ItemTemplate>
                                    <asp:Label ID="PilotCommentLB" runat="server" Text='<%# Bind("PilotComment") %>' width="400px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="PilotCommentTB" runat="server" Text='<%# Bind("PilotComment") %>' style="width:400px; height: 90px;" TextMode="MultiLine"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="PilotCommentTB" runat="server" Text="" style="width:400px; height: 90px;" TextMode="MultiLine"></asp:TextBox>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="400px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Rating">
                                <ItemTemplate>
                                    <asp:Label ID="CommentRatingLB" runat="server" Text='<%# Bind("CommentRating") %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="CommentRatingDDL" runat="server" SelectedValue='<%# Bind("CommentRating") %>'>
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="CommentRatingDDL" runat="server" SelectedValue='<%# Bind("CommentRating") %>'>
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                        <asp:ImageButton CommandName="Delete" ID="DeleteButton" runat="server" ImageUrl="~/images/x_red.bmp" AlternateText="Delete" ToolTip="Delete Existing Entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:ImageButton CommandName="Add" ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" AlternateText="Add" ToolTip="Add New Entry" />
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                        </Columns>
                        <AlternatingItemStyle BackColor="Gainsboro" />
                    </asp:DataGrid>
                    <div style="width: 100%; text-align: center; padding: 5px;">
                        <asp:Button ID="SaveButton" runat="server" Text="Save" style="width: 75px; height: 30px;" tooltip="Save Pending Changes" />
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" style="width: 75px; height: 30px;" ToolTip="Cancel Pending Changes" />                        
                    </div>
                </div>
        
        </asp:View>
        
            <asp:View ID="AddPilotView" runat="server">
            
                <div class="DataEntryContainer" >
                    <div class="DataEntryTitle" >Add New Pilot</div>
                    <table class="DataEntryTable" cellpadding="10" cellspacing="0">
                        <colgroup>
                            <col width="16%" />
                            <col width="16%" />
                            <col width="17%" />
                            <col width="17%" />
                            <col width="16%" />
                            <col width="16%" />
                        </colgroup>
                        <tr>
                            <th>Pilot Name</th>
                            <td><asp:TextBox ID="PilotNameTB" runat="server"></asp:TextBox></td>
                            <th>Sort Order</th>
                            <td><asp:TextBox ID="PilotSortOrderTB" runat="server"></asp:TextBox></td>
                            <th>Is Active</th>
                            <td><asp:CheckBox ID="PilotIsActiveCheckBox" runat="server" /></td>
                        </tr>
                        <tr>
                            <th colspan="6" style="text-align: center;">
                                <asp:ImageButton ID="PilotSaveButton" runat="server" ImageUrl="~/images/save.bmp" AlternateText="Save New Pilot" />
                                &nbsp;
                                <asp:ImageButton ID="PilotCancelButton" runat="server" ImageUrl="~/images/cancel.bmp" AlternateText="Cancel Add of New Pilot" />
                            </th>
                        </tr>
                    </table> 
                </div>
        
            </asp:View>
          
        </asp:MultiView>
        
    </div>

    <asp:HiddenField ID="ProgIdHiddenField" runat="server" />
    <asp:HiddenField ID="LocationIdHiddenField" runat="server" />
    <asp:HiddenField ID="AircraftIdHiddenField" runat="server" />
    <asp:HiddenField ID="FlightIdHiddenField" runat="server" />
    <script type="text/javascript">
        $(function() {
            $(".datePickerTB").datepicker();
        });
    </script>

</asp:Content>
