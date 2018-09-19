using System.Xml.Linq;
using DevExpress.DashboardCommon;

namespace WebAppCode.Dashboards
{
    public interface IDashboardContainer
    {
        void Configure(bool configureAllDashboards = false);
        void ConfigureDashboardContainer();
        void SetDashboardConnectionStringsProvider();
        void ConfigureDashboards();
        void ConfigureDashboard(string dashboardId);
        void RegisterDashboard(string name, ICustomDashboard dashboard);
        void RegisterDashboard(string name, Dashboard dashboard);
        void RegisterDataSource(string name, DashboardSqlDataSource dataSource);
        XDocument LoadDashboard(string dashboardId);
        bool IsDashboardCreated(string dashboardId);
    }
}
