using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories
{
    public interface IUserInfoRepository : IRepository<UserInfo, int>
    {
        /// <summary>
        /// 权限更改时更改所有用户权限 2017-05-31 wnn
        /// </summary>
        /// <param name="Role">角色</param>
        /// <param name="Permissions">权限</param>
        /// <returns></returns>
        int UpdateRole(string Role, string Permissions);
    }
}