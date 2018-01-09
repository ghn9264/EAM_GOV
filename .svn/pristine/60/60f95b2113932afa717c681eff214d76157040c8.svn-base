using System.Web.Mvc;

namespace Eam.Web.Portal.Areas.QrPrint
{
    public class QrPrintAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "QrPrint";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "QrPrint_default",
                "QrPrint/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}