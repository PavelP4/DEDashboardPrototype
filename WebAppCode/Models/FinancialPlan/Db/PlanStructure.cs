using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCode.Models.FinancialPlan.Db
{
    public class PlanStructure: BaseEntity
    {
        [Column("id_parent")]
        public int? ParentId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public virtual PlanStructure Parent { get; set; }
        public virtual ICollection<PlanStructure> Childs { get; set; }
        public virtual ICollection<FinancialPlanItem> FinancialPlanItems { get; set; }

        public PlanStructure()
        {
            FinancialPlanItems = new HashSet<FinancialPlanItem>();
            Childs = new HashSet<PlanStructure>();
        }
    }
}