using NYCshop.Assets;
using NYCshop.Attributes;
using NYCshop.Metadata;
using NYCshop.Models;
using NYCshop.Resources.ResourceFiles;
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

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;

            //string controllerName = filterContext.RouteData.Values["controller"] as string;
            string actionName = filterContext.RouteData.Values["action"] as string;
            string httpMethod = filterContext.HttpContext.Request.HttpMethod;
            ErrorLog error = new ErrorLog();
            switch (actionName.ToLower())
            {
                case "login":
                    error.ErrorContent = e.ToString();
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.Login + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.Login + "'";
                    if (Session["Username"] != null)
                        error.Username = Session["Username"] as string;

                    db.ErrorLogs.Add(error);
                    db.SaveChanges();
                    break;
                case "register":
                    error.ErrorContent = e.ToString();
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.Register + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.Register + "'";

                    db.ErrorLogs.Add(error);
                    db.SaveChanges();
                    break;
                case "logoff":
                    error.ErrorContent = e.ToString();
                    if (httpMethod.ToLower() == "get")
                        error.FunctionName = "Lỗi xảy ra ở 'Trang " + FunctionNameDisplay.LogOff + "'";
                    else error.FunctionName = "Lỗi xảy ra ở Chức năng '" + FunctionNameDisplay.LogOff + "'";

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
        // GET: /Account/Login
        public ActionResult Login()
        {
            if (Session["Username"] == null)
                return View();
            else return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
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

            return View();
        }

        //
        // GET: /User/Register
        public ActionResult Register()
        {
            if (Session["Username"] == null)
                return View();
            else return RedirectToAction("Index", "Home");
        }

        //
        // POST: /User/Login
        [HttpPost]
        public ActionResult Register(RegisterViewModel user)
        {
            User newUser = new User();
            if (ModelState.IsValid)
            {
                var findUser = db.Users.FirstOrDefault(u => u.Username == user.Username);
                if(findUser == null) // chưa tồn tại người dùng
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
                }
                else // đã tồn tại người dùng
                {
                    ViewBag.RegisterErrorMsg = "Người dùng đã tồn tại";
                    ModelState.AddModelError("", UserErrorMsg.UsernameExist);
                    return View(user);
                }
            }

            return View();
        }

        public JsonResult IsUsernameExist(string Username)
        {
            return Json(!db.Users.Any(user => user.Username == Username), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Account/LogOff
        [CheckAnynomous]
        public ActionResult LogOff()
        {
            Session["Username"] = null;
            Session["Role"] = null;

            return View("Index", "Home");
        }

        public ActionResult ConfirmPassword()
        {
            ConfirmPasswordViewModel model = new ConfirmPasswordViewModel();
            model.Username = Session["Username"] as string;
            model.Password = "";

            var user = db.Users.FirstOrDefault(u => u.Username == model.Username);
            if (user != null)
                model.CorrectPassword = user.Password;

            return PartialView("_ConfirmPasswordPartial", model);
        }
	}
}