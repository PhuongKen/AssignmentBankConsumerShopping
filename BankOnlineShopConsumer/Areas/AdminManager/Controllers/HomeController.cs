using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankOnlineShopConsumer.Areas.AdminManager.Controllers
{
    public class HomeController : BaseController
    {
        // GET: AdminManager/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}