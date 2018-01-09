using System.Xml.Serialization;

namespace Eam.Core.Config
{
    public class CacheConfigItem : ConfigNodeBase
    {
        [XmlAttribute(AttributeName = "keyRegex")]
        public string KeyRegex { get; set; }

        [XmlAttribute(AttributeName = "moduleRegex")]
        public string ModuleRegex { get; set; }

        [XmlAttribute(AttributeName = "providerName")]
        public string ProviderName { get; set; }

        [XmlAttribute(AttributeName = "minitus")]
        public int Minitus { get; set; }

        [XmlAttribute(AttributeName = "priority")]
        public int Priority { get; set; }

        [XmlAttribute(AttributeName = "isAbsoluteExpiration")]
        public bool IsAbsoluteExpiration { get; set; }

        [XmlAttribute(AttributeName = "desc")]
        public string Desc { get; set; }
    }
}