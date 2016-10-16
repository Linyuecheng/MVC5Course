using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        // GET: EF
        FabricsEntities db = new FabricsEntities();

        public ActionResult Index()
        {
            var data = db.Product.Where(p => p.ProductName.Contains("White"));
            return View(data);
        }


        public ActionResult Create()
        {
            var product = new Product()
            {
                ProductName = "White Cat",
                Active = true,
                Price = 100,
                Stock = 5
            };
            db.Product.Add(product);
            db.SaveChanges();   // 加上這行才會把變更真正存到資料庫
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var product = db.Product.Find(id);
            return View(product);
        }

        public ActionResult Update(int id)
        {
            var product = db.Product.Find(id);
            product.ProductName += "!";
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Add20Persent()
        {
            var data = db.Product.Where(p => p.ProductName.Contains("White"));
            foreach (var item in data)
            {
                item.Price = item.Price * 1.2m;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}