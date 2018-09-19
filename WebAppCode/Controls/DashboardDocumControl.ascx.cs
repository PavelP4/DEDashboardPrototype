using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.Sql;
using DevExpress.Web;
using WebAppCode.Dashboards;
using WebAppCode.Providers;


namespace WebAppCode.Controls
{
    public partial class DashboardDocumControl : System.Web.UI.UserControl
    {
        public const string SqlQuery1 = "SELECT dt.naziv, d.datum, au.name FROM pn.Dokument d INNER JOIN pn.DokumentTip dt ON d.id_dokument_tip = dt.id INNER JOIN dit.AppUser au ON d.id_updated_by = au.id WHERE d.datum < '20080101'";
        public const string CustomSqlQueryName1 = "CustomSqlQuery1";

        public const string Dashboard1Name = "Dashboard1";
        public const string Dashboard2Name = "Dashboard2";

        //public const string ChartDocumentsByDaysComponentName = "chartDashboardItem_DocumentsByDays";
        //public const string ChartDocumentsByNamesComponentName = "chartDashboardItem_DocumentsByNames";
        public const string ChartDocumentsByNamesComponentName2 = "chartDashboardItem_DocumentsByNames2";

        //private DashboardInMemoryStorage DashboardStorage
        //{
        //    get { return (DashboardInMemoryStorage)Session["ВashboardStorage"]; }
        //    set { Session["ВashboardStorage"] = value; }
        //}

        //private DashboardSqlDataSource DashboardDataSource
        //{
        //    get { return (DashboardSqlDataSource)Session["DashboardDataSource"]; }
        //    set { Session["DashboardDataSource"] = value; }
        //}

        //public void RegisterDashboard(IDashboard dashboard)
        //{
        //    Dictionary<string, IDashboard> dict 
        //        dict[dashboard.Id] 
        //        dashboard.PassData(this);
        //}

        private IDashboardContainer Container
        {
            get { return (IDashboardContainer)Session["DashboardContainer"]; }
            set { Session["DashboardContainer"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region .Control settings.

        public string Width
        {
            get { return ASPxDashboardDocum.Width.ToString(); }
            set { ASPxDashboardDocum.Width = new Unit(value); }
        }

        public string Height
        {
            get { return ASPxDashboardDocum.Height.ToString(); }
            set { ASPxDashboardDocum.Height = new Unit(value); }
        }

        #endregion

        #region .Container configuration.
        
        //private void ConfigureDashboardContainer()
        //{
        //    ASPxDashboardDocum.EnableCustomSql = true;
        //    ASPxDashboardDocum.AllowExecutingCustomSql = true;
        //    ASPxDashboardDocum.AllowCreateNewDashboard = false;
        //    ASPxDashboardDocum.AllowOpenDashboard = false;
        //    ASPxDashboardDocum.LoadDefaultDashboard = false;
        //}

        //private void SetDashboardConnectionStringsProvider()
        //{
        //    //ASPxDashboardDocum.SetConnectionStringsProvider(new ConfigFileConnectionStringsProvider());
        //    ASPxDashboardDocum.SetConnectionStringsProvider(new DashboardConnectionStringsProvider());
        //}

        //private void ConfigureDashboardStorages()
        //{
        //    DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();
        //    ConfigureDataSources(dataSourceStorage);
        //    ASPxDashboardDocum.SetDataSourceStorage(dataSourceStorage);

        //    DashboardInMemoryStorage dashboardStorage = new DashboardInMemoryStorage();
        //    ASPxDashboardDocum.SetDashboardStorage(dashboardStorage);

        //    DashboardStorage = dashboardStorage;
        //}

        //private void ConfigureDataSources(DataSourceInMemoryStorage dataSourceStorage)
        //{
        //    DashboardSqlDataSource dataSource1 =
        //        new DashboardSqlDataSource("DataSource1", DashboardConnectionStringsProvider.MsSqlConnectionName);

        //    CustomSqlQuery customSqlQuery = new CustomSqlQuery(CustomSqlQueryName1, SqlQuery1);
        //    dataSource1.Queries.Add(customSqlQuery);

        //    dataSourceStorage.RegisterDataSource(dataSource1.Name, dataSource1.SaveToXml());

        //    DashboardDataSource = dataSource1;
        //}

        #endregion

        #region .Create dashboards.

        //private void SetInitialDashboard()
        //{
        //    CreateInitialDashboard(DashboardStorage, DashboardDataSource);

        //    ASPxDashboardDocum.InitialDashboardId = ((IDashboardStorage)DashboardStorage)
        //        .GetAvailableDashboardsInfo().First().ID;
        //}

        //private void CreateInitialDashboard(DashboardInMemoryStorage storage, DashboardSqlDataSource dataSource)
        //{
        //    Dashboard dashboard = new Dashboard();

        //    ConfigureInitialDashboard(dashboard, dataSource);
          
        //    storage.RegisterDashboard(Dashboard1Name, dashboard.SaveToXDocument());
        //}

        //private void ConfigureInitialDashboard(Dashboard dashboard, DashboardSqlDataSource dataSource)
        //{
        //    dashboard.DataSources.Add(dataSource);
        //    dashboard.Title.Text = "Dashboard by code";

        //    ChartDashboardItem chart1 = CreateChartDocumentsByDays(dataSource);
        //    dashboard.Items.Add(chart1);
        //    ChartDashboardItem chart2 = CreateChartDocumentsByNames(dataSource);
        //    dashboard.Items.Add(chart2);

        //    DashboardLayoutItem chart1LayoutItem = new DashboardLayoutItem(chart1, 100);
        //    DashboardLayoutItem chart2LayoutItem = new DashboardLayoutItem(chart2, 100);

        //    DashboardLayoutGroup group1 =
        //        new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Horizontal, 100, chart1LayoutItem);
            
        //    DashboardLayoutGroup rootLayout = new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Vertical, 1, 
        //        group1,chart2LayoutItem);
        //    dashboard.LayoutRoot = rootLayout;
        //}

        private void CreateSecondDashboard(DashboardInMemoryStorage storage, DashboardSqlDataSource dataSource)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.EnableAutomaticUpdates = true;

            ConfigureSecondDashboard(dashboard, dataSource);

            storage.RegisterDashboard(Dashboard2Name, dashboard.SaveToXDocument());
            //((IDashboardStorage)DashboardStorage).SaveDashboard(Dashboard2Name, dashboard.SaveToXDocument());
        }
        private void ConfigureSecondDashboard(Dashboard dashboard, DashboardSqlDataSource dataSource)
        {
            dashboard.DataSources.Add(dataSource);
            dashboard.Title.Text = "Second Dashboard by code";

            ChartDashboardItem chart2 = CreateChartDocumentsByNames2(dataSource);
            dashboard.Items.Add(chart2);

            DashboardLayoutItem chart2LayoutItem = new DashboardLayoutItem(chart2, 100);

            DashboardLayoutGroup group1 =
                new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Horizontal, 100, chart2LayoutItem);

            DashboardLayoutGroup rootLayout = new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Vertical, 1,
                group1);
            dashboard.LayoutRoot = rootLayout;
        }
        #endregion

        #region .Create widgets.

        //private ChartDashboardItem CreateChartDocumentsByDays(DashboardSqlDataSource dataSource)
        //{
        //    ChartDashboardItem chart = new ChartDashboardItem();
           
        //    chart.Name = "Documents by days";
        //    chart.ComponentName = ChartDocumentsByDaysComponentName;
        //    chart.DataSource = dataSource;
        //    chart.DataMember = CustomSqlQueryName1;
        //    chart.ShowCaption = true;
        //    chart.Legend.Visible = true;

        //    Dimension xDimension = new Dimension("datum", DateTimeGroupInterval.DayMonthYear);
        //    chart.Arguments.Add(xDimension);           
            
        //    Dimension seriesDimension = new Dimension("naziv");
        //    chart.SeriesDimensions.Add(seriesDimension);

        //    ChartPane pane = new ChartPane();
        //    chart.Panes.Add(pane);
        //    SimpleSeries nazivCountSeries = new SimpleSeries(SimpleSeriesType.StackedLine);
        //    Measure yMeasure = new Measure("naziv", SummaryType.Count);
        //    nazivCountSeries.Value = yMeasure;
        //    pane.Series.Add(nazivCountSeries);
            
        //    pane.PrimaryAxisY.Title = "Docum counts";
        //    pane.PrimaryAxisY.TitleVisible = true;

        //    chart.AxisX.EnableZooming = true;
        //    chart.AxisX.Title = "Years";
        //    chart.AxisX.TitleVisible = true;

        //    return chart;
        //}
        //private ChartDashboardItem CreateChartDocumentsByNames(DashboardSqlDataSource dataSource)
        //{
        //    ChartDashboardItem chart = new ChartDashboardItem();

        //    chart.Name = "Documents by users";
        //    chart.ComponentName = ChartDocumentsByNamesComponentName;
        //    chart.DataSource = dataSource;
        //    chart.DataMember = CustomSqlQueryName1;
        //    chart.ShowCaption = true;

        //    Dimension xDimension = new Dimension("name");
        //    chart.Arguments.Add(xDimension);

        //    Dimension seriesDimension = new Dimension("naziv");
        //    chart.SeriesDimensions.Add(seriesDimension);

        //    ChartPane pane = new ChartPane();
        //    chart.Panes.Add(pane);
        //    SimpleSeries nazivCountSeries = new SimpleSeries(SimpleSeriesType.StackedBar);
        //    Measure yMeasure = new Measure("naziv", SummaryType.Count);
        //    nazivCountSeries.Value = yMeasure;
        //    pane.Series.Add(nazivCountSeries);

        //    pane.PrimaryAxisY.Title = "Docum counts";
        //    pane.PrimaryAxisY.TitleVisible = true;

        //    chart.AxisX.EnableZooming = true;

        //    return chart;
        //}

        private ChartDashboardItem CreateChartDocumentsByNames2(DashboardSqlDataSource dataSource)
        {
            ChartDashboardItem chart = new ChartDashboardItem();

            chart.Name = "Documents by users 2";
            chart.ComponentName = ChartDocumentsByNamesComponentName2;
            chart.DataSource = dataSource;
            chart.DataMember = CustomSqlQueryName1;
            chart.ShowCaption = true;

            Dimension xDimension = new Dimension("name");
            chart.Arguments.Add(xDimension);

            Dimension seriesDimension = new Dimension("naziv");
            chart.SeriesDimensions.Add(seriesDimension);

            ChartPane pane = new ChartPane();
            chart.Panes.Add(pane);
            SimpleSeries nazivCountSeries = new SimpleSeries(SimpleSeriesType.StackedBar);
            Measure yMeasure = new Measure("naziv", SummaryType.Count);
            nazivCountSeries.Value = yMeasure;
            pane.Series.Add(nazivCountSeries);

            pane.PrimaryAxisY.Title = "Docum counts";
            pane.PrimaryAxisY.TitleVisible = true;

            chart.AxisX.EnableZooming = false;

            return chart;
        }
        #endregion

        #region .Dashboard events.

        protected void ASPxDashboardDocum_OnInit(object sender, EventArgs e)
        {
            if (Container != null) // callback workaround
            {
                Container.Configure();
            }

            if (!((ASPxDashboard)sender).IsCallback)
            {
                Container = new DashboardContainer(ASPxDashboardDocum);
                Container.Configure();

                Container.RegisterDashboard(Dashboard1Name, new FirstDashboard(Container));
            }
        }

        protected void ASPxDashboardDocum_DashboardLoading(object sender, DashboardLoadingWebEventArgs e)
        {
            string dashboardId = e.DashboardId;

            Container.ConfigureDashboard(dashboardId);

            //if (dashboardId == Dashboard1Name && !IsDashboardCreated(dashboardId))
            //{
            //    var firstDashboard = new FirstDashboard(DashboardDataSource, CustomSqlQueryName1);
            //    firstDashboard.RegisterDashboard(Dashboard1Name, DashboardStorage);
            //}
            //else
            //if (dashboardId == Dashboard2Name && !IsDashboardCreated(dashboardId))
            //{
            //    CreateSecondDashboard(DashboardStorage, DashboardDataSource);
            //}

            e.DashboardXml = Container.LoadDashboard(dashboardId);
        }

        protected void ASPxDashboardDocum_OnCustomJSProperties(object sender, CustomJSPropertiesEventArgs e)
        {
            ASPxDashboard s = (ASPxDashboard) sender;

            s.JSProperties.Add("cpDashboard1Name", Dashboard1Name);
            s.JSProperties.Add("cpDashboard2Name", Dashboard2Name);

            s.JSProperties.Add("cpChart1Name", FirstDashboard.ChartDocumentsByDaysComponentName);
            s.JSProperties.Add("cpChart2Name", FirstDashboard.ChartDocumentsByNamesComponentName);
            s.JSProperties.Add("cpChart21Name", ChartDocumentsByNamesComponentName2);
        }

        #endregion

        #region .Service functions.

        //private bool IsDashboardCreated(string dashboardId)
        //{
        //    return ((IDashboardStorage)DashboardStorage)
        //        .GetAvailableDashboardsInfo()
        //        .FirstOrDefault(x => x.ID == dashboardId) != null;
        //}

        //private XDocument LoadDashboard(string dashboardId)
        //{
        //    return ((IDashboardStorage)DashboardStorage).LoadDashboard(dashboardId);
        //}

        #endregion
        
    }
}