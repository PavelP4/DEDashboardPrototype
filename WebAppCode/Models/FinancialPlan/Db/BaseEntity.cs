namespace WebAppCode.Models.FinancialPlan.Db
{
    public class BaseEntity
    {
        public int Id { get; set; }
        
        public int? OptimisticLockField { get; set; }
    }
}