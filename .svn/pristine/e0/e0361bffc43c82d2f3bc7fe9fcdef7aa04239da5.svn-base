using System.Collections.Generic;
using Eam.Core.Utility;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Repositories;
using EAM.Data.Services.Query;
//2017-05-27 wnn
namespace EAM.Data.Services.Impl
{
    public class RoleService : IRoleService
    {

        private readonly IUserInfoRepository _userInfoRep;
        private readonly IRoleRepository _roleRep;

        public RoleService(
            IUserInfoRepository userInfoRep,
            IRoleRepository roleRep)
        {
            _userInfoRep = userInfoRep;
            _roleRep = roleRep;
        }

        public List<Role> GetAll()
        {
            return _roleRep.Query("SELECT * FROM ROLE");
        }
        /// <summary>
        /// 根据角色获取权限
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        public Role GetRole(string Role)
        {
            var result = _roleRep.FirstOrDefault(r => r.Roles == Role);
            return result;
        }

        /// <summary>
        /// 根据ID获取权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetRole(int id)
        {
            var result = _roleRep.Find(id);
            return result;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item"></param>
        public void AddRole(Role item)
        {
            _roleRep.Add(item);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item"></param>
        public void UpdateRole(Role item)
        {
            _roleRep.Update(item);
            UserInfo userInfo = new UserInfo();
            userInfo.Role = item.Roles;
            userInfo.Permissions = item.Permissions;
            _userInfoRep.Update(userInfo);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteRole(int id)
        {
            _roleRep.Remove(id);
        }

    }
}