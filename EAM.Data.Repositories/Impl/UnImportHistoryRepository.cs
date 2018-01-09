using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class UnImportHistoryRepository : Repository<UnImportHistory, int>, IUnImportHistoryRepository
    {
        public UnImportHistoryRepository(EamDatabase database)
            : base(database)
        {

        }
    }
}