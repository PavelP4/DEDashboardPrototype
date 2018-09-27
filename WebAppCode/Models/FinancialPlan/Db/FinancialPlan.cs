using System.Collections.Generic;

namespace WebAppCode.Models.FinancialPlan.Db
{
    public class FinancialPlan: BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<FinancialPlanItem> FinancialPlanItems { get; set; }

        public FinancialPlan()
        {
            FinancialPlanItems = new HashSet<FinancialPlanItem>();
        }
    }
}