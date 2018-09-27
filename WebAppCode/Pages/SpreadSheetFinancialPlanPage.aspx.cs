using System;
using WebAppCode.Services;

namespace WebAppCode.Pages
{
    public partial class SpreadSheetPage : System.Web.UI.Page
    {
        private FinancailPlanSpreadSheetLoader _financailPlanSpreadSheetLoader;

        protected void Page_Load(object sender, EventArgs e)
        {
            _financailPlanSpreadSheetLoader = new FinancailPlanSpreadSheetLoader(ASPxSpreadsheetFinancialPlan);

            if (!Page.IsPostBack)
            {
                _financailPlanSpreadSheetLoader.LoadAndFill(1);
            }
        }
    }
}