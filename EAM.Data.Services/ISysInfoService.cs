using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Services.Query;

namespace EAM.Data.Services
{
    /// <summary>
    /// 提供所有系统信息新增,修改,删除,查询操作
    /// </summary>
    public interface ISysInfoService
    {
        #region 新增

        /// <summary>
        /// 添加一条系统信息记录
        /// </summary>
        /// <param name="info"></param>
        void AddSysInfo(SysInfo info);

        #endregion

        #region 编辑
        /// <summary>
        /// 更新一条系统信息记录
        /// </summary>
        /// <param name="info"></param>
        void UpdateSysInfo(SysInfo info);

        #endregion

        #region 查询
        PagedList<SysInfo> QueryPage(AllRecordQuery query);

        #endregion
        
        #region 导入

        #endregion

    }
}