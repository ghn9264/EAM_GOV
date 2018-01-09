using System;
using System.IO;
using System.ServiceModel;

namespace Eam.PrintService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single)]
    public class PrintService : IPrintService
    {
        public ResultBase Test()
        {
            Console.WriteLine("Test");
            return new ResultBase {Message = DateTime.Now.ToString()};
        }

        public ResultBase QrPrint(string base64Path)
        {
            Logger.Log(string.Format("QrPrint:{0}", base64Path));
            try
            {
                byte[] pathBuf = Convert.FromBase64String(base64Path);
                base64Path = System.Text.Encoding.Default.GetString(pathBuf);
                Logger.Log(string.Format("QrPrintEx:{0}", base64Path));
            }
            catch (Exception ex)
            {
                Logger.Log("转换Base64异常："+ ex);
                return new ResultBase { Code = 500, Message = ex.Message };
            }
            try
            {
                var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                var tempFielName = Path.Combine(folder, string.Format("{0}.jpg", Guid.NewGuid()));
                Downloader.DownloadPicture(base64Path, tempFielName, 10000);
                Logger.Log(string.Format("Save:{0}", tempFielName));
                PrintHelper.PrintImage(tempFielName);
                return new ResultBase { Code = 0, Message = "打印完成" };
            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("PrintIamge Exception:{0}", ex));
                return new ResultBase { Code = 500, Message = ex.Message };
            } 
        }
    }
}