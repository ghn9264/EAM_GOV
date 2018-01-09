using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eam.Web.Portal.Areas.AssetsFind.Models
{
    public class ReturnModel
    {
        public int BorrowId { get; set; }
        public string ReturnPerson { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ReturnMome { get; set; }

        public bool Validate(out string message)
        {
            message = "";
            if (BorrowId <= 0)
            {
                message = "请选择一条借出记录";
                return false;
            }
            if (string.IsNullOrEmpty(ReturnPerson))
            {
                message = "请填写归还人";
                return false;
            }
            return true;
        }
    }
}