using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebAppCode.Contexts
{
    public class DataContext: DbContext
    {
        public const string Connection_Name = "nabave";
        public static readonly string Connection_String = ConfigurationManager.ConnectionStrings[Connection_Name].ConnectionString;

        public DataContext()
            :base(Connection_Name)
        {
        }

        //public IDbSet<>  { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Configurations.Add()
        }
    }
}