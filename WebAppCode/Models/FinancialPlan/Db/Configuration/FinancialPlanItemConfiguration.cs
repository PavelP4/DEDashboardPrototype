using System.Data.Entity.ModelConfiguration;

namespace WebAppCode.Models.FinancialPlan.Db.Configuration
{
    public class FinancialPlanItemConfiguration: EntityTypeConfiguration<FinancialPlanItem>
    {
        public FinancialPlanItemConfiguration()
        {
            HasRequired(x => x.FinancialPlan)
                .WithMany(x => x.FinancialPlanItems)
                .HasForeignKey(x => x.FinancialPlanId);

            HasRequired(x => x.PlanStructure)
                .WithMany(x => x.FinancialPlanItems)
                .HasForeignKey(x => x.PlanStructureId);
        }
    }
}