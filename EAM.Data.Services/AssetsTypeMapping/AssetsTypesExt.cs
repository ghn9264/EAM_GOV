using System;
using System.Collections.Generic;
using System.Linq;

namespace EAM.Data.Services
{
    public static class AssetsTypesExt
    {
        #region AssetsTypeMapping 
        public static List<CodeTypeItem> AssetsTypeMapping = new List<CodeTypeItem>
        {
            new CodeTypeItem {CodePrix = "101", AssetsType = AssetsTypes.Land},
            new CodeTypeItem {CodePrix = "102", AssetsType = AssetsTypes.House},
            new CodeTypeItem {CodePrix = "103", AssetsType = AssetsTypes.Building},
            new CodeTypeItem {CodePrix = "203", AssetsType = AssetsTypes.Car},
            new CodeTypeItem {CodePrix = "201", AssetsType = AssetsTypes.GeneralEquipment},
            new CodeTypeItem {CodePrix = "202", AssetsType = AssetsTypes.GeneralEquipment},
            new CodeTypeItem {CodePrix = "204", AssetsType = AssetsTypes.GeneralEquipment},
            new CodeTypeItem {CodePrix = "210", AssetsType = AssetsTypes.GeneralEquipment},
            new CodeTypeItem {CodePrix = "220", AssetsType = AssetsTypes.GeneralEquipment},
            new CodeTypeItem {CodePrix = "230", AssetsType = AssetsTypes.GeneralEquipment},
            new CodeTypeItem {CodePrix = "231", AssetsType = AssetsTypes.GeneralEquipment},
            new CodeTypeItem {CodePrix = "232", AssetsType = AssetsTypes.GeneralEquipment},
            new CodeTypeItem {CodePrix = "240", AssetsType = AssetsTypes.GeneralEquipment},
            new CodeTypeItem {CodePrix = "241", AssetsType = AssetsTypes.GeneralEquipment},
            new CodeTypeItem {CodePrix = "242", AssetsType = AssetsTypes.GeneralEquipment},
            new CodeTypeItem {CodePrix = "301", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "302", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "303", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "304", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "305", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "306", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "307", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "308", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "310", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "311", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "313", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "314", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "315", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "316", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "317", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "318", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "319", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "320", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "321", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "322", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "323", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "324", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "325", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "326", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "327", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "328", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "339", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "350", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "351", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "352", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "360", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "370", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "371", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "372", AssetsType = AssetsTypes.SpecialEquipment},
            new CodeTypeItem {CodePrix = "401", AssetsType = AssetsTypes.Culturalrelic},
            new CodeTypeItem {CodePrix = "402", AssetsType = AssetsTypes.Culturalrelic},
            new CodeTypeItem {CodePrix = "601", AssetsType = AssetsTypes.Furniture},
            new CodeTypeItem {CodePrix = "602", AssetsType = AssetsTypes.Furniture},
            new CodeTypeItem {CodePrix = "501", AssetsType = AssetsTypes.Book},
            new CodeTypeItem {CodePrix = "603", AssetsType = AssetsTypes.Animalandplant},
            new CodeTypeItem {CodePrix = "604", AssetsType = AssetsTypes.Animalandplant}
        };
        #endregion

        public static string GetCnName(this AssetsTypes assetsType)
        {
            switch (assetsType)
            {
                case AssetsTypes.Land:
                    return "土地";
                    break;
                case AssetsTypes.House:
                    return "房屋";
                    break;
                case AssetsTypes.Building:
                    return "建筑物";
                    break;
                case AssetsTypes.Car:
                    return "车辆";
                    break;
                case AssetsTypes.GeneralEquipment:
                    return "通用设备";
                    break;
                case AssetsTypes.SpecialEquipment:
                    return "专用设备";
                    break;
                case AssetsTypes.Culturalrelic:
                    return "文物陈列品";
                    break;
                case AssetsTypes.Furniture:
                    return "家具、用具及装具";
                    break;
                case AssetsTypes.Book:
                    return "图书档案";
                    break;
                case AssetsTypes.Animalandplant:
                    return "特种动植物";
                    break;
                default:
                    return "SB";
                    break;
            }
        }

        public static AssetsTypes GetAssetsTypeByCatCode(this string catCode)
        {
            catCode = catCode.Substring(0, 3);
            var codeTypeItem = AssetsTypeMapping.FirstOrDefault(x => x.CodePrix == catCode);
            return codeTypeItem.AssetsType;
            return (AssetsTypes) Enum.Parse(typeof (AssetsTypes), codeTypeItem.CodePrix);
        }

        public static string GetAssetsTypeNameByCatCode(this string catCode)
        {
            catCode = catCode.Substring(0, 3);
            var codeTypeItem = Enumerable.FirstOrDefault<CodeTypeItem>(AssetsTypeMapping, x => x.CodePrix == catCode);
            if (null != codeTypeItem)
                return codeTypeItem.AssetsType.GetCnName();
            return "BigBigSB";
        }
    }
}