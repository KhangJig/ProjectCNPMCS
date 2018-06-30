using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.CustomTypes
{
    /// <summary>
    /// Kiểu trả về khi xóa sản phẩm
    /// </summary>
    public class DeleteProductsResultType : SuccessAndMsg
    {
        /// <summary>
        /// Danh sách sản phẩm cần xóa
        /// </summary>
        public List<Product> ListDelProducts { get; set; }

        /// <summary>
        /// Hàm khởi tạo cho DeleteProductsResultType
        /// </summary>
        /// <param name="isSuccess">Thành công hay không?</param>
        /// <param name="message">Thông điệp gởi tới người dùng</param>
        /// <param name="lstDelProducts">Danh sách sản phẩm cần xóa</param>
        public DeleteProductsResultType(bool isSuccess = false, string message = "", List<Product> lstDelProducts = null)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;

            if (lstDelProducts != null)
                this.ListDelProducts = lstDelProducts;
            else this.ListDelProducts = new List<Product>();
        }
    }
}