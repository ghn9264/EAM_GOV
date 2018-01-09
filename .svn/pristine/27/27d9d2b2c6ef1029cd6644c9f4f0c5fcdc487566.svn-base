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
    public class ExportCar : ExportAssetsBase
    {
        public ExportCar(IAssetsService assetsService)
            : base(assetsService)
        {

        }

        public override AssetsTypes AssetsType
        {
            get { return AssetsTypes.Car; }
        }

        public override string SaveFileName
        {
            get { return @"ZC04_车辆_导入模板.xls"; }
        }

        protected override void BuildHeader()
        {
            TempRow = Sheet.CreateRow(NextRowIndex);
            TempRow.CreateCell(0).SetCellValue("序号");
            TempRow.CreateCell(1).SetCellValue("资产名称");
            TempRow.CreateCell(2).SetCellValue("固定资产分类代码（2010国标码）");
            TempRow.CreateCell(3).SetCellValue("资产分类名称（可以不录）");
            TempRow.CreateCell(4).SetCellValue("计量单位");
            TempRow.CreateCell(5).SetCellValue("使用属性");
            TempRow.CreateCell(6).SetCellValue("使用状况");
            TempRow.CreateCell(7).SetCellValue("取得方式");

            TempRow.CreateCell(8).SetCellValue("车辆用途");
            TempRow.CreateCell(9).SetCellValue("使用性质");
            TempRow.CreateCell(10).SetCellValue("品牌型号");
            TempRow.CreateCell(11).SetCellValue("编制情况");
            TempRow.CreateCell(12).SetCellValue("机动号牌号码（车牌号）");
            TempRow.CreateCell(13).SetCellValue("机动车辆识别代号（车架号）");
            TempRow.CreateCell(14).SetCellValue("机动车辆行驶证发证日期（2013-1-1）");
            TempRow.CreateCell(15).SetCellValue("机动车排气量（升）");
            TempRow.CreateCell(16).SetCellValue("机动车发动机号");
            TempRow.CreateCell(17).SetCellValue("机动车行驶证注册登记日期（2013-1-1）");
            TempRow.CreateCell(18).SetCellValue("机动车辆行驶证所有人");

            TempRow.CreateCell(19).SetCellValue("价值类型");
            TempRow.CreateCell(20).SetCellValue("价值");
            TempRow.CreateCell(21).SetCellValue("财政性资金");
            TempRow.CreateCell(22).SetCellValue("非财政性资金");
            TempRow.CreateCell(23).SetCellValue("折旧");
            TempRow.CreateCell(24).SetCellValue("累计折旧");
            TempRow.CreateCell(25).SetCellValue("净值");
            TempRow.CreateCell(26).SetCellValue("入账日期（2013-1-1）");
            TempRow.CreateCell(27).SetCellValue("会计凭证号");
            TempRow.CreateCell(28).SetCellValue("备注");

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
                TempRow.CreateCell(5).SetCellValue(main.UsingAttribute); 
                TempRow.CreateCell(6).SetCellValue(main.UsingStyle);
                TempRow.CreateCell(7).SetCellValue(main.UsingState);
                TempRow.CreateCell(8).SetCellValue(main.GetWay);

                var attrData = AssetsService.GetCar(main.AssetsNum);
                if (null != attrData)
                {
                    TempRow.CreateCell(9).SetCellValue(attrData.CarUser);
                    TempRow.CreateCell(10).SetCellValue(attrData.UsingType);
                    TempRow.CreateCell(11).SetCellValue(attrData.CarBrand);
                    TempRow.CreateCell(12).SetCellValue(attrData.Organization);
                    TempRow.CreateCell(13).SetCellValue(attrData.CarNum);
                    TempRow.CreateCell(14).SetCellValue(attrData.CarRecCode);
                    TempRow.CreateCell(15).SetCellValue(attrData.CarCertificateDate.ToShortDateString());
                    TempRow.CreateCell(16).SetCellValue(attrData.CarExhaust);
                    TempRow.CreateCell(17).SetCellValue(attrData.CarEngineNum);
                    TempRow.CreateCell(18).SetCellValue(attrData.CarErollDare.ToShortDateString());
                    TempRow.CreateCell(19).SetCellValue(attrData.CarOwner);
                }

                TempRow.CreateCell(20).SetCellValue(main.ValueType);
                TempRow.CreateCell(21).SetCellValue(main.Money.ToString());
                TempRow.CreateCell(22).SetCellValue(main.GovMoney.ToString());
                TempRow.CreateCell(23).SetCellValue(main.NoneGovMoney.ToString());
                TempRow.CreateCell(24).SetCellValue(main.Depreciation.ToString());
                TempRow.CreateCell(25).SetCellValue(main.AccumulateDepreciation.ToString());
                TempRow.CreateCell(26).SetCellValue(main.NetWorth.ToString());
                TempRow.CreateCell(27).SetCellValue(main.PostingDate.ToShortDateString());
                TempRow.CreateCell(28).SetCellValue(main.AcountDocNum);
                TempRow.CreateCell(29).SetCellValue(main.Memo);
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