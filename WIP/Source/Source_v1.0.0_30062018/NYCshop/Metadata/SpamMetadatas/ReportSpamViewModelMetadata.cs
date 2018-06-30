using NYCshop.Models;
using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.SpamMetadatas
{
    public class ReportSpamViewModelMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "SpamIDRequired", ErrorMessageResourceType = typeof(SpamErrorMsg))]
        [Display(Name = "SpamID", ResourceType = typeof(SpamDisplay))]
        public int SpamID { get; set; }

        [Required(ErrorMessageResourceName = "SpamContentRequired", ErrorMessageResourceType = typeof(SpamErrorMsg))]
        [Display(Name = "SpamContent", ResourceType = typeof(SpamDisplay))]
        public string SpamContent { get; set; }

        [Display(Name = "ProductID", ResourceType = typeof(ProductDisplay))]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        private Product Product { get; set; }
    }
}