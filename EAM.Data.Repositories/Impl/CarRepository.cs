using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class CarRepository : Repository<CarAttribute, int>, ICarRepository
    {
        public CarRepository(EamDatabase database)
            : base(database)
        {

        }
    }
     
}
