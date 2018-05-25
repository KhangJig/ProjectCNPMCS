using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NYCshop.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Error/PageNotFound
        public ActionResult PageNotFound()
        {
            return View();
        }

        //
        // GET: /Error/AccessDenied
        public ActionResult AccessDenied()
        {
            return View();
        }
	}
}