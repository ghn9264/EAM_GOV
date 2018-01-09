﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eam.Core.Utility;
using EAM.Data.Domain;
using EAM.Data.Services;
using EAM.Data.Services.Impl;
using EAM.Data.Services.Query;
using Eam.Web.Portal.Areas.Account.Models;
using Eam.Web.Portal.Areas.SysManage.Models;
using Eam.Web.Portal._Comm;
using System.IO;
using System.Configuration;
using EAM.Data.Comm;

namespace Eam.Web.Portal.Areas.SysManage.Controllers
{
    public class SysManageController : EamAdminController
    {
        private readonly ISystemService _sysService;
        private readonly IUserService _userService;
        private readonly IAssetsService _assetsService;
        private readonly IRoleService _roleService;


        public SysManageController(ISystemService sysService,
            IUserService userService, IAssetsService assetsService, IRoleService roleService)
        {
            _sysService = sysService;
            _userService = userService;
            _assetsService = assetsService;
            _roleService = roleService;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 综合管理
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.SysManage_Manage_Interface)]
        public ActionResult InterfaceManage()
        {
            return View();
        }

        /// <summary>
        /// 部门人员
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.SysManage_Manage_UserAndDepartmen)]
        public ActionResult UserAndDepartmenManage()
        {
            var businessPermissionList = EnumHelper.GetItemValueList<EnumBusinessPermission>();
            this.ViewBag.BusinessPermissionList = new SelectList(businessPermissionList, "Key", "Value");
            ViewBag.NewUser = 50;
            ViewBag.NewDepart = 80;
            return View();
        }


        //TODO 修改数据库
        string dataBaseName = ConfigurationManager.AppSettings["DataBaseName"];
        /// <summary>
        /// 数据库备份
        /// </summary>
        /// <returns></returns>
        //TODO 记得加上权限
        public FileResult BackUpDataBase()
        {

            DateTime now = DateTime.Now;
            string tempFile = Server.MapPath("~/Temp/");
            var savePath = Path.Combine(tempFile, string.Format("{0}.sql", now.ToString("yyMMddHHmmss")));
            string cmdStr = @"/c mysqldump   " + dataBaseName + " > " + savePath;

            System.Diagnostics.Process.Start("cmd", cmdStr).WaitForExit();

            return File(savePath, "application/sql", Server.UrlEncode(now.ToString("yyMMddHHmmss") + "backup.sql"));
        }
        /// <summary>
        /// 数据库恢复
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        //TODO 记得加上权限
        public JsonResult Recovery(HttpPostedFileBase file)
        {
            JsonResult result = new JsonResult();
            if (file == null)
            {
                result.Data = new { type = 1, msg = "请选择导入文件！" };
                return result;
            }
            var fileName = string.Format("动态_{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Path.GetExtension(file.FileName));
            var saveFileName = Path.Combine(PortalSetting.UpLoadTempPath, fileName);
            file.SaveAs(saveFileName);

            string cmdStr = @"/c mysql " + dataBaseName + " < " + saveFileName;
            System.Diagnostics.Process.Start("cmd", cmdStr).WaitForExit();
            result.Data = new { type = 0 };
            return result;

        }
        /// <summary>
        /// 重置数据库 reset.sql 文件中需要写入需要清除的表
        /// </summary>
        /// <returns></returns>
        //TODO 记得加上权限
        public JsonResult Reset()
        {
            //JsonResult result = new JsonResult();
            //try
            //{
            //    var saveFileName = Path.Combine(PortalSetting.UpLoadTempPath, "reset.sql");

            //    string cmdStr = @"/c mysql " + dataBaseName + " < " + saveFileName;
            //    System.Diagnostics.Process.Start("cmd", cmdStr).WaitForExit();
            //    result.Data = new { type = 0 };
            //}
            //catch (Exception ex) {
            //    result.Data = new { type = 1, msg = ex.ToString() };
            //}
            //return result;

            //
            // 通过写sql进行清空
            //
            JsonResult result = new JsonResult();
            try
            {
                //
                // 调用资产清空服务
                //
                _assetsService.ClearAllData();
                AssetsMain zero = new AssetsMain();
                zero.EntityId = 1;
                _assetsService.AddAssets(zero);
                result.Data = new { type = 0 };
            }
            catch (Exception ex)
            {
                result.Data = new { type = 1, msg = ex.ToString() };
            }
            return result;
        }


        #region 部门管理
        private void BuildNode(List<Department> source, DeptTreeNode root, int parent)
        {
            var items = source.Where(c => c.ParentId == parent).ToList();
            root.isParent = items.Count > 0;
            foreach (var item in items)
            {
                var node = new DeptTreeNode
                {
                    name = item.DeptName,
                    id = item.EntityId,
                    pId = parent,
                    pName = item.ParentName
                };
                root.children.Add(node);
                source.Remove(item);
                BuildNode(source, node, item.EntityId);
            }
        }

        public ActionResult ListDept()
        {
            var list = _sysService.GetAll(); ;
            var root = new DeptTreeNode();
            BuildNode(list, root, 0);
            return Json(root.children, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 权限管理 2017-05-27 wnn
        /// </summary>
        /// <param name="source"></param>
        /// <param name="root"></param>
        /// <param name="parent"></param>
        [Permission(EnumBusinessPermission.SysManage_Manage_UserAndDepartmen)]
        private void BuildRoleNode(List<Role> source, RoleTreeNode root, int parent)
        {
            var items = source.ToList();
            root.isParent = items.Count > 0;
            foreach (var item in items)
            {
                var node = new RoleTreeNode
                {
                    pName = item.Permissions,
                    name = item.Roles,
                    id = item.EntityId

                };
                root.children.Add(node);
                source.Remove(item);
                //BuildRoleNode(source, node, item.EntityId);
            }
        }

        /// <summary>
        /// 权限列表 2017-05-27 wnn
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.SysManage_Manage_UserAndDepartmen)]
        public ActionResult ListRole()
        {
            var list = _roleService.GetAll();
            var root = new RoleTreeNode();
            BuildRoleNode(list, root, 0);
            return Json(root.children, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveDept(DeptModel deptModel)
        {
            try
            {
                if (string.IsNullOrEmpty(deptModel.DeptName))
                    return Content("部门名称不能为空且不超过20字");
                var dept = new Department
                {
                    ParentId = deptModel.DeptParentId,
                    DeptName = deptModel.DeptName,
                    EntityId = deptModel.DeptId
                };
                if (dept.EntityId > 0)
                    _sysService.Update(dept);
                else
                    _sysService.AddDept(dept);
                return Content("ok");
            }
            catch (Exception ex)
            {
                return Content("操作失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 保存角色 2017-05-31 wnn
        /// </summary>
        /// <param name="deptModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission(EnumBusinessPermission.SysManage_Manage_UserAndDepartmen)]
        public ActionResult SaveRole(int? EntityId, string Roles, string Permissions)
        {
            Permissions = Permissions.Trim().Trim(',');
            try
            {
                if (string.IsNullOrEmpty(Roles))
                    return Content("角色名称不能为空且不超过20字");
                var role = new Role
                {
                    Roles = Roles,
                    Permissions = Permissions,
                    EntityId = EntityId.Value
                };
                if (role.EntityId > 0)
                {
                    _roleService.UpdateRole(role);
                    _userService.UpdateRole(role.Roles, role.Permissions);
                }
                else
                    _roleService.AddRole(role);
                return Content("ok");
            }
            catch (Exception ex)
            {
                return Content("操作失败：" + ex.Message);
            }
        }
        ///// <summary>
        ///// 修改权限 2017-05-31 wnn
        ///// </summary>
        ///// <param name="deptModel"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult SaveRoleUp(int EntityId, string Permissions)
        //{
        //    Permissions = Permissions.Trim().Trim(',');
        //    try
        //    {
        //        var role = new Role
        //        {
        //            Permissions = Permissions,
        //            EntityId = EntityId
        //        };
        //        if (role.EntityId > 0)
        //            _roleService.UpdateRole(role);
        //        else
        //            _roleService.AddRole(role);
        //        return Content("ok");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content("操作失败：" + ex.Message);
        //    }
        //}

        [HttpPost]
        public ActionResult DeleteDept(int deptId)
        {
            try
            {
                _sysService.Delete(deptId);
                return Content("ok");
            }
            catch (Exception ex)
            {
                return Content("操作失败：" + ex.Message);
            }
        }

        #endregion

        #region 用户管理

        public ActionResult CreateUser()
        {
            var businessPermissionList = EnumHelper.GetItemValueList<EnumBusinessPermission>();
            ViewBag.BusinessPermissionList = new SelectList(businessPermissionList, "Key", "Value");
            return View("EditUser", new UserInfo());
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateUser(FormCollection collection)
        {
            var businessPermissionList = EnumHelper.GetItemValueList<EnumBusinessPermission>();
            ViewBag.BusinessPermissionList = new SelectList(businessPermissionList, "Key", "Value");
            var model = new UserInfo();
            TryUpdateModel<UserInfo>(model);
            model.DepartMent = model.DepartMent;// 2017-05-26 wnn
            string error;
            if (!Validate(model, out error))
            {
                ModelState.AddModelError("*", error);
                return View("EditUser", model);
            }
            if (string.IsNullOrEmpty((model.Password)))
            {
                ModelState.AddModelError("*", "登陆密码不能为空");
                return View("EditUser", model);
            }
            model.Password = _userService.GetEncryptPwd(model.Password);
            model.Permissions = collection["BusinessPermissionList"];
            _userService.AddUser(model);
            return base.PageReturn("操作成功", "/SysManage/SysManage/UserAndDepartmenManage");
        }

        public ActionResult EditUser(int id = 0)
        {
            var model = _userService.GetUser(id);
            //model.DeptName = model.DepartMent;//注释 2017-05-26 wnn
            var businessPermissionList = EnumHelper.GetItemValueList<EnumBusinessPermission>();
            ViewBag.BusinessPermissionList = new SelectList(businessPermissionList, "Key", "Value", model.Permissions);
            return View(model);
        }

        private bool Validate(UserInfo model, out string error)
        {
            error = "";
            if (string.IsNullOrEmpty((model.UserId)))
            {
                error = "用户ID不能为空";
                return false;
            }
            if (string.IsNullOrEmpty((model.UserName)))
            {
                error = "真实姓名不能为空";
                return false;
            }
            if (string.IsNullOrEmpty((model.Tel)))
            {
                error = "联系方式不能为空";
                return false;
            }
            if (model.DeptId <= 0)
            {
                //error = "请选择部门！";
                //return false;
            }
            return true;
        }

        [HttpPost]
        public ActionResult EditUser(int id, UserInfo u, FormCollection collection)
        {
            var editPwd = !string.IsNullOrEmpty(u.Password);
            var model = _userService.GetUser(id);
            var oldPwd = model.Password;
            TryUpdateModel<UserInfo>(model);
            //model.DepartMent = model.DeptName; // 注释 2017-05-26 wnn
            model.Password = editPwd ? _userService.GetEncryptPwd(model.Password) : oldPwd;
            model.Permissions = collection["BusinessPermissionList"];
            _userService.UpdateUser(model);
            return base.PageReturn("操作成功", "/SysManage/SysManage/UserAndDepartmenManage");
        }

        [HttpPost]
        public ActionResult GetAllUsers(int pageIndex = 1)
        {
            var query = new UserQuery { PageIndex = pageIndex, PageSize = 10 };
            var pagedUser = _userService.GetAll(query);
            return Json(pagedUser, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetAllUsersMax()
        {
            var query = new UserQuery { PageIndex = 1, PageSize = int.MaxValue };
            var pagedUser = _userService.GetAll(query).Items.Select(p => new { id = p.UserName, text = p.UserName });

            return Json(pagedUser, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveUser(UserModel model)
        {
            try
            {
                string err;
                if (!model.Validate(out err))
                    return Content(err);

                var dept = new UserInfo
                {
                    EntityId = model.Id,
                    DeptId = model.UserDeptId,
                    Password = _userService.GetEncryptPwd(model.Password),
                    Tel = model.Tel,
                    UserId = model.UserId,
                    UserName = model.UserName
                };
                if (dept.EntityId > 0)
                    _userService.UpdateUser(dept);
                else
                    _userService.AddUser(dept);
                return Content("ok");
            }
            catch (Exception ex)
            {
                return Content("操作失败：" + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult DeleteUser(int uid)
        {
            try
            {
                var user = _userService.GetUser(uid);
                if (null == user)
                    return Content("ok");
                if (user.UserId.Equals("admin", StringComparison.CurrentCultureIgnoreCase))
                    return Content("默认管理员无法删除");
                _userService.DeleteUser(uid);
                return Content("ok");
            }
            catch (Exception ex)
            {
                return Content("操作失败：" + ex.Message);
            }
        }

        #endregion

        #region 位置管理
        /// <summary>
        /// 位置管理
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.SysManage_Manage_StockAndPlace)]
        public ActionResult StockAndPlaceManage(string placeType = "loc")
        {
            ViewBag.Type = placeType;
            return View();
        }

        private void BuildPlaceNode(List<Place> source, PlaceTreeNode root, int parent)
        {
            var items = source.Where(c => c.ParentId == parent).ToList();
            root.isParent = items.Count > 0;
            foreach (var item in items)
            {
                var node = new PlaceTreeNode
                {
                    id = item.EntityId,
                    realName = item.PlaceName,
                    code = item.PlaceCode,
                    //name = string.Format("{0}（{1}）", item.PlaceName, item.PlaceCode),
                    name = string.Format("{0}", item.PlaceName),
                    pId = parent,
                    pName = item.ParentName,
                    pCode = item.ParentCode
                };
                root.children.Add(node);
                source.Remove(item);
                BuildPlaceNode(source, node, item.EntityId);
            }
        }

        public ActionResult ListPlace(string placeType)
        {
            var list = _sysService.GetAllPlace(placeType);
            var root = new PlaceTreeNode();
            BuildPlaceNode(list, root, 0);
            return Json(root.children, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AllListPlace()
        {
            var list = _sysService.GetAllPlace("loc");
            JsonResult result = new JsonResult();
            result.Data = list.Select(p => new
            {
                id = p.PlaceName,
                pid = p.ParentId,
                text = p.PlaceName
            });
            return result;
        }
        [HttpPost]
        public ActionResult SavePlace(Place model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.PlaceName))
                    return Content("名称不能为空且不超过20字");
                if (string.IsNullOrEmpty(model.PlaceCode))
                    return Content("编码不能为空且不超过20字");

                if (model.EntityId > 0)
                    _sysService.UpdatePlace(model);
                else

                    _sysService.AddPlace(model);
                return Content("ok");
            }
            catch (Exception ex)
            {
                return Content("操作失败：" + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult DeletePlace(int placeId)
        {
            try
            {
                _sysService.DeletePlace(placeId);
                return Content("ok");
            }
            catch (Exception ex)
            {
                return Content("操作失败：" + ex.Message);
            }
        }

        #endregion
        #region 系统管理
        /// <summary>
        /// 系统消息
        /// </summary>
        /// <returns></returns>
        public ActionResult SysMessage()
        {
            return View();
        }
        public ActionResult GetAllMessage(int pageIndex = 1, int pageSize = 10)
        {

            AssetsScrapQuery assetsQuery = new AssetsScrapQuery() { PageIndex = pageIndex, PageSize = pageSize };
            PagedList<AssetsMain> mainList = _assetsService.QueryPage(assetsQuery);

            return Json(mainList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        /// <summary>
        /// 人员导入模板 2017-05-26 wnn
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.SycData_Syc_Export)]
        public ActionResult DownLoadUserSample()
        {
            var tempFile = Server.MapPath("~/Temp/");
            var savePath = Path.Combine(tempFile,
                string.Format("{0}.xls", "人员导入模板"));
            return File(savePath, "application/ms-excel", "人员导入模板.xls");
        }
        /// <summary>
        /// 位置导入模板 2017-06-02 wnn
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.SycData_Syc_Export)]
        public ActionResult DownLoadPlaceSample()
        {
            var tempFile = Server.MapPath("~/Temp/");
            var savePath = Path.Combine(tempFile,
                string.Format("{0}.xls", "位置导入模板"));
            return File(savePath, "application/ms-excel", "位置导入模板.xls");
        }
    }
}