using NYCshop.Metadata.ProductMetadatas;
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
        public Product() { }

        public Product(Product product)
        {
            if(product != null)
            {
                this.Censor = product.Censor;
                this.Describe = product.Describe;
                this.Price = product.Price;
                this.ProductID = product.ProductID;
                this.ProductName = product.ProductName;
                this.Quanlity = product.Quanlity;
                this.SaleStatus = product.SaleStatus;
                this.SubCategoryID = product.SubCategoryID;
                this.UploadDate = product.UploadDate;
                this.Username = product.Username;
                this.Viewed = product.Viewed;
            } 
        }

        public int ProductID { get; set; }
        public string Username { get; set; }
        private User User { get; set; }
        public int SubCategoryID { get; set; }
        private SubCategory SubCategory { get; set; }
        public string ProductName { get; set; }
        public DateTime UploadDate { get; set; }
        public string Describe { get; set; }
        public int Quanlity { get; set; }
        public long Price { get; set; }
        public bool SaleStatus { get; set; }
        public bool Censor { get; set; }
        public bool Viewed { get; set; }
    }
}