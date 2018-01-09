using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class GeneralRepository : Repository<GeneralAttribute, int>, IGeneralRepository
    {
        public GeneralRepository(EamDatabase database)
            : base(database)
        {

        }

        public void Save(GeneralAttribute attr)
        {
            Db.Save<GeneralAttribute>(attr);
        }
    }
     
}
