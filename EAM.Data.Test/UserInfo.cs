using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Test
{
    [TableName("USER_INFO")]
    [PrimaryKey("ID")]
    public class UserInfo : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        [Column("USERNAME")]
        public string UserName { get; set; }

        [Column("CREATEDTIME")]
        public DateTime CreatedTime { get; set; }
    }
}