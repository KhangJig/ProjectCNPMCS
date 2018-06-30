using NYCshop.Metadata;
using NYCshop.Models;
using NYCshop.Resources.ResourceFiles;
using NYCshop.ViewModels.CategoryViewModel;
using NYCshop.ViewModels.ProductViewModel;
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

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;

            //string controllerName = filterContext.RouteData.Values["controller"] as string;
            string actionName = filterContext.RouteData.Values["action"] as string;
            string httpMethod = filterContext.HttpContext.Request.HttpMethod;
            ErrorLog error = new ErrorLog();
            switch (actionName.ToLower())
            {
                case "index":
                    error.ErrorContent = e.ToString();
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.Home + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.Home + "'";
                    if (Session["Username"] != null)
                        error.Username = Session["Username"] as string;

                    db.ErrorLogs.Add(error);
                    db.SaveChanges();
                    break;
                case "categorydetail":
                    error.ErrorContent = e.ToString();
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.CategoryDetail + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Trang chủ 'Chức năng " + FunctionNameDisplay.CategoryDetail + "'";

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

        // GET: Home
        public ActionResult Index()
        {
            var model = from c in db.Categories
                        select c;

            return View(model.ToList());
        }

        // trả về chi tiết của loại cần yêu cầu
        // GET: 
        public ActionResult CategoryDetail(int categoryID)
        {
            List<CategoryDetailViewModel> model = new List<CategoryDetailViewModel>();

            List<string> imageUrl = new List<string>();
            // lấy danh sách các sản phẩm
            var categoryDetail = (from c in db.Categories
                                  from sc in db.SubCategories
                                  from p in db.Products
                                  where p.Censor == true && categoryID == c.CategoryID && sc.SubCategoryID == p.SubCategoryID && sc.CategoryID == c.CategoryID && p.SaleStatus == false
                                  select new CategoryDetailViewModel
                                  {
                                      ListImages = imageUrl,
                                      Price = p.Price,
                                      ProductID = p.ProductID,
                                      ProductName = p.ProductName
                                  }).ToList();

            // thiết lập các ảnh của sản phẩm
            foreach (CategoryDetailViewModel product in categoryDetail)
            {
                var images = (from i in db.ImageUrls
                              where i.ProductID == product.ProductID
                              select i.Url).ToList();

                product.ListImages = images;
            }

            model = categoryDetail;

            return PartialView("_CategoryDetailPartial", model);
        }
    }
}