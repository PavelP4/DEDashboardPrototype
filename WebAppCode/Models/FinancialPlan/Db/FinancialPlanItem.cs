using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCode.Models.FinancialPlan.Db
{
    public class FinancialPlanItem : BaseEntity
    {
        public string Name { get; set; }

        public decimal? Amount { get; set; }

        [Column("id_financial_plan")]
        public int FinancialPlanId { get; set; }

        [Column("id_plan_structure")]
        public int PlanStructureId { get; set; }

        public virtual FinancialPlan FinancialPlan { get; set; }
        public virtual PlanStructure PlanStructure { get; set; }
        public virtual ICollection<FinancialPlanItemFinancialSource> FinancialPlanItemFinancialSources { get; set; }

        public FinancialPlanItem()
        {
            FinancialPlanItemFinancialSources = new HashSet<FinancialPlanItemFinancialSource>();
        }
    }
}