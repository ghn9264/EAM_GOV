using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using EAM.Data.Domain;
using EAM.Data.ImportAndExport;
using EAM.Data.ImportAndExport.Import;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.HSSF.UserModel;

namespace EAm.Test
{
    [TestClass]
    public class ImportUnitTest
    {
        [TestMethod]
        public void BxImportTest()
        {
            Console.WriteLine(DateTime.Now.Ticks);
            const string basePath = @"G:\办学导出模板.xls";
         //   var data = BxReader.ReadData(basePath);
          //  Console.WriteLine(data);
        }

        [TestMethod]
        public void DtImportTest()
        {
            Console.WriteLine(DateTime.Now.Ticks);
            const string basePath = @"D:\北京-刘亦清\Reference\中关村中学分校动态.xls";
           // var data = DtReader.ReadData(basePath);

          //  Console.WriteLine(data);
        }

        [TestMethod]
        public void FilterStringTest()
        {
            string str = "01|办公设备";
            var str2 = FilterString(str);
            Console.WriteLine(str2);
        }

        private string FilterString(string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;
            var index = source.IndexOf("|", StringComparison.Ordinal);
            if (index > -1)
                source = source.Substring(index+1);
            return source;
        }

    }
}