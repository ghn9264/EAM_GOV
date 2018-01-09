using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class ReturnDetailRepository : Repository<ReturnDetailAttribute, int>, IReturnDetailRepository
    {
        public ReturnDetailRepository(EamDatabase database)
            : base(database)
        {

        }
    }
}
