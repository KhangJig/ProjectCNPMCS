using NYCshop.Models;
using NYCshop.Resources.ResourceFiles;
using NYCshop.ViewModels.CategoryViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NYCshop.Controllers
{
    public class LayoutController : Controller
    {
        private ExLoverShopDb db = new ExLoverShopDb();

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;

            //string controllerName = filterContext.RouteData.Values["controller"] as string;
            string actionName = filterContext.RouteData.Values["action"] as string;
            string httpMethod = filterContext.HttpContext.Request.HttpMethod;
            ErrorLog error = new ErrorLog();
            switch (actionName.ToLower())
            {
                case "topmenu":
                    error.ErrorContent = e.ToString();
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.TopMenu + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.TopMenu + "'";
                    if (Session["Username"] != null)
                        error.Username = Session["Username"] as string;

                    db.ErrorLogs.Add(error);
                    db.SaveChanges();
                    break;
                default: break;
            }

            error.OccurDate = DateTime.Now;
            if (Session["Username"] != null)
                error.Username = Session["Username"] as string;

            //Log Exception e
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectToRouteResult("Default",
                    new System.Web.Routing.RouteValueDictionary{
                        {"controller", "Error"},
                        {"action", "Index"},
                    });
        }

        //
        // GET: /Layout/TopMenu
        public ActionResult TopMenu()
        {
            List<CateWithSubViewModel> model = new List<CateWithSubViewModel>();
            var categories = (from c in db.Categories
                              select c).ToList();

            foreach (Category cat in categories)
            {
                var subCategories = (from sc in db.SubCategories
                                     where sc.CategoryID == cat.CategoryID
                                     select sc).ToList();

                CateWithSubViewModel item = new CateWithSubViewModel();
                item.CategoryID = cat.CategoryID;
                item.CategoryName = cat.CategoryName;
                item.ListSubCats = subCategories;

                model.Add(item);
            }

            return PartialView("_MenuPartial", model);
        }
	}
}