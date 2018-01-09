using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services.Impl
{
    public class ServiceDetailService
    {
        private readonly IServiceDetailRepository _assetsServiceDetailRep;

        public ServiceDetailService(IServiceDetailRepository assetsServiceDetailRep)
        {
            _assetsServiceDetailRep = assetsServiceDetailRep;
        }

        public void AddAssetsServiceDetail(ServiceDetailAttribute item)
        {
            _assetsServiceDetailRep.Add(item);
        }
    }
}