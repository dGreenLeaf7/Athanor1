<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="AircraftModify.aspx.vb" Inherits="FLTWebApp.AircraftModify" 
    title="Aircraft Modify" %>
<asp:Content ID="AircraftModifyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Fleet</h2>
    <h3><asp:Label ID="AddEditTitleLabel" runat="server"></asp:Label></h3>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
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

    <table class="GridList" cellpadding="5" cellspacing="0" style="border: solid 1px black;" >
        <tr>
            <th>MSN</th>
            <th>ShipNbr</th>
            <th>Serial Nbr</th>
            <th>Tail Nbr</th>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="MSNTB" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="ShipNbrTB" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="SerialNbrTB" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="RegistrationTB" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>Manufacturer</th>
            <th>Model</th>
            <th>Color</th>
            <th>Aircraft Status</th>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ManufacturerDDL" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="ModelDDL" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="LiveryColorTB" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="AircraftStatusDDL" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th colspan="3">Description</th>
            <th>Current Engine Hours</th>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="DescriptionTB" runat="server"  style="width:99%"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="AircraftEngineHoursCurrentTB" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>Location</th>
            <th>Engineer</th>
            <th>Mechanic</th>
            <th>Combined Weight</th>
        </tr>
        <tr>
            <td>
                <asp:Label ID="CurrentLocationLB" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="EngineerDDL" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="MechanicDDL" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="TakeoffWeightDDL" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Starting Dash Reading</th>
            <th>Current Meter</th>
            <th>Current Meter Offset</th>
            <th>Meter Units</th>
        </tr>
        <tr>
            <td>
                <asp:Label ID="MeterStartLB" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="MeterCurrentLB" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="CurrentMeterOffsetLB" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="MeterUnitsLB" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Est End of Test</th>
            <th>Act End of Test</th>
            <th>Tot Req'd Test Hours</th>
            <th>Current Test Hours</th>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="EstEndOfTestingTB" runat="server" CssClass="datePickerTB"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="ActEndOfTestingTB" runat="server" CssClass="datePickerTB"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="TotalRequiredHoursTB" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="CurrentHoursLB" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100%;"  colspan="4">
                <div id="tabs" class="tabs" >
                    <ul>
                        <li><a href="#tabs-1">Locations</a></li>
                        <li><a href="#tabs-2">Flights</a></li>
                        <li><a href="#tabs-3">Work Orders</a></li>
                        <li><a href="#tabs-4">Configurations</a></li>
                        <li><a href="#tabs-5">Tests</a></li>
                        <li><a href="#tabs-6">Comments</a></li>
                    </ul>
                    <div id="tabs-1">
                                    <asp:DataGrid
                                        cssclass="ChildGridView"
                                        ID="AircraftLocationsDG"
                                        runat="server"
                                        autogenerateColumns="false"
                                        GridLines="both"
                                        DataKeyField="AircraftLocationId"
                                        BorderStyle="Solid"
                                        BorderColor="Black"
                                        Cellpadding="4"
                                        Font-Names="Verdana, Arial"
                                        ShowHeader="True"
                                        ShowFooter="True"
                                        AlternatingItemStyle-BackColor="Gainsboro"
                                        Font-size="10pt"
                                        height="100%">
                                        <HeaderStyle Font-Bold="true" Font-Size="10pt" BackColor="darkgray" ForeColor="White" />
                                        <Columns>
                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="center" FooterStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <nobr >
                                                        <asp:ImageButton ID="EditButton" runat="Server" CommandName="edit" imageurl="~/images/pencil_yellow.bmp" ToolTip="Edit this entry" />
                                                    </nobr>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <nobr>
                                                        <asp:ImageButton ID="SaveButton" runat="server" CommandName="save" imageurl="~/images/save.bmp" ToolTip="Save pending changes" />
                                                        <asp:ImageButton ID="CancelButton" runat="server" CommandName="cancel" imageurl="~/images/cancel.bmp" tooltip="Cancel pending changes" />
                                                    </nobr>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="" HeaderStyle-width="0" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="AircraftLocationIdLB" runat="server" Text='<%#: Eval("AircraftLocationId")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="AircraftLocationIdLB" runat="server" Text='<%#: Bind("AircraftLocationId")%>'></asp:Label>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Location" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label id="LocationNameLB" runat="server" text='<%#: Eval("LocationName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="LocationDDL" runat="server" Width="150px" DataTextField="LocationName" DataValueField="LocactionId" DataSource='<%# GetLocations(False) %>' SelectedValue='<%# Bind("LocationId") %>' ></asp:DropDownList>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="LocationDDL" runat="server" Width="150px" DataTextField="LocationName" DataValueField="LocactionId" DataSource='<%# GetLocations(True) %>' SelectedValue='<%# Bind("LocationId") %>' ></asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Arrival Date" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="ArrivalDateLB" runat="server" Width="80px" Text='<%#: Eval("ArrivalDate")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="ArrivalDateTB" runat="server" Width="80px" Text='<%#: Bind("ArrivalDate")%>' CssClass="datePickerTB"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="ArrivalDateTB" runat="server" Width="80px" Text='<%#: Bind("ArrivalDate")%>' CssClass="datePickerTB"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Req Hours" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="RequiredUsageLB" runat="server" Width="75px" Text='<%#: Eval("RequiredUsage")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="RequiredUsageTB" runat="server" Width="75px" Text='<%#: Bind("RequiredUsage") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="RequiredUsageTB" runat="server" Width="75px" Text='<%#: Bind("RequiredUsage") %>'></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Accum Hours" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="AccumUsageLB" runat="server" Width="75px" Text='<%#: Eval("AccumUsage")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="AccumUsageLB" runat="server" Width="75px" Text='<%#: Bind("AccumUsage") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <FooterTemplate>&nbsp;
                                                </FooterTemplate>                                        
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Est Test End" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="EstEndOfTestLB" runat="server" Width="75px" Text='<%#: Eval("EstEndOfTest")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="EstEndOfTestTB" runat="server" Width="75px" Text='<%#: Bind("EstEndOfTest") %>' CssClass="datePickerTB"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="EstEndOfTestTB" runat="server" Width="75px" Text='<%#: Bind("EstEndOfTest") %>' CssClass="datePickerTB"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Act Test End" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="ActEndOfTestLB" runat="server" Width="75px" Text='<%#: Eval("ActEndOfTest")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="ActEndOfTestTB" runat="server" Width="75px" Text='<%#: Bind("ActEndOfTest") %>' CssClass="datePickerTB"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="ActEndOfTestTB" runat="server" Width="75px" Text='<%#: Bind("ActEndOfTest") %>' CssClass="datePickerTB"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Current Location" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CurrentLocationCB" runat="server" Checked='<%# Bind("LastLocation")%>' Enabled="false" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="CurrentLocationCB" runat="server" Checked='<%# bind("LastLocation") %>' Enabled="true" />
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:CheckBox ID="CurrentLocationCB" runat="server" Checked='<%# bind("LastLocation") %>' Enabled="true" />
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="center" FooterStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                        <asp:ImageButton ID="DeleteButton" runat="server" CommandName="delete" imageurl="~/images/x_red.bmp" ToolTip="Delete this entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="AddButton" runat="server" CommandName="add" ImageUrl="~/images/plus_blue.bmp" ToolTip="AddNew Entry" />
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                    </div>
                    <div id="tabs-2">
                                    <asp:GridView 
                                        CssClass="ChildGridView" 
                                        ID="AircraftFlightsGridView" 
                                        runat="server" 
                                        gridlines="both"
                                        style="width: 800px; border: solid 1px black;"
                                        AutoGenerateColumns="False" 
                                        DataKeyNames="FlightId"
                                        AllowPaging = "true"
                                        AlternatingRowStyle-BackColor="Gainsboro"
                                        EmptyDataText="No Records Retrieved"
                                        height="100%">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="SelectFlightsButton" runat="server" CommandName="Select" ImageUrl="~/images/table.bmp" AlternateText="Select" ToolTip="View Flight Report" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FlightId" HeaderText="FlightId" InsertVisible="False" ReadOnly="True"
                                                SortExpression="FlightId" Visible="False" />
                                            <asp:BoundField DataField="LocationName" HeaderText="Location" SortExpression="LocationName" />
                                            <asp:BoundField DataField="FlightDate" HeaderText="Date" SortExpression="FlightDate" />
                                            <asp:BoundField DataField="WorkShift" HeaderText="Work Shift" SortExpression="WorkShift" />
                                            <asp:BoundField DataField="AirframeHoursStart" HeaderText="Start Hours" SortExpression="AirframeHoursstart" />
                                            <asp:BoundField DataField="AirframeHoursEnd" HeaderText="End Hours" SortExpression="AirframeHoursEnd" />
                                            <asp:BoundField DataField="FlightUsage" HeaderText="Usage" SortExpression="Usage" />
                                        </Columns>
                                    </asp:GridView>
                    </div>
                    <div id="tabs-3">
                                    <asp:GridView 
                                        CssClass="ChildGridView" 
                                        ID="AircraftWorkOrdersGridView" 
                                        runat="server" 
                                        GridLines="Both"
                                        style="width: 800px; border: solid 1px black;"
                                        AutoGenerateColumns="False" 
                                        AllowPaging="true"
                                        DataKeyNames="WorkOrderId" 
                                        AlternatingRowStyle-BackColor="Gainsboro"
                                        EmptyDataText="No Records Retrieved"
                                        height="100%">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="SelectWorkorderButton" runat="server" CommandName="Select" ImageUrl="~/images/table.bmp" AlternateText="Select" ToolTip="View Work Order" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="WorkOrderId" HeaderText="WorkOrderId" InsertVisible="False"
                                                ReadOnly="True" SortExpression="WorkOrderId" Visible="False" />
                                            <asp:BoundField DataField="LocationName" HeaderText="Location" SortExpression="LocationName" />
                                            <asp:BoundField DataField="WorkOrderNbr" HeaderText="WO Nbr" SortExpression="WorkOrderNbr" />
                                            <asp:BoundField DataField="FISComplaintNbr" HeaderText="FIS Complaint Nbr" SortExpression="FISComplaintNbr" />
                                            <asp:BoundField DataField="SquawkNbr" HeaderText="SquawkNbr" SortExpression="SquawkNbr" />
                                            <asp:BoundField DataField="RequestDate" HeaderText="Req Date" SortExpression="RequestDate" />
                                            <asp:BoundField DataField="DueDate" HeaderText="Due Date" SortExpression="DueDate" />
                                            <asp:BoundField DataField="FinishDate" HeaderText="Finish Date" SortExpression="FinishDate" />
                                            <asp:BoundField DataField="WorkOrderStatus" HeaderText="Status" SortExpression="WorkOrderStatus" />
                                        </Columns>
                                    </asp:GridView>
                    </div>
                    <div id="tabs-4">
                                    <asp:MultiView ID="AircraftConfigurationsMultiView" runat="server" ActiveViewIndex="0">
                                        <asp:View 
                                            ID="AircraftConfigsListView" 
                                            runat="server"
                                            >
                                            <div>
                                                <div>
                                                    <asp:GridView 
                                                        ID="AircraftConfigGV" 
                                                        runat="server"
                                                        gridlines="both"
                                                        style="width: 800px; border: solid 1px black;"
                                                        AutoGenerateColumns="false"
                                                        DataKeyNames="AircraftConfigurationId"
                                                        AllowPaging="True"
                                                        AlternatingRowStyle-BackColor="Gainsboro"
                                                        EmptyDataText="No Records Retrieved">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="EditAircraftConfigButton" runat="server" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit" CommandName="Select" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="AircraftConfigurationIdLB" runat="server" Text='<%#: Bind("AircraftConfigurationId")%>'></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="AircraftConfigurationIdLB" runat="server" Text='<%#: Eval("AircraftConfigurationId")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ConfigurationStartDate" HeaderText="Start Date" />
                                                            <asp:BoundField DataField="ConfigurationEndDate" HeaderText="End Date" />
                                                            <asp:BoundField DataField="ConfigurationMeterStart" HeaderText="Start Hours" />
                                                            <asp:BoundField DataField="ConfigurationMeterEnd" HeaderText="End Hours" />
                                                            <asp:BoundField DataField="ConfigurationMeterOffset" HeaderText="Hours Offset" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div style="text-align: left; padding: 5px; margin: 0px;">
                                                    <asp:ImageButton ID="AddAircraftConfigButton" runat="Server" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add New Entry" />
                                                </div>
                                            </div>
                                        </asp:View>
                                        <asp:View ID="AircraftConfigsModifyView" runat="server">
                                            <asp:HiddenField ID="AircraftConfigurationIdHF" runat="server" />
                                            <table cellpadding="5px" cellspacing="0" class="GridList">
                                                <colgroup>
                                                    <col width="25%" />
                                                    <col width="25%" />
                                                    <col width="25%" />
                                                    <col width="25%" />
                                                </colgroup>
                                                 <tr>
                                                    <th>Start Date</th>
                                                    <th>End Date</th>
                                                    <th>Model</th>
                                                    <th>&nbsp;</th>
                                                </tr>
                                                <tr>
                                                    <td><asp:TextBox ID="ConfigurationStartDateTB" runat="server" CssClass="datePickerTB" ></asp:TextBox></td>
                                                    <td><asp:TextBox ID="ConfigurationEndDateTB" runat="server" enabled="False" ></asp:TextBox></td>
                                                    <td><asp:DropDownList ID="ConfigModelDDL" runat="server"></asp:DropDownList></td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <th>Start Meter</th>
                                                    <th>End Meter</th>
                                                    <th>Meter Offset</th>
                                                    <th>Meter Units</th>
                                                </tr>
                                                <tr>
                                                    <td><asp:TextBox ID="ConfigurationMeterStartTB" runat="server" ></asp:TextBox></td>
                                                    <td><asp:TextBox ID="ConfigurationMeterEndTB" runat="server" ></asp:TextBox></td>
                                                    <td><asp:TextBox ID="ConfigurationMeterOffsetTB" runat="server"></asp:TextBox></td>
                                                    <td><asp:DropDownList ID="ConfigMeterUnits" runat="server"></asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <th colspan="4" style="text-align: center;">Description</th>
                                                </tr>
                                                <tr>
                                                    <td colspan="4"><asp:TextBox ID="ConfigurationDescriptionTB" runat="server" style="width: 100%" ></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <th>Engine Type</th>
                                                    <th>Engine Maturity</th>
                                                    <th>Engine Emissions</th>
                                                    <th>Engine Serial Nbr</th>
                                                </tr>
                                                <tr>
                                                    <td><asp:DropDownList ID="EngineTypeDDL" runat="server" ></asp:DropDownList></td>
                                                    <td><asp:DropDownList ID="EngineMaturityDDL" runat="server" ></asp:DropDownList></td>
                                                    <td><asp:DropDownList ID="EngineEmissionDDL" runat="server" ></asp:DropDownList></td>
                                                    <td><asp:TextBox ID="EngineSerialNbrTB" runat="server" ></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <th>Trans Type</th>
                                                    <th>Propeller</th>
                                                    <th>Trans Serial Nbr</th>
                                                    <th>&nbsp;</th>
                                                </tr>
                                                <tr>
                                                    <td><asp:DropDownList ID="PropellerTypeDDL" runat="Server"  ></asp:DropDownList></td>
                                                    <td><asp:TextBox ID="PropellerModelNbrTB" runat="server" ></asp:TextBox></td>
                                                    <td><asp:TextBox ID="PropellerSerialNbrTB" runat="server" ></asp:TextBox></td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <th>APU Type</th>
                                                    <th>APU Maturity</th>
                                                    <th>APU Emissions</th>
                                                    <th>APU Serial Nbr</th>
                                                </tr>
                                                <tr>
                                                    <td><asp:DropDownList ID="APUTypeDDL" runat="server" ></asp:DropDownList></td>
                                                    <td><asp:DropDownList ID="APUMaturityDDL" runat="server" ></asp:DropDownList></td>
                                                    <td><asp:DropDownList ID="APUEmissionDDL" runat="server" ></asp:DropDownList></td>
                                                    <td><asp:TextBox ID="APUSerialNbrTB" runat="server" Text=''></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <th>Gear Front</th>
                                                    <th>Gear Rear</th>
                                                    <th>AvionicsVersion</th>
                                                    <th>Emission Level</th>
                                                </tr>
                                                <tr>
                                                    <td><asp:TextBox ID="NoseGearTB" runat="server" ></asp:TextBox></td>
                                                    <td><asp:TextBox ID="MainGearTB" runat="server" ></asp:TextBox></td>
                                                    <td><asp:TextBox ID="AvionicsVersionTB" runat="server" ></asp:TextBox></td>
                                                    <td><asp:TextBox ID="EmissionLevelTB" runat="server" ></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="width: 100%; text-align:center " >
                                                        <asp:ImageButton ID="SaveAircraftConfigButton" runat="server" ImageUrl="~/images/save.bmp" ToolTip="Apply Aircraft Configuration Changes" />
                                                        <asp:ImageButton ID="CancelAircraftConfigButton" runat="server" ImageUrl="~/images/cancel.bmp" ToolTip="Cancel Aircraft Configuration Changes" />
                                                    </td>
                                                </tr>
                                            </table>                                                                        
                                        </asp:View>
                                    </asp:MultiView>
                    </div>
                    <div id="tabs-5">
                                    <div>
                                        <asp:DataGrid 
                                            ID="TestsAssignedDG"
                                            runat="server"
                                            autogenerateColumns="false"
                                            GridLines="both"
                                            DataKeyField="Id"
                                            BorderStyle="Solid"
                                            BorderColor="Black"
                                            Cellpadding="4"
                                            Font-Names="Verdana, Arial"
                                            ShowHeader="True"
                                            ShowFooter="True"
                                            Caption="Tests Assigned to Aircraft"
                                            CaptionAlign="top"
                                            AlternatingItemStyle-BackColor="Gainsboro"
                                            Font-size="10pt">
                                            <HeaderStyle Font-Bold="true" Font-Size="10pt" BackColor="darkgray" ForeColor="White" />
                                            <Columns>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="RemoveTestButton" runat="server" CommandName="Remove" imageUrl="~/images/minus_red.bmp" ToolTip="Remove Test" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn> 
                                                <asp:TemplateColumn HeaderText="" Visible="False" HeaderStyle-Width="0">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TestIdLB" runat="server" Text='<%#: Eval("Id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Test Description" HeaderStyle-Width="600px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TestDescriptionLB" runat="server" Text='<%#: Eval("StringValue")%>' style="width:75px;"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>  
                                                <asp:TemplateColumn HeaderText="Sort Order" Visible="False" HeaderStyle-Width="0px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SortOrderLB" runat="server" Text='<%#: Eval("SortOrder")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>                             
                                    </div>
                                    <div>
                                        <asp:DataGrid 
                                            ID="TestsNotAssignedDG"
                                            runat="server"
                                            autogenerateColumns="false"
                                            GridLines="both"
                                            DataKeyField="Id"
                                            BorderStyle="Solid"
                                            BorderColor="Black"
                                            Cellpadding="4"
                                            Font-Names="Verdana, Arial"
                                            ShowHeader="True"
                                            ShowFooter="True"
                                            Caption="Tests Not Assigned to Aircraft"
                                            CaptionAlign= "Top"
                                            AlternatingItemStyle-BackColor="Gainsboro"
                                            Font-size="10pt">
                                            <HeaderStyle Font-Bold="true" Font-Size="10pt" BackColor="darkgray" ForeColor="White" />
                                            <Columns>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="AssignTestbutton" runat="server" CommandName="Assign" ImageUrl="~/images/plus_green.bmp" ToolTip="Assign New Test" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>  
                                                <asp:TemplateColumn HeaderText="" Visible="False" HeaderStyle-Width="0">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TestIdLB" runat="server" Text='<%#: Eval("Id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Test Description" HeaderStyle-Width="600px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TestDescriptionLB" runat="server" Text='<%#: Eval("StringValue")%>' style="width:75px;"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>  
                                                <asp:TemplateColumn HeaderText="Sort Order" Visible="False" HeaderStyle-Width="0px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SortOrderLB" runat="server" Text='<%#: Eval("SortOrder")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>                                                 
                                            </Columns>
                                        </asp:DataGrid>                             
                                    </div>
                    </div>
                    <div id="tabs-6">
                        <asp:DataGrid
                            cssclass="ChildGridView"
                            ID="AircraftCommentsDG"
                            runat="server"
                            autogenerateColumns="false"
                            GridLines="both"
                            DataKeyField="GenericCommentId"
                            BorderStyle="Solid"
                            BorderColor="Black"
                            Cellpadding="4"
                            Font-Names="Verdana, Arial"
                            ShowHeader="True"
                            ShowFooter="True"
                            AllowPaging="true"
                            PageSize="10"
                            PagerStyle-Mode="NumericPages"
                            PagerStyle-NextPageText="Next"
                            PagerStyle-PrevPageText="Prev"
                           
                            AlternatingItemStyle-BackColor="Gainsboro"
                            Font-size="10pt"
                            height="100%">
                            <HeaderStyle Font-Bold="true" Font-Size="10pt" BackColor="darkgray" ForeColor="White" />
                            <Columns>
                                <asp:TemplateColumn  ItemStyle-HorizontalAlign="center" FooterStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="SelectButton" runat="server" CommandName="select" ImageUrl="~/images/table.bmp" ToolTip="Show related content" />
                                    </ItemTemplate>
                                    <EditItemTemplate>&nbsp;</EditItemTemplate>
                                    <FooterTemplate>&nbsp;</FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="center" FooterStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <nobr >
                                            <asp:ImageButton ID="EditButton" runat="Server" CommandName="edit" imageurl="~/images/pencil_yellow.bmp" ToolTip="Edit this entry" />
                                        </nobr>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <nobr>
                                            <asp:ImageButton ID="SaveButton" runat="server" CommandName="save" imageurl="~/images/save.bmp" ToolTip="Save pending changes" />
                                            <asp:ImageButton ID="CancelButton" runat="server" CommandName="cancel" imageurl="~/images/cancel.bmp" tooltip="Cancel pending changes" />
                                        </nobr>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="" HeaderStyle-width="0" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="SourceTableLB" runat="server" Text='<%#: Eval("SourceTable")%>'></asp:Label>
                                        <asp:Label ID="SourceIdLB" runat="server" Text='<%#: Eval("SourceId")%>'></asp:Label>
                                        <asp:Label ID="ParentIdLB" runat="server" Text='<%#: Eval("ParentId") %>'></asp:Label>
                                        <asp:Label ID="CommentTimeLB" runat="server" Text='<%#: Eval("CommentTime") %>'></asp:Label>
                                        <asp:Label ID="CommentDateTime_UTCLB" runat="server" Text='<%#: Eval("CommentDateTime_UTC", "MM/dd/yyyy HH:mm:ss.fffffff")%>'></asp:Label>
                                        <asp:Label ID="CommentDateTime_LocalLB" runat="server" text='<%#: Eval("CommentDateTime_Local", "MM/dd/yyyy HH:mm:ss.fffffff") %>'></asp:Label>
                                        <asp:Label ID="AuthorIdLB" runat="server" Text='<%#: Eval("AuthorId") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="SourceTableLB" runat="server" Text='<%#: Bind("SourceTable") %>'></asp:Label>
                                        <asp:Label ID="SourceIdLB" runat="server" Text='<%#: Bind("SourceId") %>'></asp:Label>
                                        <asp:Label ID="ParentIdLB" runat="server" Text='<%#: Bind("ParentId") %>'></asp:Label>
                                        <asp:Label ID="CommentTimeLB" runat="server" Text='<%#: Bind("CommentTime") %>'></asp:Label>
                                        <asp:Label ID="CommentDateTime_UTCLB" runat="server" Text='<%#: Bind("CommentDateTime_UTC") %>'></asp:Label>
                                        <asp:Label ID="CommentDateTime_LocalLB" runat="server" text='<%#: Bind("CommentDateTime_Local") %>'></asp:Label>
                                        <asp:Label ID="AuthorIdLB" runat="server" Text='<%#: Bind("AuthorId") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="CommentDateLB" runat="server" Text='<%#: Eval("CommentDate")%>' style="width: 75px;"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="CommentDateLB" runat="server" Text='<%#: Bind("CommentDate") %>' style="width:75px;"></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="CommentDateTB" runat="server" Text=''  CssClass="datePickerTB" style="width: 75px;"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:Label ID="CommentTypeLB" runat="server" Text='<%#: Eval("CommentType")%>' ></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="CommentTypeLB" runat="server" Text='<%#: Bind("CommentType") %>' ></asp:Label>                                    
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="CommentTypeLB" runat="server" Text="Aircraft"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Engineer">
                                    <ItemTemplate>
                                        <asp:Label ID="AuthorNameLB" runat="server" Text='<%#: Eval("AuthorName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="AuthorNameLB" runat="server" Text='<%#: Bind("AuthorName") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="CommentAuthorDDL" runat="server" DataTextField="StringValue" DataValueField="Id" DataSource='<%# GetEngineers() %>' ></asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Issue Code">
                                    <ItemTemplate>
                                        <asp:Label ID="IssueCodeLB" runat="server" Text='<%#: Eval("IssueCode")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="IssueCodeDDL" runat="Server" DataTextField="StringValue" DataValueField="Id" DataSource='<%# GetIssueCodes() %>' SelectedValue='<%# Bind("IssueCodeId") %>'></asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="IssueCodeDDL" runat="Server" DataTextField="StringValue" DataValueField="Id" DataSource='<%# GetIssueCodes() %>' ></asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Component">
                                    <ItemTemplate>
                                        <asp:Label ID="ComponentLB" runat="server" Text='<%#: Eval("ComponentName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ComponentDDL" runat="server"  DataTextField="StringValue" DataValueField="Id" DataSource='<%# GetComponents() %>' SelectedValue='<%# Bind("ComponentId") %>' >
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ComponentDDL" runat="server"  DataTextField="StringValue" DataValueField="Id" DataSource='<%# GetComponents() %>' >
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Rating">
                                    <ItemTemplate>
                                        <asp:Label ID="CommentRatingLB" runat="server" Text='<%#: Eval("CommentRating")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="CommentRatingDDL" runat="server" SelectedValue='<%#: Bind("CommentRating") %>'>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="CommentRatingDDL" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Comments">
                                    <ItemTemplate>
                                        <asp:Label ID="CommentsLB" runat="Server" Text='<%#: Eval("Comments")%>' style="width: 300px;"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="CommentsTB" runat="Server" Text='<%#: Bind("Comments") %>' TextMode="MultiLine"  style="width: 300px; height: 90px;" ></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="CommentsTB" runat="Server" Text='' TextMode="MultiLine" style="width: 300px; height: 90px;"></asp:TextBox>                                    
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="center" FooterStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                            <asp:ImageButton ID="DeleteButton" runat="server" CommandName="delete" imageurl="~/images/x_red.bmp" ToolTip="Delete this entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");' />
                                    </ItemTemplate>
                                    <EditItemTemplate>&nbsp;</EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:ImageButton ID="AddButton" runat="server" CommandName="add" ImageUrl="~/images/plus_blue.bmp" ToolTip="AddNew Entry" />
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </div>
            
            </td>
        </tr>
        <tr runat="server">
            <td colspan="4" style="text-align: center;">
                <asp:Button ID="SaveButton" runat="server" Text="Save" AccessKey="S" ToolTip="Save Pending Changes" style="width: 75px; height: 30px;" />
                <asp:Button ID="CancelButton" runat="server" Text="Cancel" AccessKey="C" ToolTip="Cancel Pending Changes" style="width: 75px; height: 30px;" />
            </td>
        </tr>
    </table>
    
    <asp:HiddenField ID="ProgIdHiddenField" runat="server" />
    <asp:HiddenField ID="AircraftIdHiddenField" runat="server" />
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
