using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_REPAIR_DETAIL")]
    [PrimaryKey("ID")]
    public class RepairDetailAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 保修单编号
        /// </summary>
        [Column("REPAIR_FORM_NO")]
        public int RepairFoemNo { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [Column("ASSETS_NO")]
        public string AssetsNo { get; set; }

        /// <summary>
        /// 问题描述
        /// </summary>
        [Column("ERROR_DESCRIPT")]
        public string ErrorDescript { get; set; }

        /// <summary>
        /// 维修结果
        /// </summary>
        [Column("SERVICES_RESULT")]
        public string ServicesResult { get; set; }


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