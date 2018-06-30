using NYCshop.Metadata.ProductMetadatas;
using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.ViewModels.ProductViewModel
{
    [MetadataType(typeof(ProductReportViewModelMetadata))]
    public class ProductReportViewModel
    {
        public ProductReportViewModel()
        {

        }

        /// <summary>
        /// Khởi tạo đối tượng ProductReportViewModel với thông tin sản phẩm, tên người dùng, họ tên
        /// </summary>
        /// <param name="product">Thông tin sản phẩm</param>
        /// <param name="username">Tên người dùng</param>
        /// <param name="name">Họ tên</param>
        public ProductReportViewModel(Product product, User user, string url)
        {
            if(product != null)
            {
                this.ProductID = product.ProductID;
                this.ProductName = product.ProductName;
                this.UploadDate = product.UploadDate;
                this.Name = user.Name;
                this.Username = user.Username;
                this.Url = url;
                this.Phone = user.Phone;
            }
        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public DateTime UploadDate { get; set; }
        public string Url { get; set; }
        public string Phone { get; set; }
    }
}