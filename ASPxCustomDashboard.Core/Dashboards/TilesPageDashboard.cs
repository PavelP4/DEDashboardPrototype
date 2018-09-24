using ASPxCustomDashboard.Core.Container;
using DevExpress.DashboardCommon;
using DevExpress.DashboardCommon.Native;
using DevExpress.Utils.Extensions;
using DevExpress.Utils.MVVM;

namespace ASPxCustomDashboard.Core.Dashboards
{
    public class TilesPageDashboard: BaseDashboard
    {
        public const string QueryName =  "Query1";
        //public const string QuerySql = "select '/Pages/TilesPage' as dbUrl";
        public const string QuerySql = "select '/Pages/TilesPage' as dbUrl";

        public const string WebPageTilesPageComponentName = "urlPageDashboardItem_TilesPage";
        public const string WebPageTilesPageAsWidgetComponentName = "urlPageDashboardItem_TilesPageAsWidget";
        

        public TilesPageDashboard(IDashboardContainer container)
            :base(container)
        {
        }

        protected override void ConfigureDataSourceQueries()
        {
            AddQueryToDataSource(QueryName, QuerySql);
        }

        protected override void Configure()
        {
            Dashboard.Title.Text = "TilesPage Dashboard";
            return;
            
            CustomDashboardItem webpage = CreateWebPageTilesPage(DataSource, QueryName);
            Dashboard.Items.Add(webpage);
            //CustomDashboardItem webpageAsWidget = CreateWebPageTilesPageAsWidget(DataSource, QueryName);
            //Dashboard.Items.Add(webpageAsWidget);

            DashboardLayoutItem layoutItem1 = new DashboardLayoutItem(webpage, 100);
            //DashboardLayoutItem layoutItem2 = new DashboardLayoutItem(webpageAsWidget, 100);

            DashboardLayoutGroup group1 =
                new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Horizontal, 100, layoutItem1);

            DashboardLayoutGroup rootLayout = new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Vertical, 1,
                group1
                //, layoutItem2
                );

            Dashboard.LayoutRoot = rootLayout;

            //var x = Dashboard.SaveToXDocument();
        }

        private CustomDashboardItem CreateWebPageTilesPage(DashboardSqlDataSource dataSource, string queryName)
        {
            var webpage = new CustomDashboardItem();
            
            webpage.Name = "WebPage widget (tiles page)";
            webpage.ComponentName = WebPageTilesPageComponentName;
            webpage.DataSource = dataSource;
            webpage.DataMember = queryName;
            webpage.ShowCaption = true;
            webpage.CustomItemType = "WebPage";
            
            webpage.GetDimensions().Add(new Dimension("dbUrl"));
            webpage.SliceTables.Add(new SliceTable(new Dimension("dbUrl")));


            
            //webpage.GetMeasures().Add(new Measure("dbUrl"));

            return webpage;
        }

        private CustomDashboardItem CreateWebPageTilesPageAsWidget(DashboardSqlDataSource dataSource, string queryName)
        {
            var webpage = new CustomDashboardItem();

            webpage.Name = "TilesPage widget (as widget)";
            webpage.ComponentName = WebPageTilesPageAsWidgetComponentName;
            webpage.DataSource = dataSource;
            webpage.DataMember = queryName;
            webpage.ShowCaption = true;
            webpage.CustomItemType = "TilesPage";

            webpage.GetDimensions().Add(new Dimension("dbUrl"));
            webpage.SliceTables.Add(new SliceTable(new Dimension("dbUrl")));



            return webpage;
        }
    }
}