using System.Text;
using System.Web.Mvc;
using Eam.Web.Portal._Comm;

namespace Eam.Web.Portal
{
    public class EamBaseController : Controller
    {

        #region override JsonResult
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return new JsonNewtonsoftResult { Data = data, ContentType = contentType, ContentEncoding = contentEncoding };
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNewtonsoftResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        } 
        #endregion

        public virtual UserSession UserSession { get; private set; }

        protected ContentResult JsonP(string callback, object data)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            return this.Content(string.Format("{0}({1})", callback, json));
        }        

        /// <summary>
        ///  警告并且历史返回
        /// </summary>
        /// <param name="notice"></param>
        /// <returns></returns>
        public ContentResult Back(string notice)
        {
            var content = new StringBuilder("<script>");
            if (!string.IsNullOrEmpty(notice))
                content.AppendFormat("alert('{0}');", notice);
            content.Append("history.go(-1)</script>");
            return this.Content(content.ToString());
        }

        public ContentResult PageReturn(string msg, string url = null)
        {
            var content = new StringBuilder("<script type='text/javascript'>");
            if (!string.IsNullOrEmpty(msg))
                content.AppendFormat("alert('{0}');", msg);
            if (string.IsNullOrWhiteSpace(url))
                url = Request.Url.ToString();
            content.Append("window.location.href='" + url + "'</script>");
            return this.Content(content.ToString());
        }
        public ContentResult PageOpenNew(string msg, string url = null)
        {
            var content = new StringBuilder("<script type='text/javascript'>");
            if (!string.IsNullOrEmpty(msg))
                content.AppendFormat("alert('{0}');", msg);
            if (string.IsNullOrWhiteSpace(url))
                url = Request.Url.ToString();
            content.AppendFormat("window.open('{0}');", url);
            content.Append("</script>");
            return this.Content(content.ToString());
        }
        /// <summary>
        /// 转向到一个提示页面，然后自动返回指定的页面
        /// </summary>
        /// <param name="notice"></param>
        /// <param name="redirect"></param>
        /// <returns></returns>
        public ContentResult Stop(string notice, string redirect, bool isAlert = false)
        {
            var content = "<meta http-equiv='refresh' content='1;url=" + redirect +
                          "' /><body style='margin-top:0px;color:red;font-size:24px;'>" + notice + "</body>";
            if (isAlert)
                content = string.Format("<script>alert('{0}'); window.location.href='{1}'</script>", notice, redirect);

            return this.Content(content);
        }
        
    }
}