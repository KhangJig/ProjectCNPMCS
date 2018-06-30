using NYCshop.Metadata.CategoryMetadatas;
using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.ViewModels.CategoryViewModel
{
    [MetadataType(typeof(CateWithSubViewModelMetadata))]
    public class CateWithSubViewModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public List<SubCategory> ListSubCats { get; set; }
    }
}