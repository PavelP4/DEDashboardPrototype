using DevExpress.DashboardCommon;

namespace WebAppCode.Dashboards
{
    public class FirstDashboard: BaseDashboard
    {
        public const string ChartDocumentsByDaysComponentName = "chartDashboardItem_DocumentsByDays";
        public const string ChartDocumentsByNamesComponentName = "chartDashboardItem_DocumentsByNames";

        public const string SqlQuery1 = "SELECT dt.naziv, d.datum, au.name FROM pn.Dokument d INNER JOIN pn.DokumentTip dt ON d.id_dokument_tip = dt.id INNER JOIN dit.AppUser au ON d.id_updated_by = au.id WHERE d.datum < '20080101'";
        public const string CustomSqlQueryName1 = "CustomSqlQuery1";


        public FirstDashboard(IDashboardContainer container)
            :base(container)
        {
        }

        protected override void ConfigureDataSourceQueries()
        {
            AddQueryToDataSource(CustomSqlQueryName1, SqlQuery1);
        }

        protected override void Configure()
        {
            Dashboard.DataSources.Add(DataSource);
            Dashboard.Title.Text = "First Dashboard";

            ChartDashboardItem chart1 = CreateChartDocumentsByDays(DataSource, SqlQuery1);
            Dashboard.Items.Add(chart1);
            ChartDashboardItem chart2 = CreateChartDocumentsByNames(DataSource, SqlQuery1);
            Dashboard.Items.Add(chart2);

            DashboardLayoutItem chart1LayoutItem = new DashboardLayoutItem(chart1, 100);
            DashboardLayoutItem chart2LayoutItem = new DashboardLayoutItem(chart2, 100);

            DashboardLayoutGroup group1 =
                new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Horizontal, 100, chart1LayoutItem);

            DashboardLayoutGroup rootLayout = new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Vertical, 1,
                group1, chart2LayoutItem);
            Dashboard.LayoutRoot = rootLayout;
        }

        private ChartDashboardItem CreateChartDocumentsByDays(DashboardSqlDataSource dataSource, string queryName)
        {
            ChartDashboardItem chart = new ChartDashboardItem();

            chart.Name = "Documents by days";
            chart.ComponentName = ChartDocumentsByDaysComponentName;
            chart.DataSource = dataSource;
            chart.DataMember = queryName;
            chart.ShowCaption = true;
            chart.Legend.Visible = true;

            Dimension xDimension = new Dimension("datum", DateTimeGroupInterval.DayMonthYear);
            chart.Arguments.Add(xDimension);

            Dimension seriesDimension = new Dimension("naziv");
            chart.SeriesDimensions.Add(seriesDimension);

            ChartPane pane = new ChartPane();
            chart.Panes.Add(pane);
            SimpleSeries nazivCountSeries = new SimpleSeries(SimpleSeriesType.StackedLine);
            Measure yMeasure = new Measure("naziv", SummaryType.Count);
            nazivCountSeries.Value = yMeasure;
            pane.Series.Add(nazivCountSeries);

            pane.PrimaryAxisY.Title = "Docum counts";
            pane.PrimaryAxisY.TitleVisible = true;

            chart.AxisX.EnableZooming = true;
            chart.AxisX.Title = "Years";
            chart.AxisX.TitleVisible = true;

            return chart;
        }

        private ChartDashboardItem CreateChartDocumentsByNames(DashboardSqlDataSource dataSource, string queryName)
        {
            ChartDashboardItem chart = new ChartDashboardItem();

            chart.Name = "Documents by users";
            chart.ComponentName = ChartDocumentsByNamesComponentName;
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

            chart.AxisX.EnableZooming = true;

            return chart;
        }


    }
}