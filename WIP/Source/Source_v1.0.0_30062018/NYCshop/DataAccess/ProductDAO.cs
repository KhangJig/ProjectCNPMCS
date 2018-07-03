using NYCshop.CustomTypes;
using NYCshop.Models;
using NYCshop.Resources.ActionMessageInController;
using NYCshop.Resources.DataAccessMessage;
using NYCshop.ViewModels.ProductViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.DataAccess
{
    public class ProductDAO
    {
        private ExLoverShopDb db = new ExLoverShopDb();
        private ImageDAO imageDAO = new ImageDAO();
        private UserDAO userDAO = new UserDAO();

        public ProductDAO()
        {

        }

        /// <summary>
        /// Trả về danh sách sản phẩm List<ProductManagerViewModel> của người dùng được xét
        /// </summary>
        /// <param name="username">Tên người dùng</param>
        /// <returns></returns>
        public List<ProductManagerViewModel> GetListProductManager(string username)
        {
            List<ProductManagerViewModel> result = new List<ProductManagerViewModel>();

            try
            {
                var products = from p in db.Products
                               where p.Username == username
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
                                   UploadDate = p.UploadDate,
                                   PerformAction = false
                               };

                // thiết lập kết quả trả về
                if(products != null && products.Count() > 0)
                    result = products.ToList();
            }
            catch(Exception e)
            {
                
            }

            return result;
        }

        /// <summary>
        /// Lấy ra tất cả sản phẩm theo tên người dùng
        /// </summary>
        /// <param name="username">Tên người dùng</param>
        /// <returns></returns>
        public SuccessAndMsg GetListProduct(string username)
        {
            try
            {
                var products = db.Products.Where(p => p.Username == username);
                if (products != null)
                    return new SuccessAndMsg(true, GetListProductResult.GetListProductSuccessful, products.ToList());

                return new SuccessAndMsg(false, GetListProductResult.GetListProductFailed);
            }
            catch
            {
                return new SuccessAndMsg(false, GetListProductResult.GetListProductFailed);
            }
        }

        /// <summary>
        /// Lấy chi tiết sản phẩm theo tên người dùng
        /// </summary>
        /// <param name="username">Tên người dùng</param>
        /// <returns></returns>
        public SuccessAndMsg GetAccListProduct(string username)
        {
            List<ProductDetailViewModel> lstProducts = new List<ProductDetailViewModel>();

            try
            {
                List<string> imageUrls = new List<string>();
                var products = from p in db.Products
                               where p.Username == username
                               select new ProductDetailViewModel
                               {
                                   ProductID = p.ProductID,
                                   ProductName = p.ProductName,
                                   Price = p.Price,
                                   Username = p.Username,
                                   ListImages = imageUrls
                               };

                // thiết lập kết quả trả về
                if (products != null && products.Count() > 0)
                {
                    lstProducts = products.ToList();

                    // gán danh sách đường dẫn hình ảnh vào các sản phẩm liên quan
                    foreach(ProductDetailViewModel product in lstProducts)
                    {
                        SuccessAndMsg getListImageUrlsResult = imageDAO.GetListImageUrls(product.ProductID);
                        if (getListImageUrlsResult.IsSuccess)
                            product.ListImages = getListImageUrlsResult.Value as List<string>;
                    }

                    return new SuccessAndMsg(true, GetListProductResult.GetListProductSuccessful, lstProducts);
                }
                    
            }
            catch (Exception e)
            {

            }

            return new SuccessAndMsg(false, GetListProductResult.GetListProductFailed);
        }

        public SuccessAndMsg GetProductReport(int productID)
        {
            var product = db.Products.FirstOrDefault(p => p.ProductID == productID);
            if (product != null)
            {
                SuccessAndMsg getUserResult = userDAO.GetUser(product.Username);
                SuccessAndMsg getImageUrlResult = imageDAO.GetFirstUrlString(productID);
                // lấy thông tin người dùng thất bại
                if (!getUserResult.IsSuccess)
                    return new SuccessAndMsg(false, getUserResult.Message);
                // lấy đường dẫn hình ảnh thất bại
                if(!getImageUrlResult.IsSuccess)
                    return new SuccessAndMsg(false, getImageUrlResult.Message);

                User user = getUserResult.Value as User;
                string url = getImageUrlResult.Value as string;
                ProductReportViewModel result = new ProductReportViewModel(product, user, url);
                return new SuccessAndMsg(true, ProductDAOMsg.GetProductReportSuccessful, result);
            }

            return new SuccessAndMsg(false, ProductDAOMsg.GetProductReportFailed);
        }

        public SuccessAndMsg GetProduct(int productID)
        {
            Product product = db.Products.FirstOrDefault(p => p.ProductID == productID);
            if (product != null)
            {
                return new SuccessAndMsg(true, ProductDAOMsg.GetProductSuccessful, product);
            }

            return new SuccessAndMsg(false, ProductDAOMsg.GetProductFailed);
        }
    }
}