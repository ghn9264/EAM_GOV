using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EAM.Data.Services;
using EAM.Data.Services.Query;
using Eam.Web.Portal.Areas.AssetsFind.Models;
using Eam.Web.Portal._Comm;

namespace Eam.Web.Portal.Areas.AssetsFind.Controllers
{
    /// <summary>
    /// 资产维护
    /// </summary>
    public class AssetsMaintainController : EamAdminController
    {
        private readonly IAssetsService _assetsService;
        private readonly IUserService _userService;

        public AssetsMaintainController(IAssetsService assetsService,IUserService userService)
        {
            _assetsService = assetsService;
            _userService = userService;
        }

        [Permission(EnumBusinessPermission.Assets_Maintain_Acquire)]
        public ActionResult AcquireMaintian()
        {
            ViewBag.DoAction = ConstTag.AcquireMaintian;
            return View();
        }

        [HttpPost]
        [Permission(EnumBusinessPermission.Assets_Maintain_Acquire)]
        public ActionResult AcquireMaintian(List<string> assetsNum, int isUse = 1)
        {
            if (assetsNum == null || assetsNum.Count == 0)
            {
                return Back("维护项目不能为空");
            }
            try
            {
                var result = _assetsService.UpdateIsUse(isUse, assetsNum);
                _userService.RemoveFormOrderList(ConstTag.AcquireMaintian);
            }
            catch (Exception ex)
            {
                return Back(string.Format("操作失败：{0}", ex.Message));
            }
            return Back("操作成功");
        }


        [Permission(EnumBusinessPermission.Assets_Maintain_Lend)]
        public ActionResult LendMaintian()
        {
            ViewBag.DoAction = ConstTag.LendMaintian;
            return View();
        }

        [HttpPost]
        [Permission(EnumBusinessPermission.Assets_Maintain_Lend)]
        public ActionResult LendMaintian(List<string> assetsNum, int isBorrow = 1)
        {
            if (assetsNum == null || assetsNum.Count == 0)
            {
                return Back("维护项目不能为空");
            }
            try
            {
                var result = _assetsService.UpdateIsBorrow(isBorrow, assetsNum);
                _userService.RemoveFormOrderList(ConstTag.LendMaintian);
            }
            catch (Exception ex)
            {
                return Back(string.Format("操作失败：{0}", ex.Message));
            }
            return Back("操作成功");
        }
        
        [Permission(EnumBusinessPermission.Assets_Maintain_Change)]
        public ActionResult InfoChange(QueryModel queryModel)
        {
            ViewBag.DoAction = ConstTag.ChangeInfo;
            return View();
        }

        /// <summary>
        /// 资产查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AssetsQuery(AssetsQuery model)
        {
            var result = _assetsService.QueryPage(model);
            return Json(result);
        }

    }
}