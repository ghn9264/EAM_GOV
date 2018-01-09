using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPoco;

namespace EAM.Data.Domain
{
    public class LedgerItem
    {
        /// <summary>
        /// 资产名称 1
        /// </summary>
        [Column("GOODS_NAME")]
        public string GoodsName { get; set; }

        /// <summary>
        /// 资产编号  2
        /// </summary>
        [Column("ASSETSNUM")]
        public string AssetsNum { get; set; }

        /// <summary>
        /// 购置日期 3
        /// </summary>
        [ResultColumn("GETDATE")]
        public string GetDate { get; set; }

        /// <summary>
        /// 品牌 4-1
        /// </summary>
        [Column("BRAND")]
        public string Brand { get; set; }

        /// <summary>
        /// 型号 4-2
        /// </summary>
        [Column("MODEL_SPECIFICATIONS")]
        public string ModelSpecification { get; set; }

        /// <summary>
        /// 购进/拨入数量 5
        /// </summary>
        [ResultColumn("INCOUNT")]
        public int InCount { get; set; }

        /// <summary>
        /// 购进/拨入单价 6
        /// </summary>
        [ResultColumn("INPRICE")]
        public decimal InPrice { get; set; }

        /// <summary> 
        /// 购进/拨入金额 7
        /// </summary>
        [ResultColumn("INMONEY")]
        public int InMoney { get; set; }

        /// <summary>
        /// 使用地点 8
        /// </summary>
        [Column("STORE_PLACE")]
        public string StorePlace { get; set; }

        /// <summary>
        /// 使用人 9
        /// </summary>
        [Column("USE_PEOPLE")]
        public string UsePeople { get; set; }

        /// <summary>
        /// 转出/报废数量数量 10
        /// </summary>
        [ResultColumn("OUTCOUNT")]
        public int OutCount { get; set; }

        /// <summary>
        /// 转出/报废单价 11
        /// </summary>
        [ResultColumn("OUTPRICE")]
        public decimal OutPrice { get; set; }

        /// <summary> 
        /// 转出/报废金额 12
        /// </summary>
        [ResultColumn("OUTMONEY")]
        public int OutMoney { get; set; }

     
        /// <summary>
        /// 剩余数量 13
        /// </summary>
        [ResultColumn("COUNT")]
        public int Count { get; set; }
        /// <summary>
        /// 剩余金额 14
        /// </summary>
        [ResultColumn("MONEY")]
        public decimal Money { get; set; }

        /// <summary>
        /// 年份 15
        /// </summary>
        [ResultColumn("YEAR")]
        public int Year { get; set; }


        /// <summary>
        /// 凭证号 16
        /// </summary>
        [ResultColumn("ACCOUNTING_DOC_NUM")]
        public string AccountingDocNum { get; set; }


        /// <summary>
        /// 折旧 17
        /// </summary>
        [ResultColumn("DEPRECIATION")]
        public decimal Depreciation { get; set; }


    }
}