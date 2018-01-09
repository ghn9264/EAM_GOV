using System.Collections.Generic;
using System.Linq;
using EAM.Data.Domain;
using EAM.Data.ImportAndExport.Export;
using EAM.Data.ImportAndExport.Export.ExportAssets;
using EAM.Data.ImportAndExport.Export.ExportDoAssets;
using EAM.Data.Services;
using System;
using System.IO;
using System.Web.Mvc;
using EAM.Data.Services.Query;
using EAM.Inventory;
using Eam.Web.Portal._Comm;
using EAM.Data.ImportAndExport.Import;

namespace Eam.Web.Portal.Areas.AssetsFind.Controllers
{
    public class DoAssetsExportController : EamAdminController
    {
        private IAssetsOptionService _assetsOptService;
        private IAssetsService _assetsService;
        private static string _gFilePath;

        public DoAssetsExportController(IAssetsOptionService assetsOptionService,IAssetsService assetsService)
        {
            _assetsOptService = assetsOptionService;
            _assetsService = assetsService;
        }

        /// <summary>
        /// 资产借出记录导出
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportBorrow(int doAssetsId)
        {
            try
            {
                var borrowExport = new ExportDoAssetsBorrow(_assetsOptService, doAssetsId);
                borrowExport.DoExport();
                var tempFile = Server.MapPath("~/Temp/");
                var savePath = Path.Combine(tempFile, string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss")));
                borrowExport.Save(savePath);
                return File(savePath, "application/ms-excel", borrowExport.SaveFileName);
            }
            catch (Exception ex)
            {
                return Back(ex.Message);
            }
        }

        /// <summary>
        /// 资产归还记录导出
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportReturn(int doAssetsId)
        {
            try
            {
                var returnExport = new ExportDoAssetsReturn(_assetsOptService, doAssetsId);
                returnExport.DoExport();
                var tempFile = Server.MapPath("~/Temp/");
                var savePath = Path.Combine(tempFile, string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss")));
                returnExport.Save(savePath);
                return File(savePath, "application/ms-excel", returnExport.SaveFileName);
            }
            catch (Exception ex)
            {
                return Back(ex.Message);
            }
        }

        /// <summary>
        /// 资产退还记录导出
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportSendBack(int doAssetsId)
        {
            try
            {
                var returnExport = new ExportDoAssetsSendBack(_assetsOptService, doAssetsId);
                returnExport.DoExport();
                var tempFile = Server.MapPath("~/Temp/");
                var savePath = Path.Combine(tempFile, string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss")));
                returnExport.Save(savePath);
                return File(savePath, "application/ms-excel", returnExport.SaveFileName);
            }
            catch (Exception ex)
            {
                return Back(ex.Message);
            }
        }

        /// <summary>
        /// 资产领用记录导出
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportAcquire(int doAssetsId)
        {
            try
            {
                var returnExport = new ExportDoAssetsAcquire(_assetsOptService, doAssetsId);
                returnExport.DoExport();
                var tempFile = Server.MapPath("~/Temp/");
                var savePath = Path.Combine(tempFile, string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss")));
                returnExport.Save(savePath);
                return File(savePath, "application/ms-excel", returnExport.SaveFileName);
            }
            catch (Exception ex)
            {
                return Back(ex.Message);
            }
        }

        /// <summary>
        /// 资产报修记录导出
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportRepair(int doAssetsId)
        {
            try
            {
                var returnExport = new ExportDoAssetsRepair(_assetsOptService, doAssetsId);
                returnExport.DoExport();
                var tempFile = Server.MapPath("~/Temp/");
                var savePath = Path.Combine(tempFile, string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss")));
                returnExport.Save(savePath);
                return File(savePath, "application/ms-excel", returnExport.SaveFileName);
            }
            catch (Exception ex)
            {
                return Back(ex.Message);
            }
        }

        /// <summary>
        /// 资产维修记录导出
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportService(int doAssetsId)
        {
            try
            {
                var returnExport = new ExportDoAssetsService(_assetsOptService, doAssetsId);
                returnExport.DoExport();
                var tempFile = Server.MapPath("~/Temp/");
                var savePath = Path.Combine(tempFile, string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss")));
                returnExport.Save(savePath);
                return File(savePath, "application/ms-excel", returnExport.SaveFileName);
            }
            catch (Exception ex)
            {
                return Back(ex.Message);
            }
        }

        /// <summary>
        /// 资产报修记录导出
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportInventory(int doAssetsId)
        {
            try
            {
                var returnExport = new ExportDoAssetsInventory(_assetsOptService, doAssetsId);
                returnExport.DoExport();
                var tempFile = Server.MapPath("~/Temp/");
                var savePath = Path.Combine(tempFile, string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss")));
                returnExport.Save(savePath);
                return File(savePath, "application/ms-excel", returnExport.SaveFileName);
            }
            catch (Exception ex)
            {
                return Back(ex.Message);
            }
        }


        /// <summary>
        /// 下载资产盘点计划文件
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadInventoryPlan(int doAssetsId)
        {
            try
            {
                var inventoryInfo = _assetsOptService.QueryInventory(doAssetsId);
                var list = new List<InventoryInfo>();
                foreach (var detail in inventoryInfo.Details)
                {
                    list.Add(new InventoryInfo
                    {
                        DetailId = detail.EntityId,
                        GoodsNo = detail.AssetsNo,
                        GoodsFiscalNo = detail.UsedNum1 ?? "",
                        GoodsModel = detail.MeasurementUnits,
                        GoodsName = detail.GoodsName,
                        GoodsStatusName = detail.UsingState,
                        GoodsNum = 0,
                        GoodsStatus = 0,
                        Remark = inventoryInfo.InventoryInfo.InventoryPerson
                    });
                }
                var tempFile = Server.MapPath("~/Temp/");
                var savePath = Path.Combine(tempFile, string.Format("Inventory_{0}.db", DateTime.Now.ToString("yyMMddHHmmss")));
                var h = new InventoryHelper(savePath);
                h.TestConnection();
                h.InsertInventory(list);
                return File(savePath, "application/octet-stream", savePath);
            }
            catch (Exception ex)
            {
                return Back(ex.Message);
            }
        }

        /// <summary>
        /// 下载资产报废计划文件
        /// 信息化
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadScrapPlanInfo(int doAssetsId)
        {
            try
            {
                var returnExport = new ExportDoAssetsScrapInfo(_assetsOptService, doAssetsId, PortalSetting.QrCodeTitle);
                returnExport.DoExport();
                var tempFile = Server.MapPath("~/Temp/");
                var savePath = Path.Combine(tempFile, string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss")));
                returnExport.Save(savePath);
                return File(savePath, "application/ms-excel", returnExport.SaveFileName);
            }
            catch (Exception ex)
            {
                return Back(ex.Message);
            }
        }

        /// <summary>
        /// 下载资产报废计划文件
        /// 信息化
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadScrapPlanBack(int doAssetsId)
        {
            try
            {
                var returnExport = new ExportDoAssetsScrapBack(_assetsOptService, doAssetsId, PortalSetting.QrCodeTitle);
                returnExport.DoExport();
                var tempFile = Server.MapPath("~/Temp/");
                var savePath = Path.Combine(tempFile, string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss")));
                returnExport.Save(savePath);
                return File(savePath, "application/ms-excel", returnExport.SaveFileName);
            }
            catch (Exception ex)
            {
                return Back(ex.Message);
            }
        }
        public static IDictionary<Guid, ProgressInfo> ImportTasks = new Dictionary<Guid, ProgressInfo>();

        //
        // 导出当前资产
        //
        //[HttpPost]
        [Permission(EnumBusinessPermission.Assets_Statistics_Query)]
        public ActionResult ExportCurrentAsset(AssetsQuery model)
        {
            //
            // 创建导出进度条信息
            //
            ProgressInfo progressInfo = new ProgressInfo();
            progressInfo.ImportedPercentVal = 2;
            var taskId = Guid.NewGuid();
            ImportTasks.Add(taskId, progressInfo);

            //
            // 查询当前资产
            //
            var assetsList = QueryCurrentAeests(model);
           
                progressInfo.ImportedPercentVal = 3;
                try
                {
                    //
                    // 导出资产
                    //
                    var landExport = new ExportCurrentAssets(_assetsService, assetsList);
                    landExport.DoExport(ref progressInfo);

                    //
                    // 文件转存
                    // 将文件缓存到服务器Temp文件夹
                    // 然后再讲文件下载到客户端
                    //
                    string tempFile = Server.MapPath("~/Temp/");
                    var savePath = Path.Combine(tempFile,
                        string.Format("当前资产_{0}.xls", DateTime.Now.ToString("yyMMddHHmmss")));
                    _gFilePath = savePath;
                    landExport.Save(savePath);
                    return File(savePath, "application/ms-excel", landExport.SaveFileName);
                    //return savePath;
                
                }
                catch (Exception ex)
                {
                    
                    return Back(ex.Message);
                }
                   
 
           
          
        }

        //
        // 进度查询
        //
        public JsonResult QueryProgress(/*Guid taskId*/)
        {
            //return Json(ImportTasks.Keys.Contains(taskId) ? ImportTasks[taskId] : new ProgressInfo());
            //ProgressInfo aa = ImportTasks[ImportTasks.Keys.Last()];
            //
            // 返回进度信息
            // 前台轮询
            //
            return Json(ImportTasks.Keys.Count > 0 ? ImportTasks[ImportTasks.Keys.Last()] : new ProgressInfo());

        }

        //
        // 下载当前资产
        //
        [Permission(EnumBusinessPermission.Assets_Statistics_Query)]
        public ActionResult DownLoadCurrentAsset(AssetsQuery model)
        {
          
                return File(_gFilePath, "application/ms-excel", "当前资产.xls");
            
           
        }

        /// <summary>
        /// 当前资产查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<AssetsMain> QueryCurrentAeests(AssetsQuery model)
        {
            //
            // 创建三个LIST用于存放资产代码、部门和存放地点树内容
            // 用于创建SQL
            //
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
                //model.PlaceList.Add(model.IndexPlace);

                //
                // 添加目标检索项目的子项
                //
                placeList = _assetsService.GetPlace(model.IndexPlace);

                if (placeList != null)
                {
                    if (placeList.Count >= 1)
                    {
                        foreach (var placeItem in placeList)
                        {
                            model.PlaceList.Add(placeItem.PlaceName);
                        }
                    }
                }
            }

            return _assetsService.QueryAsstesList(model);
        }
        /// <summary>
        /// 当前资产查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<AssetsMain> QueryCurrentAssetsWithProgress(AssetsQuery model,ref ProgressInfo progressInfo)
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
                //model.PlaceList.Add(model.IndexPlace);

                //
                // 添加目标检索项目的子项
                //
                placeList = _assetsService.GetPlace(model.IndexPlace);

                if (placeList != null)
                {
                    if (placeList.Count >= 1)
                    {
                        foreach (var placeItem in placeList)
                        {
                            model.PlaceList.Add(placeItem.PlaceName);
                        }
                    }
                }
            }

            return _assetsService.QueryAsstesList(model);
        }
    }
}