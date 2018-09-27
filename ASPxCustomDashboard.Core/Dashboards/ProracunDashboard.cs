using System.Drawing;
using ASPxCustomDashboard.Core.Container;
using ASPxCustomDashboard.Core.Providers;
using DevExpress.DashboardCommon;
using DevExpress.XtraExport.Helpers;

namespace ASPxCustomDashboard.Core.Dashboards
{
    public class ProracunDashboard: BaseDashboard
    {
        public const string ChartProracunskiPodaciComponentName = "chartProracunskiPodaci";

        public const string SqlQuery1 = @"SELECT RTRIM(razdjel) AS razdjel, 
                                              SUM(raspolozivo_iznos) AS value,
                                              'Realizacija - raspolozivo' AS valueDesc
                                              FROM ePlanNabave4_1_Replica.dbo.dvwProracun
                                              GROUP BY razdjel
                                            UNION ALL
                                            SELECT RTRIM(razdjel) AS razdjel,    
                                              SUM(fakturirano_placeno) AS value,
                                              'Realizacija - fakturirano placeno' AS valueDesc   
                                              FROM ePlanNabave4_1_Replica.dbo.dvwProracun
                                              GROUP BY razdjel
                                            UNION ALL
                                            SELECT RTRIM(razdjel) AS razdjel,    
                                              SUM(fakturirano_neplaceno) AS value,
                                              'Realizacija - fakturirano neplaceno' AS valueDesc   
                                              FROM ePlanNabave4_1_Replica.dbo.dvwProracun
                                              GROUP BY razdjel
                                            UNION ALL
                                            SELECT RTRIM(razdjel) AS razdjel,    
                                              SUM(angazirano_iznos)*(-1) AS value,
                                              'Realizacija - angazirano' AS valueDesc   
                                              FROM ePlanNabave4_1_Replica.dbo.dvwProracun
                                              GROUP BY razdjel";

        public const string CustomSqlQueryName1 = "CustomSqlQuery1";


        public ProracunDashboard(IDashboardContainer container)
            :base(container)
        {
        }

        protected override void ConfigureDataSourceQueries()
        {
            AddQueryToDataSource(CustomSqlQueryName1, SqlQuery1);
        }

        protected override void RegisterDataSource(string dashboardId, string connectionName)
        {
            base.RegisterDataSource(dashboardId, DashboardConnectionStringsProvider.EPlanNabave41ReplicaConnectionName);
        }

        protected override void Configure()
        {
            Dashboard.Title.Text = "Dashboard 2.1-1"; 

            ChartDashboardItem chartProracunskiPodaci = CreateChartProracunskiPodaci(CustomSqlQueryName1);
            Dashboard.Items.Add(chartProracunskiPodaci);

            DashboardLayoutItem chartLayoutItem = new DashboardLayoutItem(chartProracunskiPodaci, 100);

            DashboardLayoutGroup group =
                new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Horizontal, 100, chartLayoutItem);

            DashboardLayoutGroup rootLayout = new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Vertical, 1,
                group);
            Dashboard.LayoutRoot = rootLayout;
        }

        private ChartDashboardItem CreateChartProracunskiPodaci(string queryName)
        {
            ChartDashboardItem chart = new ChartDashboardItem();

            chart.Name = "Proračunski podaci";
            chart.ComponentName = ChartProracunskiPodaciComponentName;
            chart.DataSource = DataSource;
            chart.DataMember = queryName;
            chart.ShowCaption = true;

            Dimension xDimension = new Dimension("razdjel");
            chart.Arguments.Add(xDimension);

            Dimension seriesDimension = new Dimension("valueDesc");
            seriesDimension.SortOrder = DimensionSortOrder.Descending;
            chart.SeriesDimensions.Add(seriesDimension);

            ChartPane pane = new ChartPane();
            chart.Panes.Add(pane);
            SimpleSeries valueSumSeries = new SimpleSeries(SimpleSeriesType.FullStackedBar);
            Measure yMeasure = new Measure("value", SummaryType.Sum);
            yMeasure.NumericFormat.FormatType = DataItemNumericFormatType.General;
            yMeasure.NumericFormat.IncludeGroupSeparator = true;
            yMeasure.NumericFormat.Precision = 2;
            valueSumSeries.Value = yMeasure;
            pane.Series.Add(valueSumSeries);
            pane.PrimaryAxisY.TitleVisible = false;
            pane.PrimaryAxisY.Reverse = false;

            chart.AxisX.EnableZooming = false;

            chart.Legend.OutsidePosition = ChartLegendOutsidePosition.TopLeftHorizontal;

            ConfigureColorScheme(queryName, seriesDimension.GetDimensionDefinition());

            return chart;
        }

        private void ConfigureColorScheme(string queryName, DimensionDefinition dimDef)
        {
            Dashboard.ColorScheme.Add(new ColorSchemeEntry()
            {
                DataSource = DataSource,
                DataMember = queryName,
                ColorDefinition = new ColorDefinition(Color.FromArgb(-13335345)),
                DimensionKeys = { new ColorSchemeDimensionKey(dimDef, "Realizacija - raspolozivo") }
            });
            Dashboard.ColorScheme.Add(new ColorSchemeEntry()
            {
                DataSource = DataSource,
                DataMember = queryName,
                ColorDefinition = new ColorDefinition(Color.FromArgb(-13147221)),
                DimensionKeys = { new ColorSchemeDimensionKey(dimDef, "Realizacija - fakturirano placeno") }
            });
            Dashboard.ColorScheme.Add(new ColorSchemeEntry()
            {
                DataSource = DataSource,
                DataMember = queryName,
                ColorDefinition = new ColorDefinition(Color.FromArgb(-2832069)),
                DimensionKeys = { new ColorSchemeDimensionKey(dimDef, "Realizacija - fakturirano neplaceno") }
            });
            Dashboard.ColorScheme.Add(new ColorSchemeEntry()
            {
                DataSource = DataSource,
                DataMember = queryName,
                ColorDefinition = new ColorDefinition(Color.FromArgb(-3055006)),
                DimensionKeys = { new ColorSchemeDimensionKey(dimDef, "Realizacija - angazirano") }
            });
        }
    }
}