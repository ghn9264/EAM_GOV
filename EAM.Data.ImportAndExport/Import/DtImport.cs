using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using EAM.Data.Domain;
using EAM.Data.Services;

namespace EAM.Data.ImportAndExport.Import
{
    public class DtImport
    {
        private readonly IAssetsService _assetsService;
        private IImportHistoryService _importHistoryService;
        private IUnImportAssetsService _unImportAssetsService;
        private IUserService _userService;//2017-05-25 wnn
        public DtImport(IAssetsService assetsService, IImportHistoryService importHistoryService, IUnImportAssetsService unImportAssetsService)
        {
            _assetsService = assetsService;
            _importHistoryService = importHistoryService;
            _unImportAssetsService = unImportAssetsService;
        }


     
        /// <summary>
        /// 将导入文件中的数据写入到数据库
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="importProgress"></param>
        /// <returns></returns>
        public ImportResult DoImport(string filePath, ref ProgressInfo importProgress, IImportHistoryService importHistoryService, IUnImportAssetsService unImportAssetsService)
        {
            //
            // 创建导入编号和导入类型
            //
            int crntId = _importHistoryService.LastHistory().EntityId;
            int crntType = _importHistoryService.LastHistory().ImportType;

            ImportResult result = new ImportResult();

            //
            // 从导入文件中提取数据
            //
            var list = DtReader.ReadData(filePath, ref importProgress, _importHistoryService, _unImportAssetsService);

            //
            // 导入进度信息
            //
            double percent = 0.5;
            int idx = 0;
            int idxErr = 1;
            int percentStep1 = importProgress.ImportedPercentVal;

            //
            // 导入异常信息记录
            //
            var unInportAssets = new UnImportAssets();

            //
            // 开始向数据库写入数据
            //
            foreach (AssetsMainExt assetsExt in list)
            {
                try
                {

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    // 动态导入的业务流程如下：
                    // 首先判断备注栏是否有内容，如果没有则表示该记录不是通过本系统导出到动态系统，再到动态系统中附加动态码，再从动态导出并导入到本系统的。
                    // 如果经过上述流程的，资产条目的备注栏会存有本系统生成的资产编号
                    // 对于备注栏里面没有数据的，则对动态编码UsedNum1进行判重，如果有则表示该资产原来就已经导入到本系统，现在是做覆盖操作
                    // 如果UsedNum1不存于数据库中，则将该条目新增到数据库中。
                    // 对于备注栏里面有数据的，以该数据为判重条件，如果数据库中存在该资产，则表示该资产完整的走过本系统到动态再回到本系统的流程，则进行覆盖
                    // 如果备注栏里面有数据，但是数据库中没有该条目，则表示该资产已经走完本系统到动态，正在实现从动态到本系统的流程。
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    //
                    // 首先判断备注栏是否有数据
                    //
                    if (string.IsNullOrEmpty(assetsExt.Memo))
                    {
                        //
                        // 如果备注栏没有数据则表示没有走本库到动态的对接流程
                        // 对动态编码进行判空，如果动态编码为空，则将动态编码填写"DSINEST"并新增该条资产
                        //
                        if (assetsExt.UsedNum1 == "")
                        {
                            //
                            // 动态编码为空
                            //
                            assetsExt.UsedNum1 = "DSINEST";

                            //
                            // 新增该条资产
                            //
                            var _Catnum = _assetsService.GetNextAssetsNum(assetsExt.CatCode);
                            assetsExt.AssetsNum = string.Format("{0}{1}", assetsExt.CatCode, _Catnum.PadLeft(6, '0'));

                            //
                            // 默认可领用可借用
                            //
                            assetsExt.IsBorrow = 1;
                            assetsExt.IsUse = 1;
                            assetsExt.ImportId = crntId; //导入id
                            assetsExt.ImportType = crntType; //导入类型 动态

                            #region 增加

                            switch (assetsExt.AssetsType)
                            {
                                case AssetsTypes.Animalandplant:
                                    _assetsService.SaveAssets(assetsExt,
                                        assetsExt.ExtAttribute as AnimalandplantAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.Book:
                                    _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as BooksAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.Building:
                                    _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as BuildingAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.Car:
                                    _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as CarAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.Culturalrelic:
                                    _assetsService.SaveAssets(assetsExt,
                                        assetsExt.ExtAttribute as CulturalrelicAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.GeneralEquipment:
                                    _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as GeneralAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.House:
                                    _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as HouseAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.Land:
                                    _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as LandAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.SpecialEquipment:
                                    _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as SpecialAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.Furniture:
                                    _assetsService.SaveAssets(assetsExt);
                                    result.Imported.Add(assetsExt);
                                    break;
                                default:
                                    result.UnImported.Add(new AssetsMainMsg(assetsExt)
                                    {
                                        Message = "错误分类代码 " + assetsExt.AssetsNum
                                    });
                                    break;
                            }

                            #endregion

                            _assetsService.UpdateNextAssetsNum(assetsExt.CatCode);

                        }
                        else
                        {
                            //
                            // 如果备注栏没有数据，但是存在动态编码
                            // 对动态编码进行判重，如果数据库存在，则覆盖
                            //
                            if (_assetsService.IsExistByAssetsNum(assetsExt.UsedNum1))
                            {
                                //
                                // 数据库存在该动态编码，则覆盖
                                // 首先将已存在条目查出来
                                // 
                                AssetsMain _assetsMain = _assetsService.GetByDynamicNum(assetsExt.UsedNum1);
                                // 根据动态编码从主表中查询公共属性

                                assetsExt.EntityId = _assetsMain.EntityId;
                                assetsExt.AssetsNum = _assetsMain.AssetsNum;
                                assetsExt.ImportId = crntId; //导入id
                                assetsExt.ImportType = crntType; //导入类型 办学

                                //
                                // 默认可领用可借用
                                //
                                assetsExt.IsBorrow = 1;
                                assetsExt.IsUse = 1;

                                #region 覆盖

                                //
                                // 根据资产类别去附表查询附加属性
                                //
                                switch (assetsExt.AssetsType)
                                {
                                    case AssetsTypes.Animalandplant:
                                        AnimalandplantAttribute _animalandplantAttribute =
                                            _assetsService.GetAnimalandplant(_assetsMain.AssetsNum);

                                        var _attr = assetsExt.ExtAttribute as AnimalandplantAttribute;
                                        //
                                        // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                        //
                                        if (_animalandplantAttribute == null)
                                        {
                                            _attr.AssetsNum = assetsExt.AssetsNum;
                                            _assetsService.SaveAssets(assetsExt, _attr);
                                            result.Imported.Add(assetsExt);
                                        }
                                        else
                                        {
                                            _animalandplantAttribute.Birthday = _attr.Birthday;
                                            _animalandplantAttribute.Age = _attr.Age;
                                            _animalandplantAttribute.Class = _attr.Class;
                                            _animalandplantAttribute.Palce = _attr.Palce;
                                            _animalandplantAttribute.EntityId = _animalandplantAttribute.EntityId;

                                            _assetsService.SaveAssets(assetsExt, _animalandplantAttribute);
                                            result.Imported.Add(assetsExt);
                                        }
                                        break;
                                    case AssetsTypes.Book:
                                        BooksAttribute _booksAttribute =
                                            _assetsService.GetBook(_assetsMain.AssetsNum);

                                        var _bookattr = assetsExt.ExtAttribute as BooksAttribute;
                                        //
                                        // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                        //
                                        if (_booksAttribute == null)
                                        {
                                            _bookattr.AssetsNum = assetsExt.AssetsNum;
                                            _assetsService.SaveAssets(assetsExt, _bookattr);
                                            result.Imported.Add(assetsExt);
                                        }
                                        else
                                        {
                                            _booksAttribute.UseDate = _bookattr.UseDate;
                                            _booksAttribute.FileNum = _bookattr.FileNum;
                                            _booksAttribute.Press = _bookattr.Press;
                                            _booksAttribute.StoreYears = _bookattr.StoreYears;
                                            _booksAttribute.PressDate = _bookattr.PressDate;

                                            _assetsService.SaveAssets(assetsExt, _booksAttribute);
                                            result.Imported.Add(assetsExt);
                                        }
                                        break;
                                    case AssetsTypes.Building:
                                        BuildingAttribute _buildingAttribute =
                                            _assetsService.GetBuilding(_assetsMain.AssetsNum);

                                        var _buildattr = assetsExt.ExtAttribute as BuildingAttribute;
                                        //
                                        // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                        //
                                        if (_buildingAttribute == null)
                                        {
                                            _buildattr.AssetsNum = assetsExt.AssetsNum;
                                            _assetsService.SaveAssets(assetsExt, _buildattr);
                                            result.Imported.Add(assetsExt);
                                        }
                                        else
                                        {
                                            _buildingAttribute.PropertyForm = _buildattr.PropertyForm;
                                            _buildingAttribute.OwnershipCertifiateNum = _buildattr.OwnershipCertifiateNum;
                                            _buildingAttribute.Area = _buildattr.Area;
                                            _buildingAttribute.Location = _buildattr.Location;

                                            _assetsService.SaveAssets(assetsExt, _buildingAttribute);
                                            result.Imported.Add(assetsExt);
                                        }
                                        break;
                                    case AssetsTypes.Car:
                                        CarAttribute _carAttribute =
                                            _assetsService.GetCar(_assetsMain.AssetsNum);

                                        var _carattr = assetsExt.ExtAttribute as CarAttribute;
                                        //
                                        // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                        //
                                        if (_carAttribute == null)
                                        {
                                            _carattr.AssetsNum = assetsExt.AssetsNum;
                                            _assetsService.SaveAssets(assetsExt, _carattr);
                                            result.Imported.Add(assetsExt);
                                        }
                                        else
                                        {
                                            _carAttribute.CarUser = _carattr.CarUser;
                                            _carAttribute.UsingType = _carattr.UsingType;
                                            _carAttribute.Organization = _carattr.Organization;
                                            _carAttribute.CarNum = _carattr.CarNum;
                                            _carAttribute.CarRecCode = _carattr.CarRecCode;
                                            _carAttribute.CarCertificateDate = _carattr.CarCertificateDate;
                                            _carAttribute.CarExhaust = _carattr.CarExhaust;
                                            _carAttribute.CarEngineNum = _carattr.CarEngineNum;
                                            _carAttribute.CarErollDare = _carattr.CarErollDare;
                                            _carAttribute.CarOwner = _carattr.CarOwner;
                                            _carAttribute.CarBrand = _carattr.CarBrand;

                                            _assetsService.SaveAssets(assetsExt, _carAttribute);
                                            result.Imported.Add(assetsExt);
                                        }
                                        break;
                                    case AssetsTypes.Culturalrelic:
                                        CulturalrelicAttribute _culturalrelicAttribute =
                                            _assetsService.GetCulturalrelic(_assetsMain.AssetsNum);

                                        var _cultural = assetsExt.ExtAttribute as CulturalrelicAttribute;
                                        //
                                        // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                        //
                                        if (_culturalrelicAttribute == null)
                                        {
                                            _cultural.AssetsNum = assetsExt.AssetsNum;
                                            _assetsService.SaveAssets(assetsExt, _cultural);
                                            result.Imported.Add(assetsExt);
                                        }
                                        else
                                        {
                                            _culturalrelicAttribute.GoodsLevel = _cultural.GoodsLevel;
                                            _culturalrelicAttribute.Years = _cultural.Years;
                                            _culturalrelicAttribute.SourcePlace = _cultural.SourcePlace;

                                            _assetsService.SaveAssets(assetsExt, _culturalrelicAttribute);
                                            result.Imported.Add(assetsExt);
                                        }
                                        break;
                                    case AssetsTypes.GeneralEquipment:
                                        GeneralAttribute _generalAttribute =
                                            _assetsService.GetGeneral(_assetsMain.AssetsNum);

                                        var _general = assetsExt.ExtAttribute as GeneralAttribute;
                                        //
                                        // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                        //
                                        if (_generalAttribute == null)
                                        {
                                            _general.AssetsNum = assetsExt.AssetsNum;
                                            _assetsService.SaveAssets(assetsExt, _general);
                                            result.Imported.Add(assetsExt);

                                        }
                                        else
                                        {
                                            _generalAttribute.DevUse = _general.DevUse;
                                            _generalAttribute.DevFactory = _general.DevFactory;
                                            _generalAttribute.WarrantyDate = _general.WarrantyDate;
                                            _generalAttribute.WarrantyPhone = _general.WarrantyPhone;

                                            _assetsService.SaveAssets(assetsExt, _generalAttribute);
                                            result.Imported.Add(assetsExt);
                                        }
                                        break;
                                    case AssetsTypes.House:
                                        HouseAttribute _houseAttribute =
                                            _assetsService.GetHouse(_assetsMain.AssetsNum);

                                        var _house = assetsExt.ExtAttribute as HouseAttribute;
                                        //
                                        // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                        //
                                        if (_houseAttribute == null)
                                        {
                                            _house.AssetsNum = assetsExt.AssetsNum;
                                            _assetsService.SaveAssets(assetsExt, _house);
                                            result.Imported.Add(assetsExt);
                                        }
                                        else
                                        {
                                            _houseAttribute.PropertyForm = _house.PropertyForm;
                                            _houseAttribute.OwnershipCertificate = _house.OwnershipCertificate;
                                            _houseAttribute.OwnershipCertifiateNum = _house.OwnershipCertifiateNum;
                                            _houseAttribute.OwnershipTime = _house.OwnershipTime;
                                            _houseAttribute.IssuingDate = _house.IssuingDate;
                                            _houseAttribute.HouseOwner = _house.HouseOwner;
                                            _houseAttribute.DesignUse = _house.DesignUse;
                                            _houseAttribute.BuildingStruccture = _house.BuildingStruccture;
                                            _houseAttribute.Location = _house.Location;
                                            _houseAttribute.Area = _house.Area;

                                            _assetsService.SaveAssets(assetsExt, _houseAttribute);
                                            result.Imported.Add(assetsExt);
                                        }

                                        break;
                                    case AssetsTypes.Land:
                                        LandAttribute _landAttribute =
                                            _assetsService.GetLand(_assetsMain.AssetsNum);

                                        var _land = assetsExt.ExtAttribute as LandAttribute;
                                        //
                                        // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                        //
                                        if (_landAttribute == null)
                                        {
                                            _land.AssetsNum = assetsExt.AssetsNum;
                                            _assetsService.SaveAssets(assetsExt, _land);
                                            result.Imported.Add(assetsExt);
                                        }
                                        else
                                        {
                                            _landAttribute.PropertyForm = _land.PropertyForm;
                                            _landAttribute.OwnershipCertificate = _land.OwnershipCertificate;
                                            _landAttribute.OwnershipStyle = _land.OwnershipStyle;
                                            _landAttribute.OwnershipTime = _land.OwnershipTime;
                                            _landAttribute.OwnershipCertifiateNum = _land.OwnershipCertifiateNum;
                                            _landAttribute.IssuingDate = _land.IssuingDate;
                                            _landAttribute.LandOwner = _land.LandOwner;
                                            _landAttribute.LandUserStyle = _land.LandUserStyle;
                                            _landAttribute.Location = _land.Location;
                                            _landAttribute.AllArea = _land.AllArea;
                                            _landAttribute.SharingArea = _land.SharingArea;
                                            _landAttribute.SingleArea = _land.SingleArea;

                                            _assetsService.SaveAssets(assetsExt, _landAttribute);
                                            result.Imported.Add(assetsExt);
                                        }
                                        break;
                                    case AssetsTypes.SpecialEquipment:
                                        SpecialAttribute _specialAttribute =
                                            _assetsService.GetSpecial(_assetsMain.AssetsNum);

                                        var _special = assetsExt.ExtAttribute as SpecialAttribute;
                                        //
                                        // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                        //
                                        if (_specialAttribute == null)
                                        {
                                            _special.AssetsNum = assetsExt.AssetsNum;
                                            _assetsService.SaveAssets(assetsExt, _special);
                                            result.Imported.Add(assetsExt);
                                        }
                                        else
                                        {
                                            _specialAttribute.DevFactory = _special.DevFactory;
                                            _specialAttribute.WarrantyDate = _special.WarrantyDate;
                                            _specialAttribute.WarrantyPhone = _special.WarrantyPhone;
                                            _specialAttribute.SpecialBrand = _special.SpecialBrand;

                                            _assetsService.SaveAssets(assetsExt, _specialAttribute);
                                            result.Imported.Add(assetsExt);
                                        }
                                        break;
                                    case AssetsTypes.Furniture:
                                        //_assetsService.SaveAssets(assetsExt);
                                        //break;


                                        //
                                        // 除房屋以外，数量大于1的资产全部都拆分成单个资产
                                        // 拆分后资产编码相同，账面数量、账面价值、账面净值全部要处理
                                        //
                                        int _tempCount = assetsExt.Counts;
                                        //List<AssetsMain> assetsMainExts = new List<AssetsMain>();
                                        //AssetsMain assetsMainCach = new AssetsMain();
                                        if (_tempCount > 1)
                                        {
                                            assetsExt.Counts = 1;
                                            assetsExt.Money = assetsExt.Money / _tempCount;
                                            assetsExt.NetWorth = assetsExt.Money;
                                            assetsExt.InputTime = DateTime.Now;
                                            assetsExt.ExtDataTable = "ASSETS_MAIN";
                                            string _temp = assetsExt.AssetsNum.Substring(0, 15);
                                            for (int i = 1; i <= _tempCount; i++)
                                            {
                                                assetsExt.AssetsNum = _temp + "@" + i.ToString();
                                                //
                                                // 因为是覆盖，所以要找到原资产进行删除
                                                //
                                                AssetsMain _AssetsMain = _assetsService.GetByDynamicNum(assetsExt.UsedNum1); // 根据资产编号去数据库中查询
                                                if (_AssetsMain != null) // 判断这条数据是否存在主表中，则将原数据删除
                                                {
                                                    _assetsService.Delete(_AssetsMain.EntityId); // 删除原数据
                                                    _assetsService.InsertAssets(assetsExt);// 写入拆分后的新数据
                                                    Thread.Sleep(20); // 加延迟 防止数据库写不过来
                                                }
                                                else
                                                {
                                                    _assetsService.SaveAssets(assetsExt);
                                                    Thread.Sleep(20); // 加延迟 防止数据库写不过来
                                                    result.Imported.Add(assetsExt);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            _assetsService.SaveAssets(assetsExt);
                                            result.Imported.Add(assetsExt);
                                        }
                                        break;
                                    default:
                                        result.UnImported.Add(new AssetsMainMsg(assetsExt)
                                        {
                                            Message = "错误分类代码 " + assetsExt.AssetsNum
                                        });
                                        break;
                                }

                                #endregion

                            }
                            else
                            {
                                //
                                // 如果动态编码不存在，则新增
                                //
                                var _num = _assetsService.GetNextAssetsNum(assetsExt.CatCode);
                                assetsExt.AssetsNum = string.Format("{0}{1}", assetsExt.CatCode, _num.PadLeft(6, '0'));

                                //
                                // 默认可领用可借用
                                //
                                assetsExt.IsBorrow = 1;
                                assetsExt.IsUse = 1;
                                assetsExt.ImportId = crntId; //导入id
                                assetsExt.ImportType = crntType; //导入类型 动态


                                #region 增加

                                switch (assetsExt.AssetsType)
                                {
                                    case AssetsTypes.Animalandplant:
                                        _assetsService.SaveAssets(assetsExt,
                                            assetsExt.ExtAttribute as AnimalandplantAttribute);
                                        result.Imported.Add(assetsExt);
                                        break;
                                    case AssetsTypes.Book:
                                        _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as BooksAttribute);
                                        result.Imported.Add(assetsExt);
                                        break;
                                    case AssetsTypes.Building:
                                        _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as BuildingAttribute);
                                        result.Imported.Add(assetsExt);
                                        break;
                                    case AssetsTypes.Car:
                                        _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as CarAttribute);
                                        result.Imported.Add(assetsExt);
                                        break;
                                    case AssetsTypes.Culturalrelic:
                                        _assetsService.SaveAssets(assetsExt,
                                            assetsExt.ExtAttribute as CulturalrelicAttribute);
                                        result.Imported.Add(assetsExt);
                                        break;
                                    case AssetsTypes.GeneralEquipment:
                                        _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as GeneralAttribute);
                                        result.Imported.Add(assetsExt);
                                        break;
                                    case AssetsTypes.House:
                                        _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as HouseAttribute);
                                        result.Imported.Add(assetsExt);
                                        break;
                                    case AssetsTypes.Land:
                                        _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as LandAttribute);
                                        result.Imported.Add(assetsExt);
                                        break;
                                    case AssetsTypes.SpecialEquipment:
                                        _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as SpecialAttribute);
                                        result.Imported.Add(assetsExt);
                                        break;
                                    case AssetsTypes.Furniture:
                                        //
                                        // 除房屋以外，数量大于1的资产全部都拆分成单个资产
                                        // 拆分后资产编码相同，账面数量、账面价值、账面净值全部要处理
                                        //
                                        int _tempCount = assetsExt.Counts;
                                        //List<AssetsMain> assetsMainExts = new List<AssetsMain>();
                                        //AssetsMain assetsMainCach = new AssetsMain();
                                        if (_tempCount > 1)
                                        {
                                            assetsExt.Counts = 1;
                                            assetsExt.Money = assetsExt.Money / _tempCount;
                                            assetsExt.NetWorth = assetsExt.Money;
                                            assetsExt.InputTime = DateTime.Now;
                                            assetsExt.ExtDataTable = "ASSETS_MAIN";
                                            string _temp = assetsExt.AssetsNum;
                                            for (int i = 1; i <= _tempCount; i++)
                                            {

                                                assetsExt.AssetsNum = _temp + "@" + i.ToString();
                                                //assetsMainCach = assetsExt;
                                                //assetsMainExts.Add(assetsMainCach);
                                                //assetsMainCach = null;
                                                Thread.Sleep(100);
                                                _assetsService.InsertAssets(assetsExt);

                                                result.Imported.Add(assetsExt);
                                            }

                                            //_assetsService.InsertAssetsEx(assetsMainExts);
                                        }
                                        else
                                        {
                                            _assetsService.SaveAssets(assetsExt);
                                            result.Imported.Add(assetsExt);
                                        }
                                        //_assetsService.SaveAssets(assetsExt);
                                        //result.Imported.Add(assetsExt);
                                        break;
                                    default:
                                        result.UnImported.Add(new AssetsMainMsg(assetsExt)
                                        {
                                            Message = "错误分类代码 " + assetsExt.AssetsNum
                                        });
                                        break;
                                }

                                #endregion

                                _assetsService.UpdateNextAssetsNum(assetsExt.CatCode);
                            }
                        }
                    }
                    else
                    {
                        //
                        // 备注栏里面有数据
                        // 则需要对数据进行查询，如果在数据库中存在则表示
                        // 该资产已经完成了系统和动态的对接 
                        // 
                        if (_assetsService.IsExistByDynamicMemo(assetsExt.Memo))
                        {
                            //
                            // 数据库存在导入表备注的系统资产编码则覆盖该条目
                            // 首先将已存在条目查出来
                            // 
                            AssetsMain _assetsMain = _assetsService.GetBySysNum(assetsExt.Memo);
                            // 根据备注中的系统资产编码从主表中查询公共属性

                            assetsExt.EntityId = _assetsMain.EntityId;
                            assetsExt.AssetsNum = _assetsMain.AssetsNum;
                            assetsExt.ImportId = crntId; //导入id
                            assetsExt.ImportType = crntType; //导入类型 办学

                            //
                            // 默认可领用可借用
                            //
                            assetsExt.IsBorrow = 1;
                            assetsExt.IsUse = 1;

                            #region 覆盖

                            //
                            // 根据资产类别去附表查询附加属性
                            //
                            switch (assetsExt.AssetsType)
                            {
                                case AssetsTypes.Animalandplant:
                                    AnimalandplantAttribute _animalandplantAttribute =
                                        _assetsService.GetAnimalandplant(_assetsMain.AssetsNum);

                                    var _attr = assetsExt.ExtAttribute as AnimalandplantAttribute;
                                    //
                                    // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                    //
                                    if (_animalandplantAttribute == null)
                                    {
                                        _attr.AssetsNum = assetsExt.AssetsNum;
                                        _assetsService.SaveAssets(assetsExt, _attr);
                                        result.Imported.Add(assetsExt);
                                    }
                                    else
                                    {
                                        _animalandplantAttribute.Birthday = _attr.Birthday;
                                        _animalandplantAttribute.Age = _attr.Age;
                                        _animalandplantAttribute.Class = _attr.Class;
                                        _animalandplantAttribute.Palce = _attr.Palce;
                                        _animalandplantAttribute.EntityId = _animalandplantAttribute.EntityId;

                                        _assetsService.SaveAssets(assetsExt, _animalandplantAttribute);
                                        result.Imported.Add(assetsExt);
                                    }
                                    break;
                                case AssetsTypes.Book:
                                    BooksAttribute _booksAttribute =
                                        _assetsService.GetBook(_assetsMain.AssetsNum);

                                    var _bookattr = assetsExt.ExtAttribute as BooksAttribute;
                                    //
                                    // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                    //
                                    if (_booksAttribute == null)
                                    {
                                        _bookattr.AssetsNum = assetsExt.AssetsNum;
                                        _assetsService.SaveAssets(assetsExt, _bookattr);
                                        result.Imported.Add(assetsExt);
                                    }
                                    else
                                    {
                                        _booksAttribute.UseDate = _bookattr.UseDate;
                                        _booksAttribute.FileNum = _bookattr.FileNum;
                                        _booksAttribute.Press = _bookattr.Press;
                                        _booksAttribute.StoreYears = _bookattr.StoreYears;
                                        _booksAttribute.PressDate = _bookattr.PressDate;

                                        _assetsService.SaveAssets(assetsExt, _booksAttribute);
                                        result.Imported.Add(assetsExt);
                                    }
                                    break;
                                case AssetsTypes.Building:
                                    BuildingAttribute _buildingAttribute =
                                        _assetsService.GetBuilding(_assetsMain.AssetsNum);

                                    var _buildattr = assetsExt.ExtAttribute as BuildingAttribute;
                                    //
                                    // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                    //
                                    if (_buildingAttribute == null)
                                    {
                                        _buildattr.AssetsNum = assetsExt.AssetsNum;
                                        _assetsService.SaveAssets(assetsExt, _buildattr);
                                        result.Imported.Add(assetsExt);
                                    }
                                    else
                                    {
                                        _buildingAttribute.PropertyForm = _buildattr.PropertyForm;
                                        _buildingAttribute.OwnershipCertifiateNum = _buildattr.OwnershipCertifiateNum;
                                        _buildingAttribute.Area = _buildattr.Area;
                                        _buildingAttribute.Location = _buildattr.Location;

                                        _assetsService.SaveAssets(assetsExt, _buildingAttribute);
                                        result.Imported.Add(assetsExt);
                                    }
                                    break;
                                case AssetsTypes.Car:
                                    CarAttribute _carAttribute =
                                        _assetsService.GetCar(_assetsMain.AssetsNum);

                                    var _carattr = assetsExt.ExtAttribute as CarAttribute;
                                    //
                                    // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                    //
                                    if (_carAttribute == null)
                                    {
                                        _carattr.AssetsNum = assetsExt.AssetsNum;
                                        _assetsService.SaveAssets(assetsExt, _carattr);
                                        result.Imported.Add(assetsExt);
                                    }
                                    else
                                    {
                                        _carAttribute.CarUser = _carattr.CarUser;
                                        _carAttribute.UsingType = _carattr.UsingType;
                                        _carAttribute.Organization = _carattr.Organization;
                                        _carAttribute.CarNum = _carattr.CarNum;
                                        _carAttribute.CarRecCode = _carattr.CarRecCode;
                                        _carAttribute.CarCertificateDate = _carattr.CarCertificateDate;
                                        _carAttribute.CarExhaust = _carattr.CarExhaust;
                                        _carAttribute.CarEngineNum = _carattr.CarEngineNum;
                                        _carAttribute.CarErollDare = _carattr.CarErollDare;
                                        _carAttribute.CarOwner = _carattr.CarOwner;
                                        _carAttribute.CarBrand = _carattr.CarBrand;

                                        _assetsService.SaveAssets(assetsExt, _carAttribute);
                                        result.Imported.Add(assetsExt);
                                    }
                                    break;
                                case AssetsTypes.Culturalrelic:
                                    CulturalrelicAttribute _culturalrelicAttribute =
                                        _assetsService.GetCulturalrelic(_assetsMain.AssetsNum);

                                    var _cultural = assetsExt.ExtAttribute as CulturalrelicAttribute;
                                    //
                                    // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                    //
                                    if (_culturalrelicAttribute == null)
                                    {
                                        _cultural.AssetsNum = assetsExt.AssetsNum;
                                        _assetsService.SaveAssets(assetsExt, _cultural);
                                        result.Imported.Add(assetsExt);
                                    }
                                    else
                                    {
                                        _culturalrelicAttribute.GoodsLevel = _cultural.GoodsLevel;
                                        _culturalrelicAttribute.Years = _cultural.Years;
                                        _culturalrelicAttribute.SourcePlace = _cultural.SourcePlace;

                                        _assetsService.SaveAssets(assetsExt, _culturalrelicAttribute);
                                        result.Imported.Add(assetsExt);
                                    }
                                    break;
                                case AssetsTypes.GeneralEquipment:
                                    GeneralAttribute _generalAttribute =
                                        _assetsService.GetGeneral(_assetsMain.AssetsNum);

                                    var _general = assetsExt.ExtAttribute as GeneralAttribute;
                                    //
                                    // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                    //
                                    if (_generalAttribute == null)
                                    {
                                        _general.AssetsNum = assetsExt.AssetsNum;
                                        _assetsService.SaveAssets(assetsExt, _general);
                                        result.Imported.Add(assetsExt);

                                    }
                                    else
                                    {
                                        _generalAttribute.DevUse = _general.DevUse;
                                        _generalAttribute.DevFactory = _general.DevFactory;
                                        _generalAttribute.WarrantyDate = _general.WarrantyDate;
                                        _generalAttribute.WarrantyPhone = _general.WarrantyPhone;

                                        _assetsService.SaveAssets(assetsExt, _generalAttribute);
                                        result.Imported.Add(assetsExt);
                                    }
                                    break;
                                case AssetsTypes.House:
                                    HouseAttribute _houseAttribute =
                                        _assetsService.GetHouse(_assetsMain.AssetsNum);

                                    var _house = assetsExt.ExtAttribute as HouseAttribute;
                                    //
                                    // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                    //
                                    if (_houseAttribute == null)
                                    {
                                        _house.AssetsNum = assetsExt.AssetsNum;
                                        _assetsService.SaveAssets(assetsExt, _house);
                                        result.Imported.Add(assetsExt);
                                    }
                                    else
                                    {
                                        _houseAttribute.PropertyForm = _house.PropertyForm;
                                        _houseAttribute.OwnershipCertificate = _house.OwnershipCertificate;
                                        _houseAttribute.OwnershipCertifiateNum = _house.OwnershipCertifiateNum;
                                        _houseAttribute.OwnershipTime = _house.OwnershipTime;
                                        _houseAttribute.IssuingDate = _house.IssuingDate;
                                        _houseAttribute.HouseOwner = _house.HouseOwner;
                                        _houseAttribute.DesignUse = _house.DesignUse;
                                        _houseAttribute.BuildingStruccture = _house.BuildingStruccture;
                                        _houseAttribute.Location = _house.Location;
                                        _houseAttribute.Area = _house.Area;

                                        _assetsService.SaveAssets(assetsExt, _houseAttribute);
                                        result.Imported.Add(assetsExt);
                                    }

                                    break;
                                case AssetsTypes.Land:
                                    LandAttribute _landAttribute =
                                        _assetsService.GetLand(_assetsMain.AssetsNum);

                                    var _land = assetsExt.ExtAttribute as LandAttribute;
                                    //
                                    // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                    //
                                    if (_landAttribute == null)
                                    {
                                        _land.AssetsNum = assetsExt.AssetsNum;
                                        _assetsService.SaveAssets(assetsExt, _land);
                                        result.Imported.Add(assetsExt);
                                    }
                                    else
                                    {
                                        _landAttribute.PropertyForm = _land.PropertyForm;
                                        _landAttribute.OwnershipCertificate = _land.OwnershipCertificate;
                                        _landAttribute.OwnershipStyle = _land.OwnershipStyle;
                                        _landAttribute.OwnershipTime = _land.OwnershipTime;
                                        _landAttribute.OwnershipCertifiateNum = _land.OwnershipCertifiateNum;
                                        _landAttribute.IssuingDate = _land.IssuingDate;
                                        _landAttribute.LandOwner = _land.LandOwner;
                                        _landAttribute.LandUserStyle = _land.LandUserStyle;
                                        _landAttribute.Location = _land.Location;
                                        _landAttribute.AllArea = _land.AllArea;
                                        _landAttribute.SharingArea = _land.SharingArea;
                                        _landAttribute.SingleArea = _land.SingleArea;

                                        _assetsService.SaveAssets(assetsExt, _landAttribute);
                                        result.Imported.Add(assetsExt);
                                    }
                                    break;
                                case AssetsTypes.SpecialEquipment:
                                    SpecialAttribute _specialAttribute =
                                        _assetsService.GetSpecial(_assetsMain.AssetsNum);

                                    var _special = assetsExt.ExtAttribute as SpecialAttribute;
                                    //
                                    // 判断是否从子表中获取到数据，如果没有则添加到子表中
                                    //
                                    if (_specialAttribute == null)
                                    {
                                        _special.AssetsNum = assetsExt.AssetsNum;
                                        _assetsService.SaveAssets(assetsExt, _special);
                                        result.Imported.Add(assetsExt);
                                    }
                                    else
                                    {
                                        _specialAttribute.DevFactory = _special.DevFactory;
                                        _specialAttribute.WarrantyDate = _special.WarrantyDate;
                                        _specialAttribute.WarrantyPhone = _special.WarrantyPhone;
                                        _specialAttribute.SpecialBrand = _special.SpecialBrand;

                                        _assetsService.SaveAssets(assetsExt, _specialAttribute);
                                        result.Imported.Add(assetsExt);
                                    }
                                    break;
                                case AssetsTypes.Furniture:
                                    _assetsService.SaveAssets(assetsExt);
                                    break;
                                default:
                                    result.UnImported.Add(new AssetsMainMsg(assetsExt)
                                    {
                                        Message = "错误分类代码 " + assetsExt.AssetsNum
                                    });
                                    break;
                            }

                            #endregion

                        }
                        else
                        {
                            //
                            // 数据库不存在导入表备注的系统资产编码则新增该条目
                            // 动态编码填充
                            //
                            assetsExt.UsedNum1 = assetsExt.UsedNum1;

                            //
                            // 新增该条资产
                            //
                            //var num = _assetsService.GetNextAssetsNum(assetsExt.CatCode);
                            assetsExt.AssetsNum = assetsExt.Memo;

                            //
                            // 默认可领用可借用
                            //
                            assetsExt.IsBorrow = 1;
                            assetsExt.IsUse = 1;
                            assetsExt.ImportId = crntId; //导入id
                            assetsExt.ImportType = crntType; //导入类型 动态

                            #region 增加

                            switch (assetsExt.AssetsType)
                            {
                                case AssetsTypes.Animalandplant:
                                    _assetsService.SaveAssets(assetsExt,
                                        assetsExt.ExtAttribute as AnimalandplantAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.Book:
                                    _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as BooksAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.Building:
                                    _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as BuildingAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.Car:
                                    _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as CarAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.Culturalrelic:
                                    _assetsService.SaveAssets(assetsExt,
                                        assetsExt.ExtAttribute as CulturalrelicAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.GeneralEquipment:
                                    _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as GeneralAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.House:
                                    _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as HouseAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.Land:
                                    _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as LandAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.SpecialEquipment:
                                    _assetsService.SaveAssets(assetsExt, assetsExt.ExtAttribute as SpecialAttribute);
                                    result.Imported.Add(assetsExt);
                                    break;
                                case AssetsTypes.Furniture:
                                    _assetsService.SaveAssets(assetsExt);
                                    result.Imported.Add(assetsExt);
                                    break;
                                default:
                                    result.UnImported.Add(new AssetsMainMsg(assetsExt)
                                    {
                                        Message = "错误分类代码 " + assetsExt.AssetsNum
                                    });
                                    break;
                            }

                            #endregion

                            _assetsService.UpdateNextAssetsNum(assetsExt.CatCode);
                        }
                    }
                }
                //
                // 如果数据导入异常则记录导入异常问题
                //
                catch (Exception ex)
                {
                    //
                    // 保存异常信息
                    //
                    unInportAssets.Exception = ex.ToString();
                    unInportAssets.GoodsName = assetsExt.GoodsName;
                    unInportAssets.DynamicNum = assetsExt.UsedNum1;
                    //unInportAssets.TableName = data.TableName;
                    unInportAssets.TableRow = idxErr;
                    unInportAssets.ImportType = 1;//(int) assetsMain.ImportType;
                    unInportAssets.ImportPerson = importHistoryService.LastHistory().UserId;
                    unInportAssets.ImportId = importHistoryService.LastHistory().EntityId;  //Id
                    unInportAssets.ImportType = importHistoryService.LastHistory().ImportType; //type
                    unInportAssets.ImportTime = importHistoryService.LastHistory().ImportTime;
                    unInportAssets.Exception = unInportAssets.Exception.Substring(0, 255);// += ex.ToString();
                    unImportAssetsService.Add(unInportAssets); //增加为导入资产 
                    idxErr++;
                    //
                    // 继续执行下一条记录
                    //
                    continue;
                }
                //进度
                percent = percent + 60.0 / list.Count;

                try
                {
                    importProgress.ImportedPercentVal = percentStep1 + (int)percent; ;
                    idx++;
                    importProgress.ImportedAssetsNum = idx;
                }
                catch (Exception)
                {

                }

            }
            return result;
        }

     
    }
}