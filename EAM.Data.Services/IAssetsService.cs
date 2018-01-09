using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Services.Dto;
using EAM.Data.Services.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EAM.Data.Services
{
    /// <summary>
    /// 提供所有资产新增,修改,删除,查询操作
    /// </summary>
    public interface IAssetsService
    {
        #region 新增

        /// <summary>
        /// 特种动植物
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        void SaveAssets(AssetsMain main, AnimalandplantAttribute attr);

        /// <summary>
        /// 特种动植物
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int SaveAssetsEx(AssetsMain main, AnimalandplantAttribute attr);

        /// <summary>
        /// 特种动植物
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int UpdateAssetsEx(AssetsMain main, AnimalandplantAttribute attr);

        /// <summary>
        /// 图书档案
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        void SaveAssets(AssetsMain main, BooksAttribute attr);

        /// <summary>
        /// 图书档案
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int SaveAssetsEx(AssetsMain main, BooksAttribute attr);

        /// <summary>
        /// 图书档案
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int UpdateAssetsEx(AssetsMain main, BooksAttribute attr);

        /// <summary>
        /// 文物与陈列品
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        void SaveAssets(AssetsMain main, CulturalrelicAttribute attr);

        /// <summary>
        /// 文物与陈列品
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int SaveAssetsEx(AssetsMain main, CulturalrelicAttribute attr);

        /// <summary>
        /// 文物与陈列品
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int UpdateAssetsEx(AssetsMain main, CulturalrelicAttribute attr);

        /// <summary>
        /// 构筑物
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        void SaveAssets(AssetsMain main, BuildingAttribute attr);

        /// <summary>
        /// 构筑物
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int SaveAssetsEx(AssetsMain main, BuildingAttribute attr);

        /// <summary>
        /// 构筑物
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int UpdateAssetsEx(AssetsMain main, BuildingAttribute attr);

        /// <summary>
        /// 车辆
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        void SaveAssets(AssetsMain main, CarAttribute attr);

        /// <summary>
        /// 车辆
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int SaveAssetsEx(AssetsMain main, CarAttribute attr);

        /// <summary>
        /// 车辆
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int UpdateAssetsEx(AssetsMain main, CarAttribute attr);

        /// <summary>
        /// 房屋
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        void SaveAssets(AssetsMain main, HouseAttribute attr);

        /// <summary>
        /// 房屋
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int SaveAssetsEx(AssetsMain main, HouseAttribute attr);

        /// <summary>
        /// 房屋
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int UpdateAssetsEx(AssetsMain main, HouseAttribute attr);

        /// <summary>
        /// 土地
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        void SaveAssets(AssetsMain main, LandAttribute attr);

        /// <summary>
        /// 土地
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int SaveAssetsEx(AssetsMain main, LandAttribute attr);

        /// <summary>
        /// 土地
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int UpdateAssetsEx(AssetsMain main, LandAttribute attr);

        /// <summary>
        /// 专用设备
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        void SaveAssets(AssetsMain main, SpecialAttribute attr);

        /// <summary>
        /// 专用设备
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int SaveAssetsEx(AssetsMain main, SpecialAttribute attr);

        /// <summary>
        /// 专用设备
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int UpdateAssetsEx(AssetsMain main, SpecialAttribute attr);

        /// <summary>
        /// 通用设备
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        void SaveAssets(AssetsMain main, GeneralAttribute attr);

        /// <summary>
        /// 通用设备
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int SaveAssetsEx(AssetsMain main, GeneralAttribute attr);

        /// <summary>
        /// 通用设备
        /// </summary>
        /// <param name="main"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        int UpdateAssetsEx(AssetsMain main, GeneralAttribute attr);

        /// <summary>
        /// 家具
        /// </summary>
        /// <param name="main"></param> 
        void SaveAssets(AssetsMain main);

        void UpdateAssets(AssetsMain main);

        void InsertAssets(AssetsMain main);

        void InsertAssetsEx(List<AssetsMain> main);

        /// <summary>
        /// 家具
        /// </summary>
        /// <param name="main"></param>
        /// <returns></returns>
        int SaveAssetsEx(AssetsMain main);

        /// <summary>
        /// 修改某个字段
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="item"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        int ChangeInfo(int entityId, string item, string content);

        string GetNextAssetsNum(string catCode);
        int UpdateNextAssetsNum(string catCode);

        #endregion

        #region 删除

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        int LogicDelete(int entityId);

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        int Delete(int entityId);
        /// <summary>
        /// 按记录删除
        /// </summary>
        /// <param name="assetsNums"></param>
        /// <returns></returns>
        int DeleteByimportId(int importid);
        #endregion

        #region 查询

        List<AssetsMain> Get(List<string> assetsNums);

        AssetsMain Get(string assetsNum);
        AssetsMain Get2(string assetsNum);

        AssetsMain GetByMemo(string memo);

        AssetsMain GetByDynamicNum(string usedNum1);

        AssetsMain GetBySysNum(string memo);

        AssetsMain Get(int entityId);
        List<AssetsMain> Get(List<int> entityIds);

        AssetsResultDto GetResultDto(int entityId);

        PagedList<AssetsMain> QueryPage(AssetsQuery query);
        PagedList<AssetsMain> QueryPage1(AssetsQuery query);
        PagedList<AssetsMain> QueryPage(AssetsScrapQuery query);

        List<AssetsMain> QueryAsstesList(AssetsQuery query);
        List<AssetsMain> Find(Expression<Func<AssetsMain, bool>> laumdawhere);

        /// <summary>
        /// 数据比对结果表查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        PagedList<DiffResult> QueryPage(AllRecordQuery query);

        List<AssetsMain> QueryAssets(List<string> catCodePrixs);

        HouseAttribute GetHouse(string accetsNum);

        LandAttribute GetLand(string assetsNum);

        BuildingAttribute GetBuilding(string assetsNum);

        CarAttribute GetCar(string assetsNum);

        GeneralAttribute GetGeneral(string assetsNum);

        SpecialAttribute GetSpecial(string assetsNum);

        CulturalrelicAttribute GetCulturalrelic(string assetsNum);
        BooksAttribute GetBook(string assetsNum);
        AnimalandplantAttribute GetAnimalandplant(string assetsNum);

        #endregion

        #region 导入

        /// <summary>
        /// 办学判断重复
        /// </summary>
        /// <param name="stockNumber"></param>
        /// <returns></returns>
        bool IsExistByStockNumber(string stockNumber);

        /// <summary>
        /// 办学拆分判重
        /// </summary>
        /// <param name="memo"></param>
        /// <returns></returns>
        bool IsExistByMemo(string memo);

        /// <summary>
        /// 动态导入系统编码判重
        /// </summary>
        /// <param name="memo"></param>
        /// <returns></returns>
        bool IsExistByDynamicMemo(string memo);

        void AddAssets(AssetsMain main);

        /// <summary>
        /// 动态判断重复
        /// </summary>
        /// <param name="assetsNum"></param>
        /// <returns></returns>
        bool IsExistByAssetsNum(string assetsNum);

        #endregion

        #region 更新

        /// <summary>
        /// 更新资产图片
        /// </summary>
        /// <param name="assetsNum"></param>
        /// <param name="assetsPicPath"></param>
        int UpdatePicPath(string assetsNum, string assetsPicPath);

        /// <summary>
        /// 是否可领用,1为可领用，0为不可领用
        /// </summary>
        /// <param name="isBorrow"></param>
        /// <param name="assetsNums"></param>
        /// <returns></returns>
        int UpdateIsBorrow(int isBorrow, IEnumerable<string> assetsNums);

        /// <summary>
        /// 是否可领用,1为可领用，0为不可领用
        /// </summary>
        /// <param name="isUse"></param>
        /// <param name="assetsNums"></param>
        /// <returns></returns>
        int UpdateIsUse(int isUse, IEnumerable<string> assetsNums);

        #endregion


        #region 比对结果写入
        void SaveDiffResult(DiffResult result);

        void ClearDiffResult();

        List<DiffResult> QueryAllDiffResult();
        #endregion

        AseetsGeneral GetAssetsGeneral();

        int Printed(string assetsNum);

        List<ClassCode> GetClassCodeByParentCode(string parentClassCode);
        List<AssetsMain> GetEchars();

        /// <summary>
        /// 获取目标部门和其子部门
        /// </summary>
        /// <param name="deptName"></param>
        /// <returns></returns>
        List<Department> GetDepartment(string deptName);

        /// <summary>
        /// 获取目标存放地点和其子地点
        /// </summary>
        /// <param name="placeName"></param>
        /// <returns></returns>
        List<Place> GetPlace(string placeName);

        /// <summary>
        /// 从资产库中查询资产并存储到缓存库中
        /// </summary>
        /// <param name="assetsList"></param>
        void WriteToCach(List<LedGerCach> assetsList);

        /// <summary>
        /// 将资产查询结果存放到缓存表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        int QueryAsstesToCach(LedGerCachQuery query);

        /// <summary>
        /// 资产清空服务
        /// </summary>
        void ClearAllData();

    }
}