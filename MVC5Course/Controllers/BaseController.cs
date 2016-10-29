using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public abstract class BaseController : Controller
    {
        protected FabricsEntities db = new FabricsEntities();

        // GET: Base
        //public ActionResult Index()
        //{
        //    return View();
        //}

        protected override void HandleUnknownAction(string actionName)
        {
            //base.HandleUnknownAction(actionName);
            this.RedirectToAction("Index").ExecuteResult(this.ControllerContext);
        }

    }
}