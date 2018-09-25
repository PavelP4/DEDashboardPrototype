<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TilesPage.aspx.cs" Inherits="WebAppTilesPage.TilesPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>

    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/cldrjs/0.4.4/cldr.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/cldrjs/0.4.4/cldr/event.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/cldrjs/0.4.4/cldr/supplemental.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/cldrjs/0.4.4/cldr/unresolved.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/globalize/1.1.1/globalize.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/globalize/1.1.1/globalize/message.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/globalize/1.1.1/globalize/number.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/globalize/1.1.1/globalize/currency.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/globalize/1.1.1/globalize/date.min.js"></script>
    <link rel="stylesheet" type="text/css" href="https://cdn3.devexpress.com/jslib/18.1.6/css/dx.common.css" />
    <link rel="dx-theme" data-theme="generic.light" href="https://cdn3.devexpress.com/jslib/18.1.6/css/dx.light.css" />
    <script src="https://cdn3.devexpress.com/jslib/18.1.6/js/dx.all.js"></script>
    
    <link rel="stylesheet" type="text/css" href="/Content/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/Content/TilesPage.css" />
    <script src="/Scripts/TilesPage.js"></script>
</head>
<body class="dx-viewport ">
    <div class="container body-content" style="width: 600px; padding-top: 20px; border: 1px solid lightgray;">
        <div class="row">
            <div class="col-md-8">
                <div class="tilerow-caption">Računi</div>
                <div id="računiItems"></div> 
            </div>
            <div class="col-md-4">
                <div class="tilerow-links">
                    <%--<a href="#">New Sales Quote</a>
                    <a href="#">New Sales Order</a>--%>    
                </div>
            </div>
        </div>    
        <div class="row">
            <div class="col-md-8">
                <div class="tilerow-caption">Sales Orders Released Not Sheeped</div>
                <div id="releasedNotSheepedItems"></div>    
            </div>
            <div class="col-md-4">
                <div class="tilerow-links">
                    <a href="#">Navigate</a>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8">
                <div class="tilerow-caption">Returns</div>
                <div id="returnedItems"></div>    
            </div>
            <div class="col-md-4">
                <div class="tilerow-links">
                    <a href="#">New Sales Return Order</a>
                    <a href="#">New Sales Credit Memo</a>    
                </div>
            </div>
        </div>
    </div>
</body>
</html>
