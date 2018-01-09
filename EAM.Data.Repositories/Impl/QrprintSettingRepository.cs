using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class QrprintSettingRepository : Repository<QrprintSetting, int>, IQrprintSettingRepository
    {
        public QrprintSettingRepository(EamDatabase database)
            : base(database)
        {

        }

    }

}