<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashboardDocumControl.ascx.cs" Inherits="WebAppCode.Controls.DashboardDocumControl" %>
<%@ Register assembly="DevExpress.Dashboard.v18.1.Web.WebForms, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.DashboardWeb" tagprefix="dx" %>


<%--<script src="https://cdnjs.cloudflare.com/ajax/libs/cldrjs/0.4.4/cldr.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/cldrjs/0.4.4/cldr/event.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/cldrjs/0.4.4/cldr/supplemental.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/cldrjs/0.4.4/cldr/unresolved.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/globalize/1.1.1/globalize.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/globalize/1.1.1/globalize/message.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/globalize/1.1.1/globalize/number.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/globalize/1.1.1/globalize/currency.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/globalize/1.1.1/globalize/date.min.js"></script>


<script src="https://cdn3.devexpress.com/jslib/18.1.6/js/dx.all.js"></script>--%>

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