using System;
using System.Collections.Generic;
using EAM.Data.Domain;

namespace EAM.Data.Services.Dto
{
    public class InventoryOperatorDto
    {
        public InventoryOperatorDto()
        {
            AssetsInventoryResult = new Dictionary<string, string>();
        }

        /// <summary>
        /// 资产盘点计划表序号
        /// </summary>
        public int InventoryId { get; set; }

        /// <summary>
        /// 资产实际盘点人
        /// </summary>
        public string InventoryOperatePerson { get; set; }

        /// <summary>
        /// 资产实际盘点时间
        /// </summary>
        public DateTime InventoryOperateDate { get; set; }

        /// <summary>
        /// 资产实际盘点部门
        /// </summary>
        public string InventoryOperateDepartment { get; set; }

        /// <summary>
        /// 资产盘点结果
        /// </summary>
        public Dictionary<string, string> AssetsInventoryResult { get; set; }  // 这个盘点结果应该是一个列表
    }
}