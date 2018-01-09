using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services.Impl
{
    public class HouseService
    {
        private readonly IHouseRepository _assetsHouseRep;

        public HouseService(IHouseRepository assetsHouseRep)
        {
            _assetsHouseRep = assetsHouseRep;
        }

        public void AddAssetsHouse(HouseAttribute item)
        {
            _assetsHouseRep.Add(item);
        }


    }
}