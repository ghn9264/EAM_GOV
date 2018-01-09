using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EAM.Data.Domain;
using EAM.Data.Services;
using Eam.Core.Utility;
using System.IO;
using NPOI.HSSF.UserModel;

namespace EAM.Data.ImportAndExport.Export
{
    public class ExportLand : ExportAssetsBase
    {
        public ExportLand(IAssetsService assetsService): base(assetsService)
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
            get { return @"ZC01_土地_导入模板.xls"; }
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
                AssetsMain main = AssetsData[index];
                TempRow = Sheet.CreateRow(NextRowIndex);

                TempRow.CreateCell(0).SetCellValue(index+1);
                TempRow.CreateCell(1).SetCellValue(main.GoodsName);
                TempRow.CreateCell(2).SetCellValue(main.CatCode);
                TempRow.CreateCell(3).SetCellValue("");
                TempRow.CreateCell(4).SetCellValue(main.MeasurementUnits);
                TempRow.CreateCell(5).SetCellValue(main.UsingStyle);
                TempRow.CreateCell(6).SetCellValue(main.UsingState);
                TempRow.CreateCell(7).SetCellValue(main.GetWay);
                TempRow.CreateCell(8).SetCellValue(main.GetDate.ToShortDateString());

                var attrData = AssetsService.GetLand(main.AssetsNum);
                if (null != attrData)
                {
                    TempRow.CreateCell(9).SetCellValue(attrData.PropertyForm);
                    TempRow.CreateCell(10).SetCellValue(attrData.OwnershipCertificate);
                    TempRow.CreateCell(11).SetCellValue(attrData.OwnershipStyle);
                    TempRow.CreateCell(12).SetCellValue(attrData.OwnershipTime);
                    TempRow.CreateCell(13).SetCellValue(attrData.OwnershipCertifiateNum);
                    TempRow.CreateCell(14).SetCellValue(attrData.IssuingDate.ToShortDateString());
                    TempRow.CreateCell(15).SetCellValue(attrData.LandOwner);
                    TempRow.CreateCell(16).SetCellValue(attrData.LandUserStyle);
                    TempRow.CreateCell(17).SetCellValue(attrData.Location);
                    TempRow.CreateCell(18).SetCellValue(attrData.AllArea.ToString());
                    TempRow.CreateCell(19).SetCellValue(attrData.SharingArea.ToString());
                    TempRow.CreateCell(20).SetCellValue(attrData.SingleArea.ToString());
                }

                TempRow.CreateCell(21).SetCellValue(main.ValueType);
                TempRow.CreateCell(22).SetCellValue(main.Money.ToString());
                TempRow.CreateCell(23).SetCellValue(main.GovMoney.ToString());
                TempRow.CreateCell(24).SetCellValue(main.NoneGovMoney.ToString());
                TempRow.CreateCell(25).SetCellValue("");
                TempRow.CreateCell(26).SetCellValue("");
                TempRow.CreateCell(27).SetCellValue(main.NetWorth.ToString());
                TempRow.CreateCell(28).SetCellValue(main.PostingDate.ToShortDateString());
                TempRow.CreateCell(29).SetCellValue(main.AcountDocNum);
                TempRow.CreateCell(30).SetCellValue("");
                TempRow.CreateCell(31).SetCellValue(main.Memo);
                NextRowIndex++;
                Thread.Sleep(10);
            }
        }
        public void Loaddata()
        {
            QueryData();
            NextRowIndex = 4;
        }
        public void ResetRowIndex()
        {
            NextRowIndex = 4;
        }
        public void Filldata()
        {
            FillBody();
        }
        public List<List<AssetsMain>> GetPageList<T>()
        {
            List<List<AssetsMain>> PageLists = ListHelper.GetListGroup(AssetsData, 200);
            return PageLists;
        }
        public int GetPagecount()
        {
            int pagecount;
            if (null == AssetsData)
                return 0;
            if (AssetsData.Count < 200) { return 1; }
            else if (AssetsData.Count % 200 == 0) { pagecount = AssetsData.Count / 200; }
            else { pagecount = AssetsData.Count / 200 + 1; }
            return pagecount;
        }
        public void CreatTemplate()
        {
            string strBasePath = AppDomain.CurrentDomain.BaseDirectory;                          //Web程序目录
            string excelTempatePath = strBasePath + "ExportExcelTemplate\\";                    //模板目录
            // string excelTemplateFile = excelTempatePath + "ZC01_土地_导入模板.xls";            //模板文件
            string excelTemplateFile = excelTempatePath + this.SaveFileName;
            // string strNewTempFile = Path.Combine(excelTempatePath, string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss")));
            string strTempFile = excelTempatePath + "temp.xls"; //临时文件路径名
            File.Copy(excelTemplateFile, strTempFile, true);   //模板复制到临时文件
            FileStream fs = new FileStream(strTempFile, FileMode.Open, FileAccess.Read); //打开临时模板

            //将临时模板文件构造为 HSSFWorkbook
            Workbook = new HSSFWorkbook(fs);
            Sheet = Workbook.GetSheetAt(0);
            fs.Close();
            fs.Dispose();
            this.Workbook = Workbook;
        }
        public void SetCurrentAs(List<AssetsMain> list)
        {
            AssetsData = list;
        }
    }
}