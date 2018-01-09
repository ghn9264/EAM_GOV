using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eam.Web.Portal.Areas.AssetsFind.Models
{
    public class ScrapApplyModel
    {
        /// <summary>
        /// 报废申请人
        /// </summary>
        [Required(ErrorMessage = "报废申请人不能为空")]
        [StringLength(20)]
        public string ScrapPerson { get; set; }

        /// <summary>
        /// 报废申请单位
        /// </summary>
        [Required(ErrorMessage = "报废申请单位不能为空")]
        [StringLength(20)]
        public string ScrapUnit { get; set; }

        /// <summary>
        /// 报废日期
        /// </summary>
        [Required]
        public DateTime ScrapDate { get; set; }

        /// <summary>
        /// 报废申请人联系方式
        /// </summary>
        [Required(ErrorMessage = "报废申请人联系方式")]
        [StringLength(20)]
        public string ScrapPersonPhone { get; set; }

        public List<string> AssetsNum { get; set; }

    }
}