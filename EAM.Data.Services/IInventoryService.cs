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
    public interface IInventoryService
    {
        void AddAssetsInventory(InventoryAttribute item);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        PagedList<InventoryAttribute> QueryPage(AllRecordQuery query);
    }
}