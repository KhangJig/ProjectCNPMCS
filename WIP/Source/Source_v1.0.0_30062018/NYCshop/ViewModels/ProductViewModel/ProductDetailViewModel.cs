using NYCshop.Metadata.ProductMetadatas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.ViewModels.ProductViewModel
{
    [MetadataType(typeof(ProductDetailViewModelMetadata))]
    public class ProductDetailViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string SubCategory { get; set; }
        public string Category { get; set; }
        public int CategoryID { get; set; }
        public string Describe { get; set; }
        public long Price { get; set; }
        public List<string> ListImages { get; set; }
    }
}