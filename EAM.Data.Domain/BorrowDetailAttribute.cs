using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;


namespace EAM.Data.Domain
{
    [TableName("ASSETS_BORROW_DETAIL")]
    [PrimaryKey("ID")]
    public class BorrowDetailAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 借用单编号
        /// </summary>
        [Column("BORROW_FORM_NO")]
        public int BorrowFormNo { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [Column("ASSETS_NO")]
        public string AssetsNo { get; set; }
        
        /// <summary>
        /// 借用数量
        /// </summary>
        [Column("BORROW_COUNTS")]
        public int BorrowCounts { get; set; }

        /// <summary>
        /// 设备归还状态
        /// </summary>
        [Column("RETURN_STATUS")]
        public string ReturnStatus { get; set; }

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
