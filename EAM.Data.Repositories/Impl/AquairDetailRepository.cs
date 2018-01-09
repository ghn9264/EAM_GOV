using System.Collections.Generic;
using System.Linq;
using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class AquairDetailRepository : Repository<AquairDetailAttribute, int>, IAquairDetailRepository
    {
        public AquairDetailRepository(EamDatabase database)
            : base(database)
        {

        }

        public List<AquairDetailAttribute> QueryDto(int acquireId)
        {
            var sql = "select d.*,m.GOODS_NAME,m.MEASUREMENT_UNITS,m.CAT_CODE,m.PRICE from assets_aquair_detail d INNER JOIN assets_main m on m.ASSETSNUM=d.ASSETS_NO where d.ACQUIRE_FORM_NO=@0 ORDER BY d.ID asc";
            return Db.Query<AquairDetailAttribute>(sql, acquireId).ToList();
        }
    }
     
}
