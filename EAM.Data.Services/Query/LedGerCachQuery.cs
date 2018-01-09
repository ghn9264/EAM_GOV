using System.Collections.Generic;
using EAM.Data.Comm;
using NPoco;
using System;

namespace EAM.Data.Services.Query
{
    public class LedGerCachQuery : QueryBase
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
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        public string IndexClass { get; set; }
        public string IndexDept { get; set; }
        public string IndexPlace { get; set; }

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
                var sql = Sql.Builder.Append("delete from ledger_cach; INSERT INTO ledger_cach SELECT * from assets_main ");
                sql.Where("1=1");
                /*GoodsName*/
                if (!string.IsNullOrEmpty(GoodsName))
                    sql.Where("GOODS_NAME like @0", string.Format("%{0}%", GoodsName));
                /*AssetsNum*/
                if (!string.IsNullOrEmpty(AssetsNum))
                    sql.Where("ASSETSNUM = @0", AssetsNum);
                /*UsedNum1*/
                if (!string.IsNullOrEmpty(UsedNum1))
                    sql.Where("USED_NUM1 = @0", UsedNum1);
                /*GetWay*/
                if(!string.IsNullOrEmpty(GetWay))
                    sql.Where("GET_WAY = @0", GetWay);
                /*UsingState*/
                if (!string.IsNullOrEmpty(UsingState))
                    sql.Where("USING_STATE = @0", UsingState);
                /*StorePlace*/
                 if (!string.IsNullOrEmpty(StorePlace))
                     sql.Where("STORE_PLACE = @0", StorePlace);
                 /*ValueType*/
                 if (!string.IsNullOrEmpty(ValueType))
                     sql.Where("VALUE_TYPE = @0", ValueType);
                 /*MoneyMin*/
                 if (MoneyMin>0)
                     sql.Where("MONEY > @0", MoneyMin);
                 /*MoneyMax*/
                 if (MoneyMax > 0)
                     sql.Where("MONEY <= @0", MoneyMax);

                 /*GetDateStart*/
                 if (GetDateStart > DateTime.MinValue)
                     sql.Where("GET_DATE > @0", GetDateStart);
                 /*MoneyMax*/
                 if (GetDateEnd > DateTime.MinValue)
                     sql.Where("GET_DATE <= @0", GetDateEnd);

                 /*UsingAttribute*/
                 if (!string.IsNullOrEmpty(UsingAttribute))
                     sql.Where("USING_ATTRIBUTE = @0", UsingAttribute);
                 /*UsingAttribute*/
                 
                
                /*ImportedId*/
                 if (ImportId > 0)
                     sql.Where("IMPORT_ID = @0", ImportId);
                 /*ImportedType*/
                 if (ImportType > 0)
                     sql.Where("IMPORT_TYPE = @0", ImportType);


                if (Role == "普通用户")
                {
                    sql.Where("USE_PEOPLE = @0", UserName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(UsePeople))
                        sql.Where("USE_PEOPLE like @0", string.Format("%{0}%", UsePeople));
                }
                

                 
                 
                //
                // 已报废资产不能被查出
                //
                 sql.Where("IS_SCRAP = @0", 0);

                //
                // 已删除资产不能被查出
                //
                 sql.Where("IS_DELETE = @0", 0);

                 /*已经被领用的资产不能被重复领用*/
                 if (PageType == ConstTag.Acquire)
                     sql.Where("IS_USE = @0", 1);

                 //
                 // 资产类别筛选
                 //
                 string temp = "";
                 if (ClassList != null && ClassList.Count >= 1)
                 {
                     for (int i = 0; i < ClassList.Count; i++)
                     {
                         if (ClassList.Count == 1)
                         {
                             //sql.Append("AND CAT_CODE = @0", DeptList[i]);
                             temp = "AND (CAT_CODE = '" + ClassList[i] + "')";
                         }
                         else
                         {
                             if (i == 0)
                             {
                                 //sql.Append("AND CAT_CODE = @0", DeptList[i]);
                                 temp = "AND (CAT_CODE = '" + ClassList[i] + "'";

                             }
                             if (i == (ClassList.Count - 1))
                             {
                                 //sql.Append("AND CAT_CODE = @0", DeptList[i]);
                                 temp += "OR CAT_CODE = '" + ClassList[i] + "')";

                             }
                             else
                             {
                                 temp += "OR CAT_CODE = '" + ClassList[i] + "'";
                             }
                         }
                         
                         
                     }
                     sql.Append(temp);

                 }

                 //
                 // 资产使用部门筛选
                 //
                 temp = "";
                 if (DeptList != null && DeptList.Count >= 1)
                 {
                     for (int i = 0; i < DeptList.Count; i++)
                     {
                         if (DeptList.Count == 1)
                         {
                             //sql.Append("AND CAT_CODE = @0", DeptList[i]);
                             temp = "AND (USE_DEPARTMENT = '" + DeptList[i] + "')";
                         }
                         else
                         {
                             if (i == 0)
                             {
                                 //sql.Append("AND CAT_CODE = @0", DeptList[i]);
                                 temp = "AND (USE_DEPARTMENT = '" + DeptList[i] + "'";

                             }
                             if (i == (DeptList.Count - 1))
                             {
                                 //sql.Append("AND CAT_CODE = @0", DeptList[i]);
                                 temp += "OR USE_DEPARTMENT = '" + DeptList[i] + "')";

                             }
                             else
                             {
                                 temp += "OR USE_DEPARTMENT = '" + DeptList[i] + "'";
                             }
                         }
                         
                         
                     }
                     sql.Append(temp);

                 }

                 //
                 // 资产存放位置筛选
                 //
                 temp = "";
                 if (PlaceList != null && PlaceList.Count >= 1)
                 {
                     for (int i = 0; i < PlaceList.Count; i++)
                     {
                         if (PlaceList.Count == 1)
                         {
                             temp = "AND (STORE_PLACE = '" + PlaceList[i] + "')";
                         }
                         else
                         {
                             if (i == 0)
                             {
                                 //sql.Append("AND CAT_CODE = @0", DeptList[i]);
                                 temp = "AND (STORE_PLACE = '" + PlaceList[i] + "'";

                             }
                             if (i == (PlaceList.Count - 1))
                             {
                                 //sql.Append("AND CAT_CODE = @0", DeptList[i]);
                                 temp += "OR STORE_PLACE = '" + PlaceList[i] + "')";

                             }
                             else
                             {
                                 temp += "OR STORE_PLACE = '" + PlaceList[i] + "'";
                             }
                         }                                                
                     }
                     sql.Append(temp);

                     sql.Append("FROM ASSETS_MAIN");
                 }

                return sql;
            }
        }
    }
}