using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EAM.Data.Repositories;
using EAM.Data.Services;
using EAM.Data.Services.Query;

namespace Eam.Web.Portal.Controllers
{
    public class HomeController : Controller
    {
         
        public ActionResult Index()
        {
            
            return RedirectToAction("Index", "Auth", new {Area = "Account"});
        } 



    }
}