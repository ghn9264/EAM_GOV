using System.Collections.Generic;
using EAM.Data.Comm;
using NPoco;
using System;

namespace EAM.Data.Services.Query
{
    public class AssetsScrapQuery : QueryBase
    {
        /// <summary>
        /// 资产名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 使用状态
        /// </summary>
        public string UsingState { get; set; }

        /// <summary>
        /// 价值类型
        /// </summary>
        public string ValueType { get; set; }

        /// <summary>
        /// 资产编号
        /// </summary>
        public string AssetsNum { get; set; }

        /// <summary>
        /// 动态编码
        /// </summary>
        public string UsedNum1 { get; set; }

        /// <summary>
        /// 存放地点
        /// </summary>
        public string StorePlace { get; set; }

        /// <summary>
        /// 价值范围
        /// </summary>
        public decimal MoneyMin { get; set; }
        public decimal MoneyMax { get; set; }

        /// <summary>
        /// 取得日期范围
        /// </summary>
        public DateTime GetDateStart { get; set; }
        public DateTime GetDateEnd { get; set; }

        /// <summary>
        /// 取得方式
        /// </summary>
        public string GetWay { get; set; }

        //使用人
        public string UsePeople { get; set; }

        /// <summary>
        /// 使用属性
        /// </summary>
        public string UsingAttribute { get; set; }
        /// <summary> 
        /// 导入ID add by ryb
        /// </summary>
        public int ImportId { get; set; }

        /// <summary>
        /// 导入类型 add by ryb 
        /// </summary>
        public int ImportType { get; set; }

        /// <summary>
        /// 界面类型
        /// </summary>
        public string PageType { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 使用部门
        /// </summary>
        public string UseDepartment { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 选择资产类型
        /// </summary>
        public string SelectAssetsType { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        public string IndexClass { get; set; }
        public string IndexDept { get; set; }
        public string IndexPlace { get; set; }
        public string MODEL_SPECIFICATIONSandBrand { get; set; }

        public List<string> ClassList { get; set; }
        public List<string> DeptList { get; set; }
        public List<string> PlaceList { get; set; }


        public class ConstTag
        {
            public static string Acquire = "acquire";
            public static string Borrow = "borrow";
            public static string Inventory = "inventory";
            public static string Repair = "repair";
            public static string Service = "service";
            public static string ChangeInfo = "change";
            public static string Scrap = "scrap";

            public static string Query = "query";
            public static string Print = "print";

            public static string AcquireMaintian = "acquiremaintian";
            public static string LendMaintian = "lendmaintian";


        }

        public override Sql QuerySql
        {
            get
            {
                var sql = Sql.Builder.Select("ass.ID as ID,Goods_Name,AssetsNum,Used_Num1,Measurement_Units,Counts,Get_Date").From("assets_main ass").InnerJoin("assets_class_code acc").On("ass.CAT_CODE =acc.ID").Where("datediff(DATE_ADD(ass.GET_DATE, INTERVAL acc.SCRAP_YEAR YEAR),now())<0");
                
                     

         
                
                   

                return sql;
            }
        }
    }
}