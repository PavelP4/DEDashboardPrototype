using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppCode.Models.FinancialPlan;
using WebAppCode.Models.FinancialPlan.Db;

namespace WebAppCode.Services
{
    interface IFinancialPlanService
    {
        FinancialPlan GetPlanById(int id);
        IList<FinancialPlanItem> GetPlanItemsByPlanId(int id);
        IList<PlanStructure> GetPlanStructureByPlanId(int id);
        IList<FinancialPlanItemFinancialSource> GetFinancialPlanItemFinancialSourcesByPlanId(int id);
        SSFinancialPlan CreateFinancialPlanModel(int planId);
    }
}
