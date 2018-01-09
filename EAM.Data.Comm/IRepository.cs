using System;
using System.Collections.Generic;
using System.Linq.Expressions; 
using NPoco;

namespace EAM.Data.Comm
{ 
    public interface IRepository<TEntity, in TKey> where TEntity : class, IEntity<TKey>
    {
        void Add(TEntity entity);
        void Add(IEnumerable<TEntity> entities);

        bool IsNew(TEntity entity);
        int Update(TEntity entity);

        void Save(TEntity entity);

        int Remove(TKey id);
        int Remove(IEnumerable<TKey> ids);
        int Remove(TEntity entity);
        int Remove(IEnumerable<TEntity> entities);
        int RemoveAll();

        TEntity FirstOrDefault(Sql sql);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(string sql, params object[] args);

        TEntity Find(TKey id);
        List<TEntity> Find(IEnumerable<TKey> ids);

        List<TEntity> Query(Sql sql);
        List<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> orderColumn = null, bool isDesc = false);
        List<TEntity> Query(string sql, params object[] args);

        //IEnumerable<dynamic> DynamicQuery(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> orderColumn = null, bool isDesc = false);
        List<dynamic> DynamicQuery(string sql, params object[] args);

        PagedList<TEntity> PagedList(long page, long itemsPerpage, Sql sql);
        PagedList<TEntity> PagedList1(long page, long itemsPerpage, Sql sql);
        PagedList<TEntity> PagedList(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> orderColumn = null, bool isDesc = false);
        PagedList<TEntity> PagedList(int pageIndex, int pageSize, string sql, params object[] args);

        //PagedList<dynamic> DynamicPagedList(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate);
        PagedList<dynamic> DynamicPagedList(int pageIndex, int pageSize, string sql, params object[] args);
        
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        int Count(string sql, params object[] args);

        int Execute(string sql, params object[] args);

        int Execute(Sql sql);

        int DeleteItemByAssetsNum(string tableName, string assetsum);

    }
}