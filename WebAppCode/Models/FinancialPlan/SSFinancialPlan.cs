using System.Collections.Generic;
using WebAppCode.Models.FinancialPlan.Db;

namespace WebAppCode.Models.FinancialPlan
{
    public class SSFinancialPlan
    {
        public string Caption { get; set; }

        public IList<FinancialSource> FinancialSources { get; set; }

        public IList<SSFinancialPlanRow> Rows { get; set; }
    }
}