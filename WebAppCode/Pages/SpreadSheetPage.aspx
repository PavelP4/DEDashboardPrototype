<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpreadSheetPage.aspx.cs" Inherits="WebAppCode.Pages.SpreadSheetPage" %>

<%@ Register assembly="DevExpress.Web.ASPxSpreadsheet.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpreadsheet" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
        <dx:ASPxSpreadsheet ID="ASPxSpreadsheet1" runat="server" WorkDirectory="~/App_Data/WorkDirectory">
            <SettingsDocumentSelector>
                <FileListSettings View="Details"></FileListSettings>
            </SettingsDocumentSelector>
        </dx:ASPxSpreadsheet>
    </form>
    
</body>
</html>
