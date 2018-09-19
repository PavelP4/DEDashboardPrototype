using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess.Sql;
using WebAppCode.Providers;

namespace WebAppCode.Dashboards
{
    public abstract class BaseDashboard: ICustomDashboard
    {
        private string _dashboardId;

        private readonly Dashboard _dashboard;
        private readonly IDashboardContainer _dashboardContainer;

        private DashboardSqlDataSource _dataSource;
        private readonly IDictionary<string, string> _sqlQueries;

        private bool IsConfigured = false;

        public Dashboard Dashboard {
            get { return _dashboard; }
        }

        public IDashboardContainer DashboardContainer
        {
            get { return _dashboardContainer; }
        }

        public string DashboardId
        {
            get { return _dashboardId; }
        }

        public DashboardSqlDataSource DataSource
        {
            get { return _dataSource; }
        }

        public string Title
        {
            get { return _dashboard.Title.Text; }
            set { _dashboard.Title.Text = value; }
        }

        public BaseDashboard(IDashboardContainer dashboardContainer)
        {
            _dashboard = new Dashboard();
            _dashboardContainer = dashboardContainer;
            _sqlQueries = new Dictionary<string, string>();
        }

        protected void RegisterDashboard(string dashboardId)
        {
            _dashboardContainer.RegisterDashboard(dashboardId, _dashboard);
        }

        protected void AddQueryToDataSource(string queryName, string querySql)
        {
            _sqlQueries.Add(queryName, querySql);
        }

        protected void RegisterDataSource(string dashboardId)
        {
            _dataSource = new DashboardSqlDataSource(dashboardId + "_DataSource",
                DashboardConnectionStringsProvider.MsSqlConnectionName);
          
            _dataSource.Queries.AddRange(_sqlQueries.Select(x => new CustomSqlQuery(x.Key, x.Value)));
            _dashboardContainer.RegisterDataSource(_dataSource.Name, _dataSource);

            _dashboard.DataSources.Add(DataSource);
        }

        public void ConfigureDashboard(string dashboardId)
        {
            if (IsConfigured) return;

            ConfigureDataSourceQueries();
            RegisterDataSource(dashboardId);

            Configure();

            RegisterDashboard(dashboardId);

            IsConfigured = true;
            _dashboardId = dashboardId;
        }

        protected abstract void ConfigureDataSourceQueries();

        protected abstract void Configure();
    }
}