using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eam.Web.Portal
{
    public class PortalSetting
    {
        public static string UpLoadTempPath = HttpContext.Current.Server.MapPath("~/Temp");
        public static string UpLoadPicPath = HttpContext.Current.Server.MapPath("~/Upload/AssetsPic/");
        public static string UpLoadPicHttpPrix = "/Upload/AssetsPic/";

        /// <summary>
        /// 资产二维码存放地址
        /// </summary>
        public static string QrCodesPath = HttpContext.Current.Server.MapPath("~/QrCodes/");
        /// <summary>
        /// 资产二维码HTTP访问头
        /// </summary>
        public static string QrCodesHttpPrix = "/QrCodes/";

        /// <summary>
        /// 资产二维码标题
        /// </summary>
        public static string QrCodeTitle
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["QrCodeTitle"]; }
        }

    }
}