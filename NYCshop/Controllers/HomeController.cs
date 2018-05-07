using NYCshop.Metadata;
using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NYCshop.Controllers
{
    public class HomeController : Controller
    {
        private ExLoverShopDb db = new ExLoverShopDb();

        // GET: Home
        public ActionResult Index()
        {
            var model = from c in db.Categories
                        select c;

            return View(model.ToList());
        }
    }
}