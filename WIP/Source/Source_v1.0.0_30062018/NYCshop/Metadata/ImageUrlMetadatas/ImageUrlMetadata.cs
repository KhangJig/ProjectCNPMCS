using NYCshop.Models;
using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.ImageUrlMetadatas
{
    public class ImageUrlMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "ImageIDRequired", ErrorMessageResourceType = typeof(ImageUrlErrorMsg))]
        [Display(Name = "ImageID", ResourceType = typeof(ImageUrlDisplay))]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ImageID { get; set; }

        [Required(ErrorMessageResourceName = "UrlRequired", ErrorMessageResourceType = typeof(ImageUrlErrorMsg))]
        [Display(Name = "Url", ResourceType = typeof(ImageUrlDisplay))]
        public string Url { get; set; }

        [Display(Name = "ProductID", ResourceType = typeof(CategoryDisplay))]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        private Product Product { get; set; }
    }
}