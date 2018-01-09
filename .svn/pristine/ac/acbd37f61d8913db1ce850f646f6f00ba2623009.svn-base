using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eam.Web.Portal.Areas.AssetsFind.Models
{
    public class AcquireModel
    {
        /// <summary>
        /// 领用人
        /// </summary>
        [Required(ErrorMessage = "领用人不能为空")]
        [StringLength(20)]
        public string AcquirePerson { get; set; }

        /// <summary>
        /// 领用日期
        /// </summary>
        [Required]
        public DateTime AcquireDate { get; set; }

        /// <summary>
        /// 领用人联系方式
        /// </summary>
        [Required(ErrorMessage = "领用人联系方式不能为空")]
        [StringLength(20)]
        public string AcquirePersonPhone { get; set; }

        /// <summary>
        /// 领用人所在部门
        /// </summary>
        [Required(ErrorMessage = "领用人所在部门不能为空")]
        [StringLength(20)]
        public string AcquireDepartment { get; set; }

        public List<string> AssetsNum { get; set; }



    }
}