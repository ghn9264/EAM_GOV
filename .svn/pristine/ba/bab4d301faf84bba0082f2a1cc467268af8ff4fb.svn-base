using System;
using EAM.Data.Services;
using EAM.Data.Services.Dto;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Web;

namespace EAM.Data.ImportAndExport.Export.ExportDoAssets
{
    /// <summary>
    /// 导出报修单
    /// </summary>
    public class ExportDoAssetsScrapBack : ExportDoAssetsBase
    {
        public string title;
        public ExportDoAssetsScrapBack(IAssetsOptionService assetsOptionService, int doAssetsId,string companyTitle)
            : base(assetsOptionService, doAssetsId)
        {
            title = companyTitle;
        }

        private ScrapApplyDto _scrapApplyDto;

        public override string SaveFileName
        {
            get { return string.Format("AssetsScrapBack_{0}.xls", DoAssetsId); }
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
            TempCell.SetCellValue("海淀区行政事业单位国有资产处理明细表");
            //设置单元格的高度
            TempRow.Height = 30*20;
            IFont font = Workbook.CreateFont();
            font.FontName = "宋体";
            font.FontHeightInPoints = 19;
            font.Boldweight = (short)FontBoldWeight.Bold;
            TempCell.CellStyle.SetFont(font);
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0, 10));
            NextRowIndex++;
        }

        protected override void BuildHeader()
        {
            TempRow = Sheet.CreateRow(NextRowIndex);

            TempCell = TempRow.CreateCell(0);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("填报单位名称（公章）："));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0, 1));

            //
            // 单位名称
            //
            TempCell = TempRow.CreateCell(2);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(title);
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 2, 4));      // 合并单元格
            
            //
            // 编号
            //
            //TempRow = Sheet.CreateRow(NextRowIndex);
            TempCell = TempRow.CreateCell(8);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("编号："));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 8, 9));
            
            //设置单元格的宽度
            //Sheet.SetColumnWidth(0, 20 * 10);             // 序号
            Sheet.SetColumnWidth(1, 20 * 300);              // 资产名称
            Sheet.SetColumnWidth(2, 20 * 256);              // 资产名称
            Sheet.SetColumnWidth(3, 20 * 256);              // 资产名称
            Sheet.SetColumnWidth(4, 20 * 256);              // 规格型号
            Sheet.SetColumnWidth(11, 20 * 300);             // 处置单编号

            NextRowIndex++;
        }

        protected override void FillBody()
        {
            TempRow = Sheet.CreateRow(NextRowIndex);

            TempRow.CreateCell(0).SetCellValue("序号");
            TempRow.CreateCell(1).SetCellValue("资产名称");
            TempRow.CreateCell(2).SetCellValue("资产编号");
            TempRow.CreateCell(3).SetCellValue("购置年月");
            TempRow.CreateCell(4).SetCellValue("型号");           
            TempRow.CreateCell(5).SetCellValue("数量");
            TempRow.CreateCell(6).SetCellValue("单价(元)");
            TempRow.CreateCell(7).SetCellValue("原值(元)");
            TempRow.CreateCell(8).SetCellValue("现值(元)");
            TempRow.CreateCell(9).SetCellValue("备注");
            TempRow.CreateCell(10).SetCellValue("处置单编号");

            NextRowIndex++;
            int index = 1;
            foreach (var detail in _scrapApplyDto.AssetsScrapAttribute)
            {

                TempRow = Sheet.CreateRow(NextRowIndex);

                TempRow.CreateCell(0).SetCellValue(index.ToString());
                TempRow.CreateCell(1).SetCellValue(detail.GoodsName);
                TempRow.CreateCell(2).SetCellValue(detail.UsedNum1);
                TempRow.CreateCell(3).SetCellValue(detail.GetDate.ToShortDateString());
                TempRow.CreateCell(4).SetCellValue(detail.ModelSpecification);
                TempRow.CreateCell(5).SetCellValue(detail.Counts);
                TempRow.CreateCell(6).SetCellValue((detail.Money / detail.Counts).ToString());
                TempRow.CreateCell(7).SetCellValue(detail.Money.ToString());
                TempRow.CreateCell(8).SetCellValue(detail.NetWorth.ToString());
                TempRow.CreateCell(9).SetCellValue(detail.Memo);
                TempRow.CreateCell(10).SetCellValue(detail.AssetsNum);

                NextRowIndex++;
                index++;
            }
        }

        /// <summary>
        /// 表格底部信息输出
        /// </summary>
        protected override void BuildFooter()
        {
            TempRow = Sheet.CreateRow(NextRowIndex);

            //
            // 单位负责人签字
            //
            TempCell = TempRow.CreateCell(0);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("单位负责人签字："));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0,1));

            //
            // 填表人签字
            //
            TempCell = TempRow.CreateCell(5);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("填表人签字："));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 5, 8));

            //
            // 填表日期
            //
            TempCell = TempRow.CreateCell(9);
            TempCell.CellStyle = CommCellStyle;
            TempCell.SetCellValue(string.Format("填表日期："));
            Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex,9, 10));

            //TempCell = TempRow.CreateCell(0);
            //TempCell.CellStyle = FooterCellStyle;
            //TempCell.SetCellValue(string.Format("打印日期:{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            //Sheet.AddMergedRegion(new CellRangeAddress(NextRowIndex, NextRowIndex, 0, 5));
            NextRowIndex++;
        }

    }
}