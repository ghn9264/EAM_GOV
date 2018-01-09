using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_CULTURALRELIC")]
    [PrimaryKey("ID")]
    public class CulturalrelicAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 资产编号
        /// </summary>
        [Column("ASSETSNUM")]
        public string AssetsNum { get; set; }

        /// <summary>
        /// 文物等级
        /// </summary>
        [Column("GOODS_LEVEL")]
        public string GoodsLevel { get; set; }

        /// <summary>
        /// 藏品年代
        /// </summary>
        [Column("YEARS")]
        public string Years { get; set; }

        /// <summary>
        /// 来源地
        /// </summary>
        [Column("SOURCE_PLACE")]
        public string SourcePlace { get; set; }

        [Ignore]
        public int AttrId { get; set; }
    }
}