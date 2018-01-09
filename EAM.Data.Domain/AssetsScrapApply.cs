using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_SCRAP_APPLY")]
    [PrimaryKey("ID")]
    public class AssetsScrapApply : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 报废申请人
        /// </summary>
        [Column("SCRAP_PERSON")]
        public string ScrapPerson { get; set; }

        /// <summary>
        /// 报废申请单位
        /// </summary>
        [Column("SCRAP_UNIT")]
        public string ScrapUnit { get; set; }

        /// <summary>
        /// 报废日期
        /// </summary>
        [Column("SCRAP_DATE")]
        public DateTime ScrapDate { get; set; }

        /// <summary>
        /// 报废申请人联系方式
        /// </summary>
        [Column("SCRAP_PERSON_PHONE")]
        public string ScrapPersonPhone { get; set; }

        /// <summary>
        /// 报废状态
        /// </summary>
        [Column("HAS_SCRAP")]
        public int HasScrap { get; set; }

        /// <summary>
        /// 报废审核人
        /// </summary>
        [Column("SCRAP_EXAMINE_PERSON")]
        public string ScrapExaminePerson { get; set; }

        /// <summary>
        /// 报废审核时间
        /// </summary>
        [Column("SCRAP_EXAMINE_DATE")]
        public DateTime ScrapExamineDate { get; set; }

        /// <summary>
        /// 报废备注
        /// </summary>
        [Column("SCRAP_MOME")]
        public string ScrapMome { get; set; }

        [ResultColumn]
        public List<string> AssetsNums { get; set; }
    }
}