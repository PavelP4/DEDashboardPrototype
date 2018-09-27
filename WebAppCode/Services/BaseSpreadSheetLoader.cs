using DevExpress.Spreadsheet;
using DevExpress.Web.ASPxSpreadsheet;

namespace WebAppCode.Services
{
    public class BaseSpreadSheetLoader
    {
        private readonly string _password;
        private readonly ASPxSpreadsheet _sheetControl;

        protected IWorkbook Document => _sheetControl.Document;
        protected Worksheet ActiveSheet => Document.Worksheets.ActiveWorksheet;

        public BaseSpreadSheetLoader(ASPxSpreadsheet sheetControl, string password)
        {
            _sheetControl = sheetControl;
            _password = password;
        }

        protected void ProtectActiveSheet(WorksheetProtectionPermissions permissions)
        {
            ActiveSheet.Protect(_password, permissions);
        }

        protected void UnprotectActiveSheet()
        {
            if (ActiveSheet.IsProtected)
                ActiveSheet.Unprotect(_password);
        }

        protected void ClearActiveSheet()
        {
            ActiveSheet.Clear(ActiveSheet.GetUsedRange());
        }
    }
}