using NYCshop.Models;
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

        public ProductDAO()
        {

        }

        /// <summary>
        /// Trả về danh sách sản phẩm của người dùng được xét
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
    }
}