using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class CulturalrelicRepository : Repository<CulturalrelicAttribute, int>, ICulturalrelicRepository
    {
        public CulturalrelicRepository(EamDatabase database)
            : base(database)
        {

        }
    }
     
}
