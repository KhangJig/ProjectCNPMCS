using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.ProductMetadatas
{
    public class ProductManagerViewModelMetadata
    {
        [Key]
        [Display(Name = "ProductID", ResourceType = typeof(ProductDisplay))]
        public int ProductID { get; set; }

        [Display(Name = "Username", ResourceType = typeof(UserDisplay))]
        public string Username { get; set; }

        [Display(Name = "ProductName", ResourceType = typeof(ProductDisplay))]
        public string ProductName { get; set; }

        [Display(Name = "Quanlity", ResourceType = typeof(ProductDisplay))]
        public int Quanlity { get; set; }

        [Display(Name = "Price", ResourceType = typeof(ProductDisplay))]
        [DisplayFormat(DataFormatString = "{0:0,0 đ}")]
        public long Price { get; set; }

        [Display(Name = "Image", ResourceType = typeof(ProductDisplay))]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [Display(Name = "Thực hiện hành động")]
        public bool PerformAction { get; set; }

        [Display(Name = "SaleStatus", ResourceType = typeof(ProductDisplay))]
        public bool SaleStatus { get; set; }

        [Display(Name = "Censor", ResourceType = typeof(ProductDisplay))]
        public bool Censor { get; set; }

        [Display(Name = "Viewed", ResourceType = typeof(ProductDisplay))]
        public bool Viewed { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "UploadDate", ResourceType = typeof(ProductDisplay))]
        public DateTime UploadDate { get; set; }
    }
}