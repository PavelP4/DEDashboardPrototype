using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Web;

namespace WebApplication1.Providers
{
    public class DashboardConnectionStringsProvider : IDataSourceWizardConnectionStringsProvider
    {
        public Dictionary<string, string> GetConnectionDescriptions()
        {
            Dictionary<string, string> connections = new Dictionary<string, string>();
            
            connections.Add("msSqlConnection", "MS SQL Connection");
            return connections;
        }

        public DataConnectionParametersBase GetDataConnectionParameters(string name)
        {
            if (name == "msSqlConnection")
            {
                //return new MsSqlConnectionParameters("localhost", "DashboardTest", "sa", "@dmin2018", MsSqlAuthorizationType.SqlServer);
                //return new MsSqlConnectionParameters(@"ditv07\sql2012dev", "ePlanNabave4_1_HS_Test", "dokitsql2012dev", "Asdjkl098321.", MsSqlAuthorizationType.SqlServer);
                return new MsSqlConnectionParameters(@"ditv07\sql2014dev", "ePlanNabave4_1_Replica", "dokitsql2014dev", "Asdjkl098321.", MsSqlAuthorizationType.SqlServer);
            }
            throw new System.Exception("The connection string is undefined.");
        }
    }
}