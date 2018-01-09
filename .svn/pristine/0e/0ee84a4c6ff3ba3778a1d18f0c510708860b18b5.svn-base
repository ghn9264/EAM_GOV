using System.Collections.Generic;
using EAM.Data.Comm;
using EAM.Data.Domain;
using NPoco;

namespace EAM.Data.Repositories
{
    public interface IAssetsMainCombineRepository : IRepository<AssetsMainCombine, int>
    {
        int LogicDelete(int entityId);

        int LogicDelete(string assetsNums);

        List<AssetsMainCombine> LogicDeleteEx(int import_id);

        int ChangeInfo(int entityId, string item, string content);

        List<AssetsMainCombine> QueryAssets(List<string> catCodePrixs);

        List<LedgerItem> LedgerData(Sql sql);

        /// <summary>
        /// 批量更新资产使用人
        /// </summary>
        /// <param name="people"></param>
        /// <param name="assetsNums"></param>
        int UpdateUsePeople(string people, List<string> assetsNums);

        int UpdateScrapState(int isScrap, List<string> assetsNums);

        int UpdateIsBorrow(int isBorrow, IEnumerable<string> assetsNums);

        int UpdateIsUse(int isUse, IEnumerable<string> assetsNums);
        int UpdatePicPath(string assetsNum, string assetsPicPath);

        int UpdateIsPrinted(int isPrinted, string assetsNums);

    }

}