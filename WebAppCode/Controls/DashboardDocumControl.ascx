<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashboardDocumControl.ascx.cs" Inherits="WebAppCode.Controls.DashboardDocumControl" %>
<%@ Register assembly="DevExpress.Dashboard.v18.1.Web.WebForms, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.DashboardWeb" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Import Namespace="WebAppCode.Controls" %>

<script>
    var chartDocumentsByDaysComponentName = "<%=DashboardDocumControl.ChartDocumentsByDaysComponentName%>";
    var chartDocumentsByNamesComponentName = "<%=DashboardDocumControl.ChartDocumentsByNamesComponentName%>";
    var chartDocumentsByNamesComponentName2 = "<%=DashboardDocumControl.ChartDocumentsByNamesComponentName2%>";
</script>
<script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/DashboardDocumControl.js") %>"></script>


<%--<dx:ASPxCallbackPanel ID="ASPxCallbackPanel" runat="server" ClientInstanceName="CallbackPanel" Height="10px" Width="100%" 
        OnCallback="ASPxCallbackPanel_Callback"
        OnInit="ASPxCallbackPanel_OnInit"
        ClientSideEvents-EndCallback="ASPxCallbackPanel_EndCallback">
<ClientSideEvents EndCallback="ASPxCallbackPanel_EndCallback"></ClientSideEvents>
    <PanelCollection>
        <dx:PanelContent runat="server">
            <dx:ASPxDashboard ID="ASPxDashboardDocum" runat="server" ClientInstanceName="DashboardDocum" WorkingMode="Viewer"
                              OnInit="ASPxDashboardDocum_OnInit"
                              OnLoad="ASPxDashboardDocum_OnLoad"
                              OnDashboardLoading="ASPxDashboardDocum_DashboardLoading"  

                              ClientSideEvents-ItemWidgetCreated="CustomizeWidgets"  
                              ClientSideEvents-ItemWidgetUpdated="CustomizeWidgets"  
                              ClientSideEvents-ItemWidgetUpdating="UnsubscribeFromEvents"
                              ClientSideEvents-Init="InitDashboard" />
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>--%>

<dx:ASPxDashboard ID="ASPxDashboardDocum" runat="server" ClientInstanceName="DashboardDocum" WorkingMode="ViewerOnly"
                  OnInit="ASPxDashboardDocum_OnInit"
                  OnLoad="ASPxDashboardDocum_OnLoad"
                  OnDashboardLoading="ASPxDashboardDocum_DashboardLoading"  

                  ClientSideEvents-ItemWidgetCreated="CustomizeWidgets"  
                  ClientSideEvents-ItemWidgetUpdated="CustomizeWidgets"  
                  ClientSideEvents-ItemWidgetUpdating="UnsubscribeFromEvents"
                  ClientSideEvents-Init="InitDashboard"
    />





