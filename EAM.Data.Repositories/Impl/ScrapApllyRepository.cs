using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class ScrapApllyRepository : Repository<AssetsScrapApply, int>, IScrapApllyRepository
    {
        public ScrapApllyRepository(EamDatabase database)
            : base(database)
        {

        }
    }
}