using NYCshop.Models;
using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.CategoryMetadatas
{
    public class CateWithSubViewModelMetadata
    {
        [Key]
        [Display(Name = "CategoryIDRequired", ResourceType = typeof(CategoryDisplay))]
        public int CategoryID { get; set; }

        [Required(ErrorMessageResourceName = "CategoryNameRequired", ErrorMessageResourceType = typeof(CategoryErrorMsg))]
        [MaxLength(30, ErrorMessageResourceName = "CategoryNameMaxLength", ErrorMessageResourceType = typeof(CategoryErrorMsg))]
        public string CategoryName { get; set; }

        public List<SubCategory> ListSubCats { get; set; }
    }
}