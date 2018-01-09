using System;
using EAM.Data.Services;
using EAM.Data.Services.Dto;
using NPOI.SS.Util;

namespace EAM.Data.ImportAndExport.Export.ExportDoAssets
{
    /// <summary>
    /// 导出领用单
    /// </summary>
    public class ExportDoAssetsAcquire : ExportDoAssetsBase
    {
        public ExportDoAssetsAcquire(IAssetsOptionService assetsOptionService, int doAssetsId)
            : base(assetsOptionService, doAssetsId)
        {

        }

        private AcquireDto _acquireDto;

        public override string SaveFileName
        {
            get { return string.Format("AssetsAcquire_{0}.xls", DoAssetsId); }
        }

        protected override void QueryData()
        {
            _acquireDto = base.AssetsOptionService.QueryAcquire(DoAssetsId);
        }

        protected override void BuildTitle()
        {
            TempRow = Sheet.CreateRow(NextRowIndex);
            TempCell = TempRow.CreateCell(0);
            TempCell.CellStyle = HeaderStyle;
            TempCell.SetCellValue("资产领用单");
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
            TempCell.SetCellValue(string.Format("编号：{0}", _acquireDto.AquairInfo.EntityId.ToString().PadLeft(8, '0')));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0, 1));

            TempCell = TempRow.CreateCell(2);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("领用日期：{0}", _acquireDto.AquairInfo.AcquireDate.ToString("yyyy-MM-dd")));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 2, 3));

            /*TempCell = TempRow.CreateCell(4);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("预计归还日期：{0}", _borrowDto.BorrowInfo.BorrowLong.ToString("yyyy-MM-dd")));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 4, 5));*/

            NextRowIndex++;

            TempRow = Sheet.CreateRow(NextRowIndex);
            TempCell = TempRow.CreateCell(0);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("领用人：{0}", _acquireDto.AquairInfo.AcquirePerson));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0, 1));
            TempCell = TempRow.CreateCell(2);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("所在部门：{0}", _acquireDto.AquairInfo.AcquireDepartment));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 2, 3));

            TempCell = TempRow.CreateCell(4);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("联系方式：{0}", _acquireDto.AquairInfo.AcquirePersonPhone));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 4, 5));

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
            TempRow.CreateCell(2).SetCellValue("资产类别");
            TempRow.CreateCell(3).SetCellValue("单价");           
            TempRow.CreateCell(4).SetCellValue("计量单位");
            TempRow.CreateCell(5).SetCellValue("数量");

            NextRowIndex++;
            foreach (var detail in _acquireDto.Details)
            {
                TempRow = Sheet.CreateRow(NextRowIndex);
                TempRow.CreateCell(0).SetCellValue(detail.AssetsNo);
                TempRow.CreateCell(1).SetCellValue(detail.GoodsName);
                TempRow.CreateCell(2).SetCellValue(detail.CatCode);
                TempRow.CreateCell(3).SetCellValue(detail.Price.ToString());
                TempRow.CreateCell(4).SetCellValue(detail.MeasurementUnits);
                TempRow.CreateCell(5).SetCellValue(detail.AquairCounts);
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