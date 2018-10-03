using System.Drawing;
using ASPxCustomDashboard.Core.Container;
using ASPxCustomDashboard.Core.Enums;
using ASPxCustomDashboard.Core.Providers;
using DevExpress.DashboardCommon;

namespace ASPxCustomDashboard.Core.Dashboards
{
    public class VrijemeObradeDashboard : BaseDashboard
    {
        public const string ChartVrijemeObradeByNadleznostComponentName = "chartVrijemeObradeByNadleznost";
        public const string ChartVrijemeObradeByRjesavateljComponentName = "chartVrijemeObradeByRjesavatelj";

        public const string SqlQuery1 = @"SELECT [minuta]
                                              ,[korisnik]      
                                              ,[nadleznost]
                                              ,[dokument]
                                              , 1 as onevalue
                                          FROM [dbo].[dvwDnevnikRada]";
        public const string SqlQuery2 = @"SELECT [minuta]
                                              ,[korisnik]      
                                              ,[nadleznost]
                                              ,[dokument]
                                          FROM [dbo].[dvwDnevnikRada]";

        public const string CustomSqlQueryName1 = "CustomSqlQuery1";
        public const string CustomSqlQueryName2 = "CustomSqlQuery2";


        public VrijemeObradeDashboard(IDashboardContainer container)
            :base(container)
        {
        }

        protected override void ConfigureDataSourceQueries()
        {
            AddQueryToDataSource(CustomSqlQueryName1, SqlQuery1);
            AddQueryToDataSource(CustomSqlQueryName2, SqlQuery2);
        }

        protected override void RegisterDataSource(string dashboardId, string connectionName)
        {
            base.RegisterDataSource(dashboardId, DashboardConnectionStringsProvider.EPlanNabave41ReplicaConnectionName);
        }

        protected override void Configure()
        {
            Dashboard.Title.Text = "Proračunski podaci"; 

            ChartDashboardItem chartVrijemeObradeByNadleznost = CreateChartVrijemeObradeByNadleznost(CustomSqlQueryName1);
            ChartDashboardItem chartVrijemeObradeByRjesavatelj = CreateChartVrijemeObradeByRjesavatelj(CustomSqlQueryName2);

            ComboBoxDashboardItem cbNadleznostFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Nadležnost", "nadleznost");
            ComboBoxDashboardItem cbKorisnikFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Rješavatelj", "korisnik");
            ComboBoxDashboardItem cbDokumentFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Document", "dokument");

            Dashboard.Items.Add(chartVrijemeObradeByNadleznost);
            Dashboard.Items.Add(chartVrijemeObradeByRjesavatelj);
            Dashboard.Items.Add(cbNadleznostFilter);
            Dashboard.Items.Add(cbKorisnikFilter);
            Dashboard.Items.Add(cbDokumentFilter);
           
            DashboardLayoutGroup filterGroup =
                new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Horizontal, 0.1,
                    new DashboardLayoutItem(cbNadleznostFilter, 34),
                    new DashboardLayoutItem(cbKorisnikFilter, 34),
                    new DashboardLayoutItem(cbDokumentFilter, 32));

            DashboardLayoutGroup chartGroup =
                new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Vertical, 100,
                    new DashboardLayoutItem(chartVrijemeObradeByNadleznost, 50),
                    new DashboardLayoutItem(chartVrijemeObradeByRjesavatelj, 50));

            DashboardLayoutGroup rootLayout = new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Vertical, 100,
                filterGroup,
                chartGroup);

            Dashboard.LayoutRoot = rootLayout;
        }

        private ChartDashboardItem CreateChartVrijemeObradeByNadleznost(string queryName)
        {
            ChartDashboardItem chart = new ChartDashboardItem();

            chart.Name = "Vrijeme obradi u minutu by Nadležnost";
            chart.ComponentName = ChartVrijemeObradeByNadleznostComponentName;
            chart.DataSource = DataSource;
            chart.DataMember = queryName;
            chart.ShowCaption = true;
            chart.ColoringOptions.MeasuresColoringMode = ColoringMode.Hue;
            chart.AxisX.EnableZooming = false;
            chart.AxisX.Visible = false;
            chart.Legend.OutsidePosition = ChartLegendOutsidePosition.TopRightVertical;

            Dimension xDimension = new Dimension("onevalue");
            chart.Arguments.Add(xDimension);

            ChartPane pane = new ChartPane();
            Measure yMeasure = new Measure("minuta", SummaryType.Sum);
            SimpleSeries series = new SimpleSeries(SimpleSeriesType.Bar);
            series.Value = yMeasure;

            pane.Series.Add(series);
            pane.PrimaryAxisY.TitleVisible = false;
            pane.PrimaryAxisY.Reverse = false;

            chart.Panes.Add(pane);

            Dimension seriesDimension = new Dimension("nadleznost");
            chart.SeriesDimensions.Add(seriesDimension);
            seriesDimension.SortOrder = DimensionSortOrder.Descending;
            seriesDimension.SortByMeasure = yMeasure;
            seriesDimension.Name = "";

            return chart;
        }

        private ChartDashboardItem CreateChartVrijemeObradeByRjesavatelj(string queryName)
        {
            ChartDashboardItem chart = new ChartDashboardItem();

            chart.Name = "Vrijeme obradi u minutu by Rješavatelj";
            chart.ComponentName = ChartVrijemeObradeByRjesavateljComponentName;
            chart.DataSource = DataSource;
            chart.DataMember = queryName;
            chart.ShowCaption = true;
            chart.ColoringOptions.MeasuresColoringMode = ColoringMode.Hue;
            chart.AxisX.EnableZooming = true;
            chart.AxisX.Visible = true;
            chart.Legend.Visible = false;

            Dimension xDimension = new Dimension("korisnik");
            chart.Arguments.Add(xDimension);

            ChartPane pane = new ChartPane();
            Measure yMeasure = new Measure("minuta", SummaryType.Sum);
            SimpleSeries series = new SimpleSeries(SimpleSeriesType.Bar);
            series.Value = yMeasure;

            pane.Series.Add(series);
            pane.PrimaryAxisY.TitleVisible = false;
            pane.PrimaryAxisY.Reverse = false;

            chart.Panes.Add(pane);

            xDimension.SortOrder = DimensionSortOrder.Descending;
            xDimension.SortMode = DimensionSortMode.Value;
            xDimension.SortByMeasure = yMeasure;

            Dashboard.ColorScheme.Add(new ColorSchemeEntry()
            {
                DataSource = DataSource,
                DataMember = queryName,
                ColorDefinition = new ColorDefinition(DashboardColors.LightBlue),
                MeasureKey = new ColorSchemeMeasureKey(yMeasure.GetMeasureDefinition())
            });

            return chart;
        }
    }
}