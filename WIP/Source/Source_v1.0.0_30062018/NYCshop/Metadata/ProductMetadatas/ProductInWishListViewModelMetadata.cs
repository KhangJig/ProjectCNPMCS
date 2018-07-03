using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.ProductMetadatas
{
    public class ProductInWishListViewModelMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "ProductIDRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "ProductID", ResourceType = typeof(ProductDisplay))]
        public int ProductID { get; set; }

        [Required(ErrorMessageResourceName = "ProductNameRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "ProductName", ResourceType = typeof(ProductDisplay))]
        public string ProductName { get; set; }

        [Required(ErrorMessageResourceName = "PriceRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "Price", ResourceType = typeof(ProductDisplay))]
        [DisplayFormat(DataFormatString = "{0:0,0 đ}")]
        public long Price { get; set; }

        [Required(ErrorMessageResourceName = "SellerNameRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "SellerName", ResourceType = typeof(ProductDisplay))]
        public string SellerName { get; set; }

        [Required(ErrorMessageResourceName = "UsernameRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Username", ResourceType = typeof(UserDisplay))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "PhoneRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Phone", ResourceType = typeof(UserDisplay))]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceName = "UrlRequired", ErrorMessageResourceType = typeof(ImageUrlErrorMsg))]
        [Display(Name = "Url", ResourceType = typeof(ImageUrlDisplay))]
        public string ImageUrl { get; set; }

        public bool Delete { get; set; }
    }
}