using System.Collections.Generic;

namespace WebAppCode.Models.FinancialPlan.Db
{
    public class FinancialSource: BaseEntity
    {
        public string Description { get; set; }

        public virtual ICollection<FinancialPlanItemFinancialSource> FinancialPlanItemFinancialSources { get; set; }

        public FinancialSource()
        {
            FinancialPlanItemFinancialSources = new HashSet<FinancialPlanItemFinancialSource>();
        }
    }
}