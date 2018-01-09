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
    public class DtPImport
    {
        private IImportHistoryService _importHistoryService;
        private IUnImportAssetsService _unImportAssetsService;
        private ISystemService _sysService;//2017-06-05 wnn
        private IRoleService _roleService;//2017-05-31 wnn

        /// <summary>
        /// 位置导入 2017-06-05 wnn
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="importHistoryService"></param>
        /// <param name="unImportAssetsService"></param>
        public DtPImport(ISystemService sysService, IImportHistoryService importHistoryService, IUnImportAssetsService unImportAssetsService, IRoleService roleService)
        {
            _sysService = sysService;
            _importHistoryService = importHistoryService;
            _unImportAssetsService = unImportAssetsService;
            _roleService = roleService;
        }



        /// <summary>
        /// 将导入文件中的数据写入到数据库 2017-06-05 wnn
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="importProgress"></param>
        /// <returns></returns>
        public ImportResult DoPImport(string filePath, ref ProgressInfo importProgress, IImportHistoryService importHistoryService, IUnImportAssetsService unImportAssetsService, ISystemService sysService)
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
            var list = DtReader.ReadPlData(filePath, ref importProgress, _importHistoryService, _unImportAssetsService);

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
            foreach (Place place in list)
            {
                try
                {
                    var parentid = 0;
                    if (place.ParentName != "无")
                    {

                        var parentplace = _sysService.GetPlaceOne(place.ParentName);
                        parentid = parentplace.EntityId;
                    }

                    Place addPlace = new Place();

                    addPlace.ParentId = parentid;
                    addPlace.PlaceName = place.PlaceName;
                    addPlace.PlaceType = place.PlaceType;

                    var temp = _sysService.GetPlaceOne(place.PlaceName);

                    if (temp == null)
                    {
                        _sysService.AddPlace(addPlace);
                    }
                    else
                    {
                        continue;
                    }
                    //}
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
                    //unInportAssets.GoodsName = assetsExt.GoodsName;
                    //unInportAssets.DynamicNum = assetsExt.UsedNum1;
                    //unInportAssets.TableName = data.TableName;
                    unInportAssets.TableRow = idxErr;
                    unInportAssets.ImportType = 1;//(int) assetsMain.ImportType;
                    unInportAssets.ImportPerson = importHistoryService.LastHistory().UserId;
                    unInportAssets.ImportId = importHistoryService.LastHistory().EntityId;  //Id
                    unInportAssets.ImportType = importHistoryService.LastHistory().ImportType; //type
                    unInportAssets.ImportTime = importHistoryService.LastHistory().ImportTime;
                    unInportAssets.Exception = unInportAssets.Exception.Substring(0, 255);// += ex.ToString();
                    //unImportAssetsService.Add(unInportAssets); //增加为导入资产 
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
                catch (Exception ex)
                {

                }

            }
            return result;
        }
    }
}