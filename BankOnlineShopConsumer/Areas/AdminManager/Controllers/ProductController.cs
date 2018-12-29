using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BankOnlineShopConsumer.Areas.AdminManager.Controllers
{
    public class ProductController : BaseController
    {
        ProductDao dao = new ProductDao();
        // GET: AdminManager/Product
        public ActionResult Index()
        {
            var result = dao.GetListProduct();
            return View(result);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var result = dao.ViewDetail(Convert.ToInt64(id));
            if (result == null)
                return HttpNotFound();
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Assets/client/images/"));
                    string pathstring = Path.Combine(path.ToString());
                    string filename1 = Guid.NewGuid() + Path.GetExtension(file.FileName);

                    bool isexist = Directory.Exists(pathstring);
                    if (!isexist)
                    {
                        Directory.CreateDirectory(pathstring);
                    }
                    string uploadpath = string.Format("{0}\\{1}", pathstring, filename1);
                    file.SaveAs(uploadpath);
                    product.Avartar = "/Assets/client/images/" + filename1;
                }
                var result = dao.Insert(product);
                if (result == true)
                {
                    return Redirect("/AdminManager/Product/Index");
                }
                else
                {
                    return View(product);
                }
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var result = dao.GetProductById(id);
            if (result == null)
                return HttpNotFound();
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Assets/client/images/"));
                    string pathstring = Path.Combine(path.ToString());
                    string filename1 = Guid.NewGuid() + Path.GetExtension(file.FileName);

                    bool isexist = Directory.Exists(pathstring);
                    if (!isexist)
                    {
                        Directory.CreateDirectory(pathstring);
                    }
                    string uploadpath = string.Format("{0}\\{1}", pathstring, filename1);
                    file.SaveAs(uploadpath);
                    product.Avartar = "/Assets/client/images/" + filename1;
                }
                var result = dao.update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public JsonResult Delete(int? id)
        {
            if (id == null)
                return Json(new
                {
                    status = false
                });
            var result = dao.GetProductById(id);
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