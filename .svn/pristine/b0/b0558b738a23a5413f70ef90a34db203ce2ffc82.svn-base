using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NPoco;

namespace EAM.Data.Services.Query
{
    public abstract class QueryBase : IQuery
    {
        public QueryBase()
        {
            PageIndex = 1;
            PageSize = 10;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public abstract Sql QuerySql { get; }

    }
}