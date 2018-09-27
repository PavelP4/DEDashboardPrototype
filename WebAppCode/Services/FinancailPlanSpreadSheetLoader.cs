using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using DevExpress.Spreadsheet;
using DevExpress.Web.ASPxSpreadsheet;
using WebAppCode.Models.FinancialPlan;

namespace WebAppCode.Services
{
    public class FinancailPlanSpreadSheetLoader: BaseSpreadSheetLoader, ISpreadSheetLoader
    {
        private const int StartCol = 0;
        private const int CodeCol = StartCol;
        private const int StructureCol = CodeCol + 1;
        private const int DsStartCol = StructureCol + 1;

        private const int StartRow = 0;
        private const int HeaderRow = StartRow;
        private const int DataRow = StartRow + 1;

        private IFinancialPlanService _service = new FinancialPlanService();

        public FinancailPlanSpreadSheetLoader(ASPxSpreadsheet sheetControl)
            : base(sheetControl, "12345")
        {
        }

        public void LoadAndFill(int planId)
        {
            Document.BeginUpdate();
            UnprotectActiveSheet();

            var model = _service.CreateFinancialPlanModel(1);
            PopulateSheet(model);

            WorksheetProtectionPermissions selectedPermissions = WorksheetProtectionPermissions.Default;
            ProtectActiveSheet(selectedPermissions);

            Document.EndUpdate();
            Document.History.Clear();
        }

        private void PopulateSheet(SSFinancialPlan model)
        {
            ClearActiveSheet();

            var mapDs = FillHeader(model);
            FillData(model, mapDs);
        }

        private IDictionary<int,int> FillHeader(SSFinancialPlan model)
        {
            var structureCell = ActiveSheet.Cells[HeaderRow, StructureCol];
            structureCell.SetValue("Structure");
            structureCell.ColumnWidth = 320;

            var mapFs = new Dictionary<int,int>();

            for (int i = 0; i < model.FinancialSources.Count; i++)
            {
                var cell = ActiveSheet.Cells[HeaderRow, i + DsStartCol];
                cell.SetValue(model.FinancialSources[i].Description);
                ActiveSheet.Columns[cell.ColumnIndex].AutoFit();

                mapFs.Add(model.FinancialSources[i].Id, cell.ColumnIndex);
            }

            var headerRange = ActiveSheet.Range.FromLTRB(StartCol, StartRow, mapFs.Count + DsStartCol - StartCol - 1, StartRow);
            //headerRange.Borders.SetOutsideBorders(Color.Black, BorderLineStyle.Thin);
            headerRange.Borders.SetAllBorders(Color.Black, BorderLineStyle.Thin);

            return mapFs;
        }

        private void FillData(SSFinancialPlan model, IDictionary<int, int> mapFs)
        {
            var minValCol = mapFs.Values.Min();
            var maxValCol = mapFs.Values.Max();

            for (int i = 0; i < model.Rows.Count; i++)
            {
                var row = i + DataRow;
                ActiveSheet.Cells[row, CodeCol].SetValue(model.Rows[i].Code);
                ActiveSheet.Cells[row, StructureCol].SetValue(model.Rows[i].Description);

                foreach (var valItem in model.Rows[i].Values)
                {
                    var valCol = mapFs[valItem.FinancialSourceId];
                    var valCell = ActiveSheet.Cells[row, valCol];
                    valCell.SetValue(valItem.Amount);

                    var valRange = ActiveSheet.Range.FromLTRB(minValCol, row, maxValCol, row);
                    valRange.Protection.Locked = false;
                    valRange.NumberFormat = "#,##0.00";
                }
            }
        }
    }
}