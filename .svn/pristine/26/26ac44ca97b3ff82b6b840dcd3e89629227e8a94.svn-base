using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eam.Web.Portal.Areas.AssetsFind.Models
{
    public class InventoryModel
    {
        /// <summary>
        /// 盘点人
        /// </summary>
        [Required(ErrorMessage = "盘点人不能为空")]
        [StringLength(20)]
        public string InventoryOperationPerson { get; set; }

        /// <summary>
        /// 盘点时间
        /// </summary>
        [Required]
        public DateTime InventoryOperationDate { get; set; }

        /// <summary>
        /// 盘点部门
        /// </summary>
        [Required(ErrorMessage = "盘点部门不能为空")]
        [StringLength(20)]
        public string InventoryOperationDepartment { get; set; }


        public int InventoryId { get; set; }


        public bool Validate(out string message)
        {
            message = "";
            if (InventoryId <= 0)
            {
                message = "请选择一条盘点计划";
                return false;
            }
            return true;
        }

    }
}