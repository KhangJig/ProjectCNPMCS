using NYCshop.ViewModels.ErrorViewModels;
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

        /// <summary>
        /// Chuyển sang trang lỗi
        /// </summary>
        /// <param name="model">Thông tin về lỗi</param>
        /// <returns></returns>
        public ActionResult SharedError()
        {
            ErrorViewModel model = new ErrorViewModel();
            if (TempData.ContainsKey("Error"))
            {
                // gán lỗi vào biến model
                model = TempData["Error"] as ErrorViewModel;
            }

            return View(model);
        }
	}
}