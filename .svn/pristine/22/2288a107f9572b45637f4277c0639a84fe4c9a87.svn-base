using EAM.Data.Comm;
using EAM.Data.Domain;
using NPoco;

namespace EAM.Data.Repositories.Impl
{
    public class UserInfoRepository : Repository<UserInfo, int>, IUserInfoRepository
    {
        public UserInfoRepository(EamDatabase database)
            : base(database)
        {

        }

        public override UserInfo Find(int id)
        {
            var sql = Sql.Builder.Select("u.*,d.DEPT_NAME")
                .From("USER_INFO u")
                .LeftJoin("DEPARTMENT d ")
                .On("u.DEPT_ID = d.ID")
                .Where("u.ID=@0", id);
            return Db.Single<UserInfo>(sql);
        }
        /// <summary>
        /// 权限更改时更改所有用户权限 2017-05-31 wnn
        /// </summary>
        /// <param name="Role">角色</param>
        /// <param name="Permissions">权限</param>
        /// <returns></returns>
        public int UpdateRole(string Role, string Permissions)
        {
            var sql = "UPDATE user_info SET PERMISSIONS=@0 WHERE ROLE=@1";
            return Db.Execute(sql, Permissions, Role);
        }

    }
}