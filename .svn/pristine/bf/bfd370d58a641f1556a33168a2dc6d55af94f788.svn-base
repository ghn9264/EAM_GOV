using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_BUILDING")]
    [PrimaryKey("ID")]
    public class BuildingAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 资产编号
        /// </summary>
        [Column("ASSETSNUM")]
        public string AssetsNum { get; set; }

        /// <summary>
        /// 产权形势
        /// </summary>
        [Column("PROPERTY_FORM")]
        public string PropertyForm { get; set; }

        /// <summary>
        /// 权属证号
        /// </summary>
        [Column("OWNERSHIP_CERTIFICATE_NUM")]
        public string OwnershipCertifiateNum { get; set; }

        /// <summary>
        /// 构筑物面积
        /// </summary>
        [Column("AREA")]
        public decimal Area { get; set; }

        /// <summary>
        /// 坐落位置
        /// </summary>
        [Column("LOCATION")]
        public string Location { get; set; }

        [Ignore]
        public int AttrId { get; set; }
    }
}
