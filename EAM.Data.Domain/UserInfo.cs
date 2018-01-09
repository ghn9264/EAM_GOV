using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("USER_INFO")]
    [PrimaryKey("ID")]
    public class UserInfo : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        [Column("USER_ID")]
        public string UserId { get; set; }

        [Column("USER_NAME")]
        public string UserName { get; set; }

        [Column("DEPT_ID")]
        public int DeptId { get; set; }

        //[ResultColumn("DEPT_NAME")]
        //public string DeptName { get; set; }

        [Column("TEL")]
        public string Tel { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("ROLE")]
        public string Role { get; set; }
        
        [Column("DEPARTMENT")]
        public string DepartMent { get; set; }


        /// <summary>
        /// 用户权限
        /// </summary>
        [Column("PERMISSIONS")]
        public string Permissions { get; set; }
    }
}