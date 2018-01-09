using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_SERVICE")]
    [PrimaryKey("ID")]
    public class ServiceAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 维修人
        /// </summary>
        [Column("SERVICE_PERSON")]
        public string ServicePerson { get; set; }

        /// <summary>
        /// 维修人联系方式
        /// </summary>
        [Column("SERVICE_PERSON_PHONE")]
        public string ServicePersonPhone { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        [Column("SERVICE_DATE")]
        public DateTime ServiceDate { get; set; }

        /// <summary>
        /// 维修单编号
        /// </summary>
        [Column("SERVICE_FORM_NO")]
        public string ServiceFormNo { get; set; }
    }
}
