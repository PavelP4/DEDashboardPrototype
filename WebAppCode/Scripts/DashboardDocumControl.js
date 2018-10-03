
var currentDashboardId;
var movementsMap;

function CustomizeWidgets(s, e) {

    if (e.ItemName === s.cpChart1Name) {

        var chart1 = e.GetWidget();

        chart1.option('argumentAxis.label.customizeText', function (o) {
            if (!o.valueText) return o.valueText;
            
            if (o.value instanceof Date) {
                if (o.value.getMonth() === 0 && o.value.getDate() === 1) {
                    return o.value.getFullYear();
                }
                return "";
            }
            return o.valueText;
        });
    }

    CustomizeChartProracunskiPodaci(s, e);
    CustomizeChartChartVrijednostAndIznos(s, e);
    CustomizeChartVrijemeObradeByNadleznost(s, e);
}


function CustomizeChartProracunskiPodaci(s, e) {
    if (e.ItemName === s.cpChartProracunskiPodaciName) {

        var chart = e.GetWidget();

        CustomizeChartXLabels(chart, 3);
        CustomizeChartTooltip(chart, true);
    }
}

function CustomizeChartChartVrijednostAndIznos(s, e) {
    if (e.ItemName === s.cpChartVrijednostAndIznosComponentName) {

        var chart = e.GetWidget();

        CustomizeChartXLabels(chart, 0);
        CustomizeChartTooltip(chart, false);
    }
}

function CustomizeChartVrijemeObradeByNadleznost(s, e) {
    if (e.ItemName === s.cpChartVrijemeObradeByNadleznostComponentName) {

        var chart = e.GetWidget();

        //chart.option('tooltip.enabled', false);
    }
}


var ttLineRegEx = /(?<=&nbsp;&nbsp;).*?[0-9,\.]+.*?(?=<)/gmi;
var ttLineCurrencyDesRegEx = /\skn\s?/g;
var ttLineAmountRegEx = /\s[0-9,\.]+\s?/g;
var ttClearAmountRegEx = /[,\.\s]/g;
var ttNumberDLMRegEx = /[,\.]{1}(\d{2})$/g;
var ttSpacesRegEx = /\s+/g;
const maxSymbols = 15;
const spaceDLM = " ";

function CustomizeChartXLabels(chart, firstLinePlus) {

    var axisXCustomizeText = chart.option('argumentAxis.label.customizeText');
    chart.option('argumentAxis.label.customizeText', function (args) {
        var defaultText = $.proxy(axisXCustomizeText, args)(args);
        if (!defaultText.trim()) return "(Blank)";

        var textArr = defaultText.trim().replace(ttSpacesRegEx, spaceDLM).split(spaceDLM);
        if (textArr.length === 1) return defaultText;

        var lineArr = [];
        var prevLineText = "";
        var curLineText = "";

        textArr.forEach(function (item, i, arr) {
            prevLineText = curLineText;
            curLineText = prevLineText + (curLineText.length === 0 ? item : spaceDLM + item);

            if ((curLineText.length > maxSymbols && lineArr.length > 0)
                || (lineArr.length === 0 && curLineText.length > maxSymbols + firstLinePlus)) {

                lineArr.push(prevLineText);
                curLineText = item;
            }

            if (i === arr.length - 1) {
                lineArr.push(curLineText);
            }
        });

        //lineArr[0] = "<span>" + lineArr[0] + "</span>";
        var resultText = "";
        lineArr.forEach(function (item, i, arr) {
            resultText = resultText + "<span class='chart-xaxis-label'>" + item + "</span>" + (i < arr.length - 1 ? "</br>" : "");
        });
        //resultText = resultText.replace("Upravni odjel", "UO");

        return resultText;
    });

    //redrawOnResize: false
    //encodeHtml: true
    //_renderer.encodeHtml: true

    chart.option({
        encodeHtml: false,
        argumentAxis: {
            label: {
                overlappingBehavior: "none"
            }
        }
    });

    //chart.option({
    //    argumentAxis: {
    //valueMarginsEnabled: true,
    //discreteAxisDivisionMode: "crossLabels",
    //grid: {
    //    visible: true
    //},
    //label: {
    //customizeHint: function(e) {
    //    return "customizeHint";
    //},
    //indentFromAxis: 1,
    //staggeringSpacing: 0,
    //overlappingBehavior: 'stagger',
    //displayMode: 'rotate',
    //rotationAngle: -20
    //}
    //});
}

function CustomizeChartTooltip(chart, showAmountPercents) {

    var tooltipCustomizeTooltip = chart.option('tooltip.customizeTooltip');
    chart.option('tooltip.customizeTooltip', function (args) {
        var originObj = $.proxy(tooltipCustomizeTooltip, args)(args);
        var total = args.total;
        var resultHtml = originObj.html.replace(ttLineRegEx,
            function (r) {
                var rAmount = r.match(ttLineAmountRegEx);
                //var rAmount = /\s[0-9,\.]+$/g.exec(r);
                if (!rAmount) return "";

                var itemAmountTextDef = rAmount[0].trim();

                r = r.replace(ttLineCurrencyDesRegEx, " ")
                    .replace(": ", ": HRK ");
                r = r.replace(itemAmountTextDef,
                    itemAmountTextDef
                    .replace(ttNumberDLMRegEx, "_$1")
                    .replace(ttClearAmountRegEx, ".")
                    .replace("_", ","));

                if (showAmountPercents) {
                    var itemAmount = TextToNumber(itemAmountTextDef);
                    return r + " (" + ((itemAmount / total) * 100).toFixed(2).replace(".", ",") + "%)";
                }

                return r;
            });

        return {
            html: resultHtml
        };
    });
}


// Must be replaced by internal localization mechanism
function TextToNumber(text) {
    
    var t = text.replace(ttNumberDLMRegEx, "_$1");
    t = t.replace(ttClearAmountRegEx, "");
    
    var value = Number(t.replace("_", "."));
    if (isNaN(value)) {
        value = Number(t.replace("_", ","));
    }

    return value;
}

function UnsubscribeFromEvents(s, e) {
    if (e.ItemName === s.cpChart1Name) {
        var chart1 = e.GetWidget();
        chart1.option('argumentAxis.label.customizeText', undefined);
    }

    if (e.ItemName === s.cpChartProracunskiPodaciName) {
        var chart = e.GetWidget();
        chart.option('argumentAxis.label.customizeText', undefined);
    }
}

function RegisterMovementsByMap(s, e) {

    var mapValue = movementsMap[currentDashboardId + "." + e.ItemName];
    if (!mapValue) return;

    var widget = e.GetWidget();
    if (!widget) return;
    
    widget.element().click(function (e) {
        OpenDashboard(mapValue);
    });
}

function InitMovementsMap(source) {

    movementsMap = {};

    if (source.cpDashboardMovementsMap) {
        var parsed = JSON.parse(source.cpDashboardMovementsMap);

        for (var key in parsed) {
            if (parsed.hasOwnProperty(key)) {
                var val = parsed[key];
                var keyArr = key.split(".");

                if (keyArr.length !== 2) continue;

                var dashboardId = keyArr[0];
                var widgetId = keyArr[1];

                movementsMap[source[dashboardId] + "." + source[widgetId]] = source[val];
            }
        }
    }
}

function OnInitDashboard(s, e) {

    InitMovementsMap(s);

    OpenDashboard(s.cpInitialDashboard);
}

function OnItemWidgetCreated(s, e) {
    CustomizeWidgets(s, e);
    RegisterMovementsByMap(s, e);
}

function OnItemWidgetUpdated(s, e) {
    CustomizeWidgets(s, e);
    RegisterMovementsByMap(s, e);
}

function OnItemWidgetUpdating(s, e) {
    UnsubscribeFromEvents(s, e);
}

function OnBeforeRender(s, e) {
    var dashboardControl = s.GetDashboardControl();
    dashboardControl.registerExtension(new CustomItems.WebPageItemExtension(dashboardControl));
    //dashboardControl.registerExtension(new CustomItems.TilesPageItemExtension(dashboardControl));
}

function OpenDashboard(id) {

    DashboardDocum.LoadDashboard(id);
    currentDashboardId = id;
}

function OnDashboardEndUpdate() {
}