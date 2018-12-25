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
    public class PayCheckoutController : Controller
    {

        BankService db = new BankService();

        [HttpGet]
        public ActionResult Checkout()
        {
            var orderID = new OrderDao().Find();
            var total = new OrderDetailDao().Find(orderID);

            ViewBag.orderID = orderID;
            ViewBag.total = total;
            return View();

        }

        [HttpPost]
        public ActionResult Checkout(Transaction transaction)
        {
            
            try
            {
                transaction.senderAccountNumber = Convert.ToInt64(Session["PartnerAccount"].ToString());
                transaction.receiverAccountNumber = 200000000005;
                transaction.status = 1;
                transaction.type = 1;
                transaction.createAt = DateTime.Now;
                transaction.updateAt = DateTime.Now;
                var orderID = new OrderDao().Find();
                //long paypalId = 300000000005;
                if (db.Transaction(transaction))
                {
                    var orderTrue = new OrderDao().UpdateStatus(orderID);
                    Session[CommonConstants.CartSession] = null;
                    return Redirect("/hoan-thanh");
                }
                else
                {
                    return Redirect("/loi-thanh-toan");
                }
            }
            catch (Exception ex)
            {
                return Redirect("/loi-thanh-toan");
            }
            
        }
    }
}