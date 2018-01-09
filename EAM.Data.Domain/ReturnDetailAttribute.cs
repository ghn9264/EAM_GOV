using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_RETURN_DETAIL")]
    [PrimaryKey("ID")]
    public class ReturnDetailAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 归还单编号
        /// </summary>
        [Column("RETURN_FORM_NO")]
        public string ReturnFormNo { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [Column("ASSETS_NO")]
        public string AssetsNo { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [Column("MESUREMENT_UNIT")]
        public string MesurementUnit { get; set; }

        /// <summary>
        /// 归还数量
        /// </summary>
        [Column("COUNTS")]
        public int Counts { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        [Column("ASSETS_STATE")]
        public string AssetsState { get; set; }
    }
}
