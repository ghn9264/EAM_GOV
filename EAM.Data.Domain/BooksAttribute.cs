using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_BOOK")]
    [PrimaryKey("ID")]
    public class BooksAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 资产编号
        /// </summary>
        [Column("ASSETSNUM")]
        public string AssetsNum { get; set; }

        /// <summary>
        /// 投入使用日期
        /// </summary>
        [Column("USE_DATE")]
        public DateTime UseDate { get; set; }

        /// <summary>
        /// 档案号
        /// </summary>
        [Column("FILE_NUM")]
        public string FileNum { get; set; }

        /// <summary>
        /// 出版社
        /// </summary>
        [Column("PRESS")]
        public string Press { get; set; }

        /// <summary>
        /// 保存年限
        /// </summary>
        [Column("STORE_YEARS")]
        public string StoreYears { get; set; }

        /// <summary>
        /// 出版日期
        /// </summary>
        [Column("PRESS_DATE")]
        public DateTime PressDate { get; set; }

        [Ignore]
        public int AttrId { get; set; }
    }
}