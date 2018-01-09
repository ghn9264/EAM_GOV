using System.Collections.Generic;
using System.Linq;
using EAM.Data.Comm;
using EAM.Data.Domain;
using NPoco;

namespace EAM.Data.Repositories.Impl
{
    public class OrderListRepository : Repository<OrderList, int>, IOrderListRepository
    {
        public OrderListRepository(EamDatabase database)
            : base(database)
        {

        }

        public int RemoveByUserId(string userId)
        {
            return Db.Execute("DELETE FROM USER_ORDERLIST WHERE USER_ID=@0", userId);
        }

        public int RemoveByType(string type)
        {
            return Db.Execute("DELETE FROM USER_ORDERLIST WHERE TYPE=@0", type);
        }

        public List<OrderList> QueryByType(string type)
        {
            var sql = Sql.Builder.Where("TYPE=@0", type);
            return Query(sql).ToList();
        }
        public List<OrderList> QueryByChange(string type)
        {
            var sql = Sql.Builder.Where("ID in (@0)", type);
            return Query(sql).ToList();
        }
        public int UpdateIsPrinted(string assetsNums)
        {
            var sql = "UPDATE USER_ORDERLIST SET IS_PRINTED=@0 WHERE ASSETS_NUM in(@1)";
            return Db.Execute(sql, 1, assetsNums);
        }
        public int ChangeInfo(int entityId, string item, string content)
        {
            return Db.Execute("UPDATE USER_ORDERLIST SET " + item + "=@0 WHERE ID=@1", content, entityId);
        }
        
    }

}