<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="ChangePassword.aspx.vb" Inherits="FLTWebApp.ChangePassword" 
    title="Change Password" %>
<asp:Content ID="ChangePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:MultiView ID="ChangePasswordMultiview" runat="server" ActiveViewIndex="0">
        <asp:View ID="ChangePasswordView" runat="server">
            <table class="changePassword" cellpadding="5" cellspacing="0">
                <tr class="cpHeader">
                    <th colspan="2">Change Your Password</th>
                </tr>
                <tr class="cpDetail">
                    <th>Old Password</th>
                    <td>
                        <asp:TextBox CssClass="cpTextBox" ID="OldPasswordTextBox" runat="server" TextMode="password" style="width: auto;"></asp:TextBox>&nbsp;*
                    </td>
                </tr>
                <tr class="cpDetail">
                    <th>New Password</th>
                    <td>
                        <asp:TextBox CssClass="cpTextBox" ID="NewPassword1TextBox" runat="server" TextMode="password" style="width: auto;"></asp:TextBox>&nbsp;*
                    </td>
                </tr>
                <tr class="cpDetail">
                    <th>Confirm New Password</th>
                    <td>
                        <asp:TextBox CssClass="cpTextBox" ID="NewPassword2TextBox" runat="server" TextMode="password" style="width: auto;"></asp:TextBox>&nbsp;*
                    </td>
                </tr>
                <tr class="cpDetail">
                    <th>Email Address</th>
                    <td>
                        <asp:TextBox CssClass="cpTextBox" ID="EmailTextBox" runat="server" style="width: auto;"></asp:TextBox>&nbsp;*
                    </td>
                </tr>
                <tr class="cpMessage">
                    <th colspan="2"><span>The Confirm New Password must match<br /> the New Password entry.</span></th>
                </tr>
                <tr class="cpErrorMessage">
                    <th colspan="2"><asp:Label ID="ErrorMessageLabel" runat="server"></asp:Label></th>
                </tr>
                <tr class="cpFooter">
                    <th colspan="2">
                        <asp:Button ID="ChangeButton" runat="server" Text="Change Password" AccessKey="C" />
                    </th>
                </tr>
            </table>
            <div style="height:20px;">&nbsp</div>
            <asp:Table ID="BrokenRulesTable" style="width: 450px;" runat="server" GridLines="both" BorderColor="Black" BorderStyle="Solid" Borderwidth="1px" Visible="False">
                <asp:TableRow>
                    <asp:TableCell><b>Broken Rules</b></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:BulletedList ID="BrokenRulesList" runat="server" ></asp:BulletedList>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:View>
        <asp:View ID="SuccessView" runat="server">
            <div style="height:20px;">&nbsp;</div>
            <div><span>Your password has been changed!</span></div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
