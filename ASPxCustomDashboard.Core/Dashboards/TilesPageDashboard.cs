using ASPxCustomDashboard.Core.Container;
using DevExpress.DashboardCommon;
using DevExpress.Utils.Extensions;

namespace ASPxCustomDashboard.Core.Dashboards
{
    public class TilesPageDashboard: BaseDashboard
    {
        public const string QueryName =  "Query1";
        public const string QuerySql = "select null as col1";

        public const string WebPageTilesPageComponentName = "urlPageDashboardItem_TilesPage";
        

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
          
            
            CustomDashboardItem webpage = CreateWebPageTilesPage(DataSource, QueryName);
            Dashboard.Items.Add(webpage);

            DashboardLayoutItem chart1LayoutItem = new DashboardLayoutItem(webpage, 100);

            DashboardLayoutGroup group1 =
                new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Horizontal, 100, chart1LayoutItem);

            DashboardLayoutGroup rootLayout = new DashboardLayoutGroup(DashboardLayoutGroupOrientation.Vertical, 1,
                group1);
            Dashboard.LayoutRoot = rootLayout;
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
            webpage.DataItemRepository.Add(new Dimension("col1"), "DataItem0");
            webpage.SliceTables.Add(new SliceTable(new Dimension("col1")));
            
            return webpage;
        }
    }
}