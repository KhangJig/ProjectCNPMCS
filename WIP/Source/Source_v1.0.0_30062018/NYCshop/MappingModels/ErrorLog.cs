using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.Models
{
    public partial class ErrorLog
    {
        public int ErrorID { get; set; }
        public string ErrorContent { get; set; }
        public string FunctionName { get; set; }
        public DateTime OccurDate { get; set; }
        public string Username { get; set; }
        private User User { get; set; }
    }
}