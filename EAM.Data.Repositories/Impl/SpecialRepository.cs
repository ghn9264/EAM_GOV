using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class SpecialRepository : Repository<SpecialAttribute, int>, ISpecialRepository
    {
        public SpecialRepository(EamDatabase database)
            : base(database)
        {

        }
    }
}