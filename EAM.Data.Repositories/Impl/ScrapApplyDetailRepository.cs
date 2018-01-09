using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class ScrapApplyDetailRepository : Repository<AssetsScrapApplyDetail, int>, IScrapApplyDetailRepository
    {
        public ScrapApplyDetailRepository(EamDatabase database)
            : base(database)
        {

        }
    }
}