using NYCshop.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Models
{
    [MetadataType(typeof(CategoryMetadata))]
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}