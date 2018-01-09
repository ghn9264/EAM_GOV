using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EAM.Data.Domain;
using EAM.Data.Services;
using Eam.Web.Portal.Areas.AssetsInput.Models;
using Eam.Web.Portal._Comm;

namespace Eam.Web.Portal.Areas.AssetsInput.Controllers
{
    [Permission(EnumBusinessPermission.Assets_Input_Single)]
    public class AssetsInputController : EamAdminController
    {
        private readonly IAssetsService _assetsService;

        public AssetsInputController(IAssetsService assetsService)
        {
            _assetsService = assetsService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int entityId = 0, string catCode = "")
        {
            if (string.IsNullOrEmpty(catCode))
                return base.Back("未找到相应的资产分类");
            var @type = catCode.GetAssetsTypeByCatCode();

            switch (@type)
            {
                case AssetsTypes.Land:
                    return RedirectToAction("EditLand", new {entityId = entityId});
                    break;
                case AssetsTypes.Car:
                    return RedirectToAction("EditCar", new {entityId = entityId});
                    break;
                case AssetsTypes.Building:
                    return RedirectToAction("EditBuilding", new {entityId = entityId});
                    break;
                case AssetsTypes.Culturalrelic:
                    return RedirectToAction("EditCulturalrelic", new {entityId = entityId});
                    break;
                case AssetsTypes.Animalandplant:
                    return RedirectToAction("EditAnimalandplant", new {entityId = entityId});
                    break;
                case AssetsTypes.Furniture:
                    return RedirectToAction("EditFurniture", new {entityId = entityId});
                    break;
                case AssetsTypes.GeneralEquipment:
                    return RedirectToAction("EditGeneralequipment", new {entityId = entityId});
                    break;
                case AssetsTypes.House:
                    return RedirectToAction("EditHouse", new {entityId = entityId});
                    break;
                case AssetsTypes.SpecialEquipment:
                    return RedirectToAction("EditSpecialequipment", new {entityId = entityId});
                    break;
                case AssetsTypes.Book:
                    return RedirectToAction("EditBooks", new {entityId = entityId});
                    break;
            }
            return Back("未找到相应的记录");
        }

        #region Animalandplant

        public ActionResult InputAnimalandplant()
        {
            ViewBag.Main = new AssetsMain();
            return View(new AnimalandplantAttribute());
        }

        [HttpPost]
        public ActionResult InputAnimalandplant(FormCollection collection)
        {
            var main = new AssetsMain();
            this.TryUpdateModel(main);
            var attr = new AnimalandplantAttribute();
            this.TryUpdateModel(attr);
            SaveImage(main);
            attr.EntityId = attr.AttrId;
            _assetsService.SaveAssets(main, attr);
            _assetsService.UpdateNextAssetsNum(main.CatCode);
            return this.PageReturn("操作成功");
        }

        public ActionResult EditAnimalandplant(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.Animalandplant ?? new AnimalandplantAttribute();
            ViewBag.Main = assetsMain.AssetsMain;
            model.AttrId = model.EntityId;
            return View("InputAnimalandplant", model);
        }

        [HttpPost]
        public ActionResult EditAnimalandplant(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);
            var attr = _assetsService.GetAnimalandplant(main.AssetsNum);
            TryUpdateModel(attr);
            SaveImage(main);
            attr.EntityId = attr.AttrId;
            _assetsService.SaveAssets(main, attr);
            return this.PageReturn("操作成功");
        }

        #endregion

        #region Books

        public ActionResult InputBooks()
        {
            ViewBag.Main = new AssetsMain();
            return View(new BooksAttribute());
        }

        [HttpPost]
        public ActionResult InputBooks(FormCollection collection)
        {
            var main = new AssetsMain();
            this.TryUpdateModel(main);
            var attr = new BooksAttribute();
            this.TryUpdateModel(attr);
            SaveImage(main);
            attr.EntityId = attr.AttrId;
            _assetsService.SaveAssets(main, attr);
            _assetsService.UpdateNextAssetsNum(main.CatCode);
            return this.PageReturn("操作成功");
        }

        public ActionResult EditBooks(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.Book ?? new BooksAttribute();
            ViewBag.Main = assetsMain.AssetsMain;
            model.AttrId = model.EntityId;
            return View("InputBooks", model);
        }

        [HttpPost]
        public ActionResult EditBooks(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);
            var attr = _assetsService.GetBook(main.AssetsNum);
            TryUpdateModel(attr);
            SaveImage(main);
            attr.EntityId = attr.AttrId;
            _assetsService.SaveAssets(main, attr);
            return this.PageReturn("操作成功");
        }

        #endregion

        #region Building

        public ActionResult InputBuilding()
        {
            ViewBag.Main = new AssetsMain();
            return View(new LandAttribute());
        }

        [HttpPost]
        public ActionResult InputBuilding(FormCollection collection)
        {
            var main = new AssetsMain();
            this.TryUpdateModel(main);
            var attr = new BuildingAttribute();
            this.TryUpdateModel(attr);
            SaveImage(main);
            attr.EntityId = attr.AttrId;
            _assetsService.SaveAssets(main, attr);
            _assetsService.UpdateNextAssetsNum(main.CatCode);
            return this.PageReturn("操作成功");
        }

        public ActionResult EditBuilding(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.Building ?? new BuildingAttribute();
            ViewBag.Main = assetsMain.AssetsMain;
            model.AttrId = model.EntityId;
            return View("InputBuilding", model);
        }

        [HttpPost]
        public ActionResult EditBuilding(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);
            var attr = _assetsService.GetBuilding(main.AssetsNum);
            TryUpdateModel(attr);
            SaveImage(main);
            attr.EntityId = attr.AttrId;
            _assetsService.SaveAssets(main, attr);
            return this.PageReturn("操作成功");
        }


        #endregion

        #region Car

        public ActionResult InputCar()
        {
            ViewBag.Main = new AssetsMain();
            return View(new CarAttribute());
        }

        [HttpPost]
        public ActionResult InputCar(FormCollection collection)
        {
            var main = new AssetsMain();
            this.TryUpdateModel(main);
            var attr = new CarAttribute();
            this.TryUpdateModel(attr);
            SaveImage(main);
            attr.EntityId = attr.AttrId;
            main.Brand = attr.CarBrand;
            _assetsService.SaveAssets(main, attr);
            _assetsService.UpdateNextAssetsNum(main.CatCode);
            return this.PageReturn("操作成功");
        }

        public ActionResult EditCar(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.Car ?? new CarAttribute();
            ViewBag.Main = assetsMain.AssetsMain;
            model.AttrId = model.EntityId;
            model.CarBrand = assetsMain.AssetsMain.Brand;
            return View("InputCar", model);
        }

        [HttpPost]
        public ActionResult EditCar(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);
            var attr = _assetsService.GetCar(main.AssetsNum);
            TryUpdateModel(attr);
            SaveImage(main);
            attr.EntityId = attr.AttrId;
            main.Brand = attr.CarBrand;
            _assetsService.SaveAssets(main, attr);
            return this.PageReturn("操作成功");
        }

        #endregion

        #region Culturalrelic

        public ActionResult InputCulturalrelic()
        {
            ViewBag.Main = new AssetsMain();
            return View(new CulturalrelicAttribute());
        }

        [HttpPost]
        public ActionResult InputCulturalrelic(FormCollection collection)
        {
            var main = new AssetsMain();
            this.TryUpdateModel(main);
            var attr = new CulturalrelicAttribute();
            this.TryUpdateModel(attr);
            SaveImage(main);
            attr.EntityId = attr.AttrId;
            _assetsService.SaveAssets(main, attr);
            _assetsService.UpdateNextAssetsNum(main.CatCode);
            return this.PageReturn("操作成功");
        }

        public ActionResult EditCulturalrelic(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.Culturalrelic ?? new CulturalrelicAttribute();
            ViewBag.Main = assetsMain.AssetsMain;
            model.AttrId = model.EntityId;
            return View("InputCulturalrelic", model);
        }

        [HttpPost]
        public ActionResult EditCulturalrelic(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);
            var attr = _assetsService.GetCulturalrelic(main.AssetsNum);
            TryUpdateModel(attr);
            SaveImage(main);
            attr.EntityId = attr.AttrId;
            _assetsService.SaveAssets(main, attr);
            return this.PageReturn("操作成功");
        }

        #endregion

        #region Furniture
        public ActionResult InputFurniture()
        {
            ViewBag.Main = new AssetsMain();
            return View(/*new GeneralAttribute()*/);
        }

        [HttpPost]
        public ActionResult InputFurniture(FormCollection collection)
        {
            var main = new AssetsMain();
            this.TryUpdateModel(main);
            /*var attr = new GeneralAttribute();
            this.TryUpdateModel(attr);*/
            SaveImage(main);
            /*main.Brand = attr.GeneralBrand;
            attr.EntityId = attr.AttrId;*/
            _assetsService.SaveAssets(main);
            _assetsService.UpdateNextAssetsNum(main.CatCode);
            return this.PageReturn("操作成功");
        }

        public ActionResult EditFurniture(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            /*var model = assetsMain.General ?? new GeneralAttribute();*/
            ViewBag.Main = assetsMain.AssetsMain;
            /*model.GeneralBrand = assetsMain.AssetsMain.Brand;
            model.AttrId = model.EntityId;*/
            return View("InputFurniture");
        }

        [HttpPost]
        public ActionResult EditFurniture(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);
            /*var attr = _assetsService.GetGeneral(main.AssetsNum);
            TryUpdateModel(attr);*/
            SaveImage(main);
            /*main.Brand = attr.GeneralBrand;
            attr.EntityId = attr.AttrId;*/
            _assetsService.SaveAssets(main);
            return this.PageReturn("操作成功");
        }

        #endregion

        #region Generalequipment

        public ActionResult InputGeneralequipment()
        {
            ViewBag.Main = new AssetsMain();
            return View(new GeneralAttribute());
        }

        [HttpPost]
        public ActionResult InputGeneralequipment(FormCollection collection)
        {
            var main = new AssetsMain();
            this.TryUpdateModel(main);
            var attr = new GeneralAttribute();
            this.TryUpdateModel(attr);
            SaveImage(main);
            main.Brand = attr.GeneralBrand;
            attr.EntityId = attr.AttrId;
            _assetsService.SaveAssets(main, attr);
            _assetsService.UpdateNextAssetsNum(main.CatCode);
            return this.PageReturn("操作成功");
        }

        public ActionResult EditGeneralequipment(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.General ?? new GeneralAttribute();
            ViewBag.Main = assetsMain.AssetsMain;
            model.GeneralBrand = assetsMain.AssetsMain.Brand;
            model.AttrId = model.EntityId;
            return View("InputGeneralequipment", model);
        }

        [HttpPost]
        public ActionResult EditGeneralequipment(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);
            var attr = _assetsService.GetGeneral(main.AssetsNum);
            TryUpdateModel(attr);
            SaveImage(main);
            main.Brand = attr.GeneralBrand;
            attr.EntityId = attr.AttrId;
            _assetsService.SaveAssets(main, attr);
            return this.PageReturn("操作成功");
        }

        #endregion

        #region House

        public ActionResult InputHouse()
        {
            ViewBag.Main = new AssetsMain();
            return View(new HouseAttribute());
        }

        [HttpPost]
        public ActionResult InputHouse(FormCollection collection)
        {
            var main = new AssetsMain();
            this.TryUpdateModel(main);
            var attr = new HouseAttribute();
            this.TryUpdateModel(attr);
            SaveImage(main);
            _assetsService.SaveAssets(main, attr);
            _assetsService.UpdateNextAssetsNum(main.CatCode);
            return this.PageReturn("操作成功");
        }

        public ActionResult EditHouse(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.House ?? new HouseAttribute();
            ViewBag.Main = assetsMain.AssetsMain;
            model.AttrId = model.EntityId;
            return View("InputHouse", model);
        }

        [HttpPost]
        public ActionResult EditHouse(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);
            var attr = _assetsService.GetHouse(main.AssetsNum);
            TryUpdateModel(attr);
            SaveImage(main);
            attr.EntityId = attr.AttrId;
            _assetsService.SaveAssets(main, attr);
            return this.PageReturn("操作成功");
        }

        #endregion

        #region Land

        public ActionResult InputLand()
        {
            ViewBag.Main = new AssetsMain();
            return View(new LandAttribute());
        }

        [HttpPost]
        public ActionResult InputLand(FormCollection collection)
        {
            var main = new AssetsMain();
            this.TryUpdateModel(main);
            var attr = new LandAttribute();
            this.TryUpdateModel(attr);
            SaveImage(main);
            _assetsService.SaveAssets(main, attr);
            _assetsService.UpdateNextAssetsNum(main.CatCode);
            return this.PageReturn("操作成功");
        }

        public ActionResult EditLand(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.Land ?? new LandAttribute();
            ViewBag.Main = assetsMain.AssetsMain;
            model.AttrId = model.EntityId;
            return View("InputLand", model);
        }

        [HttpPost]
        public ActionResult EditLand(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);
            var attr = _assetsService.GetLand(main.AssetsNum);
            TryUpdateModel(attr);
            SaveImage(main);
            attr.EntityId = attr.AttrId;
            _assetsService.SaveAssets(main, attr);
            return this.PageReturn("操作成功");
        }

        #endregion

        #region Specialequipment

        public ActionResult InputSpecialequipment()
        {
            ViewBag.Main = new AssetsMain();
            return View(new SpecialAttribute());
        }

        [HttpPost]
        public ActionResult InputSpecialequipment(FormCollection collection)
        {
            var main = new AssetsMain();
            this.TryUpdateModel(main);
            var attr = new SpecialAttribute();
            this.TryUpdateModel(attr);
            SaveImage(main);
            _assetsService.SaveAssets(main, attr);
            _assetsService.UpdateNextAssetsNum(main.CatCode);
            return this.PageReturn("操作成功");
        }

        public ActionResult EditSpecialequipment(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.Special ?? new SpecialAttribute();
            ViewBag.Main = assetsMain.AssetsMain;
            return View("InputSpecialequipment", model);
        }

        [HttpPost]
        public ActionResult EditSpecialequipment(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);
            var attr = _assetsService.GetSpecial(main.AssetsNum);
            TryUpdateModel(attr);
            SaveImage(main);
            attr.EntityId = attr.AttrId;
            _assetsService.SaveAssets(main, attr);
            return this.PageReturn("操作成功");
        }

        #endregion

        private void SaveImage(AssetsMain main)
        {
            var fileBase = Request.Files["imgOne"];
            if (fileBase != null && fileBase.ContentLength > 0)
            {
                var fileName = string.Format("{0}_{1}{2}", main.CatCode, DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Path.GetExtension(fileBase.FileName));
                var saveFileName = Path.Combine(PortalSetting.UpLoadPicPath, fileName);
                fileBase.SaveAs(saveFileName);
                main.AssetsPicPath = fileName;
            }
        }

    }
}