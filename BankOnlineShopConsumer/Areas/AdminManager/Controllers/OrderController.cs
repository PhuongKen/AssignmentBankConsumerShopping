using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BankOnlineShopConsumer.Areas.AdminManager.Controllers
{
    public class OrderController : Controller
    {
        OrderDao dao = new OrderDao();
        // GET: AdminManager/Order
        public ActionResult Index()
        {
            return View(dao.GetListOrder());
        }

        public ActionResult Detail(int? id)
        {
            if(id==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var result = dao.GetOrderById(id);
            if (result == null)
                return HttpNotFound();
            return View(result);
        }
    }
}