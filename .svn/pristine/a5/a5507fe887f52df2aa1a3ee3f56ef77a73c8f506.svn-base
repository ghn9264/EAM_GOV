using Eam.Core.Zip;
using EAM.Data.Domain;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EAM.Data.ImportAndExport.Export
{
    public class ExportDiy<T>
    {

        public void MultiExport(ExportSpecial SPECIAL, string filename)
        {
            List<List<AssetsMain>> Lists = SPECIAL.GetPageList<AssetsMain>();
            int count = SPECIAL.GetPagecount();
            ZipParameter zp = new ZipParameter();
            zp.ZIPDirectoryName = filename;
            zp.ZIPName = "special.zip";
            for (int i = 0; i < count; i++)
            {
                SPECIAL.SetCurrentAs(Lists[i]);
                SPECIAL.CreatTemplate();
                SPECIAL.Filldata();
                var tempFile = filename;
                var savePath = Path.Combine(tempFile,
                string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss") + "ZC06_专用设备" + i));
                SPECIAL.Save(savePath);
                SPECIAL.ResetRowIndex();
                zp.ZIPFileList.Add(savePath);
            };
            CompressFile cprFile = new CompressFile();
            cprFile.ZipParameter = zp;
            string strMessage = cprFile.CompressReturnMsg();
        }
        public void MultiExport(ExportGeneral GENERAL, string filename)
        {
            List<List<AssetsMain>> Lists = GENERAL.GetPageList<AssetsMain>();
            int count = GENERAL.GetPagecount();
            ZipParameter zp = new ZipParameter();
            zp.ZIPDirectoryName = filename;
            zp.ZIPName = "GENERAL.zip";
            for (int i = 0; i < count; i++)
            {
                GENERAL.SetCurrentAs(Lists[i]);
                GENERAL.CreatTemplate();
                GENERAL.Filldata();
                var tempFile = filename;
                var savePath = Path.Combine(tempFile,
                string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss") + "ZC05_通用设备" + i));
                GENERAL.Save(savePath);
                GENERAL.ResetRowIndex();
                zp.ZIPFileList.Add(savePath);
            };
            CompressFile cprFile = new CompressFile();
            cprFile.ZipParameter = zp;
            string strMessage = cprFile.CompressReturnMsg();
        }

        public void MultiExport(ExportAnimalandplant ANIMALANDPLANT, string filename)
        {
            List<List<AssetsMain>> Lists = ANIMALANDPLANT.GetPageList<AssetsMain>();
            int count = ANIMALANDPLANT.GetPagecount();
            ZipParameter zp = new ZipParameter();
            zp.ZIPDirectoryName = filename;
            zp.ZIPName = "ANIMALANDPLANT.zip";
            for (int i = 0; i < count; i++)
            {
                ANIMALANDPLANT.SetCurrentAs(Lists[i]);
                ANIMALANDPLANT.CreatTemplate();
                ANIMALANDPLANT.Filldata();
                var tempFile = filename;
                var savePath = Path.Combine(tempFile,
                string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss") + "ZC10_特种动植物" + i));
                ANIMALANDPLANT.Save(savePath);
                ANIMALANDPLANT.ResetRowIndex();
                zp.ZIPFileList.Add(savePath);
            };
            CompressFile cprFile = new CompressFile();
            cprFile.ZipParameter = zp;
            string strMessage = cprFile.CompressReturnMsg();
        }

        public void MultiExport(ExportBook bookExport, string filename)
        {
            List<List<AssetsMain>> Lists = bookExport.GetPageList<AssetsMain>();
            int count = bookExport.GetPagecount();
            ZipParameter zp = new ZipParameter();
            zp.ZIPDirectoryName = filename;
            zp.ZIPName = "bookExport.zip";
            for (int i = 0; i < count; i++)
            {
                bookExport.SetCurrentAs(Lists[i]);
                bookExport.CreatTemplate();
                bookExport.Filldata();
                var tempFile = filename;
                var savePath = Path.Combine(tempFile,
                string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss") + "ZC09_图书档案" + i));
                bookExport.Save(savePath);
                bookExport.ResetRowIndex();
                zp.ZIPFileList.Add(savePath);
            };
            CompressFile cprFile = new CompressFile();
            cprFile.ZipParameter = zp;
            string strMessage = cprFile.CompressReturnMsg();
        }

        public void MultiExport(ExportFurniture FURNITURE, string filename)
        {
            List<List<AssetsMain>> Lists = FURNITURE.GetPageList<AssetsMain>();
            int count = FURNITURE.GetPagecount();
            ZipParameter zp = new ZipParameter();
            zp.ZIPDirectoryName = filename;
            zp.ZIPName = "FURNITURE.zip";
            for (int i = 0; i < count; i++)
            {
                FURNITURE.SetCurrentAs(Lists[i]);
                FURNITURE.CreatTemplate();
                FURNITURE.Filldata();
                var tempFile = filename;
                var savePath = Path.Combine(tempFile,
                string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss") + "ZC08_家具用具及装具" + i));
                FURNITURE.Save(savePath);
                FURNITURE.ResetRowIndex();
                zp.ZIPFileList.Add(savePath);
            };
            CompressFile cprFile = new CompressFile();
            cprFile.ZipParameter = zp;
            string strMessage = cprFile.CompressReturnMsg();
        }

        public void MultiExport(ExportCulturalrelic CULTURALRELIC, string filename)
        {
            List<List<AssetsMain>> Lists = CULTURALRELIC.GetPageList<AssetsMain>();
            int count = CULTURALRELIC.GetPagecount();
            ZipParameter zp = new ZipParameter();
            zp.ZIPDirectoryName = filename;
            zp.ZIPName = "CULTURALRELIC.zip";
            for (int i = 0; i < count; i++)
            {
                CULTURALRELIC.SetCurrentAs(Lists[i]);
                CULTURALRELIC.CreatTemplate();
                CULTURALRELIC.Filldata();
                var tempFile = filename;
                var savePath = Path.Combine(tempFile,
                string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss") + "ZC07_文物与陈列品" + i));
                CULTURALRELIC.Save(savePath);
                CULTURALRELIC.ResetRowIndex();
                zp.ZIPFileList.Add(savePath);
            };
            CompressFile cprFile = new CompressFile();
            cprFile.ZipParameter = zp;
            string strMessage = cprFile.CompressReturnMsg();
        }

        public void MultiExport(ExportCar CAR, string filename)
        {
            List<List<AssetsMain>> Lists = CAR.GetPageList<AssetsMain>();
            int count = CAR.GetPagecount();
            ZipParameter zp = new ZipParameter();
            zp.ZIPDirectoryName = filename;
            zp.ZIPName = "CAR.zip";
            for (int i = 0; i < count; i++)
            {
                CAR.SetCurrentAs(Lists[i]);
                CAR.CreatTemplate();
                CAR.Filldata();
                var tempFile = filename;
                var savePath = Path.Combine(tempFile,
                string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss") + "ZC04_车辆" + i));
                CAR.Save(savePath);
                CAR.ResetRowIndex();
                zp.ZIPFileList.Add(savePath);
            };
            CompressFile cprFile = new CompressFile();
            cprFile.ZipParameter = zp;
            string strMessage = cprFile.CompressReturnMsg();
        }

        public void MultiExport(ExportBuild BUILDING, string filename)
        {
            List<List<AssetsMain>> Lists = BUILDING.GetPageList<AssetsMain>();
            int count = BUILDING.GetPagecount();
            ZipParameter zp = new ZipParameter();
            zp.ZIPDirectoryName = filename;
            zp.ZIPName = "BUILDING.zip";
            for (int i = 0; i < count; i++)
            {
                BUILDING.SetCurrentAs(Lists[i]);
                BUILDING.CreatTemplate();
                BUILDING.Filldata();
                var tempFile = filename;
                var savePath = Path.Combine(tempFile,
                string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss") + "ZC03_构筑物" + i));
                BUILDING.Save(savePath);
                BUILDING.ResetRowIndex();
                zp.ZIPFileList.Add(savePath);
            };
            CompressFile cprFile = new CompressFile();
            cprFile.ZipParameter = zp;
            string strMessage = cprFile.CompressReturnMsg();
        }

        public void MultiExport(ExportHouse houseExport, string filename)
        {
            List<List<AssetsMain>> Lists = houseExport.GetPageList<AssetsMain>();
            int count = houseExport.GetPagecount();
            ZipParameter zp = new ZipParameter();
            zp.ZIPDirectoryName = filename;
            zp.ZIPName = "houseExport.zip";
            for (int i = 0; i < count; i++)
            {
                houseExport.SetCurrentAs(Lists[i]);
                houseExport.CreatTemplate();
                houseExport.Filldata();
                var tempFile = filename;
                var savePath = Path.Combine(tempFile,
                string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss") + "ZC02_房屋" + i));
                houseExport.Save(savePath);
                houseExport.ResetRowIndex();
                zp.ZIPFileList.Add(savePath);
            };
            CompressFile cprFile = new CompressFile();
            cprFile.ZipParameter = zp;
            string strMessage = cprFile.CompressReturnMsg();
        }

        public void MultiExport(ExportLand landExport, string filename)
        {
            List<List<AssetsMain>> Lists = landExport.GetPageList<AssetsMain>();
            int count = landExport.GetPagecount();
            ZipParameter zp = new ZipParameter();
            zp.ZIPDirectoryName = filename;
            zp.ZIPName = "landExport.zip";
            for (int i = 0; i < count; i++)
            {
                landExport.SetCurrentAs(Lists[i]);
                landExport.CreatTemplate();
                landExport.Filldata();
                var tempFile = filename;
                var savePath = Path.Combine(tempFile,
                string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss") + "ZC01_土地" + i));
                landExport.Save(savePath);
                landExport.ResetRowIndex();
                zp.ZIPFileList.Add(savePath);
            };
            CompressFile cprFile = new CompressFile();
            cprFile.ZipParameter = zp;
            string strMessage = cprFile.CompressReturnMsg();
        }
    }
}
