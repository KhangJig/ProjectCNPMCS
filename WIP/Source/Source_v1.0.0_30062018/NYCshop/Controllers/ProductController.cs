using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using NYCshop.Resources.ResourceFiles;
using NYCshop.Assets;
using NYCshop.ViewModels.CommentViewModel;
using NYCshop.ViewModels.ProductViewModel;
using NYCshop.ViewModels.CategoryViewModel;

namespace NYCshop.Controllers
{
    public class ProductController : Controller
    {
        private ExLoverShopDb db = new ExLoverShopDb();
        private GetListAndDict listAndDict = new GetListAndDict();

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
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.CategoryDetail + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.CategoryDetail + "'";
                    if (Session["Username"] != null)
                        error.Username = Session["Username"] as string;

                    db.ErrorLogs.Add(error);
                    db.SaveChanges();
                    break;
                case "detail":
                    error.ErrorContent = e.ToString();
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.ProductDetail + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.ProductDetail + "'";
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
        // GET: /Product/
        public ActionResult Index(int categoryID, int? page)
        {
            List<CategoryDetailViewModel> model = new List<CategoryDetailViewModel>();

            List<string> imageUrl = new List<string>();
            // lấy danh sách các sản phẩm
            var categoryDetail = (from c in db.Categories
                                  from sc in db.SubCategories
                                  from p in db.Products
                                  where categoryID == c.CategoryID && sc.SubCategoryID == p.SubCategoryID && sc.CategoryID == c.CategoryID
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

            var category = db.Categories.FirstOrDefault(c => c.CategoryID == categoryID);
            if (category != null)
                ViewBag.CategoryName = category.CategoryName;

            model = categoryDetail;

            ViewBag.CategoryID = categoryID;

            int pageSize = 10; // số lượng item trên 1 trang
            int pageNumber = (page ?? 1); // trả về giá trị của page nếu nó là 1 giá trị khác null, nếu không trả về 1
            return View(model.OrderByDescending(m => m.ProductID).ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Product/Detail
        public ActionResult Detail(int productID)
        {

            ProductDetailViewModel model = new ProductDetailViewModel();
            var product = db.Products.FirstOrDefault(p => p.ProductID == productID && p.Censor == true);
            if (product != null)
            {
                var subCategory = db.SubCategories.FirstOrDefault(sc => sc.SubCategoryID == product.SubCategoryID);
                var category = db.Categories.FirstOrDefault(c => c.CategoryID == subCategory.CategoryID);
                var imageUrl = (from i in db.ImageUrls
                                where i.ProductID == productID
                                select i).ToList();
                List<string> lstImages = new List<string>();
                if (imageUrl != null)
                {
                    foreach (ImageUrl img in imageUrl)
                        lstImages.Add(img.Url);
                }

                // lấy tên người dùng và số điện thoại
                var user = db.Users.FirstOrDefault(u => u.Username == product.Username);
                string name = string.Empty;
                string phone = string.Empty;
                if(user != null)
                {
                    name = user.Name;
                    phone = user.Phone;
                }

                model.ProductID = product.ProductID;
                model.Describe = product.Describe;
                model.Price = product.Price;
                model.ProductName = product.ProductName;
                model.SubCategory = subCategory.SubCategoryName;
                model.Category = category.CategoryName;
                model.ListImages = lstImages;
                model.Name = name;
                model.Phone = phone;
                model.CategoryID = category.CategoryID;
                model.Username = product.Username;

                return View(model);
            }
            
            return RedirectToAction("ProductNotFound", "Product");
        }

        /// <summary>
        /// GET: /Product/ProductNotFound
        /// Không tìm thấy sản phẩm
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductNotFound()
        {
            return View();
        }

        // Hiển thị bình luận của sản phẩm
        // GET: /Product/Comment
        public ActionResult Comment(int productID)
        {
            Dictionary<int, CommentInProductViewModel> dictComments = listAndDict.GetCommentsByProductID(productID);
            ViewData["ProductID"] = productID;

            return PartialView("_CommentViewPartial", dictComments);
        }

        // Hiển thị các sản phẩm liên quan
        // GET: /Product/GetRecommendProducts
        public ActionResult GetRecommendProducts(int productID, int categoryID)
        {
            List<CategoryDetailViewModel> model = new List<CategoryDetailViewModel>();

            List<string> imageUrl = new List<string>();
            // lấy danh sách các sản phẩm
            var categoryDetail = (from c in db.Categories
                                  from sc in db.SubCategories
                                  from p in db.Products
                                  where categoryID == c.CategoryID && sc.SubCategoryID == p.SubCategoryID && sc.CategoryID == c.CategoryID
                                  select new CategoryDetailViewModel
                                  {
                                      ListImages = imageUrl,
                                      Price = p.Price,
                                      ProductID = p.ProductID,
                                      ProductName = p.ProductName
                                  }).Take(5).Where(cd => cd.ProductID != productID).ToList();

            // thiết lập các ảnh của sản phẩm
            foreach (CategoryDetailViewModel product in categoryDetail)
            {
                var images = (from i in db.ImageUrls
                              where i.ProductID == product.ProductID
                              select i.Url).ToList();

                product.ListImages = images;
            }

            var category = db.Categories.FirstOrDefault(c => c.CategoryID == categoryID);
            if (category != null)
                ViewBag.CategoryName = category.CategoryName;

            model = categoryDetail;

            ViewBag.CategoryID = categoryID;

            return PartialView("_RecommendProductsPartial", model);
        }
	}
}