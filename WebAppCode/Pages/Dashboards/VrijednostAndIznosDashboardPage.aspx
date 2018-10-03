<%@ Page Title="ProracunDashboard 2.1-3" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VrijednostAndIznosDashboardPage.aspx.cs" Inherits="WebAppCode.Pages.Dashboards.VrijednostAndIznosDashboardPage" %>
<%@ Import Namespace="WebAppCode.Controls" %>

<%@ Register TagPrefix="uc" TagName="DashboardDocumControl" Src="~/Controls/DashboardDocumControl.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <uc:DashboardDocumControl id="DashboardDocum" runat="server" Width="100%" Height="800px" InitialDashboard="<%#DashboardDocumControl.VrijednostAndIznosDashboardName%>"/>

</asp:Content>