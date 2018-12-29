using BankOnlineShopConsumer.Common;
using BankOnlineShopConsumer.Models;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankOnlineShopConsumer.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, model.PassWord);
                if(result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index","Home");
                }
                if(result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản chưa được kích hoạt. Hoặc đã bị khóa.");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password not correct.");
                }
            }
            return View("Index");
        }

        public ActionResult LogOut()
        {
            Session.Add(CommonConstants.USER_SESSION, null);
            return RedirectToAction("Index");
        }
    }
}