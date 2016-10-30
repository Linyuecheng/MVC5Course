using MVC5Course.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MBController : BaseController
    {
        // GET: MB
        [LocalDebugOnly]
        [Share頁面上常用的ViewBag變數資料]
        public ActionResult Index()
        {
            //ViewData["Temp1"] = "暫存資料 Temp1";  // 原本這段被搬到 ActionFilter 去了

            return View();
        }



        public ActionResult MyForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MyForm(ClientLoginViewModel c)
        {
            if (ModelState.IsValid)
            {
                TempData["clientData"] = c;
                return RedirectToAction("MyFormResult");
            }
            return View();
        }

        public ActionResult MyFormResult()
        {
            return View();
        }


        public ActionResult ProductList()
        {
            var data = db.Product.OrderBy(p => p.ProductId).Take(10);
            return View(data);
        }


        public ActionResult BatchUpdate(ProductBatchUpdateViewModel[] items)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var product = db.Product.Find(item.ProductId);
                    product.ProductName = item.ProductName;
                    product.Active = item.Active;
                    product.Price = item.Price;
                    product.Stock = item.Stock;
                }
                db.SaveChanges();
                return RedirectToAction("ProductList");
            }
            return View();
        }


        public ActionResult MyError()
        {
            throw new InvalidOperationException("ERROR");

            return View();
        }

    }
}