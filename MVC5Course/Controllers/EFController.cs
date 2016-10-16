using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

            // 先刪除跟Product的關聯資料
            db.OrderLine.RemoveRange(product.OrderLine);    // OrderLine 是 product 的導覽屬性 ※參考.edmx關聯圖

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
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException deve)    // 找到發生錯誤的真正 Exception ※ 不要用 Exception
            {
                foreach (var entityErrors in deve.EntityValidationErrors)   // 列出錯誤集合中的每個錯誤
                {
                    foreach (var vErrors in entityErrors.ValidationErrors)  // 再列出錯誤集合中的每個錯誤
                    {
                        // 找到發生錯誤的物件並自訂錯誤訊息
                        throw new DbEntityValidationException(vErrors.PropertyName + "發生錯誤" + vErrors.ErrorMessage);
                    }
                }
            }
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