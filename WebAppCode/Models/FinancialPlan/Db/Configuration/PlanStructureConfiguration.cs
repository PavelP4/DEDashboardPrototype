using System.Data.Entity.ModelConfiguration;

namespace WebAppCode.Models.FinancialPlan.Db.Configuration
{
    public class PlanStructureConfiguration: EntityTypeConfiguration<PlanStructure>
    {
        public PlanStructureConfiguration()
        {
            //Property(x => x.ParentId)
            //    .HasColumnName("id_parent")
            //    .IsOptional();

            HasOptional(x => x.Parent)
                .WithMany(x => x.Childs)
                .HasForeignKey(x => x.ParentId);
        }
    }
}