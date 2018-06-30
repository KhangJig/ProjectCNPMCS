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
using NYCshop.ViewModels.UserViewModel;
using NYCshop.ViewModels.ProductViewModel;
using NYCshop.DataAccess;
using NYCshop.CustomTypes;
using NYCshop.Resources.ActionMessageInController;

namespace NYCshop.Controllers
{
    [CheckUser]
    public class UserController : Controller
    {
        private ExLoverShopDb db = new ExLoverShopDb();
        private MD5Assets md5 = new MD5Assets();
        private GetListAndDict listAndDict = new GetListAndDict();
        private AccountDAO accountDAO = new AccountDAO();
        private UserDAO dao = new UserDAO();

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;

            //string controllerName = filterContext.RouteData.Values["controller"] as string;
            string actionName = filterContext.RouteData.Values["action"] as string;
            string httpMethod = filterContext.HttpContext.Request.HttpMethod;

            // ghi thông tin lỗi vào csdl
            ErrorLog error = new ErrorLog();
            error.OccurDate = DateTime.Now;
            if (Session["Username"] != null)
                error.Username = Session["Username"] as string;
            error.ErrorContent = e.ToString();

            switch (actionName.ToLower())
            {
                case "newpost":
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.NewPost + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.NewPost + "'";
                    break;
                case "index":
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.UserInfo + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.UserInfo + "'";
                    break;
                case "changepassword":
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.ChangePassword + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.ChangePassword + "'";
                    break;
                case "changepersonaldetail":
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.ChangePassword + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.ChangePassword + "'";
                    break;
                case "mypost":
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.MyPost + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.MyPost + "'";
                    break;
                case "editproduct": 
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.EditProduct + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.EditProduct + "'";

                    break;
                case "comment":
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.Comment + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.Comment + "'";

                    break;
                default: break;
            }

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

        /// <summary>
        /// GET: /User/ChangePassword
        /// Hiển thị giao diện Đổi mật khẩu người dùng
        /// </summary>
        /// <returns></returns>
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
                SuccessAndMsg changePassResult = accountDAO.ChangePassword(model);

                if(changePassResult.IsSuccess)
                {
                    // đổi mật khẩu thành công
                    ViewBag.ChangePasswordMsg = changePassResult.Message;
                    ModelState.Clear();
                }
                else
                {
                    // đổi mật khẩu thất bại
                    ModelState.AddModelError("", changePassResult.Message);
                }
            }
            else
            {
                // đổi mật khẩu thất bại
                ModelState.AddModelError("", ActionMessage.MissingOrInvalidInfo);
            }

            return View();
        }

        /// <summary>
        /// GET: /User/ChangePersonalDetail
        /// Hiển thị thông tin người dùng
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePersonalDetail()
        {
            UserInfoViewModel model = new UserInfoViewModel();
            if (Session["Username"] != null)
            {
                string username = Session["Username"].ToString();

                var user = db.Users.FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    // thiết lập thông tin cho model
                    model = new UserInfoViewModel(user);
                }
            }

            return View(model);
        }

        /// <summary>
        /// POST: /User/ChangePersonalDetail
        /// Đổi thông tin tài khoản
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePersonalDetail(UserInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                SuccessAndMsg changeUserInfoResult = accountDAO.ChangePersonalDetail(model);

                if (changeUserInfoResult.IsSuccess)
                {
                    // đổi thông tin tài khoản thành công
                    ViewBag.UpdateInfoMsg = changeUserInfoResult.Message;
                }
                else
                {
                    // đổi thông tin tài khoản thất bại
                    ModelState.AddModelError("", changeUserInfoResult.Message);
                }
            }
            else
            {
                // nhập thiếu thông tin hoặc thông tin không hợp lệ
                ModelState.AddModelError("", ActionMessage.MissingOrInvalidInfo);
            }

            return View(model);
        }

        //
        // GET: /User/MyPost/
        public ActionResult MyPost(int? page, string viewType, string search, string userAction)
        {
            if (Session["Username"] != null)
            {
                string username = Session["Username"] as string;

                if (page == null)
                {
                    // 2. Hiển thị các bài đăng thành công
                    int pageSize = 10;
                    int pageNumber = (page ?? 1); // trả về giá trị của page nếu nó là 1 giá trị khác null, nếu không trả về 1

                    // lấy ra các bài viết theo người dùng hiện tại
                    List<ProductManagerViewModel> products = dao.GetMyPost(username);

                    ViewBag.PagedList = products.ToPagedList(pageNumber, pageSize);
                    ViewBag.TotalPages = products.Count % 10 == 0 ? products.Count / 10 : products.Count / 10 + 1;
                    return View(products.OrderByDescending(p => p.ProductID).Skip((pageNumber - 1) * 10).Take(10).ToList());
                }
                else
                {
                    // thiết lập lại các giá trị viewType, search để tránh gặp lỗi
                    viewType = viewType == null ? "" : viewType;
                    search = search == null ? "" : search;

                    // Trả về danh sách các sản phẩm theo điều kiện lọc
                    List<ProductManagerViewModel> model = dao.GetMyPost(username, viewType, search);

                    int pageSize = 10;
                    int pageNumber = (page ?? 1); // trả về giá trị của page nếu nó là 1 giá trị khác null, nếu không trả về 1

                    ViewBag.PagedList = model.ToPagedList(pageNumber, pageSize);
                    ViewBag.TotalPages = model.Count % 10 == 0 ? model.Count / 10 : model.Count / 10 + 1;
                    ViewBag.ViewType = viewType;
                    ViewBag.SearchKey = search;
                    ViewBag.UserAction = userAction;
                    return View(model.OrderByDescending(p => p.ProductID).Skip((pageNumber - 1) * 10).Take(10).ToList());
                }
            }

            return View();
        }

        //
        // POST: /User/MyPost/
        [HttpPost]
        public ActionResult MyPost(List<ProductManagerViewModel> model, int? page, string viewType, string search, string userAction)
        {
            if (Session["Username"] != null)
            {
                string username = Session["Username"] != null ? Session["Username"] as string: "";

                // thực hiện hành động của người dùng
                userAction = userAction == null ? "" : userAction;
                switch (userAction)
                {
                    case "Báo hết hàng":
                        dao.SetOutOfProducts(model);
                        break;
                    case "Báo còn hàng":
                        dao.SetRemainProducts(model);
                        break;
                    case "Xóa":
                        DeleteProductsResultType delResult = dao.PerformActionDelete(model);

                        if (delResult.IsSuccess)
                        {
                            // xóa sản phẩm thành công
                            Session["ListDeleteProduct"] = delResult.ListDelProducts;
                            ViewBag.PerformActionMsg = delResult.Message;
                        }
                        else
                        {
                            // xóa sản phẩm thất bại
                            ModelState.AddModelError("", delResult.Message);
                        }
                        break;
                    default: // không làm gì cả
                        break;
                }

                // xử lý theo từng cách hiển thị:
                viewType = viewType == null ? "" : viewType;
                search = search == null ? "" : search;

                // Trả về danh sách các sản phẩm theo điều kiện lọc
                model = dao.GetMyPost(username, viewType, search);

                ModelState.Clear(); // xóa các dữ liệu cũ

                int pageSize = 10;
                int pageNumber = (page ?? 1); // trả về giá trị của page nếu nó là 1 giá trị khác null, nếu không trả về 1

                ViewBag.PagedList = model.ToPagedList(pageNumber, pageSize);
                ViewBag.TotalPages = model.Count % 10 == 0 ? model.Count / 10 : model.Count / 10 + 1;
                ViewBag.ViewType = viewType;
                ViewBag.SearchKey = search;
                ViewBag.UserAction = userAction;
                return View(model.OrderByDescending(p => p.ProductID).Skip((pageNumber - 1) * 10).Take(10).ToList());
            }

            return View(model);
        }

        //
        // GET: /User/UndoDeleteProduct
        public ActionResult UndoDeleteProduct()
        {
            if(Session["ListDeleteProduct"] != null)
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
                    // Thêm sản phẩm mới
                    string serverPath = Server.MapPath("~/Images/Products");
                    SuccessAndMsg addProductResult = dao.AddProductToDb(username, model, files, serverPath);
                    if (addProductResult.IsSuccess)
                    {
                        // thêm sản phẩm mới thành công
                        ViewBag.AddProductMsg = addProductResult.Message;
                    }
                    else
                    {
                        // thêm sản phẩm mới thất bại
                        ModelState.AddModelError("", addProductResult.Message);
                    }
                }
                else ModelState.AddModelError("", AddNewProductResult.AddProductFailed);
            }
            else ModelState.AddModelError("", AddNewProductResult.AddProductFailed);

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
                DateTime vietnamTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
                model.ProductID = productID;
                model.TimeComment = vietnamTime; // thêm thời gian bình luận

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
                DateTime vietnamTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);

                model.TimeReply = vietnamTime; // thêm thời gian bình luận

                db.Replies.Add(model);
                db.SaveChanges();
            }
            else ModelState.AddModelError("", "Phản hồi bình luận thất bại");

            return Redirect(returnUrl);
        }
    }
}