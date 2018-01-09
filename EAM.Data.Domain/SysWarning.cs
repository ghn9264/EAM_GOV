using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("SYSWARNING")]
    [PrimaryKey("ID")]
    public class SysWarning : IEntity<int>
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 报警项目
        /// </summary>
        [Column("WARNING_ITEM")]
        public string WarningItem { get; set; }

        /// <summary>
        /// 报警级别
        /// </summary>
        [Column("WARNING_LEVEL")]
        public string WarningLevel { get; set; }

        /// <summary>
        /// 报警时间
        /// </summary>
        [Column("WARNING_TIME")]
        public DateTime WarningTime { get; set; }

        /// <summary>
        /// 报警备注
        /// </summary>
        [Column("WARNING_MEMO")]
        public string WarningMemo { get; set; }
    }
        
}
