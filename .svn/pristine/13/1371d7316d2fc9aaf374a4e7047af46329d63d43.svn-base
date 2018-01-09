using EAM.Data.Comm;
using NPoco;
using System;

namespace EAM.Data.Services.Query
{
    public class ImportHistoryQuery : QueryBase
    {
     

        /// <summary>
        /// 导入用户Id
        /// </summary>
         public string UserId { get; set; }

  
        /// <summary>
        /// 导入文件
        /// </summary>
      
        public string ImportFile { get; set; }

    
        /// <summary>
        /// 导入日期范围
        /// </summary>
        public DateTime GetDateStart { get; set; }
        public DateTime GetDateEnd { get; set; }

        /// <summary>
        /// 导入类型 1:办学,2:动态
        /// </summary>
        public int ImportType { get; set; }
        public int ImportId { get; set; }
        public override Sql QuerySql
        {
            get
            {
                var sql = Sql.Builder.Where("1=1");
                /*UserId*/
                if (!string.IsNullOrEmpty(UserId))
                    sql.Where("USER_ID = @0", string.Format("%{0}%", UserId));
                /*GetDateStart*/
                if (GetDateStart > DateTime.MinValue)
                    sql.Where("IMPORT_TIME > @0", GetDateStart);
                /*GetDateEnd*/
                if (GetDateEnd > DateTime.MinValue)
                    sql.Where("IMPORT_TIME <= @0", GetDateEnd);

                /*ImportFile*/
                if (!string.IsNullOrEmpty(ImportFile))
                    sql.Where("IMPORT_FILE = @0", ImportFile);
             
                 /*ImportId*/
                if (ImportId > 0)
                    sql.Where("ID = @0", ImportId );
               
                /*ImportType*/
                 if (ImportType > 0)
                     sql.Where("IMPORT_TYPE = @0 ", ImportType);

               //  sql.Where("ORDER BY ID DESC");
                 //ORDER BY ID DESC
                 sql.OrderBy("ID DESC");
                return sql;
            }
        }
    }
}