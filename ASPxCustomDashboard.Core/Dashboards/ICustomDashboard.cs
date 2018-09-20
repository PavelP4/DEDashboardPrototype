namespace ASPxCustomDashboard.Core.Dashboards
{
    public interface ICustomDashboard
    {
        string Title { get; set; }
        string DashboardId { get; }

        void ConfigureDashboard(string dashboardId);
    }
}
