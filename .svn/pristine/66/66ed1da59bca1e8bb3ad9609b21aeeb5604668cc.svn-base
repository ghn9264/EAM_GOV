using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAM.Data.Domain;
using EAM.Data.ImportAndExport;

namespace Eam.Web.Portal.Areas.SycData
{
    public class ImportResultModel
    {
        public ImportResultModel()
        {
            ImportedItems = new List<AssetsMain>();
            UnImportedItems = new List<AssetsMainMsg>();
        }

        public List<AssetsMain> ImportedItems { get; set; }
        public List<AssetsMainMsg> UnImportedItems { get; set; }
        public string ErrorMessage { get; set; }
        public HttpPostedFileBase MyFile { get; set; }
    }
}