using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eam.Web.Portal.Areas.AssetsFind.Models
{
    public class ChangeInfoModel
    {

        /// <summary>
        /// 修改项目
        /// </summary>
        public string ChangeItem { get; set; }

        /// <summary>
        /// 修改内容
        /// </summary>
        public string ChangeContent { get; set; }

        public bool Validate(out string message)
        {
            message = "";

            if (string.IsNullOrEmpty(ChangeContent))
            {
                message = "修改内容为空";
                return false;
            }
            return true;
        }
    }
}