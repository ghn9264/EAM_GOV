using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ROLE")]
    [PrimaryKey("ID")]
    public class Role : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        [Column("ROLE")]
        public string Roles { get; set; }
        /// <summary>
        /// 用户权限
        /// </summary>
        [Column("PERMISSIONS")]
        public string Permissions { get; set; }
    }
}