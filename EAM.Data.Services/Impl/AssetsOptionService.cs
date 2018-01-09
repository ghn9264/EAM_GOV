using System;
using System.Collections.Generic;
using System.Linq;
using EAM.Data.Domain;
using EAM.Data.Repositories;
using EAM.Data.Services.Dto;
using EAM.Data.Services.Query;

namespace EAM.Data.Services.Impl
{
    public class AssetsOptionService : IAssetsOptionService
    {
        private readonly IAquairRepository _aquairRep;
        private readonly IAquairDetailRepository _aquairDetailRep;
        private readonly IAssetsService _assetsService;
        private readonly IBorrowRepository _borrowRep;
        private readonly IBorrowDetailRepository _borrowDetailRep;
        private readonly IRepairRepository _repairRepository;
        private readonly IRepairDetailRepository _repairDetailRep;
        private readonly IInventoryRepository _inventoryRep;
        private readonly IInventoryDetailRepository _inventoryDetailRep;
        private readonly IScrapApllyRepository _scrapApllyRep;
        private readonly IScrapApplyDetailRepository _scrapApplyDetailRep;
        private readonly IAssetsMainRepository _assetsMainRepository;
        private readonly IAssetsMainRepository _assetsMainRep;

        #region CTOR
        public AssetsOptionService(IAquairRepository aquairRep,
            IAquairDetailRepository aquairDetailRep,
            IAssetsService assetsService,
            IBorrowRepository borrowRep,
            IBorrowDetailRepository borrowDetailRep,
            IRepairDetailRepository repairDetailRep,
            IRepairRepository repairRepository,
            IInventoryRepository inventoryRep,
            IInventoryDetailRepository inventoryDetailRep,
            IScrapApllyRepository scrapApllyRep,
            IScrapApplyDetailRepository scrapApplyDetailRep, IAssetsMainRepository assetsMainRepository,
            IAssetsMainRepository assetsMainRep)
        {
            _aquairRep = aquairRep;
            _aquairDetailRep = aquairDetailRep;
            _assetsService = assetsService;
            _borrowRep = borrowRep;
            _borrowDetailRep = borrowDetailRep;
            _repairDetailRep = repairDetailRep;
            _repairRepository = repairRepository;
            _inventoryRep = inventoryRep;
            _inventoryDetailRep = inventoryDetailRep;
            _scrapApllyRep = scrapApllyRep;
            _scrapApplyDetailRep = scrapApplyDetailRep;
            _assetsMainRepository = assetsMainRepository;
            _assetsMainRep = assetsMainRep;
        } 
        #endregion

        #region 资产借用
        /// <summary>
        /// 资产借用
        /// </summary>
        /// <param name="item"></param>
        public void AddAssetsBorrow(BorrowAttribute item)
        {
            _borrowRep.Add(item);
            var details = new List<BorrowDetailAttribute>();
            var assets = _assetsService.Get(item.AssetsNums);
            foreach (var asset in assets)
            {
                details.Add(new BorrowDetailAttribute
                {
                    BorrowFormNo = item.EntityId,
                    AssetsNo = asset.AssetsNum,
                    BorrowCounts = asset.Counts
                });
            }
            _borrowDetailRep.Add(details);
            //设置资产使用人
            _assetsMainRep.UpdateUsePeople(item.BorrowPerson, item.AssetsNums);
        }

        /// <summary>
        /// 查询借用详情
        /// </summary>
        /// <param name="borrowId"></param>
        /// <returns></returns>
        public BorrowDto QueryBorrow(int borrowId)
        {
            var result = new BorrowDto();
            result.BorrowInfo = _borrowRep.Find(borrowId);
            if (null != result.BorrowInfo)
                result.Details = _borrowDetailRep.QueryDto(borrowId);
            return result;
        }

        /// <summary>
        /// 查询领用详情
        /// </summary>
        /// <param name="borrowId"></param>
        /// <returns></returns>
        public AcquireDto QueryAquire(int borrowId)
        {
            var result = new AcquireDto();
            result.AquairInfo = _aquairRep.Find(borrowId);// _borrowRep.Find(borrowId);
            if (null != result.AquairInfo)
                result.Details = _aquairDetailRep.QueryDto(borrowId);// _borrowDetailRep.QueryDto(borrowId);
            return result;
        }

        /// <summary>
        /// 删除资产借用单记录
        /// </summary>
        /// <param name="EntyId"></param>
        public void DeleteAssetsBorrow(int EntityId)
        {
            //
            // 先删除附表所有借用单编号等于当前要删除的主表条目的资产明细
            //
            List<BorrowDetailAttribute> borrowList = _borrowDetailRep.Query(x => x.BorrowFormNo == EntityId);
            foreach (var borrowItem in borrowList)
            {
                _borrowDetailRep.Remove(borrowItem.EntityId);
            }

            //
            // 删除主表
            //
            _borrowRep.Remove(EntityId);

            //
            // 注意没有删除资产主表里面的领用人信息
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }
        /// <summary>
        /// 删除所有资产借用单记录
        /// </summary>
        public void DeleteAllAssetsBorrow()
        {
            //
            // 先删除附表所有借用单编号等于当前要删除的主表条目的资产明细
            //
           
                _borrowDetailRep.RemoveAll();
            

            //
            // 删除主表
            //
            _borrowRep.RemoveAll();

            //
            // 注意没有删除资产主表里面的领用人信息
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }

        #endregion

        #region 资产领用
        /// <summary>
        /// 资产领用
        /// </summary>
        /// <param name="item"></param>
        public void AddAssetsAquair(AquairAttribute item)
        {
            _aquairRep.Add(item);
            var details = new List<AquairDetailAttribute>();
            var assets = _assetsService.Get(item.AssetsNums);
            foreach (var asset in assets)
            {
                details.Add(new AquairDetailAttribute
                {
                    AcquireFormNo = item.EntityId,
                    AssetsNo = asset.AssetsNum,
                    AquairCounts = asset.Counts
                });
            }
            _aquairDetailRep.Add(details);
            _assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }

        /// <summary>
        /// 查询领用详情
        /// </summary>
        /// <param name="acquireId"></param>
        /// <returns></returns>
        public AcquireDto QueryAcquire(int acquireId)
        {
            var result = new AcquireDto();
            result.AquairInfo = _aquairRep.Find(acquireId);
            if (null != result.AquairInfo)
                result.Details = _aquairDetailRep.QueryDto(acquireId);
            return result;
        }

        /// <summary>
        /// 删除资产领用单记录
        /// </summary>
        /// <param name="EntyId"></param>
        public void DeleteAssetsAquair(int EntyId)
        {
            //
            // 先删除附表所有领用单编号等于当前要删除的主表条目的资产明细
            //
            List<AquairDetailAttribute> aquairList = _aquairDetailRep.Query(x => x.AcquireFormNo == EntyId);
            foreach (var aquairItem in aquairList)
            {
                _aquairDetailRep.Remove(aquairItem.EntityId);
            }

            //
            // 删除主表
            //
            _aquairRep.Remove(EntyId);
            
            //
            // 注意没有删除资产主表里面的领用人信息
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }
        #endregion

        
        #region 资产退还

        /// <summary>
        /// 资产退还
        /// </summary>
        /// <param name="sendBackDto"></param>
        public void AssetsSendBack(SendBackDto sendBackDto)
        {
            var aquire = _aquairRep.Find(sendBackDto.AquireId);// _borrowRep.Find(returnDto.BorrowId);
            if (null == aquire)
                throw new Exception("未找到对应的领用记录");
            if (aquire.HasSendBack > 0)
                throw new Exception("已经退还");
            aquire.SendBackPerson = sendBackDto.SendBackPerson;
            aquire.SendBackDate = sendBackDto.SendBackDate;
            aquire.SendBackMemo = sendBackDto.SendBackMemo;
            aquire.HasSendBack = 1;
            _aquairRep.Update(aquire);
            foreach (var statu in sendBackDto.AssetsStatus)
            {
                var detail =
                    _aquairDetailRep.FirstOrDefault(d => d.AcquireFormNo == aquire.EntityId && d.AssetsNo == statu.Key);
                    //_borrowDetailRep.FirstOrDefault(d => d.BorrowFormNo == aquire.EntityId && d.AssetsNo == statu.Key);
                if (null != detail)
                {
                    detail.SendBackState = statu.Value;
                    _aquairDetailRep.Update(detail);
                }
            }
            //清空使用人
            _assetsMainRep.UpdateUsePeople("", sendBackDto.AssetsStatus.Select(x => x.Key).ToList());
        }

        #endregion

        #region 资产归还
        /// <summary>
        /// 更新借出归还信息
        /// </summary>
        /// <param name="returnDto"></param>
        public void AssetsReturn(ReturnDto returnDto)
        {
            var borrow = _borrowRep.Find(returnDto.BorrowId);
            if (null == borrow)
                throw new Exception("未找到对应的借用记录");
            if (borrow.HasReturn > 0)
                throw new Exception("已经归还");
            borrow.ReturnPerson = returnDto.ReturnPerson;
            borrow.ReturnDate = returnDto.ReturnDate;
            borrow.ReturnMome = returnDto.ReturnMome;
            borrow.HasReturn = 1;
            _borrowRep.Update(borrow);
            foreach (var statu in returnDto.AssetsStatus)
            {
                var detail =
                    _borrowDetailRep.FirstOrDefault(d => d.BorrowFormNo == borrow.EntityId && d.AssetsNo == statu.Key);
                if (null != detail)
                {
                    detail.ReturnStatus = statu.Value;
                    _borrowDetailRep.Update(detail);
                }
            }
            //清空使用人
            _assetsMainRep.UpdateUsePeople("", returnDto.AssetsStatus.Select(x => x.Key).ToList());
        } 
        #endregion

        #region 资产报修

        /// <summary>
        /// 资产报修
        /// </summary>
        /// <param name="item"></param>
        public void AddAssetsRepair(RepairAttribute item)
        {
            _repairRepository.Add(item);
            var details = new List<RepairDetailAttribute>();
            var assets = _assetsService.Get(item.AssetsNums);
            foreach (var asset in assets)
            {
                details.Add(new RepairDetailAttribute
                {
                    RepairFoemNo = item.EntityId,
                    AssetsNo = asset.AssetsNum,
                    ErrorDescript = ""
                });
            }
            _repairDetailRep.Add(details);
            _assetsMainRep.UpdateUsePeople(item.RepairPerson, item.AssetsNums);
        }

        /// <summary>
        /// 查询报修详情
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        public RepairDto QueryRepair(int repairId)
        {
            var result = new RepairDto();
            result.RepairInfo = _repairRepository.Find(repairId);
            if (null != result.RepairInfo)
                result.Details = _repairDetailRep.QueryDto(repairId);
            return result;
        }

        /// <summary>
        /// 删除资产报修记录
        /// </summary>
        /// <param name="EntyId"></param>
        public void DeleteAssetsRepair(int EntityId)
        {
            //
            // 先删除附表所有借用单编号等于当前要删除的主表条目的资产明细
            //
            List<RepairDetailAttribute> repairDetailList = _repairDetailRep.Query(x => x.RepairFoemNo == EntityId);
            foreach (var repairItem in repairDetailList)
            {
                _repairDetailRep.Remove(repairItem.EntityId);
            }

            //
            // 删除主表
            //
            _repairRepository.Remove(EntityId);

            //
            // 注意没有删除资产主表里面的领用人信息
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }
        public void DeleteAllAssetsRepair()
        {
            //
            // 先删除附表所有借用单编号等于当前要删除的主表条目的资产明细
            //
       
                _repairDetailRep.RemoveAll();
            

            //
            // 删除主表
            //
            _repairRepository.RemoveAll();

            //
            // 注意没有删除资产主表里面的领用人信息
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }
        #endregion

        /// <summary>
        /// 更新报废信息
        /// </summary>
        /// <param name="scrapDto"></param>
        public void AssetsScrap(ScrapDto scrapDto)
        {
            var scrap = _scrapApllyRep.Find(scrapDto.ScrapId);
            if (null == scrap)
                throw new Exception("未找到对应的报废申请记录");
            if (scrap.HasScrap > 0)
                throw new Exception("已经报废");

            //
            // 更新报废申请记录表
            // 将记录修改为已报废
            //
            scrap.ScrapExaminePerson = scrapDto.ScrapExaminePerson;
            scrap.ScrapExamineDate = scrapDto.ScrapExamineDate;
            scrap.ScrapMome = scrapDto.ScrapMome;
            scrap.HasScrap = 1;
            _scrapApllyRep.Update(scrap);

            //
            // 更新报废记录明细表
            // 修改每条报废资产的报废状态
            //
            foreach (var statu in scrapDto.ScrapStatus)
            {
                var detail =
                    _scrapApplyDetailRep.FirstOrDefault(d => d.ScrapFormNo == scrap.EntityId && d.AssetsNo == statu.Key);
                if (null != detail)
                {
                    detail.ScrapStatus = statu.Value;
                    _scrapApplyDetailRep.Update(detail);
                }
            }

            //
            // 更新资产主表assets_main中使用人
            // 将报废的资产使用人清除
            // 将报废资产的报废状态置位
            //
            _assetsMainRep.UpdateUsePeople("", scrapDto.ScrapStatus.Select(x => x.Key).ToList());
            _assetsMainRep.UpdateScrapState(1, scrapDto.ScrapStatus.Select(x => x.Key).ToList());

        }
        
        /// <summary>
        /// 更新报修维修信息
        /// </summary>
        /// <param name="servicesDto"></param>
        public void AssetsServices(ServicesDto servicesDto)
        {
            //
            // 在报修信息表中找出相应的报修记录
            //
            var repair = _repairRepository.Find(servicesDto.RepairId);
            if (null == repair)
                throw new Exception("未找到对应的报修记录");
            if (repair.HasServices > 0)
                throw new Exception("已经维修");

            //
            // 更新报修表当中对应的记录
            //
            repair.ServicesPerson = servicesDto.ServicePerson;
            repair.ServicesDate = servicesDto.ServiceDate;
            repair.ServicesPersonPhone = servicesDto.ServicePersonPhone;
            repair.ServicesMemo = servicesDto.ServicesMemo;
            repair.HasServices = 1;
            _repairRepository.Update(repair);

            //
            // 到资产报修表详情中找到相应的资产记录
            //
            foreach (var statu in servicesDto.ServicesResult)
            {
                var detail =
                    _repairDetailRep.FirstOrDefault(d => d.RepairFoemNo == repair.EntityId && d.AssetsNo == statu.Key);
                
                //
                // 更新资产报修详情记录表
                //
                if (null != detail)
                {
                    detail.ServicesResult = statu.Value;// 资产维修结果
                    _repairDetailRep.Update(detail);
                }
            }
            _assetsMainRep.UpdateUsePeople("", servicesDto.ServicesResult.Select(x=>x.Key).ToList());
        }

        /// <summary>
        /// 更新资产盘点信息
        /// </summary>
        /// <param name="inventoryOperatorDto"></param>
        public void AssetsInventory(InventoryOperatorDto inventoryOperatorDto)
        {
            //
            // 在报修信息表中找出相应的报修记录
            //
            var inventory = _inventoryRep.Find(inventoryOperatorDto.InventoryId);
            if (null == inventory)
                throw new Exception("未找到对应的盘点计划");
            if (inventory.HasInventory > 0)
                throw new Exception("已经盘点");

            //
            // 更新报修表当中对应的记录
            //
            inventory.InventoryOperationPerson = inventoryOperatorDto.InventoryOperatePerson;
            inventory.InventoryOperationDate = inventoryOperatorDto.InventoryOperateDate;
            inventory.InventoryDepartment = inventoryOperatorDto.InventoryOperateDepartment;
            inventory.HasInventory = 1;
            _inventoryRep.Update(inventory);

            //
            // 到资产盘点表详情中找到相应的资产记录
            //
            foreach (var result in inventoryOperatorDto.AssetsInventoryResult)
            {
                var detail =
                    _inventoryDetailRep.FirstOrDefault(d => d.InventoryFormNo == inventory.EntityId && d.UsedNum1 == result.Key);

                //
                // 更新资产盘点详情记录表
                //
                if (null != detail)
                {
                    detail.InventoryResult = result.Value;        // 资产盘点结果
                    _inventoryDetailRep.Update(detail);
                }
            }
        }
        
        /// <summary>
        /// 资产盘点计划
        /// </summary>
        /// <param name="item"></param>
        public void AddAssetsInventoryPlan(InventoryAttribute item)
        {
            _inventoryRep.Add(item);
            var details = new List<InventoryDetailAttribute>();
            var assets = _assetsService.Get(item.AssetsNums);
            foreach (var asset in assets)
            {
                details.Add(new InventoryDetailAttribute
                {
                    InventoryFormNo = item.EntityId,
                    AssetsNo = asset.AssetsNum,
                    UsedNum1 = asset.UsedNum1,
                    InventoryPerson = item.InventoryPerson,
                    InventoryDate = item.InventoryDate
                });
            }
            _inventoryDetailRep.Add(details);
        }

        /// <summary>
        /// 删除资产盘点计划记录
        /// </summary>
        /// <param name="EntyId"></param>
        public void DeleteAssetsInventoryPlan(int EntityId)
        {
            //
            // 先删除附表所有借用单编号等于当前要删除的主表条目的资产明细
            //
            List<InventoryDetailAttribute> inventoryDetailList = _inventoryDetailRep.Query(x => x.InventoryFormNo == EntityId);
            foreach (var inventoryItem in inventoryDetailList)
            {
                _inventoryDetailRep.Remove(inventoryItem.EntityId);
            }

            //
            // 删除主表
            //
            _inventoryRep.Remove(EntityId);

            //
            // 注意没有删除资产主表里面的领用人信息
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }
        public void DeleteAllAssetsInventoryPlan()
        {
            //
            // 先删除附表所有借用单编号等于当前要删除的主表条目的资产明细
            //
       
                _inventoryDetailRep.RemoveAll();
         

            //
            // 删除主表
            //
            _inventoryRep.RemoveAll();

            //
            // 注意没有删除资产主表里面的领用人信息
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }


        public void AddAssetsScrapApply(AssetsScrapApply item)
        {
            //
            // 写报废申请主表
            //
            _scrapApllyRep.Add(item);

            //
            // 创建报废详情
            //
            var details = new List<AssetsScrapApplyDetail>();
            var assets = _assetsService.Get(item.AssetsNums);
            foreach (var asset in assets)
            {
                details.Add(new AssetsScrapApplyDetail
                {
                    ScrapFormNo = item.EntityId,
                    AssetsNo = asset.AssetsNum,
                    ScrapReason = ""
                });
            }
            _scrapApplyDetailRep.Add(details);
            _assetsMainRep.UpdateUsePeople(item.ScrapPerson, item.AssetsNums);
        }

        /// <summary>
        /// 查询借用详情
        /// 这个函数就是根据资产记录表里面的序号值来查询资产借出概要信息和资产借出详情里面的资产项
        /// 涉及到两张表：assets_borrow资产概要信息表和assets_borrow_detail资产详情
        /// </summary>
        /// <param name="inventoryId">这个参数是资产借出记录表里面的序号</param>
        /// <returns></returns>
        public InventoryDto QueryInventory(int inventoryId) // BorrowDto是自定义的一个类，这个类包含了前端要显示的所有数据，前端要提取概要信息和资产借出详情里面的资产项，所以打开BorrowDto
        {
            var result = new InventoryDto();  // 创建一个对象，这个对象包含了前台要的所有信息
            result.InventoryInfo = _inventoryRep.Find(inventoryId);  // 前台的资产概要信息可以直接调用仓储_borrowRep中的Find来实现，这里你可以直接照着写
            if (null != result.InventoryInfo)
                result.Details = _inventoryDetailRep.QueryDto(inventoryId);  // 这个就是用来找出资产详情里面的资产项，你也可以照着写
            return result;
        }

        

        public ScrapApplyDto QueryScrap(int scrapId) // BorrowDto是自定义的一个类，这个类包含了前端要显示的所有数据，前端要提取概要信息和资产借出详情里面的资产项，所以打开BorrowDto
        {
            var result = new ScrapApplyDto();  // 创建一个对象，这个对象包含了前台要的所有信息
            result.ScrapInfo = _scrapApllyRep.Find(scrapId);  // 前台的资产概要信息可以直接调用仓储_borrowRep中的Find来实现，这里你可以直接照着写
            if (null != result.ScrapInfo)
            {
                result.Details = _scrapApplyDetailRep.Query(x => x.ScrapFormNo == scrapId).ToList();
                // 这个就是用来找出资产详情里面的资产项，你也可以照着写
                var tempAssetsMains = new List<AssetsMain>(result.Details.Count);
                foreach (AssetsScrapApplyDetail assets in result.Details)
                {
                    AssetsScrapApplyDetail _assets = assets;
                    if (_assets == null) break;
                    AssetsMain assetsMain = _assetsMainRepository.FirstOrDefault(d => d.AssetsNum == _assets.AssetsNo);
                    if (null != assetsMain)
                    {
                        tempAssetsMains.Add(assetsMain);
                    }
                }
                result.AssetsScrapAttribute = tempAssetsMains;
            }
            return result;
        }

        /// <summary>
        /// 删除资产报废申请记录
        /// </summary>
        /// <param name="EntyId"></param>
        public void DeleteAssetsScrapApply(int EntityId)
        {
            //
            // 先删除附表所有借用单编号等于当前要删除的主表条目的资产明细
            //
            List<AssetsScrapApplyDetail> scrapApplyDetailsList = _scrapApplyDetailRep.Query(x => x.ScrapFormNo == EntityId);
            foreach (var scrapItem in scrapApplyDetailsList)
            {
                _scrapApplyDetailRep.Remove(scrapItem.EntityId);
            }

            //
            // 删除主表
            //
            _scrapApllyRep.Remove(EntityId);

            //
            // 注意没有删除资产主表里面的领用人信息
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }
        /// <summary>
        /// 删除所有资产报废申请记录
        /// </summary>
        public void DeleteAllAssetsScrapApply()
        {
            //
            // 先删除附表所有借用单编号等于当前要删除的主表条目的资产明细
            //
        
           
                _scrapApplyDetailRep.RemoveAll();
            

            //
            // 删除主表
            //
            _scrapApllyRep.RemoveAll();

            //
            // 注意没有删除资产主表里面的领用人信息
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }
        
        /// <summary>
        /// 查询台账数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<LedgerItem> LedgerData(LedgerQuery query)
        {
            List<LedgerItem> items = _assetsMainRep.LedgerData(query.QuerySql);
            Console.WriteLine(items);
            if (items == null || items.Count == 0)
                return new List<LedgerItem>();
            /*保存需要插入结余/合计的位置*/
            Dictionary<int, LedgerItem> insertItems = new Dictionary<int, LedgerItem>();
            int year = items[0].Year;
            /*临时保存年份数据*/
            var tempList = new List<LedgerItem>();
            for (int index = 0; index < items.Count; index++)
            {
                if (year != items[index].Year /*|| index == items.Count - 1*/)/*如果是新的年份,记录结余合计的位置并计算数量和金额*/
                {
                    var iItem = new LedgerItem
                    {
                        GoodsName = "上年结余",
                        InCount = tempList.Sum(x => x.InCount),
                        InMoney = tempList.Sum(x => x.InMoney),
                        Count = tempList.Sum(x => x.Count),
                        Money = tempList.Sum(x => x.Money)
                    };
                    insertItems.Add(index, iItem);
                    tempList.Clear();/*清空上一年分数据*/
                    year = items[index].Year;
                }
                /*保存当前年份数据*/
                tempList.Add(items[index]);
            }
            var last = new LedgerItem
            {
                GoodsName = "上年结余",
                InCount = tempList.Sum(x => x.InCount),
                InMoney = tempList.Sum(x => x.InMoney),
                Count = tempList.Sum(x => x.Count),
                Money = tempList.Sum(x => x.Money)
            };
            insertItems.Add(items.Count, last);

            /*将插入结余/合计的位置倒序*/
            var keys = insertItems.Keys.ToArray();
            Array.Reverse(keys);
            for (int i = 0; i < keys.Length; i++)
            {
                var key = keys[i];
                /*生成结余/合计数据插入结果集*/
                var row = insertItems[key];
                var row2 = new LedgerItem { GoodsName = "本年合计", InCount = row.InCount, InMoney = row.InMoney };
                row.InCount = 0;
                row.InMoney = 0;
                if (i != 0)
                    items.Insert(key, row);
                items.Insert(key, row2);
            }
            return items;
        }
        
    }
}