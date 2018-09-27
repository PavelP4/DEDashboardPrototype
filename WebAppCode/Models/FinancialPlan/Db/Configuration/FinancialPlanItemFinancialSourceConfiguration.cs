using System.Data.Entity.ModelConfiguration;

namespace WebAppCode.Models.FinancialPlan.Db.Configuration
{
    public class FinancialPlanItemFinancialSourceConfiguration: EntityTypeConfiguration<FinancialPlanItemFinancialSource>
    {
        public FinancialPlanItemFinancialSourceConfiguration()
        {
            HasRequired(x => x.FinancialSource)
                .WithMany(x => x.FinancialPlanItemFinancialSources)
                .HasForeignKey(x => x.FinancialSourceId);

            HasRequired(x => x.FinancialPlanItem)
                .WithMany(x => x.FinancialPlanItemFinancialSources)
                .HasForeignKey(x => x.FinancialPlanItemId);
        }
    }
}