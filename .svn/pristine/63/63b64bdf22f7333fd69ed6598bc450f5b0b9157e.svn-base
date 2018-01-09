using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{

    [TableName("ASSETS_HOUSE")]
    [PrimaryKey("ID")]
    public class HouseAttribute : IEntity<int>
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
        /// 权属证明
        /// </summary>
        [Column("OWNERSHIP_CERTIFICATE")]
        public string OwnershipCertificate { get; set; }

        /// <summary>
        /// 权属证号
        /// </summary>
        [Column("OWNERSHIP_CERTIFICATE_NUM")]
        public string OwnershipCertifiateNum { get; set; }

        /// <summary>
        /// 权属年限
        /// </summary>
        [Column("OWNERSHIP_TIME")]
        public string OwnershipTime { get; set; }

        /// <summary>
        /// 发证日期
        /// </summary>
        [Column("ISSUING_DATE")]
        public DateTime IssuingDate { get; set; }

        /// <summary>
        /// 房屋所有权人
        /// </summary>
        [Column("HOUSE_OWNER")]
        public string HouseOwner { get; set; }

        /// <summary>
        /// 设计用途
        /// </summary>
        [Column("DESIGN_USE")]
        public string DesignUse { get; set; }

        /// <summary>
        /// 建筑结构
        /// </summary>
        [Column("BUILDING_STRUCTURE")]
        public string BuildingStruccture { get; set; }

        /// <summary>
        /// 坐落位置
        /// </summary>
        [Column("LOCATION")]
        public string Location { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>
        [Column("AREA")]
        public decimal Area { get; set; }

        [Ignore]
        public int AttrId { get; set; }
    }
}