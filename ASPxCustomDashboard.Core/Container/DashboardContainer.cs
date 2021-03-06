﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ASPxCustomDashboard.Core.Dashboards;
using ASPxCustomDashboard.Core.Providers;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;

namespace ASPxCustomDashboard.Core.Container
{
    public class DashboardContainer: IDashboardContainer
    {
        private ASPxDashboard _aspxDashboard;
        private readonly DataSourceInMemoryStorage _dataSourceStorage;
        private readonly DashboardInMemoryStorage _dashboardStorage;
        private readonly DashboardConnectionStringsProvider _connectionProvider;

        private readonly IDictionary<string, ICustomDashboard> _customDashboards;

        public DashboardContainer(ASPxDashboard aspxDashboard)
        {
            _aspxDashboard = aspxDashboard;

            _dataSourceStorage = new DataSourceInMemoryStorage();
            _dashboardStorage = new DashboardInMemoryStorage();

            _connectionProvider = new DashboardConnectionStringsProvider();

            _customDashboards = new Dictionary<string, ICustomDashboard>();

            _aspxDashboard.SetDataSourceStorage(_dataSourceStorage);
            _aspxDashboard.SetDashboardStorage(_dashboardStorage);
        }

        public DashboardContainer()
            :this(new ASPxDashboard())
        {
        }

        public void Configure(bool configureAllDashboards = false)
        {
            SetDashboardConnectionStringsProvider();
            ConfigureDashboardContainer();

            if (configureAllDashboards)
            {
                ConfigureDashboards();
            }
        }

        public void ConfigureDashboardContainer()
        {
            _aspxDashboard.EnableCustomSql = true;
            _aspxDashboard.AllowExecutingCustomSql = true;
            _aspxDashboard.AllowCreateNewDashboard = false;
            _aspxDashboard.AllowOpenDashboard = false;
            _aspxDashboard.LoadDefaultDashboard = false;
        }

        public void SetDashboardConnectionStringsProvider()
        {
            //_aspxDashboard.SetConnectionStringsProvider(new ConfigFileConnectionStringsProvider());
            _aspxDashboard.SetConnectionStringsProvider(_connectionProvider);
        }

        public void ConfigureDashboards()
        {
            foreach (var dashboard in _customDashboards)
            {
                dashboard.Value.ConfigureDashboard(dashboard.Key);
            }
        }

        public void ConfigureDashboard(string dashboardId)
        {
            ICustomDashboard value;
            if (_customDashboards.TryGetValue(dashboardId, out value))
            {
                value.ConfigureDashboard(dashboardId);
            }
        }

        public void RegisterDashboard(string dashboardId, ICustomDashboard dashboard)
        {
            _customDashboards.Add(dashboardId, dashboard);
        }

        public void RegisterDashboard(string dashboardId, Type dashboardType)
        {
            var instance = CreateCustomDashboardInstance(dashboardType);
            if (instance == null) return;
            _customDashboards.Add(dashboardId, instance);
        }

        public void RegisterDashboard<T>(string dashboardId)
        {
            RegisterDashboard(dashboardId, typeof(T));
        }

        public void RegisterDashboard(string dashboardId, Dashboard dashboard)
        {
            _dashboardStorage.RegisterDashboard(dashboardId, dashboard.SaveToXDocument());
        }

        public void RegisterDataSource(string name, DashboardSqlDataSource dataSource)
        {
            _dataSourceStorage.RegisterDataSource(name, dataSource.SaveToXml());
        }

        public void RegisterDbConnectionParams(string name, DataConnectionParametersBase parameters)
        {
            _connectionProvider.AddConnectionParams(name, parameters);
        }

        public XDocument LoadDashboard(string dashboardId)
        {
            return ((IDashboardStorage)_dashboardStorage).LoadDashboard(dashboardId);
        }

        public bool IsDashboardCreated(string dashboardId)
        {
            return ((IDashboardStorage)_dashboardStorage)
                   .GetAvailableDashboardsInfo()
                   .FirstOrDefault(x => x.ID == dashboardId) != null;
        }

        public void UpdateContainerComponent(ASPxDashboard component)
        {
            _aspxDashboard = component;
        }

        private ICustomDashboard CreateCustomDashboardInstance(Type type)
        {
            var constructor = type.GetConstructor(new Type[]{ typeof(IDashboardContainer) });

            if (constructor == null) return null;

            return constructor.Invoke(new object[]{this}) as ICustomDashboard;
        }
    }
}