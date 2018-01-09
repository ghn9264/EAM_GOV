using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class ReturnRepository : Repository<ReturnAttribute, int>, IReturnRepository
    {
        public ReturnRepository(EamDatabase database)
            : base(database)
        {

        }
    } 
}
