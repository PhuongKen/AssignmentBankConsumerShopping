using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankOnlineShopConsumer.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        // GET: Register
        public ActionResult Register(User user)
        {

            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Insert(user);
                if (result == 1)
                {
                    ViewBag.SuccessMessage = "Bạn đã đăng kí tài khoản thành công, đăng nhập để tiếp tục.";
                    return RedirectToAction("Index","Login");
                }
                else if (result == -1)
                {
                    ViewBag.DuplicateMessage = "Tài khoản này đã tồn tại";
                }
                else
                {
                    ViewBag.errorAll = "Lỗi không mong muốn";
                }
            }
            return View("Index");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}