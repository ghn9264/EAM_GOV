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

        #region �ʲ�����
        /// <summary>
        /// �ʲ�����
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
            //�����ʲ�ʹ����
            _assetsMainRep.UpdateUsePeople(item.BorrowPerson, item.AssetsNums);
        }

        /// <summary>
        /// ��ѯ��������
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
        /// ��ѯ��������
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
        /// ɾ���ʲ����õ���¼
        /// </summary>
        /// <param name="EntyId"></param>
        public void DeleteAssetsBorrow(int EntityId)
        {
            //
            // ��ɾ���������н��õ���ŵ��ڵ�ǰҪɾ����������Ŀ���ʲ���ϸ
            //
            List<BorrowDetailAttribute> borrowList = _borrowDetailRep.Query(x => x.BorrowFormNo == EntityId);
            foreach (var borrowItem in borrowList)
            {
                _borrowDetailRep.Remove(borrowItem.EntityId);
            }

            //
            // ɾ������
            //
            _borrowRep.Remove(EntityId);

            //
            // ע��û��ɾ���ʲ������������������Ϣ
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }
        /// <summary>
        /// ɾ�������ʲ����õ���¼
        /// </summary>
        public void DeleteAllAssetsBorrow()
        {
            //
            // ��ɾ���������н��õ���ŵ��ڵ�ǰҪɾ����������Ŀ���ʲ���ϸ
            //
           
                _borrowDetailRep.RemoveAll();
            

            //
            // ɾ������
            //
            _borrowRep.RemoveAll();

            //
            // ע��û��ɾ���ʲ������������������Ϣ
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }

        #endregion

        #region �ʲ�����
        /// <summary>
        /// �ʲ�����
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
        /// ��ѯ��������
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
        /// ɾ���ʲ����õ���¼
        /// </summary>
        /// <param name="EntyId"></param>
        public void DeleteAssetsAquair(int EntyId)
        {
            //
            // ��ɾ�������������õ���ŵ��ڵ�ǰҪɾ����������Ŀ���ʲ���ϸ
            //
            List<AquairDetailAttribute> aquairList = _aquairDetailRep.Query(x => x.AcquireFormNo == EntyId);
            foreach (var aquairItem in aquairList)
            {
                _aquairDetailRep.Remove(aquairItem.EntityId);
            }

            //
            // ɾ������
            //
            _aquairRep.Remove(EntyId);
            
            //
            // ע��û��ɾ���ʲ������������������Ϣ
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }
        #endregion

        
        #region �ʲ��˻�

        /// <summary>
        /// �ʲ��˻�
        /// </summary>
        /// <param name="sendBackDto"></param>
        public void AssetsSendBack(SendBackDto sendBackDto)
        {
            var aquire = _aquairRep.Find(sendBackDto.AquireId);// _borrowRep.Find(returnDto.BorrowId);
            if (null == aquire)
                throw new Exception("δ�ҵ���Ӧ�����ü�¼");
            if (aquire.HasSendBack > 0)
                throw new Exception("�Ѿ��˻�");
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
            //���ʹ����
            _assetsMainRep.UpdateUsePeople("", sendBackDto.AssetsStatus.Select(x => x.Key).ToList());
        }

        #endregion

        #region �ʲ��黹
        /// <summary>
        /// ���½���黹��Ϣ
        /// </summary>
        /// <param name="returnDto"></param>
        public void AssetsReturn(ReturnDto returnDto)
        {
            var borrow = _borrowRep.Find(returnDto.BorrowId);
            if (null == borrow)
                throw new Exception("δ�ҵ���Ӧ�Ľ��ü�¼");
            if (borrow.HasReturn > 0)
                throw new Exception("�Ѿ��黹");
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
            //���ʹ����
            _assetsMainRep.UpdateUsePeople("", returnDto.AssetsStatus.Select(x => x.Key).ToList());
        } 
        #endregion

        #region �ʲ�����

        /// <summary>
        /// �ʲ�����
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
        /// ��ѯ��������
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
        /// ɾ���ʲ����޼�¼
        /// </summary>
        /// <param name="EntyId"></param>
        public void DeleteAssetsRepair(int EntityId)
        {
            //
            // ��ɾ���������н��õ���ŵ��ڵ�ǰҪɾ����������Ŀ���ʲ���ϸ
            //
            List<RepairDetailAttribute> repairDetailList = _repairDetailRep.Query(x => x.RepairFoemNo == EntityId);
            foreach (var repairItem in repairDetailList)
            {
                _repairDetailRep.Remove(repairItem.EntityId);
            }

            //
            // ɾ������
            //
            _repairRepository.Remove(EntityId);

            //
            // ע��û��ɾ���ʲ������������������Ϣ
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }
        public void DeleteAllAssetsRepair()
        {
            //
            // ��ɾ���������н��õ���ŵ��ڵ�ǰҪɾ����������Ŀ���ʲ���ϸ
            //
       
                _repairDetailRep.RemoveAll();
            

            //
            // ɾ������
            //
            _repairRepository.RemoveAll();

            //
            // ע��û��ɾ���ʲ������������������Ϣ
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }
        #endregion

        /// <summary>
        /// ���±�����Ϣ
        /// </summary>
        /// <param name="scrapDto"></param>
        public void AssetsScrap(ScrapDto scrapDto)
        {
            var scrap = _scrapApllyRep.Find(scrapDto.ScrapId);
            if (null == scrap)
                throw new Exception("δ�ҵ���Ӧ�ı��������¼");
            if (scrap.HasScrap > 0)
                throw new Exception("�Ѿ�����");

            //
            // ���±��������¼��
            // ����¼�޸�Ϊ�ѱ���
            //
            scrap.ScrapExaminePerson = scrapDto.ScrapExaminePerson;
            scrap.ScrapExamineDate = scrapDto.ScrapExamineDate;
            scrap.ScrapMome = scrapDto.ScrapMome;
            scrap.HasScrap = 1;
            _scrapApllyRep.Update(scrap);

            //
            // ���±��ϼ�¼��ϸ��
            // �޸�ÿ�������ʲ��ı���״̬
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
            // �����ʲ�����assets_main��ʹ����
            // �����ϵ��ʲ�ʹ�������
            // �������ʲ��ı���״̬��λ
            //
            _assetsMainRep.UpdateUsePeople("", scrapDto.ScrapStatus.Select(x => x.Key).ToList());
            _assetsMainRep.UpdateScrapState(1, scrapDto.ScrapStatus.Select(x => x.Key).ToList());

        }
        
        /// <summary>
        /// ���±���ά����Ϣ
        /// </summary>
        /// <param name="servicesDto"></param>
        public void AssetsServices(ServicesDto servicesDto)
        {
            //
            // �ڱ�����Ϣ�����ҳ���Ӧ�ı��޼�¼
            //
            var repair = _repairRepository.Find(servicesDto.RepairId);
            if (null == repair)
                throw new Exception("δ�ҵ���Ӧ�ı��޼�¼");
            if (repair.HasServices > 0)
                throw new Exception("�Ѿ�ά��");

            //
            // ���±��ޱ��ж�Ӧ�ļ�¼
            //
            repair.ServicesPerson = servicesDto.ServicePerson;
            repair.ServicesDate = servicesDto.ServiceDate;
            repair.ServicesPersonPhone = servicesDto.ServicePersonPhone;
            repair.ServicesMemo = servicesDto.ServicesMemo;
            repair.HasServices = 1;
            _repairRepository.Update(repair);

            //
            // ���ʲ����ޱ��������ҵ���Ӧ���ʲ���¼
            //
            foreach (var statu in servicesDto.ServicesResult)
            {
                var detail =
                    _repairDetailRep.FirstOrDefault(d => d.RepairFoemNo == repair.EntityId && d.AssetsNo == statu.Key);
                
                //
                // �����ʲ����������¼��
                //
                if (null != detail)
                {
                    detail.ServicesResult = statu.Value;// �ʲ�ά�޽��
                    _repairDetailRep.Update(detail);
                }
            }
            _assetsMainRep.UpdateUsePeople("", servicesDto.ServicesResult.Select(x=>x.Key).ToList());
        }

        /// <summary>
        /// �����ʲ��̵���Ϣ
        /// </summary>
        /// <param name="inventoryOperatorDto"></param>
        public void AssetsInventory(InventoryOperatorDto inventoryOperatorDto)
        {
            //
            // �ڱ�����Ϣ�����ҳ���Ӧ�ı��޼�¼
            //
            var inventory = _inventoryRep.Find(inventoryOperatorDto.InventoryId);
            if (null == inventory)
                throw new Exception("δ�ҵ���Ӧ���̵�ƻ�");
            if (inventory.HasInventory > 0)
                throw new Exception("�Ѿ��̵�");

            //
            // ���±��ޱ��ж�Ӧ�ļ�¼
            //
            inventory.InventoryOperationPerson = inventoryOperatorDto.InventoryOperatePerson;
            inventory.InventoryOperationDate = inventoryOperatorDto.InventoryOperateDate;
            inventory.InventoryDepartment = inventoryOperatorDto.InventoryOperateDepartment;
            inventory.HasInventory = 1;
            _inventoryRep.Update(inventory);

            //
            // ���ʲ��̵���������ҵ���Ӧ���ʲ���¼
            //
            foreach (var result in inventoryOperatorDto.AssetsInventoryResult)
            {
                var detail =
                    _inventoryDetailRep.FirstOrDefault(d => d.InventoryFormNo == inventory.EntityId && d.UsedNum1 == result.Key);

                //
                // �����ʲ��̵������¼��
                //
                if (null != detail)
                {
                    detail.InventoryResult = result.Value;        // �ʲ��̵���
                    _inventoryDetailRep.Update(detail);
                }
            }
        }
        
        /// <summary>
        /// �ʲ��̵�ƻ�
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
        /// ɾ���ʲ��̵�ƻ���¼
        /// </summary>
        /// <param name="EntyId"></param>
        public void DeleteAssetsInventoryPlan(int EntityId)
        {
            //
            // ��ɾ���������н��õ���ŵ��ڵ�ǰҪɾ����������Ŀ���ʲ���ϸ
            //
            List<InventoryDetailAttribute> inventoryDetailList = _inventoryDetailRep.Query(x => x.InventoryFormNo == EntityId);
            foreach (var inventoryItem in inventoryDetailList)
            {
                _inventoryDetailRep.Remove(inventoryItem.EntityId);
            }

            //
            // ɾ������
            //
            _inventoryRep.Remove(EntityId);

            //
            // ע��û��ɾ���ʲ������������������Ϣ
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }
        public void DeleteAllAssetsInventoryPlan()
        {
            //
            // ��ɾ���������н��õ���ŵ��ڵ�ǰҪɾ����������Ŀ���ʲ���ϸ
            //
       
                _inventoryDetailRep.RemoveAll();
         

            //
            // ɾ������
            //
            _inventoryRep.RemoveAll();

            //
            // ע��û��ɾ���ʲ������������������Ϣ
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }


        public void AddAssetsScrapApply(AssetsScrapApply item)
        {
            //
            // д������������
            //
            _scrapApllyRep.Add(item);

            //
            // ������������
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
        /// ��ѯ��������
        /// ����������Ǹ����ʲ���¼����������ֵ����ѯ�ʲ������Ҫ��Ϣ���ʲ��������������ʲ���
        /// �漰�����ű�assets_borrow�ʲ���Ҫ��Ϣ���assets_borrow_detail�ʲ�����
        /// </summary>
        /// <param name="inventoryId">����������ʲ������¼����������</param>
        /// <returns></returns>
        public InventoryDto QueryInventory(int inventoryId) // BorrowDto���Զ����һ���࣬����������ǰ��Ҫ��ʾ���������ݣ�ǰ��Ҫ��ȡ��Ҫ��Ϣ���ʲ��������������ʲ�����Դ�BorrowDto
        {
            var result = new InventoryDto();  // ����һ������������������ǰ̨Ҫ��������Ϣ
            result.InventoryInfo = _inventoryRep.Find(inventoryId);  // ǰ̨���ʲ���Ҫ��Ϣ����ֱ�ӵ��òִ�_borrowRep�е�Find��ʵ�֣����������ֱ������д
            if (null != result.InventoryInfo)
                result.Details = _inventoryDetailRep.QueryDto(inventoryId);  // ������������ҳ��ʲ�����������ʲ����Ҳ��������д
            return result;
        }

        

        public ScrapApplyDto QueryScrap(int scrapId) // BorrowDto���Զ����һ���࣬����������ǰ��Ҫ��ʾ���������ݣ�ǰ��Ҫ��ȡ��Ҫ��Ϣ���ʲ��������������ʲ�����Դ�BorrowDto
        {
            var result = new ScrapApplyDto();  // ����һ������������������ǰ̨Ҫ��������Ϣ
            result.ScrapInfo = _scrapApllyRep.Find(scrapId);  // ǰ̨���ʲ���Ҫ��Ϣ����ֱ�ӵ��òִ�_borrowRep�е�Find��ʵ�֣����������ֱ������д
            if (null != result.ScrapInfo)
            {
                result.Details = _scrapApplyDetailRep.Query(x => x.ScrapFormNo == scrapId).ToList();
                // ������������ҳ��ʲ�����������ʲ����Ҳ��������д
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
        /// ɾ���ʲ����������¼
        /// </summary>
        /// <param name="EntyId"></param>
        public void DeleteAssetsScrapApply(int EntityId)
        {
            //
            // ��ɾ���������н��õ���ŵ��ڵ�ǰҪɾ����������Ŀ���ʲ���ϸ
            //
            List<AssetsScrapApplyDetail> scrapApplyDetailsList = _scrapApplyDetailRep.Query(x => x.ScrapFormNo == EntityId);
            foreach (var scrapItem in scrapApplyDetailsList)
            {
                _scrapApplyDetailRep.Remove(scrapItem.EntityId);
            }

            //
            // ɾ������
            //
            _scrapApllyRep.Remove(EntityId);

            //
            // ע��û��ɾ���ʲ������������������Ϣ
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }
        /// <summary>
        /// ɾ�������ʲ����������¼
        /// </summary>
        public void DeleteAllAssetsScrapApply()
        {
            //
            // ��ɾ���������н��õ���ŵ��ڵ�ǰҪɾ����������Ŀ���ʲ���ϸ
            //
        
           
                _scrapApplyDetailRep.RemoveAll();
            

            //
            // ɾ������
            //
            _scrapApllyRep.RemoveAll();

            //
            // ע��û��ɾ���ʲ������������������Ϣ
            //_assetsMainRep.UpdateUsePeople(item.AcquirePerson, item.AssetsNums);
        }
        
        /// <summary>
        /// ��ѯ̨������
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<LedgerItem> LedgerData(LedgerQuery query)
        {
            List<LedgerItem> items = _assetsMainRep.LedgerData(query.QuerySql);
            Console.WriteLine(items);
            if (items == null || items.Count == 0)
                return new List<LedgerItem>();
            /*������Ҫ�������/�ϼƵ�λ��*/
            Dictionary<int, LedgerItem> insertItems = new Dictionary<int, LedgerItem>();
            int year = items[0].Year;
            /*��ʱ�����������*/
            var tempList = new List<LedgerItem>();
            for (int index = 0; index < items.Count; index++)
            {
                if (year != items[index].Year /*|| index == items.Count - 1*/)/*������µ����,��¼����ϼƵ�λ�ò����������ͽ��*/
                {
                    var iItem = new LedgerItem
                    {
                        GoodsName = "�������",
                        InCount = tempList.Sum(x => x.InCount),
                        InMoney = tempList.Sum(x => x.InMoney),
                        Count = tempList.Sum(x => x.Count),
                        Money = tempList.Sum(x => x.Money)
                    };
                    insertItems.Add(index, iItem);
                    tempList.Clear();/*�����һ�������*/
                    year = items[index].Year;
                }
                /*���浱ǰ�������*/
                tempList.Add(items[index]);
            }
            var last = new LedgerItem
            {
                GoodsName = "�������",
                InCount = tempList.Sum(x => x.InCount),
                InMoney = tempList.Sum(x => x.InMoney),
                Count = tempList.Sum(x => x.Count),
                Money = tempList.Sum(x => x.Money)
            };
            insertItems.Add(items.Count, last);

            /*���������/�ϼƵ�λ�õ���*/
            var keys = insertItems.Keys.ToArray();
            Array.Reverse(keys);
            for (int i = 0; i < keys.Length; i++)
            {
                var key = keys[i];
                /*���ɽ���/�ϼ����ݲ�������*/
                var row = insertItems[key];
                var row2 = new LedgerItem { GoodsName = "����ϼ�", InCount = row.InCount, InMoney = row.InMoney };
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