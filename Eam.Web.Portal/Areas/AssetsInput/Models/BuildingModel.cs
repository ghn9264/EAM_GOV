namespace Eam.Web.Portal.Areas.AssetsInput.Models
{
    public class BuildingModel
    {
        public int EntityId { get; set; }

        //资产编号
        public string AssetsNum { get; set; }

        //产权形势
        public string PropertyForm { get; set; }

        //权属证号
        public string OwnershipCertifiateNum { get; set; }

        //构筑物面积
        public decimal Area { get; set; }

        //坐落位置
        public string Location { get; set; }
    }
}