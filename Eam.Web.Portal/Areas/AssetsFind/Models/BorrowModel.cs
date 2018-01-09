using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eam.Web.Portal.Areas.AssetsFind.Models
{
    public class BorrowModel
    {
        /// <summary>
        /// 借用人
        /// </summary>
        [Required(ErrorMessage = "借用人不能为空")]
        [StringLength(20)]
        public string BorrowPerson { get; set; }

        /// <summary>
        /// 借用日期
        /// </summary>
        [Required]
        public DateTime BorrowDate { get; set; }

        /// <summary>
        /// 借用用途
        /// </summary>
        public string BorrowUse { get; set; }

        /// <summary>
        /// 预计归还日期
        /// </summary>
        [Required]
        public DateTime BorrowLong { get; set; }

        /// <summary>
        /// 借用人联系方式
        /// </summary>
        [Required(ErrorMessage = "借用人联系方式不能为空")]
        [StringLength(20)]
        public string BorrowPersonPhone { get; set; }

        /// <summary>
        /// 借用人所在部门
        /// </summary>
        [Required(ErrorMessage = "借用人所在部门不能为空")]
        [StringLength(20)]
        public string BorrowDepartment { get; set; }

        public List<string> AssetsNum { get; set; }


    }
}