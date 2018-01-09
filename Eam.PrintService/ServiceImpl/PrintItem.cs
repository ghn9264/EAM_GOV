using System.Runtime.Serialization;

namespace Eam.PrintService
{
    [DataContract]
    public class PrintItem
    {
        [DataMember]
        public string AssetsNum { get; set; }

        [DataMember]
        public string QrPath { get; set; }
    }

    [DataContract]
    public class ResultBase
    {
        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}