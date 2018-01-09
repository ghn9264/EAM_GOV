using System.Collections.Generic;
using System.Linq;
using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class RepairDetailRepository : Repository<RepairDetailAttribute, int>, IRepairDetailRepository
    {
        public RepairDetailRepository(EamDatabase database)
            : base(database)
        {

        }

        public List<RepairDetailAttribute> QueryDto(int repairId)
        {
            var sql = "select d.*,m.GOODS_NAME,m.MEASUREMENT_UNITS,m.CAT_CODE,m.PRICE from assets_repair_detail d INNER JOIN assets_main m on m.ASSETSNUM=d.ASSETS_NO where d.REPAIR_FORM_NO=@0 ORDER BY d.ID asc";
            return Db.Query<RepairDetailAttribute>(sql, repairId).ToList();
        }
    }
}