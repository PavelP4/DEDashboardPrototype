﻿using System.Collections.Generic;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Web;

namespace WebAppCode.Providers
{
    public class DashboardConnectionStringsProvider : IDataSourceWizardConnectionStringsProvider
    {
        public const string MsSqlConnectionName = "msSqlConnection";

        public Dictionary<string, string> GetConnectionDescriptions()
        {
            Dictionary<string, string> connections = new Dictionary<string, string>();
            
            connections.Add(MsSqlConnectionName, "MS SQL Connection");
            return connections;
        }

        public DataConnectionParametersBase GetDataConnectionParameters(string name)
        {
            if (name == MsSqlConnectionName)
            {
                //return new MsSqlConnectionParameters("localhost", "DashboardTest", "sa", "@dmin2018", MsSqlAuthorizationType.SqlServer);
                return new MsSqlConnectionParameters(@"ditv07\sql2012dev", "ePlanNabave4_1_HS_Test", "dokitsql2012dev", "Asdjkl098321.", MsSqlAuthorizationType.SqlServer);
            }
            throw new System.Exception("The connection string is undefined.");
        }
    }
}