using System.Collections.Generic;
using System.Linq;
using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class BorrowDetailRepository : Repository<BorrowDetailAttribute, int>, IBorrowDetailRepository
    {
        public BorrowDetailRepository(EamDatabase database)
            : base(database)
        {

        }

        public List<BorrowDetailAttribute> QueryDto(int borrowId)
        {
            var sql = "select d.*,m.GOODS_NAME,m.MEASUREMENT_UNITS,m.CAT_CODE,m.PRICE from assets_borrow_detail d INNER JOIN assets_main m on m.ASSETSNUM=d.ASSETS_NO where d.BORROW_FORM_NO=@0 ORDER BY  d.ID asc";
            return Db.Query<BorrowDetailAttribute>(sql, borrowId).ToList();
        }
    }
     
}
