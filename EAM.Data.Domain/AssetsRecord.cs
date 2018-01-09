using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_RECORD")]
    [PrimaryKey("ID")]
    public class AssetsRecord : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        [Column("USER_ID")]
        public string UserId { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Column("CREATED_TIME")]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 资产编码
        /// </summary>
        [Column("ASSETS_NUM")]
        public string AssetsNum { get; set; }

        /// <summary>
        /// 资产名称
        /// </summary>
        [Column("GOODS_NAME")]
        public string GoodsName { get; set; }

        /// <summary>
        /// 资产类别代码
        /// </summary>
        [Column("CAT_CODE")]
        public string CatCode { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        [Column("PRICE")]
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Column("COUNTS")]
        public int Counts { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [Column("MEASUREMENT_UNITS")]
        public string MeasurementUnits { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [Column("Type")]
        public string Type { get; set; }

        /// <summary>
        /// 是否已经打印
        /// </summary>
        [Column("IS_PRINTED")]
        public int IsPrinted { get; set; }

        /// <summary>
        /// 动态编码
        /// </summary>
        [Column("USED_NUM1")]
        public string UsedNum1 { get; set; }

        /// <summary>
        /// 主表ID
        /// </summary>
        [Column("MAIN_ID")]
        public int MainId { get; set; }

        /// <summary>
        /// 使用人
        /// </summary>
        [Column("USE_PEOPLE")]
        public string UsePeople { get; set; }

        /// <summary>
        /// 存放地点
        /// </summary>
        [Column("STORE_PLACE")]
        public string StorePlace { get; set; }

        /// <summary>
        /// 使用部门
        /// </summary>
        [Column("USE_DEPARTMENT")]
        public string UseDepartment { get; set; }
    }
}