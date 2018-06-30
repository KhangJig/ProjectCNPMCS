using NYCshop.Models;
using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.ErrorLogMetadatas
{
    public class ErrorLogMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "ErrorIDRequired", ErrorMessageResourceType = typeof(ErrorLogErrorMsg))]
        public int ErrorID { get; set; }

        [Required(ErrorMessageResourceName = "ErrorContentRequired", ErrorMessageResourceType = typeof(ErrorLogErrorMsg))]
        public string ErrorContent { get; set; }

        [Required(ErrorMessageResourceName = "FunctionNameRequired", ErrorMessageResourceType = typeof(ErrorLogErrorMsg))]
        public string FunctionName { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceName = "OccurDateRequired", ErrorMessageResourceType = typeof(ErrorLogErrorMsg))]
        public DateTime OccurDate { get; set; }

        public string Username { get; set; }
        [ForeignKey("Username")]
        private User User { get; set; }
    }
}