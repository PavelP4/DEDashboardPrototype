var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};

var CustomItems;
(function (CustomItems) {
    CustomItems.TILESPAGE_EXTENSION_NAME = 'TilesPage';
    CustomItems.tilesPageMeta = {
        bindings: [{
            propertyName: "CustomDimensions",
            dataItemType: "Dimension",
            array: true,
            displayName: "Custom Dimensions"
        }],
        properties: [],
        icon: CustomItems.TILESPAGE_EXTENSION_NAME,
        title: "Tiles Page",
        index: 0
    };
})(CustomItems || (CustomItems = {}));

var CustomItems;
(function (CustomItems) {
    CustomItems.TILESPAGE_ICON = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<!-- Generator: Adobe Illustrator 21.0.2, SVG Export Plug-In . SVG Version: 6.00 Build 0)  -->\n<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">\n<svg version=\"1.1\" id=\"" + CustomItems.tilesPageMeta.icon + "\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\"\n\t viewBox=\"0 0 24 24\" style=\"enable-background:new 0 0 24 24;\" xml:space=\"preserve\">\n<path class=\"dx_darkgray\" d=\"M20,23H4c-0.6,0-1-0.4-1-1V2c0-0.6,0.4-1,1-1h12.6c0.3,0,0.5,0.1,0.7,0.3l3.4,3.4\n\tC20.9,4.9,21,5.1,21,5.4V22C21,22.6,20.6,23,20,23z\"/>\n<path class=\"dx_white\" d=\"M19,21H5V3h11v2c0,0.6,0.4,1,1,1h2V21z\"/>\n<path class=\"dx_blue\" d=\"M13.7,17.5c-0.2-0.4-1.6-1.8-1.4-2.2s0.2-1.1-0.1-1.3c-0.3-0.1-0.7,0.1-0.7-0.2c-0.1-0.3-1.1-0.2-1.2-1.6\n\tc-0.1-1.5-0.6-2-1.2-2s-1.6,0.6-1.5,0c0-0.1,0-0.2,0-0.3C6.6,10.9,6,12.4,6,14c0,3.3,2.7,6,6,6c0.6,0,1.1-0.1,1.6-0.2\n\tC13.7,19.1,13.9,17.8,13.7,17.5z\"/>\n<path class=\"dx_blue\" d=\"M12,8c-1.1,0-2.2,0.3-3.1,0.9c0,0,0.1,0,0.1,0c1,0.2,3.1,0.7,3.1,0.3c0-0.4-0.1-0.9,0.1-0.8\n\tc0.2,0.2,0.8,0.7,0.6,1c-0.2,0.3-0.8,0.6-0.6,0.9c0.2,0.2,0.8,0.6,1,0.4s-0.1-0.9,0.2-0.8c0.3,0,1.8,0.8,1.3,1.1\n\tc-0.5,0.3-1.4,1.9-1.9,2c-0.5,0.1-0.9,0.2-0.8,0.6c0.2,0.5,0.5,0.2,0.7,0.3c0.1,0.1,0.1,0.4,0.3,0.6s0.4,0.1,0.7,0.1\n\tc0.3-0.1,2.5,0.9,2.3,1.4c-0.2,0.5-0.2,1.2-1,2.1c-0.5,0.5-0.7,1.1-0.9,1.5c2.3-0.8,4-3,4-5.6C18,10.7,15.3,8,12,8z\"/>\n</svg>";
})(CustomItems || (CustomItems = {}));

var CustomItems;
(function (CustomItems) {
    var tilesPageItem = (function (_super) {
        __extends(tilesPageItem, _super);
        function tilesPageItem(model, $container, options) {
            var _this = _super.call(this, model, $container, options) || this;
            
            return _this;
        }
        tilesPageItem.prototype.renderContent = function ($element, changeExisting, afterRenderCallback) {
            
            if (!changeExisting || !this._page) {
                this._page = CreateMarkup();

                $element.append(this._page);

                InitPageWidgets();
            }
        };
        return tilesPageItem;
    }(DevExpress.Dashboard.customViewerItem));
    CustomItems.tilesPageItem = tilesPageItem;
})(CustomItems || (CustomItems = {}));

var CustomItems;
(function (CustomItems) {
    var tilesPageItemExtension = (function () {
        function tilesPageItemExtension(dashboardControl) {
            this.name = CustomItems.TILESPAGE_EXTENSION_NAME;
            this.metaData = CustomItems.tilesPageMeta;
            this.createViewerItem = function (model, $element, content) {
                return new CustomItems.tilesPageItem(model, $element, content);
            };
            dashboardControl.registerIcon(CustomItems.TILESPAGE_ICON);
        }
        return tilesPageItemExtension;
    }());
    CustomItems.tilesPageItemExtension = tilesPageItemExtension;
})(CustomItems || (CustomItems = {}));



var imgSrc = "/Images/Sheet.png";

var računiItemsDS = [
    {
        id: "1",
        caption: "Sales Quotes - Open",
        amount: 1,
        imageSrc: imgSrc
    },
    {
        id: "2",
        caption: "Sales Orders - Open",
        amount: 25,
        imageSrc: imgSrc
    }
];

var releasedNotSheepedItemsDS = [
    {
        id: "1",
        caption: "Ready to Sheep",
        amount: 6,
        imageSrc: imgSrc
    },
    {
        id: "2",
        caption: "Partially Sheeped",
        amount: 0,
        imageSrc: imgSrc
    },
    {
        id: "3",
        caption: "Delayed",
        amount: 14,
        imageSrc: imgSrc
    }
];

var returnedItemsDS = [
    {
        id: "1",
        caption: "Sales Return Orders - Open",
        amount: 0,
        imageSrc: imgSrc
    },
    {
        id: "2",
        caption: "Sales Credit Memos - Open",
        amount: 1,
        imageSrc: imgSrc
    }
];

var tileRows = [
    {
        elementId: "forReleaseItems",
        dataSource: računiItemsDS
    },
    {
        elementId: "releasedNotSheepedItems",
        dataSource: releasedNotSheepedItemsDS
    },
    {
        elementId: "returnedItems",
        dataSource: returnedItemsDS
    }
];


function CreateMarkup() {
    return $(
        "<div class='container body-content' style='width: 100%;'>" +
        "<div class='row' >"+
        "<div class='col-md-8'>"+
        "<div class='tilerow-caption'>For Release</div>"+
        "<div id='forReleaseItems'></div>"+
        "</div>"+
        "<div class='col-md-4'>"+
        "<div class='tilerow-links'>"+
        "<a href='#'>New Sales Quote</a>"+
        "<a href='#'>New Sales Order</a>"+
        "</div>"+
        "</div>"+
        "</div >"+
        "<div class='row'>"+
        "<div class='col-md-8'>"+
        "<div class='tilerow-caption'>Sales Orders Released Not Sheeped</div>"+
        "<div id='releasedNotSheepedItems'></div>"+
        "</div>"+
        "<div class='col-md-4'>"+
        "<div class='tilerow-links'>"+
        "<a href='#'>Navigate</a>"+
        "</div>"+
        "</div>"+
        "</div>"+
        "<div class='row'>"+
        "<div class='col-md-8'>"+
        "<div class='tilerow-caption'>Returns</div>"+
        "<div id='returnedItems'></div>"+
        "</div>"+
        "<div class='col-md-4'>"+
        "<div class='tilerow-links'>"+
        "<a href='#'>New Sales Return Order</a>"+
        "<a href='#'>New Sales Credit Memo</a>"+
        "</div>"+
        "</div>"+
        "</div>"+
        "</div >");
}

function InitPageWidgets() {
    tileRows.forEach(function (val) {
        $("#" + val.elementId).dxTileView({
            dataSource: val.dataSource,
            itemTemplate: ItemTemplateHandler,
            height: 120,
            width: "100%",
            baseItemHeight: 98,
            baseItemWidth: 98,
            direction: "horizontal",
            itemMargin: 10,
            onItemClick: function () {
                window.location.href = "http://www.example.com";
            }
        });
    });
}

function ItemTemplateHandler(itemData, itemIndex, itemElement) {
    itemElement
        .append("<div class='tileitem'>" +
            "<div>" +
            "<img class='tileitem-image' src='" + itemData.imageSrc + "' />" +
            "<div class='tileitem-amount'>" + itemData.amount + "</div>" +
            "</div>" +
            "<div class='tileitem-caption'>" + itemData.caption + "</div>" +
            "</div>");
}