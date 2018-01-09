using System;
using System.IO;

namespace Eam.PrintService
{
    public static class Logger
    {
        #region FileLog

        private static StreamWriter _logWriter;

        public static void Log(string content)
        {
            try
            {
                if (null == _logWriter)
                {
                    var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                    _logWriter = new StreamWriter(Path.Combine(folder,string.Format(@"print_{0}.log", DateTime.Now.ToString("yyMMdd"))), true);
                }
                var log = string.Format("{0}  {1}", DateTime.Now.ToString("yy-MM-dd HH:mm:ss"), content);
                _logWriter.WriteLine(log);
                _logWriter.Flush();
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

    }
}