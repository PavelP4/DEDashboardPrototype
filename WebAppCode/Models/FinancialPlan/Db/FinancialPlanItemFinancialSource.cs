using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCode.Models.FinancialPlan.Db
{
    public class FinancialPlanItemFinancialSource: BaseEntity
    {
        public decimal? Amount { get; set; }

        [Column("id_financial_plan_item")]
        public int FinancialPlanItemId { get; set; }

        [Column("id_financial_source")]
        public int FinancialSourceId { get; set; }

        public FinancialPlanItem FinancialPlanItem { get; set; }
        public FinancialSource FinancialSource { get; set; }
    }
}