using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

namespace PrintTest
{
    public static class PrintHelper
    {
        private static string _printerName;
        private static bool _deleteAfterPrint = false;
        private static string _imgPath;

        #region 打印

        public static void PrintImage(string imgPath)
        {
            if (!File.Exists(imgPath))
            {
                Logger.Log(string.Format("文件不存在 {0}", imgPath));
                return;
            }
            _imgPath = imgPath;
            PrintDocument pd = new PrintDocument();
            pd.PrintController = new StandardPrintController();
            Margins margin = new Margins(0, 0, 0, 0);
            pd.DefaultPageSettings.Margins = margin;
            PaperSize ps = new PaperSize("CustomPaperSize", 275, 150);
            pd.DefaultPageSettings.PaperSize = ps;
            try
            {
                /*if (string.IsNullOrEmpty(_printerName))
                {
                    _printerName = ConfigurationManager.AppSettings["PrinterName"].Trim();
                    _deleteAfterPrint = Convert.ToBoolean(ConfigurationManager.AppSettings["DeleteAfterPrint"].Trim());
                }
                pd.PrinterSettings.PrinterName = _printerName;*/
                pd.DocumentName = imgPath;
                pd.PrintPage += PrintImage;
                pd.EndPrint += PdEndPrint;
                pd.Print();
            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("打印异常 {0}", ex));
                pd.PrintController.OnEndPrint(pd, new PrintEventArgs());
            }
        }

        private static void PdEndPrint(object sender, PrintEventArgs e)
        {
            if (_deleteAfterPrint) File.Delete(_imgPath);
        }

        private static void PrintImage(object sender, PrintPageEventArgs e)
        {
            using (Image temp = Image.FromFile(_imgPath))
            {
                e.Graphics.DrawImage(temp, new Rectangle(0, 0, 320, 180), new Rectangle(0, 0, temp.Width, temp.Height),
                    GraphicsUnit.Pixel);
            }
        }

        #endregion

    }
}