using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.Models
{
    public partial class SubCategory
    {
        public int SubCategoryID { get; set; }
        public int CategoryID { get; set; }
        private Category Category { get; set; }
        public string SubCategoryName { get; set; }
    }
}