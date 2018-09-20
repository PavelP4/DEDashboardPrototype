


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
        
        chart1.element().click(function (e) {

            s.LoadDashboard(s.cpDashboard2Name);
        });
    }
    else
        if (e.ItemName === s.cpChart2Name) {

        var chart2 = e.GetWidget();

        chart2.element().click(function (e) {
            
        });
    }
    else
    if (e.ItemName === s.cpChart21Name) {

        var chart21 = e.GetWidget();

        chart21.element().click(function (e) {

            OpenDashboard(s.cpDashboard1Name);
        });
    }
}

function UnsubscribeFromEvents(s, e) {
    if (e.ItemName === s.cpChart1Name) {
        var chart1 = e.GetWidget();
        chart1.option('argumentAxis.label.customizeText', undefined);
    }
}

function OnInitDashboard(s, e) {
    OpenDashboard(s.cpDashboard1Name);
}

function OnItemWidgetCreated(s, e) {
    CustomizeWidgets(s, e);
}

function OnItemWidgetUpdated(s, e) {
    CustomizeWidgets(s, e);
}

function OnItemWidgetUpdating(s, e) {
    UnsubscribeFromEvents(s, e);
}

function OpenDashboard(id) {

    DashboardDocum.LoadDashboard(id);
}