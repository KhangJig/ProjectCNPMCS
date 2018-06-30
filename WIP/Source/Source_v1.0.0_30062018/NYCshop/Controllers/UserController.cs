using NYCshop.Assets;
using NYCshop.Attributes;
using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using NYCshop.Resources.ResourceFiles;

namespace NYCshop.Controllers
{
    [CheckUser]
    public class UserController : Controller
    {
        private ExLoverShopDb db = new ExLoverShopDb();
        private MD5Assets md5 = new MD5Assets();
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
                case "newpost":
                    error.ErrorContent = e.ToString();
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.NewPost + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.NewPost + "'";
                    if (Session["Username"] != null)
                        error.Username = Session["Username"] as string;
                    break;
                case "index":
                    error.ErrorContent = e.ToString();
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.UserInfo + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.UserInfo + "'";
                    break;
                case "changepassword":
                    error.ErrorContent = e.ToString();
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.ChangePassword + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.ChangePassword + "'";
                    break;
                case "changepersonaldetail":
                    error.ErrorContent = e.ToString();
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.ChangePassword + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.ChangePassword + "'";
                    break;
                case "mypost": 
                    error.ErrorContent = e.ToString();
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.MyPost + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.MyPost + "'";
                    break;
                case "editproduct": 
                    error.ErrorContent = e.ToString();
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.EditProduct + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.EditProduct + "'";

                    break;
                case "comment":
                    error.ErrorContent = e.ToString();
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.Comment + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.Comment + "'";

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

        // trang thông tin tài khoản được ưu tiên hàng đầu
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /User/ChangePassword
        public ActionResult ChangePassword()
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel();
            if (Session["Username"] != null)
                model.Username = Session["Username"].ToString();

            return View(model);
        }

        //
        // POST: /User/ChangePassword
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
        // GET: /User/ChangePersonalDetail
        public ActionResult ChangePersonalDetail()
        {
            UserInfoViewModel model = new UserInfoViewModel();
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
        // POST: /User/ChangePersonalDetail
        [HttpPost]
        public ActionResult ChangePersonalDetail(UserInfoViewModel model)
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
        // GET: /User/MyPost/
        public ActionResult MyPost(int? page)
        {
            // 2. Hiển thị tất cả tin của tôi thành công
            if (Session["Username"] != null)
            {
                string username = Session["Username"].ToString();

                Dictionary<int, string> dictImages = new Dictionary<int, string>();
                var images = from i in db.ImageUrls
                             select i;

                foreach (ImageUrl img in images)
                    if (!dictImages.ContainsKey(img.ProductID))
                        dictImages.Add(img.ProductID, img.Url);

                var products = (from p in db.Products
                                where p.Username == username
                                select new ProductManagerViewModel
                                {
                                    ProductID = p.ProductID,
                                    ProductName = p.ProductName,
                                    Price = p.Price,
                                    Quanlity = p.Quanlity,
                                    SaleStatus = p.SaleStatus,
                                    DeleteProduct = false,
                                    Image = ""
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

        //
        // POST: /User/MyPost/
        [HttpPost]
        public ActionResult MyPost(List<ProductManagerViewModel> model, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1); // trả về giá trị của page nếu nó là 1 giá trị khác null, nếu không trả về 1

            var deleteProduct = model.FirstOrDefault(m => m.DeleteProduct == true);
            if(deleteProduct != null) // có sản phẩm bị xóa
            {
                if (Session["Username"] != null)
                {
                    string username = Session["Username"].ToString();

                    // xóa sản phẩm được chọn
                    #region xóa sản phẩm được chọn
                    List<ProductManagerViewModel> delProducts = model.FindAll(m => m.DeleteProduct == true);
                    foreach(ProductManagerViewModel delProduct in delProducts)
                    {
                        var currDelProduct = db.Products.FirstOrDefault(p => p.ProductID == delProduct.ProductID);
                        if (currDelProduct != null)
                        {
                            // xóa sản phẩm
                            db.Products.Remove(currDelProduct);

                            // xóa đường dẫn hình ảnh của sản phẩm đó trong CSDL
                            var imageUrls = (from i in db.ImageUrls
                                            where i.ProductID == delProduct.ProductID
                                            select i).ToList();

                            foreach(ImageUrl img in imageUrls)
                                db.ImageUrls.Remove(img);

                            // xóa các hình ảnh trên server hiện tại
                            foreach (ImageUrl image in imageUrls)
                            {
                                string url = Request.MapPath("~" + image.Url);
                                if (System.IO.File.Exists(url))
                                    System.IO.File.Delete(url);
                            }
                        }   
                    }
                    #endregion

                    // cập nhật sản phẩm đã hết hàng
                    #region cập nhật sản phẩm đã hết hàng
                    #endregion

                    db.SaveChanges();

                    // 2. Hiển thị tất cả tin của tôi thành công
                    #region 2. Hiển thị tất cả tin của tôi thành công

                    Dictionary<int, string> dictImages = new Dictionary<int, string>();
                    var images = from i in db.ImageUrls
                                 select i;

                    foreach (ImageUrl img in images)
                        if (!dictImages.ContainsKey(img.ProductID))
                            dictImages.Add(img.ProductID, img.Url);

                    var products = (from p in db.Products
                                    where p.Username == username
                                    select new ProductManagerViewModel
                                    {
                                        ProductID = p.ProductID,
                                        ProductName = p.ProductName,
                                        Price = p.Price,
                                        Quanlity = p.Quanlity,
                                        SaleStatus = p.SaleStatus,
                                        DeleteProduct = false,
                                        Image = ""
                                    }).ToList();

                    foreach (ProductManagerViewModel p in products)
                        if (dictImages.ContainsKey(p.ProductID))
                            p.Image = dictImages[p.ProductID];

                    ViewBag.PagedList = products.OrderByDescending(p => p.ProductID).ToPagedList(pageNumber, pageSize);
                    ModelState.Clear(); // xóa sạch các dữ liệu cũ của model, tránh trường hợp không làm mới được dữ liệu

                    ViewBag.DeleteProductMsg = "Đã xóa " + delProducts.Count + " sản phẩm";
                    return View(products);
                    #endregion
                }
            }

            ViewBag.PagedList = model.OrderByDescending(p => p.ProductID).ToPagedList(pageNumber, pageSize);
            return View(model);
        }

        //
        // GET: /User/NewPost/
        public ActionResult NewPost()
        {
            List<SelectListItem> categories = listAndDict.GetListCategories();
            categories[0].Selected = true;

            ViewBag.Categories = categories;
            ViewBag.DictCategories = listAndDict.GetDictCategories();

            return View();
        }

        //
        // POST: /User/NewPost/
        [HttpPost]
        public ActionResult NewPost(Product model, List<HttpPostedFileBase> files)
        {
            if (Session["Username"] != null)
            {
                string username = Session["Username"] as string;

                // thêm danh sách các loại sp để hiển thị lại
                List<SelectListItem> categories = listAndDict.GetListCategories();
                categories[0].Selected = true;

                ViewBag.Categories = categories;
                ViewBag.DictCategories = listAndDict.GetDictCategories();

                if (ModelState.IsValid)
                {
                    if (files != null && files[0] != null)
                    {
                        // lấy productID lớn nhất
                        var productMaxID = (from p in db.Products
                                            select p).OrderByDescending(a => a.ProductID).FirstOrDefault().ProductID;

                        // thêm sp vào CSDL
                        model.SaleStatus = false;
                        model.Username = username;
                        model.ProductID = productMaxID + 1;

                        db.Products.Add(model);

                        // copy hình ảnh vào thư mục /Images/Products và đổi tên thích hợp
                        #region copy hình ảnh vào thư mục /Images/Products và đổi tên thích hợp
                        string path = Server.MapPath("~/Images/Products");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        // lấy id lớn nhất trong ImagesUrl
                        int imageMaxID = (from i in db.ImageUrls
                                          select i).OrderByDescending(a => a.ImageID).FirstOrDefault().ImageID;

                        int currentMaxID = imageMaxID + 1;

                        List<ImageUrl> images = new List<ImageUrl>();
                        for (int i = 0; i < files.Count; i++)
                        {
                            HttpPostedFileBase postedFile = files[i];
                            if (files != null)
                            {
                                string extension = Path.GetExtension(postedFile.FileName);
                                string fileName = model.ProductID + "_" + (i + 1).ToString() + extension;
                                postedFile.SaveAs(Path.Combine(path, fileName));

                                // thêm đường dẫn hình ảnh vào CSDL
                                ImageUrl imageUrl = new ImageUrl();
                                imageUrl.ImageID = currentMaxID;
                                imageUrl.ProductID = model.ProductID;
                                imageUrl.Url = "/Images/Products/" + fileName;
                                images.Add(imageUrl);

                                currentMaxID++;
                            }
                        }
                        #endregion

                        db.ImageUrls.AddRange(images);
                        db.SaveChanges();
                        ViewBag.AddProductMsg = "Thêm sản phẩm thành công";
                    }
                    else ModelState.AddModelError("", "Bạn phải chọn ảnh cho sản phẩm");
                }
            }

            return View();
        }

        //
        // GET: /User/EditProduct/{ProductID}
        public ActionResult EditProduct(int productID)
        {
            string username = string.Empty;
            if (Session["Username"] != null)
                username = Session["Username"] as string;

            var product = db.Products.FirstOrDefault(p => p.ProductID == productID && p.Username == username);
            int categoryID = 3;
            EditProductViewModel currProduct = new EditProductViewModel();

            if (product != null) // người dùng hiện tại là người đăng sản phẩm này
            {
                currProduct.ProductID = product.ProductID;
                currProduct.ProductName = product.ProductName;
                currProduct.Quanlity = product.Quanlity;
                currProduct.Price = product.Price;
                currProduct.Describe = product.Describe;
                currProduct.SaleStatus = product.SaleStatus;
                currProduct.SubCategoryID = product.SubCategoryID;
                currProduct.Update = false;

                // thiết lập lại categoryID
                var subCategory = db.SubCategories.FirstOrDefault(sc => sc.SubCategoryID == currProduct.SubCategoryID);
                if (subCategory != null)
                {
                    categoryID = subCategory.CategoryID;

                    // thiết lập loại sản phẩm và loại sản phẩm con đang được chọn
                    List<SelectListItem> categories = listAndDict.GetListCategories(categoryID);
                    ViewBag.Categories = categories;
                    ViewBag.DictCategories = listAndDict.GetDictCategories(subCategory.SubCategoryID);
                    ViewBag.CategoryID = categoryID;

                    // thiết lập các hình ảnh của sản phẩm
                    var images = (from i in db.ImageUrls
                                  where i.ProductID == productID
                                  select i);
                    List<string> imageUrls = new List<string>();
                    foreach (ImageUrl image in images)
                        imageUrls.Add(image.Url);
                    ViewBag.ListImages = imageUrls; // thêm danh sách hình ảnh vào ViewBag
                }

                return View(currProduct);
            }

            return RedirectToAction("AccessDenied", "Error");
        }

        //
        // POST: /User/EditProduct/{ProductID}
        [HttpPost]
        public ActionResult EditProduct(EditProductViewModel model, List<HttpPostedFileBase> files, int productID)
        {
            if (Session["Username"] != null)
            {
                string username = Session["Username"] as string;
                var product = db.Products.FirstOrDefault(p => p.ProductID == productID);
                int currentSubCategory = model.SubCategoryID;

                if (ModelState.IsValid)
                {
                    if ((files != null && files[0] != null) || !model.Update)
                    {
                        // cập nhật thông tin sản phẩm
                        if (product != null)
                        {
                            product.ProductName = model.ProductName;
                            product.Price = model.Price;
                            product.Describe = model.Describe;
                            product.Quanlity = model.Quanlity;
                            product.SubCategoryID = model.SubCategoryID;
                        }

                        if (files != null && files[0] != null)
                        {
                            // xóa các hình ảnh hiện tại
                            #region xóa các hình ảnh hiện tại
                            var imageUrls = (from i in db.ImageUrls
                                             where i.ProductID == productID
                                             select i).AsEnumerable();

                            foreach (ImageUrl image in imageUrls)
                            {
                                string url = Request.MapPath("~" + image.Url);
                                if (System.IO.File.Exists(url))
                                    System.IO.File.Delete(url);
                            }

                            // xóa các đường dẫn hình ảnh hiện tại
                            db.ImageUrls.RemoveRange(imageUrls);
                            #endregion

                            // copy hình ảnh vào thư mục /Images/Products và đổi tên thích hợp
                            #region copy hình ảnh vào thư mục /Images/Products và đổi tên thích hợp
                            string path = Server.MapPath("~/Images/Products");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            // lấy id lớn nhất trong ImagesUrl
                            int imageMaxID = (from i in db.ImageUrls
                                              select i).OrderByDescending(a => a.ImageID).FirstOrDefault().ImageID - imageUrls.Count();

                            int currentMaxID = imageMaxID + 1;

                            List<ImageUrl> images = new List<ImageUrl>();
                            for (int i = 0; i < files.Count; i++)
                            {
                                HttpPostedFileBase postedFile = files[i];
                                if (files != null)
                                {
                                    string extension = Path.GetExtension(postedFile.FileName);
                                    string fileName = model.ProductID + "_" + (i + 1).ToString() + extension;
                                    postedFile.SaveAs(Path.Combine(path, fileName));

                                    // thêm đường dẫn hình ảnh vào CSDL
                                    ImageUrl imageUrl = new ImageUrl();
                                    imageUrl.ImageID = currentMaxID;
                                    imageUrl.ProductID = model.ProductID;
                                    imageUrl.Url = "/Images/Products/" + fileName;
                                    images.Add(imageUrl);

                                    currentMaxID++;
                                }
                            }
                            #endregion

                            db.ImageUrls.AddRange(images);
                        }

                        db.SaveChanges(); // lưu mọi thay đổi trong CSDL
                        ViewBag.EditProductMsg = "Cập nhật sản phẩm thành công";
                    }
                    else
                    {
                        // khôi phục lại dữ liệu chưa được chỉnh sửa
                        model.ProductName = product.ProductName;
                        model.Quanlity = product.Quanlity;
                        model.SaleStatus = product.SaleStatus;
                        model.SubCategoryID = product.SubCategoryID;
                        model.Price = product.Price;
                        model.Describe = product.Describe;

                        currentSubCategory = product.SubCategoryID;

                        ModelState.AddModelError("", "Bạn phải chọn ảnh cho sản phẩm");
                    }
                }

                // thiết lập loại sp, loại sp con để hiển thị
                #region thiết lập loại sp, loại sp con để hiển thị
                int categoryID = 3;

                if (product != null)
                {
                    // thiết lập lại categoryID
                    var subCategory = db.SubCategories.FirstOrDefault(sc => sc.SubCategoryID == currentSubCategory);
                    if (subCategory != null)
                    {
                        categoryID = subCategory.CategoryID;

                        // thiết lập loại sản phẩm và loại sản phẩm con đang được chọn
                        List<SelectListItem> categories = listAndDict.GetListCategories(categoryID);
                        ViewBag.Categories = categories;
                        ViewBag.DictCategories = listAndDict.GetDictCategories(subCategory.SubCategoryID);
                        ViewBag.CategoryID = categoryID;

                        // thiết lập các hình ảnh của sản phẩm
                        var images = (from i in db.ImageUrls
                                      where i.ProductID == productID
                                      select i);
                        List<string> imageUrls = new List<string>();
                        foreach (ImageUrl image in images)
                            imageUrls.Add(image.Url);
                        ViewBag.ListImages = imageUrls; // thêm danh sách hình ảnh vào ViewBag
                    }
                }
                #endregion
            }

            return View(model);
        }

        // kiểm tra xem giá sản phẩm có >= 1000 vnđ không?
        public JsonResult CheckPrice(long Price)
        {
            return Json(Price >= 1000, JsonRequestBehavior.AllowGet);
        }

        // kiểm tra xem số lượng sản phẩm có >= 1 không?
        public JsonResult CheckQuanlity(int Quanlity)
        {
            return Json(Quanlity > 0, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /User/Comment
        [HttpPost]
        public ActionResult Comment(Comment model, string returnUrl, int productID)
        {
            if (ModelState.IsValid)
            {
                model.ProductID = productID;
                model.TimeComment = DateTime.Now; // thêm thời gian bình luận

                db.Comments.Add(model);
                db.SaveChanges();
            }
            else ModelState.AddModelError("", "Bình luận thất bại");

            return Redirect(returnUrl);
        }

        //
        // POST: /User/ReplyComment
        [HttpPost]
        public ActionResult ReplyComment(Reply model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                model.TimeReply = DateTime.Now; // thêm thời gian bình luận

                db.Replies.Add(model);
                db.SaveChanges();
            }
            else ModelState.AddModelError("", "Phản hồi bình luận thất bại");

            return Redirect(returnUrl);
        }
    }
}