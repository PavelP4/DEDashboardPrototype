<%@ Page Title="2.1-1 ProracunDashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProracunDashboardPage.aspx.cs" Inherits="WebAppCode.Pages.Dashboards.ProracunDashboardPage" %>
<%@ Import Namespace="WebAppCode.Controls" %>

<%@ Register TagPrefix="uc" TagName="DashboardDocumControl" Src="~/Controls/DashboardDocumControl.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <uc:DashboardDocumControl id="DashboardDocum" runat="server" Width="100%" Height="800px" InitialDashboard="<%#DashboardDocumControl.ProracunDashboardName%>"/>

</asp:Content>
