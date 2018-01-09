using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eam.Web.Portal.Areas.AssetsFind.Models
{
    public class RepairModel
    {
        /// <summary>
        /// 报修人
        /// </summary>
        [Required(ErrorMessage = "借用人不能为空")]
        [StringLength(20)]
        public string RepairPerson { get; set; }

        /// <summary>
        /// 报修部门
        /// </summary>
        [Required(ErrorMessage = "借用人所在部门不能为空")]
        [StringLength(20)]
        public string RepairDepartment { get; set; }

        /// <summary>
        /// 报修人联系方式
        /// </summary>
        [Required(ErrorMessage = "借用人联系方式不能为空")]
        [StringLength(20)]
        public string RepairPersonPhone { get; set; }

        /// <summary>
        /// 报修日期
        /// </summary>
        [Required]
        public DateTime RepairDate { get; set; }

        public List<string> AssetsNum { get; set; }

    }
}