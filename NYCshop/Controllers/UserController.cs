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
<<<<<<< HEAD
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
=======
        // GET: /User/ChangePassword
        public ActionResult ChangePassword()
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel();
            if(Session["Username"] != null)
                model.Username = Session["Username"].ToString();
>>>>>>> 72f387bfd0c11282e09e50c8fe76e2517811a9d1

            return View(model);
        }

        //
<<<<<<< HEAD
        // POST: /User/ChangePersonalDetail
        [HttpPost]
        public ActionResult ChangePersonalDetail(UserInfoViewModel model)
=======
        // POST: /User/ChangePassword
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
>>>>>>> 72f387bfd0c11282e09e50c8fe76e2517811a9d1
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.Username == model.Username);

                if (user != null)
                {
<<<<<<< HEAD
                    user.Username = model.Username;
                    user.Address = model.Address;
                    user.Email = model.Email;
                    user.Name = model.Name;
                    user.Phone = model.Phone;

                    db.SaveChanges();
                    ViewBag.UpdateInfoMsg = "Cập nhật thông tin thành công";
                }
                else ModelState.AddModelError("", "Cập nhật thông tin thất bại");
=======
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
>>>>>>> 72f387bfd0c11282e09e50c8fe76e2517811a9d1
            }

            return View(model);
        }
<<<<<<< HEAD
=======
	}
>>>>>>> 72f387bfd0c11282e09e50c8fe76e2517811a9d1
}