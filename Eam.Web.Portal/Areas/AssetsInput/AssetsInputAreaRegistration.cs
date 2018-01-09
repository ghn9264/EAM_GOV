using System.Web.Mvc;

namespace Eam.Web.Portal.Areas.AssetsInput
{
    public class AssetsInputAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AssetsInput";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AssetsInput_default",
                "AssetsInput/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}