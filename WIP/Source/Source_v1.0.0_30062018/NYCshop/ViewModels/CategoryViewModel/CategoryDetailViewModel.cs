using NYCshop.Metadata.CategoryMetadatas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.ViewModels.CategoryViewModel
{
    [MetadataType(typeof(CategoryDetailViewModelMetadata))]
    public class CategoryDetailViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public long Price { get; set; }
        public List<string> ListImages { get; set; }
    }
}