using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_GENERAL")]
    [PrimaryKey("ID")]
    public class GeneralAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 资产编号
        /// </summary>
        [Column("ASSETSNUM")]
        public string AssetsNum { get; set; }

        /// <summary>
        /// 设备用途
        /// </summary>
        [Column("DEV_USE")]
        public string DevUse { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        [Column("DEV_FACTORY")]
        public string DevFactory { get; set; }

        /// <summary>
        /// 保修截至日期
        /// </summary>
        [Column("WARRANTY_DATE")]
        public DateTime WarrantyDate { get; set; }

        /// <summary>
        /// 保修联系电话
        /// </summary>
        [Column("WARRANTY_PHONE")]
        public string WarrantyPhone { get; set; }

        [Ignore]
        public int AttrId { get; set; }

        /// <summary>
        /// 该属性仅仅是存储主表中的这个字段
        /// </summary>
        [Ignore]
        public string GeneralBrand { get; set; }
    }
}
