<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="FLTFleet.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Contoso Flight Log Tracking - Login Page</title>
    <link href="/Css/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="LoginContainer">
            <asp:Login ID="Login1" runat="server" 
                cssclass="LoginControl"
                DestinationPageUrl="~/Default.aspx"
                PasswordRecoveryUrl="password_recovery.aspx" 
                PasswordRecoveryText="Forgot Password?" 
                DisplayRememberMe="False" 
                RememberMeText="Remember Me Next Time"
                 BorderPadding="5"
                 
                 >
                <TextBoxStyle Width="150px" />
                <TitleTextStyle Font-Bold="True" HorizontalAlign="Center" />
            </asp:Login>
        </div>
    </form>
</body>
</html>
