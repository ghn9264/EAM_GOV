using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eam.Web.Portal.Areas.AssetsFind.Models
{
    public class PrintModel
    {

        public int EntityId { get; set; }


        public string Printset { get; set; }

        public string PrintTitleX { get; set; }

        public string PrintTitleY { get; set; }

        public string PrintcodeX { get; set; }

        public string PrintcodeY { get; set; }

        public string LableName { get; set; }

        public string LableContact { get; set; }

        public string PrintFontSize { get; set; }
    }
}