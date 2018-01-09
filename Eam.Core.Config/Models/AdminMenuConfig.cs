using System;

namespace Eam.Core.Config
{
    [Serializable]
    public class AdminMenuConfig : ConfigFileBase
    {
        public AdminMenuConfig()
        {
        }

        public AdminMenuGroup[] AdminMenuGroups { get; set; }
    }
}
