using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eam.Web.Portal.Areas.AssetsFind.Models
{
    public class ScrapModel
    {
        public int ScrapId { get; set; }

        /// <summary>
        /// 报废审核人
        /// </summary>
        public string ScrapExaminePerson { get; set; }

        /// <summary>
        /// 报废审核时间
        /// </summary>
        public DateTime ScrapExamineDate { get; set; }

        public string ScrapMome { get; set; }

        public bool Validate(out string message)
        {
            message = "";
            if (ScrapId <= 0)
            {
                message = "请选择一条报废记录";
                return false;
            }
            if (string.IsNullOrEmpty(ScrapExaminePerson))
            {
                message = "请填写报废审核人";
                return false;
            }
            return true;
        }
    }
}