using System.Collections.Generic;
using System.Linq;
using EAM.Data.Comm;
using EAM.Data.Domain;
using NPoco;

namespace EAM.Data.Repositories.Impl
{
    public class LedGerCachRepository : Repository<LedGerCach, int>, ILedGerCachRepository
    {
        public LedGerCachRepository(EamDatabase database)
            : base(database)
        {

        }

        public int LogicDelete(int entityId)
        {
            return Db.Execute("UPDATE ASSETS_MAIN SET IS_DELETE=1 WHERE ID=@0", entityId);
        }

        public List<LedGerCach> QueryAssets(List<string> catCodePrixs)
        {
            var sql = Sql.Builder.Where("LEFT(CAT_CODE,3) IN(@0)", catCodePrixs);
            return Query(sql);
        }

        public List<LedgerItem> LedgerData(Sql sql)
        {
            return Db.Query<LedgerItem>(sql).ToList();
        }

        public int UpdateUsePeople(string people, List<string> assetsNums)
        {
            var sql = "UPDATE ASSETS_MAIN SET USE_PEOPLE=@0 WHERE ASSETSNUM in(@1)";
            return Db.Execute(sql, people, assetsNums);
        }

        public int UpdateScrapState(int isScrap, List<string> assetsNums)
        {
            var sql = "UPDATE ASSETS_MAIN SET IS_SCRAP=@0 WHERE ASSETSNUM in(@1)";
            return Db.Execute(sql, isScrap, assetsNums);
        }

        public int UpdateIsBorrow(int isBorrow, IEnumerable<string> assetsNums)
        {
            var sql = "UPDATE ASSETS_MAIN SET IS_BORROW=@0 WHERE ASSETSNUM in(@1)";
            return Db.Execute(sql, isBorrow, assetsNums);
        }

        public int UpdateIsUse(int isUse, IEnumerable<string> assetsNums)
        {
            var sql = "UPDATE ASSETS_MAIN SET IS_USE=@0 WHERE ASSETSNUM in(@1)";
            return Db.Execute(sql, isUse, assetsNums);
        }

        public int UpdateIsPrinted(int isPrinted, string assetsNums)
        {
            var sql = "UPDATE ASSETS_MAIN SET IS_PRINTED=@0 WHERE ASSETSNUM in(@1)";
            return Db.Execute(sql, 1, assetsNums);
        }

        public int UpdatePicPath(string assetsNum, string assetsPicPath)
        {
            var sql = "UPDATE ASSETS_MAIN SET ASSETS_PIC_PATH = @0 WHERE ASSETSNUM = @1";
            return Db.Execute(sql, assetsPicPath, assetsNum);
        }
    }

}