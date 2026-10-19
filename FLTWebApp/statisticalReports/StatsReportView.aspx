<%-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
     Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source. --%>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/FLT.Master" CodeBehind="StatsReportView.aspx.vb" Inherits="FLTWebApp.StatsReportView" 
    title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="1" style="background-color: Silver;">
        <tr>
            <td>
                <asp:Button ID="BackButton" runat="server" Text="&lt; - Back" />
            </td>
        </tr>
        <tr>
            <td style="background-color: White; text-align: left;">
                &nbsp;
                <CR:CrystalReportViewer 
                    ID="myCrystalReportViewer" 
                    runat="server" 
                    HasCrystalLogo="False"
                    PrintMode="PDF"
                    AutoDataBind="true" />
            </td>
        </tr>
    </table>
</asp:Content>
