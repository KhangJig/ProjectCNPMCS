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

namespace NYCshop.Controllers
{
    [CheckUser]
    public class UserController : Controller
    {
        private ExLoverShopDb db = new ExLoverShopDb();
        private MD5Assets md5 = new MD5Assets();
        private GetListAndDict listAndDict = new GetListAndDict();

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
            if(Session["Username"] != null)
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
        public ActionResult MyPost()
        {


            return View();
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
            if(Session["Username"] != null)
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
                        for (int i = 0; i < files.Count; i++ )
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
	}
}