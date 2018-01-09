using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services.Impl
{
    public class ReturnService
    {

        private readonly IReturnRepository _assetsReturnRep;

        public ReturnService(IReturnRepository assetsReturnRep)
        {
            _assetsReturnRep = assetsReturnRep;
        }

        public void AddAssetsReturn(ReturnAttribute item)
        {
            _assetsReturnRep.Add(item);
        }


    }
}