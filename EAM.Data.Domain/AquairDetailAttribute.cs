using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_AQUAIR_DETAIL")]
    [PrimaryKey("ID")]
    public class AquairDetailAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 领用单编号
        /// </summary>
        [Column("ACQUIRE_FORM_NO")]
        public int AcquireFormNo { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [Column("ASSETS_NO")]
        public string AssetsNo { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [Column("SENDBACK_STATUS")]
        public string SendBackState { get; set; }

        /// <summary>
        /// 领用数量
        /// </summary>
        [Column("AQUAIR_COUNTS")]
        public int AquairCounts { get; set; } 


        /// <summary>
        /// 资产名称 ResultColumn
        /// </summary>
        [ResultColumn("GOODS_NAME")]
        public string GoodsName { get; set; }

        /// <summary>
        /// 计量单位 ResultColumn
        /// </summary>
        [ResultColumn("MEASUREMENT_UNITS")]
        public string MeasurementUnits { get; set; }

        /// <summary>
        /// 资产类别代码 ResultColumn
        /// </summary>
        [ResultColumn("CAT_CODE")]
        public string CatCode { get; set; }

        /// <summary>
        /// 资产类别代码 ResultColumn
        /// </summary>
        [ResultColumn("PRICE")]
        public decimal Price { get; set; }
    }
}