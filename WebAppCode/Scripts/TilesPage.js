var imgSrc = "/Images/Sheet.png";

var računiItemsDS = [
    {
        Id: "1",
        Caption: "Na odobrenju",
        Amount: 0,
        ImageSrc: imgSrc
    }, 
    {
        Id: "2",
        Caption: "Na predovjeri",
        Amount: 0,
        ImageSrc: imgSrc
    }
];

var releasedNotSheepedItemsDS = [
    {
        Id: "1",
        Caption: "Ready to Ship",
        Amount: 6,
        ImageSrc: imgSrc
    },
    {
        Id: "2",
        Caption: "Partially Shipped",
        Amount: 0,
        ImageSrc: imgSrc
    },
    {
        Id: "3",
        Caption: "Delayed",
        Amount: 14,
        ImageSrc: imgSrc
    }
];

var returnedItemsDS = [
    {
        Id: "1",
        Caption: "Sales Return Orders - Open",
        Amount: 0,
        ImageSrc: imgSrc
    },
    {
        Id: "2",
        Caption: "Sales Credit Memos - Open",
        Amount: 1,
        ImageSrc: imgSrc
    }
];

var tileRows = [
    {
        elementId: "računiItems",
        dataSource: računiItemsDS,
        dataSourceUrl: "api/tilespage/racunitiles"
    },
    {
        elementId: "releasedNotSheepedItems",
        dataSource: releasedNotSheepedItemsDS,
        dataSourceUrl: "api/tilespage/releasedNotSheepedtiles"
    },
    {
        elementId: "returnedItems",
        dataSource: returnedItemsDS,
        dataSourceUrl: "api/tilespage/returnedtiles"
    }
];

$(function () {

    tileRows.forEach(function(val, index) {
        $("#" + val.elementId).dxTileView({
            dataSource: index === 0 ? val.dataSourceUrl : val.dataSource,
            itemTemplate: ItemTemplateHandler,
            height: 120,
            width: "100%",
            baseItemHeight: 98,
            baseItemWidth: 98,
            direction: "horizontal",
            itemMargin: 10,
            onItemClick: function (s) {
                window.location.href = s.itemData.Href;
            }
        });
    });
});

function ItemTemplateHandler(itemData, itemIndex, itemElement) {
    itemElement
        .append("<div class='tileitem'>" +
                    "<div>" +
                        "<img class='tileitem-image' src='" + imgSrc + "' />" +
                        "<div class='tileitem-amount'>" + itemData.Amount + "</div>" +
                    "</div>" +
                    "<div class='tileitem-caption'>" + itemData.Caption + "</div>" +
                "</div>");
}