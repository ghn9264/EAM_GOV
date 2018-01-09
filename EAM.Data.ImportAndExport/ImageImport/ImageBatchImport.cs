using System;
using System.IO;
using Eam.Core.Zip;
using EAM.Data.Services;

namespace EAM.Data.ImportAndExport.ImageImport
{
    public class ImageBatchImport
    {
        private readonly IAssetsService _assetsService;

        public ImageBatchImport(IAssetsService assetsService)
        {
            _assetsService = assetsService;
        }

        public ImageImportResult DoImport(string filePath, string targetFolder)
        {
            var result = new ImageImportResult {ZipFilePath = filePath};
            if (!File.Exists(filePath))
            {
                result.ErrorMessage = string.Format("文件不存在:{0}", filePath);
                return result;
            }
            var subFolder = DateTime.Now.ToString("yyMMddHHmmssf");
            targetFolder = Path.Combine(targetFolder, subFolder);
            if (!ZipHelper.UnZip(filePath, targetFolder))
            {
                result.ErrorMessage = "解压文件失败";
                return result;
            }
            var files = Directory.GetFiles(targetFolder);
            foreach (var file in files)
            {
                var assetsNum = Path.GetFileNameWithoutExtension(file);
                var fileNameWithSubFolder = string.Format("{0}/{1}", subFolder, Path.GetFileName(file));
                var count = _assetsService.UpdatePicPath(assetsNum, fileNameWithSubFolder);
                result.Result.Add(new ImportItem(Path.GetFileName(file), count));
            }
            File.Delete(filePath);
            return result;
        }
    }
}