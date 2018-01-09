using System;
using EAM.Data.Comm;
using EAM.Data.Domain;
using NPoco;




namespace EAM.Data.Repositories.Impl
{
    public class ClassCodeRepository : Repository<ClassCode, string>, IClassCodeRepository
    {
        public ClassCodeRepository(EamDatabase database)
            : base(database)
        {

        }

        public ClassCode GetClassCode(string catCode)
        {
            try
            { return FirstOrDefault(x => x.EntityId == catCode); }
            catch (Exception ex)
            {
                return null;
            }
            
            //return Db.FirstOrDefault<ClassCode>(Sql.Builder.Where("EntityId=@0", catCode));
        }

        public int UpdateNextNum(string cateCode)
        {
            string sql = "UPDATE ASSETS_CLASS_CODE SET NEXTNUM=NEXTNUM+1 WHERE ID=@0";
            return Db.Execute(sql, cateCode);
        }
    }

}