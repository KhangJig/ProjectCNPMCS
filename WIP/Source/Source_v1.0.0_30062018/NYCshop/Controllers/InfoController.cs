using NYCshop.CustomTypes;
using NYCshop.DataAccess;
using NYCshop.Models;
using NYCshop.Resources.ResourceFiles;
using NYCshop.ViewModels.ErrorViewModels;
using NYCshop.ViewModels.ProductViewModel;
using NYCshop.ViewModels.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace NYCshop.Controllers
{
    /// <summary>
    /// Hiển thị thông tin
    /// </summary>
    public class InfoController : Controller
    {
        private InfoDAO dao = new InfoDAO();
        private ProductDAO productDAO = new ProductDAO();
        private int pageSize = 8; // số lượng item trên 1 trang

        /// <summary>
        /// GET: /Info/Account
        /// Hiển thị các thông tin liên quan đến người dùng: thông tin cá nhân, các mặt hàng đang bán
        /// </summary>
        /// <param name="name"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public ActionResult AccountInfo(int? page, string username)
        {
            // Lấy ra thông tin của người dùng
            GetAccountInfoResultType getInfoResult = dao.GetAccountInfo(username);

            // lấy thông tin thành công
            if(getInfoResult.IsSuccess)
            {
                AccountInfoViewModel accInfo = getInfoResult.AccountInfo;
                ViewBag.Page = page;
                ViewBag.Name = accInfo.Name;
                return View(accInfo);
            }

            // đặt lỗi trong TempData để sử dụng hiển thị ra màn hình lỗi
            TempData["Error"] = new ErrorViewModel(InfoActionErrorMsg.GetAccountInfoError);
            return RedirectToAction("Error", "Info");
        }

        /// <summary>
        /// Lấy ra tất cả bài đăng của người dùng
        /// </summary>
        /// <param name="username">Tên người dùng</param>
        /// <returns></returns>
        public ActionResult AccountPost(int? page, string username, string name)
        {
            SuccessAndMsg getListProductResult = productDAO.GetAccListProduct(username);
            if(getListProductResult.IsSuccess)
            {
                // lấy danh sách sản phẩm thành công
                List<ProductDetailViewModel> lstProducts = getListProductResult.Value as List<ProductDetailViewModel>;

                int pageNumber = (page ?? 1); // trả về giá trị của page nếu nó là 1 giá trị khác null, nếu không trả về 1

                ViewBag.Username = username;
                ViewBag.ProductQuanlity = lstProducts.Count;
                ViewBag.Name = name;
                return PartialView("_GetAccountPostPartial", lstProducts.OrderByDescending(m => m.ProductID).ToPagedList(pageNumber, pageSize));
            }

            // đặt lỗi trong TempData để sử dụng hiển thị ra màn hình lỗi
            TempData["Error"] = new ErrorViewModel(InfoActionErrorMsg.AccountPostError);
            return RedirectToAction("Error", "Info");
        }

        /// <summary>
        /// Chuyển sang trang lỗi
        /// </summary>
        /// <param name="model">Thông tin về lỗi</param>
        /// <returns></returns>
        public ActionResult Error()
        {
            ErrorViewModel model = new ErrorViewModel();
            if(TempData.ContainsKey("Error"))
            {
                // gán lỗi vào biến model
                model = TempData["Error"] as ErrorViewModel;
            }

            return View(model);
        }
	}
}