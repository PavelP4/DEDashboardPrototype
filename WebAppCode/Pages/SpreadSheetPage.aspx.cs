using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Spreadsheet;

namespace WebAppCode.Pages
{
    public partial class SpreadSheetPage : System.Web.UI.Page
    {
        protected const string password = "123";

        protected IWorkbook Document => ASPxSpreadsheet1.Document;

        protected Worksheet ActiveSheet => Document.Worksheets.ActiveWorksheet;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PrepareSpreadsheetDocument();
            }
        }
        

        protected void PrepareSpreadsheetDocument()
        {
            Document.BeginUpdate();
            if (ActiveSheet.IsProtected)
                ActiveSheet.Unprotect(password);

            UnlockCellRange();

            WorksheetProtectionPermissions selectedPermissions = WorksheetProtectionPermissions.Default;

            //Allows end-users to insert hyperlinks 
            selectedPermissions |= WorksheetProtectionPermissions.InsertHyperlinks;

            //Allows end-users to format cells 
            selectedPermissions |= WorksheetProtectionPermissions.FormatCells;

            ActiveSheet.Protect(password, selectedPermissions);

            Document.EndUpdate();
            Document.History.Clear();
        }

        protected void UnlockCellRange()
        {
            Range unlockedRange = ActiveSheet["E9:K18"];
            unlockedRange.Borders.SetOutsideBorders(Color.Black, BorderLineStyle.Thin);
            unlockedRange.Protection.Locked = false;

            Range titleRange = ActiveSheet["E8:K8"];
            titleRange.Merge();
            titleRange.Borders.SetOutsideBorders(Color.Black, BorderLineStyle.Thin);
            titleRange.SetValue("Not protected cells below");
            titleRange.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            titleRange.FillColor = Color.Tomato;
        }
    }
}