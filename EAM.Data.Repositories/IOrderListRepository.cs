using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using EAM.Data.Domain;
using NPoco;

namespace EAM.Data.Repositories
{
    public interface IOrderListRepository : IRepository<OrderList, int>
    {
        int RemoveByUserId(string userId);
        int RemoveByType(string type);

        List<OrderList> QueryByType(string type);
        List<OrderList> QueryByChange(string type);

        int UpdateIsPrinted(string assetsNums);
        int ChangeInfo(int entityId, string item, string content);


    }
}