using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services.Impl
{
    public class AssetsMainService
    {
        private readonly IAssetsMainRepository _assetsMainRep;

        public AssetsMainService(IAssetsMainRepository assetsMainRep)
        {
            _assetsMainRep = assetsMainRep;
        }

        public void AddAssetsMain(AssetsMain item)
        {
            _assetsMainRep.Add(item);
        }
    }
}