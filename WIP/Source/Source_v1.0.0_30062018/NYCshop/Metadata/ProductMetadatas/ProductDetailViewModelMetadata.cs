using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.ProductMetadatas
{
    public class ProductDetailViewModelMetadata
    {
        [Key]
        [Display(Name = "ProductID", ResourceType = typeof(ProductDisplay))]
        public int ProductID { get; set; }

        [Display(Name = "ProductName", ResourceType = typeof(ProductDisplay))]
        public string ProductName { get; set; }

        [Display(Name = "Username", ResourceType = typeof(UserDisplay))]
        public string Username { get; set; }

        [Display(Name = "Name", ResourceType = typeof(UserDisplay))]
        public string Name { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(UserDisplay))]
        public string Phone { get; set; }

        [Display(Name = "SubCategoryID", ResourceType = typeof(SubCategoryDisplay))]
        public string SubCategory { get; set; }

        [Display(Name = "CategoryName", ResourceType = typeof(CategoryDisplay))]
        public string Category { get; set; }

        [Display(Name = "CategoryID", ResourceType = typeof(CategoryDisplay))]
        public int CategoryID { get; set; }

        [Display(Name = "Describe", ResourceType = typeof(ProductDisplay))]
        public string Describe { get; set; }

        [Display(Name = "Price", ResourceType = typeof(ProductDisplay))]
        [DisplayFormat(DataFormatString = "{0:0,0 đ}")]
        public long Price { get; set; }

        public List<string> ListImages { get; set; }
    }
}