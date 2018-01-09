using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_SERVICE_DETAIL")]
    [PrimaryKey("ID")]
    public class ServiceDetailAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 维修单编号
        /// </summary>
        [Column("SERVICE_FORM_NO")]
        public string ServiceFormNo { get; set; }

        /// <summary>
        /// 资产编号
        /// </summary>
        [Column("ASSETS_NO")]
        public string AssetsNo { get; set; }

        /// <summary>
        /// 维修结果
        /// </summary>
        [Column("SERVICE_RESULT")]
        public string ServiceResult { get; set; }
    }
}
