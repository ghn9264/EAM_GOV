using System;
using System.Collections.Generic;
using Eam.Web.Portal._Comm;

namespace Eam.Web.Portal
{
    public class UserSession
    {
        public UserSession()
        {
            this.UserId = "Anonymous";
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public string UserDept { get; set; }
        public List<EnumBusinessPermission> BusinessPermission { get; set; } 
    }
}
