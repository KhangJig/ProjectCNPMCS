using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.SearchMetadata
{
    public class SearchViewModelMetadata
    {
        [Key]
        public string SearchString { get; set; }
        public string SearchType { get; set; }
    }
}