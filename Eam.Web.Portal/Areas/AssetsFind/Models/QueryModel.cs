using System;

namespace Eam.Web.Portal.Areas.AssetsFind.Models
{
    public class QueryModel
    {
        /// <summary>
        /// 资产名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 使用状态
        /// </summary>
        public string UsingState { get; set; }

        //取得日期

        /// <summary>
        /// 资产编号
        /// </summary>
        public string AssetsNum { get; set; }


        /// <summary>
        /// 存放地点
        /// </summary>
         public string StorePlace { get; set; }
         /// <summary>
         /// 价值范围
         /// </summary>
         public decimal MoneyRange { get; set; }
         /// <summary>
         /// 取得方式
         /// </summary>
         public string GetWay { get; set; }
         /// <summary>
         /// 会计凭证号
         /// </summary>
         public string AcountDocNum { get; set; }

         //使用人

         /// <summary>
         /// 经办人
         /// </summary>
         public string Agent { get; set; }

         /// <summary>
         /// 入账日期
         /// </summary>
         public DateTime PostingDate { get; set; }

         /// <summary>
         /// 使用属性
         /// </summary>
         public string UsingAttribute { get; set; }

         /// <summary>
         /// 价值范围
         /// </summary>
         public decimal MoneyMin { get; set; }
         public decimal MoneyMax { get; set; }

         /// <summary>
         /// 取得日期范围
         /// </summary>
         public DateTime GetDateStart { get; set; }
         public DateTime GetDateEnd { get; set; }


         /// <summary>
         /// 备注
         /// </summary>
         public string Memo { get; set; }

    }
}