using System.Collections.Specialized;
using System.Web.Routing;

namespace Eam.Web.Portal
{
    public class WebExceptionContext
    {
        public string Ip { get; set; }
        public string CurrentUrl { get; set; }
        public string RefUrl { get; set; }
        public bool IsAjaxRequest { get; set; }
        public NameValueCollection FormData { get; set; }
        public NameValueCollection QueryData { get; set; }
        public RouteValueDictionary RouteData { get; set; }
    }
}
