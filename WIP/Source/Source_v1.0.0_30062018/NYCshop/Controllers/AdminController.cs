using NYCshop.Assets;
using NYCshop.Attributes;
using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using NYCshop.Resources.ResourceFiles;
using NYCshop.ViewModels.UserViewModel;
using NYCshop.ViewModels.ProductViewModel;

namespace NYCshop.Controllers
{
    [CheckAdmin]
    public class AdminController : Controller
    {
        private ExLoverShopDb db = new ExLoverShopDb();
        private MD5Assets md5 = new MD5Assets();
        private GetListAndDict listAndDict = new GetListAndDict();

        /// <summary>
        /// Xử lý các ngoại lệ xảy ra trong từng action hoặc giao diện (view)
        /// </summary>
        /// <param name="filterContext">Nội dung của ngoại lệ</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;

            //string controllerName = filterContext.RouteData.Values["controller"] as string;
            string actionName = filterContext.RouteData.Values["action"] as string;
            string httpMethod = filterContext.HttpContext.Request.HttpMethod;

            // khởi tạo đối tượng ghi lỗi xảy ra
            ErrorLog error = new ErrorLog();
            error.OccurDate = DateTime.Now;
            if (Session["Username"] != null)
                error.Username = Session["Username"] as string;
            error.ErrorContent = e.ToString();

            // xác định xem lỗi xảy ra ở đâu?
            switch (actionName.ToLower())
            {
                case "login":
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.Login + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.Login + "'";

                    db.ErrorLogs.Add(error);
                    db.SaveChanges();
                    break;
                case "register":
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.Register + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.Register + "'";

                    db.ErrorLogs.Add(error);
                    db.SaveChanges();
                    break;
                case "logoff":
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.LogOff + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.LogOff + "'";

                    db.ErrorLogs.Add(error);
                    db.SaveChanges();
                    break;
                default: break;
            }

            //Log Exception e
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectToRouteResult("Default",
                    new System.Web.Routing.RouteValueDictionary{
                        {"controller", "Error"},
                        {"action", "Index"},
                    });
        }


        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }

        
        /// <summary>
        /// GET: /Admin/ChangePassword
        /// Đổi mật khẩu của admin
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel();
            if (Session["Username"] != null)
                model.Username = Session["Username"].ToString();

            return View(model);
        }

        /// <summary>
        /// POST: /Admin/ChangePassword
        /// Đổi mật khẩu của admin
        /// </summary>
        /// <param name="model">Model chứa thông tin về mật khẩu</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.Username == model.Username);

                if (user != null)
                {
                    // 1.4 Mật khẩu cũ không đúng
                    if (md5.GetMd5Hash(model.OldPassword) != user.Password)
                        ModelState.AddModelError("", "Mật khẩu cũ không đúng");
                    else
                    {
                        string newPassword = md5.GetMd5Hash(model.NewPassword);
                        // 1.1 trường hợp trùng mật khẩu mới và mật khẩu cũ
                        if (newPassword == user.Password)
                            ModelState.AddModelError("", "Mật khẩu mới không được trùng mật khẩu cũ");
                        else
                        {
                            // 2. Đổi mật khẩu thành công
                            user.Password = md5.GetMd5Hash(model.NewPassword);

                            db.SaveChanges();
                            ViewBag.ChangePasswordMsg = "Đổi mật khẩu thành công";
                        }
                    }
                }
                else ModelState.AddModelError("", "Đổi mật khẩu thất bại");
            }

            return View(model);
        }

        //
        // GET: /Admin/ChangePersonalDetail
        public ActionResult ChangePersonalDetail()
        {
            AccountInfoViewModel model = new AccountInfoViewModel();
            if (Session["Username"] != null)
            {
                string username = Session["Username"].ToString();

                var user = db.Users.FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    model.Username = user.Username;
                    model.Address = user.Address;
                    model.Email = user.Email;
                    model.Name = user.Name;
                    model.Phone = user.Phone;
                }
            }

            return View(model);
        }

        //
        // POST: /Admin/ChangePersonalDetail
        [HttpPost]
        public ActionResult ChangePersonalDetail(AccountInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.Username == model.Username);

                if (user != null)
                {
                    user.Username = model.Username;
                    user.Address = model.Address;
                    user.Email = model.Email;
                    user.Name = model.Name;
                    user.Phone = model.Phone;

                    db.SaveChanges();
                    ViewBag.UpdateInfoMsg = "Cập nhật thông tin thành công";
                }
                else ModelState.AddModelError("", "Cập nhật thông tin thất bại");
            }

            return View(model);
        }

        //
        // GET: /Admin/ManagePost/
        public ActionResult ManagePost(int? page, string viewType, string date, string search, string adminAction)
        {
            if (page == null)
            {
                // 2. Hiển thị các bài đăng thành công
                if (Session["Username"] != null)
                {

                    Dictionary<int, string> dictImages = new Dictionary<int, string>();
                    var images = from i in db.ImageUrls
                                 select i;

                    foreach (ImageUrl img in images)
                        if (!dictImages.ContainsKey(img.ProductID))
                            dictImages.Add(img.ProductID, img.Url);

                    var products = (from p in db.Products
                                    where p.Censor == false && p.Viewed == false
                                    select new ProductManagerViewModel
                                    {
                                        ProductID = p.ProductID,
                                        ProductName = p.ProductName,
                                        Price = p.Price,
                                        Quanlity = p.Quanlity,
                                        SaleStatus = p.SaleStatus,
                                        Image = "",
                                        Censor = p.Censor,
                                        Username = p.Username,
                                        UploadDate = p.UploadDate
                                    }).ToList();

                    foreach (ProductManagerViewModel p in products)
                        if (dictImages.ContainsKey(p.ProductID))
                        {
                            p.Image = dictImages[p.ProductID];
                        }

                    int pageSize = 10;
                    int pageNumber = (page ?? 1); // trả về giá trị của page nếu nó là 1 giá trị khác null, nếu không trả về 1

                    ViewBag.PagedList = products.OrderByDescending(p => p.ProductID).ToPagedList(pageNumber, pageSize);
                    return View(products);
                }

                return View();
            }
            else
            {
                // thiết lập cho thuộc tính viewType:
                // true: tin đã duyệt
                // false: tin chưa duyệt
                string censor = "";
                string uploadDate = ""; // thiết lập ngày đăng tin
                string searchKey = ""; // thiết lập từ khóa tìm kiếm theo tên người dùng hoặc tên sản phẩm

                // Summary: xử lý theo từng cách hiển thị:
                //
                viewType = viewType == null ? "" : viewType;
                date = date == null ? "" : date;
                search = search == null ? "" : search;
                switch (viewType)
                {
                    case "Tất cả":
                        censor += "Censor='True' OR Censor='False'";
                        break;
                    case "Tin đã duyệt":
                        censor += "Censor='True'";
                        break;
                    case "Tin chưa duyệt":
                        censor += "Censor='False'";
                        break;
                    default:
                        break;
                }

                // thiết lập ngày cho chuỗi truy vấn
                if (String.Compare("", date) != 0)
                    uploadDate += string.Format("UploadDate='{0}'", date);
                else uploadDate += "UploadDate>'1-1-1'";

                // thiết lập từ khóa tìm kiếm cho chuỗi truy vấn
                if (String.Compare("", search) != 0)
                    searchKey += string.Format("LOWER(ProductName) LIKE N'%{0}%' OR LOWER(Username) LIKE N'%{0}%'", search);
                else searchKey += "ProductName <> '' AND Username <> ''";

                string sql = string.Format("SELECT * FROM Products WHERE {0} AND {1} AND {2}", censor, uploadDate, searchKey);
                var products = db.Products.SqlQuery(sql).ToList();

                // thiết lập danh sách hình ảnh
                Dictionary<int, string> dictImages = new Dictionary<int, string>();
                var images = from i in db.ImageUrls
                             select i;

                foreach (ImageUrl img in images)
                    if (!dictImages.ContainsKey(img.ProductID))
                        dictImages.Add(img.ProductID, img.Url);

                // khởi tạo lại model
                List<ProductManagerViewModel> model = new List<ProductManagerViewModel>();

                foreach (Product product in products)
                {
                    ProductManagerViewModel p = new ProductManagerViewModel();
                    p.Censor = product.Censor;
                    p.Image = dictImages.ContainsKey(product.ProductID) ? dictImages[product.ProductID] : "";
                    p.Price = product.Price;
                    p.ProductID = product.ProductID;
                    p.ProductName = product.ProductName;
                    p.Quanlity = product.Quanlity;
                    p.SaleStatus = product.SaleStatus;
                    p.UploadDate = product.UploadDate;
                    p.Username = product.Username;
                    p.Viewed = product.Viewed;

                    model.Add(p);
                }

                int pageSize = 10;
                int pageNumber = (page ?? 1); // trả về giá trị của page nếu nó là 1 giá trị khác null, nếu không trả về 1

                ViewBag.PagedList = model.OrderByDescending(p => p.ProductID).ToPagedList(pageNumber, pageSize);
                ViewBag.ViewType = viewType;
                ViewBag.UploadDate = date;
                ViewBag.SearchKey = search;
                ViewBag.AdminAction = adminAction;
                return View(model);
            }
        }

        //
        // POST: /Admin/ManagePost/
        [HttpPost]
        public ActionResult ManagePost(List<ProductManagerViewModel> model, int? page, string viewType, string date, string search, string adminAction)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1); // trả về giá trị của page nếu nó là 1 giá trị khác null, nếu không trả về 1

            // thực hiện hành động tương ứng
            adminAction = adminAction == null ? "" : adminAction;
            switch(adminAction)
            {
                case "Duyệt": 
                    foreach(ProductManagerViewModel product in model)
                    {
                        if(product.PerformAction)
                        {
                            var pro = db.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
                            if(pro != null)
                                pro.Censor = product.PerformAction;
                        }
                    }
                    db.SaveChanges();
                    break;
                case "Không duyệt":
                    foreach(ProductManagerViewModel product in model)
                    {
                        if (product.PerformAction)
                        {
                            var pro = db.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
                            if(pro != null)
                                pro.Censor = !product.PerformAction;
                        }
                    }
                    db.SaveChanges();
                    break;
                case "Xóa":
                    if (model != null)
                    {
                        List<Product> lstDeleteProduct = new List<Product>();
                        List<Product> lstSaveDeleteProduct = new List<Product>();
                        for (int i = 0; i < model.Count; i++)
                        {
                            ProductManagerViewModel p = model[i];
                            // báo hết hàng
                            if (p.PerformAction)
                            {
                                var product = db.Products.FirstOrDefault(pro => pro.ProductID == p.ProductID);
                                // tồn tại sản phẩm này
                                if (product != null)
                                {
                                    lstDeleteProduct.Add(product);

                                    Product delProduct = new Product();
                                    delProduct.Censor = product.Censor;
                                    delProduct.Describe = product.Describe;
                                    delProduct.Price = product.Price;
                                    delProduct.ProductID = product.ProductID;
                                    delProduct.ProductName = product.ProductName;
                                    delProduct.Quanlity = product.Quanlity;
                                    delProduct.SaleStatus = product.SaleStatus;
                                    delProduct.SubCategoryID = product.SubCategoryID;
                                    delProduct.UploadDate = product.UploadDate;
                                    delProduct.Username = product.Username;
                                    delProduct.Viewed = product.Viewed;
                                    lstSaveDeleteProduct.Add(delProduct);
                                }
                            }
                        }

                        Session["ListDeleteProduct"] = lstSaveDeleteProduct;
                        // xóa sản phẩm
                        db.Products.RemoveRange(lstDeleteProduct);
                        db.SaveChanges();
                        ViewBag.PerformActionMsg = "Xóa thành công " + lstDeleteProduct.Count + " sản phẩm";
                    }

                    break;
                default: break;
            }

            // thiết lập cho thuộc tính viewType:
            // true: tin đã duyệt
            // false: tin chưa duyệt
            string censor = "";
            string uploadDate = ""; // thiết lập ngày đăng tin
            string searchKey = ""; // thiết lập từ khóa tìm kiếm theo tên người dùng hoặc tên sản phẩm

            // Summary: xử lý theo từng cách hiển thị:
            //
            viewType = viewType == null ? "" : viewType;
            date = date == null ? "" : date;
            search = search == null ? "" : search;
            switch(viewType)
            {
                case "Tất cả":
                    censor += "(Censor='True' OR Censor='False')";
                    break;
                case "Tin đã duyệt":
                    censor += "Censor='True'";
                    break;
                case "Tin chưa duyệt":
                    censor += "Censor='False'";
                    break;
                default: 
                    break;
            }

            // thiết lập ngày cho chuỗi truy vấn
            if (String.Compare("", date) != 0)
                uploadDate += string.Format("UploadDate='{0}'", date);
            else uploadDate += "UploadDate>'1-1-1'";

            // thiết lập từ khóa tìm kiếm cho chuỗi truy vấn
            if (String.Compare("", search) != 0)
                searchKey += string.Format("(LOWER(ProductName) LIKE N'%{0}%' OR LOWER(Username) LIKE N'%{0}%')", search);
            else searchKey += "ProductName <> '' AND Username <> ''";

            string sql = string.Format("SELECT * FROM Products WHERE {0} AND {1} AND {2}", censor, uploadDate, searchKey);
            var products = db.Products.SqlQuery(sql).ToList();

            // thiết lập danh sách hình ảnh
            Dictionary<int, string> dictImages = new Dictionary<int, string>();
            var images = from i in db.ImageUrls
                         select i;

            foreach (ImageUrl img in images)
                if (!dictImages.ContainsKey(img.ProductID))
                    dictImages.Add(img.ProductID, img.Url);

            // khởi tạo lại model
            if (model != null)
                model.Clear();
            else model = new List<ProductManagerViewModel>();

            foreach(Product product in products)
            {
                ProductManagerViewModel p = new ProductManagerViewModel();
                p.Censor = product.Censor;
                p.Image = dictImages.ContainsKey(product.ProductID) ? dictImages[product.ProductID] : "";
                p.Price = product.Price;
                p.ProductID = product.ProductID;
                p.ProductName = product.ProductName;
                p.Quanlity = product.Quanlity;
                p.SaleStatus = product.SaleStatus;
                p.UploadDate = product.UploadDate;
                p.Username = product.Username;
                p.Viewed = product.Viewed;

                model.Add(p);
            }

            ModelState.Clear(); // xóa các dữ liệu cũ

            ViewBag.PagedList = model.OrderByDescending(p => p.ProductID).ToPagedList(pageNumber, pageSize);
            ViewBag.ViewType = viewType;
            ViewBag.UploadDate = date;
            ViewBag.SearchKey = search;
            ViewBag.AdminAction = adminAction;
            return View(model);
        }

        //
        // GET: /Admin/UndoDeleteProduct
        public ActionResult UndoDeleteProduct()
        {
            if (Session["ListDeleteProduct"] != null)
            {
                List<Product> lstDeleteProduct = Session["ListDeleteProduct"] as List<Product>;
                if (lstDeleteProduct.Count > 0)
                {
                    db.Products.AddRange(lstDeleteProduct);
                    db.SaveChanges();
                }

                Session["ListDeleteProduct"] = null;
            }


            return RedirectToAction("MyPost", "User");
        }

        //
        // GET: /Admin/ShowDetailDemo
        public ActionResult ShowDetailDemo(int productID)
        {
            ProductDetailViewModel model = new ProductDetailViewModel();
            var product = db.Products.FirstOrDefault(p => p.ProductID == productID);
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
                if (user != null)
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
            }

            return View(model);
        }
	}
}