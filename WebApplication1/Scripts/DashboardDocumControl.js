


function OnBeforeRender(s, e) {
    var dashboardControl = s.GetDashboardControl();
    dashboardControl.registerExtension(new CustomItems.WebPageItemExtension(dashboardControl));
}
