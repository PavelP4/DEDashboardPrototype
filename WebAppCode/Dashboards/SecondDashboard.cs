using DevExpress.DashboardCommon;

namespace WebAppCode.Dashboards
{
    public class SecondDashboard: BaseDashboard
    {
        public const string ChartDocumentsByNamesComponentName2 = "chartDashboardItem_DocumentsByNames2";

        public const string SqlQuery1 = "SELECT dt.naziv, d.datum, au.name FROM pn.Dokument d INNER JOIN pn.DokumentTip dt ON d.id_dokument_tip = dt.id INNER JOIN dit.AppUser au ON d.id_updated_by = au.id WHERE d.datum < '20080101'";
        public const string CustomSqlQueryName1 = "CustomSqlQuery1";


        public SecondDashboard(IDashboardContainer container)
            :base(container)
        {
        }

        protected override void ConfigureDataSourceQueries()
        {
            AddQueryToDataSource(CustomSqlQueryName1, SqlQuery1);
        }

        protected override void Configure()
        {
            Dashboard.Title.Text = "Second Dashboard by code";

            ChartDashboardItem chart2 = CreateChartDocumentsByNames2(DataSource, CustomSqlQueryName1);
            Dashboard.Items.Add(chart2);

            DashboardLayoutItem chart2LayoutItem = new DashboardLayoutItem(chart2, 100);

            DashboardLayoutGroup group1 =
                new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Horizontal, 100, chart2LayoutItem);

            DashboardLayoutGroup rootLayout = new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Vertical, 1,
                group1);
            Dashboard.LayoutRoot = rootLayout;
        }

        private ChartDashboardItem CreateChartDocumentsByNames2(DashboardSqlDataSource dataSource, string queryName)
        {
            ChartDashboardItem chart = new ChartDashboardItem();

            chart.Name = "Documents by users 2";
            chart.ComponentName = ChartDocumentsByNamesComponentName2;
            chart.DataSource = dataSource;
            chart.DataMember = queryName;
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
    }
}