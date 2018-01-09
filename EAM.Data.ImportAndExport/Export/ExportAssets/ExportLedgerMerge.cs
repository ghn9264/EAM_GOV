using System;
using System.Collections.Generic;
using System.Threading;
using EAM.Data.Domain;
using EAM.Data.ImportAndExport.Import;
using EAM.Data.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.HPSF;
using System.IO;
using NPOI.SS.Util;
//using System.Drawing;
using NPOI.HSSF.Util;
using System.Web;
using System.Net;
using System.Net.Mail;  

namespace EAM.Data.ImportAndExport.Export.ExportAssets
{
    public class ExportLedgerMerge : ExportLedgerMergeBase
    {
        public ExportLedgerMerge(IAssetsService assetsService, AssetsMain assetsMain)
            : base(assetsService, assetsMain)
        {

        }

        public override AssetsTypes AssetsType
        {
            get { return AssetsTypes.Land; }
        }

        /// <summary>
        /// 创建表Title
        /// </summary>
        protected override void BuildTitle()
        {
            NextRowIndex++;
            NextRowIndex++;

        }

        /// <summary>
        /// 创建表头
        /// </summary>
        protected override void BuildHeader()
        {
            NextRowIndex++;
            NextRowIndex++;
        }

        /// <summary>
        /// 填充数据到表格
        /// </summary>
        protected override void FillBody()
        {
            if (null == _AssetsMain)
                return;

            //
            // 填写资产分类
            //
            TempRow = Sheet.GetRow(2);
            TempRow.GetCell(35).SetCellValue(_AssetsMain.CatCode);

            //
            // 填写资产名称
            //
            TempRow = Sheet.GetRow(3);
            TempRow.GetCell(35).SetCellValue(_AssetsMain.GoodsName);

            //
            // 填写资产第一行
            //
            TempRow = Sheet.GetRow(9);
            // 填写时间
            TempRow.GetCell(0).SetCellValue(_AssetsMain.GetDate.Year);      //年
            TempRow.GetCell(1).SetCellValue(_AssetsMain.GetDate.Month);     //月
            TempRow.GetCell(2).SetCellValue(_AssetsMain.GetDate.Day);       //日
            // 填写摘要
            TempRow.GetCell(4).SetCellValue(_AssetsMain.GoodsName);
            // 填写单价
            TempRow.GetCell(5).SetCellValue(_AssetsMain.Money.ToString());
            // 填写数量
            TempRow.GetCell(6).SetCellValue(_AssetsMain.Counts.ToString());
            // 填写￥符号
            TempRow.GetCell(17 - _AssetsMain.Money.ToString().Length).SetCellValue("￥");
            // 拆分购进或拨入金额填写
            for (int i = 0; i < _AssetsMain.Money.ToString().Replace(".", "").Length; i++)
            {
                TempRow.GetCell(18 - _AssetsMain.Money.ToString().Length + i).SetCellValue(_AssetsMain.Money.ToString().Replace(".","").Substring(i, 1));
            }
            // 拆分余额金额填写
            TempRow.GetCell(36).SetCellValue(_AssetsMain.Counts.ToString());
            // 填写￥符号
            TempRow.GetCell(47 - _AssetsMain.Money.ToString().Length).SetCellValue("￥");            
            for (int i = 0; i < _AssetsMain.Money.ToString().Replace(".", "").Length; i++)
            {
                TempRow.GetCell(48 - _AssetsMain.Money.ToString().Length + i).SetCellValue(_AssetsMain.Money.ToString().Replace(".", "").Substring(i, 1));
            }

            //
            // 填写资产第二行
            //
            TempRow = Sheet.GetRow(10);
            TempRow.GetCell(4).SetCellValue("本年合计");
            // 填写数量
            TempRow.GetCell(6).SetCellValue(_AssetsMain.Counts.ToString());
            // 填写￥符号
            TempRow.GetCell(17 - _AssetsMain.Money.ToString().Length).SetCellValue("￥");
            // 拆分购进或拨入金额填写
            for (int i = 0; i < _AssetsMain.Money.ToString().Replace(".", "").Length; i++)
            {
                TempRow.GetCell(18 - _AssetsMain.Money.ToString().Length + i).SetCellValue(_AssetsMain.Money.ToString().Replace(".", "").Substring(i, 1));
            }

            //
            // 填写资产第二行
            //
            TempRow = Sheet.GetRow(11);
            TempRow.GetCell(4).SetCellValue("上年结转");
            // 拆分余额金额填写
            TempRow.GetCell(36).SetCellValue(_AssetsMain.Counts.ToString());
            // 填写￥符号
            TempRow.GetCell(47 - _AssetsMain.Money.ToString().Length).SetCellValue("￥");
            for (int i = 0; i < _AssetsMain.Money.ToString().Replace(".", "").Length; i++)
            {
                TempRow.GetCell(48 - _AssetsMain.Money.ToString().Length + i).SetCellValue(_AssetsMain.Money.ToString().Replace(".", "").Substring(i, 1));
            }

            Thread.Sleep(10);
        }

        /// <summary>
        /// 填充数据到表格并给出进度
        /// </summary>
        /// <param name="progressinfo"></param>
        protected override void FillBody(ref ProgressInfo progressinfo)
        {

            if (null == _AssetsMain)
                return;

            TempRow = Sheet.CreateRow(NextRowIndex);

            // 使用属性0
            TempRow.CreateCell(0).SetCellValue(_AssetsMain.CatCode);

            //
            // 换行
            //
            NextRowIndex++;
            Thread.Sleep(10);
        }
    }
}