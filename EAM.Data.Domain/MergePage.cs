using System.Collections.Generic;
using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Services.Dto
{
    public class MergePage
    {
        /// <summary>
        /// 计算机类型数据
        /// </summary>
        public PagedList<ScrapMerge> ComputerPagedList { get; set; }

        /// <summary>
        /// 服务器类型数据
        /// </summary>
        public PagedList<ScrapMerge> ServicePagedList { get; set; }

        /// <summary>
        /// 投影仪类型数据
        /// </summary>
        public PagedList<ScrapMerge> TouYingPagedList { get; set; }


        /// <summary>
        /// 其它类型数据
        /// </summary>
        public PagedList<ScrapMerge> OtherPagedList { get; set; }

        /// <summary>
        /// 批复导出数据
        /// </summary>
        public PagedList<ScrapMerge> GetOutPagedList { get; set; }
    }
}