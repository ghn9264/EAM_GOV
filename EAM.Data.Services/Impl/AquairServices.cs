using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services.Impl
{
    public class AquairServices
    {

        private readonly IAquairRepository _assetsAquairRep;

        public AquairServices(IAquairRepository assetsAquairRep)
        {
            _assetsAquairRep = assetsAquairRep;
        }

        public void AddAssetsAquair(AquairAttribute item)
        {
            _assetsAquairRep.Add(item);
        }


    }
}