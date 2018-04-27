using NYCshop.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Models
{
    [MetadataType(typeof(ProductMetadata))]
    public class Product
    {
        public int ProductID { get; set; }
        public string Username { get; set; }
        private User User { get; set; }
        public int SubCategoryID { get; set; }
        private SubCategory SubCategory { get; set; }
        public string ProductName { get; set; }
        public string Describe { get; set; }
        public int Quanlity { get; set; }
        public long Price { get; set; }
        public bool SaleStatus { get; set; }
    }

    [MetadataType(typeof(ProductViewModelMetadata))]
    public class ProductViewModel
    {

    }
}