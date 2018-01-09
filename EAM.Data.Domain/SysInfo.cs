using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("SYSINFO")]
    [PrimaryKey("ID")]
    public class SysInfo : IEntity<int>
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 操作项目
        /// </summary>
        [Column("DO_ITEM")]
        public string DoItem { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [Column("DO_PERSON")]
        public string DoPerson { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [Column("DO_TIME")]
        public DateTime DoTime { get; set; }

        /// <summary>
        /// 操作备注
        /// </summary>
        [Column("D0_MEMO")]
        public string DoMemo { get; set; }
    }
        
}
