using System.Collections.Generic;
using EAM.Data.Domain;

namespace EAM.Data.Services.Dto
{
    public class InventoryDto
    {
        public InventoryAttribute InventoryInfo { get; set; }  // 这个就是资产借出概要信息，BorrowAttribute就对应assets_borrow表，所以你刚才在前台看到的BorrowInfo就是从这里来的，带你重新捋一遍
        public List<InventoryDetailAttribute> Details { get; set; } // 这个就是资产借出详情表里面的资产项，BorrowDetailAttribute就对应assets_borrow_Detail表
    }
}