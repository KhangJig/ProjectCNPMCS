using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.CustomTypes
{
    /// <summary>
    /// Kiểu trả về khi lấy ra danh sách các sản phẩm
    /// </summary>
    public class GetListProductResultType : SuccessAndMsg
    {
        /// <summary>
        /// Danh sách các sản phẩm
        /// </summary>
        public List<Product> ListProducts { get; set; }

        public GetListProductResultType(bool isSuccess = false, string message = "", List<Product> lstProducts = null)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;

            if (lstProducts != null)
                this.ListProducts = lstProducts;
            else this.ListProducts = new List<Product>();
        }
    }
}