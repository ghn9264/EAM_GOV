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
    /// 提供所有资产新增,修改,删除,查询操作
    /// </summary>
    public interface IAssetsBorrrowServices
    {

        #region 新增

        void AddBorrowRecord(BorrowAttribute item);

        #endregion

        #region 编辑


        void UpdateBorrowRecord(BorrowAttribute assetsBorrrow);

        #endregion

        #region 删除

        #endregion

        #region 查询

        PagedList<BorrowAttribute> QueryPage(AllRecordQuery query);
        

        #endregion

        #region 导入


        #endregion

    }
}