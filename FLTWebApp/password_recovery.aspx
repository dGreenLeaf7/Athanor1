<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="password_recovery.aspx.vb" Inherits="FLTWebApp.password_recovery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Password Recovery</title>
    <link href="/Css/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:MultiView ID="PR_Multiview" runat="server" ActiveViewIndex="0">
            <asp:View ID="PR_request_view" runat="server">
                <div style="width: 600px; margin: 100px auto; position: relative; top: 100px;">
                    <table cellpadding="5" cellspacing="0" style="border: solid 3px black; background-color: Gainsboro;">
                        <tr>
                            <th colspan="3">Enter user name and click 'Send' button.<br />A new password will be sent to your email address on record.</th>
                        </tr>
                        <tr><td colspan="3">&nbsp;</td></tr>
                        <tr>
                            <th>User Name:</th>
                            <td><asp:TextBox ID="UserNameTB" runat="server"></asp:TextBox></td>
                            <td><asp:Button ID="SendButton" runat="server" Text="Send" /></td>
                        </tr>
                        <tr>
                            <td align="center"><asp:Label style="color: Red; font-weight: bold;" ID="ErrorMessageLB" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </asp:View>
            <asp:View ID="PR_sent_view" runat="server">
                <div style="width: 600px; margin: 100px auto; position: relative; top: 100px; padding: 10px; border: solid 3px black; background-color: gainsboro;">
                    <p>A new password has been generated for the user's account. This password has been mailed to the user account's email of record.</p>
                    <p>If you do not receive your email in a timely manner, please contact the webmaster of this website.</p>
                    <p><a href="Login.aspx">Click here</a> to return to the Log In page.</p>
                </div>
            </asp:View>
        </asp:MultiView>
    </form>
</body>
</html>
