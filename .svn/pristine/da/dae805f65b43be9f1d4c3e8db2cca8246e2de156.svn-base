using EAM.Data.Domain;
using EAM.Data.Services;
using System.Collections.Generic;
using System.Linq;

namespace EAM.Data.ImportAndExport.Export
{
    public abstract class ExportQueryAssetsBase : ExportQueryBaseEx
    {
        protected IAssetsService AssetsService;
        protected List<AssetsMain> AssetsData;

        public abstract AssetsTypes AssetsType { get; }

        public override string SheetName
        {
            get { return "SHEET1"; }
        }

        public ExportQueryAssetsBase(IAssetsService assetsService,List<AssetsMain> assetsMains )
            : base()
        {
            AssetsService = assetsService;
            AssetsData = assetsMains;
        }

        protected override void BuildTitle()
        {
           var title = AssetsType.GetCnName();
            Sheet.CreateRow(NextRowIndex).CreateCell(0).SetCellValue(title);
            NextRowIndex++;
            TempRow = Sheet.CreateRow(NextRowIndex);
            TempRow.CreateCell(0).SetCellValue("单位名称：");
            TempRow.CreateCell(1).SetCellValue("测试单位");
            NextRowIndex++;
        }

        protected override void QueryData()
        {
            //var codeStart =
            //    AssetsTypesExt.AssetsTypeMapping.Where(t => t.AssetsType == AssetsType).Select(x => x.CodePrix).ToList();
            //AssetsData = AssetsService.QueryAssets(codeStart);
        }

        protected override void BuildFooter()
        {
            
        }
    }
}