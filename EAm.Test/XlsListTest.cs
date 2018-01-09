using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;

namespace EAm.Test
{
    [TestClass]
    public class XlsListTest
    {

        [TestMethod]
        public void ListTest()
        {
            var path=@"C:\1.xls";
            var Workbook = new HSSFWorkbook();
            var Sheet = Workbook.CreateSheet("Sheet1");

            CellRangeAddressList regions = new CellRangeAddressList(0,65535,0,0);
            DVConstraint constraint = DVConstraint.CreateExplicitListConstraint(new string[] { "itemA", "itemB", "itemC" });
            HSSFDataValidation dataValidate = new HSSFDataValidation(regions, constraint);
            Sheet.AddValidationData(dataValidate);

            Sheet.GetRow(0).GetCell(0).SetCellValue("itemB");

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                Workbook.Write(fs);
            }

        }
    }

}
