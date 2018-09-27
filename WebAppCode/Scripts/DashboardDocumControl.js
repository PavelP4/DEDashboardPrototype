
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
}

function CustomizeChartProracunskiPodaci(s, e) {
    if (e.ItemName === s.cpChartProracunskiPodaciName) {

        var chart = e.GetWidget();

        //chart.option('argumentAxis.label.customizeText', function (o) {
        //    //if (!o.valueText) return o.valueText;

            
        //    //return o.valueText.substring(0, 40);
        //    //return o.valueText;
        //    console.log(o.valueText,o);
        //});

        chart.option({
            argumentAxis: {
                //valueMarginsEnabled: true,
                //discreteAxisDivisionMode: "crossLabels",
                //grid: {
                //    visible: true
                //},
                label: {
                    //customizeHint: function(e) {
                    //    return "customizeHint";
                    //},
                    //indentFromAxis: 1,
                    //rotationAngle: -20,
                    //staggeringSpacing: 0,
                    //overlappingBehavior: 'stagger',
                    displayMode: 'rotate',
                    rotationAngle: -20
                    //customizeText: function (o) {
                    //    console.log("customizeText", this);
                    //    return o.valueText;
                    //}
                }
            }
            //tooltip: {
            //    enabled: true,
            //    customizeTooltip: function (args) {
            //        console.log("customizeTooltip", args);
            //        return {
            //            html: "<div style='width: 40px;'>qwerw e qwerrr vv xasdas dd fasdf faaaaa dasdf</div>"
            //        };
            //    }
            //}
        });
    }
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