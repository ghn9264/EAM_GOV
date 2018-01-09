using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EAM.Data.Domain;
using EAM.Data.ImportAndExport.ImageImport;
using EAM.Data.ImportAndExport.Import;
using EAM.Data.Services;
using Eam.Web.Portal.Areas.SycData;
using Eam.Web.Portal._Comm;

namespace Eam.Web.Portal.Areas.AssetsInput.Controllers
{
    //[Permission(EnumBusinessPermission.Assets_Input_Batch)]   // liu 2016.5.24
    public class BatchInputController : EamAdminController
    {
        private IAssetsService _assetsService;
        public BatchInputController(IAssetsService assetsService)
        {
            _assetsService = assetsService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Permission(EnumBusinessPermission.SycData_Syc_BanXue)]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return View(new ImageImportResult { ErrorMessage = "请选择导入文件！" });
            }
            var fileName = string.Format("Image_{0}{1}",
                DateTime.Now.ToString("yyyyMMddHHmmss"), Path.GetExtension(file.FileName));
            var saveFileName = Path.Combine(PortalSetting.UpLoadTempPath, fileName);
            try
            {
                file.SaveAs(saveFileName);
            }
            catch (Exception ex)
            {
                return View(new ImageImportResult { ErrorMessage = "上传文件异常！" + ex });
            }
            try
            {
                var bxImport = new ImageBatchImport(_assetsService);
                var reuslt = bxImport.DoImport(saveFileName, PortalSetting.UpLoadPicPath);
                Console.WriteLine(reuslt);
            }

            catch (Exception ex)
            {
                return View(new ImportResultModel {ErrorMessage = "导入异常！" + ex});
            }
            return View();
        }
    }
}