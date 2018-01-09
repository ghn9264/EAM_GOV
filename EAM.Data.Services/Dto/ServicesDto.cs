using System;
using System.Collections.Generic;
using EAM.Data.Domain;

namespace EAM.Data.Services.Dto
{
    public class ServicesDto
    {
        public ServicesDto()
        {
            ServicesResult = new Dictionary<string, string>();
        }

        /// <summary>
        /// 资产保修单编号
        /// </summary>
        public int RepairId { get; set; }

        /// <summary>
        /// 资产维修人
        /// </summary>
        public string ServicePerson { get; set; }

        /// <summary>
        /// 资产维修日期
        /// </summary>
        public DateTime ServiceDate { get; set; }

        /// <summary>
        /// 资产维修人联系方式
        /// </summary>
        public string ServicePersonPhone { get; set; }
        public string ServicesMemo { get; set; }  
        /// <summary>
        /// 资产维修结果
        /// </summary>
        public Dictionary<string, string> ServicesResult { get; set; }
    }
}