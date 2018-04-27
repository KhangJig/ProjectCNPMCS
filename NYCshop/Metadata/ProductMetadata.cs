using NYCshop.Models;
using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata
{
    public class ProductMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "ProductIDRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "ProductID", ResourceType = typeof(ProductDisplay))]
        public int ProductID { get; set; }

        [Display(Name = "Username", ResourceType = typeof(UserDisplay))]
        public string Username { get; set; }
        [ForeignKey("Username")]
        private User User { get; set; }

        [Display(Name = "SubCategoryID", ResourceType = typeof(SubCategoryDisplay))]
        public int SubCategoryID { get; set; }
        [ForeignKey("SubCategoryID")]
        private SubCategory SubCategory { get; set; }

        [Required(ErrorMessageResourceName = "ProductNameRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "ProductName", ResourceType = typeof(ProductDisplay))]
        public string ProductName { get; set; }

        [Required(ErrorMessageResourceName = "DescribeRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "Describe", ResourceType = typeof(ProductDisplay))]
        public string Describe { get; set; }

        [Required(ErrorMessageResourceName = "QuanlityRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "Quanlity", ResourceType = typeof(ProductDisplay))]
        public int Quanlity { get; set; }

        [Required(ErrorMessageResourceName = "PriceRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "Price", ResourceType = typeof(ProductDisplay))]
        public long Price { get; set; }

        [Required(ErrorMessageResourceName = "SaleStatusRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "SaleStatus", ResourceType = typeof(ProductDisplay))]
        public bool SaleStatus { get; set; }
    }

    public class ProductViewModelMetadata
    {

    }
}