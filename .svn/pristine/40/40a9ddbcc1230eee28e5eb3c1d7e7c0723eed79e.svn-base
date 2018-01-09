using EAM.Data.Comm;
using NPoco;
using System;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_MAIN_COMBINE")]
    [PrimaryKey("ID")]
    [NPoco.ExplicitColumns]
    public class AssetsMainCombine : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 资产编号
        /// </summary>
        [Column("ASSETSNUM")]
        public string AssetsNum { get; set; }

        /// <summary>
        /// 资产名称
        /// </summary>
        [Column("GOODS_NAME")]
        public string GoodsName { get; set; }

        /// <summary>
        /// 资产类别代码
        /// </summary>
        [Column("CAT_CODE")]
        public string CatCode { get; set; }

        /// <summary>
        /// 资产曾用编号1 动态导入使用
        /// </summary>
        [Column("USED_NUM1")]
        public string UsedNum1 { get; set; }

        /// <summary>
        /// 资产曾用编号2 办学导入使用
        /// </summary>
        [Column("USED_NUM2")]
        public string UsedNum2 { get; set; }

        /// <summary>
        /// 取得方式
        /// </summary>
        [Column("GET_WAY")]
        public string GetWay { get; set; }

        /// <summary>
        /// 取得日期
        /// </summary>
        [Column("GET_DATE")]
        public DateTime GetDate { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        [Column("AGENT")]
        public string Agent { get; set; }

        /// <summary>
        /// 使用属性
        /// </summary>
        [Column("USING_ATTRIBUTE")]
        public string UsingAttribute { get; set; }

        /// <summary>
        /// 使用状态
        /// </summary>
        [Column("USING_STATE")]
        public string UsingState { get; set; }

        /// <summary>
        /// 使用方式
        /// </summary>
        [Column("USING_STYLE")]
        public string UsingStyle { get; set; }

        /// <summary>
        /// 存放地点
        /// </summary>
        [Column("STORE_PLACE")]
        public string StorePlace { get; set; }

        /// <summary>
        /// 价值类型
        /// </summary>
        [Column("VALUE_TYPE")]
        public string ValueType { get; set; }

        /// <summary>
        /// 价值
        /// </summary>
        [Column("MONEY")]
        public decimal Money { get; set; }

        /// <summary>
        /// 财政性资金
        /// </summary>
        [Column("GOV_MONEY")]
        public decimal GovMoney { get; set; }

        /// <summary>
        /// 非财政性资金
        /// </summary>
        [Column("NO_GOV_MONEY")]
        public decimal NoneGovMoney { get; set; }

        /// <summary>
        /// 入账日期
        /// </summary>
        [Column("POSTING_DATE")]
        public DateTime PostingDate { get; set; }

        /// <summary>
        /// 折旧
        /// </summary>
        [Column("DEPRECIATION")]
        public decimal Depreciation { get; set; }

        /// <summary>
        /// 累计折旧
        /// </summary>
        [Column("ACCUMULATED_DEPRECIATION")]
        public decimal AccumulateDepreciation { get; set; }

        /// <summary>
        /// 净值
        /// </summary>
        [Column("NET_WORTHNET")]
        public decimal NetWorth { get; set; }

        /// <summary>
        /// 会计凭证号
        /// </summary>
        [Column("ACCOUNTING_DOC_NUM")]
        public string AcountDocNum { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [Column("MEASUREMENT_UNITS")]
        public string MeasurementUnits { get; set; }

        /// <summary>
        /// 录入人
        /// </summary>
        [Column("INPUT_PERSON")]
        public string InputPerson { get; set; }

        /// <summary>
        /// 库存号
        /// </summary>
        [Column("STOCK_NUMBER")]
        public string StockNumber { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Column("COUNTS")]
        public int Counts { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [Column("PRICE")]
        public decimal Price { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [Column("BRAND")]
        public string Brand { get; set; }

        /// <summary>
        /// 型号规格
        /// </summary>
        [Column("MODEL_SPECIFICATIONS")]
        public string ModelSpecification { get; set; }

        /// <summary>
        /// 工程材料
        /// </summary>
        [Column("ENGINEERING_MATERIALS")]
        public decimal EngineeringMaterial { get; set; }

        /// <summary>
        /// 是否计入固定资产
        /// </summary>
        [Column("IS_FIXEDASSETS")]
        public int IsFixAssets { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        [Column("IS_DELETE")]
        public int IsDelete { get; set; }

        /// <summary>
        /// 是否已报废
        /// </summary>
        [Column("IS_SCRAP")]
        public int IsScrap { get; set; }

        /// <summary>
        /// 是否可领用,1为可领用，0为不可领用
        /// </summary>
        [Column("IS_USE")]
        public int IsUse { get; set; }

        /// <summary>
        /// 是否可借用 1为可借用，0为不可借用
        /// </summary>
        [Column("IS_BORROW")]
        public int IsBorrow { get; set; }

        /// <summary>
        /// 资产照片路径
        /// </summary>
        [Column("ASSETS_PIC_PATH")]
        public string AssetsPicPath { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("MEMO")]
        public string Memo { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        [Column("INPUT_TIME")]
        public DateTime InputTime { get; set; }

        /// <summary>
        /// 其他属性存储位置
        /// </summary>
        [Column("EXT_DATA_TABLE")]
        public string ExtDataTable { get; set; }

        /// <summary>
        /// 使用人
        /// </summary>
        [Column("USE_PEOPLE")]
        public string UsePeople { get; set; }


        /// <summary>
        /// 导入历史记录ID  add by ryb 06-28
        /// </summary>
        [Column("IMPORT_ID")]
        public long ImportId { get; set; }

        /// <summary>
        /// 导入历史记录ID  add by ryb 06-28
        /// </summary>
        [Column("IMPORT_TYPE")]
        public long ImportType { get; set; }

        [Column("IS_PRINTED")]
        public int IsPrinted { get; set; }

        /// <summary>
        /// 使用部门
        /// </summary>
        [Column("USE_DEPARTMENT")]
        public string UseDepartment { get; set; }

        /// <summary>
        /// 是否为盘盈资产
        /// </summary>
        [Column("IS_INVENTORY_PROFIT")]
        public byte IsIventoryProfit { get; set; }
        
    }
}
