using NYCshop.Assets;
using NYCshop.Attributes;
using NYCshop.CustomTypes;
using NYCshop.DataAccess;
using NYCshop.Metadata;
using NYCshop.Models;
using NYCshop.Resources.ActionMessageInController;
using NYCshop.Resources.ResourceFiles;
using NYCshop.ViewModels.UserViewModel;
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
        private AccountDAO dao = new AccountDAO();

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
                LoginResultType loginResult = dao.Login(model);

                if (loginResult.IsSuccess)
                {
                    Session["Username"] = loginResult.CurrentUser.Username;
                    Session["Role"] = loginResult.CurrentUser.Role;

                    // chuyển đến trang được yêu cầu trước đó
                    if (returnUrl != null)
                        return Redirect(returnUrl);
                    
                    // chuyển về trang chủ nếu không có trang yêu cầu trước đó
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError("", loginResult.Message);
                }
            }
            else
            {
                // hiển thị thông báo lỗi cho người dùng
                ModelState.AddModelError("", ActionMessage.MissingOrInvalidInfo);
            }

            return View();
        }

        //
        // GET: /User/Register
        public ActionResult Register()
        {
            // nếu người dùng đã đăng nhập thì chuyển về trang chủ, ngược lại thì chuyển đến trang đăng ký
            if (Session["Username"] == null)
                return View();
            else return RedirectToAction("Index", "Home");
        }

        //
        // POST: /User/Register
        [HttpPost]
        public ActionResult Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                SuccessAndMsg registerResult = dao.Register(user);
                if(registerResult.IsSuccess)
                {
                    // hiển thị thông báo đăng ký thành công
                    ViewBag.RegisterMsg = registerResult.Message;

                    // xóa dữ liệu cũ đi
                    ModelState.Clear();
                }
                else // lỗi đăng ký: tài khoản đã tồn tại
                {
                    ModelState.AddModelError("", registerResult.Message);
                    return View(user);
                }
            }
            else
            {
                // hiển thị thông báo lỗi cho người dùng
                ModelState.AddModelError("", ActionMessage.MissingOrInvalidInfo);
            }

            return View();
        }

        /// <summary>
        /// Kiểm tra xem người dùng có tồn tại hay không
        /// </summary>
        /// <param name="Username">Tên người dùng</param>
        /// <returns></returns>
        public JsonResult IsUsernameExist(string Username)
        {
            return Json(!db.Users.Any(user => user.Username == Username), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GET: /Account/LogOff
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        [CheckAnynomous]
        public ActionResult LogOff()
        {
            // xóa phiên làm việc của người dùng
            Session["Username"] = null;
            Session["Role"] = null;

            return View("Index", "Home");
        }

        /// <summary>
        /// Xác nhận mật khẩu cho thao tác xóa
        /// </summary>
        /// <returns></returns>
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