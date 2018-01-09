using EAM.Data.Comm;
using NPoco;
using System;
using System.Collections.Generic;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_REPAIR")]
    [PrimaryKey("ID")]

    public class RepairAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 报修人
        /// </summary>
        [Column("REPAIR_PERSON")]
        public string RepairPerson { get; set; }

        /// <summary>
        /// 保修部门
        /// </summary>
        [Column("REPAIR_DEPARTMENT")]
        public string RepairDepartment { get; set; }

        /// <summary>
        /// 报修人联系方式
        /// </summary>
        [Column("REPAIR_PERSON_PHONE")]
        public string RepairPersonPhone { get; set; }

        /// <summary>
        /// 报修日期
        /// </summary>
        [Column("REPAIR_DATE")]
        public DateTime RepairDate { get; set; }

        /// <summary>
        /// 保修单编号
        /// </summary>
        [Column("REPAIR_FORM_NO")]
        public string RepairFormNo { get; set; }

        /// <summary>
        /// 是否维修
        /// </summary>
        [Column("HAS_SERVICES")]
        public int HasServices { get; set; }

        /// <summary>
        /// 维修人
        /// </summary>
        [Column("SERVICES_PERSON")]
        public string ServicesPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        [Column("SERVICES_DATE")]
        public DateTime ServicesDate { get; set; }

        /// <summary>
        /// 维修备注
        /// </summary>
        [Column("SERVICES_MEMO")]
        public string ServicesMemo { get; set; }

        /// <summary>
        /// 维修人联系方式
        /// </summary>
        [Column("SERVICES_PERSON_PHONE")]
        public string ServicesPersonPhone { get; set; }

        [Ignore]
        public List<string> AssetsNums { get; set; }
    }
}