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
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine(DateTime.Now.Ticks);

            var basePath = @"G:\办学导出模板.xls";
            DataTable data = BxReader.ReadFromFile(basePath);



            Console.WriteLine(data);

        }
    }
}