using System;
using EAM.Data.Services;
using EAM.Data.Services.Dto;
using NPOI.SS.Util;

namespace EAM.Data.ImportAndExport.Export.ExportDoAssets
{
    /// <summary>
    /// 导出报修单
    /// </summary>
    public class ExportDoAssetsInventory : ExportDoAssetsBase
    {
        public ExportDoAssetsInventory(IAssetsOptionService assetsOptionService, int doAssetsId)
            : base(assetsOptionService, doAssetsId)
        {

        }

        private InventoryDto _inventoryDto;

        public override string SaveFileName
        {
            get { return string.Format("AssetsInventory_{0}.xls", DoAssetsId); }
        }

        protected override void QueryData()
        {
            _inventoryDto = base.AssetsOptionService.QueryInventory(DoAssetsId);// .QueryRepair(DoAssetsId);
        }

        protected override void BuildTitle()
        {
            TempRow = Sheet.CreateRow(NextRowIndex);
            TempCell = TempRow.CreateCell(0);
            TempCell.CellStyle = HeaderStyle;
            TempCell.SetCellValue("资产盘点单");
            //设置单元格的高度
            TempRow.Height = 30*20;
            
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0, 9));
            NextRowIndex++;
        }

        protected override void BuildHeader()
        {
            TempRow = Sheet.CreateRow(NextRowIndex);
            TempCell = TempRow.CreateCell(0);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("编号：{0}", _inventoryDto.InventoryInfo.EntityId.ToString().PadLeft(8, '0')));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0, 1));

            TempCell = TempRow.CreateCell(2);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("盘点日期：{0}", _inventoryDto.InventoryInfo.InventoryOperationDate.ToString("yyyy-MM-dd")));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 2, 3));

            TempCell = TempRow.CreateCell(4);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("盘点人：{0}", _inventoryDto.InventoryInfo.InventoryOperationPerson));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 4, 5));

            NextRowIndex++;

            TempRow = Sheet.CreateRow(NextRowIndex);
            TempCell = TempRow.CreateCell(0);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("盘点部门：{0}", _inventoryDto.InventoryInfo.InventoryDepartment));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0, 1));
            TempCell = TempRow.CreateCell(2);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("操作部门：{0}", _inventoryDto.InventoryInfo.InventoryOperationDepartment));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 2, 3));
            //TempCell.SetCellValue(string.Format("联系方式：{0}", _inventoryDto.InventoryInfo.));
            //Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 2, 3));

            //设置单元格的宽度
            Sheet.SetColumnWidth(0, 20 * 256);
            Sheet.SetColumnWidth(1, 20 * 256);
            Sheet.SetColumnWidth(2, 20 * 256);
            Sheet.SetColumnWidth(3, 20 * 256);
            Sheet.SetColumnWidth(4, 20 * 256);
            Sheet.SetColumnWidth(5, 20 * 256);

            NextRowIndex++;
        }

        protected override void FillBody()
        {
            TempRow = Sheet.CreateRow(NextRowIndex);
            TempRow.CreateCell(0).SetCellValue("资产编码");
            TempRow.CreateCell(1).SetCellValue("资产名称");
            TempRow.CreateCell(2).SetCellValue("动态编码");          
            TempRow.CreateCell(4).SetCellValue("计量单位");
            TempRow.CreateCell(5).SetCellValue("数量");
            TempRow.CreateCell(6).SetCellValue("价值");
            TempRow.CreateCell(7).SetCellValue("规格型号");
            TempRow.CreateCell(8).SetCellValue("存放地点");
            TempRow.CreateCell(9).SetCellValue("盘点结果");

            NextRowIndex++;
            foreach (var detail in _inventoryDto.Details)
            {
                TempRow = Sheet.CreateRow(NextRowIndex);
                TempRow.CreateCell(0).SetCellValue(detail.AssetsNo);
                TempRow.CreateCell(1).SetCellValue(detail.GoodsName);
                TempRow.CreateCell(2).SetCellValue(detail.UsedNum1);
                TempRow.CreateCell(4).SetCellValue(detail.MeasurementUnits);
                TempRow.CreateCell(5).SetCellValue(detail.Counts);
                TempRow.CreateCell(6).SetCellValue(detail.Money.ToString());
                TempRow.CreateCell(7).SetCellValue(detail.ModelSpecification);
                TempRow.CreateCell(8).SetCellValue(detail.StorePlace);
                TempRow.CreateCell(9).SetCellValue(detail.InventoryResult);
                NextRowIndex++;
            }
        }

        protected override void BuildFooter()
        {
            TempRow = Sheet.CreateRow(NextRowIndex);
            TempCell = TempRow.CreateCell(0);
            TempCell.CellStyle = FooterCellStyle;
            TempCell.SetCellValue(string.Format("打印日期:{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0, 5));
            NextRowIndex++;
        }

    }
}