using NYCshop.Assets;
using NYCshop.Attributes;
using NYCshop.Metadata;
using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NYCshop.Controllers
{
    public class AccountController : Controller
    {
        private ExLoverShopDb db = new ExLoverShopDb();
        private MD5Assets md5 = new MD5Assets();

        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl, string login, string register)
        {
            if (register != null)
                return RedirectToAction("Register");

            if (login != null)
            {
                if (ModelState.IsValid)
                {
                    string password = md5.GetMd5Hash(model.Password);
                    var currUser = db.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == password);

                    // đăng nhập thành công
                    if (currUser != null)
                    {
                        if (!currUser.Active)
                            // 1.3 Tài khoản bị khóa
                            ModelState.AddModelError("", "Tài khoản đã bị khóa, liên hệ admin để mở tài khoản");
                        else
                        {
                            Session["Username"] = currUser.Username;
                            Session["Role"] = currUser.Role;

                            if (returnUrl != null)
                                return Redirect(returnUrl);
                            else return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                        // 1.1 Sai tên tài khoản hoặc mật khẩu
                        ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu");
                }
            }

            return View();
        }

        //
        // GET: /User/Register
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /User/Login
        [HttpPost]
        public ActionResult Register(RegisterViewModel user)
        {
            User newUser = new User();
            if (ModelState.IsValid)
            {
                newUser.Username = user.Username;
                newUser.Name = user.Name;
                newUser.Address = user.Address;
                newUser.Email = user.Email;
                newUser.Phone = user.Password;
                newUser.Active = true;
                newUser.Role = "User";
                newUser.JoiningDate = DateTime.Now;
                newUser.Password = md5.GetMd5Hash(user.Password);

                db.Users.Add(newUser);
                db.SaveChanges();

                ViewBag.RegisterMsg = "Thêm người dùng mới thành công";

                return View();
            }

            return View();
        }

        public JsonResult IsUsernameExist(string Username)
        {
            return Json(!db.Users.Any(user => user.Username == Username), JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /User/LogOff
        [CheckAuthorize]
        //[HttpPost]
        public ActionResult LogOff()
        {
            Session["Username"] = null;
            Session["Role"] = null;

            return View("Index", "Home");
        }
	}
}