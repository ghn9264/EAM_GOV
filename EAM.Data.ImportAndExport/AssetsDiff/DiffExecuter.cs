using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Domain;
using EAM.Data.ImportAndExport.Import;
using EAM.Data.Services;
using EAM.Data.Services.Query;

namespace EAM.Data.ImportAndExport.AssetsDiff
{
    /// <summary>
    /// 数据比对
    /// </summary>
    public class DiffExecuter
    {
        private readonly IAssetsService _assetsService;
        private ProgressInfo progress;
        private IUnImportAssetsService _unImportAssetsService;
        private IImportHistoryService _importHistoryService;
        public DiffExecuter(IAssetsService assetsService, IImportHistoryService importHistoryService, IUnImportAssetsService unImportAssetsService)
        {
            _assetsService = assetsService;
            _importHistoryService = importHistoryService;
            _unImportAssetsService = unImportAssetsService;
        }

        public List<DiffResult> Execute(string banxueFile, string dynamicFile,string diffBase)
        {

            //
            // 清空之前的比对结果
            //
            _assetsService.ClearDiffResult();

            //
            // 创建比对结果
            //
            var result = new List<DiffResult>();

            //
            // 获取本库数据
            //
            var localData = _assetsService.QueryPage(new AssetsQuery {PageIndex = 1, PageSize = int.MaxValue});

            //
            // 获取办学数据
            //
            List<AssetsMain> banxueData=null;
            if (!string.IsNullOrEmpty(banxueFile))
            {
                //double temp;
                banxueData = BxReader.ReadData(banxueFile,ref  progress, _importHistoryService, _unImportAssetsService);
                banxueData = banxueData ?? new List<AssetsMain>();
            }

            //
            // 获取动态数据
            //
            List<AssetsMainExt> dynamicData = null;
            if (!string.IsNullOrEmpty(dynamicFile))
            {
                dynamicData = DtReader.ReadData(dynamicFile, ref progress, _importHistoryService, _unImportAssetsService);
                dynamicData = dynamicData ?? new List<AssetsMainExt>();
            }

            //
            // 开始比对
            //
            switch (diffBase)
            {
                case "本库":
                    for (var index = 0; index < localData.Items.Count; index++)
                    {
                        AssetsMain item = localData.Items[index];
                        var diffItem = new DiffResult
                        {
                            Index = result.Count + 1,
                            LocalCode = item.AssetsNum,
                            LocalName = item.GoodsName,
                            LocalValue = item.Money,
                            LocalMode = item.ModelSpecification,
                            LocalState = "存在"
                        };

                        //
                        // 办学比对
                        // 首先看办学数据是否存在该库存号
                        //
                        bool _tempState = banxueData.Any(x => x.UsedNum2 == item.UsedNum2);

                        //
                        // 如果存在该库存号，则比对资产名称/价值/规格
                        // 上述三项中有任意一项不匹配则表示存在但是数有差异
                        //
                        if (_tempState)
                        {
                            //
                            // 取出该条数据
                            //
                            AssetsMain _banxueData = new AssetsMain();
                            _banxueData = banxueData.FirstOrDefault(x => x.UsedNum2 == item.UsedNum2);

                            //
                            // 比对资产名称/价值/规格
                            //
                            bool _allSame = true;

                            bool _nameSame = _banxueData.GoodsName == item.GoodsName;
                            bool _moneySame = _banxueData.Money == item.Money;
                            bool _modeSame = _banxueData.Brand == item.Brand;
                            _allSame = _allSame & _nameSame & _moneySame & _modeSame;

                            //
                            // 填充比对结果
                            //
                            diffItem.BxState = _allSame ? "精确匹配" : "存在但有差异";
                            diffItem.BxCode = _banxueData.UsedNum2;
                            diffItem.BxName = _banxueData.GoodsName;
                            diffItem.BxValue = _banxueData.Money;
                            diffItem.BxMode = _banxueData.Brand;
                        }
                        else
                        {
                            //
                            // 如果不存在该库存号，则无需对该资产进行办学比对
                            //
                            diffItem.BxState = "不存在";
                            diffItem.BxCode = "";
                            diffItem.BxName = "";
                            diffItem.BxValue = 0;
                            diffItem.BxMode = "";
                        }

                        //
                        // 动态比对
                        //
                        _tempState = dynamicData.Any(x => x.UsedNum2 == item.UsedNum2);

                        //
                        // 如果存在该库存号，则比对资产名称/价值/规格
                        // 上述三项中有任意一项不匹配则表示存在但是数有差异
                        //
                        if (_tempState)
                        {
                            //
                            // 取出该条数据
                            //
                            AssetsMain _dynamicData = new AssetsMain();
                            _dynamicData = dynamicData.FirstOrDefault(x => x.UsedNum2 == item.UsedNum2);

                            //
                            // 比对资产名称/价值/规格
                            //
                            bool _allSame = true;

                            bool _nameSame = _dynamicData.GoodsName == item.GoodsName;
                            bool _moneySame = _dynamicData.Money == item.Money;
                            bool _modeSame = _dynamicData.Brand == item.Brand;
                            _allSame = _allSame & _nameSame & _moneySame & _modeSame;

                            //
                            // 填充比对结果
                            //
                            diffItem.DtState = _allSame ? "精确匹配" : "存在但有差异";
                            diffItem.DtCode = _dynamicData.UsedNum2;
                            diffItem.DtName = _dynamicData.GoodsName;
                            diffItem.DtValue = _dynamicData.Money;
                            diffItem.DtMode = _dynamicData.Brand;
                        }
                        else
                        {
                            //
                            // 如果不存在该库存号，则无需对该资产进行办学比对
                            //
                            diffItem.DtState = "不存在";
                            diffItem.DtCode = "";
                            diffItem.DtName = "";
                            diffItem.DtValue = 0;
                            diffItem.DtMode = "";
                        }

                        //
                        // 总比对结果
                        //
                        if ((diffItem.LocalState == "存在") && (diffItem.BxState == "存在") && (diffItem.DtState == "存在"))
                        {
                            diffItem.DiffState = "匹配";
                        }
                        else
                        {
                            diffItem.DiffState = "不匹配";
                        }
                        //
                        // 添加到比对结果列表
                        //
                        result.Add(diffItem);
                    }
                    break;
                case "办学库":
                    for (var index = 0; index < banxueData.Count; index++)
                    {
                        AssetsMain item = banxueData[index];
                        var diffItem = new DiffResult
                        {
                            Index = result.Count + 1,
                            BxCode = item.AssetsNum,
                            BxName = item.GoodsName,
                            BxValue = item.Money,
                            BxMode = item.ModelSpecification,
                            BxState = "存在"
                        };

                        //
                        // 本库比对
                        // 首先看办学数据是否存在该库存号
                        //
                        bool _tempState = localData.Items.Any(x => x.UsedNum2 == item.UsedNum2);

                        //
                        // 如果存在该库存号，则比对资产名称/价值/规格
                        // 上述三项中有任意一项不匹配则表示存在但是数有差异
                        //
                        if (_tempState)
                        {
                            //
                            // 取出该条数据
                            //
                            AssetsMain _localData = new AssetsMain();
                            _localData = localData.Items.FirstOrDefault(x => x.UsedNum2 == item.UsedNum2);

                            //
                            // 比对资产名称/价值/规格
                            //
                            bool _allSame = true;

                            bool _nameSame = _localData.GoodsName == item.GoodsName;
                            bool _moneySame = _localData.Money == item.Money;
                            bool _modeSame = _localData.Brand == item.Brand;
                            _allSame = _allSame & _nameSame & _moneySame & _modeSame;

                            //
                            // 填充比对结果
                            //
                            diffItem.LocalState = _allSame ? "精确匹配" : "存在但有差异";
                            diffItem.LocalCode = _localData.UsedNum2;
                            diffItem.LocalName = _localData.GoodsName;
                            diffItem.LocalValue = _localData.Money;
                            diffItem.LocalMode = _localData.Brand;
                        }
                        else
                        {
                            //
                            // 如果不存在该库存号，则无需对该资产进行办学比对
                            //
                            diffItem.LocalState = "不存在";
                            diffItem.LocalCode = "";
                            diffItem.LocalName = "";
                            diffItem.LocalValue = 0;
                            diffItem.LocalMode = "";
                        }

                        //
                        // 动态比对
                        //
                        _tempState = dynamicData.Any(x => x.UsedNum2 == item.UsedNum2);

                        //
                        // 如果存在该库存号，则比对资产名称/价值/规格
                        // 上述三项中有任意一项不匹配则表示存在但是数有差异
                        //
                        if (_tempState)
                        {
                            //
                            // 取出该条数据
                            //
                            AssetsMain _dynamicData = new AssetsMain();
                            _dynamicData = dynamicData.FirstOrDefault(x => x.UsedNum2 == item.UsedNum2);

                            //
                            // 比对资产名称/价值/规格
                            //
                            bool _allSame = true;

                            bool _nameSame = _dynamicData.GoodsName == item.GoodsName;
                            bool _moneySame = _dynamicData.Money == item.Money;
                            bool _modeSame = _dynamicData.Brand == item.Brand;
                            _allSame = _allSame & _nameSame & _moneySame & _modeSame;

                            //
                            // 填充比对结果
                            //
                            diffItem.DtState = _allSame ? "精确匹配" : "存在但有差异";
                            diffItem.DtCode = _dynamicData.UsedNum2;
                            diffItem.DtName = _dynamicData.GoodsName;
                            diffItem.DtValue = _dynamicData.Money;
                            diffItem.DtMode = _dynamicData.Brand;
                        }
                        else
                        {
                            //
                            // 如果不存在该库存号，则无需对该资产进行办学比对
                            //
                            diffItem.DtState = "不存在";
                            diffItem.DtCode = "";
                            diffItem.DtName = "";
                            diffItem.DtValue = 0;
                            diffItem.DtMode = "";
                        }

                        //
                        // 总比对结果
                        //
                        if ((diffItem.LocalState == "存在") && (diffItem.BxState == "存在") && (diffItem.DtState == "存在"))
                        {
                            diffItem.DiffState = "匹配";
                        }
                        else
                        {
                            diffItem.DiffState = "不匹配";
                        }

                        //
                        // 添加到比对结果列表
                        //
                        result.Add(diffItem);
                    }
                    break;
                case "动态库":
                    for (var index = 0; index < dynamicData.Count; index++)
                    {
                        AssetsMain item = dynamicData[index];
                        var diffItem = new DiffResult
                        {
                            Index = result.Count + 1,
                            DtCode = item.AssetsNum,
                            DtName = item.GoodsName,
                            DtValue = item.Money,
                            DtMode = item.ModelSpecification,
                            DtState = "存在"
                        };

                        //
                        // 本库比对
                        // 首先看办学数据是否存在该库存号
                        //
                        bool _tempState = localData.Items.Any(x => x.UsedNum2 == item.UsedNum2);

                        //
                        // 如果存在该库存号，则比对资产名称/价值/规格
                        // 上述三项中有任意一项不匹配则表示存在但是数有差异
                        //
                        if (_tempState)
                        {
                            //
                            // 取出该条数据
                            //
                            AssetsMain _localData = new AssetsMain();
                            _localData = localData.Items.FirstOrDefault(x => x.UsedNum2 == item.UsedNum2);

                            //
                            // 比对资产名称/价值/规格
                            //
                            bool _allSame = true;

                            bool _nameSame = _localData.GoodsName == item.GoodsName;
                            bool _moneySame = _localData.Money == item.Money;
                            bool _modeSame = _localData.Brand == item.Brand;
                            _allSame = _allSame & _nameSame & _moneySame & _modeSame;

                            //
                            // 填充比对结果
                            //
                            diffItem.LocalState = _allSame ? "精确匹配" : "存在但有差异";
                            diffItem.LocalCode = _localData.UsedNum2;
                            diffItem.LocalName = _localData.GoodsName;
                            diffItem.LocalValue = _localData.Money;
                            diffItem.LocalMode = _localData.Brand;
                        }
                        else
                        {
                            //
                            // 如果不存在该库存号，则无需对该资产进行办学比对
                            //
                            diffItem.LocalState = "不存在";
                            diffItem.LocalCode = "";
                            diffItem.LocalName = "";
                            diffItem.LocalValue = 0;
                            diffItem.LocalMode = "";
                        }

                        //
                        // 办学比对
                        // 首先看办学数据是否存在该库存号
                        //
                        _tempState = banxueData.Any(x => x.UsedNum2 == item.UsedNum2);

                        //
                        // 如果存在该库存号，则比对资产名称/价值/规格
                        // 上述三项中有任意一项不匹配则表示存在但是数有差异
                        //
                        if (_tempState)
                        {
                            //
                            // 取出该条数据
                            //
                            AssetsMain _banxueData = new AssetsMain();
                            _banxueData = banxueData.FirstOrDefault(x => x.UsedNum2 == item.UsedNum2);

                            //
                            // 比对资产名称/价值/规格
                            //
                            bool _allSame = true;

                            bool _nameSame = _banxueData.GoodsName == item.GoodsName;
                            bool _moneySame = _banxueData.Money == item.Money;
                            bool _modeSame = _banxueData.Brand == item.Brand;
                            _allSame = _allSame & _nameSame & _moneySame & _modeSame;

                            //
                            // 填充比对结果
                            //
                            diffItem.BxState = _allSame ? "精确匹配" : "存在但有差异";
                            diffItem.BxCode = _banxueData.UsedNum2;
                            diffItem.BxName = _banxueData.GoodsName;
                            diffItem.BxValue = _banxueData.Money;
                            diffItem.BxMode = _banxueData.Brand;
                        }
                        else
                        {
                            //
                            // 如果不存在该库存号，则无需对该资产进行办学比对
                            //
                            diffItem.BxState = "不存在";
                            diffItem.BxCode = "";
                            diffItem.BxName = "";
                            diffItem.BxValue = 0;
                            diffItem.BxMode = "";
                        }

                        //
                        // 总比对结果
                        //
                        if ((diffItem.LocalState == "存在") && (diffItem.BxState == "存在") && (diffItem.DtState == "存在"))
                        {
                            diffItem.DiffState = "匹配";
                        }
                        else
                        {
                            diffItem.DiffState = "不匹配";
                        }

                        //
                        // 添加到比对结果列表
                        //
                        result.Add(diffItem);
                    }
                    break;
                default:
                    break;
            }

            

            //
            // 将比对结果写入比对结果表
            //
            foreach (DiffResult itemResult in result)
            {
                _assetsService.SaveDiffResult(itemResult);
            }
            
            return result;
        }
    }
}