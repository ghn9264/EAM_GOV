using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services.Impl
{
    public class InventoryDetailService
    {
        private readonly IInventoryDetailRepository _assetsInventoryDetailRep;

        public InventoryDetailService(IInventoryDetailRepository assetsInventoryDetailRep)
        {
            _assetsInventoryDetailRep = assetsInventoryDetailRep;
        }

        public void AddAssetsInventoryDetail(InventoryDetailAttribute item)
        {
            _assetsInventoryDetailRep.Add(item);
        }


    }
}