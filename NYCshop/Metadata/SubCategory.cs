using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata
{
    [MetadataType(typeof(SubCategoryMetadata))]
    public class SubCategory
    {
        public int SubCategoryID { get; set; }
        public int CategoryID { get; set; }
        private Category Category { get; set; }
        public string SubCategoryName { get; set; }
    }
}