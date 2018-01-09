using System;

namespace Eam.Web.Portal.Areas.AssetsInput.Models
{
    public class BookModel
    {
        public string CopyRight { get;set; }

        public int EntityId { get; set; }

        //资产编号
        public string AssetsNum { get; set; }

        //投入使用日期
        public DateTime UseDate { get; set; }

        //档案号
        public string FileNum { get; set; }

        //出版社
        public string Press { get; set; }

        //保存年限
        public string StoreYears { get; set; }

        //出版日期
        public DateTime PressDate { get; set; }
    }
}