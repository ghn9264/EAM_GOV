using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eam.Web.Portal.Areas.AssetsFind.Models
{
    public class ServicesModel
    {
        public int RepairId { get; set; }
        public string ServicePerson { get; set; }
        public DateTime ServiceDate { get; set; }
        public string ServicePersonPhone { get; set; }
        public string ServicesMemo { get; set; }

        public bool Validate(out string message)
        {
            message = "";
            if (RepairId <= 0)
            {
                message = "请选择一条借出记录";
                return false;
            }
            if (string.IsNullOrEmpty(ServicePerson))
            {
                message = "请填写维修人";
                return false;
            }
            return true;
        }
    }
}