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
        
        public const string ChartDocumentsByNamesComponentName2 = "chartDashboardItem_DocumentsByNames2";
        
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
                Container.Configure();
            }

            if (!((ASPxDashboard)sender).IsCallback)
            {
                Container = new DashboardContainer(ASPxDashboardDocum);
                Container.Configure();

                Container.RegisterDashboard(Dashboard1Name, new FirstDashboard(Container));
                Container.RegisterDashboard(Dashboard2Name, new SecondDashboard(Container));

                //Container.ConfigureDashboards();
            }
        }

        protected void ASPxDashboardDocum_DashboardLoading(object sender, DashboardLoadingWebEventArgs e)
        {
            string dashboardId = e.DashboardId;

            Container.Configure();
            Container.ConfigureDashboard(dashboardId);

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

    }
}