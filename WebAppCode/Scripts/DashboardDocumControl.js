var chartDocumentsByDaysComponentName;
var chartDocumentsByNamesComponentName;
var chartDocumentsByNamesComponentName2;

//var openDashboardExtension;

function CustomizeWidgets(s, e) {

    if (e.ItemName === chartDocumentsByDaysComponentName) {

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

            //var $chart2 = $("div[data-layout-item-name=" + chartDocumentsByNamesComponentName + "]");
            //if ($chart2.length === 0) return;

            //if ($chart2.is(":visible")) {
            //    $chart2.hide();
            //} else {
            //    $chart2.show();
            //}
            //CallbackPanel.PerformCallback(2);

            DashboardDocum.LoadDashboard("Dashboard2");
        });
    }
    else
    if (e.ItemName === chartDocumentsByNamesComponentName) {

        var chart2 = e.GetWidget();

        chart2.element().click(function (e) {

            //var $chart2 = $(e.target).parent();
            //$chart2.hide();
        });
    }
    else
    if (e.ItemName === chartDocumentsByNamesComponentName2) {

        var chart21 = e.GetWidget();

        chart21.element().click(function (e) {

            DashboardDocum.LoadDashboard("Dashboard1");
        });
    }
}

function UnsubscribeFromEvents(s, e) {
    if (e.ItemName === chartDocumentsByDaysComponentName) {
        var chart1 = e.GetWidget();
        chart1.option('argumentAxis.label.customizeText', undefined);
    }
    if (e.ItemName === chartDocumentsByNamesComponentName) {
        //var chart2 = args.GetWidget();
        
    }
}

//function ItemClickHandler(args) {
//    console.log("ItemClickHandler", args);
//}

function ASPxCallbackPanel_EndCallback() {
    //openDashboard("Dashboard2");
}

function InitDashboard(s, e) {
    //openDashboardExtension = s.GetDashboardControl().findExtension("dxdde-open");
    DashboardDocum.LoadDashboard("Dashboard1");
}

function openDashboard(id) {

    DashboardDocum.LoadDashboard(id);
}