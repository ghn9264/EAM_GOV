using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eam.Web.Portal._Comm;

namespace Eam.Web.Portal.Areas.SysManage.Models
{
    public class PlaceTreeNode : TreeNodeBase<int>
    {
        public string code { get; set; }
        public string pCode { get; set; }

        /// <summary>
        /// 界面显示用name拼接了code的，用这个字段存储真实的名称
        /// </summary>
        public string realName { get; set; }
    }
}