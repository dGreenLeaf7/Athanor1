<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="FlightModify.aspx.vb" Inherits="FLTWebApp.FlightModify" 
    title="Flight Modify Page" %>
<asp:Content ID="FlightModifyContent" ContentPlaceHolderID="MainContent" runat="server">
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
    
    <div style="border: solid 2px black; padding: 0; margin: 0;">
    
        <asp:MultiView ID="ParentChildMultiview" runat="server" ActiveViewIndex="0">
        
            <asp:View ID="AddView" runat="server">
            
                <table cellpadding="5" cellspacing="0" width="800px" class="ModifyTable" border="solid 1px black">
                    <colgroup>
                        <col width="25%" />
                        <col width="25%" />
                        <col width="25%" />
                        <col width="25%" />
                    </colgroup>
                    <tr>
                        <th colspan="4" style="font-weight: bold; font-size: 1.5em; color: #FFFFFF; background-color: gray;">Flight Key Information</th>
                    </tr>
                    <tr>
                        <th>Location: </th>
                        <td><asp:DropDownList ID="LocationIdDDL" runat="server" AutoPostBack="true"></asp:DropDownList></td>
                        <th>Aircraft:</th>
                        <td><asp:DropDownList ID="AircraftIdDDL" runat="server"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <th>Flight Date:</th>
                        <td><asp:TextBox ID="FlightDateTB" runat="server" CssClass="datePickerTB"></asp:TextBox></td>
                        <th>Work Shift:</th>
                        <td><asp:DropDownList ID="WorkShiftDDL" runat="server"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <th colspan="4">
                            <asp:Button ID="NextButton" runat="server" text="Next" Width="75px" />
                            <asp:Button ID="EditButton" runat="server" Text="Edit" Width="75px" Visible="false" />
                            <asp:Button ID="RetryButton" runat="server" Text="Retry" Width="75px" Visible="false" />
                            <asp:Button ID="CancelButton1" runat="server" Text="Cancel" Width="75px" OnClick="CancelButton_Click" />
                        </th>
                    </tr>
                </table>
            
            </asp:View>
            
            <asp:View ID="EditView" runat="server">

                <asp:Table id="Table2" runat="server" cellpadding="5" cellspacing="0" width="800px" CssClass="ModifyTable" GridLines="Both">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell style="font-weight: bold; font-size: 1.5em; color: #FFFFFF; background-color:gray;">RDC Evaluation</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <table class="GridList" style="width:100%; border: solid 1px black;" cellpadding="5" cellspacing="1"  >
                                <tr>
                                    <th>Location: </th>
                                    <td><asp:DropDownList ID="LocationEditDDL" runat="server" AutoPostBack="true"></asp:DropDownList></td>
                                    <th>Aircraft:</th>
                                    <td><asp:DropDownList ID="AircraftEditDDL" runat="server"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <th>Flight Date:</th>
                                    <td><asp:TextBox ID="FlightDateEditTB" runat="server" CssClass="datePickerTB"></asp:TextBox></td>
                                    <th>Work Shift:</th>
                                    <td><asp:DropDownList ID="WorkShiftEditDDL" runat="server"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <th>Evaluator's Name</th>
                                    <td style="vertical-align:text-top">
                                        <asp:DropDownList ID="PilotDDL" runat="server"></asp:DropDownList>
                                        &nbsp;
                                        <asp:ImageButton id="AddPilotButton" runat="server" ImageUrl="~/images/table.bmp" tooltip="Add New Pilot" style="top:5px; position: relative" /></td>
                                    <th>Route</th>
                                    <td><asp:DropDownList ID="SchedRouteDDL" runat="server"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <th>Gate Depart Time</th>
                                    <td><asp:TextBox ID="DepartDateTimeTB" runat="server"></asp:TextBox></td>
                                    <th>Gate Return Time</th>
                                    <td><asp:TextBox ID="ReturnDateTimeTB" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <th>Airframe Hours Start</th>
                                    <td><asp:Label ID="AirframeHoursStartLB" runat="server"></asp:Label></td>
                                    <th>Airframe Hours End</th>
                                    <td><asp:Label ID="AirframeHoursEndLB" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <th>Hobbs Start</th>
                                    <td><asp:Label ID="HobbsStartLB" runat="server"></asp:Label></td>
                                    <th>Hobbs End</th>
                                    <td><asp:Label ID="HobbsEndLB" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <th>Used Meter Offset</th>
                                    <td><asp:Label ID="UsedMeterOffsetLB" runat="server"></asp:Label></td>
                                    <th>Total Flight Hours</th>
                                    <td><asp:Label id="TotalHoursLabel" runat="server"></asp:Label></td>
                                </tr>
                            </table>    
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                        <div>
                            <div id="tabs" class="tabs">
                                <ul>
                                    <li><a href="#tabs-1">PRE-TEST</a></li>
                                    <li><a href="#tabs-2">LIQUID FILLS</a></li>
                                    <li><a href="#tabs-3">COMMENTS</a></li>
                                    <li><a href="#tabs-4">ENGINE RUN</a></li>
                                    <li><a href="#tabs-5">POST-TEST</a></li>
                                    <li><a href="#tabs-6">REGION HOURS</a></li>
                                </ul>
                                <div id="tabs-1">
                                    <table cellpadding="5" cellspacing="0" width="100%" class="GridList" >
                                        <colgroup>
                                            <col width="8.33%" />
                                            <col width="8.33%" />
                                            <col width="8.33%" />
                                            <col width="8.33%" />
                                            <col width="8.33%" />
                                            <col width="8.33%" />
                                            <col width="8.33%" />
                                            <col width="8.33%" />
                                            <col width="8.33%" />
                                            <col width="8.33%" />
                                            <col width="8.33%" />
                                            <col width="8.33%" />
                                        </colgroup>
                                        <tr>
                                            <th colspan="3">Start Usage</th>
                                            <td colspan="3"><asp:TextBox ID="HobbsStartTB" runat="server" AutoPostBack="true"></asp:TextBox></td>
                                            <th colspan="3">Start Engine Hours</th>
                                            <td colspan="3"><asp:TextBox ID="EngineHoursStartTB" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <th colspan="3">Engine Oil Level</th>
                                            <td colspan="3">
                                                <asp:DropDownList ID="OilLevelIndicatorStartDDL" runat="server">
                                                    <asp:ListItem Selected="true" Value="0">ADD</asp:ListItem>
                                                    <asp:ListItem Value="1">1/4</asp:ListItem>
                                                    <asp:ListItem Value="2">1/2</asp:ListItem>
                                                    <asp:ListItem Value="3">3/4</asp:ListItem>
                                                    <asp:ListItem Value="4">FULL</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <th colspan="3">Hydraulic Level</th>
                                            <td colspan="3">
                                                <asp:DropDownList ID="HydraulicLevelIndicatorStart" runat="server">
                                                    <asp:ListItem Selected="true" Value="0">EMPTY</asp:ListItem>
                                                    <asp:ListItem Value="1">1/4</asp:ListItem>
                                                    <asp:ListItem Value="2">1/2</asp:ListItem>
                                                    <asp:ListItem Value="3">3/4</asp:ListItem>
                                                    <asp:ListItem Value="4">FULL</asp:ListItem>
                                                </asp:DropDownList>                    
                                            </td>
                                        </tr>
                                        <tr>
                                            <th colspan="2">FTI OK:</th>
                                            <td colspan="2">
                                                <asp:DropDownList ID="PreFlightFtiOKDDL" runat="server">
                                                    <asp:ListItem Selected="true" Value="0">--</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="-1">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <th colspan="2">Telemetry OK:</th>
                                            <td colspan="2">
                                                <asp:DropDownList ID="PreFlightTelemetryOKDDL" runat="server">
                                                    <asp:ListItem Selected="true" Value="0">--</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="-1">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <th colspan="2">Telemetry Check Complete:</th>
                                            <td colspan="2">
                                                <asp:DropDownList ID="PreFlightTelemetryCheckCompleteDDL" runat="server">
                                                    <asp:ListItem Selected="true" Value="0">--</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="-1">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th colspan="6" style="text-align: left;">Weight Tracking</th>
                                            <th colspan="2" style="text-align: left;">Takeoff Weight</th>
                                            <td colspan="4" style="text-align: left;"><asp:Label ID="TakeoffWeightLB" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <th colspan="2">Airframe</th>
                                            <td colspan="4"><asp:DropDownList ID="AirframeDDL" runat="server" style="width: 250px;"></asp:DropDownList></td>
                                            <th colspan="2">Cargo 1</th>
                                            <td colspan="4"><asp:DropDownList ID="Cargo1DDL" runat="server" style="width: 250px;"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <th colspan="2">Ballast</th>
                                            <td colspan="4"><asp:DropDownList ID="BallastDDL" runat="server" style="width: 250px;"></asp:DropDownList></td>
                                            <th colspan="2">Cargo 2</th>
                                            <td colspan="4"><asp:DropDownList ID="Cargo2DDL" runat="server" style="width: 250px;"></asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="tabs-2">
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
                                                    width="800px"
                                                    AlternatingItemStyle-BackColor="Gainsboro"
                                                    OnItemCommand="LiquidFillDG_ItemCommand"
                                                    AutoGenerateColumns="False">

                                                    <HeaderStyle Font-Bold="True" Font-Size="10pt" BackColor="darkgray" ForeColor="White" />
                                                    <Columns>
                                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
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
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LiquidFillIdLB" runat="server" Text='<%#: Eval("LiquidFillId")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="LiquidFillIdLB" runat="server" Text='<%#: Bind("LiquidFillId") %>'></asp:Label>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Type" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LiquidGroupNameLB" runat="server" Text='<%#: Eval("LiquidGroupName")%>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="LiquidGroupDDL" runat="server" DataTextField="StringValue" DataValueField="Id" SelectedValue='<%# Bind("LiquidGroupId") %>' DataSource="<%# GetLiquidGroupList() %>"  Width="100px"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="LiquidGroupDDL" runat="server" DataTextField="StringValue" DataValueField="Id" DataSource="<%# GetLiquidGroupList() %>"  Width="100px"></asp:DropDownList>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Sub-Type" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LiquidTypeNameLB" runat="server" Text='<%#: Eval("LiquidTypeName")%>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="LiquidTypeDDL" runat="server" DataTextField="StringValue" DataValueField="Id" SelectedValue='<%# Bind("LiquidTypeId") %>' DataSource="<%# GetLiquidTypeList() %>" Width="100px"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="LiquidTypeDDL" runat="server" DataTextField="StringValue" DataValueField="Id" DataSource="<%# GetLiquidTypeList() %>" Width="100px"></asp:DropDownList>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Amt" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LiquidAmtLB" runat="Server" Text='<%#: Eval("LiquidAmt")%>' Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="LiquidAmtTB" runat="server" Text='<%#: Bind("LiquidAmt") %>' Width="50px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="LiquidAmtTB" runat="server" Text="" Width="50px"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Unit" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LiquidUnitNameLB" runat="server" Text='<%#: Eval("LiquidUnitName")%>' width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="LiquidUnitDDL" runat="server" DataTextField="StringValue" DataValueField="Id" SelectedValue='<%# Bind("LiquidUnitId") %>' DataSource="<%# GetLiquidUnitList() %>" Width="50px"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="LiquidUnitDDL" runat="server" DataTextField="StringValue" DataValueField="Id" DataSource="<%# GetLiquidUnitList() %>" Width="50px"></asp:DropDownList>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Hobbs" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="HobbsLB" runat="server" Text='<%#: Eval("HobbsReading")%>' Width="67px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="HobbsTB" runat="server" Text='<%#: Bind("HobbsReading") %>' Width="67px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="HobbsTB" runat="server" Text="" Width="67px"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Approver" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" FooterStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ApproverLB" runat="server" Text='<%#: Eval("LiquidFillApprover")%>' Width="83px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="ApproverTB" runat="server" Text='<%#: Bind("LiquidFillApprover") %>' Width="83px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="ApproverTB" runat="server" width="83px"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Location" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LocationLB" runat="server" Text='<%#: Eval("LiquidFillLocation")%>' Width="200px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="LocationTB" runat="server" Text='<%#: Bind("LiquidFillLocation") %>' Width="200px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="LocationTB" runat="server" Text="" Width="200px"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                    <asp:ImageButton CommandName="Delete" ID="DeleteButton" runat="server" ImageUrl="~/images/x_red.bmp" AlternateText="Delete" ToolTip="Delete Existing Entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton CommandName="Add" ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" AlternateText="Add" ToolTip="Add New Entry" />
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                </div>
                                <div id="tabs-3">
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
                                                    width="800"
                                                    AlternatingItemStyle-BackColor="Gainsboro"
                                                    OnItemCommand="FlightCommentDG_ItemCommand">
                                                    <HeaderStyle Font-Bold="True" Font-Size="10pt" BackColor="darkGray" ForeColor="White" />
                                                    <Columns>
                                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
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
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="" HeaderStyle-Width="0" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="FlightCommentIdLB" runat="server" Text='<%#: Eval("FlightCommentId")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="FlightCommentIdLB" runat="server" Text='<%#: Bind("FlightCommentId") %>'></asp:Label>                            
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Component" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ComponentNameLB" runat="server" Text='<%#: Eval("ComponentName")%>' width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ComponentDDL" runat="server" DataTextField="StringValue" DataValueField="Id" SelectedValue='<%# Bind("ComponentId") %>' DataSource='<%# GetAircraftComponentList() %>' Width="100px"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="ComponentDDL" runat="server" DataTextField="StringValue" DataValueField="Id" DataSource='<%# GetAircraftComponentList() %>' Width="100px"></asp:DropDownList>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Issue Code" HeaderStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="IssueCodeLB" runat="server" Text='<%#: Eval("IssueCode")%>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="IssueCodeDDL" runat="server" DataTextField="StringValue" DataValueField="Id" SelectedValue='<%# Bind("IssueCodeId") %>' DataSource='<%# GetIssueCodeList() %>' Width="100px"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="IssueCodeDDL" runat="server" DataTextField="StringValue" DataValueField="Id" DataSource='<%# GetIssueCodeList() %>' Width="100px"></asp:DropDownList>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Time" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="CommentTimeLB" runat="server" Text='<%#: Eval("CommentTime")%>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="CommentTimeTB" runat="server" Text='<%#: Bind("CommentTime") %>' Width="100px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="CommentTimeTB" runat="server" Text="" Width="100px"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Test Observations and Comments" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="PilotCommentLB" runat="server" Text='<%#: Eval("PilotComment")%>' width="400px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="PilotCommentTB" runat="server" Text='<%#: Bind("PilotComment") %>' style="width:400px; height: 90px;" TextMode="MultiLine"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="PilotCommentTB" runat="server" Text="" style="width:400px; height: 90px;" TextMode="MultiLine"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Rating" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="CommentRatingLB" runat="server" Text='<%#: Eval("CommentRating")%>' Width="50px"></asp:Label>
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
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                    <asp:ImageButton CommandName="Delete" ID="DeleteButton" runat="server" ImageUrl="~/images/x_red.bmp" AlternateText="Delete" ToolTip="Delete Existing Entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton CommandName="Add" ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" AlternateText="Add" ToolTip="Add New Entry" />
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                </div>
                                <div id="tabs-4">
                                                <asp:DataGrid 
                                                    ID="FlightEngineRunDG"
                                                    runat="server"
                                                    AutoGenerateColumns="False"
                                                    BorderStyle="Solid"
                                                    BorderColor="Black"
                                                    Font-Names="Verdana, Arial"
                                                    Font-Size="10pt"
                                                    CellPadding="5"
                                                    Cellspacing="0"
                                                    DataKeyField="FlightEngineRunId"
                                                    ShowHeader="True"
                                                    ShowFooter="True"
                                                    width="800"
                                                    AlternatingItemStyle-BackColor="Gainsboro">
                                                    <HeaderStyle Font-Bold="True" Font-Size="10pt" BackColor="darkGray" ForeColor="White" />
                                                    <Columns>
                                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
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
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="" HeaderStyle-Width="0" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="FlightEngineRunIdLB" runat="server" Visible="False" Text='<%#: Eval("FlightEngineRunId")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="FlightEngineRunIdLB" runat="server" Visible="False" Text='<%#: Bind("FlightEngineRunId") %>'></asp:Label>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="RUN START TIME" HeaderStyle-Width="200px" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="RunStartTimeLB" runat="server" Text='<%#: Eval("RunStartTime")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="RunStartTimeTB" runat="server" Text='<%#: Bind("RunStartTime") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="RunStartTimeTB" runat="server" Text=""></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="RUN END TIME" HeaderStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="RunEndTimeLB" runat="server" Text='<%#: Eval("RunEndTime")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="RunEndTimeTB" runat="server" Text='<%#: Bind("RunEndTime") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="RunEndTimeTB" runat="server" Text=""></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="ENGINE SPEED (RPM)" HeaderStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="EngineSpeedLB" runat="server" Text='<%#: Eval("EngineSpeed")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="EngineSpeedTB" runat="server" Text='<%#: Bind("EngineSpeed") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="EngineSpeedTB" runat="server" Text=""></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                    <asp:ImageButton CommandName="Delete" ID="DeleteButton" runat="server" ImageUrl="~/images/x_red.bmp" AlternateText="Delete" ToolTip="Delete Existing Entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton CommandName="Add" ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" AlternateText="Add" ToolTip="Add New Entry" />
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                </div>
                                <div id="tabs-5">
                                                <table class="GridList" cellpadding="5" cellspacing="0" style="width: 100%" >
                                                    <tr>
                                                        <th>End Usage</th>
                                                        <td><asp:TextBox ID="HobbsEndTB" runat="server" AutoPostBack="true" ></asp:TextBox></td>
                                                        <th>End Engine Hours</th>
                                                        <td><asp:TextBox ID="EngineHoursEndTB" runat="server"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <th>Meter Unit</th>
                                                        <td><asp:Label ID="MeterUnitLB" runat="server"></asp:Label></td>
                                                        <th colspan="2">NACELLE SYSTEM CHECK</th>
                                                    </tr>
                                                    <tr>
                                                        <th>FTI OK</th>
                                                        <td>
                                                            <asp:DropDownList ID="PostFlightFtiOKDDL" runat="server">
                                                                <asp:ListItem Selected="true" Value="0">--</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="-1">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <th>Carbon/Soot Buildup</th>
                                                        <td>
                                                            <asp:DropDownList ID="CarbonSootBuildupDDL" runat="server">
                                                                <asp:ListItem Selected="true" Value="0">--</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="-1">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>Telemetry Check Complete</th>
                                                        <td>
                                                            <asp:DropDownList id="PostFlightTelemetryCheckCompleteDDL" runat="server">
                                                                <asp:ListItem Selected="true" Value="0">--</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="-1">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <th>Leaks or Discoloration</th>
                                                        <td>
                                                            <asp:DropDownList ID="LeaksOrDiscolorationDDL" runat="server">
                                                                <asp:ListItem Selected="true" Value="0">--</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="-1">No</asp:ListItem>                            
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                </div>
                                <div id="tabs-6" style="text-align: center">
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
                                        width="400"
                                        AlternatingItemStyle-BackColor="Gainsboro"
                                        OnItemCommand="FlightRegionTimeDG_ItemCommand">
                                        <HeaderStyle Font-Bold="True" Font-Size="10pt" BackColor="darkGray" ForeColor="White" />
                                        <Columns>
                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
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
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="" HeaderStyle-Width="0" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="FlightRegionTimeIdLB" runat="server" Text='<%#: Eval("FlightRegionTimeId")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="FlightRegionTimeIdLB" runat="server" Text='<%#: Bind("FlightRegionTimeId") %>'></asp:Label>                                        
                                                </EditItemTemplate>
                                                <FooterTemplate>&nbsp;
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Region" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="RegionCodeLB" runat="server" Text='<%#: Eval("RegionCode")%>' Width="50px" ></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="RegionDDL" runat="server" DataTextField="Code" DataValueField="Id" SelectedValue='<%# Bind("RegionId") %>' DataSource='<%# GetRegionList() %>' Width="50px"></asp:DropDownList>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="RegionDDL" runat="server" DataTextField="Code" DataValueField="Id" DataSource='<%# GetRegionList() %>' Width="50px"></asp:DropDownList>                                        
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Hours" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="HoursLB" runat="server" Text='<%#: Eval("Hours")%>' width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="HoursTB" runat="server" Text='<%#: Bind("Hours") %>' Width="100px"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="HoursTB" runat="server" Text="" Width="100px"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                        <asp:ImageButton CommandName="Delete" ID="DeleteButton" runat="server" ImageUrl="~/images/x_red.bmp" AlternateText="Delete" ToolTip="Delete Existing Entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton CommandName="Add" ID="AddButton" runat="server" ImageUrl="~/images/plus_blue.bmp" AlternateText="Add" ToolTip="Add New Entry" />
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>    
                                    </asp:DataGrid>
                                </div>
                            </div>
                        </div>
                        
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center">
                            <asp:Button ID="SaveButton" runat="server" Text="Save" style="width: 75px; height: 30px;" tooltip="Save Pending Changes" />
                            <asp:Button ID="CancelButton" runat="server" Text="Cancel" style="width: 75px; height: 30px;" ToolTip="Cancel Pending Changes" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:View>
            
            <asp:View ID="AddPilotView" runat="server">
            
                <h3>Add New Pilot</h3>
                <table cellpadding="5" cellspacing="0" class="childtable" >
                    <tr>
                        <th>Pilot Name</th>
                        <td><asp:TextBox ID="PilotNameTB" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>Sort Order</th>
                        <td><asp:TextBox ID="PilotSortOrderTB" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>Is Active</th>
                        <td><asp:CheckBox ID="PilotIsActiveCheckBox" runat="server" /></td>
                    </tr>
                    <tr>
                        <th colspan="2" style="text-align: center;">
                            <asp:ImageButton ID="PilotSaveButton" runat="server" ImageUrl="~/images/save.bmp" AlternateText="Save New Pilot" />
                            &nbsp;
                            <asp:ImageButton ID="PilotCancelButton" runat="server" ImageUrl="~/images/cancel.bmp" AlternateText="Cancel Add of New Pilot" />
                        </th>
                    </tr>
                </table> 
            
            </asp:View>
        
        </asp:MultiView>
        
    </div>

    <asp:HiddenField ID="ProgIdHiddenField" runat="server" />
    <asp:HiddenField ID="LocationIdHiddenField" runat="server" />
    <asp:HiddenField ID="AircraftIdHiddenField" runat="server" />
    <asp:HiddenField ID="FlightIdHiddenField" runat="server" />
    <asp:HiddenField ID="AircraftEngineHoursCurrentHF" runat="server" />

    <script type="text/javascript">
        $(function() {
            $(".datePickerTB").datepicker();
        });
    </script>
    <script type="text/javascript">
        $(function() {
            $("#tabs").tabs({
                show: function() {
                    var selIdx = $('#tabs').tabs('option','selected');
                    $("#<%= TabIndexHF.ClientId %>").val(selIdx);
                },
                selected: <%= TabIndexHF.Value %>
            });
        });
    </script>
    <asp:HiddenField id="TabIndexHF" runat="server" Value="0" />
</asp:Content>
