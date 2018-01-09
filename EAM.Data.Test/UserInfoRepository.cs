using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EAM.Data.Comm;
using NPoco;
using NPoco.Linq;

namespace EAM.Data.Test
{
    public class UserInfoRepository : Repository<UserInfo, int>
    {
        public UserInfoRepository(Database database) : base(database)
        {
            
        }

        public IEnumerable<UserInfo> Query(Expression<Func<UserInfo, bool>> predicate, Expression<Func<UserInfo, object>> column,bool isDesc)
        {
            IQueryProvider<UserInfo> query = new QueryProvider<UserInfo>(Db);
            query = query.Where(predicate);
            query = isDesc ? query.OrderByDescending(column) : query.OrderBy(column);
            return query.ToEnumerable();
        }
    }
}