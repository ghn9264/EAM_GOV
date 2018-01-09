using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services.Impl
{
    public class RepairDetailService
    {

        private readonly IRepairDetailRepository _assetsRepairDetailRep;

        public RepairDetailService(IRepairDetailRepository assetsRepairDetailRep)
        {
            _assetsRepairDetailRep = assetsRepairDetailRep;
        }

        public void AddAssetsRepairDetail(RepairDetailAttribute item)
        {
            _assetsRepairDetailRep.Add(item);
        }


    }
}