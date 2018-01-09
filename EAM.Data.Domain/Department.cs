using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("DEPARTMENT")]
    [PrimaryKey("ID")]
    public class Department : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        [Column("PARENT_ID")]
        public int ParentId { get; set; }

        /// <summary>
        /// 父级名称
        /// </summary>
        [ResultColumn("PARENT_NAME")]
        public string ParentName { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Column("DEPT_NAME")]
        public string DeptName { get; set; }
    }
}