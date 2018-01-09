using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EAM.Data.Comm
{
  public class ErrorLog
    {
      public static void Write(ExceptionContext filterContext)
      {
          var dt = DateTime.Now;
          var logPath = HttpContext.Current.Server.MapPath("/log/" + dt.ToString("yyyy-MM"));
          if (!Directory.Exists(logPath))
          {
              Directory.CreateDirectory(logPath);
          }
          var logFilePath = string.Format("{0}/{1}.txt", logPath, dt.ToString("yyyy-MM-dd"));
          StreamWriter writer = null;
          try
          {
              writer = new StreamWriter(logFilePath, true, Encoding.UTF8);
              writer.WriteLine("------------------------------------------------------------------------------");
              writer.WriteLine("出错时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
              writer.WriteLine("错误信息：" + filterContext.Exception.Message);
              writer.WriteLine("Controller：" + filterContext.Controller);
              writer.WriteLine("错误源：" + filterContext.Exception.Source);
              writer.WriteLine("堆栈信息：" + filterContext.Exception.StackTrace);
              writer.WriteLine("------------------------------------------------------------------------------");
          }
          catch
          {
          }
          finally
          {
              if (writer != null)
              {
                  writer.Close();
              }
          }
      }
    }
}
