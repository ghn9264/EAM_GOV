using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EAM.Data.Domain;
using EAM.Data.Services;

namespace EAM.Data.ImportAndExport.Export
{
    public class ExportDiffResult : ExportDiffResultBase
    {
        public ExportDiffResult(IAssetsService assetsService)
            : base(assetsService)
        {

        }

        public override AssetsTypes AssetsType
        {
            get { return AssetsTypes.Land; }
        }
        protected override void BuildTitle()
        {
        	  NextRowIndex++;
              NextRowIndex++;
           
        }
     
     
        public override string SaveFileName
        {
            get { return @"比对结果.xls"; }
        }

        protected override void BuildHeader()
        {
          /*  TempRow = Sheet.CreateRow(NextRowIndex);
            TempRow.CreateCell(0).SetCellValue("序号");
            TempRow.CreateCell(1).SetCellValue("资产名称");
            TempRow.CreateCell(2).SetCellValue("固定资产分类代码（2010国标码）");
            TempRow.CreateCell(3).SetCellValue("资产分类名称（可以不录）");
            TempRow.CreateCell(4).SetCellValue("计量单位");
            TempRow.CreateCell(5).SetCellValue("使用属性");
            TempRow.CreateCell(6).SetCellValue("使用状况");
            TempRow.CreateCell(7).SetCellValue("取得方式");
            TempRow.CreateCell(8).SetCellValue("取得日期（2013-1-1）");

            TempRow.CreateCell(9).SetCellValue("产权形式");
            TempRow.CreateCell(10).SetCellValue("权属证明（有产权填）");
            TempRow.CreateCell(11).SetCellValue("权属性质（有产权填）");
            TempRow.CreateCell(12).SetCellValue("权属年限（年数，有产权填）");
            TempRow.CreateCell(13).SetCellValue("权属证号（有产权填）");
            TempRow.CreateCell(14).SetCellValue("发证日期（有产权填）（2013-1-1）");
            TempRow.CreateCell(15).SetCellValue("土地使用权人（有产权填）");
            TempRow.CreateCell(16).SetCellValue("土地使用权类型");
            TempRow.CreateCell(17).SetCellValue("坐落位置");
            TempRow.CreateCell(18).SetCellValue("土地使用权面积（平米）");
            TempRow.CreateCell(19).SetCellValue("分摊面积（平米）");
            TempRow.CreateCell(20).SetCellValue("独用面积（平米）");
            
            TempRow.CreateCell(21).SetCellValue("价值类型");
            TempRow.CreateCell(22).SetCellValue("价值");
            TempRow.CreateCell(23).SetCellValue("财政性资金");
            TempRow.CreateCell(24).SetCellValue("非财政性资金");

            TempRow.CreateCell(25).SetCellValue("摊销");
            TempRow.CreateCell(26).SetCellValue("累计摊销");
            TempRow.CreateCell(27).SetCellValue("净值");
            TempRow.CreateCell(28).SetCellValue("入账日期（2013-1-1）");
            TempRow.CreateCell(29).SetCellValue("入账科目");
            TempRow.CreateCell(30).SetCellValue("会计凭证号");
            TempRow.CreateCell(31).SetCellValue("备注");*/

            NextRowIndex++;
            NextRowIndex++;
        }

        protected override void FillBody()
        {
            if (null == AssetsData)
                return;
            for(int index=0;index<AssetsData.Count;index++)
            {
                DiffResult main = AssetsData[index];
                TempRow = Sheet.CreateRow(NextRowIndex);

                TempRow.CreateCell(0).SetCellValue(index+1);
                TempRow.CreateCell(1).SetCellValue(main.DiffState);
                TempRow.CreateCell(2).SetCellValue(main.LocalState);
                TempRow.CreateCell(3).SetCellValue(main.LocalCode);
                TempRow.CreateCell(4).SetCellValue(main.LocalName);
                TempRow.CreateCell(5).SetCellValue(main.LocalValue.ToString());
                TempRow.CreateCell(6).SetCellValue(main.LocalMode);
                TempRow.CreateCell(7).SetCellValue(main.BxState);
                TempRow.CreateCell(8).SetCellValue(main.BxCode);
                TempRow.CreateCell(9).SetCellValue(main.BxName);
                TempRow.CreateCell(10).SetCellValue(main.BxValue.ToString());
                TempRow.CreateCell(11).SetCellValue(main.BxMode);
                TempRow.CreateCell(12).SetCellValue(main.DiffState);
                TempRow.CreateCell(13).SetCellValue(main.DtCode);
                TempRow.CreateCell(14).SetCellValue(main.DtName);
                TempRow.CreateCell(15).SetCellValue(main.DtValue.ToString());
                TempRow.CreateCell(16).SetCellValue(main.DtMode);
               
                NextRowIndex++;
                Thread.Sleep(10);
            }
        }
    }
}