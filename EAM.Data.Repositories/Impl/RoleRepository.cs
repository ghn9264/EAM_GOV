using EAM.Data.Comm;
using EAM.Data.Domain;
using NPoco;

namespace EAM.Data.Repositories.Impl
{
    public class RoleRepository : Repository<Role, int>, IRoleRepository
    {
        public RoleRepository(EamDatabase database)
            : base(database)
        {

        }

        public override Role Find(int id)
        {
            var sql = Sql.Builder.Select("*")
                .From("ROLE");
            return Db.Single<Role>(sql);
        }
    }
}