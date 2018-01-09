using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class BuildingRepository : Repository<BuildingAttribute, int>, IBuildingRepository
    {
        public BuildingRepository(EamDatabase database)
            : base(database)
        {

        }
    } 
}
