using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services.Impl
{
    public class ReturnDetailService
    {
        private readonly IReturnDetailRepository _assetsReturnDetailRep;

        public ReturnDetailService(IReturnDetailRepository assetsReturnDetailRep)
        {
            _assetsReturnDetailRep = assetsReturnDetailRep;
        }

        public void AddAssetsReturnDetail(ReturnDetailAttribute item)
        {
            _assetsReturnDetailRep.Add(item);
        }
    }
}