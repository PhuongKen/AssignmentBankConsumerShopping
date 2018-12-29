using BankOnlineShopConsumer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankOnlineShopConsumer.Areas.AdminManager.Controllers
{
    public class BaseController : Controller
    {
        // GET: AdminManager/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    System.Web.Routing.RouteValueDictionary(new { controller = "Login", action = "Index", Area = "AdminManager" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}