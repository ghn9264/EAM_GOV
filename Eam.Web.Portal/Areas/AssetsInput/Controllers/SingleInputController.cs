using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Linq;
using System.Web.Routing;
using EAM.Data.Domain;
using EAM.Data.Services;
using EAM.Data.Services.Dto;
using Eam.Web.Portal.Areas.AssetsInput.Models;
using Eam.Web.Portal._Comm;

namespace Eam.Web.Portal.Areas.AssetsInput.Controllers
{
    [Permission(EnumBusinessPermission.Assets_Input_Single)]
    public class SingleInputController : EamAdminController
    {
        private readonly IAssetsService _assetsService;
        private readonly IUserService _userService;

        public SingleInputController(IAssetsService assetsService, IUserService userService)
        {
            _assetsService = assetsService;
            _userService = userService;
        }

        // GET: AssetsInput/SingleInput
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TreeTest()
        {

            return  View();
        }

        public ActionResult SingleInput()
        {
            ViewBag.Main = new AssetsMain()
            {
                GetDate = DateTime.Now,
                PostingDate = DateTime.Now
            };
            
            return View(new AllAttribute());
            //return View();
        }

        /// <summary>
        /// 单个资产呈现
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowSingleAssets(int entityId)
        {
            ViewBag.EntityId = entityId;
            var result = _assetsService.GetResultDto(entityId);
            if (null != result.AssetsMain && !string.IsNullOrEmpty(result.AssetsMain.AssetsPicPath))
            {
                var picHttpPath = Path.Combine(PortalSetting.UpLoadPicHttpPrix, result.AssetsMain.AssetsPicPath);
                result.AssetsMain.AssetsPicPath = picHttpPath;
            }
            return View(result);
        }


        [System.Web.Mvc.HttpPost]
        public ActionResult SingleInput(TypeModel typeModel,FormCollection collection)
        {
            var _type = typeModel.AssetType;
            if (string.IsNullOrEmpty(_type))
                return base.Back("未找到相应的资产分类");
            var @type = _type.GetAssetsTypeByCatCode();

            var main = new AssetsMain();
            var allAttribute = new AllAttribute();
            int EntyId = 0;
            var href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=";

            //var attr = null;
            switch (@type)
            {
                case AssetsTypes.Land:

                     main = new AssetsMain();
                    this.TryUpdateModel(main);

                    //
                    // 默认可领用可借用
                    //
                    main.IsBorrow = 1;
                    main.IsUse = 1;

                    allAttribute = new AllAttribute();
                    this.TryUpdateModel(allAttribute);
                    var Landattr = new LandAttribute()
                    {
                        PropertyForm = allAttribute.LandPropertyForm,
                        OwnershipCertificate = allAttribute.LandOwnershipCertifiateNum,
                        OwnershipStyle = allAttribute.LandOwnershipStyle,
                        OwnershipTime = allAttribute.LandOwnershipTime,
                        OwnershipCertifiateNum = allAttribute.LandOwnershipCertifiateNum,
                        IssuingDate = allAttribute.LandIssuingDate,
                        LandOwner = allAttribute.LandOwner,
                        LandUserStyle = allAttribute.LandUserStyle,
                        Location = allAttribute.LandLocation,
                        AllArea = allAttribute.LandAllArea,
                        SharingArea = allAttribute.LandSharingArea,
                        SingleArea = allAttribute.LandSingleArea
                    };
                    //this.TryUpdateModel(attr);
                    SaveImage(main);

                    EntyId = _assetsService.SaveAssetsEx(main, Landattr);
            
                    _assetsService.UpdateNextAssetsNum(main.CatCode);

                    //
                    // 资产详情查看
                    //
                    href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
                    return Redirect(href);
                    break;
                case AssetsTypes.Car:
                    main = new AssetsMain();
                    this.TryUpdateModel(main);

                    //
                    // 默认可领用可借用
                    //
                    main.IsBorrow = 1;
                    main.IsUse = 1;

                    allAttribute = new AllAttribute();
                    this.TryUpdateModel(allAttribute);
                    var carattr = new CarAttribute()
                    {
                        CarUser = allAttribute.CarUser,
                        UsingType = allAttribute.CarUsingType,
                        Organization = allAttribute.CarOrganization,
                        CarNum = allAttribute.CarNum,
                        CarRecCode = allAttribute.CarRecCode,
                        CarCertificateDate = allAttribute.CarCertificateDate,
                        CarExhaust = allAttribute.CarExhaust,
                        CarEngineNum = allAttribute.CarEngineNum,
                        CarErollDare = allAttribute.CarErollDare,
                        CarOwner = allAttribute.CarOwner,
                        CarBrand = allAttribute.CarBrand
                    };
                    //this.TryUpdateModel(attr);
                    SaveImage(main);

                    EntyId = _assetsService.SaveAssetsEx(main, carattr);
                    href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
                    _assetsService.UpdateNextAssetsNum(main.CatCode);

                    return Redirect(href);
                    break;
                case AssetsTypes.Building:
                    main = new AssetsMain();
                    this.TryUpdateModel(main);

                    //
                    // 默认可领用可借用
                    //
                    main.IsBorrow = 1;
                    main.IsUse = 1;


                    allAttribute = new AllAttribute();
                    this.TryUpdateModel(allAttribute);
                    var Buildingattr = new BuildingAttribute()
                    {
                        PropertyForm = allAttribute.BuildPropertyForm,
                        OwnershipCertifiateNum = allAttribute.BuildOwnershipCertifiateNum,
                        Area = allAttribute.BuildArea,
                        Location = allAttribute.BuildLocation
                       
                    };
                    //this.TryUpdateModel(attr);
                    SaveImage(main);

                    EntyId = _assetsService.SaveAssetsEx(main, Buildingattr);
                    href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
                    _assetsService.UpdateNextAssetsNum(main.CatCode);

                    return Redirect(href);
                    break;                    
                case AssetsTypes.Culturalrelic:
                     main = new AssetsMain();
                    this.TryUpdateModel(main);

                    //
                    // 默认可领用可借用
                    //
                    main.IsBorrow = 1;
                    main.IsUse = 1;

                    allAttribute = new AllAttribute();
                    this.TryUpdateModel(allAttribute);
                    var Culturalrelicattr = new CulturalrelicAttribute()
                    {
                        GoodsLevel = allAttribute.CuiltureGoodsLevel,
                        Years = allAttribute.CuiltureYears,
                        SourcePlace = allAttribute.CuiltureSourcePlace
                     
                       
                    };
                    //this.TryUpdateModel(attr);
                    SaveImage(main);

                    EntyId = _assetsService.SaveAssetsEx(main, Culturalrelicattr);
                    href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
                    _assetsService.UpdateNextAssetsNum(main.CatCode);

                    return Redirect(href);
                    break;   
                case AssetsTypes.Animalandplant:
                    main = new AssetsMain();
                    this.TryUpdateModel(main);

                    //
                    // 默认可领用可借用
                    //
                    main.IsBorrow = 1;
                    main.IsUse = 1;

                    allAttribute = new AllAttribute();
                    this.TryUpdateModel(allAttribute);
                    var Animalandplant = new AnimalandplantAttribute()
                    {
                        Birthday = allAttribute.AnimalBirthday,
                        Age = allAttribute.AnimalAge,
                        Class = allAttribute.AnimalClass,
                        Palce = allAttribute.AnimalPalce
                       
                    };
                    //this.TryUpdateModel(attr);
                    SaveImage(main);

                    EntyId = _assetsService.SaveAssetsEx(main, Animalandplant);
                    href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
                    _assetsService.UpdateNextAssetsNum(main.CatCode);

                    return Redirect(href);
                    break;
                case AssetsTypes.Furniture:
                     main = new AssetsMain();
                    this.TryUpdateModel(main);

                    //
                    // 默认可领用可借用
                    //
                    main.IsBorrow = 1;
                    main.IsUse = 1;


                    allAttribute = new AllAttribute();
                    this.TryUpdateModel(allAttribute);
                    var Furniture = new FromUriAttribute(){
                       
                       
                    };
                    //this.TryUpdateModel(attr);
                    SaveImage(main);

                    EntyId = _assetsService.SaveAssetsEx(main);
                    href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
                    _assetsService.UpdateNextAssetsNum(main.CatCode);

                    return Redirect(href);
                    break;
                case AssetsTypes.GeneralEquipment:
                      main = new AssetsMain();
                    this.TryUpdateModel(main);

                    //
                    // 默认可领用可借用
                    //
                    main.IsBorrow = 1;
                    main.IsUse = 1;

                    allAttribute = new AllAttribute();
                    this.TryUpdateModel(allAttribute);
                    var GeneralEquipment = new GeneralAttribute()
                    {
                        DevUse = allAttribute.GeneralDevUse,
                        DevFactory = allAttribute.GeneralDevFactory,
                        WarrantyDate = allAttribute.GeneralWarrantyDate,
                        WarrantyPhone = allAttribute.GeneralWarrantyPhone,
                        GeneralBrand = allAttribute.GeneralBrand
                       
                    };
                    //this.TryUpdateModel(attr);
                    SaveImage(main);

                    EntyId = _assetsService.SaveAssetsEx(main, GeneralEquipment);
                    href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
                    _assetsService.UpdateNextAssetsNum(main.CatCode);

                    return Redirect(href);
                    break; 
                case AssetsTypes.House:
                       main = new AssetsMain();
                    this.TryUpdateModel(main);

                    //
                    // 默认可领用可借用
                    //
                    main.IsBorrow = 1;
                    main.IsUse = 1;

                    allAttribute = new AllAttribute();
                    this.TryUpdateModel(allAttribute);
                    var House = new HouseAttribute()
                    {
                        PropertyForm = allAttribute.HousePropertyForm,
                        OwnershipCertificate = allAttribute.HouseOwnershipCertificate,
                        OwnershipCertifiateNum = allAttribute.HouseOwnershipCertifiateNum,
                        OwnershipTime = allAttribute.HouseOwnershipTime,
                        IssuingDate = allAttribute.HouseIssuingDate,
                        HouseOwner = allAttribute.HouseOwner,
                        DesignUse = allAttribute.HouseDesignUse,
                        BuildingStruccture= allAttribute.HouseBuildingStruccture,
                        Location=allAttribute.HouseLocation,
                        Area=allAttribute.HouseArea

                       
                    };
                    //this.TryUpdateModel(attr);
                    SaveImage(main);

                    EntyId = _assetsService.SaveAssetsEx(main, House);
                    href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
                    _assetsService.UpdateNextAssetsNum(main.CatCode);

                    return Redirect(href);
                    break; 
                case AssetsTypes.SpecialEquipment:
                       main = new AssetsMain();
                    this.TryUpdateModel(main);

                    //
                    // 默认可领用可借用
                    //
                    main.IsBorrow = 1;
                    main.IsUse = 1;

                    allAttribute = new AllAttribute();
                    this.TryUpdateModel(allAttribute);
                    var SpecialEquipment = new SpecialAttribute()
                    {
                        DevFactory = allAttribute.SpecialDevFactory,
                        WarrantyDate = allAttribute.SpecialWarrantyDate,
                        WarrantyPhone = allAttribute.SpecialWarrantyPhone,
                        SpecialBrand = allAttribute.SpecialBrand
                     
                       
                    };
                    //this.TryUpdateModel(attr);
                    SaveImage(main);

                    EntyId = _assetsService.SaveAssetsEx(main, SpecialEquipment);
                    href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
                    _assetsService.UpdateNextAssetsNum(main.CatCode);

                    return Redirect(href);
                    break; 
                case AssetsTypes.Book:
                       main = new AssetsMain();
                    this.TryUpdateModel(main);

                    //
                    // 默认可领用可借用
                    //
                    main.IsBorrow = 1;
                    main.IsUse = 1;

                    allAttribute = new AllAttribute();
                    this.TryUpdateModel(allAttribute);
                    var Book = new BooksAttribute()
                    {
                        UseDate = allAttribute.BookUseDate,
                        FileNum = allAttribute.BookFileNum,
                        Press = allAttribute.BookPress,
                        StoreYears = allAttribute.BookStoreYears,
                        PressDate=allAttribute.BookPressDate

                       
                    };
                    //this.TryUpdateModel(attr);
                    SaveImage(main);

                    EntyId = _assetsService.SaveAssetsEx(main, Book);
                    href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
                    _assetsService.UpdateNextAssetsNum(main.CatCode);

                    return Redirect(href);
                    break; 
            }
            return Back("资产类型错误");
        }

        public ActionResult InputLand(FormCollection collection)
        {
            var main = new AssetsMain();
            this.TryUpdateModel(main);
            var attr = new LandAttribute();
            this.TryUpdateModel(attr);
            SaveImage(main);

            int EntyId = _assetsService.SaveAssetsEx(main, attr);
            
            _assetsService.UpdateNextAssetsNum(main.CatCode);

            RedirectToAction("ViewDetail", "AssetsFind", new { Areas = "AssetsFind", entityId = EntyId });

            //href="/AssetsFind/AssetsFind/ViewDetail?entityId=EntyId '&t=' + (new Date()).valueOf()
                
            //return this.PageReturn("操作成功");
            return Back("操作成功");
        }

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

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="main"></param>
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


        public ActionResult Edit(int entityId = 0, string catCode = "")
        {
            if (string.IsNullOrEmpty(catCode))
                return base.Back("未找到相应的资产分类");
            var @type = catCode.GetAssetsTypeByCatCode();

            switch (@type)
            {
                case AssetsTypes.Land:
                    return RedirectToAction("EditLand", new { entityId = entityId });
                    break;
                case AssetsTypes.Car:
                    return RedirectToAction("EditCar", new { entityId = entityId });
                    break;
                case AssetsTypes.Building:
                    return RedirectToAction("EditBuilding", new { entityId = entityId });
                    break;
                case AssetsTypes.Culturalrelic:
                    return RedirectToAction("EditCulturalrelic", new { entityId = entityId });
                    break;
                case AssetsTypes.Animalandplant:
                    return RedirectToAction("EditAnimalandplant", new { entityId = entityId });
                    break;
                case AssetsTypes.Furniture:
                    return RedirectToAction("EditFurniture", new { entityId = entityId });
                    break;
                case AssetsTypes.GeneralEquipment:
                    return RedirectToAction("EditGeneralequipment", new { entityId = entityId });
                    break;
                case AssetsTypes.House:
                    return RedirectToAction("EditHouse", new { entityId = entityId });
                    break;
                case AssetsTypes.SpecialEquipment:
                    return RedirectToAction("EditSpecialequipment", new { entityId = entityId });
                    break;
                case AssetsTypes.Book:
                    return RedirectToAction("EditBooks", new { entityId = entityId });
                    break;
            }
            return Back("未找到相应的记录");
        }

        #region Animalandplant


        public ActionResult EditAnimalandplant(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.Animalandplant ?? new AnimalandplantAttribute();
            var viewModel = new AllAttribute()
            {
                AnimalBirthday = model.Birthday,
                AnimalAge = model.Age,
                AnimalClass = model.Class,
                AnimalPalce = model.Palce
            };
            ViewBag.Main = assetsMain.AssetsMain;
            model.AttrId = model.EntityId;
            return View("SingleInput", viewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult EditAnimalandplant(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);

            SaveImage(main);

            var allAttribute = new AllAttribute();
            this.TryUpdateModel(allAttribute);

            int EntyId = 0;
            try
            {

                var attr = _assetsService.GetAnimalandplant(main.AssetsNum);

                attr.Birthday = allAttribute.AnimalBirthday;
                attr.Age = allAttribute.AnimalAge;
                attr.Class = allAttribute.AnimalClass;
                attr.Palce = allAttribute.AnimalPalce;
                attr.EntityId = attr.AttrId;
                EntyId = _assetsService.UpdateAssetsEx(main, attr);

            }
            catch (Exception)
            {

                EntyId = _assetsService.SaveAssetsEx(main);
            }


            //
            // 资产详情查看
            //
            var href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
            return Redirect(href);
        }

        #endregion

        #region Books

        public ActionResult EditBooks(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.Book ?? new BooksAttribute();
            var viewModel = new AllAttribute()
            {
                BookUseDate = model.UseDate,
                BookFileNum = model.FileNum,
                BookPress = model.Press,
                BookStoreYears = model.StoreYears,
                BookPressDate = model.PressDate
            };
            ViewBag.Main = assetsMain.AssetsMain;
            model.AttrId = model.EntityId;
            return View("SingleInput", viewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult EditBooks(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);

            SaveImage(main);

            var allAttribute = new AllAttribute();
            this.TryUpdateModel(allAttribute);

            int EntyId = 0;
            try
            {

                var attr = _assetsService.GetBook(main.AssetsNum);
                attr.UseDate = allAttribute.BookUseDate;
                attr.FileNum = allAttribute.BookFileNum;
                attr.Press = allAttribute.BookPress;
                attr.StoreYears = allAttribute.BookStoreYears;
                attr.PressDate = allAttribute.BookPressDate;
                attr.EntityId = attr.AttrId;
                EntyId = _assetsService.UpdateAssetsEx(main, attr);

            }
            catch (Exception)
            {

                EntyId = _assetsService.SaveAssetsEx(main);
            }

            var href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
            return Redirect(href);
        }

        #endregion

        #region Building


        public ActionResult EditBuilding(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.Building ?? new BuildingAttribute();
            var viewModel = new AllAttribute()
            {
                BuildPropertyForm = model.PropertyForm,
                BuildOwnershipCertifiateNum = model.OwnershipCertifiateNum,
                BuildArea = model.Area,
                BuildLocation = model.Location
              
            };
            ViewBag.Main = assetsMain.AssetsMain;
            model.AttrId = model.EntityId;
            return View("SingleInput", viewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult EditBuilding(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);

            SaveImage(main);

            var allAttribute = new AllAttribute();
            this.TryUpdateModel(allAttribute);

            int EntyId = 0;
            try
            {


                var attr = _assetsService.GetBuilding(main.AssetsNum);
                attr.PropertyForm = allAttribute.BuildPropertyForm;
                attr.OwnershipCertifiateNum = allAttribute.BuildOwnershipCertifiateNum;
                attr.Area = allAttribute.BuildArea;
                attr.Location = allAttribute.BuildLocation;
                attr.EntityId = attr.AttrId;
                EntyId = _assetsService.UpdateAssetsEx(main, attr);

            }
            catch (Exception)
            {

                EntyId = _assetsService.SaveAssetsEx(main);
            }

            var href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
            return Redirect(href);
        }


        #endregion

        #region Car
        public ActionResult EditCar(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.Car ?? new CarAttribute();
            var viewModel = new AllAttribute()
            {
                CarUser = model.CarUser,
                CarUsingType = model.UsingType,
                CarOrganization = model.Organization,
                CarEngineNum = model.CarNum,
                CarRecCode = model.CarRecCode,
                CarCertificateDate = model.CarCertificateDate,
                CarExhaust = model.CarExhaust,
                CarNum = model.CarEngineNum,
                CarErollDare = model.CarErollDare,
                CarOwner = model.CarOwner,
                CarBrand = model.CarBrand

            };
            ViewBag.Main = assetsMain.AssetsMain;
            model.AttrId = model.EntityId;
            model.CarBrand = assetsMain.AssetsMain.Brand;
            return View("SingleInput", viewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult EditCar(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);

            SaveImage(main);

            var allAttribute = new AllAttribute();
            this.TryUpdateModel(allAttribute);

            int EntyId = 0;
            try
            {

                var attr = _assetsService.GetCar(main.AssetsNum);
                attr.CarUser = allAttribute.CarUser;
                attr.UsingType = allAttribute.CarUsingType;
                attr.Organization = allAttribute.CarOrganization;
                attr.CarNum = allAttribute.CarNum;
                attr.CarRecCode = allAttribute.CarRecCode;
                attr.CarCertificateDate = allAttribute.CarCertificateDate;
                attr.CarExhaust = allAttribute.CarExhaust;
                attr.CarEngineNum = allAttribute.CarEngineNum;
                attr.CarErollDare = allAttribute.CarErollDare;
                attr.CarOwner = allAttribute.CarOwner;
                attr.CarBrand = allAttribute.CarBrand;
                attr.EntityId = attr.AttrId;
                main.Brand = attr.CarBrand;
                EntyId = _assetsService.UpdateAssetsEx(main, attr);

            }
            catch (Exception)
            {

                EntyId = _assetsService.SaveAssetsEx(main);
            }
            
            var href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
            return Redirect(href);
        }

        #endregion

        #region Culturalrelic

        public ActionResult EditCulturalrelic(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.Culturalrelic ?? new CulturalrelicAttribute();
            var viewModel = new AllAttribute()
            {
                CuiltureGoodsLevel = model.GoodsLevel,
                CuiltureYears = model.Years,
                CuiltureSourcePlace = model.SourcePlace
            };
            ViewBag.Main = assetsMain.AssetsMain;
            model.AttrId = model.EntityId;
            return View("SingleInput", viewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult EditCulturalrelic(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);

            SaveImage(main);

            var allAttribute = new AllAttribute();
            this.TryUpdateModel(allAttribute);

            int EntyId = 0;
            try
            {
                var attr = _assetsService.GetCulturalrelic(main.AssetsNum);
                attr.GoodsLevel = allAttribute.CuiltureGoodsLevel;
                attr.Years = allAttribute.CuiltureYears;
                attr.SourcePlace = allAttribute.CuiltureSourcePlace;
                attr.EntityId = attr.AttrId;
                EntyId = _assetsService.UpdateAssetsEx(main, attr);

            }
            catch (Exception)
            {

                EntyId = _assetsService.SaveAssetsEx(main);
            }

            var href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
            return Redirect(href);
        }

        #endregion

        #region Furniture


        public ActionResult EditFurniture(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.AssetsMain ?? new AssetsMain();
            var viewModel = new AllAttribute()
            {
                //CuiltureGoodsLevel = model.GoodsLevel,
                //CuiltureYears = model.Years,
                //CuiltureSourcePlace = model.SourcePlace


            };
            ViewBag.Main = assetsMain.AssetsMain;
            //model.AttrId = model.EntityId;
            return View("SingleInput", viewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult EditFurniture(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);
            SaveImage(main);
            int EntyId = _assetsService.SaveAssetsEx(main);
            var href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
            return Redirect(href);
        }

        #endregion

        #region Generalequipment


        public ActionResult EditGeneralequipment(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.General ?? new GeneralAttribute();
            var viewModel = new AllAttribute()
            {
                GeneralDevUse = model.DevUse,
                GeneralDevFactory = model.DevFactory,
                GeneralWarrantyDate = model.WarrantyDate,
                GeneralWarrantyPhone = model.WarrantyPhone
            };
            ViewBag.Main = assetsMain.AssetsMain;
            model.AttrId = model.EntityId;
            return View("SingleInput", viewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult EditGeneralequipment(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            var orderlsit = _userService.QueryByAssetsNum(ConstTag.ChangeInfo,main.AssetsNum).FirstOrDefault();
          

            
            
            TryUpdateModel(main);

            SaveImage(main);

            var allAttribute = new AllAttribute();
         
            this.TryUpdateModel(allAttribute);

            int EntyId = 0;
            try
            {
                var attr = _assetsService.GetGeneral(main.AssetsNum);
                attr.DevUse = allAttribute.GeneralDevUse;
                attr.DevFactory = allAttribute.GeneralDevFactory;
                attr.WarrantyDate = allAttribute.GeneralWarrantyDate;
                attr.WarrantyPhone = allAttribute.GeneralWarrantyPhone;

                attr.EntityId = attr.AttrId;
                EntyId = _assetsService.UpdateAssetsEx(main, attr);
                orderlsit.GoodsName = collection["GoodSName"];
                orderlsit.UsePeople = collection["UsePeople"];
                orderlsit.StorePlace = collection["StorePlace"];
                orderlsit.UseDepartment = collection["UseDepartment "];
                _userService.UpdateOrderList(orderlsit);

            }
            catch (Exception)
            {

                EntyId = _assetsService.SaveAssetsEx(main);
            }

            var href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
            return Redirect(href);
        }

        #endregion

        #region House

        public ActionResult EditHouse(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.House ?? new HouseAttribute();
            var viewModel = new AllAttribute()
            {
                HousePropertyForm = model.PropertyForm,
                HouseOwnershipCertificate = model.OwnershipCertificate,
                HouseOwnershipCertifiateNum = model.OwnershipCertifiateNum,
                HouseOwnershipTime = model.OwnershipTime,
                HouseIssuingDate = model.IssuingDate,
                HouseOwner = model.HouseOwner,
                HouseDesignUse = model.DesignUse,
                HouseBuildingStruccture = model.BuildingStruccture,
                HouseLocation = model.Location,
                HouseArea = model.Area
       

            };
            ViewBag.Main = assetsMain.AssetsMain;
            model.AttrId = model.EntityId;
            return View("SingleInput", viewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult EditHouse(FormCollection collection, int entityId = 0)
        {
            //
            // Get函数作用是从数据库assets_main表里面根据entityId查询出来某条资产的所有数据
            //
            var main = _assetsService.Get(entityId);

            //
            // MVC从前端将数据获取到后台
            // 这里是将前台的数据更新到main变量中
            // 前端数据后台的对应关系如下：
            // 1、后台的变量命名要和前端的元素id以及name命名保持一致
            // 2、而且前端的每个元素的id和name命名都不重复
            // 保证上述规则，调用这个方法才会保证前台的数据更新到后台中。
            //
            TryUpdateModel(main);

            //
            // 保存图片
            //
            SaveImage(main);

            //
            // 实例化一个AllAttribute类
            // 这个类里面包含了所有类型的资产的差异化属性
            //
            var allAttribute = new AllAttribute();

            //
            // MVC将前端的数据更新到后台
            // 这里是将前台的数据更新到allAttribute变量当中
            //
            this.TryUpdateModel(allAttribute);

            //
            // 定义一个int变量
            //
            int EntyId = 0;
            try
            {
                //
                // 定义一个attr变量
                // 通过调用GetHouse方法给attr变量赋值
                // 这一句的作用是根据资产编号assetsNum到数据库House表里面去查询某一条记录
                // 这一句不仅仅是给attr变量赋值，同时也意味着给attr变量定义变量的数据类型
                //
                var attr = _assetsService.GetHouse(main.AssetsNum);

                // 
                // 将allAttribute中的数据复制到attr变量当中
                // 其中allAttribute中的数据来自于前端
                // 这样的话讲意味着前端的数据更新到了attr变量当中
                //
                attr.PropertyForm = allAttribute.HousePropertyForm;
                attr.OwnershipCertificate = allAttribute.HouseOwnershipCertificate;
                attr.OwnershipCertifiateNum = allAttribute.HouseOwnershipCertifiateNum;
                attr.OwnershipTime = allAttribute.HouseOwnershipTime;
                attr.IssuingDate = allAttribute.HouseIssuingDate;
                attr.HouseOwner = allAttribute.HouseOwner;
                attr.DesignUse = allAttribute.HouseDesignUse;
                attr.BuildingStruccture = allAttribute.HouseBuildingStruccture;
                attr.Location = allAttribute.HouseLocation;
                attr.Area = allAttribute.HouseArea;

                //
                // 这一句又一次将图片进行了保存
                // 为什么要进行两次图片的保存，尚不清楚原因
                //
                SaveImage(main);

                //
                // 将attr的AttrId赋值给AttrId的EntityId
                // 作用不明
                //
                attr.EntityId = attr.AttrId;

                //
                // 调用UpdateAssetsEx方法
                // 入口参数是main和attr
                // 通过对前面代码的分析，其中main应该是保存了资产的通用属性数据，并且数据是从前端取过来的
                // attr保存了从前端过来的资产的差异化属性数据
                // 所以这一句的作用是将前台的数据更新到数据库当中去。
                //
                EntyId = _assetsService.UpdateAssetsEx(main, attr);

            }
            catch (Exception)
            {
                //
                // 如果在执行try里面的代码的时候出现异常
                // 则执行下面这条语句
                // 这条语句的作用是将main的数据写到数据库当中去
                //
                EntyId = _assetsService.SaveAssetsEx(main);
            }
            
            //
            // 定义个链接地址
            // 注意连接地址是带参数的，参数名为entityId，给entityId传入的值是EntyId
            //
            var href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;

            //
            // 跳转到上面定义好的那个连接
            //
            return Redirect(href);
        }

        #endregion

        #region Land


        public ActionResult EditLand(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.Land ?? new LandAttribute();
            var viewModel = new AllAttribute()
            {
                LandPropertyForm = model.PropertyForm,
                LandOwnershipCertificate = model.OwnershipCertificate,
                LandOwnershipStyle = model.OwnershipStyle,
                LandOwnershipTime = model.OwnershipTime,
                LandOwnershipCertifiateNum = model.OwnershipCertifiateNum,
                LandIssuingDate = model.IssuingDate,
                LandOwner = model.LandOwner,
                LandUserStyle = model.LandUserStyle,
                LandLocation = model.Location,
                LandAllArea = model.AllArea,
                LandSharingArea = model.SharingArea,
                LandSingleArea = model.SingleArea
            };
            ViewBag.Main = assetsMain.AssetsMain;
            model.AttrId = model.EntityId;
            return View("SingleInput", viewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult EditLand(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);
            SaveImage(main);
            var allAttribute = new AllAttribute();
            this.TryUpdateModel(allAttribute);

            int EntyId = 0;
            try
            {
                var attr = _assetsService.GetLand(main.AssetsNum);

                attr.PropertyForm = allAttribute.LandPropertyForm;
                attr.OwnershipCertificate = allAttribute.LandOwnershipCertifiateNum;
                attr.OwnershipStyle = allAttribute.LandOwnershipStyle;
                attr.OwnershipTime = allAttribute.LandOwnershipTime;
                attr.OwnershipCertifiateNum = allAttribute.LandOwnershipCertifiateNum;
                attr.IssuingDate = allAttribute.LandIssuingDate;
                attr.LandOwner = allAttribute.LandOwner;
                attr.LandUserStyle = allAttribute.LandUserStyle;
                attr.Location = allAttribute.LandLocation;
                attr.AllArea = allAttribute.LandAllArea;
                attr.SharingArea = allAttribute.LandSharingArea;
                attr.SingleArea = allAttribute.LandSingleArea;
                //TryUpdateModel(attr);
                SaveImage(main);
                attr.EntityId = attr.AttrId;
                EntyId = _assetsService.UpdateAssetsEx(main, attr);
                //_assetsService.UpdateAssetsEx(main, attr);

            }
            catch (Exception)
            {

                EntyId = _assetsService.SaveAssetsEx(main);
            }
            

            //
            // 资产详情查看
            //
            var href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
            return Redirect(href);
            //return this.PageReturn("操作成功");
        }

        #endregion

        #region Specialequipment

        public ActionResult EditSpecialequipment(int entityId = 0)
        {
            var assetsMain = _assetsService.GetResultDto(entityId);
            if (null == assetsMain || assetsMain.AssetsMain == null)
                return Back("未找到相应的记录");
            var model = assetsMain.Special ?? new SpecialAttribute();
            var viewModel = new AllAttribute()
            {
                SpecialDevFactory = model.DevFactory,
                SpecialWarrantyDate = model.WarrantyDate,
                SpecialWarrantyPhone = model.WarrantyPhone,
                SpecialBrand = model.SpecialBrand
            
            };
            ViewBag.Main = assetsMain.AssetsMain;
            return View("SingleInput", viewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult EditSpecialequipment(FormCollection collection, int entityId = 0)
        {
            var main = _assetsService.Get(entityId);
            TryUpdateModel(main);
            SaveImage(main);
            var allAttribute = new AllAttribute();
            this.TryUpdateModel(allAttribute);
            int EntyId = 0;
            try
            {
                var attr = _assetsService.GetSpecial(main.AssetsNum);
                attr.DevFactory = allAttribute.SpecialDevFactory;
                attr.WarrantyDate = allAttribute.SpecialWarrantyDate;
                attr.WarrantyPhone = allAttribute.SpecialWarrantyPhone;
                attr.SpecialBrand = allAttribute.SpecialBrand;
                attr.EntityId = attr.AttrId;
                EntyId = _assetsService.UpdateAssetsEx(main, attr);
                //_assetsService.UpdateAssetsEx(main, attr);
                
            }
            catch (Exception)
            {

                EntyId = _assetsService.SaveAssetsEx(main);
            }

            var href = "/AssetsInput/SingleInput/ShowSingleAssets?entityId=" + EntyId;
            return Redirect(href);
            
        }

        #endregion


        
    }
}