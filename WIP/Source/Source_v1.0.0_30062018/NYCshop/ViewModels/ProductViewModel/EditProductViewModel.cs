using NYCshop.Metadata.ProductMetadatas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.ViewModels.ProductViewModel
{
    [MetadataType(typeof(EditProductViewModelMetadata))]
    public class EditProductViewModel
    {
        public EditProductViewModel()
        {
            this.Update = false;
        }

        public int ProductID { get; set; }
        public int SubCategoryID { get; set; }
        public string ProductName { get; set; }
        public string Describe { get; set; }
        public int Quanlity { get; set; }
        public long Price { get; set; }
        public bool SaleStatus { get; set; }
        public bool Update { get; set; }
    }
}