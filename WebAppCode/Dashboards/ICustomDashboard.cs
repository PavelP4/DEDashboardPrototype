using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppCode.Dashboards
{
    public interface ICustomDashboard
    {
        string Title { get; set; }
        string DashboardId { get; }

        void ConfigureDashboard(string dashboardId);
    }
}
