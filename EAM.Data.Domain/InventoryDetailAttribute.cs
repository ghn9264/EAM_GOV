using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_INVENTORY_DETAIL")]
    [PrimaryKey("ID")]
    public class InventoryDetailAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 盘点计划编号
        /// </summary>
        [Column("INVENTORY_FORM_NO")]
        public int InventoryFormNo { get; set; }


        /// <summary>
        /// 资产编号
        /// </summary>
        [Column("ASSETS_NO")]
        public string AssetsNo { get; set; }

        /// <summary>
        /// 盘点人
        /// </summary>
        [Column("INVENTORY_PERSON")]
        public string InventoryPerson { get; set; }

        /// <summary>
        /// 盘点时间
        /// </summary>
        [Column("INVENTORY_DATE")]
        public DateTime InventoryDate { get; set; }

        /// <summary>
        /// 盘点时间
        /// </summary>
        [Column("INVENTORY_RESULT")]
        public string InventoryResult { get; set; }
        
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
        /// 使用状态 ResultColumn
        /// </summary>
        [ResultColumn("USING_STATE")]
        public string UsingState { get; set; }

        /// <summary>
        /// 资产曾用编号1 动态导入使用
        /// </summary>
        [Column("USED_NUM1")]
        public string UsedNum1 { get; set; }

        /// <summary>
        /// 资产数量
        /// </summary>
        [ResultColumn("COUNTS")]
        public string Counts { get; set; }

        /// <summary>
        /// 资产存放地点
        /// </summary>
        [ResultColumn("STORE_PLACE")]
        public string StorePlace { get; set; }

        /// <summary>
        /// 资产型号规格
        /// </summary>
        [ResultColumn("MODEL_SPECIFICATIONS")]
        public string ModelSpecification { get; set; }

        /// <summary>
        /// 价值
        /// </summary>
        [ResultColumn("MONEY")]
        public decimal Money { get; set; }
        
    }
}
