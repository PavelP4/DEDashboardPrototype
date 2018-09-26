<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashboardDocumControl.ascx.cs" Inherits="WebAppCode.Controls.DashboardDocumControl" %>
<%@ Register assembly="DevExpress.Dashboard.v18.1.Web.WebForms, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.DashboardWeb" tagprefix="dx" %>


<script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/DashboardDocumControl.js") %>"></script>

<dx:ASPxDashboard ID="ASPxDashboardDocum" runat="server" ClientInstanceName="DashboardDocum"
                  OnInit="ASPxDashboardDocum_OnInit"
                  OnDashboardLoading="ASPxDashboardDocum_DashboardLoading"
                  OnCustomJSProperties="ASPxDashboardDocum_OnCustomJSProperties"

                  ClientSideEvents-ItemWidgetCreated="OnItemWidgetCreated"  
                  ClientSideEvents-ItemWidgetUpdated="OnItemWidgetUpdated"  
                  ClientSideEvents-ItemWidgetUpdating="OnItemWidgetUpdating"
                  ClientSideEvents-Init="OnInitDashboard"
                  ClientSideEvents-BeforeRender="OnBeforeRender" WorkingMode="ViewerOnly"/>

<script src="/Custom_Items/webpage-extension.js"></script>
<script src="/Custom_Items/tilespage-extension.js"></script>