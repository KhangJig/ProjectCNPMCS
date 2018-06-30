using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.CategoryMetadatas
{
    public class CategoryMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "CategoryIDRequired", ErrorMessageResourceType = typeof(CategoryErrorMsg))]
        [Display(Name = "CategoryID", ResourceType = typeof(CategoryDisplay))]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }

        [Required(ErrorMessageResourceName = "CategoryNameRequired", ErrorMessageResourceType = typeof(CategoryErrorMsg))]
        [MaxLength(30, ErrorMessageResourceName = "CategoryNameMaxLength", ErrorMessageResourceType = typeof(CategoryErrorMsg))]
        [Display(Name = "CategoryName", ResourceType = typeof(CategoryDisplay))]
        public string CategoryName { get; set; }

        [Required(ErrorMessageResourceName = "CategoryImageRequired", ErrorMessageResourceType = typeof(CategoryErrorMsg))]
        [Display(Name = "CategoryImage", ResourceType = typeof(CategoryDisplay))]
        public string CategoryImage { get; set; }
    }
}