using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.ProductMetadatas
{
    public class EditProductViewModelMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "ProductIDRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "ProductID", ResourceType = typeof(ProductDisplay))]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [Display(Name = "SubCategoryID", ResourceType = typeof(SubCategoryDisplay))]
        public int SubCategoryID { get; set; }

        [Required(ErrorMessageResourceName = "ProductNameRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "ProductName", ResourceType = typeof(ProductDisplay))]
        public string ProductName { get; set; }

        [Required(ErrorMessageResourceName = "DescribeRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "Describe", ResourceType = typeof(ProductDisplay))]
        public string Describe { get; set; }

        [Required(ErrorMessageResourceName = "QuanlityRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "Quanlity", ResourceType = typeof(ProductDisplay))]
        [System.Web.Mvc.Remote("CheckQuanlity", "User", ErrorMessageResourceName = "InvalidQuanlity", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        public int Quanlity { get; set; }

        [Required(ErrorMessageResourceName = "PriceRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "Price", ResourceType = typeof(ProductDisplay))]
        [System.Web.Mvc.Remote("CheckPrice", "User", ErrorMessageResourceName = "InvalidPrice", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [DisplayFormat(DataFormatString = "{0:0,0 đ}")]
        public long Price { get; set; }

        [Required(ErrorMessageResourceName = "SaleStatusRequired", ErrorMessageResourceType = typeof(ProductErrorMsg))]
        [Display(Name = "SaleStatus", ResourceType = typeof(ProductDisplay))]
        public bool SaleStatus { get; set; }

        public bool Update { get; set; }
    }
}