using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eam.Web.Portal.Areas.AssetsFind.Models
{
    public class SendBackModel
    {
        public int BorrowId { get; set; }
        public string SendBackPerson { get; set; }
        public DateTime SendBackDate { get; set; }
        public string SendBackMemo { get; set; }

        public bool Validate(out string message)
        {
            message = "";
            if (BorrowId <= 0)
            {
                message = "请选择一条借出记录";
                return false;
            }
            if (string.IsNullOrEmpty(SendBackPerson))
            {
                message = "请填写退还人";
                return false;
            }
            return true;
        }
    }
}