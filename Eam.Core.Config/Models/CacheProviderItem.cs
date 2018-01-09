using System.Xml.Serialization;

namespace Eam.Core.Config
{
    public class CacheProviderItem : ConfigNodeBase
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name {get;set;}
        [XmlAttribute(AttributeName = "type")]
        public string Type {get;set;}
        [XmlAttribute(AttributeName = "desc")]
        public string Desc { get; set; }
    }
}