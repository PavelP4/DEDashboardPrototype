
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
    else
    if (e.ItemName === s.cpChart2Name) {
    }
    else
    if (e.ItemName === s.cpChart21Name) {
        
    }


}

function UnsubscribeFromEvents(s, e) {
    if (e.ItemName === s.cpChart1Name) {
        var chart1 = e.GetWidget();
        chart1.option('argumentAxis.label.customizeText', undefined);
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

    OpenDashboard(s.cpDashboard1Name);
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

function OpenDashboard(id) {

    DashboardDocum.LoadDashboard(id);
    currentDashboardId = id;
}