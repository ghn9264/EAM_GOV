using EAM.Data.Domain;
using EAM.Data.Services;
using System.Collections.Generic;
using System.Linq;
using EAM.Data.Services.Impl;

namespace EAM.Data.ImportAndExport.Export
{
    public abstract class ExportScrapMergeBase : ExportScrapBase
    {
        protected IScrapService _ScrapService;
        protected ScrapMerge _ScrapMerge;

        public abstract AssetsTypes AssetsType { get; }

        public override string SheetName
        {
            get { return "SHEET1"; }
        }

        public ExportScrapMergeBase(IScrapService ScrapService, ScrapMerge scrapMerges)
            : base(scrapMerges.type)
        {
            _ScrapService = ScrapService;
            _ScrapMerge = scrapMerges;
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
        }

        protected override void BuildFooter()
        {

        }
    }
}