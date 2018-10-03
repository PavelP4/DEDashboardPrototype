<%@ Page Title="ProracunDashboard 2.1-4" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VrijemeObradeDashboardPage.aspx.cs" Inherits="WebAppCode.Pages.Dashboards.VrijemeObradeDashboardPage" %>
<%@ Import Namespace="WebAppCode.Controls" %>

<%@ Register TagPrefix="uc" TagName="DashboardDocumControl" Src="~/Controls/DashboardDocumControl.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <uc:DashboardDocumControl id="DashboardDocum" runat="server" Width="100%" Height="800px" InitialDashboard="<%#DashboardDocumControl.VrijemeObradeDashboardName%>"/>

</asp:Content>
