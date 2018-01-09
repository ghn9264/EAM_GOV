using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAM.Data.ImportAndExport.AssetsDiff
{
    public class DiffItem
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 资产编号
        /// </summary>
        public string AssetsNum { get; set; }
        /// <summary>
        /// 资产名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 本库状态
        /// </summary>
        public bool LocalStatus { get; set; }
        public string LocalStatusEx { get; set; }
        /// <summary>
        /// 办学库状态
        /// </summary>
        public bool BanxueStatus { get; set; }

        public string BanxueStatusEx { get; set; }
        /// <summary>
        /// 动态库状态
        /// </summary>
        public bool DynamicStatus { get; set; }
        public string DynamicStatusEx { get; set; }
    }
}
