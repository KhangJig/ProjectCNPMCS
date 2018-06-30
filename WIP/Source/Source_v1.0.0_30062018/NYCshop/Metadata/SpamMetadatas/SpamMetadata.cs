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
    public class SpamMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "SpamIDRequired", ErrorMessageResourceType = typeof(SpamErrorMsg))]
        [Display(Name = "SpamID", ResourceType = typeof(SpamDisplay))]
        public int SpamID { get; set; }

        [MinLength(6, ErrorMessageResourceName = "ReporterMinLength", ErrorMessageResourceType = typeof(SpamErrorMsg))]
        [Display(Name = "Reporter", ResourceType = typeof(SpamDisplay))]
        public string Reporter { get; set; }
        [ForeignKey("Reporter")]
        public virtual User UserReporter { get; set; }

        [Required(ErrorMessageResourceName = "SpamContentRequired", ErrorMessageResourceType = typeof(SpamErrorMsg))]
        [Display(Name = "SpamContent", ResourceType = typeof(SpamDisplay))]
        public string SpamContent { get; set; }

        [Display(Name = "ProductID", ResourceType = typeof(ProductDisplay))]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        private Product Product { get; set; }

        [Required(ErrorMessageResourceName = "ReportDateRequired", ErrorMessageResourceType = typeof(SpamErrorMsg))]
        [Display(Name = "ReportDate", ResourceType = typeof(SpamDisplay))]
        [DataType(DataType.DateTime)]
        public DateTime ReportDate { get; set; }

        [Required(ErrorMessageResourceName = "ResolvedRequired", ErrorMessageResourceType = typeof(SpamErrorMsg))]
        [Display(Name = "Resolved", ResourceType = typeof(SpamDisplay))]
        public bool Resolved { get; set; }

        [Display(Name = "ResolveDate", ResourceType = typeof(SpamDisplay))]
        [DataType(DataType.DateTime)]
        public DateTime? ResolveDate { get; set; }

        [MinLength(6, ErrorMessageResourceName = "ResolverMinLength", ErrorMessageResourceType = typeof(SpamErrorMsg))]
        [Display(Name = "Resolver", ResourceType = typeof(SpamDisplay))]
        public string Resolver { get; set; }
        [ForeignKey("Resolver")]
        public virtual User UserResolver { get; set; }

        [Display(Name = "ProperReport", ResourceType = typeof(SpamDisplay))]
        public bool? ProperReport { get; set; }
    }
}