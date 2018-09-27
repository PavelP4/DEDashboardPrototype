using System.Collections.Generic;
using WebAppCode.Models.FinancialPlan.Db;

namespace WebAppCode.Models.FinancialPlan
{
    public class SSFinancialPlanRow
    {
        public string Code { get; set; }

        public string Description { get; set; }

        public IList<FinancialPlanItemFinancialSource> Values { get; set; }
    }
}