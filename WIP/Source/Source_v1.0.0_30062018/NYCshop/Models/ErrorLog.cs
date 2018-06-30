using NYCshop.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Models
{
    [MetadataType(typeof(ErrorLogMetadata))]
    public class ErrorLog
    {
        public int ErrorID { get; set; }
        public string ErrorContent { get; set; }
        public string FunctionName { get; set; }
        public DateTime OccurDate { get; set; }
        public string Username { get; set; }
        private User User { get; set; }
    }
}