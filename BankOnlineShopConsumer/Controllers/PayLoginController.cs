using BankOnlineShopConsumer.Common;
using BankOnlineShopConsumer.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankOnlineShopConsumer.Controllers
{
    public class PayLoginController : Controller
    {
        BankService db = new BankService();

        // GET: PayLogin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(PartnerAccount partner)
        {
            if (db.LoginPartnerAccount(partner) == false)
            {
                return View(partner);
            }
            else
            {
                Session["PartnerAccount"] = db.Find(partner.partnerAccount).accountNumber;
                return RedirectToAction("Checkout","PayCheckout");
            }
        }

        public JsonResult SaveOrder(string shipname, string mobile, string address, string email)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            var userSession = (UserLogin)Session[BankOnlineShopConsumer.Common.CommonConstants.USER_SESSION];

            order.CustomerID = userSession.UserID;
            order.ShipName = shipname;
            order.ShipMobile = mobile;
            order.ShipAddress = address;
            order.ShipEmail = email;

            var id = new OrderDao().Insert(order);
            var cart = (List<CartItem>)Session[CommonConstants.CartSession];
            var detailDao = new OrderDetailDao();
            foreach (var item in cart)
            {
                var orderDetail = new OrderDetail();
                orderDetail.ProductID = item.Product.ID;
                orderDetail.OrderID = id;
                orderDetail.Price = item.Product.Price;
                orderDetail.Quantity = item.Quantity;
                detailDao.Insert(orderDetail);
            }
            return Json(new
            {
                status = true
            });
        }

    }
}