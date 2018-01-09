using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAM.Data.ImportAndExport.ImageImport
{
    public class ImageImportResult
    {
        public string ZipFilePath { get; set; }
        public string ErrorMessage { get; set; }
        public List<ImportItem> Result { get;private set; }

        public ImageImportResult()
        {
            Result = new List<ImportItem>();
        }
    }

    public class ImportItem
    {
        public string FileName { get; private set; }
        public int UpdateedCount { get; private set; }

        public ImportItem(string fileName, int updateedCount)
        {
            FileName = fileName;
            UpdateedCount = updateedCount;
        }
    }
}
