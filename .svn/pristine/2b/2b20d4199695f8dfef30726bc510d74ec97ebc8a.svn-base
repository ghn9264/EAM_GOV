using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EAM.Data.Domain;
using EAM.Data.ImportAndExport.AssetsDiff;
using EAM.Data.ImportAndExport.Export;
using EAM.Data.ImportAndExport.Import;
using EAM.Data.Services;
using Eam.Web.Portal.Areas.QrPrint.Models;
using Eam.Web.Portal._Comm;

namespace Eam.Web.Portal.Areas.QrPrint.Controllers
{
    public class QrPrintController : EamAdminController
    {
        private IAssetsService _assetsService;
        private IImportHistoryService _importHistoryService;

        public QrPrintController(IAssetsService assetsService,
            IImportHistoryService importHistoryService)
        {
            _assetsService = assetsService;
            _importHistoryService = importHistoryService;
        }

     

        public ActionResult Index()
        {
            return View(new QrPrintModel { });
        }
        public ActionResult QrPrintSetting()
        {
            return View(new QrPrintModel { });

        }



       

    }
}