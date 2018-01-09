using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EAM.Data.Domain;
using EAM.Data.ImportAndExport.Export.ExportLedger;
using EAM.Data.Services;
using EAM.Data.Services.Query;
using Eam.Web.Portal._Comm;
using System.Web.Script.Serialization;

namespace Eam.Web.Portal.Areas.AssetsFind.Controllers
{
    /// <summary>
    /// 资产台账
    /// </summary>
    public class StatisticsController : EamAdminController
    {
        private readonly IAssetsService _assetsService; 
        private readonly IAssetsOptionService _assetsOptionService;

        public StatisticsController(IAssetsOptionService assetsOptionService,IAssetsService assetsService)
        {
            _assetsOptionService = assetsOptionService;
            _assetsService = assetsService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Ledger");
        }

        [Permission(EnumBusinessPermission.Assets_Statistics_Ledger)]
        public ActionResult Ledger()
        {
            return View(new LedgerQuery());
        }

        [Permission(EnumBusinessPermission.Assets_Statistics_Ledger)]
        [HttpPost]
        public ActionResult Ledger(LedGerCachQuery model, LedgerQuery query)
        {
            //
            // 先从资产库中筛选资产放入缓存库
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

              _assetsService.QueryAsstesToCach(model);

            //
            // 写入到缓冲库
            //
            //_assetsService.WriteToCach(List<AssetsMain> result);

             var result = _assetsOptionService.LedgerData(query);
 


            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(new { result = result, query = query }),
                ContentType = "application/json"
            };
        }

        [Permission(EnumBusinessPermission.Assets_Statistics_Ledger)]
        [HttpPost]
        public ActionResult LedgerExport(LedgerQuery query)
        {
          
       
        {
          
            try
                    {
                       // Request.QueryString

                        var ledgerExport = new ExportLedger(_assetsOptionService);
                        //ledgerExport.DoExport(query);
                        ledgerExport.DoExport(query);
                       // LedgerQuery
                        string tempFile = Server.MapPath("~/Temp/");
                        var savePath = Path.Combine(tempFile, string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss")+ "台账"));
                        ledgerExport.Save(savePath);
                        return File(savePath, "application/ms-excel", ledgerExport.SaveFileName);
                    }
                    catch (Exception ex)
                    {
                        return Back(ex.Message);
                    }
             
                }






          //  return Content(result.ToString());
        }
    }
}