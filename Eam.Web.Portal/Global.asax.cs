using System.Collections.Generic;
using EAM.Inventory;
using NPoco;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Eam.Web.Portal
{
    public class MvcApplication : HttpApplication
    {
        public MvcApplication()
        {
            /*BeginRequest += new EventHandler(MvcApplication_BeginRequest);
            EndRequest += new EventHandler(MvcApplication_EndRequest);
            AuthenticateRequest += new EventHandler(Application_AuthenticateRequest);*/

            /*var h = new InventoryHelper(@"C:\Inventory.db");
            h.TestConnection();

            var list = new List<InventoryInfo>();
            list.Add(new InventoryInfo { GoodsName = "dddddddddd", GoodsNo = "22222222222", GoodsFiscalNo = "444444444444", GoodsModel = "ewwew", DetailId = 1, GoodsStatus = 1, GoodsNum = 0, GoodsStatusName = "dfda", Remark = "eee" });
            list.Add(new InventoryInfo { GoodsName = "dddddddddd", GoodsNo = "22222222222", GoodsFiscalNo = "444444444444", GoodsModel = "ewwew", DetailId = 1, GoodsStatus = 1, GoodsNum = 0, GoodsStatusName = "dfda", Remark = "eee" });
            list.Add(new InventoryInfo { GoodsName = "dddddddddd", GoodsNo = "22222222222", GoodsFiscalNo = "444444444444", GoodsModel = "ewwew", DetailId = 1, GoodsStatus = 1, GoodsNum = 0, GoodsStatusName = "dfda", Remark = "eee" });
            list.Add(new InventoryInfo { GoodsName = "dddddddddd", GoodsNo = "22222222222", GoodsFiscalNo = "444444444444", GoodsModel = "ewwew", DetailId = 1, GoodsStatus = 1, GoodsNum = 0, GoodsStatusName = "dfda", Remark = "eee" });
            list.Add(new InventoryInfo { GoodsName = "dddddddddd", GoodsNo = "22222222222", GoodsFiscalNo = "444444444444", GoodsModel = "ewwew", DetailId = 1, GoodsStatus = 1, GoodsNum = 0, GoodsStatusName = "dfda", Remark = "eee" });
            list.Add(new InventoryInfo { GoodsName = "dddddddddd", GoodsNo = "22222222222", GoodsFiscalNo = "444444444444", GoodsModel = "ewwew", DetailId = 1, GoodsStatus = 1, GoodsNum = 0, GoodsStatusName = "dfda", Remark = "eee" });
 
            h.InsertInventory(list);

            var list2 = h.GetInventoryList(@"C:\Inventory.db");
            Console.WriteLine(list2);*/
        }

        private void MvcApplication_BeginRequest(object sender, EventArgs e)
        {
            /*Database db = new Database(ConnStr, DatabaseType.MySQL);
            HttpContext.Current.Cache["DataBase"] = db;*/
        }

        private void MvcApplication_EndRequest(object sender, EventArgs e)
        {
            Database db = HttpContext.Current.Cache["DataBase"] as Database;
            if (null != db)
                db.CloseSharedConnection();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var application = sender as HttpApplication;
            /*if (application.Context.User != null)
            {
                var user = application.Context.User;
                var identity = user.Identity as FormsIdentity;
                if (identity != null)
                {
                    // We could explicitly construct an Principal object with roles info using System.Security.Principal.GenericPrincipal
                    var principalWithRoles = new GenericPrincipal(identity, identity.Ticket.UserData.Split(','));
                    // Replace the user object
                    application.Context.User = principalWithRoles;
                }
            }*/
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ControllerBuilder.Current.SetControllerFactory(new InjectControllerFactory());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

         
    }
}