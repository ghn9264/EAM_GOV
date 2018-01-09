namespace Eam.Web.Portal.Areas.AssetsInput.Models
{
    public class ServiceDetailModel
    {
        public int EntityId { get; set; }

        //维修单编号
        public string ServiceFormNo { get; set; }

        //资产编号
        public string AssetsNo { get; set; }

        //维修结果
        public string ServiceResult { get; set; }
    }
}