namespace Eam.Web.Portal.Areas.AssetsInput.Models
{
    public class CulturalrelicModel
    {
        public int EntityId { get; set; }

        //资产编号
        public string AssetsNum { get; set; }

        //文物等级
        public string GoodsLevel { get; set; }

        //藏品年代
        public string Years { get; set; }

        //来源地
        public string SourcePlacce { get; set; }
    }
}