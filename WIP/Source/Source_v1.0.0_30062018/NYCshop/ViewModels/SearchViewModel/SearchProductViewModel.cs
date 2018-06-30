using NYCshop.Metadata.SearchMetadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.ViewModels.SearchViewModel
{
    [MetadataType(typeof(SearchProductViewModelMetadata))]
    public class SearchProductViewModel
    {
        public int ProductID { get; set; }
        public int SubCategoryID { get; set; }
        public string ProductName { get; set; }
        public string Describe { get; set; }
        public int Quanlity { get; set; }
        public long Price { get; set; }
        public List<string> Images { get; set; }
    }
}