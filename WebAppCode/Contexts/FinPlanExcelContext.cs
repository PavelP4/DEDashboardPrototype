using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebAppCode.Models.FinancialPlan.Db;
using WebAppCode.Models.FinancialPlan.Db.Configuration;

namespace WebAppCode.Contexts
{
    public class FinPlanExcelContext: DbContext
    {
        public const string Connection_Name = "finplanexcel";
        public static readonly string Connection_String = ConfigurationManager.ConnectionStrings[Connection_Name].ConnectionString;

        public FinPlanExcelContext()
            :base(Connection_Name)
        {
        }

        public IDbSet<FinancialPlan> FinancialPlans { get; set; }
        public IDbSet<FinancialPlanItem> FinancialPlanItems { get; set; }
        public IDbSet<PlanStructure> PlanStructures { get; set; }
        public IDbSet<FinancialSource> FinancialSources { get; set; }
        public IDbSet<FinancialPlanItemFinancialSource> FinancialPlanItemFinancialSources { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new FinancialPlanConfiguration());
            modelBuilder.Configurations.Add(new FinancialPlanItemConfiguration());
            modelBuilder.Configurations.Add(new PlanStructureConfiguration());
            modelBuilder.Configurations.Add(new FinancialSourceConfiguration());
            modelBuilder.Configurations.Add(new FinancialPlanItemFinancialSourceConfiguration());
        }
    }
}