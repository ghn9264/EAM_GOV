using System;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Repositories;
using EAM.Data.Services.Query;

namespace EAM.Data.Services.Impl
{
    public class SysWarningServices : ISysWarningService
    {
        /// <summary>
        /// 私有字段
        /// </summary>
        private readonly ISysWarningRepository _sysWarningRep;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysInfoRep"></param>
        public SysWarningServices(ISysWarningRepository sysInfoRep)
        {
            _sysWarningRep = sysInfoRep;
        }

        #region 新增
        public void AddSysWarning(SysWarning item)
        {
            _sysWarningRep.Add(item);
        }
        #endregion

        #region 编辑

        public void UpdateSysWarning(SysWarning info)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 查询

        /// <summary>
        /// 系统信息表查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PagedList<SysWarning> QueryPage(AllRecordQuery query)
        {
            return _sysWarningRep.PagedList(query.PageIndex, query.PageSize, query.QuerySql);
        }
        #endregion

    }
}
