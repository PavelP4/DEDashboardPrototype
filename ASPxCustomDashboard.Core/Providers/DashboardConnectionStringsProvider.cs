using System.Collections.Generic;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Web;

namespace ASPxCustomDashboard.Core.Providers
{
    public class DashboardConnectionStringsProvider : IDataSourceWizardConnectionStringsProvider
    {
        public const string MsSqlConnectionName = "msSqlConnection";
        public const string EPlanNabave41ReplicaConnectionName = "ePlanNabave4_1_ReplicaConnection";

        private readonly IDictionary<string, DataConnectionParametersBase> _connectionParams;

        public DashboardConnectionStringsProvider()
        {
            _connectionParams = new Dictionary<string, DataConnectionParametersBase>();
        }

        public void AddConnectionParams(string connectionName, DataConnectionParametersBase connectionParams)
        {
            _connectionParams.Add(connectionName, connectionParams);
        }

        public Dictionary<string, string> GetConnectionDescriptions()
        {
            Dictionary<string, string> connections = new Dictionary<string, string>();
            
            connections.Add(MsSqlConnectionName, "MS SQL Connection");
            connections.Add(EPlanNabave41ReplicaConnectionName, "MS SQL ePlanNabave41Replica Connection");
            return connections;
        }

        public DataConnectionParametersBase GetDataConnectionParameters(string name)
        {
            if (_connectionParams.TryGetValue(name, out var parameters))
            {
                return parameters;
            }

            throw new System.Exception("The connection string is undefined.");
        }
    }
}