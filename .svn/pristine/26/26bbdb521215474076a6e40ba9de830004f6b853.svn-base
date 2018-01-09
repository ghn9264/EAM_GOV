using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EAM.Data.Comm;
using EAM.Data.Domain;
using NPoco;

namespace EAM.Data.Services.Query
{
    public class AllRecordQuery : QueryBase
    {
        public override Sql QuerySql
        {
            get
            {
                var sql = Sql.Builder.Where("1=1");
                //if (!string.IsNullOrEmpty(GoodsName))
                //    //'%' + strKeyword + '%'
                //  sql.Where("GOODS_NAME like @0",string.Format("%{0}%", GoodsName));
                //if (!string.IsNullOrEmpty(AssetsNum))
                //    sql.Where("ASSETS_NUM =@0", AssetsNum);
                return sql;
            }
        }
    }
}