using System;
using System.Linq.Expressions;
using NPoco;

namespace EAM.Data.Services.Query
{
    public interface IQuery
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
        Sql QuerySql { get; }
    }
}