using NYCshop.CustomTypes;
using NYCshop.DataAccess;
using NYCshop.Models;
using NYCshop.Resources.ActionMessageInController;
using NYCshop.ViewModels.ErrorViewModels;
using NYCshop.ViewModels.ProductViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NYCshop.Controllers
{
    public class WishListController : Controller
    {
        private WishListDAO dao = new WishListDAO();

        //
        // GET: /WishList/
        public ActionResult Index()
        {
            Dictionary<int, ProductInWishListViewModel> model = new Dictionary<int, ProductInWishListViewModel>();
            if (Session["WishList"] == null)
                Session["WishList"] = new Dictionary<int, ProductInWishListViewModel>();

            // lấy Danh sách sản phẩm muốn mua từ Session
            model = Session["WishList"] as Dictionary<int, ProductInWishListViewModel>;

            return View(model.Values.ToList());
        }

        //
        // GET: /WishList/
        [HttpPost]
        public ActionResult Index(List<ProductInWishListViewModel> model)
        {
            if(model != null)
            {
                SuccessAndMsg updateWishList = null;
                if(Session["Username"] != null)
                {
                    string username = Session["Username"] as string;
                    updateWishList = dao.UpdateWishListInDb(model, username);
                }
                else updateWishList = dao.UpdateWishList(model);
                if (updateWishList.IsSuccess)
                {
                    Dictionary<int, ProductInWishListViewModel> wishList = updateWishList.Value as Dictionary<int, ProductInWishListViewModel>;
                    Session["WishList"] = wishList;
                    ViewBag.UpdateWishListMsg = WishListControllerMsg.UpdateWishListSuccessful;
                    model = wishList.Values.ToList();
                }
                else
                {
                    // cập nhật Danh sách sản phẩm muốn mua thất bại
                    TempData["Error"] = new ErrorViewModel(updateWishList.Message);
                    return RedirectToAction("SharedError", "Error");
                }
            }
            else model = new List<ProductInWishListViewModel>();
            
            return View(model);
        }

        //
        // POST:
        public ActionResult AddProductToWishList(int productID)
        {
            // nếu chưa có Danh sách sản phẩm muốn mua thì khởi tạo
            if (Session["WishList"] == null)
                Session["WishList"] = new Dictionary<int, ProductInWishListViewModel>();

            // đã có Danh sách sản phẩm muốn mua
            Dictionary<int, ProductInWishListViewModel> dictProducts = Session["WishList"] as Dictionary<int, ProductInWishListViewModel>;
            SuccessAndMsg addProdToWishListResult = null;
            if(Session["Username"] != null)
            {
                string username = Session["Username"] as string;
                addProdToWishListResult = dao.AddProdToWishListInDb(username, productID);
                dao.AddProdToWishList(dictProducts, productID);
            }
            else addProdToWishListResult = dao.AddProdToWishList(dictProducts, productID);

            if (addProdToWishListResult.IsSuccess)
            {
                // thay đổi Danh sách sản phẩm muốn mua
                Session["WishList"] = dictProducts;
                return RedirectToAction("Index");
            }   

            // thêm sản phẩm vào Danh sách sản phẩm muốn mua thất bại
            TempData["Error"] = new ErrorViewModel(addProdToWishListResult.Message);
            return RedirectToAction("SharedError", "Error");
        }        
	}
}