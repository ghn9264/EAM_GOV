using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Services.Query;

namespace EAM.Data.Services
{
    /// <summary>
    /// 提供用户相关操作
    /// </summary>
    public interface IUserService
    {
        #region ORDERLIST

        bool IsExistOrderList(string userId, string assetsNum, string type);

        OrderList GetOrderListOne(int id);

        void AddToOrderList(OrderList item);

        IEnumerable<OrderList> Query(string type);
        IEnumerable<OrderList> QueryByAssetsNum(string type, string assetsnum);
        void RemoveFormOrderList(IEnumerable<int> ids);

        void RemoveFormOrderList(string type);
        void RemoveAllFormOrderList();

        List<OrderList> QueryFormOrderList(string type);
        List<OrderList> QueryChangeOrderList(string type);

        void EmptyOrderList(string userId);

        void UpdateIsPrinted(string assetsNum);
        void UpdateOrderList(OrderList item);
        int ChangeInfo(int entityId, string item, string content);

        #endregion

        #region ASSETSRECORD

        bool IsExistAssetsRecord(string userId, string assetsNum, string type);

        void AddToAssetsRecord(AssetsRecord item);

        IEnumerable<AssetsRecord> QueryR(string type);

        IEnumerable<AssetsRecord> QueryNumR(string num);
        IEnumerable<AssetsRecord> QueryByAssetsNumR(string type, string assetsnum);
        void RemoveFormAssetsRecord(IEnumerable<int> ids);

        void RemoveFormAssetsRecord(string type);
        void RemoveAllFormAssetsRecord();

        List<AssetsRecord> QueryFormAssetsRecord(string type);

        void EmptyAssetsRecord(string userId);

        void UpdateIsPrintedR(string assetsNum);
        void UpdateAssetsRecord(AssetsRecord item);
        int ChangeInfoR(int entityId, string item, string content);

        #endregion

        #region 登陆

        string GetEncryptPwd(string password);

        UserInfo GetUser(string userName);

        #endregion

        #region 用户管理

        UserInfo GetUser(int id);
        void AddUser(UserInfo item);
        void UpdateUser(UserInfo item);
        /// <summary>
        /// 权限更改时更改所有用户权限 2017-05-31 wnn
        /// </summary>
        /// <param name="Role">角色</param>
        /// <param name="Permissions">权限</param>
        void UpdateRole(string Role, string Permissions);
        void DeleteUser(int id);
        PagedList<UserInfo> GetAll(UserQuery query);

        #endregion


    }
}