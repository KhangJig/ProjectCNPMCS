using NYCshop.Metadata.ProductMetadatas;
using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.ViewModels.ProductViewModel
{
    [MetadataType(typeof(ProductManagerViewModelMetadata))]
    public class ProductManagerViewModel
    {
        public ProductManagerViewModel()
        {

        }

        public ProductManagerViewModel(Product product, string imageUrl)
        {
            this.Censor = product.Censor;
            this.Image = imageUrl;
            this.Price = product.Price;
            this.ProductID = product.ProductID;
            this.ProductName = product.ProductName;
            this.Quanlity = product.Quanlity;
            this.SaleStatus = product.SaleStatus;
            this.UploadDate = product.UploadDate;
            this.Username = product.Username;
            this.Viewed = product.Viewed;
        }

        public int ProductID { get; set; }
        public string Username { get; set; }
        public string ProductName { get; set; }
        public int Quanlity { get; set; }
        public long Price { get; set; }
        public string Image { get; set; }
        public bool PerformAction { get; set; }
        public bool SaleStatus { get; set; }
        public bool Censor { get; set; }
        public bool Viewed { get; set; }
        public DateTime UploadDate { get; set; }
    }
}