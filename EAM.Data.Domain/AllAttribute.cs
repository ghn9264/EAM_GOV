using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    public class AllAttribute
    {
        #region 土地

        public string LandPropertyForm { get; set; }

        public string LandOwnershipCertificate { get; set; }

        public string LandOwnershipStyle { get; set; }


        public string LandOwnershipTime { get; set; }


        public string LandOwnershipCertifiateNum { get; set; }

        public DateTime LandIssuingDate { get; set; }

        public string LandOwner { get; set; }

        public string LandUserStyle { get; set; }

        public string LandLocation { get; set; }

        public decimal LandAllArea { get; set; }

        /// <summary>
        /// 分摊面积
        /// </summary>
        public decimal LandSharingArea { get; set; }

        /// <summary>
        /// 独用面积
        /// </summary>
        public decimal LandSingleArea { get; set; }

        #endregion

        #region 房屋

            /// <summary>
            /// 产权形势
            /// </summary>
            public string HousePropertyForm { get; set; }

            /// <summary>
            /// 权属证明
            /// </summary>
            public string HouseOwnershipCertificate { get; set; }

            /// <summary>
            /// 权属证号
            /// </summary>
            public string HouseOwnershipCertifiateNum { get; set; }

            /// <summary>
            /// 权属年限
            /// </summary>
            public string HouseOwnershipTime { get; set; }

            /// <summary>
            /// 发证日期
            /// </summary>
            public DateTime HouseIssuingDate { get; set; }

            /// <summary>
            /// 房屋所有权人
            /// </summary>
            public string HouseOwner { get; set; }

            /// <summary>
            /// 设计用途
            /// </summary>
            public string HouseDesignUse { get; set; }

            /// <summary>
            /// 建筑结构
            /// </summary>
            public string HouseBuildingStruccture { get; set; }

            /// <summary>
            /// 坐落位置
            /// </summary>
            public string HouseLocation { get; set; }

            /// <summary>
            /// 建筑面积
            /// </summary>
            public decimal HouseArea { get; set; }

            //public int AttrId { get; set; }
        #endregion

        #region 构筑物

                /// <summary>
                /// 产权形势
                /// </summary>
                public string BuildPropertyForm { get; set; }

                /// <summary>
                /// 权属证号
                /// </summary>
                public string BuildOwnershipCertifiateNum { get; set; }

                /// <summary>
                /// 构筑物面积
                /// </summary>
                public decimal BuildArea { get; set; }

                /// <summary>
                /// 坐落位置
                /// </summary>
                public string BuildLocation { get; set; }
        #endregion

        #region 车辆

                    /// <summary>
                    /// 车辆用途
                    /// </summary>
                    public string CarUser { get; set; }

                    /// <summary>
                    /// 使用性质
                    /// </summary>
                    public string CarUsingType { get; set; }

                    /// <summary>
                    /// 编制情况
                    /// </summary>
                    public string CarOrganization { get; set; }

                    /// <summary>
                    /// 机动号牌号码
                    /// </summary>
                    public string CarNum { get; set; }

                    /// <summary>
                    /// 机动车辆识别代码
                    /// </summary>
                    public string CarRecCode { get; set; }

                    /// <summary>
                    /// 机动车辆行驶证发证日期
                    /// </summary>
                    public DateTime CarCertificateDate { get; set; }

                    /// <summary>
                    /// 机动车排气量
                    /// </summary>
                    public string CarExhaust { get; set; }

                    /// <summary>
                    /// 机动车发动机号
                    /// </summary>
                    public string CarEngineNum { get; set; }

                    /// <summary>
                    /// 机动车行驶证注册登记日期
                    /// </summary>
                    public DateTime CarErollDare { get; set; }

                    /// <summary>
                    /// 机动车辆行驶证所有人
                    /// </summary>
                    public string CarOwner { get; set; }

                    /// <summary>
                    /// 机动车品牌
                    /// </summary>
                    public string CarBrand { get; set; }

        #endregion

        #region 通用设备
                    /// <summary>
                    /// 设备用途
                    /// </summary>
                    public string GeneralDevUse { get; set; }

                    /// <summary>
                    /// 生产厂家
                    /// </summary>
                    public string GeneralDevFactory { get; set; }

                    /// <summary>
                    /// 保修截至日期
                    /// </summary>
                    public DateTime GeneralWarrantyDate { get; set; }

                    /// <summary>
                    /// 保修联系电话
                    /// </summary>
                    public string GeneralWarrantyPhone { get; set; }

                    /// <summary>
                    /// 保修联系电话
                    /// </summary>
                    public string GeneralBrand { get; set; }

        #endregion

        #region 专用设备

            /// <summary>
            /// 生产厂家
            /// </summary>
            public string SpecialDevFactory { get; set; }

            /// <summary>
            /// 保修截至日期
            /// </summary>
            public DateTime SpecialWarrantyDate { get; set; }

            /// <summary>
            /// 保修联系电话
            /// </summary>
            public string SpecialWarrantyPhone { get; set; }

            /// <summary>
            /// 保修联系电话
            /// </summary>
            public string SpecialBrand { get; set; }
        #endregion

        #region 图书

                /// <summary>
                /// 投入使用日期
                /// </summary>
                public DateTime BookUseDate { get; set; }

                /// <summary>
                /// 档案号
                /// </summary>
                public string BookFileNum { get; set; }

                /// <summary>
                /// 出版社
                /// </summary>
                public string BookPress { get; set; }

                /// <summary>
                /// 保存年限
                /// </summary>
                public string BookStoreYears { get; set; }

                /// <summary>
                /// 出版日期
                /// </summary>
                public DateTime BookPressDate { get; set; }

        #endregion

        #region 家具
        #endregion

        #region 文物

            /// <summary>
            /// 文物等级
            /// </summary>
            public string CuiltureGoodsLevel { get; set; }

            /// <summary>
            /// 藏品年代
            /// </summary>
            public string CuiltureYears { get; set; }

            /// <summary>
            /// 来源地
            /// </summary>
            public string CuiltureSourcePlace { get; set; }
        #endregion

        #region 特殊动植物

            /// <summary>
            /// 出生/栽种年份
            /// </summary>
            public DateTime AnimalBirthday { get; set; }

            /// <summary>
            /// 预计寿命/种龄
            /// </summary>
            public string AnimalAge { get; set; }

            /// <summary>
            /// 纲属科
            /// </summary>
            public string AnimalClass { get; set; }

            /// <summary>
            /// 产地
            /// </summary>
            public string AnimalPalce { get; set; }

        #endregion
    }
}
