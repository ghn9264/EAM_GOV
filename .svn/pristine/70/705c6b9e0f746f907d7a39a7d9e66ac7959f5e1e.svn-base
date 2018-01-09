using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eam.Web.Portal.Areas.SysManage.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int UserDeptId { get; set; }
        public string Tel { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }

        public bool Validate(out string error)
        {
            error = "";
            if (string.IsNullOrEmpty((UserId)))
            {
                error = "用户ID不能为空";
                return false;
            }
            if (string.IsNullOrEmpty((UserName)))
            {
                error = "真实姓名不能为空";
                return false;
            }
            if (string.IsNullOrEmpty((UserName)))
            {
                error = "真实姓名不能为空";
                return false;
            }
            if (UserDeptId <= 0)
            {
                error = "请选择部门！";
                return false;
            }
            if (string.IsNullOrEmpty((Password)))
            {
                error = "登陆密码不能为空";
                return false;
            }
            if (Password!=RePassword)
            {
                error = "两次输入密码不一致";
                return false;
            }
            return true;
        }

    }
}