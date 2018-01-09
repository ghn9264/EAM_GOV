using System;
using EAM.Data.Services;
using EAM.Data.Services.Dto;
using NPOI.SS.Util;

namespace EAM.Data.ImportAndExport.Export.ExportDoAssets
{
    /// <summary>
    /// 导出报修单
    /// </summary> 
    public class ExportDoAssetsScrapInfo : ExportDoAssetsBase
    {
        public string title;
        public ExportDoAssetsScrapInfo(IAssetsOptionService assetsOptionService, int doAssetsId,string _title)
            : base(assetsOptionService, doAssetsId)
        {
            title = _title;
        }

        private ScrapApplyDto _scrapApplyDto;
        
        public override string SaveFileName
        {
            get { return string.Format("AssetsScrapInfo_{0}.xls", DoAssetsId); }
        }

        protected override void QueryData()
        {
            _scrapApplyDto = base.AssetsOptionService.QueryScrap(DoAssetsId);
        }

        protected override void BuildTitle()
        {
            TempRow = Sheet.CreateRow(NextRowIndex);
            TempCell = TempRow.CreateCell(0);
            TempCell.CellStyle = HeaderStyle;
            TempCell.SetCellValue("信息化设备报废明细表");
            //设置单元格的高度
            TempRow.Height = 30*20;
            
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0, 6));
            NextRowIndex++;
        }

        protected override void BuildHeader()
        {
            TempRow = Sheet.CreateRow(NextRowIndex);
            TempCell = TempRow.CreateCell(0);
            TempCell.CellStyle = CommCellStyle;
            //TempCell.SetCellValue(string.Format("学校公章："));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0, 1));
            
            //设置单元格的宽度
            //Sheet.SetColumnWidth(0, 20 * 10);
            //设置单元格的宽度
            Sheet.SetColumnWidth(1, 20 * 256);
            Sheet.SetColumnWidth(2, 20 * 300);
            Sheet.SetColumnWidth(4, 20 * 300);

            NextRowIndex++;
        }

        protected override void FillBody()
        {
            TempRow = Sheet.CreateRow(NextRowIndex);
            TempRow.CreateCell(0).SetCellValue("序号");
            TempRow.CreateCell(1).SetCellValue("学校名称");
            TempRow.CreateCell(2).SetCellValue("固定资产名称");
            TempRow.CreateCell(3).SetCellValue("品牌");           
            TempRow.CreateCell(4).SetCellValue("规格型号");
            TempRow.CreateCell(5).SetCellValue("数量");
            TempRow.CreateCell(6).SetCellValue("报送时间");

            NextRowIndex++;
            int index = 1;
            foreach (var detail in _scrapApplyDto.AssetsScrapAttribute)
            {
                TempRow = Sheet.CreateRow(NextRowIndex);
                TempRow.CreateCell(0).SetCellValue(index.ToString());
                TempRow.CreateCell(1).SetCellValue(title);
                TempRow.CreateCell(2).SetCellValue(detail.GoodsName);
                TempRow.CreateCell(3).SetCellValue(detail.Brand);
                TempRow.CreateCell(4).SetCellValue(detail.ModelSpecification);
                TempRow.CreateCell(5).SetCellValue(detail.Counts);
                NextRowIndex++;
                index++;
            }

        }

        protected override void BuildFooter()
        {
            TempRow = Sheet.CreateRow(NextRowIndex);
            TempCell = TempRow.CreateCell(0);
            TempCell.CellStyle = FooterCellStyle;
            TempCell.SetCellValue("注:请各校打印此表加盖公章,需纸制文档与电子文档各一份。");
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0, 6));
            NextRowIndex++;
        }

    }
}