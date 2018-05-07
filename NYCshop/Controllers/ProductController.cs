using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace NYCshop.Controllers
{
    public class ProductController : Controller
    {
        private ExLoverShopDb db = new ExLoverShopDb();

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

            model = categoryDetail;

            ViewBag.CategoryID = categoryID;

            int pageSize = 10; // số lượng item trên 1 trang
            int pageNumber = (page ?? 1); // trả về giá trị của page nếu nó là 1 giá trị khác null, nếu không trả về 1
            return View(model.OrderByDescending(m => m.ProductID).ToPagedList(pageNumber, pageSize));
        }
	}
}