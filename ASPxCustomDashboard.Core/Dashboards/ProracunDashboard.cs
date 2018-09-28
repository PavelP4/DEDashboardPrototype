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

        public const string SqlQuery1 = @"SELECT RTRIM(razdjel) as razdjel, 
                                              raspolozivo_iznos AS value,
                                              'Realizacija - raspolozivo' AS valueDesc,
                                              pozicija,
                                              program,
                                              glava,
                                              projekt_aktivnost,
                                              izvori_sredstava,
                                              ekonomska_klasifikacija,
                                              korisnik
                                              FROM dbo.dvwProracun  
                                            UNION ALL
                                            SELECT RTRIM(razdjel) as razdjel,    
                                              fakturirano_placeno AS value,
                                              'Realizacija - fakturirano placeno' AS valueDesc,
                                              pozicija,
                                              program,
                                              glava,
                                              projekt_aktivnost,
                                              izvori_sredstava,
                                              ekonomska_klasifikacija,
                                              korisnik   
                                              FROM dbo.dvwProracun 
                                            UNION ALL
                                            SELECT RTRIM(razdjel) as razdjel,    
                                              fakturirano_neplaceno AS value,
                                              'Realizacija - fakturirano neplaceno' AS valueDesc,
                                              pozicija,
                                              program,
                                              glava,
                                              projekt_aktivnost,
                                              izvori_sredstava,
                                              ekonomska_klasifikacija,
                                              korisnik   
                                              FROM dbo.dvwProracun                                             
                                            UNION ALL
                                            SELECT RTRIM(razdjel) as razdjel,    
                                              angazirano_iznos*(-1) AS value,
                                              'Realizacija - angazirano' AS valueDesc,
                                              pozicija,
                                              program,
                                              glava,
                                              projekt_aktivnost,
                                              izvori_sredstava,
                                              ekonomska_klasifikacija,
                                              korisnik   
                                              FROM dbo.dvwProracun";

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
            ComboBoxDashboardItem cbPozicijaFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Pozicija", "pozicija");
            ComboBoxDashboardItem cbRazdjelFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Razdjel", "razdjel");
            ComboBoxDashboardItem cbProgramFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Program", "program");
            ComboBoxDashboardItem cbGlavaFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Glava", "glava");
            ComboBoxDashboardItem cbProjektAktivnostFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Projekt/Aktivnost", "projekt_aktivnost");
            ComboBoxDashboardItem cbIzvoriSredstavaFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Izvori sredstava", "izvori_sredstava");
            ComboBoxDashboardItem cbEkonomskaKlasifikacijaFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Ekonomska klasifikacija", "ekonomska_klasifikacija");
            ComboBoxDashboardItem cbKorisnikFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Korisnik", "korisnik");
            Dashboard.Items.Add(chartProracunskiPodaci);
            Dashboard.Items.Add(cbPozicijaFilter);
            Dashboard.Items.Add(cbRazdjelFilter);
            Dashboard.Items.Add(cbProgramFilter);
            Dashboard.Items.Add(cbGlavaFilter);
            Dashboard.Items.Add(cbProjektAktivnostFilter);
            Dashboard.Items.Add(cbIzvoriSredstavaFilter);
            Dashboard.Items.Add(cbEkonomskaKlasifikacijaFilter);
            Dashboard.Items.Add(cbKorisnikFilter);


            DashboardLayoutItem chartLayoutItem = new DashboardLayoutItem(chartProracunskiPodaci, 145);

            DashboardLayoutGroup filterGroupRow1 =
                new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Horizontal, 50,
                    new DashboardLayoutItem(cbPozicijaFilter, 25),
                    new DashboardLayoutItem(cbProgramFilter, 25),
                    new DashboardLayoutItem(cbProjektAktivnostFilter, 25),
                    new DashboardLayoutItem(cbEkonomskaKlasifikacijaFilter, 25));
            DashboardLayoutGroup filterGroupRow2 =
                new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Horizontal, 50,
                    new DashboardLayoutItem(cbRazdjelFilter, 25),
                    new DashboardLayoutItem(cbGlavaFilter, 25),
                    new DashboardLayoutItem(cbIzvoriSredstavaFilter, 25),
                    new DashboardLayoutItem(cbKorisnikFilter, 25));

            DashboardLayoutGroup filterGroup =
                new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Vertical, 55,
                    filterGroupRow1,
                    filterGroupRow2);

            DashboardLayoutGroup rootLayout = new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Vertical, 1,
                filterGroup,
                chartLayoutItem);

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
            yMeasure.NumericFormat.FormatType = DataItemNumericFormatType.Currency;
            yMeasure.NumericFormat.IncludeGroupSeparator = true;
            yMeasure.NumericFormat.Precision = 2;
            yMeasure.NumericFormat.Unit = DataItemNumericUnit.Ones;
            yMeasure.NumericFormat.CurrencyCultureName = "hr-HR";
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

        private ComboBoxDashboardItem CreateComboBoxFilter(string queryName, string name, string field)
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