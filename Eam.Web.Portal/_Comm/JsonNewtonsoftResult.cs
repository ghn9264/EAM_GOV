using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Eam.Web.Portal._Comm
{
    public class JsonNewtonsoftResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if ((this.JsonRequestBehavior == JsonRequestBehavior.DenyGet) 
                && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("JsonRequest_GetNotAllowed");
            }
            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }
            if (Data != null)
            {
                 IsoDateTimeConverter timeFormat = new IsoDateTimeConverter {DateTimeFormat = "yyyy-MM-dd HH:mm:ss"};
                JsonSerializerSettings jsetting = new JsonSerializerSettings();
                jsetting.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include;
                jsetting.Converters.Add(timeFormat);

                response.Write(JsonConvert.SerializeObject(Data, Formatting.Indented, jsetting));
            }
        }
    }
}