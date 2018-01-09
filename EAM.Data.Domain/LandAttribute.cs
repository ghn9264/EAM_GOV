using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_LAND")]
    [PrimaryKey("ID")]
    public class LandAttribute : IEntity<int>
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
        /// 权属性质
        /// </summary>
        [Column("OWNERSHIP_STYLE")]
        public string OwnershipStyle { get; set; }

        /// <summary>
        /// 权属年限
        /// </summary>
        [Column("OWNERSHIP_TIME")]
        public string OwnershipTime { get; set; }

        /// <summary>
        /// 权属证号
        /// </summary>
        [Column("OWNERSHIP_CERTIFICATE_NUM")]
        public string OwnershipCertifiateNum { get; set; }

        /// <summary>
        /// 发证日期
        /// </summary>
        [Column("ISSUING_DATE")]
        public DateTime IssuingDate { get; set; }

        /// <summary>
        /// 土地使用权人
        /// </summary>
        [Column("LAND_OWNER")]
        public string LandOwner { get; set; }

        /// <summary>
        /// 土地使用权类型
        /// </summary>
        [Column("LAND_USER_STYLE")]
        public string LandUserStyle { get; set; }

        /// <summary>
        /// 坐落位置
        /// </summary>
        [Column("LOCATION")]
        public string Location { get; set; }

        /// <summary>
        /// 土地使用权面积
        /// </summary>
        [Column("ALL_AREA")]
        public decimal AllArea { get; set; }

        /// <summary>
        /// 分摊面积
        /// </summary>
        [Column("SHARING_AREA")]
        public decimal SharingArea { get; set; }

        /// <summary>
        /// 独用面积
        /// </summary>
        [Column("SINGLE_AREA")]
        public decimal SingleArea { get; set; }

        [Ignore]
        public int AttrId { get; set; }
    }
}