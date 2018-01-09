using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class AnimalRepository : Repository<AnimalandplantAttribute, int>, IAnimalRepository
    {
        public AnimalRepository(EamDatabase database)
            : base(database)
        {

        }
         
    }

}