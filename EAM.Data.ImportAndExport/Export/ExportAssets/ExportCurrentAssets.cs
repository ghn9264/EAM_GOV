using System;
using System.Collections.Generic;
using System.Threading;
using EAM.Data.Domain;
using EAM.Data.ImportAndExport.Import;
using EAM.Data.Services;

namespace EAM.Data.ImportAndExport.Export.ExportAssets
{
    public class ExportCurrentAssets : ExportQueryAssetsBase
    {
        public ExportCurrentAssets(IAssetsService assetsService,List<AssetsMain> assetsList )
            : base(assetsService, assetsList)
        {

        }

        public override AssetsTypes AssetsType
        {
            get { return AssetsTypes.Land; }
        }
        protected override void BuildTitle()
        {
        	  NextRowIndex++;
              NextRowIndex++;
           
        }
     
     
        public override string SaveFileName
        {
            get { return @"当前资产.xls"; }
        }

        protected override void BuildHeader()
        {
          /*  TempRow = Sheet.CreateRow(NextRowIndex);
            TempRow.CreateCell(0).SetCellValue("序号");
            TempRow.CreateCell(1).SetCellValue("资产名称");
            TempRow.CreateCell(2).SetCellValue("固定资产分类代码（2010国标码）");
            TempRow.CreateCell(3).SetCellValue("资产分类名称（可以不录）");
            TempRow.CreateCell(4).SetCellValue("计量单位");
            TempRow.CreateCell(5).SetCellValue("使用属性");
            TempRow.CreateCell(6).SetCellValue("使用状况");
            TempRow.CreateCell(7).SetCellValue("取得方式");
            TempRow.CreateCell(8).SetCellValue("取得日期（2013-1-1）");

            TempRow.CreateCell(9).SetCellValue("产权形式");
            TempRow.CreateCell(10).SetCellValue("权属证明（有产权填）");
            TempRow.CreateCell(11).SetCellValue("权属性质（有产权填）");
            TempRow.CreateCell(12).SetCellValue("权属年限（年数，有产权填）");
            TempRow.CreateCell(13).SetCellValue("权属证号（有产权填）");
            TempRow.CreateCell(14).SetCellValue("发证日期（有产权填）（2013-1-1）");
            TempRow.CreateCell(15).SetCellValue("土地使用权人（有产权填）");
            TempRow.CreateCell(16).SetCellValue("土地使用权类型");
            TempRow.CreateCell(17).SetCellValue("坐落位置");
            TempRow.CreateCell(18).SetCellValue("土地使用权面积（平米）");
            TempRow.CreateCell(19).SetCellValue("分摊面积（平米）");
            TempRow.CreateCell(20).SetCellValue("独用面积（平米）");
            
            TempRow.CreateCell(21).SetCellValue("价值类型");
            TempRow.CreateCell(22).SetCellValue("价值");
            TempRow.CreateCell(23).SetCellValue("财政性资金");
            TempRow.CreateCell(24).SetCellValue("非财政性资金");

            TempRow.CreateCell(25).SetCellValue("摊销");
            TempRow.CreateCell(26).SetCellValue("累计摊销");
            TempRow.CreateCell(27).SetCellValue("净值");
            TempRow.CreateCell(28).SetCellValue("入账日期（2013-1-1）");
            TempRow.CreateCell(29).SetCellValue("入账科目");
            TempRow.CreateCell(30).SetCellValue("会计凭证号");
            TempRow.CreateCell(31).SetCellValue("备注");*/

            NextRowIndex++;
            NextRowIndex++;
        }

        protected override void FillBody()
        {
            if (null == AssetsData)
                return;
            for(int index=0;index<AssetsData.Count;index++)
            {
                AssetsMain main = AssetsData[index];
                TempRow = Sheet.CreateRow(NextRowIndex);

                // 使用属性0
                TempRow.CreateCell(0).SetCellValue(main.UsingAttribute);
                // 资产编号1
                TempRow.CreateCell(1).SetCellValue(main.AssetsNum);
                // 资产分类代码2
                TempRow.CreateCell(2).SetCellValue(main.CatCode);
                // 资产分类名称3
                TempRow.CreateCell(3).SetCellValue("");
                // 资产名称4
                TempRow.CreateCell(4).SetCellValue(main.GoodsName);
                // 计量单位5
                TempRow.CreateCell(5).SetCellValue(main.MeasurementUnits);
                // 数量6
                TempRow.CreateCell(6).SetCellValue(main.Counts);
                // 品牌及规格型号7
                TempRow.CreateCell(7).SetCellValue(main.ModelSpecification);
                // 使用性质32
                TempRow.CreateCell(32).SetCellValue("");
                // 取得方式9
                TempRow.CreateCell(9).SetCellValue(main.UsingStyle);
                // 使用状况10
                TempRow.CreateCell(10).SetCellValue(main.UsingState);
                // 使用方向11
                TempRow.CreateCell(11).SetCellValue("");
                // 使用人12
                TempRow.CreateCell(12).SetCellValue(main.UsePeople);
                // 价值类型13
                TempRow.CreateCell(13).SetCellValue(main.ValueType);
                // 价值14
                TempRow.CreateCell(14).SetCellValue(main.Money.ToString());
                // 财政性资金15
                TempRow.CreateCell(15).SetCellValue(main.GovMoney.ToString());
                // 非财政性资金16
                TempRow.CreateCell(16).SetCellValue(main.NoneGovMoney.ToString());
                // 净值17
                TempRow.CreateCell(17).SetCellValue(main.NetWorth.ToString());
                // 取得日期19
                TempRow.CreateCell(19).SetCellValue(main.GetDate.ToShortDateString());
                // 使用管理部门20
                TempRow.CreateCell(20).SetCellValue(main.UseDepartment);
                // 入账形式21
                TempRow.CreateCell(21).SetCellValue("");
                // 入账科目22
                TempRow.CreateCell(22).SetCellValue("");
                // 入账日期23
                TempRow.CreateCell(23).SetCellValue(main.PostingDate.ToShortDateString());
                // 会计凭证号24
                TempRow.CreateCell(24).SetCellValue(main.AcountDocNum);
                // 存放地点25
                TempRow.CreateCell(25).SetCellValue(main.StorePlace);
                // 备注56
                TempRow.CreateCell(56).SetCellValue(main.Memo);

                // 附属信息
                if (string.IsNullOrEmpty(main.CatCode))
                {
                    NextRowIndex++;
                    continue;
                }
                
                var @type = main.CatCode.GetAssetsTypeByCatCode();
                
                switch (@type)
                {
                    case AssetsTypes.Land:
                        //
                        //
                        //
                        var attrData = AssetsService.GetLand(main.AssetsNum);

                        if (null != attrData)
                        {
                            //////////////////////////////////////////////////////////////////
                            // 土地
                            //////////////////////////////////////////////////////////////////
                            // 产权形式39
                            TempRow.CreateCell(39).SetCellValue(attrData.PropertyForm);
                            // 权属证明40
                            TempRow.CreateCell(40).SetCellValue(attrData.OwnershipCertificate);
                            // 权属性质45
                            TempRow.CreateCell(45).SetCellValue(attrData.OwnershipStyle);
                            // 权属年限38
                            TempRow.CreateCell(38).SetCellValue(attrData.OwnershipTime);
                            // 权属证号44
                            TempRow.CreateCell(44).SetCellValue(attrData.OwnershipCertifiateNum);
                            // 发证日期36
                            TempRow.CreateCell(36).SetCellValue(attrData.IssuingDate.ToShortDateString());
                            // 土地使用权人46
                            TempRow.CreateCell(46).SetCellValue(attrData.LandOwner);
                            // 土地使用权类型41
                            TempRow.CreateCell(41).SetCellValue(attrData.LandUserStyle);
                            // 地类（用途）48
                            // 坐落位置55
                            TempRow.CreateCell(55).SetCellValue(attrData.Location);
                            // 土地使用权面积42
                            TempRow.CreateCell(42).SetCellValue(attrData.AllArea.ToString());
                            // 独用面积53
                            TempRow.CreateCell(53).SetCellValue(attrData.SingleArea.ToString());
                            // 分摊面积54
                            TempRow.CreateCell(54).SetCellValue(attrData.SharingArea.ToString());
                            //////////////////////////////////////////////////////////////////
                            // 土地
                            //////////////////////////////////////////////////////////////////
                            
                        }
                        break;
                    case AssetsTypes.Car:
                        //
                        //
                        //
                        var carAttr = AssetsService.GetCar(main.AssetsNum);

                        if (null != carAttr)
                        {
                            //////////////////////////////////////////////////////////////////
                            // 车辆
                            //////////////////////////////////////////////////////////////////
                            // 编制情况8
                            TempRow.CreateCell(8).SetCellValue(carAttr.Organization);
                            // 行驶证注册登记日期18
                            TempRow.CreateCell(18).SetCellValue(carAttr.CarErollDare);
                            // 号牌号码27 
                            TempRow.CreateCell(27).SetCellValue(carAttr.CarNum);
                            // 发动机号码28
                            TempRow.CreateCell(28).SetCellValue(carAttr.CarEngineNum);
                            // 车辆识别代号(车架号)29
                            TempRow.CreateCell(29).SetCellValue(carAttr.CarRecCode);
                            // 排气量30
                            TempRow.CreateCell(30).SetCellValue(carAttr.CarExhaust);
                            // 车辆行驶证所有人31
                            TempRow.CreateCell(31).SetCellValue(carAttr.CarOwner);
                            // 车辆用途50
                            TempRow.CreateCell(50).SetCellValue(carAttr.CarUser);
                            //////////////////////////////////////////////////////////////////
                            // 车辆
                            //////////////////////////////////////////////////////////////////
                        }
                        break;
                    case AssetsTypes.Building:
                        //
                        //
                        //
                        var buildAttr = AssetsService.GetBuilding(main.AssetsNum);

                        if (null != buildAttr)
                        {
                            //////////////////////////////////////////////////////////////////
                            // 构筑物
                            //////////////////////////////////////////////////////////////////
                            // 产权形式39
                            TempRow.CreateCell(39).SetCellValue(buildAttr.PropertyForm);
                            // 权属证号44
                            TempRow.CreateCell(44).SetCellValue(buildAttr.OwnershipCertifiateNum);
                            // 坐落位置55
                            TempRow.CreateCell(55).SetCellValue(buildAttr.Location);
                            //////////////////////////////////////////////////////////////////
                            // 构筑物
                            //////////////////////////////////////////////////////////////////
                        }
                        break;
                    case AssetsTypes.Culturalrelic:
                        //
                        //
                        //
                        var culturaAttr = AssetsService.GetCulturalrelic(main.AssetsNum);

                        if (null != culturaAttr)
                        {

                            //////////////////////////////////////////////////////////////////
                            // 文物
                            //////////////////////////////////////////////////////////////////
                            // 文物等级33
                            TempRow.CreateCell(33).SetCellValue(culturaAttr.GoodsLevel);
                            // 藏品年代34
                            TempRow.CreateCell(34).SetCellValue(culturaAttr.Years);
                            // 来源地/产地35
                            TempRow.CreateCell(35).SetCellValue(culturaAttr.SourcePlace);
                            //////////////////////////////////////////////////////////////////
                            // 文物
                            //////////////////////////////////////////////////////////////////
                        }
                        break;
                    case AssetsTypes.Animalandplant:
                        //
                        //
                        //
                        var animalAttr = AssetsService.GetAnimalandplant(main.AssetsNum);

                        if (null != animalAttr)
                        {
                            //////////////////////////////////////////////////////////////////
                            // 特种动植物
                            //////////////////////////////////////////////////////////////////
                            // 来源地/产地35
                            TempRow.CreateCell(35).SetCellValue(animalAttr.Palce);
                            //////////////////////////////////////////////////////////////////
                            // 特种动植物
                            //////////////////////////////////////////////////////////////////
                        }
                        break;
                    case AssetsTypes.Furniture:
                       
                        break;
                    case AssetsTypes.GeneralEquipment:
                        //
                        //
                        //
                        var GenaalAttr = AssetsService.GetGeneral(main.AssetsNum);

                        if (null != GenaalAttr)
                        {
                            //////////////////////////////////////////////////////////////////
                            // 通用设备
                            //////////////////////////////////////////////////////////////////
                            // 设备用途49
                            TempRow.CreateCell(49).SetCellValue(GenaalAttr.DevUse);
                            // 生产厂家26
                            TempRow.CreateCell(26).SetCellValue(GenaalAttr.DevFactory);
                            //////////////////////////////////////////////////////////////////
                            // 通用设备
                            //////////////////////////////////////////////////////////////////
                        }
                        break;
                    case AssetsTypes.House:
                        //
                        //
                        //
                        var houseAttr = AssetsService.GetHouse(main.AssetsNum);

                        if (null != houseAttr)
                        {
                            //////////////////////////////////////////////////////////////////
                            // 房屋
                            //////////////////////////////////////////////////////////////////
                            // 产权形式39
                            TempRow.CreateCell(39).SetCellValue(houseAttr.PropertyForm);
                            // 权属证明40
                            TempRow.CreateCell(40).SetCellValue(houseAttr.OwnershipCertificate);
                            // 权属证号44
                            TempRow.CreateCell(44).SetCellValue(houseAttr.OwnershipCertifiateNum);
                            // 权属年限38
                            TempRow.CreateCell(38).SetCellValue(houseAttr.OwnershipTime);
                            // 发证日期36
                            TempRow.CreateCell(36).SetCellValue(houseAttr.IssuingDate.ToShortDateString());
                            // 房屋所有权人47
                            TempRow.CreateCell(47).SetCellValue(houseAttr.HouseOwner);
                            // 设计用途51
                            TempRow.CreateCell(51).SetCellValue(houseAttr.DesignUse);
                            // 建筑结构52
                            TempRow.CreateCell(52).SetCellValue(houseAttr.BuildingStruccture);
                            // 坐落位置55
                            TempRow.CreateCell(55).SetCellValue(houseAttr.Location);
                            // 建筑面积43
                            TempRow.CreateCell(43).SetCellValue(houseAttr.Area.ToString());
                            //////////////////////////////////////////////////////////////////
                            // 房屋
                            //////////////////////////////////////////////////////////////////
                        }
                        break;
                    case AssetsTypes.SpecialEquipment:
                        //
                        //
                        //
                        var specialAttr = AssetsService.GetSpecial(main.AssetsNum);

                        if (null != specialAttr)
                        {
                            //////////////////////////////////////////////////////////////////
                            // 专用设备
                            //////////////////////////////////////////////////////////////////
                            // 生产厂家26
                            TempRow.CreateCell(26).SetCellValue(specialAttr.DevFactory);
                            //////////////////////////////////////////////////////////////////
                            // 专用设备
                            //////////////////////////////////////////////////////////////////
                        }
                        break;
                    case AssetsTypes.Book:
                        //
                        //
                        //
                        var bookAttr = AssetsService.GetBook(main.AssetsNum);

                        if (null != bookAttr)
                        {
                            //////////////////////////////////////////////////////////////////
                            // 图书
                            //////////////////////////////////////////////////////////////////
                            // 投入使用日期37
                            TempRow.CreateCell(37).SetCellValue(bookAttr.UseDate.ToShortDateString());
                            //////////////////////////////////////////////////////////////////
                            // 图书
                            //////////////////////////////////////////////////////////////////
                        }
                        break;
                }

                //
                // 换行
                //
                NextRowIndex++;
                Thread.Sleep(10);
            }
        }

        protected override void FillBody(ref ProgressInfo progressinfo)
        {
  
            if (null == AssetsData)
                return;
            progressinfo.TotalAssetsNum = AssetsData.Count;
            double step = 98.0 / AssetsData.Count;
            double percent = 0;
            for (int index = 0; index < AssetsData.Count; index++)
            {
                progressinfo.ImportedAssetsNum = index + 1;
                percent+= step;
                progressinfo.ImportedPercentVal = (int)percent;
                 AssetsMain main = AssetsData[index];
                TempRow = Sheet.CreateRow(NextRowIndex);

                // 使用属性0
                TempRow.CreateCell(0).SetCellValue(main.UsingAttribute);
                // 资产编号1
                TempRow.CreateCell(1).SetCellValue(main.UsedNum1);
                // 资产分类代码2
                TempRow.CreateCell(2).SetCellValue(main.CatCode);
                // 资产分类名称3
                TempRow.CreateCell(3).SetCellValue("");
                // 资产名称4
                TempRow.CreateCell(4).SetCellValue(main.GoodsName);
                // 计量单位5
                TempRow.CreateCell(5).SetCellValue(main.MeasurementUnits);
                // 数量6
                TempRow.CreateCell(6).SetCellValue(main.Counts);
                // 品牌及规格型号7
                TempRow.CreateCell(7).SetCellValue(main.ModelSpecification);
                // 使用性质32
                TempRow.CreateCell(32).SetCellValue("");
                // 取得方式9
                TempRow.CreateCell(9).SetCellValue(main.UsingStyle);
                // 使用状况10
                TempRow.CreateCell(10).SetCellValue(main.UsingState);
                // 使用方向11
                TempRow.CreateCell(11).SetCellValue("");
                // 使用人12
                TempRow.CreateCell(12).SetCellValue(main.UsePeople);
                // 价值类型13
                TempRow.CreateCell(13).SetCellValue(main.ValueType);
                // 价值14
                TempRow.CreateCell(14).SetCellValue(main.Money.ToString());
                // 财政性资金15
                TempRow.CreateCell(15).SetCellValue(main.GovMoney.ToString());
                // 非财政性资金16
                TempRow.CreateCell(16).SetCellValue(main.NoneGovMoney.ToString());
                // 净值17
                TempRow.CreateCell(17).SetCellValue(main.NetWorth.ToString());
                // 取得日期19
                TempRow.CreateCell(19).SetCellValue(main.GetDate.ToShortDateString());
                // 使用管理部门20
                TempRow.CreateCell(20).SetCellValue(main.UseDepartment);
                // 入账形式21
                TempRow.CreateCell(21).SetCellValue("");
                // 入账科目22
                TempRow.CreateCell(22).SetCellValue("");
                // 入账日期23
                TempRow.CreateCell(23).SetCellValue(main.PostingDate.ToShortDateString());
                // 会计凭证号24
                TempRow.CreateCell(24).SetCellValue(main.AcountDocNum);
                // 存放地点25
                TempRow.CreateCell(25).SetCellValue(main.StorePlace);
                // 备注56
                TempRow.CreateCell(56).SetCellValue(main.Memo);

                // 附属信息
                if (string.IsNullOrEmpty(main.CatCode))
                {
                    NextRowIndex++;
                    continue;
                }

                var @type = main.CatCode.GetAssetsTypeByCatCode();

                switch (@type)
                {
                    case AssetsTypes.Land:
                        //
                        //
                        //
                        var attrData = AssetsService.GetLand(main.AssetsNum);

                        if (null != attrData)
                        {
                            //////////////////////////////////////////////////////////////////
                            // 土地
                            //////////////////////////////////////////////////////////////////
                            // 产权形式39
                            TempRow.CreateCell(39).SetCellValue(attrData.PropertyForm);
                            // 权属证明40
                            TempRow.CreateCell(40).SetCellValue(attrData.OwnershipCertificate);
                            // 权属性质45
                            TempRow.CreateCell(45).SetCellValue(attrData.OwnershipStyle);
                            // 权属年限38
                            TempRow.CreateCell(38).SetCellValue(attrData.OwnershipTime);
                            // 权属证号44
                            TempRow.CreateCell(44).SetCellValue(attrData.OwnershipCertifiateNum);
                            // 发证日期36
                            TempRow.CreateCell(36).SetCellValue(attrData.IssuingDate.ToShortDateString());
                            // 土地使用权人46
                            TempRow.CreateCell(46).SetCellValue(attrData.LandOwner);
                            // 土地使用权类型41
                            TempRow.CreateCell(41).SetCellValue(attrData.LandUserStyle);
                            // 地类（用途）48
                            // 坐落位置55
                            TempRow.CreateCell(55).SetCellValue(attrData.Location);
                            // 土地使用权面积42
                            TempRow.CreateCell(42).SetCellValue(attrData.AllArea.ToString());
                            // 独用面积53
                            TempRow.CreateCell(53).SetCellValue(attrData.SingleArea.ToString());
                            // 分摊面积54
                            TempRow.CreateCell(54).SetCellValue(attrData.SharingArea.ToString());
                            //////////////////////////////////////////////////////////////////
                            // 土地
                            //////////////////////////////////////////////////////////////////

                        }
                        break;
                    case AssetsTypes.Car:
                        //
                        //
                        //
                        var carAttr = AssetsService.GetCar(main.AssetsNum);

                        if (null != carAttr)
                        {
                            //////////////////////////////////////////////////////////////////
                            // 车辆
                            //////////////////////////////////////////////////////////////////
                            // 编制情况8
                            TempRow.CreateCell(8).SetCellValue(carAttr.Organization);
                            // 行驶证注册登记日期18
                            TempRow.CreateCell(18).SetCellValue(carAttr.CarErollDare);
                            // 号牌号码27 
                            TempRow.CreateCell(27).SetCellValue(carAttr.CarNum);
                            // 发动机号码28
                            TempRow.CreateCell(28).SetCellValue(carAttr.CarEngineNum);
                            // 车辆识别代号(车架号)29
                            TempRow.CreateCell(29).SetCellValue(carAttr.CarRecCode);
                            // 排气量30
                            TempRow.CreateCell(30).SetCellValue(carAttr.CarExhaust);
                            // 车辆行驶证所有人31
                            TempRow.CreateCell(31).SetCellValue(carAttr.CarOwner);
                            // 车辆用途50
                            TempRow.CreateCell(50).SetCellValue(carAttr.CarUser);
                            //////////////////////////////////////////////////////////////////
                            // 车辆
                            //////////////////////////////////////////////////////////////////
                        }
                        break;
                    case AssetsTypes.Building:
                        //
                        //
                        //
                        var buildAttr = AssetsService.GetBuilding(main.AssetsNum);

                        if (null != buildAttr)
                        {
                            //////////////////////////////////////////////////////////////////
                            // 构筑物
                            //////////////////////////////////////////////////////////////////
                            // 产权形式39
                            TempRow.CreateCell(39).SetCellValue(buildAttr.PropertyForm);
                            // 权属证号44
                            TempRow.CreateCell(44).SetCellValue(buildAttr.OwnershipCertifiateNum);
                            // 坐落位置55
                            TempRow.CreateCell(55).SetCellValue(buildAttr.Location);
                            //////////////////////////////////////////////////////////////////
                            // 构筑物
                            //////////////////////////////////////////////////////////////////
                        }
                        break;
                    case AssetsTypes.Culturalrelic:
                        //
                        //
                        //
                        var culturaAttr = AssetsService.GetCulturalrelic(main.AssetsNum);

                        if (null != culturaAttr)
                        {

                            //////////////////////////////////////////////////////////////////
                            // 文物
                            //////////////////////////////////////////////////////////////////
                            // 文物等级33
                            TempRow.CreateCell(33).SetCellValue(culturaAttr.GoodsLevel);
                            // 藏品年代34
                            TempRow.CreateCell(34).SetCellValue(culturaAttr.Years);
                            // 来源地/产地35
                            TempRow.CreateCell(35).SetCellValue(culturaAttr.SourcePlace);
                            //////////////////////////////////////////////////////////////////
                            // 文物
                            //////////////////////////////////////////////////////////////////
                        }
                        break;
                    case AssetsTypes.Animalandplant:
                        //
                        //
                        //
                        var animalAttr = AssetsService.GetAnimalandplant(main.AssetsNum);

                        if (null != animalAttr)
                        {
                            //////////////////////////////////////////////////////////////////
                            // 特种动植物
                            //////////////////////////////////////////////////////////////////
                            // 来源地/产地35
                            TempRow.CreateCell(35).SetCellValue(animalAttr.Palce);
                            //////////////////////////////////////////////////////////////////
                            // 特种动植物
                            //////////////////////////////////////////////////////////////////
                        }
                        break;
                    case AssetsTypes.Furniture:

                        break;
                    case AssetsTypes.GeneralEquipment:
                        //
                        //
                        //
                        var GenaalAttr = AssetsService.GetGeneral(main.AssetsNum);

                        if (null != GenaalAttr)
                        {
                            //////////////////////////////////////////////////////////////////
                            // 通用设备
                            //////////////////////////////////////////////////////////////////
                            // 设备用途49
                            TempRow.CreateCell(49).SetCellValue(GenaalAttr.DevUse);
                            // 生产厂家26
                            TempRow.CreateCell(26).SetCellValue(GenaalAttr.DevFactory);
                            //////////////////////////////////////////////////////////////////
                            // 通用设备
                            //////////////////////////////////////////////////////////////////
                        }
                        break;
                    case AssetsTypes.House:
                        //
                        //
                        //
                        var houseAttr = AssetsService.GetHouse(main.AssetsNum);

                        if (null != houseAttr)
                        {
                            //////////////////////////////////////////////////////////////////
                            // 房屋
                            //////////////////////////////////////////////////////////////////
                            // 产权形式39
                            TempRow.CreateCell(39).SetCellValue(houseAttr.PropertyForm);
                            // 权属证明40
                            TempRow.CreateCell(40).SetCellValue(houseAttr.OwnershipCertificate);
                            // 权属证号44
                            TempRow.CreateCell(44).SetCellValue(houseAttr.OwnershipCertifiateNum);
                            // 权属年限38
                            TempRow.CreateCell(38).SetCellValue(houseAttr.OwnershipTime);
                            // 发证日期36
                            TempRow.CreateCell(36).SetCellValue(houseAttr.IssuingDate.ToShortDateString());
                            // 房屋所有权人47
                            TempRow.CreateCell(47).SetCellValue(houseAttr.HouseOwner);
                            // 设计用途51
                            TempRow.CreateCell(51).SetCellValue(houseAttr.DesignUse);
                            // 建筑结构52
                            TempRow.CreateCell(52).SetCellValue(houseAttr.BuildingStruccture);
                            // 坐落位置55
                            TempRow.CreateCell(55).SetCellValue(houseAttr.Location);
                            // 建筑面积43
                            TempRow.CreateCell(43).SetCellValue(houseAttr.Area.ToString());
                            //////////////////////////////////////////////////////////////////
                            // 房屋
                            //////////////////////////////////////////////////////////////////
                        }
                        break;
                    case AssetsTypes.SpecialEquipment:
                        //
                        //
                        //
                        var specialAttr = AssetsService.GetSpecial(main.AssetsNum);

                        if (null != specialAttr)
                        {
                            //////////////////////////////////////////////////////////////////
                            // 专用设备
                            //////////////////////////////////////////////////////////////////
                            // 生产厂家26
                            TempRow.CreateCell(26).SetCellValue(specialAttr.DevFactory);
                            //////////////////////////////////////////////////////////////////
                            // 专用设备
                            //////////////////////////////////////////////////////////////////
                        }
                        break;
                    case AssetsTypes.Book:
                        //
                        //
                        //
                        var bookAttr = AssetsService.GetBook(main.AssetsNum);

                        if (null != bookAttr)
                        {
                            //////////////////////////////////////////////////////////////////
                            // 图书
                            //////////////////////////////////////////////////////////////////
                            // 投入使用日期37
                            TempRow.CreateCell(37).SetCellValue(bookAttr.UseDate.ToShortDateString());
                            //////////////////////////////////////////////////////////////////
                            // 图书
                            //////////////////////////////////////////////////////////////////
                        }
                        break;
                }

                //
                // 换行
                //
                NextRowIndex++;
                Thread.Sleep(10);
            }
        }
    }
}