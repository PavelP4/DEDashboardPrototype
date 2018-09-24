using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using DevExpress.DashboardWeb;

namespace WebAppCode
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            //DevExpress.DashboardWeb.DashboardBootstrapper.SessionState = System.Web.SessionState.SessionStateBehavior.Disabled;
            //ASPxDashboard.StaticInitialize();

            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}