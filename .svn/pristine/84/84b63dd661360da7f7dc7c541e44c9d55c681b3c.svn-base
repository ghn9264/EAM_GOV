using System.Collections.Generic;
using Eam.Core.Utility;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Repositories;
using EAM.Data.Services.Query;

namespace EAM.Data.Services.Impl
{
    public class UserService : IUserService
    {

        private readonly IOrderListRepository _orderListRep;
        private readonly IAssetsRecordRepository _assetsRecordRep;//2017-06-06 wnn
        private readonly IUserInfoRepository _userInfoRep;
        private readonly IDepartmentRepository _deptRep;

        public UserService(IOrderListRepository orderListRep, IAssetsRecordRepository assetsRecordRep,//2017-06-06 wnn
            IUserInfoRepository userInfoRep,
            IDepartmentRepository deptRep)
        {
            _orderListRep = orderListRep;
            _assetsRecordRep = assetsRecordRep;//2017-06-06 wnn
            _userInfoRep = userInfoRep;
            _deptRep = deptRep;
        }
        #region ORDERLIST
        public bool IsExistOrderList(string userId, string assetsNum, string type)
        {
            var count = _orderListRep.Count(x => x.UserId == userId && x.AssetsNum == assetsNum && x.Type == type);
            return count > 0;
        }


        public OrderList GetOrderListOne(int id)
        {
            var sql = "SELECT * FROM user_orderlist WHERE ID=" + id;
            return _orderListRep.FirstOrDefault(sql);
        }

        public void AddToOrderList(OrderList item)
        {
            _orderListRep.Add(item);
        }

        public IEnumerable<OrderList> Query(string type)
        {
            return _orderListRep.Query(x => x.Type == type).ToArray();
        }


        public IEnumerable<OrderList> QueryByAssetsNum(string type, string assetsnum)
        {
            return _orderListRep.Query(x => x.AssetsNum == assetsnum && x.Type == type).ToArray();
        }

        public void RemoveFormOrderList(IEnumerable<int> ids)
        {
            _orderListRep.Remove(ids);
        }
        public int ChangeInfo(int entityId, string item, string content)
        {
            return _orderListRep.ChangeInfo(entityId, item, content);
        }
        public void RemoveAllFormOrderList()
        {
            _orderListRep.RemoveAll();
        }

        public void RemoveFormOrderList(string type)
        {
            _orderListRep.RemoveByType(type);
        }

        public List<OrderList> QueryFormOrderList(string type)
        {
            return _orderListRep.QueryByType(type);
        }
        public List<OrderList> QueryChangeOrderList(string type)
        {
            return _orderListRep.QueryByChange(type);
        }
        public void EmptyOrderList(string userId)
        {
            _orderListRep.RemoveByUserId(userId);
        }

        public void UpdateIsPrinted(string assetsNum)
        {
            _orderListRep.UpdateIsPrinted(assetsNum);
        }
        public void UpdateOrderList(OrderList item)
        {
            _orderListRep.Update(item);
        }



        #endregion

        #region ASSETSRECORD
        public bool IsExistAssetsRecord(string userId, string assetsNum, string type)
        {
            var count = _assetsRecordRep.Count(x => x.UserId == userId && x.AssetsNum == assetsNum && x.Type == type);
            return count > 0;
        }

        public void AddToAssetsRecord(AssetsRecord item)
        {
            _assetsRecordRep.Add(item);
        }

        public IEnumerable<AssetsRecord> QueryR(string type)
        {
            return _assetsRecordRep.Query(x => x.Type == type).ToArray();
        }
        public IEnumerable<AssetsRecord> QueryNumR(string num)
        {
            return _assetsRecordRep.Query(x => x.AssetsNum == num).ToArray();
        }
        public IEnumerable<AssetsRecord> QueryByAssetsNumR(string type, string assetsnum)
        {
            return _assetsRecordRep.Query(x => x.AssetsNum == assetsnum && x.Type == type).ToArray();
        }

        public void RemoveFormAssetsRecord(IEnumerable<int> ids)
        {
            _assetsRecordRep.Remove(ids);
        }
        public int ChangeInfoR(int entityId, string item, string content)
        {
            return _assetsRecordRep.ChangeInfo(entityId, item, content);
        }
        public void RemoveAllFormAssetsRecord()
        {
            _assetsRecordRep.RemoveAll();
        }

        public void RemoveFormAssetsRecord(string type)
        {
            _assetsRecordRep.RemoveByType(type);
        }

        public List<AssetsRecord> QueryFormAssetsRecord(string type)
        {
            return _assetsRecordRep.QueryByType(type);
        }

        public void EmptyAssetsRecord(string userId)
        {
            _assetsRecordRep.RemoveByUserId(userId);
        }

        public void UpdateIsPrintedR(string assetsNum)
        {
            _assetsRecordRep.UpdateIsPrinted(assetsNum);
        }
        public void UpdateAssetsRecord(AssetsRecord item)
        {
            _assetsRecordRep.Update(item);
        }



        #endregion

        public string GetEncryptPwd(string password)
        {
            return HashEncrypt.Md5HashForHex(password);
        }

        public UserInfo GetUser(string userName)
        {
            var result = _userInfoRep.FirstOrDefault(u => u.UserId == userName);
            return result;
        }

        public UserInfo GetUser(int id)
        {
            var result = _userInfoRep.Find(id);
            return result;
        }

        public void AddUser(UserInfo item)
        {
            _userInfoRep.Add(item);
        }

        public void UpdateUser(UserInfo item)
        {
            _userInfoRep.Update(item);
        }

        public void DeleteUser(int id)
        {
            _userInfoRep.Remove(id);
        }

        public PagedList<UserInfo> GetAll(UserQuery query)
        {
            return _userInfoRep.PagedList(query.PageIndex, query.PageSize, query.QuerySql);
        }

        /// <summary>
        /// 权限更改时更改所有用户权限 2017-05-31 wnn
        /// </summary>
        /// <param name="Role">角色</param>
        /// <param name="Permissions">权限</param>
        public void UpdateRole(string Role, string Permissions)
        {
            _userInfoRep.UpdateRole(Role, Permissions);
        }
    }
}