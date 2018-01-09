using System;
using System.Collections.Generic;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Repositories;
using EAM.Data.Services.Query;

namespace EAM.Data.Services.Impl
{
    public class ClassCodeServices : IClassCodeServices
    {
        /// <summary>
        /// 私有字段
        /// </summary>
        private readonly IClassCodeRepository _classCodeRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="classCodeRepository"></param>
        public ClassCodeServices(IClassCodeRepository classCodeRepository)
        {
            _classCodeRepository = classCodeRepository;
        }

        #region 新增
        public void AddClassCode(ClassCode item)
        {
            _classCodeRepository.Add(item);
        }
        #endregion

        #region 编辑

        public void UpdateClassCode(ClassCode info)
        {
            _classCodeRepository.Update(info);
        }

      
        #endregion

        #region 查询

        public ClassCode Get(string catCode)
        {
            return _classCodeRepository.FirstOrDefault(c => c.EntityId == catCode);
        }

        public List<ClassCode> GetByParentCode(string parentCatCode)
        {
            return _classCodeRepository.Query(c => c.ParentId == parentCatCode);  
        }

        public List<ClassCode> GetAll()
        {
            return _classCodeRepository.Query();
        }

        /// <summary>
        /// 系统信息表查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PagedList<ClassCode> QueryPage(AllRecordQuery query)
        {
            return _classCodeRepository.PagedList(query.PageIndex, query.PageSize, query.QuerySql);
        }
        #endregion

    }
}
