<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="WorkOrderModify.aspx.vb" Inherits="FLTWebApp.WorkOrderModify" 
    title="Work Order Modify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Work Order Modify</h2>
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
    <table cellspacing="0" cellpadding="0" style="width: 800px; border: solid 1px black">
        <tr>
          <td>
            <table class="GridList" cellspacing="0" cellpadding="5" style="width: 100%">
              <colgroup>
                <col style="width: 20%" />
                <col style="width: 20%" />
                <col style="width: 20%" />
                <col style="width: 20%" />
                <col style="width: 20%" />
              </colgroup>
              <tr>
                  <th>Aircraft *</th>
                  <th>Location *</th>
                  <th>Work Order Type *</th>
                  <th>Work Order Status *</th>
                  <th>Work Order Nbr</th>
              </tr>
              <tr>
                  <td><asp:DropDownList ID="AircraftDDL" runat="server"></asp:DropDownList></td>
                  <td><asp:DropDownList ID="LocationDDL" runat="server"></asp:DropDownList></td>              
                  <td><asp:DropDownList ID="WorkOrderTypeDDL" runat="server"></asp:DropDownList></td>
                  <td><asp:DropDownList ID="WorkOrderStatusDDL" runat="server"></asp:DropDownList></td>
                  <td><asp:Label ID="WorkOrderNbrLB" runat="server"></asp:Label></td>
              </tr>
              <tr>
                <th>Request Date *</th>
                <th>Engineer *</th>
                <th>Engineering Action *</th>
                <th>Date Fault Occurred</th>
                <th>Meter Fault Occurred</th>
              </tr>
              <tr>
                <td><asp:TextBox ID="RequestDateTB" runat="server" CssClass="datePickerTB" ></asp:TextBox></td>
                <td><asp:DropDownList ID="EngineerDDL" runat="server"></asp:DropDownList></td>
                <td><asp:DropDownList ID="EngineeringActionDDL" runat="server"></asp:DropDownList></td>
                <td><asp:TextBox ID="FaultOccurredDateTB" runat="server" CssClass="datePickerTB"></asp:TextBox></td>
                <td><asp:TextBox ID="FaultOccurredMeterTB" runat="server"></asp:TextBox></td>
              </tr>
              <tr>
                <th>Due Date</th>
                <th>Due Time</th>
                <th>Start Meter</th>
                <th>Mechanic</th>
                <th>&nbsp;</th>
              </tr>
              <tr>
                <td><asp:TextBox ID="DueDateTB" runat="Server" CssClass="datePickerTB" ></asp:TextBox></td>
                <td><asp:TextBox ID="DueTimeTB" runat="server"></asp:TextBox></td>
                <td><asp:TextBox ID="StartMeterTB" runat="server"></asp:TextBox></td>
                <td><asp:DropDownList ID="MechanicDDL" runat="server"></asp:DropDownList></td>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <th>Finish Date</th>
                <th>Finish Time</th>
                <th>End Meter</th>
                <th colspan="2">Aircraft Disposition</th>
              </tr>
              <tr>
                <td><asp:TextBox ID="FinishDateTB" runat="Server" CssClass="datePickerTB" ></asp:TextBox></td>
                <td><asp:TextBox ID="FinishTimeTB" runat="server"></asp:TextBox></td>
                <td><asp:TextBox ID="EndMeterTB" runat="server"></asp:TextBox></td>
                <td colspan="2"><asp:TextBox ID="AircraftDispositionTB" runat="server" style="width:97%"></asp:TextBox></td>
              </tr>
              <tr>
                <th>FIS Complaint Nbr</th>
                <th>Squawk Number</th>
                <th>&nbsp;</th>
                <th colspan="2" rowspan="2" style="text-align: left;">
                  <div>
                    <div style="text-align:left;"><asp:CheckBox ID="AircraftIssueAlertCB" runat="server" Text="Aircraft Issue Alert" /></div>
                    <div style="text-align:left;"><asp:CheckBox ID="MaintIssueAlertCB" runat="server" Text="Maintenance Issue Alert" /></div>                 
                  </div>
                </th>
              </tr>
              <tr>
                <td><asp:TextBox ID="FISComplaintNbrTB" runat="server"></asp:TextBox></td>
                <td><asp:TextBox ID="SquawkNbrTB" runat="server"></asp:TextBox></td>
                <td>&nbsp;</td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <div id="tabs" class="tabs">
              <ul>
                <li><a href="#tabs-1">Reasons</a></li>
                <li><a href="#tabs-2">Instructions</a></li>
                <li><a href="#tabs-3">Performed Work</a></li>
                <li><a href="#tabs-4">Eng. Comments</a></li>
                <li><a href="#tabs-5">Tech Comments</a></li>
                <li><a href="#tabs-6">Parts</a></li>
                <li><a href="#tabs-7">Docs</a></li>
              </ul>
              <div id="tabs-1" style="text-align:center">
                  <asp:DataGrid
                    ID="WOReasonsDG" 
                    runat="server"
                    cssclass="ChildGridView"
                    autogenerateColumns="false"
                    GridLines="both"
                    DataKeyField="WOReasonId"
                    BorderStyle="Solid"
                    BorderColor="Black"
                    Cellpadding="4"
                    Font-Names="Verdana, Arial"
                    ShowHeader="True"
                    ShowFooter="True"
                    AlternatingItemStyle-BackColor="Gainsboro"
                    Font-size="10pt"
                    >
                    <Columns>
                        <asp:TemplateColumn>
                            <EditItemTemplate>
                                <asp:ImageButton ID="SaveButton" runat="server" CommandName="Update" ImageUrl="~/images/save.bmp" ToolTip="Save changes" />
                                <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" ImageUrl="~/images/cancel.bmp" ToolTip="Cancel changes" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit this entry" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="false">
                            <EditItemTemplate>
                                <asp:Label ID="WOReasonsIdLB" runat="server" Text='<%#: Bind("WOReasonID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="WOReasonsIdLB" runat="server" Text='<%#: Eval("WOReasonID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Reasons" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:Label ID="ReasonLineLB" runat="server" Text='<%#: Eval("ReasonLine") %>' Width="400px"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="ReasonLineTB" runat="server" Text='<%#: Bind("ReasonLine") %>' style="width:400px; height: 90px;" TextMode="MultiLine"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="ReasonLineTB" runat="server" Text="" style="width:400px; height: 90px;" TextMode="MultiLine"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton" runat="Server" CommandName="Delete" ImageUrl="~/images/x_red.bmp" ToolTip="Delete this entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="AddButton" runat="server" CommandName="Add" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add new entry" />
                            </FooterTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                  </asp:DataGrid>
              </div>
              <div id="tabs-2" style="text-align:center">
                  <asp:DataGrid 
                    ID="WOInstructionsDG" 
                    runat="server"
                    cssclass="ChildGridView"
                    autogenerateColumns="false"
                    GridLines="both"
                    DataKeyField="WOInstructionsId"
                    BorderStyle="Solid"
                    BorderColor="Black"
                    Cellpadding="4"
                    Font-Names="Verdana, Arial"
                    ShowHeader="True"
                    ShowFooter="True"
                    AlternatingItemStyle-BackColor="Gainsboro"
                    Font-size="10pt"
                    >
                    <Columns>
                        <asp:TemplateColumn>
                            <EditItemTemplate>
                                <asp:ImageButton ID="SaveButton" runat="server" CommandName="Update" ImageUrl="~/images/save.bmp" ToolTip="Save changes" />
                                <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" ImageUrl="~/images/cancel.bmp" ToolTip="Cancel changes" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit this entry" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="false">
                            <EditItemTemplate>
                                <asp:Label ID="WOInstructionsIdLB" runat="server" Text='<%#: Bind("WOInstructionsID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="WOInstructionsIdLB" runat="server" Text='<%#: Eval("WOInstructionsID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Instructions" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:Label ID="InstructionLineLB" runat="server" Text='<%#: Eval("InstructionLine") %>' Width="400px"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="InstructionLineTB" runat="server" Text='<%#: Bind("InstructionLine") %>' style="width:400px; height: 90px;" TextMode="MultiLine"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="InstructionLineTB" runat="server" Text="" style="width:400px; height: 90px;" TextMode="MultiLine"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton" runat="Server" CommandName="Delete" ImageUrl="~/images/x_red.bmp" ToolTip="Delete this entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="AddButton" runat="server" CommandName="Add" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add new entry" />
                            </FooterTemplate>

                        </asp:TemplateColumn>
                    </Columns>
                  </asp:DataGrid>
              </div>
              <div id="tabs-3" style="text-align:center">
                  <asp:DataGrid 
                    ID="WOPerformedWorkDG" 
                    runat="server"
                    cssclass="ChildGridView"
                    autogenerateColumns="false"
                    GridLines="both"
                    DataKeyField="PerformedWorkId"
                    BorderStyle="Solid"
                    BorderColor="Black"
                    Cellpadding="4"
                    Font-Names="Verdana, Arial"
                    ShowHeader="True"
                    ShowFooter="True"
                    AlternatingItemStyle-BackColor="Gainsboro"
                    Font-size="10pt"
                    >
                    <Columns>
                        <asp:TemplateColumn>
                            <EditItemTemplate>
                                <asp:ImageButton ID="SaveButton" runat="server" CommandName="Update" ImageUrl="~/images/save.bmp" ToolTip="Save changes" />
                                <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" ImageUrl="~/images/cancel.bmp" ToolTip="Cancel changes" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit this entry" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="false">
                            <EditItemTemplate>
                                <asp:Label ID="PerformedWorkIdLB" runat="server" Text='<%#: Bind("PerformedWorkID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="PerformedWorkIdLB" runat="server" Text='<%#: Eval("PerformedWorkId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Work Performed" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:Label ID="PerformedWorkLineLB" runat="server" Text='<%#: Eval("PerformedWorkLine") %>' Width="400px"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="PerformedWorkLineTB" runat="server" Text='<%#: Bind("PerformedWorkLine") %>' style="width:400px; height: 90px;" TextMode="MultiLine"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="PerformedWorkLineTB" runat="server" Text="" style="width:400px; height: 90px;" TextMode="MultiLine"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton" runat="Server" CommandName="Delete" ImageUrl="~/images/x_red.bmp" ToolTip="Delete this entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="AddButton" runat="server" CommandName="Add" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add new entry" />
                            </FooterTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                  </asp:DataGrid>
              </div>
              <div id="tabs-4" style="text-align:center">
                  <asp:DataGrid 
                    ID="WOEngineerCommentsDG" 
                    runat="server"
                    cssclass="ChildGridView"
                    autogenerateColumns="false"
                    GridLines="both"
                    DataKeyField="EngineerCommentsId"
                    BorderStyle="Solid"
                    BorderColor="Black"
                    Cellpadding="4"
                    Font-Names="Verdana, Arial"
                    ShowHeader="True"
                    ShowFooter="True"
                    AlternatingItemStyle-BackColor="Gainsboro"
                    Font-size="10pt"
                    >
                    <Columns>
                        <asp:TemplateColumn>
                            <EditItemTemplate>
                                <asp:ImageButton ID="SaveButton" runat="server" CommandName="Update" ImageUrl="~/images/save.bmp" ToolTip="Save changes" />
                                <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" ImageUrl="~/images/cancel.bmp" ToolTip="Cancel changes" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit this entry" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="false">
                            <EditItemTemplate>
                                <asp:Label ID="EngineerCommentsIDLB" runat="server" Text='<%#: Bind("EngineerCommentsID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="EngineerCommentsIDLB" runat="server" Text='<%#: Eval("EngineerCommentsID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="CommentDateLB" runat="server" Text='<%#: Eval("CommentDate")%>' style="width: 75px;"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="CommentDateTB" runat="server" Text='<%#: Bind("CommentDate") %>'  CssClass="datePickerTB" style="width:75px;"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="CommentDateTB" runat="server" Text=''  CssClass="datePickerTB" style="width: 75px;"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Component">
                            <ItemTemplate>
                                <asp:Label ID="ComponentLB" runat="server" Text='<%#: Eval("Component") %>'></asp:Label>
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
                        <asp:TemplateColumn HeaderText="Issue Code" HeaderStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="IssueCodeLB" runat="server" Text='<%#: Eval("IssueCode") %>' Width="100px"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="IssueCodeDDL" runat="server" DataTextField="StringValue" DataValueField="Id" SelectedValue='<%# Bind("IssueCodeId") %>' DataSource='<%# GetIssueCodeList() %>' Width="100px"></asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="IssueCodeDDL" runat="server" DataTextField="StringValue" DataValueField="Id" DataSource='<%# GetIssueCodeList() %>' Width="100px"></asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateColumn>                        
                        <asp:TemplateColumn HeaderText="Rating">
                            <ItemTemplate>
                                <asp:Label ID="CommentRatingLB" runat="server" Text='<%#: Eval("CommentRating") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="CommentRatingDDL" runat="server" SelectedValue='<%#: Bind("CommentRating") %>'>
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
                                <asp:DropDownList ID="CommentRatingDDL" runat="server">
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
                        <asp:TemplateColumn HeaderText="Engineer Comments" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:Label ID="CommentsLB" runat="server" Text='<%#: Eval("Comments") %>' style="width:400px; height: 90px;"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="CommentsTB" runat="server" Text='<%#: Bind("Comments") %>' style="width:400px; height: 90px;" TextMode="MultiLine"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="CommentsTB" runat="server" Text="" style="width:400px; height: 90px;" TextMode="MultiLine"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton" runat="Server" CommandName="Delete" ImageUrl="~/images/x_red.bmp" ToolTip="Delete this entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="AddButton" runat="server" CommandName="Add" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add new entry" />
                            </FooterTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                  </asp:DataGrid>
              </div>
              <div id="tabs-5" style="text-align:center">
                  <asp:DataGrid 
                    ID="WOTechCommentsDG" 
                    runat="server"
                    cssclass="ChildGridView"
                    autogenerateColumns="false"
                    GridLines="both"
                    DataKeyField="WOMechanicCommentsId"
                    BorderStyle="Solid"
                    BorderColor="Black"
                    Cellpadding="4"
                    Font-Names="Verdana, Arial"
                    ShowHeader="True"
                    ShowFooter="True"
                    AlternatingItemStyle-BackColor="Gainsboro"
                    Font-size="10pt"
                    >
                    <Columns>
                        <asp:TemplateColumn>
                            <EditItemTemplate>
                                <asp:ImageButton ID="SaveButton" runat="server" CommandName="Update" ImageUrl="~/images/save.bmp" ToolTip="Save changes" />
                                <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" ImageUrl="~/images/cancel.bmp" ToolTip="Cancel changes" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit this entry" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="false">
                            <EditItemTemplate>
                                <asp:Label ID="WOMechanicCommentsIdLB" runat="server" Text='<%#: Bind("WOMechanicCommentsId") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="WOMechanicCommentsIdLB" runat="server" Text='<%#: Eval("WOMechanicCommentsId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="CommentDateLB" runat="server" Text='<%#: Eval("CommentDate") %>' style="width: 75px;"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="CommentDateTB" runat="server" Text='<%#: Bind("CommentDate") %>'  CssClass="datePickerTB" style="width:75px;"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="CommentDateTB" runat="server" Text=''  CssClass="datePickerTB" style="width: 75px;"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Component">
                            <ItemTemplate>
                                <asp:Label ID="ComponentLB" runat="server" Text='<%#: Eval("Component") %>'></asp:Label>
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
                        <asp:TemplateColumn HeaderText="Issue Code" HeaderStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="IssueCodeLB" runat="server" Text='<%#: Eval("IssueCode") %>' Width="100px"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="IssueCodeDDL" runat="server" DataTextField="StringValue" DataValueField="Id" SelectedValue='<%# Bind("IssueCodeId") %>' DataSource='<%# GetIssueCodeList() %>' Width="100px"></asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="IssueCodeDDL" runat="server" DataTextField="StringValue" DataValueField="Id" DataSource='<%# GetIssueCodeList() %>' Width="100px"></asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateColumn>                        
                        <asp:TemplateColumn HeaderText="Rating">
                            <ItemTemplate>
                                <asp:Label ID="CommentRatingLB" runat="server" Text='<%#: Eval("CommentRating") %>'></asp:Label>
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
                                <asp:DropDownList ID="CommentRatingDDL" runat="server">
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
                        <asp:TemplateColumn HeaderText="Mechanic Comments" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:Label ID="CommentsLB" runat="server" Text='<%#: Eval("Comments") %>' style="width:400px; height: 90px;"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="CommentsTB" runat="server" Text='<%#: Bind("Comments") %>' style="width:400px; height: 90px;" TextMode="MultiLine"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="CommentsTB" runat="server" Text="" style="width:400px; height: 90px;" TextMode="MultiLine"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton" runat="Server" CommandName="Delete" ImageUrl="~/images/x_red.bmp" ToolTip="Delete this entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="AddButton" runat="server" CommandName="Add" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add new entry" />
                            </FooterTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                  </asp:DataGrid>
              </div>
              <div id="tabs-6" style="text-align:center">
                  <asp:DataGrid 
                    ID="WOPartsDG" 
                    runat="server"
                    cssclass="ChildGridView"
                    autogenerateColumns="false"
                    GridLines="both"
                    DataKeyField="WorkOrderPartId"
                    BorderStyle="Solid"
                    BorderColor="Black"
                    Cellpadding="4"
                    Font-Names="Verdana, Arial"
                    ShowHeader="True"
                    ShowFooter="True"
                    AlternatingItemStyle-BackColor="Gainsboro"
                    Font-size="10pt"
                    >
                    <Columns>
                        <asp:TemplateColumn HeaderText="" >
                            <EditItemTemplate>
                                <asp:ImageButton ID="SaveButton" runat="server" CommandName="Update" ImageUrl="~/images/save.bmp" ToolTip="Save changes" />
                                <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" ImageUrl="~/images/cancel.bmp" ToolTip="Cancel changes" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" ImageUrl="~/images/pencil_yellow.bmp" ToolTip="Edit this entry" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="false">
                            <EditItemTemplate>
                                <asp:Label ID="WorkOrderPartIdLB" runat="server" Text='<%#: Bind("WorkOrderPartId") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="WorkOrderPartIdLB" runat="server" Text='<%#: Eval("WorkOrderPartId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Part Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="PartNameTB" runat="server" Text='<%#: Bind("PartName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="PartNameLB" runat="server" Text='<%#: Eval("PartName") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="PartNameTB" runat="server" Text=''></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Part Descr">
                            <EditItemTemplate>
                                <asp:TextBox ID="PartDescriptionTB" runat="server" Text='<%#: Bind("PartDescription") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="PartDescriptionLB" runat="server" Text='<%#: Eval("PartDescription") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="PartDescriptionTB" runat="server" Text=''></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Part Module">
                            <EditItemTemplate>
                                <asp:TextBox ID="PartModuleTB" runat="Server" Text='<%#: Bind("PartModule") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="PartModuleLB" runat="server" Text='<%#: Eval("PartModule") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="PartModuleTB" runat="Server" Text=''></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="FIS ID Removed">
                            <EditItemTemplate>
                                <asp:TextBox ID="FisIdRemovedTB" runat="server" Text='<%#: Bind("FisIDRemoved") %>'></asp:TextBox><br />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="FisIdRemovedLB" runat="server" Text='<%#: Eval("FisIDRemoved") %>'></asp:Label><br />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="FisIdRemovedTB" runat="server" Text=''></asp:TextBox><br />
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="FIS ID Added">
                            <EditItemTemplate>
                                <asp:TextBox ID="FisIdAddedTB" runat="server" Text='<%#: Bind("FisIDAdded") %>'></asp:TextBox><br />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="FisIdAddedLB" runat="server" Text='<%#: Eval("FisIDAdded") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="FisIdAddedTB" runat="server" Text=''></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nbr Failed" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                            <EditItemTemplate>
                                <asp:TextBox ID="PartNbrFailedTB" runat="server" Text='<%#: Bind("PartNbrFailed")%>' style="width:46px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="PartNbrFailedLB" runat="server" Text='<%#: Eval("PartNbrFailed") %>' style="width:46px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="PartNbrFailedTB" runat="server" Text='' style="width:46px"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nbr Removed" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                            <EditItemTemplate>
                                <asp:TextBox ID="PartNbrRemovedTB" runat="server" Text='<%#: Bind("PartNbrRemoved")%>' style="width:46px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="PartNbrRemovedLB" runat="server" Text='<%#: Eval("PartNbrRemoved")%>' style="width:46px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="PartNbrRemovedTB" runat="server" Text='' style="width: 46px;"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nbr Added" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                            <EditItemTemplate>
                                <asp:TextBox ID="PartNbrAddedTB" runat="server" Text='<%#: Bind("PartNbrAdded") %>' style="width:46px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="PartNbrAddedLB" runat="server" Text='<%#: Eval("PartNbrAdded")%>' style="width:46px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="PartNbrAddedTB" runat="server" Text='' style="width:46px"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton" runat="Server" CommandName="Delete" ImageUrl="~/images/x_red.bmp" ToolTip="Delete this entry" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="AddButton" runat="server" CommandName="Add" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add new entry" />
                            </FooterTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                  </asp:DataGrid>
              </div>
              <div id="tabs-7" style="text-align:center">
                <asp:MultiView ID="DocumentsMV" runat="server" ActiveViewIndex="0">
                    <asp:View ID="DocumentListView" runat="server">
                        <table cellspacing="0" cellpadding="5px" style="border: solid 0 white;">
                            <colgroup>
                                <col width="5px" />
                                <col width="*" />
                                <col width="5px" />
                            </colgroup>
                            <tr>
                                <td>&nbsp;</td>
                                <th style="text-align: left; padding-left: 10px;">
                                    <asp:ImageButton ID="AddDocumentButton" runat="server" ImageUrl="~/images/plus_blue.bmp" ToolTip="Add New Document" />                
                                </th>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:GridView 
                                        ID="WODocumentsGV" 
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
                                                    <asp:Label ID="DocumentIdLB" runat="server" Visible="false" Text='<%#: Eval("DocumentId")%>'></asp:Label>
                                                    <asp:Label ID="DocumentTypeIdLB" runat="server" Visible="False" Text='<%#: Eval("DocumentTypeId")%>'></asp:Label>
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
                                                    <asp:Label ID="MimeType" runat="server" text='<%#: Eval("MimeType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="DocumentSizeLB" runat="server" Text='<%#: Eval("DocumentSize")%>'></asp:Label>
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
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="DeleteButton" runat="server" ImageUrl="~/images/x_red.bmp" ToolTip="Delete this entry" CommandName="Delete" OnClientClick='return confirm("Are you certain that you want to delete this entry?");'  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="DocumentUploadView" runat="server">
                        <table class="GridList" cellpadding="5" cellspacing="0" style="border: solid 1px black;" >
                            <tr>
                                <th>Document to upload</th>
                                <td><asp:FileUpload ID="FileUpoad1" runat="server" style="width: 600px;" /></td>
                            </tr>
                            <tr>
                                <th>Document Description</th>
                                <td><asp:TextBox ID="DocumentDescriptionTB" runat="server" style="width: 600px;"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align:center;"><asp:Button ID="UploadButton" runat="server" Text="Upload" />&nbsp;<asp:Button ID="UploadCancelButton" runat="Server" Text="Cancel" /> </td>
                            </tr>
                        </table>                    
                    </asp:View>
                </asp:MultiView>
              </div>
            </div>
          </td>
        </tr>
        <tr>
          <td style="text-align: center;">
            <asp:Button ID="SaveButton" runat="server" Text="Save" style="width: 75px; height: 30px;" />&nbsp;
            <asp:Button ID="CancelButton" runat="server" Text="Cancel" style="width: 75px; height: 30px;" />
          </td>
        </tr>
    </table>
    <asp:HiddenField ID="ProgIdHiddenField" runat="server" />
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
