using System;

namespace Eam.Web.Portal.Areas.AssetsInput.Models
{
    public class ReturnModel
    {
        public int EntityId { get; set; }

        //借用人
        public string BorrowPerson { get; set; }

        //归还人
        public string ReturnPerson { get; set; }

        //归还日期
        public DateTime ReturnDate { get; set; }

        //归还人联系方式
        public string ReturnPersonPhone { get; set; }

        //归还人所在部门
        public string ReturnDepartment { get; set; }

        //归还单编号
        public string ReturnFormNo { get; set; }
    }
}