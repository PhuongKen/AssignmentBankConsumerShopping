using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BankOnlineShopConsumer.Areas.AdminManager.Controllers
{
    public class UserController : Controller
    {
        // GET: AdminManager/User
        public ActionResult Index()
        {
            var dao = new UserDao();
            return View(dao.ListUser());
        }

        public ActionResult Detail(int? id)
        {
            var dao = new UserDao();
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var result = dao.GetUserById(id);
            if (result == null)
                return HttpNotFound();
            return View(result);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var dao = new UserDao();
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var result = dao.GetUserById(id);
            if (result == null)
                return HttpNotFound();
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.update(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public JsonResult Delete(int? id)
        {
            var dao = new UserDao();
            if (id == null)
                return Json(new
                {
                    status = false
                });
            var result = dao.GetUserById(id);
            if (result == null)
                return Json(new
                {
                    status = false
                });
            var ok = dao.delete(result);
            if (ok)
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}