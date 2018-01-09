using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_SPECIAL")]
    [PrimaryKey("ID")]
    public class SpecialAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 资产编号
        /// </summary>
        [Column("ASSETSNUM")]
        public string AssetsNum { get; set; }

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

        [Ignore]
        public string SpecialBrand { get; set; }
    }
}