﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<%@ Register TagPrefix="uc" TagName="DashboardDocumControl" Src="~/Controls/DashboardDocumControl.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <uc:DashboardDocumControl id="DashboardDocum1" runat="server" />

</asp:Content>
