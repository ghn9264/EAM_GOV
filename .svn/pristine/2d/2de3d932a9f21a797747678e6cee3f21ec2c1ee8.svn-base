using System.Collections.Generic;
using EAM.Data.Domain;
using EAM.Data.Services.Dto;
using EAM.Data.Services.Query;

namespace EAM.Data.Services
{
    public interface IAssetsOptionService
    {
        #region 借用
        /// <summary>
        /// 资产借用
        /// </summary>
        /// <param name="item"></param>
        void AddAssetsBorrow(BorrowAttribute item);
        /// <summary>
        /// 查询借用详情
        /// </summary>
        /// <param name="borrowId"></param>
        /// <returns></returns>
        BorrowDto QueryBorrow(int borrowId);

        /// <summary>
        /// 查询领用详情
        /// </summary>
        /// <param name="borrowId"></param>
        /// <returns></returns>
        AcquireDto QueryAquire(int borrowId);

        /// <summary>
        /// 删除资产借用单记录
        /// </summary>
        /// <param name="EntyId"></param>
        void DeleteAssetsBorrow(int EntyId);
        /// <summary>
        /// 删除所有资产借用单记录
        /// </summary>
        void DeleteAllAssetsBorrow();
        #endregion

        #region 领用
        /// <summary>
        /// 资产领用
        /// </summary>
        /// <param name="item"></param>
        void AddAssetsAquair(AquairAttribute item);
        /// <summary>
        /// 查询领用详情
        /// </summary>
        /// <param name="acquireId"></param>
        /// <returns></returns>
        AcquireDto QueryAcquire(int acquireId);


        /// <summary>
        /// 删除资产领用单记录
        /// </summary>
        /// <param name="EntyId"></param>
        void DeleteAssetsAquair(int EntyId);
        #endregion

        #region 归还
        /// <summary>
        /// 资产归还
        /// </summary>
        /// <param name="returnDto"></param>
        void AssetsReturn(ReturnDto returnDto);

        /// <summary>
        /// 资产退还
        /// </summary>
        /// <param name="sendBackDto"></param>
        void AssetsSendBack(SendBackDto sendBackDto);

        #endregion

        #region 报修
        void AddAssetsRepair(RepairAttribute item); 
        RepairDto QueryRepair(int repairId);

        void DeleteAssetsRepair(int EntityId);
        void DeleteAllAssetsRepair();
        #endregion

        InventoryDto QueryInventory(int inventoryId); 
        ScrapApplyDto QueryScrap(int scrapId);
       
        void AddAssetsInventoryPlan(InventoryAttribute item);

        void DeleteAssetsInventoryPlan(int EntityId);
        void DeleteAllAssetsInventoryPlan();
        void AddAssetsScrapApply(AssetsScrapApply item);

        void DeleteAssetsScrapApply(int EntityId);
        void DeleteAllAssetsScrapApply();
        void AssetsServices(ServicesDto servicesDto);

        void AssetsInventory(InventoryOperatorDto inventoryOperatorDto);
        void AssetsScrap(ScrapDto scrapDto);

        /// <summary>
        /// 查询台账数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<LedgerItem> LedgerData(LedgerQuery query);

    }
}