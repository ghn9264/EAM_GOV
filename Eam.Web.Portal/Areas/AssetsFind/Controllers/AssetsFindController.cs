﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http.Results;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Services;
using EAM.Data.Services.Query;
using System.IO;
using System.Web.Mvc;
using Eam.Web.Portal.Areas.AssetsFind.Models;
using Enumerable = System.Linq.Enumerable;

namespace Eam.Web.Portal.Areas.AssetsFind.Controllers
{
    /// <summary>
    /// 资产查询
    /// </summary>
    public class AssetsFindController : EamAdminController
    {
        private readonly IAssetsService _assetsService;
        private readonly IAssetsAcquireServices _assetsAcquireServices;
        private readonly IAssetsBorrrowServices _assetsBorrrowServices;
        private readonly IRepairService _repairService;
        private readonly IInventoryService _inventoryService;
        private readonly IScrapService _scrapService;
        private readonly IUserService _userService;
        private readonly IQrprintSettingService _qrprintSettingService;

        public AssetsFindController(IAssetsService assetsService,
            IAssetsAcquireServices assetsAcquireServices,
            IAssetsBorrrowServices assetsBorrrowServices,
            IRepairService repairService,
            IInventoryService inventoryService,
            IScrapService scrapService,
            IUserService userService,
            IQrprintSettingService qrprintSettingService)
        {
            _assetsService = assetsService;
            _assetsAcquireServices = assetsAcquireServices;
            _assetsBorrrowServices = assetsBorrrowServices;
            _repairService = repairService;
            _inventoryService = inventoryService;
            _scrapService = scrapService;
            _userService = userService;
            _qrprintSettingService = qrprintSettingService;
        }
        //正在进行中
        [HttpPost]
        public ActionResult AssetsQuery(AssetsQuery model)
        {
            List<ClassCode> classList = new List<ClassCode>();
            List<Department> deptList = new List<Department>();
            List<Place> placeList = new List<Place>();


            //
            // 获取类别及其子类别
            //
            if (!string.IsNullOrEmpty(model.IndexClass))
            {
                classList = _assetsService.GetClassCodeByParentCode(model.IndexClass);

                if (classList != null)
                {

                    if (model.ClassList == null)
                    {
                        model.ClassList = new List<string>();
                    }
                    else
                    {
                        model.ClassList.Clear();
                    }

                    if (classList.Count >= 1)
                    {
                        foreach (var classItem in classList)
                        {
                            model.ClassList.Add(classItem.EntityId);
                        }
                    }
                }

            }

            //
            // 获取部门及其子部门
            //
            if (!string.IsNullOrEmpty(model.IndexDept))
            {
                if (model.Role == "部门负责人")
                {
                    //
                    // 获取当前用户部门及其子部门列表
                    //
                    deptList = _assetsService.GetDepartment(model.UseDepartment);

                    //
                    // 取出部门名称
                    //
                    var tempDept = new List<string>();
                    if (deptList != null)
                    {
                        if (deptList.Count >= 1)
                        {
                            tempDept.AddRange(Enumerable.Select(deptList, deptItem => deptItem.DeptName));
                        }
                    }

                    //
                    // 判断用户在界面上选中的部门是否隶属于该用户
                    // 如果隶属于则重新查询选中部门及其子部门
                    //
                    if (tempDept.Contains(model.IndexDept))
                    {
                        //
                        // 清空历史数据
                        //
                        if (model.DeptList == null)
                        {
                            model.DeptList = new List<string>();
                        }
                        else
                        {
                            model.DeptList.Clear();
                        }

                        //
                        // 查询用户选中部门及其子部门
                        //
                        deptList = _assetsService.GetDepartment(model.IndexDept);

                        //
                        // 将部门添加到检索项
                        //
                        if (deptList != null)
                        {
                            if (deptList.Count >= 1)
                            {
                                foreach (var deptItem in deptList)
                                {
                                    model.DeptList.Add(deptItem.DeptName);
                                }
                            }
                        }
                    }
                    else
                    {
                        //
                        // 如果是选中的部门不属于用户的部门或者子部门则忽略用户在界面上所选中的部门
                        // 以用户所在部门及其子部门作为部门筛选条件
                        //
                        model.DeptList = tempDept;
                    }
                }
                else
                {
                    //
                    // 如果用户不是部门负责人，则根据用户选中的部门及其子部门作为部门筛选条件
                    // 清空历史数据
                    //
                    if (model.DeptList == null)
                    {
                        model.DeptList = new List<string>();
                    }
                    else
                    {
                        model.DeptList.Clear();
                    }
                    //model.DeptList.Add(model.IndexDept);

                    //
                    // 用户选中的部门及其子部门作为部门筛选条件
                    //
                    deptList = _assetsService.GetDepartment(model.IndexDept);

                    if (deptList != null)
                    {
                        if (deptList.Count >= 1)
                        {
                            foreach (var deptItem in deptList)
                            {
                                model.DeptList.Add(deptItem.DeptName);// 添加到部门检索项
                            }
                        }
                    }
                }
            }
            else
            {
                //
                // 如果用户在界面上没有选择部门
                // 那就判断用户是否是部门负责人
                // 如果是部门负责人就根据用户所在部门进行筛选
                //
                if (model.Role == "部门负责人")
                {
                    model.IndexDept = model.UseDepartment;
                    //
                    // 清空历史数据
                    //
                    if (model.DeptList == null)
                    {
                        model.DeptList = new List<string>();
                    }
                    else
                    {
                        model.DeptList.Clear();
                    }
                    //model.DeptList.Add(model.IndexDept);

                    //
                    // 添加目标检索项子项
                    //
                    deptList = _assetsService.GetDepartment(model.IndexDept);

                    if (deptList != null)
                    {
                        if (deptList.Count >= 1)
                        {
                            foreach (var deptItem in deptList)
                            {
                                model.DeptList.Add(deptItem.DeptName);
                            }
                        }
                    }
                }
            }
            //
            // 获取地点及其子地点
            //
            if (!string.IsNullOrEmpty(model.IndexPlace))
            {
                //
                // 清空之前的数据
                //
                if (model.PlaceList == null)
                {
                    model.PlaceList = new List<string>();
                }
                else
                {
                    model.PlaceList.Clear();
                }

                //
                // 添加目标检索项
                //
                model.PlaceList.Add(model.IndexPlace);

                //
                // 添加目标检索项目的子项
                //
                //placeList = _assetsService.GetPlace(model.IndexPlace);

                //if (placeList != null)
                //{
                //    if (placeList.Count >= 1)
                //    {
                //        foreach (var placeItem in placeList)
                //        {
                //            model.PlaceList.Add(placeItem.PlaceName);   //liu 2016-10-20
                //            //model.PlaceList.Add(placeItem.EntityId.ToString());   // liu 2016-12-12
                //        }
                //    }
                //}
            }

            var result = _assetsService.QueryPage1(model);
            return Json(result);
        }

        //逻辑删除资产
        public ActionResult LogicRemoveOne(int entityId = 0, string catCode = "")
        {
            var result = _assetsService.LogicDelete(entityId);// 

            //if (string.IsNullOrEmpty(catCode))
            //    return base.Back("未找到相应的资产分类");
            //var @type = catCode.GetAssetsTypeByCatCode();

            //switch (@type)
            //{
            //    case AssetsTypes.Land:
            //        return RedirectToAction("EditLand", new { entityId = entityId });
            //        break;
            //    case AssetsTypes.Car:
            //        return RedirectToAction("EditCar", new { entityId = entityId });
            //        break;
            //    case AssetsTypes.Building:
            //        return RedirectToAction("EditBuilding", new { entityId = entityId });
            //        break;
            //    case AssetsTypes.Culturalrelic:
            //        return RedirectToAction("EditCulturalrelic", new { entityId = entityId });
            //        break;
            //    case AssetsTypes.Animalandplant:
            //        return RedirectToAction("EditAnimalandplant", new { entityId = entityId });
            //        break;
            //    case AssetsTypes.Furniture:
            //        return RedirectToAction("EditFurniture", new { entityId = entityId });
            //        break;
            //    case AssetsTypes.GeneralEquipment:
            //        return RedirectToAction("EditGeneralequipment", new { entityId = entityId });
            //        break;
            //    case AssetsTypes.House:
            //        return RedirectToAction("EditHouse", new { entityId = entityId });
            //        break;
            //    case AssetsTypes.SpecialEquipment:
            //        return RedirectToAction("EditSpecialequipment", new { entityId = entityId });
            //        break;
            //    case AssetsTypes.Book:
            //        return RedirectToAction("EditBooks", new { entityId = entityId });
            //        break;
            //}
            //return Back(/*entityId.ToString()+ */"删除成功！");
            return Json(true);
        }

        /// <summary>
        /// 逻辑删除资产
        /// </summary>
        /// <param name="entityId">资产主表ID</param>
        /// <param name="catCode">资产分类代码</param>
        /// <param name="orderListId">OderList表中的ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogicRemoveOneEx(string entityId = "", string catCode = "", string orderListId = "")
        {
            entityId = entityId.Trim('|');
            orderListId = orderListId.Trim('|');

            string[] strSo = entityId.Split('|');
            for (int i = 0; i < strSo.Length; i++)
            {
                int idOne = int.Parse(strSo[i]);

                //
                // 删除主表中的资产
                //
                //for (int i = 0; i < 1; i++)
                //{
                _assetsService.LogicDelete(idOne);

                //
                // 删除在OderList中的资产
                //
                try
                {
                    string[] strSoT = orderListId.Split('|');
                    for (int j = 0; j < strSoT.Length; j++)
                    {
                        int idTwo = int.Parse(strSoT[j]);
                        //2017-06-07 wnn
                        var order = _userService.GetOrderListOne(idTwo);
                        if (order != null)
                        {
                            AssetsRecord assetsRecord = new AssetsRecord();//实例化
                            assetsRecord.AssetsNum = order.AssetsNum;//赋值
                            assetsRecord.CatCode = order.CatCode;
                            assetsRecord.Counts = order.Counts;
                            assetsRecord.CreatedTime = order.CreatedTime;
                            assetsRecord.GoodsName = order.GoodsName;
                            assetsRecord.IsPrinted = order.IsPrinted;
                            assetsRecord.MainId = order.MainId;
                            assetsRecord.MeasurementUnits = order.MeasurementUnits;
                            assetsRecord.Price = order.Price;
                            assetsRecord.StorePlace = order.StorePlace;
                            assetsRecord.Type = order.Type;
                            assetsRecord.UseDepartment = order.UseDepartment;
                            assetsRecord.UsedNum1 = order.UsedNum1;
                            assetsRecord.UsePeople = order.UsePeople;
                            assetsRecord.UserId = order.UserId;

                            _userService.AddToAssetsRecord(assetsRecord);//执行添加
                        }
                        _userService.RemoveFormOrderList(new[] { idTwo });
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return Back("删除成功！");
        }

        /// <summary>
        /// 从购物车中逻辑删除资产
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogicRemoveAllFromOrderList(string type)
        {
            //
            // 先从OrderList中找出所有资产
            //
            List<OrderList> orderLists = _userService.QueryFormOrderList(type);
            List<int> assetsEntityId = orderLists.Select(p => p.MainId).ToList();

            //
            // 从主表中删除资产
            //
            foreach (var index in assetsEntityId)
            {
                _assetsService.LogicDelete(index);
            }

            //
            // 从orderList中删除
            //
            //2017-06-07 wnn
            var order = _userService.Query(type);//将满足条件的查询出来
            foreach (var item in order)
            {
                if (order != null)
                {
                    AssetsRecord assetsRecord = new AssetsRecord();//实例化
                    assetsRecord.AssetsNum = item.AssetsNum;//赋值
                    assetsRecord.CatCode = item.CatCode;
                    assetsRecord.Counts = item.Counts;
                    assetsRecord.CreatedTime = item.CreatedTime;
                    assetsRecord.GoodsName = item.GoodsName;
                    assetsRecord.IsPrinted = item.IsPrinted;
                    assetsRecord.MainId = item.MainId;
                    assetsRecord.MeasurementUnits = item.MeasurementUnits;
                    assetsRecord.Price = item.Price;
                    assetsRecord.StorePlace = item.StorePlace;
                    assetsRecord.Type = item.Type;
                    assetsRecord.UseDepartment = item.UseDepartment;
                    assetsRecord.UsedNum1 = item.UsedNum1;
                    assetsRecord.UsePeople = item.UsePeople;
                    assetsRecord.UserId = item.UserId;

                    _userService.AddToAssetsRecord(assetsRecord);//执行添加
                }
            }
            _userService.RemoveFormOrderList(type);
            return Json(true);
        }

        /// <summary>
        /// 变更购物车中的所有资产
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangeAllFromOrderList(string type, string changeItem, string changeContent)
        {
            type = type.Trim('|').Replace('|', ',');
            //
            // 先从OrderList中找出所有资产
            //
            List<OrderList> orderLists = _userService.QueryChangeOrderList(type);

            if (orderLists == null)
            {
                return Json(false);
            }

            List<int> assetsEntityId = orderLists.Select(p => p.MainId).ToList();

            var _changeItem = "";
            switch (changeItem)
            {
                case "存放地点":
                    _changeItem = "STORE_PLACE";
                    break;
                case "使用部门":
                    _changeItem = "USE_DEPARTMENT";
                    break;
                case "使用人":
                    _changeItem = "USE_PEOPLE";
                    break;
                case "资产名称":
                    _changeItem = "GOODS_NAME";
                    break;
                default:
                    break;
            }

            //
            // 从主表中删除资产
            //
            //foreach (var index in assetsEntityId)
            //{
            //    _assetsService.ChangeInfo(index, _changeItem, changeContent);

            //}
            foreach (var item in orderLists)
            {
                _assetsService.ChangeInfo(item.MainId, _changeItem, changeContent);
                _userService.ChangeInfo(item.EntityId, _changeItem, changeContent);
            }

            //
            // 将orderList中的数据清空
            //
            //_userService.RemoveFormOrderList(type);

            return Json(true);
        }


        /// <summary>
        /// 资产领用记录查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AcquireRecordQuery(AllRecordQuery model)
        {
            var result = _assetsAcquireServices.QueryPage(model);
            return Json(result);
        }
        /// <summary>
        /// 全部资产领用记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AllAcquireRecordQuery(AllRecordQuery model)
        {
            model.PageSize = int.MaxValue;
            var result = _assetsAcquireServices.QueryPage(model);
            return Json(result);
        }

        /// <summary>
        /// 资产借出记录查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BorrowRecordQuery(AllRecordQuery model)
        {
            var result = _assetsBorrrowServices.QueryPage(model);
            return Json(result);
        }
        /// <summary>
        /// 借出记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AllBorrowRecordQuery(AllRecordQuery model)
        {
            model.PageSize = int.MaxValue;
            var result = _assetsBorrrowServices.QueryPage(model);
            return Json(result);
        }

        /// <summary>
        /// 领用记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AllAquireRecordQuery(AllRecordQuery model)
        {
            model.PageSize = int.MaxValue;
            var result = _assetsAcquireServices.QueryPage(model);
            return Json(result);
        }

        /// <summary>
        /// 资产报修记录查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RepairRecordQuery(AllRecordQuery model)
        {
            var result = _repairService.QueryPage(model);
            return Json(result);
        }
        /// <summary>
        /// 全部报销记录查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AllRepairRecordQuery(AllRecordQuery model)
        {
            model.PageSize = int.MaxValue;
            var result = _repairService.QueryPage(model);
            return Json(result);
        }
        /// <summary>
        /// 盘点查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult InventoryRecordQuery(AllRecordQuery model)
        {
            var result = _inventoryService.QueryPage(model);
            return Json(result);
        }
        /// <summary>
        /// 全部盘点查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AllInventoryRecordQuery(AllRecordQuery model)
        {
            model.PageSize = int.MaxValue;
            var result = _inventoryService.QueryPage(model);
            return Json(result);
        }
        /// <summary>
        /// 资产报废申请查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ScrapRecordQuery(AllRecordQuery model)
        {
            var result = _scrapService.QueryPage(model);
            return Json(result);
        }
        /// <summary>
        /// 获取所有报废资产
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AllScrapRecordQuery(AllRecordQuery model)
        {
            model.PageSize = int.MaxValue;
            var result = _scrapService.QueryPage(model);
            return Json(result);
        }
        /// <summary>
        /// 获取所有报废资产进行遍历，合并以及分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult DoPreScrapReply(int entity, AllRecordQuery query)
        {

            var assetsNumList = _scrapService.GetAssetsScrapList(entity); // 根据序号获取资产编号
            var assetsscrapreply = new List<AssetsScrapReply>(); // 实例化assets_scrap_reply表
            var scrapMergeList = new List<ScrapMerge>();

            // 进行遍历
            for (int i = 0; i < assetsNumList.Count; i++)
            {
                //
                // 合并资产
                // 合并条件：资产名称，品牌及规格型号，价值，购置日期 一模一样的资产才进行合并
                // 实现方法：定义一个List作为实例化assets_scrap_reply表，然后将遍历出的资产assetsNumList(i)与List对比，不存在则写入List，
                //           存在则将存在List中的资产数量加1，将所有的资产遍历完对比过后，再将List写入表assets_scrap_reply
                //

                for (int j = 0; j <= scrapMergeList.Count; j++)
                {
                    var assets = _assetsService.Get2(assetsNumList[i].ToString());

                    if (scrapMergeList.Count == 0)
                    {
                        #region 添加到scrapMergeList

                        var scrapMerge = new ScrapMerge()
                        {
                            ScrapFormNo = entity,
                            AssetsNum = assets.AssetsNum,
                            Price = assets.Money,
                            GetsDate = assets.GetDate,
                            UsedYears = (DateTime.Now.Year - assets.GetDate.Year).ToString(CultureInfo.InvariantCulture),
                            ModelSpecification = assets.ModelSpecification,
                            Brand = assets.Brand,
                            Money = assets.Money,
                            AssetsName = assets.GoodsName,
                            Counts = assets.Counts
                        };
                        //
                        // 判断是什么类
                        //
                        if (assets.CatCode.ToString().Substring(0, 3) == "201" &&
                                 assets.CatCode.ToString().Substring(0, 9) != "201010300")
                        {
                            //计算机类
                            scrapMerge.type = 1;
                        }
                        else if (assets.CatCode.ToString().Substring(0, 9) == "201010300")
                        {
                            //服务器类
                            scrapMerge.type = 2;
                        }
                        else if (assets.CatCode.ToString().Substring(0, 5) == "20202")
                        {
                            //投影设备
                            scrapMerge.type = 3;
                        }
                        else
                        {
                            scrapMerge.type = 4;
                        }
                        scrapMergeList.Add(scrapMerge); // 写入List

                        #endregion

                        break;                                                  // 跳出比对遍历 将下一条资产进行比对
                    }

                    //
                    // 是否满足合并条件
                    //
                    if (assets.GoodsName == scrapMergeList[j].AssetsName &&
                        assets.Brand + assets.ModelSpecification == scrapMergeList[j].Brand + scrapMergeList[j].ModelSpecification &&
                        assets.Money == scrapMergeList[j].Price &&
                        assets.GetDate == scrapMergeList[j].GetsDate)           // 判断 资产名称，品牌及规格型号，单价，购置日期 是否相同
                    {
                        scrapMergeList[j].Counts++;                             // 数量叠加
                        //assetsscrapreply[j].Money += assets.Money;            // 金额暂时不累加
                        break;                                                  // 跳出比对遍历 将下一条资产进行比对
                    }
                    else if (j == scrapMergeList.Count - 1)                     // 只有与最后一条数据比对完才添加这条资产
                    {
                        #region 添加到scrapMergeList

                        var scrapMerge = new ScrapMerge()
                        {
                            ScrapFormNo = entity,
                            AssetsNum = assets.AssetsNum,
                            Price = assets.Money,
                            GetsDate = assets.GetDate,
                            UsedYears = (DateTime.Now.Year - assets.GetDate.Year).ToString(CultureInfo.InvariantCulture),
                            ModelSpecification = assets.ModelSpecification,
                            Brand = assets.Brand,
                            Money = assets.Money,
                            AssetsName = assets.GoodsName,
                            Counts = assets.Counts
                        };
                        //
                        // 判断是什么类
                        //
                        if (assets.CatCode.ToString().Substring(0, 3) == "201" &&
                                 assets.CatCode.ToString().Substring(0, 9) != "201010300")
                        {
                            //计算机类
                            scrapMerge.type = 1;
                        }
                        else if (assets.CatCode.ToString().Substring(0, 9) == "201010300")
                        {
                            //服务器类
                            scrapMerge.type = 2;
                        }
                        else if (assets.CatCode.ToString().Substring(0, 5) == "20202")
                        {
                            //投影设备
                            scrapMerge.type = 3;
                        }
                        else
                        {
                            scrapMerge.type = 4;
                        }
                        scrapMergeList.Add(scrapMerge); // 写入List

                        #endregion

                        break;                                                  // 跳出比对遍历 将下一条资产进行比对
                    }
                }
            }

            //
            // 重新计算金额
            //
            foreach (var item in scrapMergeList)
            {
                if (item.Counts > 1)
                {
                    item.Money *= item.Counts;
                }
            }

            //
            // 写入数据库
            //
            _scrapService.RemoveLastScrapMerge(entity);         // 添加之前先删除,防止重复点击同一条报废申请记录的分类合并按钮
            _scrapService.AddScrapMerge(scrapMergeList);


            //
            // 读取表assets_scrap_reply中的数据返回到前台用于显示
            //
            //query.PageSize = int.MaxValue;
            //var result = _scrapService.QueryPageReply(query);
            //var result = new PagedList<AssetsScrapReply>();
            //// 筛选数据
            //for(int i = 0; i < _a.Items.Count; i++){
            //    if(_a.Items[i].ScrapFormNo == entity)
            //    {
            //        result.Items.Add(_a.Items[i]);
            //    }
            //}

            return Json("OK");
        }

        /// <summary>
        /// 根据资产编号查询批复表，返回数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ReplyTable(string entity)
        {
            var result = _scrapService.GetRelpyTable(entity);
            return Json(result);
        }
        /// <summary>
        /// 资产详情查询
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public ActionResult ViewDetail(int entityId)
        {
            ViewBag.EntityId = entityId;
            var result = _assetsService.GetResultDto(entityId);
            if (null != result.AssetsMain && !string.IsNullOrEmpty(result.AssetsMain.AssetsPicPath))
            {
                var picHttpPath = Path.Combine(PortalSetting.UpLoadPicHttpPrix, result.AssetsMain.AssetsPicPath);
                result.AssetsMain.AssetsPicPath = picHttpPath;
            }
            //2017-06-07 wnn
            var Acquire = _userService.QueryNumR(result.AssetsMain.AssetsNum);//领用
            ViewBag.Acquire = Acquire;
            //var Borrow = _userService.QueryByAssetsNumR(ConstTag.Borrow, result.AssetsMain.AssetsNum);//借用
            //ViewBag.Borrow = Borrow;
            //var Service = _userService.QueryByAssetsNumR(ConstTag.Service, result.AssetsMain.AssetsNum);//报修
            //ViewBag.Service = Service;
            //var Repair = _userService.QueryByAssetsNumR(ConstTag.Repair, result.AssetsMain.AssetsNum);//维修
            //ViewBag.Repair = Repair;
            //var ChangeInfo = _userService.QueryByAssetsNumR(ConstTag.ChangeInfo, result.AssetsMain.AssetsNum);//盘点
            //ViewBag.ChangeInfo = ChangeInfo;

            return View(result);
        }

        /// <summary>
        /// 生成资产标签
        /// </summary>
        /// <param name="assetsNums"></param>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult CreateQrCode(List<string> assetsNums, int entityId)
        {
            ViewBag.EntityId = entityId;
            var result = new Dictionary<string, string>();
            //从cookie获取配置文件
            string settings;
            int titleX = 0;
            int titleY = 0;
            int codeX = 0;
            int codeY = 0;
            int fontSize = 0;
            string lableName="";
            string lableContact="";
            if (Request.Cookies["printset"] == null)
            {
                settings = "assetname-assetcode-brandtype-storeplace-user";
            }
            else
            {
                settings = Request.Cookies["printset"].Value;
            }
            if (Request.Cookies["printTitleX"] != null)
            {
                titleX = int.Parse(Request.Cookies["printTitleX"].Value);
            }
            if (Request.Cookies["printTitleY"] != null)
            {
                titleY = int.Parse(Request.Cookies["printTitleY"].Value);
            }
            if (Request.Cookies["printcodeX"] != null)
            {
                codeX = int.Parse(Request.Cookies["printcodeX"].Value);
            }
            if (Request.Cookies["printcodeY"] != null)
            {
                codeY = int.Parse(Request.Cookies["printcodeY"].Value);
            }
            if (Request.Cookies["printFontSize"] != null)
            {
                fontSize = int.Parse(Request.Cookies["printFontSize"].Value);
            }
            if (null != assetsNums && assetsNums.Count > 0)
            {
                var assets = _assetsService.Get(assetsNums);
                foreach (var item in assets)
                {
                    //
                    // 如果资产的取得日期为0001/1/1 0:00:00则将日期值设置为当天
                    //
                    if (item.GetDate.Equals(DateTime.MinValue))
                    {
                        item.GetDate = DateTime.Now;
                    }

                    //
                    // 实例化info来存储资产条码打印所需数据
                    // 调用QrCodeInfo类
                    //
                    var info = new QrCode.QrCodeBuilder.QrCodeInfo
                    {
                        //Title = item.GoodsName2,
                        Title = PortalSetting.QrCodeTitle,/*"石油附属实验小学"*/
                        Name = item.GoodsName,
                        Num = item.AssetsNum,
                        CatCode = item.CatCode,
                        ModelSpecification = item.ModelSpecification,
                        StorePlace = item.StorePlace,
                        ofsNum = item.AssetsNum,
                        UsePeople = item.UsePeople,
                        //为了打印二维码新加入的属性
                        DateTime = item.GetDate,
                        de = item.UseDepartment
                    };

                    /*有动态码的显示动态码，无动态码的显示系统自动生成的码*/
                    if (!string.IsNullOrEmpty(item.UsedNum1)
                        && item.UsedNum1 != "DSINEST")
                    {
                        info.Num = item.UsedNum1;
                    }
                    var fileName = string.Format("{0}.jpg", item.AssetsNum);

                    QrCode.QrCodeBuilder.Build(info, Path.Combine(PortalSetting.QrCodesPath, fileName), settings, titleX, titleY, codeX, codeY,fontSize, lableName,lableContact);
                    var httpPath = string.Format("{0}://{1}{2}{3}",
                        HttpContext.Request.Url.Scheme,
                        HttpContext.Request.Url.Authority,
                        PortalSetting.QrCodesHttpPrix,
                        fileName);
                    if (!result.ContainsKey(item.AssetsNum))
                    {
                        result.Add(item.AssetsNum, httpPath);
                    }
                }
            }
            return View(result);
        }

        /// <summary>
        /// 生成资产标签
        /// </summary>
        /// <param name="assetsNums"></param>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult CreateQrCodeEx(List<string> assetsNums)
        {
            var result = new Dictionary<string, string>();
            //从cookie获取配置文件
            string settings;
            int titleX = 0;
            int titleY = 0;
            int codeX = 0;
            int codeY = 0;
            int fontSize = 0;
            string lableName="";
            string lableContact="";
            QrprintSetting qrset = new QrprintSetting();
            qrset = _qrprintSettingService.getqrset();
            if (qrset.Printset.Equals("empty"))
            {
                settings = "assetname-assetcode-brandtype-storeplace-user";
            }
            else
            {
                settings = qrset.Printset;
            }
            if (!qrset.PrintTitleX.Equals(""))
            {
                titleX = int.Parse(qrset.PrintTitleX);
            }
            if (!qrset.PrintTitleY.Equals(""))
            {
                titleY = int.Parse(qrset.PrintTitleY);
            }
            if (!qrset.PrintcodeX.Equals(""))
            {
                codeX = int.Parse(qrset.PrintcodeX);
            }
            if (!qrset.PrintcodeY.Equals(""))
            {
                codeY = int.Parse(qrset.PrintcodeY);
            }
            if (!qrset.LableName.Equals(""))
            {
                lableName = qrset.LableName;
            }
            if (!qrset.LableContact.Equals(""))
            {
                lableContact = qrset.LableContact;
            }
            if (!qrset.PrintFontSize.Equals(""))
            {
                fontSize = int.Parse(qrset.PrintFontSize);
            }
            //if (Request.Cookies["printset"] == null)
            //{
            //    settings = "assetname-assetcode-brandtype-storeplace-user";
            //}
            //else
            //{
            //    settings = Request.Cookies["printset"].Value;
            //}
            //if (Request.Cookies["printTitleX"] != null)
            //{
            //    titleX = int.Parse(Request.Cookies["printTitleX"].Value);
            //}
            //if (Request.Cookies["printTitleY"] != null)
            //{
            //    titleY = int.Parse(Request.Cookies["printTitleY"].Value);
            //}
            //if (Request.Cookies["printcodeX"] != null)
            //{
            //    codeX = int.Parse(Request.Cookies["printcodeX"].Value);
            //}
            //if (Request.Cookies["printcodeY"] != null)
            //{
            //    codeY = int.Parse(Request.Cookies["printcodeY"].Value);
            //}

            //
            // 生成资产条码
            //
            if (null != assetsNums && assetsNums.Count > 0)
            {
                //
                // 根据资产编号去数据库取回该资产数据
                //
                var assets = _assetsService.Get(assetsNums);
                foreach (var item in assets)
                {
                    //
                    // 如果资产的取得日期为0001/1/1 0:00:00则将日期值设置为当天
                    //
                    if (item.GetDate.Equals(DateTime.MinValue))
                    {
                        item.GetDate = DateTime.Now;
                    }

                    //
                    // 实例化info来存储资产条码打印所需数据
                    // 调用QrCodeInfo类
                    //
                    var info = new QrCode.QrCodeBuilder.QrCodeInfo
                    {
                        //Title = item.GoodsName2,
                        Title = PortalSetting.QrCodeTitle,
                        Name = item.GoodsName,
                        Num = item.AssetsNum,
                        CatCode = item.CatCode,
                        ModelSpecification = item.ModelSpecification,
                        StorePlace = item.StorePlace,
                        ofsNum = item.AssetsNum,
                        UsePeople = item.UsePeople,
                        //为了打印二维码新加入的属性
                        DateTime = item.GetDate,
                        de = item.UseDepartment
                    };

                    /*有动态码的显示动态码，无动态码的显示系统自动生成的码*/
                    if (!string.IsNullOrEmpty(item.UsedNum1)
                        && item.UsedNum1 != "DSINEST")
                    {
                        info.Num = item.UsedNum1;
                    }
                    var fileName = string.Format("{0}.jpg", item.AssetsNum);

                    //
                    // 生成整个条码
                    //
                    QrCode.QrCodeBuilder.Build(info, Path.Combine(PortalSetting.QrCodesPath, fileName), settings, titleX, titleY, codeX, codeY,fontSize, lableName, lableContact);
                    var httpPath = string.Format("{0}://{1}{2}{3}",
                        HttpContext.Request.Url.Scheme,
                        HttpContext.Request.Url.Authority,
                        PortalSetting.QrCodesHttpPrix,
                        fileName);
                    if (!result.ContainsKey(item.AssetsNum))
                    {
                        result.Add(item.AssetsNum, httpPath);
                    }

                }
            }
            return View(result);
        }


        [HttpGet]
        public ActionResult DownloadQrCode(string assetsNum)
        {
            var fileName = string.Format("{0}.jpg", assetsNum);
            var realPath = Path.Combine(PortalSetting.QrCodesPath, fileName);
            return File(realPath, @"image/jpeg ");
        }

        /// <summary>
        /// 打印设置页面
        /// </summary>
        /// <returns></returns>
        //TODO 记得加上权限
        public ActionResult QrPrintSetting()
        {
            QrprintSetting qrset = new QrprintSetting();
            qrset =_qrprintSettingService.getqrset();
            PrintModel prm = new PrintModel();
            prm.LableContact = qrset.LableContact;
            prm.LableName = qrset.LableName;
            prm.PrintcodeX = qrset.PrintcodeX;
            prm.PrintcodeY = qrset.PrintcodeY;
            prm.Printset = qrset.Printset;
            prm.PrintTitleX = qrset.PrintTitleX;
            prm.PrintTitleY = qrset.PrintTitleY;
            prm.PrintFontSize = qrset.PrintFontSize;
            return View(prm);
        }
        /// <summary>
        /// 打印页面存数据库
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult QrPrintSetting(QrprintSetting qrset)
        {
            try
            {
                if (qrset.LableContact==null)
                {
                    qrset.LableContact = "";
                }
                if (qrset.LableName == null)
                {
                    qrset.LableName = "";
                }
                _qrprintSettingService.add(qrset);
                QrprintSettinghistory qrsethistory = new QrprintSettinghistory();
                ShallowCopyHelper.CopyPropertiesValue(qrset, qrsethistory);
                qrsethistory.SettingTime = DateTime.Now.ToString();
                _qrprintSettingService.add(qrsethistory);

            }
            catch (Exception e)
            {

                throw;
            }
           
            return View();
        }

        /// <summary>
        /// 标记已经打印
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Printed(string assetsNum)
        {
            _userService.UpdateIsPrinted(assetsNum);
            var result = _assetsService.Printed(assetsNum);
            return Json(result);
        }

        /// <summary>
        /// 将经过筛选之后的资产全部添加到资产操作列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddAllToOrderList(AssetsQuery model)
        {
            string type = model.PageType;
            //
            // 筛选资产
            //
            #region 筛选资产
            List<ClassCode> classList = new List<ClassCode>();
            List<Department> deptList = new List<Department>();
            List<Place> placeList = new List<Place>();



            //
            // 获取类别及其子类别
            //
            if (!string.IsNullOrEmpty(model.IndexClass))
            {
                classList = _assetsService.GetClassCodeByParentCode(model.IndexClass);

                if (classList != null)
                {

                    if (model.ClassList == null)
                    {
                        model.ClassList = new List<string>();
                    }
                    else
                    {
                        model.ClassList.Clear();
                    }

                    if (classList.Count >= 1)
                    {
                        foreach (var classItem in classList)
                        {
                            model.ClassList.Add(classItem.EntityId);
                        }
                    }
                }

            }

            //
            // 获取部门及其子部门
            //
            if (!string.IsNullOrEmpty(model.IndexDept))
            {
                if (model.Role == "部门负责人")
                {
                    deptList = _assetsService.GetDepartment(model.UseDepartment);

                    var tempDept = new List<string>();

                    if (deptList != null)
                    {
                        if (deptList.Count >= 1)
                        {
                            tempDept.AddRange(Enumerable.Select(deptList, deptItem => deptItem.DeptName));
                        }
                    }

                    //
                    // 部门负责人只能看见部门及其子部门下的资产
                    //
                    if (tempDept.Contains(model.IndexDept))
                    {
                        //
                        // 清空历史数据
                        //
                        if (model.DeptList == null)
                        {
                            model.DeptList = new List<string>();
                        }
                        else
                        {
                            model.DeptList.Clear();
                        }
                        model.DeptList.Add(model.IndexDept);

                        ////
                        //// 添加目标检索项子项
                        ////
                        //deptList = _assetsService.GetDepartment(model.IndexDept);

                        //if (deptList != null)
                        //{
                        //    if (deptList.Count >= 1)
                        //    {
                        //        foreach (var deptItem in deptList)
                        //        {
                        //            model.DeptList.Add(deptItem.DeptName);
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        //
                        // 如果是选中的部门不属于用户的部门或者子部门则
                        //
                        model.DeptList = tempDept;
                    }
                }
                else
                {
                    //
                    // 清空历史数据
                    //
                    if (model.DeptList == null)
                    {
                        model.DeptList = new List<string>();
                    }
                    else
                    {
                        model.DeptList.Clear();
                    }
                    model.DeptList.Add(model.IndexDept);

                    //
                    // 添加目标检索项子项
                    //
                    //deptList = _assetsService.GetDepartment(model.IndexDept);

                    //if (deptList != null)
                    //{
                    //    if (deptList.Count >= 1)
                    //    {
                    //        foreach (var deptItem in deptList)
                    //        {
                    //            model.DeptList.Add(deptItem.DeptName);
                    //        }
                    //    }
                    //}
                }
            }
            else
            {
                if (model.Role == "部门负责人")
                {
                    model.IndexDept = model.UseDepartment;
                    //
                    // 清空历史数据
                    //
                    if (model.DeptList == null)
                    {
                        model.DeptList = new List<string>();
                    }
                    else
                    {
                        model.DeptList.Clear();
                    }
                    model.DeptList.Add(model.IndexDept);

                    //
                    // 添加目标检索项子项
                    //
                    //deptList = _assetsService.GetDepartment(model.IndexDept);

                    //if (deptList != null)
                    //{
                    //    if (deptList.Count >= 1)
                    //    {
                    //        foreach (var deptItem in deptList)
                    //        {
                    //            model.DeptList.Add(deptItem.DeptName);
                    //        }
                    //    }
                    //}
                }
            }
            //
            // 获取地点及其子地点
            //
            if (!string.IsNullOrEmpty(model.IndexPlace))
            {
                //
                // 清空之前的数据
                //
                if (model.PlaceList == null)
                {
                    model.PlaceList = new List<string>();
                }
                else
                {
                    model.PlaceList.Clear();
                }

                //
                // 添加目标检索项
                //
                model.PlaceList.Add(model.IndexPlace);

                //
                // 添加目标检索项目的子项
                //
                //placeList = _assetsService.GetPlace(model.IndexPlace);

                //if (placeList != null)
                //{
                //    if (placeList.Count >= 1)
                //    {
                //        foreach (var placeItem in placeList)
                //        {
                //            model.PlaceList.Add(placeItem.PlaceName);   //liu 2016-10-20
                //            //model.PlaceList.Add(placeItem.EntityId.ToString());   // liu 2016-12-12
                //        }
                //    }
                //}
            }

            var list = _assetsService.QueryAsstesList(model);
            #endregion

            #region 添加到操作列表
            //
            // 添加到OrderList
            //
            if (null != list && list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (!_userService.IsExistOrderList(UserId, item.AssetsNum, type))
                    {
                        var entity = new OrderList
                        {
                            UserId = UserId,
                            CreatedTime = DateTime.Now,
                            GoodsName = item.GoodsName,
                            AssetsNum = item.AssetsNum,
                            CatCode = item.CatCode,
                            Price = item.Price,
                            Type = type,
                            Counts = item.Counts,
                            MeasurementUnits = item.MeasurementUnits,
                            IsPrinted = item.IsPrinted,
                            UsedNum1 = item.UsedNum1,
                            MainId = item.EntityId,
                            UsePeople = item.UsePeople,
                            UseDepartment = item.UseDepartment,
                            StorePlace = item.StorePlace
                        };
                        _userService.AddToOrderList(entity);
                    }
                }
                return Json(true);
            }

            return Json(false);

            #endregion
        }
    }
}