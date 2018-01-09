﻿using System;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_IMPORT_HISTORY")]
    [PrimaryKey("ID")]
    public class ImportHistory : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 导入用户
        /// </summary>
        [Column("USER_ID")]
        public string UserId{ get; set; }

        /// <summary>
        /// 导入日期
        /// </summary>
        [Column("IMPORT_TIME")]
        public DateTime ImportTime { get; set; }

        /// <summary>
        /// 领用人联系方式
        /// </summary>
        [Column("IMPORT_FILE")]
        public string ImportFile { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        [Column("TOTAL_ROWS")]
        public int TotalRows { get; set; }

        /// <summary>
        /// 导入记录数
        /// </summary>
        [Column("IMPORT_ROWS")]
        public int ImportRows { get; set; }

        /// <summary>
        /// 未导入记录数
        /// </summary>
        [Column("UNIMPORT_ROWS")]
        public int UnImportRows { get; set; }

        /// <summary>
        /// 导入异常
        /// </summary>
        [Column("EXCEPTION")]
        public string Exception { get; set; }

        /// <summary>
        /// 导入类型 1:办学,2:动态
        /// </summary>
        [Column("IMPORT_TYPE")]
        public int ImportType { get; set; }
    }
     
}