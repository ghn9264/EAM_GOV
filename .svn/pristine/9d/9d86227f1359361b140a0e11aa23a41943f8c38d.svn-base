using EAM.Data.Services;
using NPOI.SS.UserModel;

namespace EAM.Data.ImportAndExport.Export.ExportDoAssets
{
    public abstract class ExportDoAssetsBase : ExportBase
    {
        protected IAssetsOptionService AssetsOptionService;
        protected int DoAssetsId;

        public ExportDoAssetsBase(IAssetsOptionService assetsOptionService, int doAssetsId) : base()
        {
            AssetsOptionService = assetsOptionService;
            DoAssetsId = doAssetsId;
            
            HeaderStyle = Workbook.CreateCellStyle();
            HeaderStyle.Alignment = HorizontalAlignment.Center;
            HeaderStyle.VerticalAlignment = VerticalAlignment.Center;
            IFont font = Workbook.CreateFont();
            font.Boldweight = short.MaxValue;
            HeaderStyle.SetFont(font);


            CommCellStyle = Workbook.CreateCellStyle();
            CommCellStyle.VerticalAlignment = VerticalAlignment.Center;
            CommCellStyle.WrapText = true;

            FooterCellStyle = Workbook.CreateCellStyle();
            FooterCellStyle.Alignment = HorizontalAlignment.Center;
            FooterCellStyle.VerticalAlignment = VerticalAlignment.Center;
            FooterCellStyle.WrapText = true;
        }

        public override string SheetName
        {
            get { return "SHEET1"; }
        }

        public ICellStyle HeaderStyle { get; private set; }
        public ICellStyle CommCellStyle { get; private set; }
        public ICellStyle FooterCellStyle { get; private set; }
    }
}