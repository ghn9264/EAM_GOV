using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAM.Data.Domain;


namespace Eam.Web.Portal.Areas.QrPrint.Models
{
    public class QrPrintModel
    {
        public QrPrintModel()
        {
            LastDynamic = new ImportHistory();
            LastBanxue = new ImportHistory();
          
        }

        public ImportHistory LastBanxue { get; set; }
        public ImportHistory LastDynamic { get; set; }

     

        public string ErrMessage { get; set; }
    }
}