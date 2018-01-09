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
    public interface IClassCodeServices
    {
        #region 新增

        void AddClassCode(ClassCode item);
        #endregion

        #region 编辑

        void UpdateClassCode(ClassCode info);

        #endregion

        #region 查询

        ClassCode Get(string catCode);

        List<ClassCode> GetByParentCode(string parentCatCode);

        List<ClassCode> GetAll();

        /// <summary>
        /// 系统信息表查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        PagedList<ClassCode> QueryPage(AllRecordQuery query);

        #endregion

    }
}