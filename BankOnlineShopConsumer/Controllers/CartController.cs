using BankOnlineShopConsumer.Common;
using BankOnlineShopConsumer.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BankOnlineShopConsumer.Controllers
{
    public class CartController : Controller
    {

        //public const string CartSession = "CartSession";

        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult AddItem(long productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[CommonConstants.CartSession];

            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CommonConstants.CartSession] = list;
            }
            else
            {
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                Session[CommonConstants.CartSession] = list;
            }

            return RedirectToAction("Index");
        }


        public JsonResult DeleteAll()
        {
            Session[CommonConstants.CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null && jsonItem.Quantity >0)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }

            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string shipname, string mobile, string address, string email)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;

            var userSession = (UserLogin)Session[BankOnlineShopConsumer.Common.CommonConstants.USER_SESSION];

            order.CustomerID = userSession.UserID;
            order.ShipName = shipname;
            order.ShipMobile = mobile;
            order.ShipAddress = address;
            order.ShipEmail = email;

            try
            {
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
                Session[CommonConstants.CartSession] = null;
            }
            catch(Exception ex)
            {
                return Redirect("/loi-thanh-toan");
            }
            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult ErrorPayment()
        {
            return View();
        }
    }
}