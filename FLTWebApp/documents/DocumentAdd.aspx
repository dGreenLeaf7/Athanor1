<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="DocumentAdd.aspx.vb" Inherits="FLTWebApp.DocumentAdd" 
    title="Add Document Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Add Document</h2>
    <h3><asp:Label ID="AddEditTitleLabel" runat="server"></asp:Label></h3>
    <div><asp:Label ID="ErrorMessageLabel" runat="server" style="color: Red;" Width="400px"></asp:Label></div>
    <div><asp:Label ID="SuccessMessageLabel" runat="server" style="color: Green;" Width="400px"></asp:Label></div>
    <asp:BulletedList ID="BrokenRulesList" runat="server" Visible = "false">
    </asp:BulletedList>    
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
            <td colspan="2" style="text-align:center;"><asp:Button ID="UploadButton" runat="server" Text="Upload" />&nbsp;<asp:Button ID="CancelButton" runat="Server" Text="Cancel" /> </td>
        </tr>
    </table>
    <asp:HiddenField ID="ProgIdHiddenField" runat="server" />
    
</asp:Content>
