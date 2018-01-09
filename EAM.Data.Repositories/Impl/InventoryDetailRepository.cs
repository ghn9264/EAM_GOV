using System.Collections.Generic;
using System.Linq;
using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class InventoryDetailRepository : Repository<InventoryDetailAttribute, int>, IInventoryDetailRepository
    {
        public InventoryDetailRepository(EamDatabase database)
            : base(database)
        {
          
        }
        public List<InventoryDetailAttribute> QueryDto(int inventoryId)
        {
            var sql = "select d.*,m.GOODS_NAME,m.MEASUREMENT_UNITS,m.USING_STATE,m.USED_NUM1,m.MODEL_SPECIFICATIONS,m.STORE_PLACE,m.COUNTS,m.MONEY from ASSETS_INVENTORY_DETAIL d INNER JOIN assets_main m on m.ASSETSNUM=d.ASSETS_NO where d.INVENTORY_FORM_NO=@0 ORDER BY d.ID asc";
            return Db.Query<InventoryDetailAttribute>(sql, inventoryId).ToList();
        }
    } 
}
