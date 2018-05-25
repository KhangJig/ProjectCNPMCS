using NYCshop.Models;
using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata
{
    public class SubCategoryMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "SubCategoryIDRequired", ErrorMessageResourceType = typeof(SubCategoryErrorMsg))]
        [Display(Name = "SubCategoryID", ResourceType = typeof(SubCategoryDisplay))]
        public int SubCategoryID { get; set; }

        [Display(Name = "CategoryIDRequired", ResourceType = typeof(CategoryDisplay))]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        private Category Category { get; set; }

        [Required(ErrorMessageResourceName = "SubCategoryNameRequired", ErrorMessageResourceType = typeof(SubCategoryErrorMsg))]
        [Display(Name = "SubCategoryName", ResourceType = typeof(SubCategoryDisplay))]
        public string SubCategoryName { get; set; }
    }
}