using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Repositories;
using EAM.Data.Services.Query;

namespace EAM.Data.Services.Impl
{
    public class RepairService : IRepairService
    {
        private readonly IRepairRepository _assetsRepairRep;

        public RepairService(IRepairRepository assetsRepairRep)
        {
            _assetsRepairRep = assetsRepairRep;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="item"></param>
        public void AddAssetsRepair(RepairAttribute item)
        {
            _assetsRepairRep.Add(item);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PagedList<RepairAttribute> QueryPage(AllRecordQuery query)
        {
            return _assetsRepairRep.PagedList(query.PageIndex, query.PageSize, query.QuerySql);
        }


    }
}