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
    public class UserQuery : QueryBase
    {
        public override Sql QuerySql
        {
            get
            {
                //var sql= Sql.Builder.Select("u.*,d.DEPT_NAME").From("USER_INFO u").LeftJoin("DEPARTMENT d ").On("u.DEPT_ID = d.ID").OrderBy("u.ID desc");
                var sql = Sql.Builder.Select("u.*,d.DEPT_NAME").From("USER_INFO u").LeftJoin("DEPARTMENT d ").On("u.DEPT_ID = d.ID").OrderBy("u.ID desc");
                return sql;
            }
        }
    }
}