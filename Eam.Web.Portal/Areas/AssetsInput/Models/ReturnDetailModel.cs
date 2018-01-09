namespace Eam.Web.Portal.Areas.AssetsInput.Models
{
    public class ReturnDetailModel
    {
        public int EntityId { get; set; }

        //归还单编号
        public string ReturnFormNo { get; set; }

        //设备编号
        public string AssetsNo { get; set; }

        //计量单位
        public string MesurementUnit { get; set; }

        //归还数量
        public int Counts { get; set; }

        //设备状态
        public string AssetsState { get; set; }
    }
}