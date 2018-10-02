using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPxCustomDashboard.Core.Container;
using ASPxCustomDashboard.Core.Dashboards;
using ASPxCustomDashboard.Core.Providers;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.Web;
using Newtonsoft.Json;


namespace WebAppCode.Controls
{
    public partial class DashboardDocumControl : System.Web.UI.UserControl
    {
        //public const string Dashboard1Name = "Dashboard1";
        //public const string Dashboard2Name = "Dashboard2";
        //public const string DashboardTilesPageName = "DashboardTilesPage";
        public const string ProracunDashboardName = "ProracunDashboard";
        public const string VrijednostAndIznosDashboardName = "VrijednostAndIznosDashboard";

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

        
        #region .Dashboard events.

        protected void ASPxDashboardDocum_OnInit(object sender, EventArgs e)
        {
            if (Container != null) // callback workaround
            {
                Container.UpdateContainerComponent(ASPxDashboardDocum);
                Container.Configure();
            }

            if (!((ASPxDashboard)sender).IsCallback)
            {
                Container = new DashboardContainer(ASPxDashboardDocum);
                Container.Configure();

                Container.RegisterDbConnectionParams(
                    DashboardConnectionStringsProvider.MsSqlConnectionName, 
                    new MsSqlConnectionParameters(
                        @"ditv07\sql2012dev", "ePlanNabave4_1_HS_Test", "dokitsql2012dev", "Asdjkl098321.", 
                        MsSqlAuthorizationType.SqlServer));

                Container.RegisterDbConnectionParams(
                    DashboardConnectionStringsProvider.EPlanNabave41ReplicaConnectionName,
                    new MsSqlConnectionParameters(
                        @"ditv07\sql2014dev", "ePlanNabave4_1_Replica", "dokitsql2014dev", "Asdjkl098321.",
                        MsSqlAuthorizationType.SqlServer));

                //Container.RegisterDashboard(Dashboard1Name, new FirstDashboard(Container));
                //Container.RegisterDashboard<FirstDashboard>(Dashboard1Name);
                //Container.RegisterDashboard(Dashboard2Name, typeof(SecondDashboard));
                //Container.RegisterDashboard(DashboardTilesPageName, typeof(TilesPageDashboard));

                Container.RegisterDashboard(ProracunDashboardName, typeof(ProracunDashboard));
                Container.RegisterDashboard(VrijednostAndIznosDashboardName, typeof(VrijednostAndIznosDashboard));


                //Container.ConfigureDashboards();
            }
        }

        protected void ASPxDashboardDocum_DashboardLoading(object sender, DashboardLoadingWebEventArgs e)
        {
            string dashboardId = e.DashboardId;
         
            Container.ConfigureDashboard(dashboardId);

            e.DashboardXml = Container.LoadDashboard(dashboardId);
        }

        protected void ASPxDashboardDocum_OnCustomJSProperties(object sender, CustomJSPropertiesEventArgs e)
        {
            ASPxDashboard s = (ASPxDashboard) sender;

            //s.JSProperties.Add("cpDashboard1Name", Dashboard1Name);
            //s.JSProperties.Add("cpDashboard2Name", Dashboard2Name);
            //s.JSProperties.Add("cpDashboardTilesPageName", DashboardTilesPageName);
            s.JSProperties.Add("cpProracunDashboardName", ProracunDashboardName);
            s.JSProperties.Add("cpVrijednostAndIznosDashboardName", VrijednostAndIznosDashboardName);

            s.JSProperties.Add("cpInitialDashboard", VrijednostAndIznosDashboardName);

            //s.JSProperties.Add("cpChart1Name", FirstDashboard.ChartDocumentsByDaysComponentName);
            //s.JSProperties.Add("cpChart2Name", FirstDashboard.ChartDocumentsByNamesComponentName);
            //s.JSProperties.Add("cpChart21Name", SecondDashboard.ChartDocumentsByNamesComponentName2);
            //s.JSProperties.Add("cpWebPageWidgetName", TilesPageDashboard.WebPageTilesPageComponentName);
            s.JSProperties.Add("cpChartProracunskiPodaciName", ProracunDashboard.ChartProracunskiPodaciComponentName);
            s.JSProperties.Add("cpChartVrijednostAndIznosComponentName", VrijednostAndIznosDashboard.ChartVrijednostAndIznosComponentName);

            Dictionary<string, string> movementsMap = new Dictionary<string, string>();
            //movementsMap.Add("cpDashboard1Name.cpChart1Name", "cpDashboard2Name");
            //movementsMap.Add("cpDashboard1Name.cpChart2Name", "cpDashboard2Name");
            //movementsMap.Add("cpDashboard2Name.cpChart21Name", "cpDashboard1Name");

            s.JSProperties.Add("cpDashboardMovementsMap", JsonConvert.SerializeObject(movementsMap));
            
            
        }

        #endregion

    }
}