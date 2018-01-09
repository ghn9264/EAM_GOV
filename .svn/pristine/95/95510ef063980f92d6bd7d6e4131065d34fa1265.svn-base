using System;
using System.Collections.Generic;
using System.Threading;
using EAM.Data.Domain;
using EAM.Data.ImportAndExport.Export.ExportDoAssets;
using EAM.Data.Services;
using EAM.Data.Services.Dto;
using EAM.Data.Services.Query;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace EAM.Data.ImportAndExport.Export.ExportLedger
{
    /// <summary>
    /// 导出借用单
    /// </summary>
    
  
    public class ExportLedger : ExportDynamicBase
    {
        protected IAssetsOptionService _assetsOptionService;
        public ExportLedger(IAssetsOptionService assetsOptionService)
            : base()
        {
            _assetsOptionService = assetsOptionService;
        }

        private List<LedgerItem> _ledgerTems; //(LedgerQuery query);

        public override string SheetName
        {
            get { return "SHEET1"; }
        }

        public override string SaveFileName
        {
           // get { return string.Format("Ledger_.xls"); }
               get { return @"固定资产明细账_导出模板.xls"; }
        }

        protected override void QueryData()
        {
            // _LedgerItem = base.AssetsOptionService.q (DoAssetsId);
        }

        //标题
        protected override void BuildTitle()
        {
            /*复制模板*/
        }

        //字段
        protected override void BuildHeader()
        {
/*复制模板*/
        }

        //主体

        protected void FillMoney(IRow row, decimal money, int colOffset)
        {
            //  string[] strMoney = new string[11]; //0亿1千2百3十4万5千6百7十8元9角10分
            string strMoney = Convert.ToString(money);
            string[] str = strMoney.Split('.');
            if (strMoney=="0")
            {
                return;  //为零不需要填充
            }
            int len = str.Length;
            int jiao = 0;
            int fen = 0;

            //填充小数部分
            if (str.Length == 1)
            {
                
                 row.CreateCell(colOffset + 10).SetCellValue(fen);
                row.CreateCell(colOffset + 9).SetCellValue(jiao);
                    
                
                //nothing
            }
            else if (len == 2)
            {
                if (str[1].Length == 1) jiao = str[1][0];
                else
                {
                    char[] charJiaofen = str[1].ToCharArray();
                   string strJiao = charJiaofen[0].ToString();
                   string strFen = charJiaofen[1].ToString();
                   jiao = Convert.ToInt32(strJiao);
                   fen = Convert.ToInt32(strFen); //str[1][1];
                  
                }
                row.CreateCell(colOffset + 10).SetCellValue(fen);
                row.CreateCell(colOffset + 9).SetCellValue(jiao);
            }
            else
            {
                //错误
                return;
            }


            //填充整数部分
           


            int col = colOffset + 8;
            char[] charYuan = str[0].ToCharArray();
            //for (int i = col; i > col - str[0].Length; i--)
            int j = 0;
            int lenYuan = str[0].Length;
            for (int i = 0; i < lenYuan; i++)
            {
                j = col - i;
                string strbit = charYuan[lenYuan - i - 1].ToString();
                int intbit = Convert.ToInt32(strbit);
                row.CreateCell(j).SetCellValue(intbit);
            }



        }


        protected override void FillBody()
        {
            NextRowIndex = 6;
            foreach (var item in _ledgerTems)
            {
                TempRow = Sheet.CreateRow(NextRowIndex);
                //  TempRow.CreateCell(34);
                //TempRow.PhysicalNumberOfCreateCell
                TempRow.CreateCell(1).SetCellValue(item.GoodsName); //商品名称
                TempRow.CreateCell(2).SetCellValue(item.AssetsNum); //资产编号
                TempRow.CreateCell(3).SetCellValue(item.GetDate); //购置日期
                TempRow.CreateCell(4).SetCellValue(item.Brand + item.ModelSpecification); //规格型号
                TempRow.CreateCell(5).SetCellValue(item.InCount); //数量
                TempRow.CreateCell(6).SetCellValue((item.InPrice).ToString()); //单价
                FillMoney(TempRow, item.InMoney, 7);
                // TempRow.CreateCell(7).SetCellValue(item.InMoney); //购入或掉拨金额
                TempRow.CreateCell(18).SetCellValue(item.StorePlace); //使用地点
                TempRow.CreateCell(19).SetCellValue(item.UsePeople); //使用（保管）人
                TempRow.CreateCell(20).SetCellValue(item.OutCount); //报废或转出  数量

                //报废或转出
                FillMoney(TempRow, item.OutMoney, 21); //报废或转出  金额
                TempRow.CreateCell(32).SetCellValue(item.Count); //余额  数量
                FillMoney(TempRow, item.Money, 33); //余额  金额
                NextRowIndex++;
                Thread.Sleep(10);


            }

            //




        }

        protected override void BuildFooter()
        {

        }

        protected void QueryData(LedgerQuery query)
        {
            _ledgerTems = _assetsOptionService.LedgerData(query);//.q(DoAssetsId);
        }
        public void DoExport(LedgerQuery query)
        {
            // BuildTitle();
            QueryData(query); //查询数据
            // BuildHeader();
            FillBody();
            // BuildFooter();
        }
    }
}