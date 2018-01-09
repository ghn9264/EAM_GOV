using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eam.Web.Portal.Areas.AssetsInput.Models
{
    public class AssetsMainModel
    {
        // 序号
        public int EntityId { get; set; }

        // 资产编号
        public string AssetsNum { get; set; }

        // 资产名称
        public string GoodsName { get; set; }

        // 资产类别代码
        public string CatCode { get; set; }

        // 资产曾用编号1
        public string UsedNum1 { get; set; }

        // 资产曾用编号2
        public string UsedNum2 { get; set; }

        // 取得方式
        public string GetWay { get; set; }

        // 取得日期
        public DateTime GetDate { get; set; }

        // 资产照片路径
        public string AssetsPicPath { get; set; }

        // 经办人
        public string Agent { get; set; }

        // 使用属性
        public string UsingAttribute { get; set; }

        // 使用状况
        public string UsingState { get; set; }

        // 使用方式
        public string UsingStyle { get; set; }

        // 存放地点
        public string StorePlace { get; set; }

        // 价值类型
        public string ValueType { get; set; }

        // 价值
        public decimal Money { get; set; }

        // 财政性资金
        public decimal GovMoney { get; set; }

        // 非财政性资金
        public decimal NoneGovMoney { get; set; }

        // 入账日期
        public DateTime PostingDate { get; set; }

        // 折旧
        public decimal Depreciation { get; set; }

        // 累计折旧
        public decimal AccumulateDepreciation { get; set; }

        // 净值
        public decimal NetWorth { get; set; }

        // 会计凭证号
        public string AcountDocNum { get; set; }

        // 计量单位
        public string MeasurementUnits { get; set; }

        // 资产录入人
        public string InputPerson { get; set; }

        // 库存号
        public string StockNumber { get; set; }

        // 数量
        public int Counts { get; set; }

        // 单价
        public decimal Price { get; set; }

        // 厂家品牌
        public string Brand { get; set; }

        // 型号规格
        public string ModelSpecification { get; set; }

        // 工程材料
        public decimal EngineeringMaterial { get; set; }

        // 是否计入固定资产
        public int IsFixAssets { get; set; }

        // 是否删除
        public int IsDelete { get; set; }

        // 是否可领用
        public int IsUsed { get; set; }

        // 是否可借用
        public int IsBorrow { get; set; }

    }
}