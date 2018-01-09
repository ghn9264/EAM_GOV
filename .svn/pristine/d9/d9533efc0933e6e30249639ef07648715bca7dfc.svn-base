using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_BORROW")]
    [PrimaryKey("ID")]
    public class AssetsBorrrow : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 借用人
        /// </summary>
        [Column("BORROW_PERSON")]
        public string BorrowPerson { get; set; }

        /// <summary>
        /// 借用日期
        /// </summary>
        [Column("BORROW_DATE")]
        public DateTime BorrowDate { get; set; }

        /// <summary>
        /// 借用用途
        /// </summary>
        [Column("BORROW_USE")]
        public string BorrowUse { get; set; }

        /// <summary>
        /// 预计归还日期
        /// </summary>
        [Column("BORROW_LONG")]
        public DateTime BorrowLong { get; set; }

        /// <summary>
        /// 借用人联系方式
        /// </summary>
        [Column("BORROW_PERSON_PHONE")]
        public string BorrowPersonPhone { get; set; }

        /// <summary>
        /// 借用人所在部门
        /// </summary>
        [Column("BORROW_DEPARTMENT")]
        public string BorrowDepartment { get; set; }

        /// <summary>
        /// 借用单编号
        /// </summary>
        [Column("BORROW_FORM_NO")]
        public string BorrowFormNo { get; set; }
    }
}