using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class SysWarningRepository : Repository<SysWarning, int>, ISysWarningRepository
    {
        public SysWarningRepository(EamDatabase database)
            : base(database)
        {

        }
    }

}