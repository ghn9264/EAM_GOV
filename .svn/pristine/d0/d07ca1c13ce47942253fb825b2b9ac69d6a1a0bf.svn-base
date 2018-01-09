using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_CAR")]
    [PrimaryKey("ID")]
    public class CarAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 资产编号
        /// </summary>
        [Column("ASSETSNUM")]
        public string AssetsNum { get; set; }

        /// <summary>
        /// 车辆用途
        /// </summary>
        [Column("CAR_USER")]
        public string CarUser { get; set; }

        /// <summary>
        /// 使用性质
        /// </summary>
        [Column("USING_TYPE")]
        public string UsingType { get; set; }
        
        /// <summary>
        /// 编制情况
        /// </summary>
        [Column("ORGANIZATION")]
        public string Organization { get; set; }

        /// <summary>
        /// 机动号牌号码
        /// </summary>
        [Column("CAR_NUM")]
        public string CarNum { get; set; }

        /// <summary>
        /// 机动车辆识别代码
        /// </summary>
        [Column("CAR_REC_CODE")]
        public string CarRecCode { get; set; }

        /// <summary>
        /// 机动车辆行驶证发证日期
        /// </summary>
        [Column("CAR_CERTIFICATE_DATE")]
        public DateTime CarCertificateDate { get; set; }

        /// <summary>
        /// 机动车排气量
        /// </summary>
        [Column("CAR_EXHAUST")]
        public string CarExhaust { get; set; }

        /// <summary>
        /// 机动车发动机号
        /// </summary>
        [Column("CAR_ENGINE_NUM")]
        public string CarEngineNum { get; set; }

        /// <summary>
        /// 机动车行驶证注册登记日期
        /// </summary>
        [Column("CAR_EROLL_DATE")]
        public DateTime CarErollDare { get; set; }

        /// <summary>
        /// 机动车辆行驶证所有人
        /// </summary>
        [Column("CAR_OWNER")]
        public string CarOwner { get; set; }

        [Ignore]
        public int AttrId { get; set; }

        /// <summary>
        /// 该属性仅仅是存储主表中的这个字段
        /// </summary>
        [Ignore]
        public string CarBrand { get; set; }
    }
}