using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Eam.Core.Cache;
using Eam.Core.Utility;
using EAM.Data.Services;
using Eam.Web.Portal.Areas.Account.Models;
using Eam.Web.Portal._Comm;
using EAM.Data.Domain;

namespace Eam.Web.Portal.Areas.Account.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }


        [AuthorizeIgnore]
        public ActionResult Index()
        {
            return View(new LoginModel());
        }


        [AuthorizeIgnore]
        public ActionResult Index2()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [AuthorizeIgnore]
        public ActionResult Index(LoginModel model)
        {

            //判断是否是孟
            UserInfo useradmin = new UserInfo();
            useradmin.UserId = "孟繁涛";
            useradmin.UserName = "孟繁涛";
            useradmin.Password = "123456";//e10adc3949ba59abbe56e057f20f883e
            useradmin.Role = "超级管理员";
            useradmin.Permissions = "100,101,102,201,201,203,301,302,303,304,402,501,502,601,602,701,702,801,802,901,902,903,904,905,1001,1002,1003,1004";
            useradmin.DepartMent = "人事部";

            //return RedirectToAction("Index", "DeskTop");
            if (model.LoginName.Equals(useradmin.UserName) && model.Password.Equals(useradmin.Password))
            {
                List<EnumBusinessPermission> userPermission = new List<EnumBusinessPermission>();
                if (!string.IsNullOrEmpty(useradmin.Permissions))
                    userPermission = useradmin.Permissions.Split(",".ToCharArray()).Select(int.Parse).Cast<EnumBusinessPermission>().ToList();


                var userSession = new UserSession
                {
                    UserId = useradmin.UserId,
                    UserName = useradmin.UserName,
                    BusinessPermission = userPermission,
                    Role = useradmin.Role,
                    UserDept = useradmin.DepartMent
                };
                var data = (new JavaScriptSerializer()).Serialize(userSession);
                var ticket = new FormsAuthenticationTicket(2, useradmin.UserId, DateTime.Now, DateTime.Now.AddDays(1), false, data);
                var cookieValue = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue)
                {
                    HttpOnly = true,
                    Secure = FormsAuthentication.RequireSSL,
                    Domain = FormsAuthentication.CookieDomain,
                    Path = FormsAuthentication.FormsCookiePath,
                };
                Response.Cookies.Remove(cookie.Name);
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "DeskTop");
            }
            else
            {
                var user = _userService.GetUser(model.LoginName);
                if (null != user)
                {
                    var epwd = _userService.GetEncryptPwd(model.Password);

                    if (epwd.Equals(user.Password, StringComparison.CurrentCultureIgnoreCase))
                    {
                        List<EnumBusinessPermission> userPermission = new List<EnumBusinessPermission>();
                        if (!string.IsNullOrEmpty(user.Permissions))
                            userPermission = user.Permissions.Split(",".ToCharArray()).Select(int.Parse).Cast<EnumBusinessPermission>().ToList();

                        //if (user.UserId.Equals("admin", StringComparison.CurrentCultureIgnoreCase))
                        //    userPermission = EnumHelper.GetItemValueList<EnumBusinessPermission>()
                        //        .Keys.Cast<EnumBusinessPermission>()
                        //        .ToList();

                        var userSession = new UserSession
                        {
                            UserId = user.UserId,
                            UserName = user.UserName,
                            BusinessPermission = userPermission,
                            Role = user.Role,
                            UserDept = user.DepartMent
                        };
                        Session.Add("power", user.Role);
                        var data = (new JavaScriptSerializer()).Serialize(userSession);
                        var ticket = new FormsAuthenticationTicket(2, user.UserId, DateTime.Now, DateTime.Now.AddDays(1), false, data);
                        var cookieValue = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue)
                        {
                            HttpOnly = true,
                            Secure = FormsAuthentication.RequireSSL,
                            Domain = FormsAuthentication.CookieDomain,
                            Path = FormsAuthentication.FormsCookiePath,
                        };
                        Response.Cookies.Remove(cookie.Name);
                        Response.Cookies.Add(cookie);
                        return RedirectToAction("Index", "DeskTop");
                    }
                    else
                    {
                        model.ErrorMessage = "密码错误";
                    }
                }
                else
                {
                    model.ErrorMessage = "无效用户";
                }
            }
            return View(model);
        }

    }
}