﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eam.Web.Portal.Areas.AssetsFind.Models
{
    public class InventoryPlanModel
    {
        /// <summary>
        /// 盘点人
        /// </summary>
        [Required(ErrorMessage = "盘点人不能为空")]
        [StringLength(20)]
        public string InventoryPerson { get; set; }

        /// <summary>
        /// 盘点时间
        /// </summary>
        [Required]
        public DateTime InventoryDate { get; set; }

        /// <summary>
        /// 盘点部门
        /// </summary>
        [Required(ErrorMessage = "盘点部门不能为空")]
        [StringLength(20)]
        public string InventoryDepartment { get; set; }

        public List<string> AssetsNum { get; set; }

        /// <summary>
        /// 操作部门
        /// </summary>
        [Required(ErrorMessage = "操作部门不能为空")]
        [StringLength(20)]
        public string InventoryOperationDepartment { get; set; }

    }
}