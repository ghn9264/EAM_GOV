using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_RETURN")]
    [PrimaryKey("ID")]
    public class ReturnAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 借用人
        /// </summary>
        [Column("BORROW_PERSON")]
        public string BorrowPerson { get; set; }

        /// <summary>
        /// 归还人
        /// </summary>
        [Column("RETURN_PERSON")]
        public string ReturnPerson { get; set; }

        /// <summary>
        /// 归还日期
        /// </summary>
        [Column("RETURN_DATE")]
        public DateTime ReturnDate { get; set; }

        /// <summary>
        /// 归还人联系方式
        /// </summary>
        [Column("RETURN_PERSON_PHONE")]
        public string ReturnPersonPhone { get; set; }

        /// <summary>
        /// 归还人所在部门
        /// </summary>
        [Column("RETURN_DEPARTMENT")]
        public string ReturnDepartment { get; set; }

        /// <summary>
        /// 归还单编号
        /// </summary>
        [Column("RETURN_FORM_NO")]
        public string ReturnFormNo { get; set; }
    }
}
