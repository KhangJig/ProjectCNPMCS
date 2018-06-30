using NYCshop.Models;
using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.ProductMetadatas
{
    public class ProductMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "ProductIDRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "ProductID", ResourceType = typeof(ProductDisplay))]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        [DisplayFormat(DataFormatString = "{0:0,0 đ}")]
        public long Price { get; set; }

        [Required(ErrorMessageResourceName = "SaleStatusRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "SaleStatus", ResourceType = typeof(ProductDisplay))]
        public bool SaleStatus { get; set; }

        [Required(ErrorMessageResourceName = "CensorRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "Censor", ResourceType = typeof(ProductDisplay))]
        public bool Censor { get; set; }

        [Required(ErrorMessageResourceName = "ViewedRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "Viewed", ResourceType = typeof(ProductDisplay))]
        public bool Viewed { get; set; }

        [Required(ErrorMessageResourceName = "UploadDateRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [DataType(DataType.Date)]
        [Display(Name = "UploadDate", ResourceType = typeof(ProductDisplay))]
        public DateTime UploadDate { get; set; }
    }
}