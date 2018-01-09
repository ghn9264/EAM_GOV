using System;
using System.Collections.Generic;
using System.Linq;

namespace Eam.Web.Portal._Comm
{
    public class PermissionAttribute  : Attribute
    {
        public List<EnumBusinessPermission> Permissions { get; set; }

        public PermissionAttribute(params EnumBusinessPermission[] parameters)
        {
            Permissions = parameters.ToList();
        }
    }
}