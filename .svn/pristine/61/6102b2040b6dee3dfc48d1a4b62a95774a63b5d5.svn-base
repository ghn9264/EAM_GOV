using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services.Impl
{
    public class ServiceService
    {

        private readonly IServiceRepository _assetsServiceRep;

        public ServiceService(IServiceRepository assetsServiceRep)
        {
            _assetsServiceRep = assetsServiceRep; 
        }

        public void AddAssetsService(ServiceAttribute item)
        {
            _assetsServiceRep.Add(item);
        }


    }
}