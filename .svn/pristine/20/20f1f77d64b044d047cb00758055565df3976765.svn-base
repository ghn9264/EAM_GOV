using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAM.Data.Domain;
using EAM.Data.ImportAndExport.AssetsDiff;

namespace Eam.Web.Portal.Areas.SycData.Models
{
    public class DiffModel
    {
        public DiffModel()
        {
            LastDynamic = new ImportHistory();
            LastBanxue = new ImportHistory();
            DataDiffs = new List<DiffItem>();
        }

        public ImportHistory LastBanxue { get; set; }
        public ImportHistory LastDynamic { get; set; }
        public List<DiffItem> DataDiffs { get; set; }

        public string ErrMessage { get; set; }
    }
}