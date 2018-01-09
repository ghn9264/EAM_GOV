using System;

namespace Eam.Core.Config
{
    [Serializable]
    public class CacheConfig : ConfigFileBase
    {
        public CacheConfig()
        {
        }

        public CacheConfigItem[] CacheConfigItems { get; set; }
        public CacheProviderItem[] CacheProviderItems { get; set; }
    }
}
