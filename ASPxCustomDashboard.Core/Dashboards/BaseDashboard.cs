﻿using System.Collections.Generic;
using System.Linq;
using ASPxCustomDashboard.Core.Container;
using ASPxCustomDashboard.Core.Providers;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess.Sql;

namespace ASPxCustomDashboard.Core.Dashboards
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

        protected virtual void RegisterDataSource(string dashboardId, string connectionName)
        {
            if (!_sqlQueries.Any()) return;

            _dataSource = new DashboardSqlDataSource(dashboardId + "_DataSource",
                connectionName);
          
            _dataSource.Queries.AddRange(_sqlQueries.Select(x => new CustomSqlQuery(x.Key, x.Value)));
            _dashboardContainer.RegisterDataSource(_dataSource.Name, _dataSource);

            _dashboard.DataSources.Add(DataSource);
        }

        public void ConfigureDashboard(string dashboardId)
        {
            if (IsConfigured) return;

            ConfigureDataSourceQueries();
            RegisterDataSource(dashboardId, DashboardConnectionStringsProvider.MsSqlConnectionName);

            Configure();

            RegisterDashboard(dashboardId);

            IsConfigured = true;
            _dashboardId = dashboardId;
        }

        protected abstract void ConfigureDataSourceQueries();

        protected abstract void Configure();

        protected virtual ComboBoxDashboardItem CreateComboBoxFilter(string queryName, string name, string field)
        {
            var cb = new ComboBoxDashboardItem();

            cb.Name = name;
            cb.ComboBoxType = ComboBoxDashboardItemType.Standard;
            cb.DataSource = DataSource;
            cb.DataMember = queryName;
            cb.ShowCaption = true;
            cb.EnableSearch = true;

            cb.GetDataMembers().Add(field);

            Dimension fdim = new Dimension(field);
            fdim.Name = name;
            cb.FilterDimensions.Add(fdim);

            return cb;
        }
    }
}