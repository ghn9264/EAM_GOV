using System.Web.Mvc;

namespace Eam.Web.Portal.Areas.SycData
{
    public class SycDataAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SycData";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SycData_default",
                "SycData/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}