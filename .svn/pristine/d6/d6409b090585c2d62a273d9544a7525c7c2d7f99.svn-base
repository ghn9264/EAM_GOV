using NPoco; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NPoco.Linq;

namespace EAM.Data.Comm
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected Repository(Database database)
        {
            Db = database;
        }

        protected Database Db { get; private set; }

        public virtual void Add(TEntity entity)
        {
            Db.Insert(entity);
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            Db.InsertBulk(entities);
        }

        public virtual bool IsNew(TEntity entity)
        {
            return Db.IsNew<TEntity>(entity);
        }

        public virtual int Update(TEntity entity)
        {
            return Db.Update(entity);
        }

        public virtual void Save(TEntity entity)
        {
            Db.Save<TEntity>(entity);
        }

        public virtual int Remove(TKey id)
        {
            return Db.Delete<TEntity>(id);
        }

        public virtual int Remove(IEnumerable<TKey> ids)
        {
            return Db.DeleteMany<TEntity>().Where(x => ids.Contains(x.EntityId)).Execute();
        }

        public virtual int Remove(TEntity entity)
        {
            return Db.Delete(entity);
        }

        public virtual int Remove(IEnumerable<TEntity> entities)
        {
            var ids = entities.Select(x => x.EntityId);
            return Remove(ids);
        }

        public virtual int RemoveAll()
        {
            return Db.DeleteWhere<TEntity>("1=1");
        }

        public virtual TEntity FirstOrDefault(Sql sql)
        {
            return Db.SingleOrDefault<TEntity>(sql);
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Db.FetchWhere(predicate).FirstOrDefault();
        }

        public virtual TEntity FirstOrDefault(string sql, params object[] args)
        {
            return Db.FirstOrDefault<TEntity>(sql, args);
        }

        public virtual TEntity Find(TKey id)
        {
            return Db.SingleById<TEntity>(id);
        }

        public virtual List<TEntity> Find(IEnumerable<TKey> ids)
        {
            return Db.FetchWhere<TEntity>(x => ids.Contains(x.EntityId));
        }

        public virtual List<TEntity> Query(Sql sql)
        {
            return Db.Query<TEntity>(sql).ToList();
        }

        public virtual List<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> orderColumn = null, bool isDesc = false)
        {
            IQueryProvider<TEntity> query = new QueryProvider<TEntity>(Db);
            if (null != predicate)
                query = query.Where(predicate);
            if (null != orderColumn)
                query = isDesc ? query.OrderByDescending(orderColumn) : query.OrderBy(orderColumn);
           
            return query.ToList();
        }

        public virtual List<TEntity> Query(string sql, params object[] args)
        {
            return Db.Query<TEntity>(sql, args).ToList();
        }

        public virtual List<dynamic> DynamicQuery(string sql, params object[] args)
        {
            return Db.Query<dynamic>(sql, args).ToList();
        }

        public virtual PagedList<TEntity> PagedList(long pageIndex, long pageSize, Sql sql)
        {
            var pageData = Db.Page<TEntity>(pageIndex, pageSize, sql);
            var allItem = Db.Query<TEntity>(sql).ToList();
            return new PagedList<TEntity>(pageData.Items, pageIndex, pageSize, pageData.TotalItems, allItem);
        }

        public virtual PagedList<TEntity> PagedList1(long pageIndex, long pageSize, Sql sql)
        {
            var pageData = Db.Page<TEntity>(pageIndex, pageSize, sql);
            return new PagedList<TEntity>(pageData.Items, pageIndex, pageSize, pageData.TotalItems);
        }

        public virtual PagedList<TEntity> PagedList(int pageIndex, int pageSize,
            Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> orderColumn = null,
            bool isDesc = false)
        {
            IQueryProvider<TEntity> query = new QueryProvider<TEntity>(Db);
            if (null != predicate)
                query = query.Where(predicate);
            if (null != orderColumn)
                query = isDesc ? query.OrderByDescending(orderColumn) : query.OrderBy(orderColumn);
            var pageData = query.ToPage(pageIndex, pageSize);
            return new PagedList<TEntity>(pageData.Items, pageIndex, pageSize, pageData.TotalItems);
        }

        public virtual PagedList<TEntity> PagedList(int pageIndex, int pageSize, string sql, params object[] args)
        {
            var pageData = Db.Page<TEntity>(pageIndex, pageSize, sql, args);
            return new PagedList<TEntity>(pageData.Items, pageIndex, pageSize, pageData.TotalItems);
        }

        public virtual PagedList<dynamic> DynamicPagedList(int pageIndex, int pageSize, string sql, params object[] args)
        {
            var pageData = Db.Page<dynamic>(pageIndex, pageSize, sql, args);
            return new PagedList<dynamic>(pageData.Items, pageIndex, pageSize, pageData.TotalItems);
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            QueryProvider<TEntity> query = new QueryProvider<TEntity>(Db);
            if (null != predicate)
                query.Where(predicate);
            return query.Count();
        }

        public virtual int Count(string sql, params object[] args)
        {
            return Db.ExecuteScalar<int>(sql, args);
        }

        public virtual int Execute(string sql, params object[] args)
        {
            return Db.Execute(sql, args);
        }

        public virtual int Execute(Sql sql)
        {
            return Db.Execute(sql);
        }

        /// <summary>
        /// Create By Liu 2017-1-16
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="assetsum"></param>
        /// <returns></returns>
        public virtual int DeleteItemByAssetsNum(string tableName, string assetsum)
        {
            //return Db.Execute("DELETE " + tableName + " WHERE ASSETS_NUM=@0", assetsum);
            return Db.Execute("DELETE FROM " + tableName + " WHERE ASSETSNUM=@0", assetsum);
            
        }
    }
}