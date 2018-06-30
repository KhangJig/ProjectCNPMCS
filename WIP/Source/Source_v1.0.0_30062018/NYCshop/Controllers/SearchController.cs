using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using NYCshop.Resources.ResourceFiles;
using NYCshop.ViewModels.ProductViewModel;
using NYCshop.ViewModels.SearchViewModel;

namespace NYCshop.Controllers
{
    public class SearchController : Controller
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
                case "viewsearch":
                    error.ErrorContent = e.ToString();
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.ViewSearch + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.ViewSearch + "'";
                    if (Session["Username"] != null)
                        error.Username = Session["Username"] as string;
                    break;
                case "searchproduct":
                    error.ErrorContent = e.ToString();
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.SearchProduct + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.SearchProduct + "'";
                    break;
                default: break;
            }

            error.OccurDate = DateTime.Now;
            if (Session["Username"] != null)
                error.Username = Session["Username"] as string;

            db.ErrorLogs.Add(error);
            db.SaveChanges();

            //Log Exception e
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectToRouteResult("Default",
                    new System.Web.Routing.RouteValueDictionary{
                        {"controller", "Error"},
                        {"action", "Index"},
                    });
        }

        // Hiển thị nút tìm kiếm
        // GET: /Search/ViewSearch
        public ActionResult ViewSearch()
        {
            SearchViewModel model = new SearchViewModel();
            model.SearchString = "";
            model.SearchType = "Tên sản phẩm";

            return PartialView("_SearchPartial", model);
        }

        //
        // GET: /Search/SearchProduct
        public ActionResult SearchProduct(SearchViewModel model, int? page)
        {
            var products = SearchWithSearchType(model.SearchString, model.SearchType);

            // chèn danh sách hình ảnh vào từng sản phẩm tương ứng
            foreach(SearchProductViewModel p in products)
            {
                List<string> images = (from i in db.ImageUrls
                                       where i.ProductID == p.ProductID
                                       select i.Url).ToList();
                p.Images = images;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1); // trả về giá trị của page nếu nó là 1 giá trị khác null, nếu không trả về 1

            ViewBag.PagedList = products.OrderByDescending(p => p.ProductID).ToPagedList(pageNumber, pageSize);
            ViewBag.SearchString = model.SearchString;
            ViewBag.SearchType = model.SearchType;
            return View(products);
        }

        //
        // POST: /Search/SearchProduct
        [HttpPost]
        public ActionResult SearchProduct(List<SearchProductViewModel> model, string searchString, string searchType, int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1); // trả về giá trị của page nếu nó là 1 giá trị khác null, nếu không trả về 1

            ViewBag.PagedList = model.OrderByDescending(p => p.ProductID).ToPagedList(pageNumber, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.SearchType = searchType;
            return View(model);
        }

        // tìm các loại sản phẩm cần tìm
        public List<SearchProductViewModel> SearchWithSearchType(string searchString, string searchType)
        {
            List<SearchProductViewModel> lstProduct = new List<SearchProductViewModel>();
            if(searchType != null && searchType != "")
            {
                string[] lstType = searchType.Split(',');

                for (int i = 0; i < lstType.Count(); i++)
                {
                    string type = lstType[i];
                    switch (type)
                    {
                        case "Tên sản phẩm":
                            lstProduct = db.Products.Where(p => p.ProductName.ToLower().Contains(searchString.ToLower()) == true)
                            .AsEnumerable()
                            .Select(x => new SearchProductViewModel
                            {
                                Describe = x.Describe,
                                Images = new List<string>(),
                                Price = x.Price,
                                ProductID = x.ProductID,
                                ProductName = x.ProductName,
                                Quanlity = x.Quanlity,
                                SubCategoryID = x.SubCategoryID
                            }).ToList();
                            break;
                        case "Mô tả":
                            lstProduct = db.Products.Where(p => p.Describe.ToLower().Contains(searchString.ToLower()) == true)
                            .AsEnumerable()
                            .Select(x => new SearchProductViewModel
                            {
                                Describe = x.Describe,
                                Images = new List<string>(),
                                Price = x.Price,
                                ProductID = x.ProductID,
                                ProductName = x.ProductName,
                                Quanlity = x.Quanlity,
                                SubCategoryID = x.SubCategoryID
                            }).ToList();
                            break;
                        case "Người đăng":
                            lstProduct = db.Products.Where(p => p.Username.ToLower().Contains(searchString.ToLower()) == true)
                            .AsEnumerable()
                            .Select(x => new SearchProductViewModel
                            {
                                Describe = x.Describe,
                                Images = new List<string>(),
                                Price = x.Price,
                                ProductID = x.ProductID,
                                ProductName = x.ProductName,
                                Quanlity = x.Quanlity,
                                SubCategoryID = x.SubCategoryID
                            }).ToList();
                            break;
                        default: break;
                    }
                }
            }

            return lstProduct;
        }
	}
}