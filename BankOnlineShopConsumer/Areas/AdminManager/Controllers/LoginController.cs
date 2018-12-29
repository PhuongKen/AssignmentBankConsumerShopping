using BankOnlineShopConsumer.Areas.AdminManager.Models;
using BankOnlineShopConsumer.Common;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankOnlineShopConsumer.Areas.AdminManager.Controllers
{
    public class LoginController : Controller
    {
        // GET: AdminManager/Login
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
                var dao = new AdminDao();
                var result = dao.Login(model.UserName, model.PassWord);
                if (result == 1)
                {
                    var admin = dao.GetById(model.UserName);
                    var adminSession = new AdminLogin();
                    adminSession.UserName = admin.UserName;
                    adminSession.UserID = admin.ID;
                    Session.Add(CommonConstants.ADMIN_SESSION, adminSession);
                    return RedirectToAction("Index", "Home");
                }
                if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản chưa được kích hoạt. Hoặc đã bị khóa.");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng.");
                }
            }
            return View("Index");
        }

       
    }
}