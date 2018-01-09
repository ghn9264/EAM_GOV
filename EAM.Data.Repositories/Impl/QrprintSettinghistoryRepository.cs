using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class QrprintSettinghistoryRepository : Repository<QrprintSettinghistory, int>, IQrprintSettinghistoryRepository
    {
        public QrprintSettinghistoryRepository(EamDatabase database)
            : base(database)
        {

        }

    }

}