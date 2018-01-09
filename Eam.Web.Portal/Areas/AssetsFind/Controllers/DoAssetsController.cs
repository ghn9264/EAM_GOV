﻿using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EAM.Data.Domain;
using EAM.Data.ImportAndExport.Export.ExportAssets;
using EAM.Data.Services;
using EAM.Data.Services.Dto;
using EAM.Inventory;
using Eam.Web.Portal.Areas.AssetsFind.Models;
using Eam.Web.Portal._Comm;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Eam.Core.Utility;

namespace Eam.Web.Portal.Areas.AssetsFind.Controllers
{
    public class DoAssetsController : EamAdminController
    {
        private readonly IAssetsOptionService _assetsOptionService;
        private readonly IUserService _userService;
        private readonly IClassCodeServices _classcodeService;
        private readonly IScrapService _scrapService;

        public DoAssetsController(IAssetsOptionService assetsOptionService,
            IUserService userService, IClassCodeServices classcodeService,
            IScrapService scrapService)
        {
            _assetsOptionService = assetsOptionService;
            _userService = userService;
            _classcodeService = classcodeService;
            _scrapService = scrapService;
        }


        public ActionResult Index()
        {
            return RedirectToAction("AcquireAssets");
        }

        [Permission(EnumBusinessPermission.Assets_Do_Acquire)]
        public ActionResult AcquireAssets()
        {
            ViewBag.DoAction = ConstTag.Acquire;
            return View();
        }

        [Permission(EnumBusinessPermission.Assets_Do_Acquire)]
        public ActionResult SendBackAssets()
        {
            ViewBag.DoAction = ConstTag.SendBack;
            return View();
        }



        /// <summary>
        /// 领用按钮点击事件处理
        /// 将资产领用单添加到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission(EnumBusinessPermission.Assets_Do_Acquire)]
        public ActionResult AcquireAssets(AcquireModel model)
        {
            //
            // 判断输入合法性
            //
            if (!ModelState.IsValid)
                return View(model);
            if (model.AssetsNum == null || model.AssetsNum.Count == 0)
            {
                ModelState.AddModelError("*", "领用项目不能为空");
                return View(model);
            }

            //
            // 获得领用单输入数据
            //
            var assetsAquair = new AquairAttribute
            {
                AcquireDate = model.AcquireDate,
                AcquireDepartment = model.AcquireDepartment,
                AcquirePerson = model.AcquirePerson,
                AcquirePersonPhone = model.AcquirePersonPhone,
                AssetsNums = model.AssetsNum
            };

            //
            // 添加到数据库
            //
            _assetsOptionService.AddAssetsAquair(assetsAquair);

           // _userService.RemoveFormById();
            //
            // 删除所有待领用资产
            //
            //2017-06-07 wnn
            var order = _userService.Query(ConstTag.Acquire);//将满足条件的查询出来
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

            _userService.RemoveFormOrderList(ConstTag.Acquire);

            return PageReturn("领用成功");
        }

        /// <summary>
        /// 删除资产领用记录
        /// </summary>
        /// <param name="doAssetsId"></param>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Assets_Do_Acquire)]
        public ActionResult DeleteAcquireRecord(int doAssetsId)
        {

            //
            // 删除资产领用记录主表
            //
            _assetsOptionService.DeleteAssetsAquair(doAssetsId);

            return Back("删除成功");
        }


        [Permission(EnumBusinessPermission.Assets_Do_Borrow)]
        public ActionResult BorrowAssets()
        {
            ViewBag.DoAction = ConstTag.Borrow;
            return View();
        }

        /// <summary>
        /// 借用按钮点击事件处理
        /// 将资产借用单添加到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission(EnumBusinessPermission.Assets_Do_Borrow)]
        public ActionResult BorrowAssets(BorrowModel model)
        {
            //
            // 判断输入合法性
            //
            if (!ModelState.IsValid)
                return View(model);
            if (model.AssetsNum == null || model.AssetsNum.Count == 0)
            {
                ModelState.AddModelError("*", "借用项目不能为空");
                return View(model);
            }

            //
            // 获得领用单输入数据
            //
            var assetsBorrow = new BorrowAttribute
            {
                BorrowPerson = model.BorrowPerson,
                BorrowDate = model.BorrowDate,
                BorrowUse = model.BorrowUse,
                BorrowDeparitment = model.BorrowDepartment,
                BorrowPersonPhone = model.BorrowPersonPhone,
                BorrowLong = model.BorrowLong,
                AssetsNums = model.AssetsNum
            };

            //
            // 添加到数据库
            //
            _assetsOptionService.AddAssetsBorrow(assetsBorrow);

            //
            // 删除所有待领用资产
            //

            //2017-06-07 wnn
            var order = _userService.Query(ConstTag.Borrow);//将满足条件的查询出来
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

            _userService.RemoveFormOrderList(ConstTag.Borrow);

            return PageReturn("借用成功");
        }

        /// <summary>
        /// 删除资产借用记录
        /// </summary>
        /// <param name="doAssetsId"></param>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Assets_Do_Borrow)]
        public ActionResult DeleteBorrowAssetsRecord(int doAssetsId)
        {

            //
            // 删除资产领用记录主表
            //
            _assetsOptionService.DeleteAssetsBorrow(doAssetsId);

            return Back("删除成功");
        }
        /// <summary>
        /// 删除所有资产借用记录
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Assets_Do_Borrow)]
        public ActionResult DeleteAllBorrowAssetsRecord()
        {

            //
            // 删除资产领用记录主表
            //
            _assetsOptionService.DeleteAllAssetsBorrow();

            return Json(true);
        }


        /// <summary>
        /// 查询借用详情
        /// </summary>
        /// <param name="borrowId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BorrowDetail(int borrowId = 0) // $.post("/AssetsFind/DoAssets/BorrowDetail", { borrowId: borrowId }, function (result) 这个就是前台调用这个函数的写法
        // BorrowDetail函数名称对应，传递参数对应，result这个是函数返回结果，对应ActionResult，从函数体里面看出来是返回Json(dto);
        {
            var dto = _assetsOptionService.QueryBorrow(borrowId); // 这个就是查询出资产记录的函数QueryBorrow，这个函数实现在_assetsOptionService这个对象里，这个对象在IAssetsOptionService类里面
            return Json(dto);
        }

        /// <summary>
        /// 查询领用详情
        /// </summary>
        /// <param name="borrowId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AquireDetail(int borrowId = 0) // $.post("/AssetsFind/DoAssets/BorrowDetail", { borrowId: borrowId }, function (result) 这个就是前台调用这个函数的写法
        // BorrowDetail函数名称对应，传递参数对应，result这个是函数返回结果，对应ActionResult，从函数体里面看出来是返回Json(dto);
        {
            var dto = _assetsOptionService.QueryAquire(borrowId); // 这个就是查询出资产记录的函数QueryBorrow，这个函数实现在_assetsOptionService这个对象里，这个对象在IAssetsOptionService类里面
            return Json(dto);
        }

        /// <summary>
        /// 资产归还
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Permission(EnumBusinessPermission.Assets_Do_SendBack)]
        public ActionResult SendBackAssets(SendBackModel model, FormCollection form)
        {
            string err;
            if (!model.Validate(out err))
                return Json(err);
            var returnDto = new SendBackDto
            {
                AquireId = model.BorrowId,
                SendBackDate = model.SendBackDate,
                SendBackMemo = model.SendBackMemo,
                SendBackPerson = model.SendBackPerson
            };
            foreach (var key in form.AllKeys)
            {
                if (key.StartsWith("_STATUS_"))
                {
                    returnDto.AssetsStatus.Add(key.Replace("_STATUS_", ""), form[key]);
                }
            }
            try
            {
                _assetsOptionService.AssetsSendBack(returnDto);
                return Json("ok");
            }
            catch (Exception ex)
            {
                return Json("退还失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 资产盘点计划
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Assets_Do_InventoryPlan)]
        public ActionResult InventoryPlanAssets()
        {
            ViewBag.DoAction = ConstTag.Inventory;
            return View();
        }

        /// <summary>
        /// 盘点计划按钮点击事件处理
        /// 将资产盘点计划单添加到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission(EnumBusinessPermission.Assets_Do_InventoryPlan)]
        public ActionResult InventoryPlanAssets(InventoryPlanModel model)
        {
            //
            // 判断输入合法性
            //
            if (!ModelState.IsValid)
                return View(model);
            if (model.AssetsNum == null || model.AssetsNum.Count == 0)
            {
                ModelState.AddModelError("*", "盘点项目不能为空");
                return View(model);
            }

            //
            // 从orderList表中查询出inventory类型记录
            //
            var inventoryList = _userService.QueryFormOrderList(ConstTag.Inventory);


            //
            // 获得领用单输入数据
            //
            var assetsInventory = new InventoryAttribute
            {
                InventoryDate = model.InventoryDate,
                InventoryPerson = model.InventoryPerson,
                InventoryDepartment = model.InventoryDepartment,
                InventoryOperationDepartment = model.InventoryOperationDepartment,
                AssetsNums = inventoryList.Select(A => A.AssetsNum).ToList()
            };

            //
            // 添加到数据库
            //
            _assetsOptionService.AddAssetsInventoryPlan(assetsInventory);

            //
            // 删除所有待领用资产
            //
            //2017-06-07 wnn
            var order = _userService.Query(ConstTag.Inventory);//将满足条件的查询出来
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
            _userService.RemoveFormOrderList(ConstTag.Inventory);

            return PageReturn("创建盘点成功");
        }

        [HttpPost]
        public ActionResult UploadInventoryPlanFile(HttpPostedFileBase file)
        {
            int ErrorCode = 100;
            if (file.ContentLength == 0)
            {
                var result = new { Code = ErrorCode, Message = "文件内容为空" };
                return Content(JsonConvert.SerializeObject(result));
            }
            var tempFile = Server.MapPath("~/Temp/");
            var savePath = Path.Combine(tempFile, string.Format("Inventory_up_{0}.db", DateTime.Now.ToString("yyMMddHHmmss")));
            try
            {
                file.SaveAs(savePath);
                var inventoryHelper = new InventoryHelper(savePath);
                var inventoryResult = inventoryHelper.GetInventoryList();
                var result = new { Code = 0, Message = "读取数据成功", Data = inventoryResult };
                return Content(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                var result = new { Code = ErrorCode, Message = "请上传数据库文件" };
                return Content(JsonConvert.SerializeObject(result));
            }
        }

        /// <summary>
        /// 资产盘点
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Assets_Do_Inventory)]
        public ActionResult InventoryAssets()
        {
            ViewBag.DoAction = ConstTag.Inventory;
            return View();
        }

        /// <summary>
        /// 盘点计划按钮点击事件处理
        /// 将资产盘点计划单添加到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission(EnumBusinessPermission.Assets_Do_InventoryPlan)]
        public ActionResult InventoryAssets(InventoryModel model, FormCollection form)
        {
            string err;
            if (!model.Validate(out err)) // 判断，如果打包过来的数据不合法，返回err，结束
                return Json(err);
            var inventoryDto = new InventoryOperatorDto // 初始化接口对象，用于接收打包的数据
            {
                InventoryId = model.InventoryId, // 接收资产盘点计划表序号
                InventoryOperatePerson = model.InventoryOperationPerson, // 打包过来的 盘点人 对应到 资产实际盘点人
                InventoryOperateDepartment = model.InventoryOperationDepartment, // 打包过来的 盘点部门 对应到 资产实际盘点部门
                InventoryOperateDate = model.InventoryOperationDate // 盘点时间 对应到 资产实际盘点时间
            };
            //
            // 循环 遍历出每一条数据放到key里面
            //
            foreach (var key in form.AllKeys)
            {
                //
                // 判断遍历出的数据开头是否有"_INVENTORYRESULT_" （动态编号）
                //
                if (key.StartsWith("_INVENTORYRESULT_"))
                {
                    // 将来自key中的数据的逗号（，）前面的字符串（"_INVENTORYRESULT_"）去掉，然后将拆分后的数据放到 form[key] 里面，然后再将动态编号对应的（AssetsInventoryResult 盘点结果）添加到数据库
                    // 增加去逗号。
                    inventoryDto.AssetsInventoryResult.Add(key.Replace("_INVENTORYRESULT_", ""), form[key].Replace(",", ""));
                }
            }
            //
            // 抛异常 无异常则说明成功，返回OK  有异常返回失败+异常问题
            //
            try
            {
                _assetsOptionService.AssetsInventory(inventoryDto);
                return Json("ok");
            }
            catch (Exception ex)
            {

                return Json("盘点失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 查询盘点详情
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InventoryDetail(int inventoryId = 0) // $.post("/AssetsFind/DoAssets/BorrowDetail", { borrowId: borrowId }, function (result) 这个就是前台调用这个函数的写法
        // BorrowDetail函数名称对应，传递参数对应，result这个是函数返回结果，对应ActionResult，从函数体里面看出来是返回Json(dto);
        {
            var dto = _assetsOptionService.QueryInventory(inventoryId); // 这个就是查询出资产记录的函数QueryBorrow，这个函数实现在_assetsOptionService这个对象里，这个对象在IAssetsOptionService类里面
            return Json(dto);
        }

        /// <summary>
        /// 删除资产盘点记录
        /// </summary>
        /// <param name="doAssetsId"></param>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Assets_Do_InventoryPlan)]
        public ActionResult DeleteInventoryPlanRecord(int doAssetsId)
        {

            //
            // 删除资产领用记录主表
            //
            _assetsOptionService.DeleteAssetsInventoryPlan(doAssetsId);

            return Back("删除成功");
        }
        [Permission(EnumBusinessPermission.Assets_Do_InventoryPlan)]
        public ActionResult DeleteAllInventoryPlanRecord()
        {

            //
            // 删除资产领用记录主表
            //
            _assetsOptionService.DeleteAllAssetsInventoryPlan();

            return Back("删除成功");
        }

        /// <summary>
        /// 资产报修
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Assets_Do_Repair)]
        public ActionResult RepairAssets()
        {
            ViewBag.DoAction = ConstTag.Repair;
            return View();
        }

        /// <summary>
        /// 报修按钮点击事件处理
        /// 将资产报修单添加到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission(EnumBusinessPermission.Assets_Do_Repair)]
        public ActionResult RepairAssets(RepairModel model)
        {
            //
            // 判断输入合法性
            //
            if (!ModelState.IsValid)
                return View(model);
            if (model.AssetsNum == null || model.AssetsNum.Count == 0)
            {
                ModelState.AddModelError("*", "报修项目不能为空");
                return View(model);
            }

            //
            // 获得领用单输入数据
            //
            var assetsRepair = new RepairAttribute
            {
                RepairPerson = model.RepairPerson,
                RepairPersonPhone = model.RepairPersonPhone,
                RepairDate = model.RepairDate,
                RepairDepartment = model.RepairDepartment,
                AssetsNums = model.AssetsNum
            };

            //
            // 添加到数据库
            //
            _assetsOptionService.AddAssetsRepair(assetsRepair);

            //
            // 删除所有待报修资产
            //
            _userService.RemoveFormOrderList(ConstTag.Repair);

            return PageReturn("报修成功");
        }

        /// <summary>
        /// 查询维修详情
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RepairDetail(int repairId = 0)
        {
            var dto = _assetsOptionService.QueryRepair(repairId);
            return Json(dto);
        }

        /// <summary>
        /// 删除资产借用记录
        /// </summary>
        /// <param name="doAssetsId"></param>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Assets_Do_Repair)]
        public ActionResult DeleteRepairRecord(int doAssetsId)
        {

            //
            // 删除资产领用记录主表
            //
            _assetsOptionService.DeleteAssetsRepair(doAssetsId);

            return Back("删除成功");
        }
        [Permission(EnumBusinessPermission.Assets_Do_Repair)]
        public ActionResult DeleteAllRepairRecord()
        {

            //
            // 删除资产领用记录主表
            //
            _assetsOptionService.DeleteAllAssetsRepair();

            return Back("删除成功");
        }

        /// <summary>
        /// 资产归还
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Assets_Do_Return)]
        public ActionResult ReturnAssets()
        {
            return View();
        }

        /// <summary>
        /// 资产归还
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Permission(EnumBusinessPermission.Assets_Do_Return)]
        public ActionResult ReturnAssets(ReturnModel model, FormCollection form)
        {
            string err;
            if (!model.Validate(out err))
                return Json(err);
            var returnDto = new ReturnDto
            {
                BorrowId = model.BorrowId,
                ReturnDate = model.ReturnDate,
                ReturnMome = model.ReturnMome,
                ReturnPerson = model.ReturnPerson
            };
            foreach (var key in form.AllKeys)
            {
                if (key.StartsWith("_STATUS_"))
                {
                    returnDto.AssetsStatus.Add(key.Replace("_STATUS_", ""), form[key]);
                }
            }
            try
            {
                _assetsOptionService.AssetsReturn(returnDto);
                return Json("ok");
            }
            catch (Exception ex)
            {
                return Json("归还失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 资产维修
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Assets_Do_Service)]
        public ActionResult ServiceAssets()
        {
            return View();
        }

        /// <summary>
        /// 资产维修
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Permission(EnumBusinessPermission.Assets_Do_Repair)]
        public ActionResult ServiceAssets(ServicesModel model, FormCollection form)
        {
            string err;
            if (!model.Validate(out err))
                return Json(err);
            var serviceDto = new ServicesDto()
            {
                RepairId = model.RepairId,
                ServiceDate = model.ServiceDate,
                ServicePersonPhone = model.ServicePersonPhone,
                ServicePerson = model.ServicePerson,
                ServicesMemo = model.ServicesMemo,

            };
            foreach (var key in form.AllKeys)
            {
                if (key.StartsWith("_REPAIRRESULT_"))
                {
                    serviceDto.ServicesResult.Add(key.Replace("_REPAIRRESULT_", ""), form[key]);
                }
            }
            try
            {
                _assetsOptionService.AssetsServices(serviceDto);
                return Json("ok");
            }
            catch (Exception ex)
            {
                return Json("维修提交失败：" + ex.Message);
            }
        }


        /// <summary>
        /// 资产报废申请
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Assets_Do_ScrapApply)]
        public ActionResult ScrapApply()
        {
            ViewBag.DoAction = ConstTag.Scrap;
            return View();
        }


        /// <summary>
        /// 提交报废申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission(EnumBusinessPermission.Assets_Do_ScrapApply)]
        public ActionResult ScrapApply(ScrapApplyModel model)
        {
            //
            // 判断输入合法性
            //
            if (!ModelState.IsValid)
                return View(model);
            if (model.AssetsNum == null || model.AssetsNum.Count == 0)
            {
                ModelState.AddModelError("*", "报废项目不能为空");
                return View(model);
            }

            //
            // 获得领用单输入数据
            //
            var assetsScrap = new AssetsScrapApply
            {

                ScrapPerson = model.ScrapPerson,
                ScrapUnit = model.ScrapUnit,
                ScrapDate = model.ScrapDate,
                ScrapPersonPhone = model.ScrapPersonPhone,
                AssetsNums = model.AssetsNum
            };

            //
            // 添加到数据库
            //
            _assetsOptionService.AddAssetsScrapApply(assetsScrap);

            //
            // 删除所有待报修资产
            //
            _userService.RemoveFormOrderList(ConstTag.Scrap);

            return PageReturn("申请报废成功");
        }

        /// <summary>
        /// 删除资产报废申请记录
        /// </summary>
        /// <param name="doAssetsId"></param>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Assets_Do_ScrapApply)]
        public ActionResult DeleteScrapApplyRecord(int doAssetsId)
        {

            //
            // 删除资产领用记录主表
            //
            _assetsOptionService.DeleteAssetsScrapApply(doAssetsId);

            return Back("删除成功");
        }
        /// <summary>
        /// 删除所有资产报废记录
        /// </summary>
        /// <param name="doAssetsId"></param>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Assets_Do_ScrapApply)]
        public ActionResult DeleteAllScrapApplyRecord()
        {

            //
            // 删除资产领用记录主表
            //
            _assetsOptionService.DeleteAllAssetsScrapApply();

            return Back("删除成功");
        }

        /// <summary>
        /// 资产报废
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Assets_Do_Scrap)]
        public ActionResult ScrapAssets()
        {
            return View();
        }
        /// <summary>
        /// 资产报废设置
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Assets_Do_Scrap)]
        public ActionResult ScrapSet()
        {
            return View();
        }
        /// <summary>
        /// 资产报废批复
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Scrap_Reply)]
        public ActionResult ScrapReply()
        {
            var model = new ScrapReplyModel();
            return View(model);
        }

        /// <summary>
        /// 将参数绑定到图片
        /// </summary>
        /// <param name="bandPic"></param>
        /// <returns></returns>
        public ActionResult UpScrapReply(ScrapReplyModel bandPic)
        {
            try
            {
                //
                // 先通过图片信息判断图片是否已经绑定数据
                //
                var hasPic = _scrapService.GetAssetsScrapPhoto(bandPic.PicPath);

                if (hasPic == null)
                {
                    //
                    // 该图片没有绑定过数据则新增
                    //
                    var assetsScrapPhoto = new AssetsScrapPhoto()
                    {
                        type = bandPic.Type,
                        src = bandPic.PicPath,
                        srcd = bandPic.PicPath1,
                        //xuexiaomingcheng // 学校名称
                        txianshijishu = bandPic.XianshiJishu,
                        tzuigaoxianshifenbianlv = bandPic.ZuigaoFenbianlv,
                        tbiaochengguangliangdu = bandPic.BiaochengLiangdu,
                        tbiaochengduibidu = bandPic.BiaoChengDuibiDu,
                        fcpu = bandPic.F_CPU,
                        fzhuban = bandPic.F_Zhuban,
                        fyingpan = bandPic.F_Yingpan,
                        fneicun = bandPic.F_Neicun,
                        fxianka = bandPic.F_Xianka,
                        fguangqu = bandPic.F_Guanqu,
                        qzhuyaocanshu1 = bandPic.MainPara1,
                        qzhuyaocanshu2 = bandPic.MainPara2,
                        qzhuyaocanshu3 = bandPic.MainPara3,
                        qzhuyaocanshu4 = bandPic.MainPara4,
                        qzhuyaocanshu5 = bandPic.MainPara5,
                        qzhuyaocanshu6 = bandPic.MainPara6,
                        dcpu = bandPic.CPU,
                        dzhuban = bandPic.Zhuban,
                        dyingpan = bandPic.Yingpan,
                        dneicun = bandPic.Neicun,
                        dxianka = bandPic.Xianka,
                        dguangqu = bandPic.Guanqu,
                        dxianshiqi = bandPic.HasXianshiqi,
                        dxianshiqichicun = bandPic.XianshiqiChicun,
                        dxianshiqileixing = bandPic.XianshiqiLeixing,
                        djianpan = bandPic.HasKeybord,
                        dshubiao = bandPic.HasMouse
                    };

                    // 根据分类确定公共属性值
                    switch (bandPic.Type)
                    {
                        case 1:
                            assetsScrapPhoto.tianbiaoren = bandPic.D_TianBiaoren;
                            assetsScrapPhoto.xinxihuafuzeren = bandPic.D_Fuzeren;
                            assetsScrapPhoto.dianhua = bandPic.D_FuzerenDianhua;
                            assetsScrapPhoto.guidingshiyongnianxian = bandPic.D_GuidingShiyongNianxian;
                            break;
                        case 2:
                            assetsScrapPhoto.tianbiaoren = bandPic.F_TianBiaoren;
                            assetsScrapPhoto.xinxihuafuzeren = bandPic.F_Fuzeren;
                            assetsScrapPhoto.dianhua = bandPic.F_FuzerenDianhua;
                            assetsScrapPhoto.guidingshiyongnianxian = bandPic.F_GuidingShiyongNianxian;
                            break;
                        case 3:
                            assetsScrapPhoto.tianbiaoren = bandPic.T_TianBiaoren;
                            assetsScrapPhoto.xinxihuafuzeren = bandPic.T_Fuzeren;
                            assetsScrapPhoto.dianhua = bandPic.T_FuzerenDianhua;
                            assetsScrapPhoto.guidingshiyongnianxian = bandPic.T_GuidingShiyongNianxian;
                            break;
                        case 4:
                            assetsScrapPhoto.tianbiaoren = bandPic.Q_TianBiaoren;
                            assetsScrapPhoto.xinxihuafuzeren = bandPic.Q_Fuzeren;
                            assetsScrapPhoto.dianhua = bandPic.Q_FuzerenDianhua;
                            assetsScrapPhoto.guidingshiyongnianxian = bandPic.Q_GuidingShiyongNianxian;
                            break;
                        default:
                            break;
                    }

                    //
                    // 新增记录
                    //
                    _scrapService.AddAssetsScrapPhoto(assetsScrapPhoto);

                }
                else
                {
                    //
                    // 该图片已经绑定了数据则覆盖
                    //
                    //
                    // 该图片没有绑定过数据则新增
                    //
                    var assetsScrapPhoto = new AssetsScrapPhoto()
                    {
                        EntityId = hasPic.EntityId,
                        type = bandPic.Type,
                        src = bandPic.PicPath,
                        srcd = bandPic.PicPath1,
                        //xuexiaomingcheng // 学校名称
                        txianshijishu = bandPic.XianshiJishu,
                        tzuigaoxianshifenbianlv = bandPic.ZuigaoFenbianlv,
                        tbiaochengguangliangdu = bandPic.BiaochengLiangdu,
                        tbiaochengduibidu = bandPic.BiaoChengDuibiDu,
                        fcpu = bandPic.F_CPU,
                        fzhuban = bandPic.F_Zhuban,
                        fyingpan = bandPic.F_Yingpan,
                        fneicun = bandPic.F_Neicun,
                        fxianka = bandPic.F_Xianka,
                        fguangqu = bandPic.F_Guanqu,
                        qzhuyaocanshu1 = bandPic.MainPara1,
                        qzhuyaocanshu2 = bandPic.MainPara2,
                        qzhuyaocanshu3 = bandPic.MainPara3,
                        qzhuyaocanshu4 = bandPic.MainPara4,
                        qzhuyaocanshu5 = bandPic.MainPara5,
                        qzhuyaocanshu6 = bandPic.MainPara6,
                        dcpu = bandPic.CPU,
                        dzhuban = bandPic.Zhuban,
                        dyingpan = bandPic.Yingpan,
                        dneicun = bandPic.Neicun,
                        dxianka = bandPic.Xianka,
                        dguangqu = bandPic.Guanqu,
                        dxianshiqi = bandPic.HasXianshiqi,
                        dxianshiqichicun = bandPic.XianshiqiChicun,
                        dxianshiqileixing = bandPic.XianshiqiLeixing,
                        djianpan = bandPic.HasKeybord,
                        dshubiao = bandPic.HasMouse
                    };
                    switch (bandPic.Type)
                    {
                        case 1:
                            assetsScrapPhoto.tianbiaoren = bandPic.D_TianBiaoren;
                            assetsScrapPhoto.xinxihuafuzeren = bandPic.D_Fuzeren;
                            assetsScrapPhoto.dianhua = bandPic.D_FuzerenDianhua;
                            assetsScrapPhoto.guidingshiyongnianxian = bandPic.D_GuidingShiyongNianxian;
                            break;
                        case 2:
                            assetsScrapPhoto.tianbiaoren = bandPic.F_TianBiaoren;
                            assetsScrapPhoto.xinxihuafuzeren = bandPic.F_Fuzeren;
                            assetsScrapPhoto.dianhua = bandPic.F_FuzerenDianhua;
                            assetsScrapPhoto.guidingshiyongnianxian = bandPic.F_GuidingShiyongNianxian;
                            break;
                        case 3:
                            assetsScrapPhoto.tianbiaoren = bandPic.T_TianBiaoren;
                            assetsScrapPhoto.xinxihuafuzeren = bandPic.T_Fuzeren;
                            assetsScrapPhoto.dianhua = bandPic.T_FuzerenDianhua;
                            assetsScrapPhoto.guidingshiyongnianxian = bandPic.T_GuidingShiyongNianxian;
                            break;
                        case 4:
                            assetsScrapPhoto.tianbiaoren = bandPic.Q_TianBiaoren;
                            assetsScrapPhoto.xinxihuafuzeren = bandPic.Q_Fuzeren;
                            assetsScrapPhoto.dianhua = bandPic.Q_FuzerenDianhua;
                            assetsScrapPhoto.guidingshiyongnianxian = bandPic.Q_GuidingShiyongNianxian;
                            break;
                        default:
                            break;
                    }
                    //
                    // 覆盖记录
                    //
                    _scrapService.AddAssetsScrapPhoto(assetsScrapPhoto);
                }

                return Json("OK");
            }
            catch (Exception e)
            {
                return Json(e.ToString());
            }
        }

        /// <summary>
        /// 查找图片
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public ActionResult FindPhoto(string src)
        {

            var photo = _scrapService.GetAssetsScrapPhoto(src);
            if (photo != null)
            {
                var model = new ScrapReplyModel()
                {
                    //PicMd5
                    Type = photo.type,
                    PicPath = photo.src,
                    PicPath1 = photo.srcd,
                    D_TianBiaoren = photo.tianbiaoren,
                    D_Fuzeren = photo.xinxihuafuzeren,
                    D_FuzerenDianhua = photo.dianhua,
                    D_GuidingShiyongNianxian = photo.guidingshiyongnianxian,
                    F_TianBiaoren = photo.tianbiaoren,
                    F_Fuzeren = photo.xinxihuafuzeren,
                    F_FuzerenDianhua = photo.dianhua,
                    F_GuidingShiyongNianxian = photo.guidingshiyongnianxian,
                    T_TianBiaoren = photo.tianbiaoren,
                    T_Fuzeren = photo.xinxihuafuzeren,
                    T_FuzerenDianhua = photo.dianhua,
                    T_GuidingShiyongNianxian = photo.guidingshiyongnianxian,
                    Q_TianBiaoren = photo.tianbiaoren,
                    Q_Fuzeren = photo.xinxihuafuzeren,
                    Q_FuzerenDianhua = photo.dianhua,
                    Q_GuidingShiyongNianxian = photo.guidingshiyongnianxian,
                    MainPara1 = photo.qzhuyaocanshu1,
                    MainPara2 = photo.qzhuyaocanshu2,
                    MainPara3 = photo.qzhuyaocanshu3,
                    MainPara4 = photo.qzhuyaocanshu4,
                    MainPara5 = photo.qzhuyaocanshu5,
                    MainPara6 = photo.qzhuyaocanshu6,
                    CPU = photo.dcpu,
                    Zhuban = photo.dzhuban,
                    Yingpan = photo.dyingpan,
                    Neicun = photo.dneicun,
                    Xianka = photo.dxianka,
                    Guanqu = photo.dguangqu,
                    HasXianshiqi = photo.dxianshiqi,
                    XianshiqiChicun = photo.dxianshiqichicun,
                    XianshiqiLeixing = photo.dxianshiqileixing,
                    HasKeybord = photo.djianpan,
                    HasMouse = photo.dshubiao,
                    XianshiJishu = photo.txianshijishu,
                    ZuigaoFenbianlv = photo.tzuigaoxianshifenbianlv,
                    BiaochengLiangdu = photo.tbiaochengguangliangdu,
                    BiaoChengDuibiDu = photo.tbiaochengduibidu,
                    F_CPU = photo.fcpu,
                    F_Zhuban = photo.fzhuban,
                    F_Yingpan = photo.fyingpan,
                    F_Neicun = photo.fneicun,
                    F_Xianka = photo.fxianka,
                    F_Guanqu = photo.fguangqu,
                };
                return Json(model);
            }

            return Json("");
        }
        public ActionResult GetAssetsScarp(string catId)
        {
            var classcode = _classcodeService.Get(catId);
            return Json(classcode, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改资产年限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateAssetsScarp(string catId, int scrapYear)
        {
            if (catId.Length == 9)
            {
                var classcode = _classcodeService.Get(catId);
                classcode.SCRAPYEAR = scrapYear;
                _classcodeService.UpdateClassCode(classcode);


            }
            else
            {
                List<ClassCode> classcList = new List<ClassCode>();

                GetAllChildClassCode(catId, classcList);
                foreach (var item in classcList)
                {
                    item.SCRAPYEAR = scrapYear;
                    _classcodeService.UpdateClassCode(item);

                }

            }


            return Json("ok");



        }

        public void GetAllChildClassCode(string parentId, List<ClassCode> total)
        {


            List<ClassCode> classcList = _classcodeService.GetByParentCode(parentId);
            if (classcList != null && classcList.Count > 0)
            {
                total.AddRange(classcList);
                foreach (var item in classcList)
                {
                    GetAllChildClassCode(item.EntityId, total);
                }
            }


        }



        /// <summary>
        /// 资产报废
        /// </summary>
        /// <param name="model"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission(EnumBusinessPermission.Assets_Do_Return)]
        public ActionResult ScrapAssets(ScrapModel model, FormCollection form)
        {
            string err;
            if (!model.Validate(out err))
                return PageReturn(err);
            var scrapDto = new ScrapDto
            {
                ScrapId = model.ScrapId,
                ScrapExamineDate = model.ScrapExamineDate,
                ScrapExaminePerson = model.ScrapExaminePerson,
                ScrapMome = model.ScrapMome
            };
            foreach (var key in form.AllKeys)
            {
                if (key.StartsWith("_SCRAP_"))
                {
                    scrapDto.ScrapStatus.Add(key.Replace("_SCRAP_", ""), form[key]);
                }
            }
            try
            {
                _assetsOptionService.AssetsScrap(scrapDto);
                return PageReturn("报废成功");
            }
            catch (Exception ex)
            {
                return Json("报废失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 资产报废详情
        /// </summary>
        /// <param name="scrapId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ScrapDetail(int scrapId = 0) // $.post("/AssetsFind/DoAssets/BorrowDetail", { borrowId: borrowId }, function (result) 这个就是前台调用这个函数的写法
        // BorrowDetail函数名称对应，传递参数对应，result这个是函数返回结果，对应ActionResult，从函数体里面看出来是返回Json(dto);
        {
            var dto = _assetsOptionService.QueryScrap(scrapId); // 这个就是查询出资产记录的函数QueryBorrow，这个函数实现在_assetsOptionService这个对象里，这个对象在IAssetsOptionService类里面
            return Json(dto);
        }

        /// <summary>
        /// 资产打印
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryAssets()
        {
            ViewBag.DoAction = ConstTag.Query;
            return View();
        }

        /// <summary>
        /// 资产查询
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryAssetsEx()
        {
            ViewBag.DoAction = ConstTag.Query;
            return View();
        }

        /// <summary>
        /// 资产报表
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.Assets_Do_ExportData)]
        public ActionResult ExportData()
        {
            ViewBag.DoAction = ConstTag.ExportData;
            return View();
        }

        /// <summary>
        /// 查询分类合并后的结果
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MergeRecordQuery(int formNum)
        {
            var result = _scrapService.QueryMergePage(1, int.MaxValue, formNum);
            return Json(result);
        }

        /// <summary>
        /// 查询批复导出列表数据
        /// </summary>
        /// <param name="formNum"></param>
        /// <returns></returns>
        public ActionResult GetOut()
        {
            var result = _scrapService.QueryGetOut(1, int.MaxValue);
            return Json(result);
        }

        /// <summary>
        /// 将表单里的数据写入表格SCRAPMERGE
        /// </summary>
        /// <param name="bandPic"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ScrapReply(ScrapReplyModel bandPic)
        {
            try
            {
                //
                // 根据EntityId查询出要批复的资产，然后更改字段值
                //
                var scrapMerge = _scrapService.FindMergePage(bandPic.EntityId);
                scrapMerge.EntityId = bandPic.EntityId;
                scrapMerge.qzhuyaocanshu1 = bandPic.MainPara1;
                scrapMerge.qzhuyaocanshu2 = bandPic.MainPara2;
                scrapMerge.qzhuyaocanshu3 = bandPic.MainPara3;
                scrapMerge.qzhuyaocanshu4 = bandPic.MainPara4;
                scrapMerge.qzhuyaocanshu5 = bandPic.MainPara5;
                scrapMerge.qzhuyaocanshu6 = bandPic.MainPara6;
                // 报废理由 Reason
                scrapMerge.txianshijishu = bandPic.XianshiJishu;
                scrapMerge.tzuigaoxianshifenbianlv = bandPic.ZuigaoFenbianlv;
                scrapMerge.tbiaochengguangliangdu = bandPic.BiaochengLiangdu;
                scrapMerge.tbiaochengduibidu = bandPic.BiaoChengDuibiDu;
                scrapMerge.dcpu = bandPic.CPU;
                scrapMerge.dzhuban = bandPic.Zhuban;
                scrapMerge.dyingpan = bandPic.Yingpan;
                scrapMerge.dneicun = bandPic.Neicun;
                scrapMerge.dxianka = bandPic.Xianka;
                scrapMerge.dguangqu = bandPic.Guanqu;
                scrapMerge.dxianshiqi = bandPic.HasXianshiqi;
                scrapMerge.dxianshiqichicun = bandPic.XianshiqiChicun;
                scrapMerge.dxianshiqileixing = bandPic.XianshiqiLeixing;
                scrapMerge.djianpan = bandPic.HasKeybord;
                scrapMerge.dshubiao = bandPic.HasMouse;
                scrapMerge.fcpu = bandPic.F_CPU;
                scrapMerge.fzhuban = bandPic.F_Zhuban;
                scrapMerge.fyingpan = bandPic.F_Yingpan;
                scrapMerge.fneicun = bandPic.F_Neicun;
                scrapMerge.fxianka = bandPic.F_Xianka;
                scrapMerge.fguangqu = bandPic.F_Guanqu;
                scrapMerge.HasGetOut = 2;
                scrapMerge.Isdelet = 0;

                switch (bandPic.Type)
                {
                    case 1:
                        scrapMerge.DeadYears = bandPic.D_GuidingShiyongNianxian;
                        scrapMerge.People = bandPic.D_Fuzeren;
                        scrapMerge.Tel = bandPic.D_FuzerenDianhua;
                        scrapMerge.FillPerson = bandPic.D_TianBiaoren;
                        break;
                    case 2:
                        scrapMerge.DeadYears = bandPic.F_GuidingShiyongNianxian;
                        scrapMerge.People = bandPic.F_Fuzeren;
                        scrapMerge.Tel = bandPic.F_FuzerenDianhua;
                        scrapMerge.FillPerson = bandPic.F_TianBiaoren;
                        break;
                    case 3:
                        scrapMerge.DeadYears = bandPic.T_GuidingShiyongNianxian;
                        scrapMerge.People = bandPic.T_Fuzeren;
                        scrapMerge.Tel = bandPic.T_FuzerenDianhua;
                        scrapMerge.FillPerson = bandPic.T_TianBiaoren;
                        break;
                    case 4:
                        scrapMerge.DeadYears = bandPic.Q_GuidingShiyongNianxian;
                        scrapMerge.People = bandPic.Q_Fuzeren;
                        scrapMerge.Tel = bandPic.Q_FuzerenDianhua;
                        scrapMerge.FillPerson = bandPic.Q_TianBiaoren;
                        break;
                    default:
                        break;
                }

                //
                // 保存图片
                //
                var fileBase = Request.Files["picFile"];
                var saveFileName = "";
                if (fileBase != null && fileBase.ContentLength > 0)
                {
                    var fileName = string.Format("{0}_{1}{2}", bandPic.Type, DateTime.Now.ToString("yyyyMMddHHmmss"),
                        Path.GetExtension(fileBase.FileName));
                    saveFileName = Path.Combine(PortalSetting.UpLoadPicPath, fileName);
                    fileBase.SaveAs(saveFileName);
                    //bandPic.AssetsPicPath = fileName;
                }

                //
                // 把图片在服务器上的地址写入数据库
                //
                scrapMerge.PhotoSrc = saveFileName;

                //
                // 保存计算机类图片2
                //
                var fileBase1 = Request.Files["picFile1"];
                var saveFileName1 = "";
                if (fileBase1 != null && fileBase1.ContentLength > 0)
                {
                    var fileName1 = string.Format("{0}_{1}{2}", bandPic.Type, DateTime.Now.ToString("yyyyMMddHHmmss"),
                        Path.GetExtension(fileBase1.FileName));
                    saveFileName1 = Path.Combine(PortalSetting.UpLoadPicPath, fileName1);
                    fileBase1.SaveAs(saveFileName1);
                    //bandPic.AssetsPicPath = fileName;
                }

                //
                // 把图片在服务器上的地址写入数据库
                //
                scrapMerge.PhotoSrcD = saveFileName1;

                _scrapService.AddScrapMerge(scrapMerge);    // 覆盖：根据提交的表单数据，补全SCRAPMERGE表中对应EntityId所空缺的值
                var href = "/AssetsFind/DoAssets/ScrapReply";
                return Redirect(href);
            }
            catch (Exception e)
            {

                return Json("NO" + e);
            }


        }

        /// <summary>
        /// 导出一条信息化资产报废表
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public ActionResult ExportSingleScrapTable(string Entity = "0")
        {
            if (!string.IsNullOrWhiteSpace(Entity))
            {
                Entity = Entity.Trim('|');
                Entity = Entity.Replace('|', ',');
            }
            else {
                Entity = "0";
            }
            //
            // 根据表单号去查询数据并返回下载文件
            //
            string[] strSo = Entity.Split(',');

            var srcapItem = _scrapService.GetScrapMerge(int.Parse(strSo[0]));
            //
            // 导出该报废条目的信息化报废表
            //
            var scrapMergeExport = new ExportScrapMerge(_scrapService, srcapItem);
            scrapMergeExport.DoExport();

            for (int i = 1; i < strSo.Length; i++)
            {
                int idOne = int.Parse(strSo[i]);
                srcapItem = _scrapService.GetScrapMerge(int.Parse(strSo[i]));
                scrapMergeExport = new ExportScrapMerge(_scrapService, srcapItem);
                scrapMergeExport.DoExport();
            }


            //
            // 文件转存
            // 将文件缓存到服务器Temp文件夹
            // 然后再讲文件下载到客户端
            //
            string tempFile = Server.MapPath("~/Temp/");

            //
            // 设置临时文件名称
            //
            var savePath = Path.Combine(tempFile,
                string.Format("{0}_{1}_{2}", srcapItem.AssetsNum,DateTime.Now.ToString("yyMMdd"), scrapMergeExport.SaveFileName));

            //
            // 设置下载文件名称
            //
            var downLoadName = string.Format("{0}_{1}.xls", srcapItem.AssetsNum, DateTime.Now.ToString("yyMMdd"));

            //
            // 将临时文件保存到指定路径
            //
            
            scrapMergeExport.Save(savePath);

            for (int i = 0; i < strSo.Length; i++)
            {
                int idOne = int.Parse(strSo[i]);
                //
                // 根据EntityID更改导出状态为已导出
                //
                var aaa = _scrapService.FindMergePage(idOne);
                aaa.EntityId = idOne;
                aaa.HasGetOut = 1;
                _scrapService.AddScrapMerge(aaa);
            }
            //
            // 下载文件
            //
            //return File(savePath, "application/ms-excel", downLoadName);
            savePath = RelativePathHelper.MakeRelativePath(tempFile, savePath);
            savePath = "/Temp/" + savePath;
           return Content(savePath);
        }

        /// <summary>
        /// 根据EntityID查询到批复条目，然后将IS_DELET状态改为1（已删除）
        /// </summary>
        /// <param name="Entity"></param>
        public ActionResult DeletScrap(string Entity)
        {
            Entity = Entity.Trim('|');
            string[] strSo = Entity.Split('|');
            for (int i = 0; i < strSo.Length; i++)
            {
                int idOne = int.Parse(strSo[i]);
                var aaa = _scrapService.FindMergePage(idOne);
                aaa.EntityId = idOne;
                aaa.Isdelet = 1;
                _scrapService.AddScrapMerge(aaa);
            }
            return Json("OK");
        }

    }
}