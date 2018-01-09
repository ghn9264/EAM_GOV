using System.Collections.Generic;

namespace EAM.Data.Services
{
    public class CodeTypeItem
    {
        /// <summary>
        /// 资产分类代码头
        /// </summary>
        public string CodePrix { get; set; }

        /// <summary>
        /// 本系统资产类型
        /// </summary>
        public AssetsTypes AssetsType { get; set; }
    }


    public static class Mappings
    {
        /// <summary>
        /// 办学字段映射字典
        /// </summary>
        public static Dictionary<string, string> BxAssetsMapping;

        /// <summary>
        /// 动态字段映射字典
        /// </summary>
        public static Dictionary<string, string> DtAssetsMapping;

        static Mappings()
        {
            #region BxAssetsMapping
            BxAssetsMapping = new Dictionary<string, string>
            {
                {"StockNumber", "库存号"},
                {"GoodsName", "资产名称"}, 
                {"GoodsName2", "扩展名"},
                {"CatCode", "固定资产代码"},
                {"Memo", "增加备注"},
                {"Brand", "厂家品牌"},
                {"MeasurementUnits", "计量单位"},
                {"AcountDocNum", "增加凭单号"},
                {"ModelSpecification", "型号规格"},
                {"IsFixAssets", "是否计入固定资产"},
                {"GetDate", "取得日期(购置日期)"},
                {"PostingDate", "入账日期(增加日期)"},
                {"Price", "单价(元)"},
                {"EngineeringMaterial", "工程材料"},
                {"GovMoney", "金额(元)"},
                {"Counts", "现有数量"},
                {"Agent", "增加经办人"}
            }; 
            #endregion

            #region DtAssetsMapping
            DtAssetsMapping = new Dictionary<string, string>
            {
                /*公共属性*/
                {"AssetsNum", "资产编号"},
                {"GoodsName","资产名称"},
                {"CatCode","资产分类代码"},
                {"GetWay","取得方式"},
                {"GetDate","取得日期"},
                {"MeasurementUnits","计量单位"},
                {"Counts","数量"},
                {"ModelSpecification","品牌及规格型号"},
                {"UsingState","使用状况"},
                {"UsingStyle","使用方向"},
                {"ValueType","价值类型"},
                {"Money","价值"},
                {"GovMoney","财政性资金"},
                {"NoneGovMoney","非财政性资金"},
                {"NetWorth","净值"},
                {"PostingDate","入账日期"},
                {"AcountDocNum","会计凭证号"},
                {"StorePlace","存放地点"},

                /*差异属性*/
                {"CarCertificateDate","行驶证注册登记日期"},    // 车辆-203
                {"CarNum","号牌号码（车牌号）"},         // 车辆-203
                {"CarEngineNum","发动机号"},             // 车辆-203
                {"CarRecCode","车辆识别代号（车架号）"}, // 车辆-203
                {"CarExhaust","排气量"},                 // 车辆-203
                {"CarOwner","车辆行驶证所有人"},         // 车辆-203
                {"UsingType","使用性质"},                // 车辆-203
                {"CarUser","车辆用途"},                  // 车辆-203

                {"Press","出版社"},                      // 图书-501
                {"UseDate","投入使用日期"},              // 图书-501
                {"PressDate","出版日期"},                // 图书-501
                {"FileNum","档案号"},                    // 图书-501
                {"StoreYears","保存年限"},               // 图书-501

                {"DevUse","设备用途"},                   // 通用设备-201
                {"DevFactory","生产厂家"},               // 专用设备-301/通用设备-201
                        
                {"GoodsLevel","文物等级"},               // 文物-401
                {"Years","藏品年代"},                    // 文物-401
                {"SourcePlace","来源地/产地"},           // 文物-401

                {"IssuingDate","发证日期"},              // 房屋-102
                {"DesignUse","设计用途"},                // 房屋-102
                {"BuildingStruccture","建筑结构"},        // 房屋-102
                {"Area","建筑面积"},                     // 房屋-102
                {"HouseOwner","房屋所有权人"},           // 房屋-102
                {"OwnershipTime","权属年限"},            // 房屋-102/土地-101


                {"SingleArea","独用面积"},               // 土地-101
                {"SharingArea","分摊面积"},              // 土地-101
                {"LandOwner","土地使用权人"},            // 土地-101
                {"LandUserStyle","土地使用权类型"},      // 土地-101
                {"AllArea","土地使用权面积"},            // 土地-101
                {"OwnershipCertificate","权属证明"},     // 土地-101/房屋-102
                {"OwnershipCertifiateNum","权属证号"},  // 土地-101/房屋-102
                {"OwnershipStyle","权属性质"},           // 土地-101/房屋-102
                {"Location","坐落位置"},                 // 房屋-102/构筑物-103
                {"PropertyForm","产权形式"}              // 构筑物-103/土地-101/房屋-102
            }; 
            #endregion

            
        }

    }
}