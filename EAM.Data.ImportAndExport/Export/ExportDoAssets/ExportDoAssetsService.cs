using System;
using EAM.Data.Services;
using EAM.Data.Services.Dto;
using NPOI.SS.Util;

namespace EAM.Data.ImportAndExport.Export.ExportDoAssets
{
    /// <summary>
    /// 导出维修单
    /// </summary>
    public class ExportDoAssetsService : ExportDoAssetsBase
    {
        public ExportDoAssetsService(IAssetsOptionService assetsOptionService, int doAssetsId)
            : base(assetsOptionService, doAssetsId)
        {

        }

        private RepairDto _repairDto;

        public override string SaveFileName
        {
            get { return string.Format("AssetsService_{0}.xls", DoAssetsId); }
        }

        protected override void QueryData()
        {
            _repairDto = base.AssetsOptionService.QueryRepair(DoAssetsId);
        }

        protected override void BuildTitle()
        {
            TempRow = Sheet.CreateRow(NextRowIndex);
            TempCell = TempRow.CreateCell(0);
            TempCell.CellStyle = HeaderStyle;
            TempCell.SetCellValue("资产维修单");
            //设置单元格的高度
            TempRow.Height = 30*20;
            
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0, 5));
            NextRowIndex++;
        }

        protected override void BuildHeader()
        {
            TempRow = Sheet.CreateRow(NextRowIndex);
            TempCell = TempRow.CreateCell(0);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("编号：{0}", _repairDto.RepairInfo.EntityId.ToString().PadLeft(8, '0')));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0, 1));

            TempCell = TempRow.CreateCell(2);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("报修日期：{0}", _repairDto.RepairInfo.RepairDate.ToString("yyyy-MM-dd")));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 2, 3));
            
            TempCell = TempRow.CreateCell(4);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("维修日期：{0}", _repairDto.RepairInfo.ServicesDate.ToString("yyyy-MM-dd")));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 4, 5));

            NextRowIndex++;

            TempRow = Sheet.CreateRow(NextRowIndex);
            TempCell = TempRow.CreateCell(0);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("维修人：{0}", _repairDto.RepairInfo.ServicesPerson));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0, 1));
            
            TempCell = TempRow.CreateCell(2);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("联系方式：{0}", _repairDto.RepairInfo.ServicesPersonPhone));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 2, 3));
            
            //设置单元格的宽度
            Sheet.SetColumnWidth(0, 20 * 256);
            Sheet.SetColumnWidth(1, 20 * 256);
            Sheet.SetColumnWidth(2, 20 * 256);
            Sheet.SetColumnWidth(3, 20 * 256);
            Sheet.SetColumnWidth(4, 20 * 256);
            Sheet.SetColumnWidth(5, 20 * 256);

            NextRowIndex++; 
            
            TempRow = Sheet.CreateRow(NextRowIndex);
            TempRow.CreateCell(0).SetCellValue(string.Format("维修说明：{0}", _repairDto.RepairInfo.ServicesMemo));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0, 5));
            NextRowIndex++;
        }

        protected override void FillBody()
        {
            TempRow = Sheet.CreateRow(NextRowIndex);
            TempRow.CreateCell(0).SetCellValue("资产编码");
            TempRow.CreateCell(1).SetCellValue("资产名称");
            TempRow.CreateCell(2).SetCellValue("资产类别");
            TempRow.CreateCell(3).SetCellValue("单价");           
            TempRow.CreateCell(4).SetCellValue("计量单位");
            TempRow.CreateCell(5).SetCellValue("维修结果");

            NextRowIndex++;
            foreach (var detail in _repairDto.Details)
            {
                TempRow = Sheet.CreateRow(NextRowIndex);
                TempRow.CreateCell(0).SetCellValue(detail.AssetsNo);
                TempRow.CreateCell(1).SetCellValue(detail.GoodsName);
                TempRow.CreateCell(2).SetCellValue(detail.CatCode);
                TempRow.CreateCell(3).SetCellValue(detail.Price.ToString());
                TempRow.CreateCell(4).SetCellValue(detail.MeasurementUnits);
                TempRow.CreateCell(5).SetCellValue(detail.ServicesResult);
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