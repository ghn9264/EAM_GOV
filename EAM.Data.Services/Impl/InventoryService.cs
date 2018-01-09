using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Repositories;
using EAM.Data.Services.Query;

namespace EAM.Data.Services.Impl
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _assetsInventoryRep;

        public InventoryService(IInventoryRepository assetsInventoryRep)
        {
            _assetsInventoryRep = assetsInventoryRep;
        }

        public void AddAssetsInventory(InventoryAttribute item)
        {
            _assetsInventoryRep.Add(item);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PagedList<InventoryAttribute> QueryPage(AllRecordQuery query)
        {
            return _assetsInventoryRep.PagedList(query.PageIndex, query.PageSize, query.QuerySql);
        }
    }
}