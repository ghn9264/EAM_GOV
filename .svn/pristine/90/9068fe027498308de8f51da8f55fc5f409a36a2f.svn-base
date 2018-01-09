using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Repositories;
using EAM.Data.Services.Query;
using NPoco;

namespace EAM.Data.Services.Impl
{
    public class ImportHistoryService : IImportHistoryService
    {
        private readonly IImportHistoryRepository _importHistoryRep;
        private readonly IAssetsMainRepository _assetsMainRep;

        private readonly IAnimalRepository _animalRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly ICarRepository _carRepository;
        private readonly ICulturalrelicRepository _culturalrelicRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IGeneralRepository _generalRepository;
        private readonly IHouseRepository _houseRepository;
        private readonly ILandRepository _landRepository;
        private readonly ISpecialRepository _specialRepository;

        public ImportHistoryService(IImportHistoryRepository importHistoryRep, IAssetsMainRepository assetsMainRep, IAnimalRepository animalRepository, IBooksRepository booksRepository, ICarRepository carRepository, ICulturalrelicRepository culturalrelicRepository, IBuildingRepository buildingRepository, IGeneralRepository generalRepository, IHouseRepository houseRepository, ILandRepository landRepository, ISpecialRepository specialRepository)
        {
            _importHistoryRep = importHistoryRep;
            _assetsMainRep = assetsMainRep;
            _animalRepository = animalRepository;
            _booksRepository = booksRepository;
            _carRepository = carRepository;
            _culturalrelicRepository = culturalrelicRepository;
            _buildingRepository = buildingRepository;
            _generalRepository = generalRepository;
            _houseRepository = houseRepository;
            _landRepository = landRepository;
            _specialRepository = specialRepository;
        }

        public int UploadRateOfProgress { get; set; }
        public ImportHistory Add(ImportHistory item)
        {
            _importHistoryRep.Add(item);
            return item;
        }
        public int Remove(int id)
        {
          return  _importHistoryRep.Remove(id);
        }

        public int DeleteRecordAndData(int id)
        {
            try
            {
                //
                // 删除记录
                //
                _importHistoryRep.Remove(id);

                //
                // 删除记录对应的资产
                //
                var assetsList = _assetsMainRep.LogicDeleteEx(id);

                //
                // 删除资产相应的附属属性
                //
                foreach (var item in assetsList)
                {
                    //
                    // 根据资产分类代码获取资产分类名称
                    //
                    if (string.IsNullOrEmpty(item.CatCode))
                        return 1;
                    var @type = item.CatCode.GetAssetsTypeByCatCode();

                    switch (@type)
                    {
                        case AssetsTypes.Land:
                            return _landRepository.DeleteItemByAssetsNum("ASSETS_LAND", item.AssetsNum);
                            break;
                        case AssetsTypes.Car:
                            return _carRepository.DeleteItemByAssetsNum("ASSETS_CAR", item.AssetsNum);
                            break;
                        case AssetsTypes.Building:
                            return _buildingRepository.DeleteItemByAssetsNum("ASSETS_BUILDING", item.AssetsNum);
                            break;
                        case AssetsTypes.Culturalrelic:
                            return _culturalrelicRepository.DeleteItemByAssetsNum("ASSETS_CULTURALRELIC", item.AssetsNum);
                            break;
                        case AssetsTypes.Animalandplant:
                            return _animalRepository.DeleteItemByAssetsNum("ASSETS_ANIMAL", item.AssetsNum);
                            break;
                        case AssetsTypes.Furniture:
                            return 1;
                            break;
                        case AssetsTypes.GeneralEquipment:
                            return _generalRepository.DeleteItemByAssetsNum("ASSETS_GENERAL", item.AssetsNum);
                            break;
                        case AssetsTypes.House:
                            return _houseRepository.DeleteItemByAssetsNum("ASSETS_HOUSE", item.AssetsNum);
                            break;
                        case AssetsTypes.SpecialEquipment:
                            return _specialRepository.DeleteItemByAssetsNum("ASSETS_SPECIAL", item.AssetsNum);
                            break;
                        case AssetsTypes.Book:
                            return _booksRepository.DeleteItemByAssetsNum("ASSETS_BOOK", item.AssetsNum);
                            break;
                    }
                }

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int RemoveAll()
        { 
            return  _importHistoryRep.RemoveAll();
         }

        public void Update(ImportHistory item)
        {
            _importHistoryRep.Update(item);
        }

        public ImportHistory LastHistory(int importType)
        {
            var sql = @"SELECT * FROM assets_import_history WHERE IMPORT_TYPE=@0 ORDER BY ID DESC LIMIT 1";
            return _importHistoryRep.FirstOrDefault(sql, importType);
        }
        public ImportHistory LastHistory()
        {
            var sql = @"SELECT * FROM assets_import_history  ORDER BY ID DESC LIMIT 1";
            return _importHistoryRep.FirstOrDefault(sql);
        }

        public PagedList<ImportHistory> QueryPage(ImportHistoryQuery query)
        {
            return _importHistoryRep.PagedList(query.PageIndex, query.PageSize, query.QuerySql);
        }
        public PagedList<ImportHistory> QueryPage(AllRecordQuery query)
        {
            return _importHistoryRep.PagedList(query.PageIndex, query.PageSize, query.QuerySql);
        }

    

    }
}