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
                                 where categoryID == c.CategoryID && sc.SubCategoryID == p.SubCategoryID && sc.CategoryID == c.CategoryID
                                 select new CategoryDetailViewModel 
                                 {
                                    ListImages = imageUrl,
                                    Price = p.Price,
                                    ProductID = p.ProductID,
                                    ProductName = p.ProductName
                                 }).ToList();

            // thiết lập các ảnh của sản phẩm
            foreach(CategoryDetailViewModel product in categoryDetail)
            {
                var images = (from i in db.ImageUrls
                              where i.ProductID == product.ProductID
                              select i.Url).ToList();

                product.ListImages = images;
            }

            model = categoryDetail;

            return PartialView("_CategoryDetailPartial", model);
        }

        //
        // GET: /Home/Upload
        public ActionResult Upload()
        {
            return View();
        }

        //
        // POST: /Home/Upload/
        [HttpPost]
        public ActionResult Upload(List<HttpPostedFileBase> files)
        {
            string path = Server.MapPath("~/Images/TestImages");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (HttpPostedFileBase postedFile in files)
            {
                if (files != null)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(path, fileName));
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }
            }

            return View();
        }
    }
}