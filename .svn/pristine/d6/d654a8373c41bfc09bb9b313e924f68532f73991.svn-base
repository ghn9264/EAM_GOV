using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services.Impl
{
    public class AquairDetailServices
    {
        private readonly IAquairDetailRepository _assetsAquairDetailRep;
        public AquairDetailServices(IAquairDetailRepository assetsAquairDetailRep)
        {
            _assetsAquairDetailRep = assetsAquairDetailRep;
        }

        public   void AddAssetsAquairDetail(AquairDetailAttribute item)
        {
            _assetsAquairDetailRep.Add(item);
        }


    }
}
