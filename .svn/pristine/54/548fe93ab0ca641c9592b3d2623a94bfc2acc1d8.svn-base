using EAM.Data.Comm;
using NPoco;
using System;

namespace EAM.Data.Services.Query
{
    public class UnImportAssetsQuery : QueryBase
    {

        /// <summary>
        /// 资产名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 动态资产编号
        /// </summary>
        public string DynamicNum { get; set; }

        /// <summary>
        /// 办学库存号
        /// </summary>
        public string StoreNum { get; set; }

        /// <summary>
        /// 导入表名字
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 在导入表中第几行
        /// </summary>
        public string TableRow { get; set; }

        /// <summary>
        /// 导入记录ID
        /// </summary>
        public int ImportId { get; set; }

        /// <summary>
        /// 导入类型 1:办学,2:动态
        /// </summary>
        public int ImportType { get; set; }

        /// <summary>
        /// 导入日期
        /// </summary>
        public DateTime ImportTime { get; set; }

        /// <summary>
        /// 导入用户
        /// </summary>
        public string ImportPerson { get; set; }

        /// <summary>
        /// 导入异常
        /// </summary>
        public string Exception { get; set; }
        public override Sql QuerySql
        {
            get
            {
                var sql = Sql.Builder.Where("1=1");


                /*ImportId*/
                if (ImportId > 0)
                   // sql.Where("ID = @0", ImportId);
                    sql.Where("IMPORT_ID = @0", ImportId);

                /*ImportType*/
                if (ImportType > 0)
                    sql.Where("IMPORT_TYPE = @0", ImportType);
      
                return sql;
            }
        }
    }
}