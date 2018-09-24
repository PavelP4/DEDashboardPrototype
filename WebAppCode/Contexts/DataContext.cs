using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebAppCode.Models.Db;

namespace WebAppCode.Contexts
{
    public class DataContext: DbContext
    {
        public const string Connection_Name = "localConnection";
        public static readonly string Connection_String = ConfigurationManager.ConnectionStrings[Connection_Name].ConnectionString;

        public DataContext()
            :base(Connection_Name)
        {
        }

        public IDbSet<TableA> TableA { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Configurations.Add()
        }
    }
}