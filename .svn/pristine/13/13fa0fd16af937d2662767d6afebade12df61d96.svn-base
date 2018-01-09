using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EAM.Data.Domain;
using EAM.Data.Services;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace EAM.Data.ImportAndExport.Export
{
    public abstract class ExportBase
    {
        protected IWorkbook Workbook;
        protected ISheet Sheet;
        protected IRow TempRow;
        protected ICell TempCell;
        protected int NextRowIndex = 0;


        public abstract string SaveFileName { get; }
        public abstract string SheetName { get; }
        public ExportBase()
        {
            Workbook = new HSSFWorkbook();
            Sheet = Workbook.CreateSheet(SheetName);
        }

        protected abstract void BuildTitle();

        protected virtual void QueryData()
        {

        }

        protected abstract void BuildHeader();
        protected abstract void FillBody();
        protected abstract void BuildFooter();

        public void DoExport()
        {
            BuildTitle();
            QueryData();
            BuildHeader();
            FillBody();
            BuildFooter();
        }

        public virtual void Save(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                Workbook.Write(fs);
            }
        }
    }
}