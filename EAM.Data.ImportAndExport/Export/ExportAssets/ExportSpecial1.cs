﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EAM.Data.Domain;
using EAM.Data.Services;

namespace EAM.Data.ImportAndExport.Export
{
    public class ExportSpecial1 : ExportAssetsBase
    {
        public ExportSpecial1(IAssetsService assetsService)
            : base(assetsService)
        {

        }

        public override AssetsTypes AssetsType
        {
            get { return AssetsTypes.SpecialEquipment; }
        }

        public override string SaveFileName
        {
            get { return @"ZC06_专用设备_导入模板.xls"; }
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
            TempRow.CreateCell(8).SetCellValue("取得日期（2013-1-1）");
            TempRow.CreateCell(9).SetCellValue("品牌及规格型号");

            TempRow.CreateCell(10).SetCellValue("生产厂家");

            TempRow.CreateCell(11).SetCellValue("价值类型");
            TempRow.CreateCell(12).SetCellValue("价值");
            TempRow.CreateCell(13).SetCellValue("财政性资金");
            TempRow.CreateCell(14).SetCellValue("非财政性资金");
            TempRow.CreateCell(15).SetCellValue("折旧");
            TempRow.CreateCell(16).SetCellValue("累计折旧");
            TempRow.CreateCell(17).SetCellValue("净值");
            TempRow.CreateCell(18).SetCellValue("入账日期（2013-1-1）");
            TempRow.CreateCell(19).SetCellValue("会计凭证号");
            TempRow.CreateCell(20).SetCellValue("备注");

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

                TempRow.CreateCell(0).SetCellValue(index + 1);
                TempRow.CreateCell(1).SetCellValue(main.GoodsName);
                TempRow.CreateCell(2).SetCellValue(main.CatCode);
                TempRow.CreateCell(3).SetCellValue("");
                TempRow.CreateCell(4).SetCellValue(main.MeasurementUnits);
                TempRow.CreateCell(5).SetCellValue(main.UsingStyle);
                TempRow.CreateCell(6).SetCellValue(main.UsingState);
                TempRow.CreateCell(7).SetCellValue(main.GetWay);
                TempRow.CreateCell(8).SetCellValue(main.GetDate);
                TempRow.CreateCell(9).SetCellValue(main.Brand);

                var attrData = AssetsService.GetSpecial(main.AssetsNum);
                if (null != attrData)
                {
                    TempRow.CreateCell(10).SetCellValue(attrData.DevFactory);
                }

                TempRow.CreateCell(11).SetCellValue(main.ValueType);
                TempRow.CreateCell(12).SetCellValue(main.Money.ToString());
                TempRow.CreateCell(13).SetCellValue(main.GovMoney.ToString());
                TempRow.CreateCell(14).SetCellValue(main.NoneGovMoney.ToString());
                TempRow.CreateCell(15).SetCellValue(main.Depreciation.ToString());
                TempRow.CreateCell(16).SetCellValue(main.AccumulateDepreciation.ToString());
                TempRow.CreateCell(17).SetCellValue(main.NetWorth.ToString());
                TempRow.CreateCell(18).SetCellValue(main.PostingDate);
                TempRow.CreateCell(19).SetCellValue(main.AcountDocNum);
                TempRow.CreateCell(20).SetCellValue(main.Memo);
                NextRowIndex++;
                Thread.Sleep(10);
            }
        }
    }
}