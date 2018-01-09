using System.Web.Mvc;

namespace Eam.Web.Portal.Areas.AssetsFind
{
    public class AssetsFindAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AssetsFind";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AssetsFind_default",
                "AssetsFind/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}