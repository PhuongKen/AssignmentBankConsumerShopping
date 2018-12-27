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
            var userSession = (UserLogin)Session[BankOnlineShopConsumer.Common.CommonConstants.USER_SESSION];
            var orderID = new OrderDao().Find(userSession.UserID);
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
                if (ModelState.IsValid)
                {
                    transaction.senderAccountNumber = Convert.ToInt64(Session["PartnerAccount"].ToString());
                    transaction.receiverAccountNumber = 200000000005;
                    transaction.status = 1;
                    transaction.type = 1;
                    transaction.createAt = DateTime.Now;
                    transaction.updateAt = DateTime.Now;
                    var userSession = (UserLogin)Session[BankOnlineShopConsumer.Common.CommonConstants.USER_SESSION];
                    var orderID = new OrderDao().Find(userSession.UserID);
                    //long paypalId = 300000000005;
                    int result = db.Transaction(transaction);

                    var total = new OrderDetailDao().Find(orderID);
                    
                    if (orderID.ToString() != transaction.billId)
                    {
                        ModelState.AddModelError("", "Bạn đã thay đổi mã đơn hàng mà bạn đã chọn để thanh toán." +
                            " Chúng tôi không thể thực hiện tác vụ.");
                        ViewBag.orderID = orderID;
                        ViewBag.total = total;
                    }

                    if (total != transaction.amount)
                    {
                        ModelState.AddModelError("", "Bạn đã thay đổi số tiền trong mã đơn hàng mà bạn đã chọn để thanh toán." +
                            " Chúng tôi không thể thực hiện tác vụ.");
                        ViewBag.orderID = orderID;
                        ViewBag.total = total;
                    }

                    if (result == 1)
                    {
                        var orderTrue = new OrderDao().UpdateStatus(orderID);
                        Session[CommonConstants.CartSession] = null;
                        return Redirect("/hoan-thanh");
                    }
                    else if (result == -1)
                    {
                        ModelState.AddModelError("", "Số tiền trong tài khoản của bạn không đủ để thực hiện giao dịch này.");
                        ViewBag.orderID = orderID;
                        ViewBag.total = total;
                    }
                }

                var userSes= (UserLogin)Session[BankOnlineShopConsumer.Common.CommonConstants.USER_SESSION];
                var ID = new OrderDao().Find(userSes.UserID);
                var totalPrice = new OrderDetailDao().Find(ID);
                ViewBag.orderID = ID;
                ViewBag.total = totalPrice;
                ViewBag.name = transaction.name;
                ViewBag.content = transaction.content;
                return View(transaction);
            }
            catch
            {
                //var userSes = (UserLogin)Session[BankOnlineShopConsumer.Common.CommonConstants.USER_SESSION];
                //var ID = new OrderDao().Find(userSes.UserID);
                //var totalPrice = new OrderDetailDao().Find(ID);
                //ViewBag.orderID = ID;
                //ViewBag.total = totalPrice;
                //ViewBag.name = transaction.name;
                //ViewBag.content = transaction.content;
                return View(transaction);
            }
        }

    }
}