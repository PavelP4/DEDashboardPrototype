<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashboardDocumControl.ascx.cs" Inherits="WebApplication1.Controls.WebUserControl" %>
<%@ Register assembly="DevExpress.Dashboard.v18.1.Web.WebForms, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.DashboardWeb" tagprefix="dx" %>

<script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/DashboardDocumControl.js") %>"></script>

<dx:ASPxDashboard ID="ASPxDashboardDocum" runat="server" AllowExecutingCustomSql="True" EnableCustomSql="True" Height="850px" DashboardStorageFolder="~/App_Data/Dashboards"
                  ClientSideEvents-BeforeRender="OnBeforeRender">
</dx:ASPxDashboard>

<script src="/Custom_Items/webpage-extension.js"></script>


