using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.ProductMetadatas
{
    public class ProductReportViewModelMetadata
    {
        [Key]
        [Display(Name = "ProductName", ResourceType = typeof(ProductDisplay))]
        public int ProductID { get; set; }

        [Display(Name = "ProductName", ResourceType = typeof(ProductDisplay))]
        public string ProductName { get; set; }

        [Display(Name = "Name", ResourceType = typeof(UserDisplay))]
        public string Name { get; set; }

        [Display(Name = "Username", ResourceType = typeof(UserDisplay))]
        public string Username { get; set; }

        [Display(Name = "UploadDate", ResourceType = typeof(ProductDisplay))]
        [DataType(DataType.Date)]
        public DateTime UploadDate { get; set; }

        [Display(Name = "Url", ResourceType = typeof(ImageUrlDisplay))]
        public string Url { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(UserDisplay))]
        public string Phone { get; set; }
    }
}