using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_INVENTORY")]
    [PrimaryKey("ID")]
    public class InventoryAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

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
        /// 盘点计划编号
        /// </summary>
        [Column("INVENTORY_FORM_NO")]
        public string InventoryFormNo { get; set; }
        
        /// <summary>
        /// 盘点部门
        /// </summary>
        [Column("INVENTORY_DEPARTMENT")]
        public string InventoryDepartment { get; set; }

        /// <summary>
        /// 是否盘点
        /// </summary>
        [Column("HAS_INVENTORY")]
        public int HasInventory { get; set; }

        /// <summary>
        /// 盘点操作人
        /// </summary>
        [Column("INVENTORY_OPERATION_PERSON")]
        public string InventoryOperationPerson { get; set; }

        /// <summary>
        /// 盘点操作时间
        /// </summary>
        [Column("INVENTORY_OPERATION_DATE")]
        public DateTime InventoryOperationDate { get; set; }

        /// <summary>
        /// 盘点操作部门
        /// </summary>
        [Column("INVENTORY_OPERATION_DEPARTMENT")]
        public string InventoryOperationDepartment { get; set; }

        [ResultColumn]
        public List<string> AssetsNums { get; set; }

    }
}
