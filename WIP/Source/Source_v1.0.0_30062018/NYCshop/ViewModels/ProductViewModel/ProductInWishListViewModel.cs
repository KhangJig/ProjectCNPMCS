using NYCshop.Metadata.ProductMetadatas;
using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.ViewModels.ProductViewModel
{
    /// <summary>
    /// Thông tin sản phẩm khi hiển thị trên trang Danh sách sản phẩm muốn mua
    /// </summary>
    [MetadataType(typeof(ProductInWishListViewModelMetadata))]
    public class ProductInWishListViewModel
    {
        public ProductInWishListViewModel() { }

        public ProductInWishListViewModel(Product product, User user, string imageUrl) 
        {
            if(product != null && user != null && imageUrl != null)
            {
                this.ProductID = product.ProductID;
                this.ProductName = product.ProductName;
                this.Price = product.Price;
                this.Username = product.Username;
                this.SellerName = user.Name;
                this.Phone = user.Phone;
                this.ImageUrl = imageUrl;
                this.Delete = false;
            }
        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public long Price { get; set; }
        public string SellerName { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public bool Delete { get; set; }
    }
}