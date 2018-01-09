using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_AQUAIR")]
    [PrimaryKey("ID")]
    public class AquairAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 领用人
        /// </summary>
        [Column("ACQUIRE_PERSON")]
        public string AcquirePerson { get; set; }

        /// <summary>
        /// 领用日期
        /// </summary>
        [Column("ACQUIRE_DATE")]
        public DateTime AcquireDate { get; set; }

        /// <summary>
        /// 领用人联系方式
        /// </summary>
        [Column("ACQUIRE_PERSON_PHONE")]
        public string AcquirePersonPhone { get; set; }

        /// <summary>
        /// 领用人所在部门
        /// </summary>
        [Column("ACQUIRE_DEPARTMENT")]
        public string AcquireDepartment { get; set; }

        /// <summary>
        /// 领用单编号
        /// </summary>
        [Column("ACQUIRE_FORM_NO")]
        public string AcquireFormNo { get; set; }

        // <summary>
        /// 是否退还
        /// </summary>
        [Column("HAS_SENDBACK")]
        public int HasSendBack { get; set; }

        /// <summary>
        /// 退还人
        /// </summary>
        [Column("SENDBACK_PERSON")]
        public string SendBackPerson { get; set; }

        /// <summary>
        /// 退还时间
        /// </summary>
        [Column("SENDBACK_DATE")]
        public DateTime SendBackDate { get; set; }

        /// <summary>
        /// 退还备注
        /// </summary>
        [Column("SENDBACK_MEMO")]
        public string SendBackMemo { get; set; }

        [ResultColumn]
        public List<string> AssetsNums { get; set; }
    }
}