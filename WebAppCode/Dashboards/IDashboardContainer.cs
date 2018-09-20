using System;
using System.Xml.Linq;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;

namespace WebAppCode.Dashboards
{
    public interface IDashboardContainer
    {
        void Configure(bool configureAllDashboards = false);
        void ConfigureDashboardContainer();
        void SetDashboardConnectionStringsProvider();
        void ConfigureDashboards();
        void ConfigureDashboard(string dashboardId);
        void RegisterDashboard(string dashboardId, ICustomDashboard dashboard);
        void RegisterDashboard(string dashboardId, Type dashboardType);
        void RegisterDashboard<T>(string dashboardId);
        void RegisterDashboard(string dashboardId, Dashboard dashboard);
        void RegisterDataSource(string name, DashboardSqlDataSource dataSource);
        void RegisterDbConnectionParams(string name, DataConnectionParametersBase parameters);
        XDocument LoadDashboard(string dashboardId);
        bool IsDashboardCreated(string dashboardId);
        void UpdateContainerComponent(ASPxDashboard component);
    }
}
