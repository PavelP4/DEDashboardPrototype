using System.Drawing;
using ASPxCustomDashboard.Core.Container;
using ASPxCustomDashboard.Core.Enums;
using ASPxCustomDashboard.Core.Providers;
using DevExpress.DashboardCommon;

namespace ASPxCustomDashboard.Core.Dashboards
{
    public class VrijednostAndIznosDashboard : BaseDashboard
    {
        public const string ChartVrijednostAndIznosComponentName = "chartVrijednostAndIznos";

        public const string SqlQuery1 = @"SELECT RTRIM(razdjel) as razdjel, 
                                              planirana_vrijednost,
                                              iznos_realizacije,
                                              pozicija,
                                              program,
                                              glava,
                                              projekt_aktivnost,
                                              izvori_sredstava,
                                              ekonomska_klasifikacija,
                                              korisnik,
                                              oj_korisnika,
                                              predmet_nabave
                                            FROM dbo.dvwRealizacijaPlanaNabave";

        public const string CustomSqlQueryName1 = "CustomSqlQuery1";


        public VrijednostAndIznosDashboard(IDashboardContainer container)
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
            Dashboard.Title.Text = "Proračunski podaci"; 

            ChartDashboardItem chartProracunskiPodaci = CreateChartProracunskiPodaci(CustomSqlQueryName1);
            ComboBoxDashboardItem cbPozicijaFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Pozicija", "pozicija");
            ComboBoxDashboardItem cbRazdjelFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Razdjel", "razdjel");
            ComboBoxDashboardItem cbProgramFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Program", "program");
            ComboBoxDashboardItem cbGlavaFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Glava", "glava");
            ComboBoxDashboardItem cbProjektAktivnostFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Projekt/Aktivnost", "projekt_aktivnost");
            ComboBoxDashboardItem cbIzvoriSredstavaFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Izvori sredstava", "izvori_sredstava");
            ComboBoxDashboardItem cbEkonomskaKlasifikacijaFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Ekonomska klasifikacija", "ekonomska_klasifikacija");
            ComboBoxDashboardItem cbKorisnikFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Korisnik", "korisnik");
            ComboBoxDashboardItem cbPredmetNabaveFilter = CreateComboBoxFilter(CustomSqlQueryName1, "Predmet nabave", "predmet_nabave");
            Dashboard.Items.Add(chartProracunskiPodaci);
            Dashboard.Items.Add(cbPozicijaFilter);
            Dashboard.Items.Add(cbRazdjelFilter);
            Dashboard.Items.Add(cbProgramFilter);
            Dashboard.Items.Add(cbGlavaFilter);
            Dashboard.Items.Add(cbProjektAktivnostFilter);
            Dashboard.Items.Add(cbIzvoriSredstavaFilter);
            Dashboard.Items.Add(cbEkonomskaKlasifikacijaFilter);
            Dashboard.Items.Add(cbKorisnikFilter);
            Dashboard.Items.Add(cbPredmetNabaveFilter);


            DashboardLayoutItem chartLayoutItem = new DashboardLayoutItem(chartProracunskiPodaci, 145);

            DashboardLayoutGroup filterGroupRow1 =
                new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Horizontal, 50,
                    new DashboardLayoutItem(cbPozicijaFilter, 20),
                    new DashboardLayoutItem(cbProgramFilter, 20),
                    new DashboardLayoutItem(cbProjektAktivnostFilter, 20),
                    new DashboardLayoutItem(cbEkonomskaKlasifikacijaFilter, 20),
                    new DashboardLayoutItem(cbKorisnikFilter, 20));
            DashboardLayoutGroup filterGroupRow2 =
                new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Horizontal, 50,
                    new DashboardLayoutItem(cbRazdjelFilter, 25),
                    new DashboardLayoutItem(cbGlavaFilter, 25),
                    new DashboardLayoutItem(cbIzvoriSredstavaFilter, 25),
                    new DashboardLayoutItem(cbPredmetNabaveFilter, 25));

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

            chart.Name = "Planirana vrijednost and iznos realizacije by Oj korisnika";
            chart.ComponentName = ChartVrijednostAndIznosComponentName;
            chart.DataSource = DataSource;
            chart.DataMember = queryName;
            chart.ShowCaption = true;
            chart.ColoringOptions.MeasuresColoringMode = ColoringMode.Hue;
            chart.AxisX.EnableZooming = false;
            //chart.AxisX.LimitVisiblePoints = true;
            //chart.AxisX.VisiblePointsCount = 8;
            chart.Legend.OutsidePosition = ChartLegendOutsidePosition.TopLeftHorizontal;

            Dimension xDimension = new Dimension("oj_korisnika");
            //xDimension.IsDiscreteNumericScale = true;
            xDimension.SortOrder = DimensionSortOrder.Descending;
            chart.Arguments.Add(xDimension);
            
            ChartPane pane = new ChartPane();
            pane.Series.Add(DefineSeries("planirana_vrijednost", queryName, "Planirana vrijednost", DashboardColors.LightBlue));
            pane.Series.Add(DefineSeries("iznos_realizacije", queryName, "Iznos realizacije", DashboardColors.Blue));
            pane.PrimaryAxisY.TitleVisible = false;
            pane.PrimaryAxisY.Reverse = false;

            chart.Panes.Add(pane);
            
            return chart;
        }

        private SimpleSeries DefineSeries(string dataMember, string queryName, string caption, Color color)
        {
            Measure yMeasure = new Measure(dataMember, SummaryType.Sum);

            SimpleSeries series = new SimpleSeries(SimpleSeriesType.Bar);
            series.Value = yMeasure;

            yMeasure.Name = caption;
            yMeasure.NumericFormat.FormatType = DataItemNumericFormatType.Currency;
            yMeasure.NumericFormat.IncludeGroupSeparator = true;
            yMeasure.NumericFormat.Precision = 2;
            yMeasure.NumericFormat.Unit = DataItemNumericUnit.Ones;
            yMeasure.NumericFormat.CurrencyCultureName = "hr-HR";

            Dashboard.ColorScheme.Add(new ColorSchemeEntry()
            {
                DataSource = DataSource,
                DataMember = queryName,
                ColorDefinition = new ColorDefinition(color),
                MeasureKey = new ColorSchemeMeasureKey(yMeasure.GetMeasureDefinition())
            });

            return series;
        }
    }
}