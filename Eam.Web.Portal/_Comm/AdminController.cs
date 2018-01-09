using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Eam.Core.Cache;
using Eam.Core.Utility;
using EAM.Data.Domain;
using Eam.Web.Portal.Areas.Account.Models;
using Eam.Web.Portal._Comm;
using EAM.Data.Comm;

namespace Eam.Web.Portal
{
    public class EamAdminController : EamBaseController
    {
        public string UserId
        {
            get
            {
                if (Request.IsAuthenticated)
                    return Request.RequestContext.HttpContext.User.Identity.Name;
                return "";
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var noAuthorizeAttributes =
                filterContext.ActionDescriptor.GetCustomAttributes(typeof (AuthorizeIgnoreAttribute), false);
            if (noAuthorizeAttributes.Length > 0)
                return;
            base.OnActionExecuting(filterContext);
            if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = RedirectToAction("Index", "Auth", new { area = "Account" });
                return;
            }
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                filterContext.Result = RedirectToAction("Index", "Auth", new { area = "Account" });
                return;
            }
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var userSession = (new JavaScriptSerializer()).Deserialize<UserSession>(ticket.UserData);
            if (userSession == null)
            {
                filterContext.Result = RedirectToAction("Index", "Auth", new { area = "Account" });
                return;
            }

            ViewBag.UserSession = userSession;
            var permissionAttributes = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof (PermissionAttribute),false).Cast<PermissionAttribute>();
            permissionAttributes =filterContext.ActionDescriptor.GetCustomAttributes(typeof (PermissionAttribute), false)
                    .Cast<PermissionAttribute>()
                    .Union(permissionAttributes);
            var attributes = permissionAttributes as IList<PermissionAttribute> ?? permissionAttributes.ToList();
            bool hasPermission = true;
            if (attributes.Any())
            {
                hasPermission = true;
                foreach (var attr in attributes)
                {
                    foreach (var permission in attr.Permissions)
                    {
                        if (!userSession.BusinessPermission.Contains(permission))
                        {
                            hasPermission = false;
                            break;
                        }
                    }
                }

                if (!hasPermission)
                {
                    if (Request.UrlReferrer != null)
                        filterContext.Result = this.Stop("没有权限！", Request.UrlReferrer.AbsoluteUri);
                    else
                        filterContext.Result = Content("没有权限！");
                }
            }
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            // 记录错误日志
            ErrorLog.Write(filterContext);
        }
    }
}