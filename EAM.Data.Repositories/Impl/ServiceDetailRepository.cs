using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class ServiceDetailRepository : Repository<ServiceDetailAttribute, int>, IServiceDetailRepository
    {
        public ServiceDetailRepository(EamDatabase database)
            : base(database)
        {

        }
    } 
}
