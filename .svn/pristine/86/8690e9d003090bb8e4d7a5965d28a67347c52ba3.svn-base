using System;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Repositories;
using EAM.Data.Services.Query;

namespace EAM.Data.Services.Impl
{
    public class SysInfoServices : ISysInfoService
    {
        /// <summary>
        /// 私有字段
        /// </summary>
        private readonly ISysInfoRepository _sysInfoRep;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysInfoRep"></param>
        public SysInfoServices(ISysInfoRepository sysInfoRep)
        {
            _sysInfoRep = sysInfoRep;
        }

        #region 新增
        public void AddSysInfo(SysInfo item)
        {
            _sysInfoRep.Add(item);
        }
        #endregion

        #region 编辑

        public void UpdateSysInfo(SysInfo info)
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
        public PagedList<SysInfo> QueryPage(AllRecordQuery query)
        {
            return _sysInfoRep.PagedList(query.PageIndex, query.PageSize, query.QuerySql);
        }
        #endregion

    }
}
