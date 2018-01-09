namespace EAM.Inventory
{
    public class InventoryInfo
    {
        public int Id { get; set; }
        public string GoodsNo { get; set; }
        public string GoodsFiscalNo { get; set; }
        public string GoodsName { get; set; }
        public string GoodsModel { get; set; }
        public int GoodsStatus { get; set; }
        public string GoodsStatusName { get; set; }
        public int GoodsNum { get; set; }
        public int DetailId { get; set; }
        public string Remark { get; set; }
    }
}