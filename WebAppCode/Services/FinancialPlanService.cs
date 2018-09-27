using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Ajax.Utilities;
using WebAppCode.Contexts;
using WebAppCode.Models.FinancialPlan;
using WebAppCode.Models.FinancialPlan.Db;

namespace WebAppCode.Services
{
    public class FinancialPlanService: IFinancialPlanService
    {
        public FinancialPlan GetPlanById(int id)
        {
            using (var db = new FinPlanExcelContext())
            {
                return db.FinancialPlans.AsNoTracking()
                    .Include(p => p.FinancialPlanItems)
                    .FirstOrDefault(x => x.Id == id); 
            }
        }

        public IList<FinancialPlanItem> GetPlanItemsByPlanId(int id)
        {
            using (var db = new FinPlanExcelContext())
            {
                return db.FinancialPlanItems.AsNoTracking()
                    .Where(x => x.FinancialPlanId == id)
                    .ToList();
            }
        }

        public IList<PlanStructure> GetPlanStructureByPlanId(int id)
        {
            using (var db = new FinPlanExcelContext())
            {
                return db.Database.SqlQuery<PlanStructure>(
                    @"WITH pstrCTE AS
                        (
                          SELECT ps.id, ps.id_parent, ps.code, ps.description, ps.OptimisticLockField
                          FROM FinancialPlanItem fpi INNER JOIN PlanStructure ps ON fpi.id_plan_structure = ps.id
                          WHERE fpi.id_financial_plan = @planid
                          UNION ALL
                          SELECT ps2.id, ps2.id_parent, ps2.code, ps2.description, ps2.OptimisticLockField
                          FROM PlanStructure ps2 INNER JOIN pstrCTE pstr ON ps2.Id = pstr.id_parent
                        )
                        SELECT DISTINCT r.id, r.id_parent as parentid, r.code, r.description, r.OptimisticLockField FROM pstrCTE r
                          ORDER BY Code", 
                    new SqlParameter("@planid", id)).ToList();
            }
        }

        public IList<FinancialPlanItemFinancialSource> GetFinancialPlanItemFinancialSourcesByPlanId(int id)
        {
            using (var db = new FinPlanExcelContext())
            {
                return (
                    from pi in db.FinancialPlanItems.AsNoTracking()
                    join pisi in db.FinancialPlanItemFinancialSources.AsNoTracking()
                        on pi.Id equals pisi.FinancialPlanItemId
                    select pisi
                ).Include(x => x.FinancialSource).ToList();
            }
        }

        public IList<FinancialSource> GetAllFinancialSources()
        {
            using (var db = new FinPlanExcelContext())
            {
                return db.FinancialSources.AsNoTracking()
                    .OrderBy(x => x.Description)
                    .ToList();
            }
        }

        public SSFinancialPlan CreateFinancialPlanModel(int planId)
        {
            var plan = GetPlanById(planId);

            if (plan == null) return null;

            var planStructure = GetPlanStructureByPlanId(planId);
            var valueSources = GetFinancialPlanItemFinancialSourcesByPlanId(planId);
            var financialSources = GetAllFinancialSources();

            var rows = (
                from ps in planStructure
                join pi in plan.FinancialPlanItems on ps.Id equals pi.PlanStructureId into temp1
                from pi in temp1.DefaultIfEmpty()
                select new SSFinancialPlanRow()
                {
                    Code = ps.Code,
                    Description = ps.Description,
                    Values = pi != null 
                        ? valueSources.Where(x => x.FinancialPlanItemId == pi.Id).ToList() 
                        : new List<FinancialPlanItemFinancialSource>()
                }).ToList();

            return new SSFinancialPlan()
            {
                Caption = plan.Name,
                FinancialSources = financialSources,
                Rows = rows
            };
        }
    }
}