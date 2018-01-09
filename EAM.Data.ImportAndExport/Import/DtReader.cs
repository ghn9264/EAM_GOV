using System.Collections.Generic;
using System.Linq;
using EAM.Data.Domain;
using EAM.Data.Services;
using NPOI.HSSF.UserModel;
using System;
using System.Data;
using System.IO;

namespace EAM.Data.ImportAndExport.Import
{
    public static class DtReader
    {
        private static DataTable ReadFromFile(string filePath)
        {
            HSSFWorkbook hssfworkbook;

            #region 初始化信息

            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }

            #endregion

            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            int rowNum = sheet.LastRowNum;
            Console.WriteLine(rowNum);
            if (rowNum < 1)
                return null;

            DataTable dt = new DataTable();
            rows.MoveNext();
            HSSFRow row = (HSSFRow)rows.Current;
            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                DataColumn dtc = new DataColumn(row.GetCell(j).ToString(), typeof(string));
                dt.Columns.Add(dtc);
            }
            for (int rowIndex = 1; rowIndex <= rowNum; rowIndex++)
            {
                try
                {

                    Console.WriteLine("rowIndex:{0}", rowIndex);
                    var row2 = sheet.GetRow(rowIndex);
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        NPOI.SS.UserModel.ICell cell = row2.GetCell(i);
                        Console.WriteLine(cell == null ? "" : cell + "\t\t");
                        Console.WriteLine("   celIndex:{0}", i);

                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                }
                catch (Exception ex)
                {
                    continue;
                    //  throw;
                }
            }

            /*Console.WriteLine();
            while (rows.MoveNext())
            {
                row = (HSSFRow) rows.Current;
                DataRow dr = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    NPOI.SS.UserModel.ICell cell = row.GetCell(i);
                    Console.WriteLine(cell == null ? "" : cell + "\t\t");
                    Console.WriteLine();

                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                dt.Rows.Add(dr);
            }*/
            return dt;
        }

        public static List<AssetsMainExt> ReadData(string filePath, ref ProgressInfo importProgress, IImportHistoryService importHistoryService, IUnImportAssetsService unImportAssetsService)
        {
            var list = new List<AssetsMainExt>();
            DataTable data = ReadFromFile(filePath);
            //importProgress.TotalAssetsNum = data.Rows.Count;
            try
            {
                importProgress.TotalAssetsNum = data.Rows.Count;
            }
            catch (Exception)
            {

            }
            double total = data.Rows.Count;
            double percent = 0.5;
            int idx = 0;
            for (int rowIndex = 0; rowIndex < data.Rows.Count; rowIndex++)
            {
                idx++;
                DataRow row = data.Rows[rowIndex];
                var assetsMain = new AssetsMainExt();
                var unInportAssets = new UnImportAssets();

                try
                {

                    #region 填充默认值

                    assetsMain.UsingAttribute = "01|办公设备";
                    assetsMain.UsingState = "01|在用";
                    assetsMain.UsingStyle = "01|自用";
                    assetsMain.GetWay = "01|购置";
                    assetsMain.ValueType = "01|原值";
                    assetsMain.NoneGovMoney = 0;

                    #endregion

                    #region 数据抽取
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["AssetsNum"]))
                    {
                        assetsMain.UsedNum1 = Convert.ToString(row[Mappings.DtAssetsMapping["AssetsNum"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["GoodsName"]))
                    {
                        assetsMain.GoodsName = Convert.ToString(row[Mappings.DtAssetsMapping["GoodsName"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["CatCode"]))
                    {
                        assetsMain.CatCode = Convert.ToString(row[Mappings.DtAssetsMapping["CatCode"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["GetWay"]))
                    {
                        assetsMain.GetWay = SplitString(Convert.ToString(row[Mappings.DtAssetsMapping["GetWay"]]));
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["GetDate"]))
                    {
                        if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["GetDate"]].ToString()))
                        {
                            assetsMain.GetDate = Convert.ToDateTime(row[Mappings.DtAssetsMapping["GetDate"]]);//原有
                        }
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["MeasurementUnits"]))
                    {
                        assetsMain.MeasurementUnits = Convert.ToString(row[Mappings.DtAssetsMapping["MeasurementUnits"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["UseDepartment"]))
                    {
                        assetsMain.UseDepartment = Convert.ToString(row[Mappings.DtAssetsMapping["UseDepartment"]]);
                    }

                    //
                    // 数量可能出现空格
                    //
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["Counts"]))
                    {
                        if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["Counts"]].ToString()))
                        {
                            string _count = Convert.ToString(row[Mappings.DtAssetsMapping["Counts"]]);
                            _count.Replace(" ", "");

                            if (_count != "")
                            {
                                try
                                {
                                    assetsMain.Counts = (int)Convert.ToDouble(_count);
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        }
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["ModelSpecification"]))
                    {
                        assetsMain.ModelSpecification = Convert.ToString(row[Mappings.DtAssetsMapping["ModelSpecification"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["UsingState"]))
                    {
                        assetsMain.UsingState = SplitString(Convert.ToString(row[Mappings.DtAssetsMapping["UsingState"]]));
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["UsingStyle"]))
                    {
                        assetsMain.UsingStyle = SplitString(Convert.ToString(row[Mappings.DtAssetsMapping["UsingStyle"]]));
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["ValueType"]))
                    {
                        assetsMain.ValueType = SplitString(Convert.ToString(row[Mappings.DtAssetsMapping["ValueType"]]));
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["Money"]))
                    {
                        if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["Money"]].ToString()))
                            assetsMain.Money = Convert.ToDecimal(row[Mappings.DtAssetsMapping["Money"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["GovMoney"]))
                    {
                        if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["GovMoney"]].ToString()))
                            assetsMain.GovMoney = Convert.ToDecimal(row[Mappings.DtAssetsMapping["GovMoney"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["NoneGovMoney"]))
                    {
                        if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["NoneGovMoney"]].ToString()))
                            assetsMain.NoneGovMoney = Convert.ToDecimal(row[Mappings.DtAssetsMapping["NoneGovMoney"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["NetWorth"]))
                    {
                        if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["NetWorth"]].ToString()))
                            assetsMain.NetWorth = Convert.ToDecimal(row[Mappings.DtAssetsMapping["NetWorth"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["PostingDate"]))
                    {
                        if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["PostingDate"]].ToString()))
                            assetsMain.PostingDate = Convert.ToDateTime(row[Mappings.DtAssetsMapping["PostingDate"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["AcountDocNum"]))
                    {
                        assetsMain.AcountDocNum = Convert.ToString(row[Mappings.DtAssetsMapping["AcountDocNum"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["StorePlace"]))
                    {
                        assetsMain.StorePlace = Convert.ToString(row[Mappings.DtAssetsMapping["StorePlace"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["UsePeople"]))
                    {
                        assetsMain.UsePeople = Convert.ToString(row[Mappings.DtAssetsMapping["UsePeople"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["Memo"]))
                    {
                        assetsMain.Memo = Convert.ToString(row[Mappings.DtAssetsMapping["Memo"]]);
                    }
                    /*assetsMain.Brand=*/

                    #endregion

                    #region 附加属性

                    if (assetsMain.CatCode.Length < 3)
                    {
                        throw new Exception(string.Format("{0}行 资产分类代码为空：{1},{2}",
                            rowIndex + 2, assetsMain.UsedNum1, assetsMain.GoodsName));
                    }
                    var catCodePrix = assetsMain.CatCode.Substring(0, 3);
                    var assetsType = AssetsTypesExt.AssetsTypeMapping.FirstOrDefault(t => t.CodePrix == catCodePrix);
                    if (null == assetsType)
                    {
                        throw new Exception(string.Format("{0}行 不能识别的资产分类代码：{1},{2},{3}",
                            rowIndex + 2, assetsMain.UsedNum1, assetsMain.GoodsName, assetsMain.CatCode));
                    }
                    switch (assetsType.AssetsType)
                    {
                        case AssetsTypes.Car:
                            var carAttr = new CarAttribute();
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["CarCertificateDate"]))
                            {
                                if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["CarCertificateDate"]].ToString()))
                                    carAttr.CarCertificateDate = Convert.ToDateTime(row[Mappings.DtAssetsMapping["CarCertificateDate"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["CarNum"]))
                            {
                                carAttr.CarNum = Convert.ToString(row[Mappings.DtAssetsMapping["CarNum"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["CarEngineNum"]))
                            {
                                carAttr.CarEngineNum = Convert.ToString(row[Mappings.DtAssetsMapping["CarEngineNum"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["CarRecCode"]))
                            {
                                carAttr.CarRecCode = Convert.ToString(row[Mappings.DtAssetsMapping["CarRecCode"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["CarExhaust"]))
                            {
                                carAttr.CarExhaust = Convert.ToString(row[Mappings.DtAssetsMapping["CarExhaust"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["CarOwner"]))
                            {
                                carAttr.CarOwner = Convert.ToString(row[Mappings.DtAssetsMapping["CarOwner"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["UsingType"]))
                            {
                                carAttr.UsingType = Convert.ToString(row[Mappings.DtAssetsMapping["UsingType"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["CarUser"]))
                            {
                                carAttr.CarUser = Convert.ToString(row[Mappings.DtAssetsMapping["CarUser"]]);
                            }
                            assetsMain.ExtAttribute = carAttr;
                            assetsMain.AssetsType = AssetsTypes.Car;
                            break;
                        case AssetsTypes.Book:
                            var bookAttr = new BooksAttribute();
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["Press"]))
                            {
                                bookAttr.Press = Convert.ToString(row[Mappings.DtAssetsMapping["Press"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["UseDate"]))
                            {
                                if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["UseDate"]].ToString()))
                                    bookAttr.UseDate = Convert.ToDateTime(row[Mappings.DtAssetsMapping["UseDate"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["PressDate"]))
                            {
                                if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["PressDate"]].ToString()))
                                    bookAttr.PressDate = Convert.ToDateTime(row[Mappings.DtAssetsMapping["PressDate"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["FileNum"]))
                            {
                                bookAttr.FileNum = Convert.ToString(row[Mappings.DtAssetsMapping["FileNum"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["StoreYears"]))
                            {
                                bookAttr.StoreYears = Convert.ToString(row[Mappings.DtAssetsMapping["StoreYears"]]);
                            }
                            assetsMain.ExtAttribute = bookAttr;
                            assetsMain.AssetsType = AssetsTypes.Book;
                            break;
                        case AssetsTypes.GeneralEquipment:
                            var gEqAttr = new GeneralAttribute();
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["DevUse"]))
                            {
                                gEqAttr.DevUse = Convert.ToString(row[Mappings.DtAssetsMapping["DevUse"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["DevFactory"]))
                            {
                                gEqAttr.DevFactory = Convert.ToString(row[Mappings.DtAssetsMapping["DevFactory"]]);
                            }
                            assetsMain.ExtAttribute = gEqAttr;
                            assetsMain.AssetsType = AssetsTypes.GeneralEquipment;
                            break;
                        case AssetsTypes.Culturalrelic:
                            var cultAttr = new CulturalrelicAttribute();
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["GoodsLevel"]))
                            {
                                cultAttr.GoodsLevel = Convert.ToString(row[Mappings.DtAssetsMapping["GoodsLevel"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["Years"]))
                            {
                                cultAttr.Years = Convert.ToString(row[Mappings.DtAssetsMapping["Years"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["SourcePlace"]))
                            {
                                cultAttr.SourcePlace = Convert.ToString(row[Mappings.DtAssetsMapping["SourcePlace"]]);
                            }
                            assetsMain.ExtAttribute = cultAttr;
                            assetsMain.AssetsType = AssetsTypes.Culturalrelic;
                            break;
                        case AssetsTypes.House:
                            var houseAttr = new HouseAttribute();
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["IssuingDate"]))
                            {
                                if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["IssuingDate"]].ToString()))
                                    houseAttr.IssuingDate = Convert.ToDateTime(row[Mappings.DtAssetsMapping["IssuingDate"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["DesignUse"]))
                            {
                                houseAttr.DesignUse = Convert.ToString(row[Mappings.DtAssetsMapping["DesignUse"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["BuildingStruccture"]))
                            {
                                houseAttr.BuildingStruccture = Convert.ToString(row[Mappings.DtAssetsMapping["BuildingStruccture"]]);
                            }

                            if (data.Columns.Contains(Mappings.DtAssetsMapping["Area"]))
                            {
                                if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["Area"]].ToString()))
                                    houseAttr.Area = Convert.ToDecimal(row[Mappings.DtAssetsMapping["Area"]]);
                            }

                            if (data.Columns.Contains(Mappings.DtAssetsMapping["HouseOwner"]))
                            {
                                houseAttr.HouseOwner = Convert.ToString(row[Mappings.DtAssetsMapping["HouseOwner"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["OwnershipTime"]))
                            {
                                houseAttr.OwnershipTime = Convert.ToString(row[Mappings.DtAssetsMapping["OwnershipTime"]]);
                            }
                            assetsMain.ExtAttribute = houseAttr;
                            assetsMain.AssetsType = AssetsTypes.House;
                            break;
                        case AssetsTypes.Land:
                            var landAttr = new LandAttribute();

                            if (data.Columns.Contains(Mappings.DtAssetsMapping["SingleArea"]))
                            {
                                if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["SingleArea"]].ToString()))
                                    landAttr.SingleArea = Convert.ToDecimal(row[Mappings.DtAssetsMapping["SingleArea"]]);
                            }

                            if (data.Columns.Contains(Mappings.DtAssetsMapping["SharingArea"]))
                            {
                                if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["SharingArea"]].ToString()))
                                    landAttr.SharingArea = Convert.ToDecimal(row[Mappings.DtAssetsMapping["SharingArea"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["LandOwner"]))
                            {
                                landAttr.LandOwner = Convert.ToString(row[Mappings.DtAssetsMapping["LandOwner"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["LandUserStyle"]))
                            {
                                landAttr.LandUserStyle = Convert.ToString(row[Mappings.DtAssetsMapping["LandUserStyle"]]);
                            }

                            if (data.Columns.Contains(Mappings.DtAssetsMapping["AllArea"]))
                            {
                                if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["AllArea"]].ToString()))
                                    landAttr.AllArea = Convert.ToDecimal(row[Mappings.DtAssetsMapping["AllArea"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["OwnershipCertificate"]))
                            {
                                landAttr.OwnershipCertificate = Convert.ToString(row[Mappings.DtAssetsMapping["OwnershipCertificate"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["OwnershipCertifiateNum"]))
                            {
                                landAttr.OwnershipCertifiateNum = Convert.ToString(row[Mappings.DtAssetsMapping["OwnershipCertifiateNum"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["OwnershipStyle"]))
                            {
                                landAttr.OwnershipStyle = Convert.ToString(row[Mappings.DtAssetsMapping["OwnershipStyle"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["Location"]))
                            {
                                landAttr.Location = Convert.ToString(row[Mappings.DtAssetsMapping["Location"]]);
                            }
                            if (data.Columns.Contains(Mappings.DtAssetsMapping["PropertyForm"]))
                            {
                                landAttr.PropertyForm = Convert.ToString(row[Mappings.DtAssetsMapping["PropertyForm"]]);
                            }
                            assetsMain.ExtAttribute = landAttr;
                            assetsMain.AssetsType = AssetsTypes.Land;
                            break;
                        case AssetsTypes.Furniture:
                            assetsMain.AssetsType = AssetsTypes.Furniture;
                            break;
                        case AssetsTypes.SpecialEquipment:
                            var specAttr = new SpecialAttribute();
                            specAttr.DevFactory = Convert.ToString(row[Mappings.DtAssetsMapping["DevFactory"]]);
                            /*if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["WarrantyDate"]].ToString()))
                                specAttr.WarrantyDate = Convert.ToDateTime(row[Mappings.DtAssetsMapping["WarrantyDate"]]);
                            specAttr.WarrantyPhone = Convert.ToString(row[Mappings.DtAssetsMapping["WarrantyPhone"]]);*/
                            assetsMain.ExtAttribute = specAttr;
                            assetsMain.AssetsType = AssetsTypes.SpecialEquipment;
                            break;
                    }

                    #endregion

                    list.Add(assetsMain);
                    percent = percent + 40.0 / total;
                    importProgress.ImportedPercentVal = (int)percent;
                }
                catch (Exception ex)
                {


                    unInportAssets.Exception = ex.ToString();
                    unInportAssets.GoodsName = assetsMain.GoodsName;
                    unInportAssets.DynamicNum = assetsMain.UsedNum1;
                    unInportAssets.TableName = data.TableName;
                    unInportAssets.TableRow = idx + 1;
                    unInportAssets.ImportType = 1;//(int) assetsMain.ImportType;
                    unInportAssets.ImportPerson = importHistoryService.LastHistory().UserId;
                    unInportAssets.ImportId = importHistoryService.LastHistory().EntityId;  //Id
                    unInportAssets.ImportType = importHistoryService.LastHistory().ImportType; //type
                    unInportAssets.ImportTime = importHistoryService.LastHistory().ImportTime;
                    unInportAssets.Exception = unInportAssets.Exception.Substring(0, 255);// += ex.ToString();
                    unImportAssetsService.Add(unInportAssets); //增加为导入资产 
                    //unImportAssetsService.Update(unInportAssets);
                }

            }
            return list;
        }

        private static string SplitString(string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;
            var index = source.IndexOf("|", StringComparison.Ordinal);
            if (index > -1)
                source = source.Substring(index + 1);
            return source;
        }

        /// <summary>
        /// 人员导入 2017-05-26 wnn
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="importProgress"></param>
        /// <param name="importHistoryService"></param>
        /// <param name="unImportAssetsService"></param>
        /// <returns></returns>
        public static List<UserInfo> ReadUData(string filePath, ref ProgressInfo importProgress, IImportHistoryService importHistoryService, IUnImportAssetsService unImportAssetsService)
        {
            var list = new List<UserInfo>();
            DataTable data = ReadFromFile(filePath);
            //importProgress.TotalAssetsNum = data.Rows.Count;
            try
            {
                importProgress.TotalAssetsNum = data.Rows.Count;
            }
            catch (Exception)
            {

            }
            double total = data.Rows.Count;
            double percent = 0.5;
            int idx = 0;
            for (int rowIndex = 0; rowIndex < data.Rows.Count; rowIndex++)
            {
                idx++;
                DataRow row = data.Rows[rowIndex];
                var user = new UserInfo();
                var unInportAssets = new UnImportAssets();

                try
                {

                    #region 填充默认值

                    user.DeptId = 0;
                    user.Password = "123456";
                    user.Permissions = "";
                    //user.DeptName = "人事部";
                    user.Role = "普通用户";
                    user.DepartMent = "人事部";


                    #endregion

                    if (data.Columns.Contains(Mappings.UserMapping["UserId"]))
                    {
                        user.UserId = Convert.ToString(row[Mappings.UserMapping["UserId"]]);
                    }
                    if (data.Columns.Contains(Mappings.UserMapping["UserName"]))
                    {
                        user.UserName = Convert.ToString(row[Mappings.UserMapping["UserName"]]);
                    }
                    if (data.Columns.Contains(Mappings.UserMapping["Tel"]))
                    {
                        user.Tel = Convert.ToString(row[Mappings.UserMapping["Tel"]]);
                    }
                    if (data.Columns.Contains(Mappings.UserMapping["Role"]))
                    {
                        if (!string.IsNullOrWhiteSpace(row[Mappings.UserMapping["Role"]].ToString()))
                        {
                            user.Role = Convert.ToString(row[Mappings.UserMapping["Role"]]);
                        }
                    }
                    if (data.Columns.Contains(Mappings.UserMapping["Permissions"]))
                    {
                        user.Permissions = Convert.ToString(row[Mappings.UserMapping["Permissions"]]);
                    }
                    if (data.Columns.Contains(Mappings.UserMapping["Password"]))
                    {
                        user.Password = Convert.ToString(row[Mappings.UserMapping["Password"]]);
                    }
                    if (data.Columns.Contains(Mappings.UserMapping["DeptId"]))
                    {
                        user.DeptId = Convert.ToInt32(Convert.ToString(row[Mappings.UserMapping["DeptId"]]));
                    }
                    if (data.Columns.Contains(Mappings.UserMapping["DepartMent"]))
                    {
                        user.DepartMent = Convert.ToString(row[Mappings.UserMapping["DepartMent"]]);
                    }

                    list.Add(user);
                    percent = percent + 40.0 / total;
                    importProgress.ImportedPercentVal = (int)percent;
                }
                catch (Exception ex)
                {


                    unInportAssets.Exception = ex.ToString();
                    //unInportAssets.GoodsName = assetsMain.GoodsName;
                    //unInportAssets.DynamicNum = assetsMain.UsedNum1;
                    unInportAssets.TableName = data.TableName;
                    unInportAssets.TableRow = idx + 1;
                    unInportAssets.ImportType = 1;//(int) assetsMain.ImportType;
                    unInportAssets.ImportPerson = importHistoryService.LastHistory().UserId;
                    unInportAssets.ImportId = importHistoryService.LastHistory().EntityId;  //Id
                    unInportAssets.ImportType = importHistoryService.LastHistory().ImportType; //type
                    unInportAssets.ImportTime = importHistoryService.LastHistory().ImportTime;
                    unInportAssets.Exception = unInportAssets.Exception.Substring(0, 255);// += ex.ToString();
                    //unImportAssetsService.Add(unInportAssets); //增加为导入资产 
                    //unImportAssetsService.Update(unInportAssets);
                }

            }
            return list;
        }


        /// <summary>
        /// 盘盈导入 2017-05-27 wnn
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="importProgress"></param>
        /// <param name="importHistoryService"></param>
        /// <param name="unImportAssetsService"></param>
        /// <returns></returns>
        public static List<AssetsMainExt> ReadPData(string filePath, ref ProgressInfo importProgress, IImportHistoryService importHistoryService, IUnImportAssetsService unImportAssetsService)
        {
            var list = new List<AssetsMainExt>();
            DataTable data = ReadFromFile(filePath);
            //importProgress.TotalAssetsNum = data.Rows.Count;
            try
            {
                importProgress.TotalAssetsNum = data.Rows.Count;
            }
            catch (Exception)
            {

            }
            double total = data.Rows.Count;
            double percent = 0.5;
            int idx = 0;
            for (int rowIndex = 0; rowIndex < data.Rows.Count; rowIndex++)
            {
                idx++;
                DataRow row = data.Rows[rowIndex];
                var assetsMain = new AssetsMainExt();
                var unInportAssets = new UnImportAssets();

                try
                {

                    #region 填充默认值

                    assetsMain.UsingAttribute = "01|办公设备";
                    assetsMain.UsingState = "01|在用";
                    assetsMain.UsingStyle = "01|自用";
                    assetsMain.GetWay = "01|购置";
                    assetsMain.ValueType = "01|原值";
                    assetsMain.NoneGovMoney = 0;
                    assetsMain.GetDate = DateTime.Now;
                    assetsMain.IsIventoryProfit = 1;

                    #endregion

                    #region 数据抽取
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["AssetsNum"]))
                    {
                        assetsMain.UsedNum1 = Convert.ToString(row[Mappings.DtAssetsMapping["AssetsNum"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["GoodsName"]))
                    {
                        assetsMain.GoodsName = Convert.ToString(row[Mappings.DtAssetsMapping["GoodsName"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["CatCode"]))
                    {
                        assetsMain.CatCode = Convert.ToString(row[Mappings.DtAssetsMapping["CatCode"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["GetWay"]))
                    {
                        assetsMain.GetWay = SplitString(Convert.ToString(row[Mappings.DtAssetsMapping["GetWay"]]));
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["GetDate"]))
                    {
                        if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["GetDate"]].ToString()))
                        {
                            assetsMain.GetDate = Convert.ToDateTime(row[Mappings.DtAssetsMapping["GetDate"]]);//原有
                        }
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["MeasurementUnits"]))
                    {
                        assetsMain.MeasurementUnits = Convert.ToString(row[Mappings.DtAssetsMapping["MeasurementUnits"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["UseDepartment"]))
                    {
                        assetsMain.UseDepartment = Convert.ToString(row[Mappings.DtAssetsMapping["UseDepartment"]]);
                    }

                    //
                    // 数量可能出现空格
                    //
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["Counts"]))
                    {
                        if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["Counts"]].ToString()))
                        {
                            string _count = Convert.ToString(row[Mappings.DtAssetsMapping["Counts"]]);
                            _count.Replace(" ", "");

                            if (_count != "")
                            {
                                try
                                {
                                    assetsMain.Counts = (int)Convert.ToDouble(_count);
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        }
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["ModelSpecification"]))
                    {
                        assetsMain.ModelSpecification = Convert.ToString(row[Mappings.DtAssetsMapping["ModelSpecification"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["UsingState"]))
                    {
                        assetsMain.UsingState = SplitString(Convert.ToString(row[Mappings.DtAssetsMapping["UsingState"]]));
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["UsingStyle"]))
                    {
                        assetsMain.UsingStyle = SplitString(Convert.ToString(row[Mappings.DtAssetsMapping["UsingStyle"]]));
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["ValueType"]))
                    {
                        assetsMain.ValueType = SplitString(Convert.ToString(row[Mappings.DtAssetsMapping["ValueType"]]));
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["Money"]))
                    {
                        if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["Money"]].ToString()))
                            assetsMain.Money = Convert.ToDecimal(row[Mappings.DtAssetsMapping["Money"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["GovMoney"]))
                    {
                        if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["GovMoney"]].ToString()))
                            assetsMain.GovMoney = Convert.ToDecimal(row[Mappings.DtAssetsMapping["GovMoney"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["NoneGovMoney"]))
                    {
                        if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["NoneGovMoney"]].ToString()))
                            assetsMain.NoneGovMoney = Convert.ToDecimal(row[Mappings.DtAssetsMapping["NoneGovMoney"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["NetWorth"]))
                    {
                        if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["NetWorth"]].ToString()))
                            assetsMain.NetWorth = Convert.ToDecimal(row[Mappings.DtAssetsMapping["NetWorth"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["PostingDate"]))
                    {
                        if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["PostingDate"]].ToString()))
                            assetsMain.PostingDate = Convert.ToDateTime(row[Mappings.DtAssetsMapping["PostingDate"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["AcountDocNum"]))
                    {
                        assetsMain.AcountDocNum = Convert.ToString(row[Mappings.DtAssetsMapping["AcountDocNum"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["StorePlace"]))
                    {
                        assetsMain.StorePlace = Convert.ToString(row[Mappings.DtAssetsMapping["StorePlace"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["UsePeople"]))
                    {
                        assetsMain.UsePeople = Convert.ToString(row[Mappings.DtAssetsMapping["UsePeople"]]);
                    }
                    if (data.Columns.Contains(Mappings.DtAssetsMapping["Memo"]))
                    {
                        assetsMain.Memo = Convert.ToString(row[Mappings.DtAssetsMapping["Memo"]]);
                    }
                    /*assetsMain.Brand=*/

                    #endregion

                    //#region 附加属性

                    //if (assetsMain.CatCode.Length < 3)
                    //{
                    //    throw new Exception(string.Format("{0}行 资产分类代码为空：{1},{2}",
                    //        rowIndex + 2, assetsMain.UsedNum1, assetsMain.GoodsName));
                    //}
                    //var catCodePrix = assetsMain.CatCode.Substring(0, 3);
                    //var assetsType = AssetsTypesExt.AssetsTypeMapping.FirstOrDefault(t => t.CodePrix == catCodePrix);
                    //if (null == assetsType)
                    //{
                    //    throw new Exception(string.Format("{0}行 不能识别的资产分类代码：{1},{2},{3}",
                    //        rowIndex + 2, assetsMain.UsedNum1, assetsMain.GoodsName, assetsMain.CatCode));
                    //}
                    //switch (assetsType.AssetsType)
                    //{
                    //    case AssetsTypes.Car:
                    //        var carAttr = new CarAttribute();
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["CarCertificateDate"]))
                    //        {
                    //            if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["CarCertificateDate"]].ToString()))
                    //                carAttr.CarCertificateDate = Convert.ToDateTime(row[Mappings.DtAssetsMapping["CarCertificateDate"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["CarNum"]))
                    //        {
                    //            carAttr.CarNum = Convert.ToString(row[Mappings.DtAssetsMapping["CarNum"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["CarEngineNum"]))
                    //        {
                    //            carAttr.CarEngineNum = Convert.ToString(row[Mappings.DtAssetsMapping["CarEngineNum"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["CarRecCode"]))
                    //        {
                    //            carAttr.CarRecCode = Convert.ToString(row[Mappings.DtAssetsMapping["CarRecCode"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["CarExhaust"]))
                    //        {
                    //            carAttr.CarExhaust = Convert.ToString(row[Mappings.DtAssetsMapping["CarExhaust"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["CarOwner"]))
                    //        {
                    //            carAttr.CarOwner = Convert.ToString(row[Mappings.DtAssetsMapping["CarOwner"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["UsingType"]))
                    //        {
                    //            carAttr.UsingType = Convert.ToString(row[Mappings.DtAssetsMapping["UsingType"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["CarUser"]))
                    //        {
                    //            carAttr.CarUser = Convert.ToString(row[Mappings.DtAssetsMapping["CarUser"]]);
                    //        }
                    //        assetsMain.ExtAttribute = carAttr;
                    //        assetsMain.AssetsType = AssetsTypes.Car;
                    //        break;
                    //    case AssetsTypes.Book:
                    //        var bookAttr = new BooksAttribute();
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["Press"]))
                    //        {
                    //            bookAttr.Press = Convert.ToString(row[Mappings.DtAssetsMapping["Press"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["UseDate"]))
                    //        {
                    //            if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["UseDate"]].ToString()))
                    //                bookAttr.UseDate = Convert.ToDateTime(row[Mappings.DtAssetsMapping["UseDate"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["PressDate"]))
                    //        {
                    //            if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["PressDate"]].ToString()))
                    //                bookAttr.PressDate = Convert.ToDateTime(row[Mappings.DtAssetsMapping["PressDate"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["FileNum"]))
                    //        {
                    //            bookAttr.FileNum = Convert.ToString(row[Mappings.DtAssetsMapping["FileNum"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["StoreYears"]))
                    //        {
                    //            bookAttr.StoreYears = Convert.ToString(row[Mappings.DtAssetsMapping["StoreYears"]]);
                    //        }
                    //        assetsMain.ExtAttribute = bookAttr;
                    //        assetsMain.AssetsType = AssetsTypes.Book;
                    //        break;
                    //    case AssetsTypes.GeneralEquipment:
                    //        var gEqAttr = new GeneralAttribute();
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["DevUse"]))
                    //        {
                    //            gEqAttr.DevUse = Convert.ToString(row[Mappings.DtAssetsMapping["DevUse"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["DevFactory"]))
                    //        {
                    //            gEqAttr.DevFactory = Convert.ToString(row[Mappings.DtAssetsMapping["DevFactory"]]);
                    //        }
                    //        assetsMain.ExtAttribute = gEqAttr;
                    //        assetsMain.AssetsType = AssetsTypes.GeneralEquipment;
                    //        break;
                    //    case AssetsTypes.Culturalrelic:
                    //        var cultAttr = new CulturalrelicAttribute();
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["GoodsLevel"]))
                    //        {
                    //            cultAttr.GoodsLevel = Convert.ToString(row[Mappings.DtAssetsMapping["GoodsLevel"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["Years"]))
                    //        {
                    //            cultAttr.Years = Convert.ToString(row[Mappings.DtAssetsMapping["Years"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["SourcePlace"]))
                    //        {
                    //            cultAttr.SourcePlace = Convert.ToString(row[Mappings.DtAssetsMapping["SourcePlace"]]);
                    //        }
                    //        assetsMain.ExtAttribute = cultAttr;
                    //        assetsMain.AssetsType = AssetsTypes.Culturalrelic;
                    //        break;
                    //    case AssetsTypes.House:
                    //        var houseAttr = new HouseAttribute();
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["IssuingDate"]))
                    //        {
                    //            if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["IssuingDate"]].ToString()))
                    //                houseAttr.IssuingDate = Convert.ToDateTime(row[Mappings.DtAssetsMapping["IssuingDate"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["DesignUse"]))
                    //        {
                    //            houseAttr.DesignUse = Convert.ToString(row[Mappings.DtAssetsMapping["DesignUse"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["BuildingStruccture"]))
                    //        {
                    //            houseAttr.BuildingStruccture = Convert.ToString(row[Mappings.DtAssetsMapping["BuildingStruccture"]]);
                    //        }

                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["Area"]))
                    //        {
                    //            if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["Area"]].ToString()))
                    //                houseAttr.Area = Convert.ToDecimal(row[Mappings.DtAssetsMapping["Area"]]);
                    //        }

                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["HouseOwner"]))
                    //        {
                    //            houseAttr.HouseOwner = Convert.ToString(row[Mappings.DtAssetsMapping["HouseOwner"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["OwnershipTime"]))
                    //        {
                    //            houseAttr.OwnershipTime = Convert.ToString(row[Mappings.DtAssetsMapping["OwnershipTime"]]);
                    //        }
                    //        assetsMain.ExtAttribute = houseAttr;
                    //        assetsMain.AssetsType = AssetsTypes.House;
                    //        break;
                    //    case AssetsTypes.Land:
                    //        var landAttr = new LandAttribute();

                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["SingleArea"]))
                    //        {
                    //            if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["SingleArea"]].ToString()))
                    //                landAttr.SingleArea = Convert.ToDecimal(row[Mappings.DtAssetsMapping["SingleArea"]]);
                    //        }

                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["SharingArea"]))
                    //        {
                    //            if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["SharingArea"]].ToString()))
                    //                landAttr.SharingArea = Convert.ToDecimal(row[Mappings.DtAssetsMapping["SharingArea"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["LandOwner"]))
                    //        {
                    //            landAttr.LandOwner = Convert.ToString(row[Mappings.DtAssetsMapping["LandOwner"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["LandUserStyle"]))
                    //        {
                    //            landAttr.LandUserStyle = Convert.ToString(row[Mappings.DtAssetsMapping["LandUserStyle"]]);
                    //        }

                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["AllArea"]))
                    //        {
                    //            if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["AllArea"]].ToString()))
                    //                landAttr.AllArea = Convert.ToDecimal(row[Mappings.DtAssetsMapping["AllArea"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["OwnershipCertificate"]))
                    //        {
                    //            landAttr.OwnershipCertificate = Convert.ToString(row[Mappings.DtAssetsMapping["OwnershipCertificate"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["OwnershipCertifiateNum"]))
                    //        {
                    //            landAttr.OwnershipCertifiateNum = Convert.ToString(row[Mappings.DtAssetsMapping["OwnershipCertifiateNum"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["OwnershipStyle"]))
                    //        {
                    //            landAttr.OwnershipStyle = Convert.ToString(row[Mappings.DtAssetsMapping["OwnershipStyle"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["Location"]))
                    //        {
                    //            landAttr.Location = Convert.ToString(row[Mappings.DtAssetsMapping["Location"]]);
                    //        }
                    //        if (data.Columns.Contains(Mappings.DtAssetsMapping["PropertyForm"]))
                    //        {
                    //            landAttr.PropertyForm = Convert.ToString(row[Mappings.DtAssetsMapping["PropertyForm"]]);
                    //        }
                    //        assetsMain.ExtAttribute = landAttr;
                    //        assetsMain.AssetsType = AssetsTypes.Land;
                    //        break;
                    //    case AssetsTypes.Furniture:
                    //        assetsMain.AssetsType = AssetsTypes.Furniture;
                    //        break;
                    //    case AssetsTypes.SpecialEquipment:
                    //        var specAttr = new SpecialAttribute();
                    //        specAttr.DevFactory = Convert.ToString(row[Mappings.DtAssetsMapping["DevFactory"]]);
                    //        /*if (!string.IsNullOrEmpty(row[Mappings.DtAssetsMapping["WarrantyDate"]].ToString()))
                    //            specAttr.WarrantyDate = Convert.ToDateTime(row[Mappings.DtAssetsMapping["WarrantyDate"]]);
                    //        specAttr.WarrantyPhone = Convert.ToString(row[Mappings.DtAssetsMapping["WarrantyPhone"]]);*/
                    //        assetsMain.ExtAttribute = specAttr;
                    //        assetsMain.AssetsType = AssetsTypes.SpecialEquipment;
                    //        break;
                    //}

                    //#endregion

                    list.Add(assetsMain);
                    percent = percent + 40.0 / total;
                    importProgress.ImportedPercentVal = (int)percent;
                }
                catch (Exception ex)
                {


                    unInportAssets.Exception = ex.ToString();
                    unInportAssets.GoodsName = assetsMain.GoodsName;
                    unInportAssets.DynamicNum = assetsMain.UsedNum1;
                    unInportAssets.TableName = data.TableName;
                    unInportAssets.TableRow = idx + 1;
                    unInportAssets.ImportType = 1;//(int) assetsMain.ImportType;
                    unInportAssets.ImportPerson = importHistoryService.LastHistory().UserId;
                    unInportAssets.ImportId = importHistoryService.LastHistory().EntityId;  //Id
                    unInportAssets.ImportType = importHistoryService.LastHistory().ImportType; //type
                    unInportAssets.ImportTime = importHistoryService.LastHistory().ImportTime;
                    unInportAssets.Exception = unInportAssets.Exception.Substring(0, 255);// += ex.ToString();
                    unImportAssetsService.Add(unInportAssets); //增加为导入资产 
                    //unImportAssetsService.Update(unInportAssets);
                }

            }
            return list;
        }
        /// <summary>
        /// 位置导入 2017-06-05 wnn
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="importProgress"></param>
        /// <param name="importHistoryService"></param>
        /// <param name="unImportAssetsService"></param>
        /// <returns></returns>
        public static List<Place> ReadPlData(string filePath, ref ProgressInfo importProgress, IImportHistoryService importHistoryService, IUnImportAssetsService unImportAssetsService)
        {
            var list = new List<Place>();
            DataTable data = ReadFromFile(filePath);
            //importProgress.TotalAssetsNum = data.Rows.Count;
            try
            {
                importProgress.TotalAssetsNum = data.Rows.Count;
            }
            catch (Exception)
            {

            }
            double total = data.Rows.Count;
            double percent = 0.5;
            int idx = 0;
            for (int rowIndex = 0; rowIndex < data.Rows.Count; rowIndex++)
            {
                idx++;
                DataRow row = data.Rows[rowIndex];
                var place = new Place();
                var unInportAssets = new UnImportAssets();

                try
                {

                    #region 填充默认值

                    place.ParentId = 0;
                    place.ParentName = "无";
                    place.PlaceType = "loc";



                    #endregion

                    //if (data.Columns.Contains(Mappings.UserMapping["ParentName"]))
                    //{
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(row[Mappings.PlaceMapping["ParentName"]])))
                    {
                        place.ParentName = Convert.ToString(row[Mappings.PlaceMapping["ParentName"]]);
                    }
                    else
                    {
                        continue;
                    }
                    //}

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(row[Mappings.PlaceMapping["PlaceName"]])))
                    {
                        place.PlaceName = Convert.ToString(row[Mappings.PlaceMapping["PlaceName"]]);
                    }
                    else
                    {
                        continue;
                    }

                    list.Add(place);
                    percent = percent + 40.0 / total;
                    importProgress.ImportedPercentVal = (int)percent;
                }
                catch (Exception ex)
                {


                    unInportAssets.Exception = ex.ToString();
                    //unInportAssets.GoodsName = assetsMain.GoodsName;
                    //unInportAssets.DynamicNum = assetsMain.UsedNum1;
                    unInportAssets.TableName = data.TableName;
                    unInportAssets.TableRow = idx + 1;
                    unInportAssets.ImportType = 1;//(int) assetsMain.ImportType;
                    unInportAssets.ImportPerson = importHistoryService.LastHistory().UserId;
                    unInportAssets.ImportId = importHistoryService.LastHistory().EntityId;  //Id
                    unInportAssets.ImportType = importHistoryService.LastHistory().ImportType; //type
                    unInportAssets.ImportTime = importHistoryService.LastHistory().ImportTime;
                    unInportAssets.Exception = unInportAssets.Exception.Substring(0, 255);// += ex.ToString();
                    //unImportAssetsService.Add(unInportAssets); //增加为导入资产 
                    //unImportAssetsService.Update(unInportAssets);
                }

            }
            return list;
        }

    }
}