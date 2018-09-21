var imgSrc = "/Images/Sheet.png";

var forReleaseItemsDS = [
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
        dataSource: forReleaseItemsDS
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

$(function () {

    tileRows.forEach(function(val) {
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
});

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