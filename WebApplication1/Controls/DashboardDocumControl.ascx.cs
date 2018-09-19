using System;
using WebApplication1.Providers;

namespace WebApplication1.Controls
{
    public partial class WebUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ASPxDashboard1.SetConnectionStringsProvider(new ConfigFileConnectionStringsProvider());
            ASPxDashboardDocum.SetConnectionStringsProvider(new DashboardConnectionStringsProvider());
        }
    }
}