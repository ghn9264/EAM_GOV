namespace Eam.Web.Portal.Areas.AssetsInput.Models
{
    public class BorrowDetailModel
    {
        public int EntityId { get; set; }

        //借用单编号
        public string BorrowFormNo { get; set; }

        //设备编号
        public string AssetsNo { get; set; }

        //计量单位
        public string MesurementUnit { get; set; }

        //借用数量
        public string Counts { get; set; }

        //设备状态
        public string AssetsSate { get; set; }
    }
}