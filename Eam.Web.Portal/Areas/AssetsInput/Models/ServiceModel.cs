using System;

namespace Eam.Web.Portal.Areas.AssetsInput.Models
{
    public class ServiceModel
    {
        public int EntityId { get; set; }

        //维修人
        public string ServicePerson { get; set; }

        //维修人联系方式
        public string ServicePersonPhone { get; set; }

        //维修时间
        public DateTime ServiceDate { get; set; }

        //维修单编号
        public string ServiceFormNo { get; set; }
    }
}