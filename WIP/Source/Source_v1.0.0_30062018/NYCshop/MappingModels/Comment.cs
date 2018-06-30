using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.Models
{
    public partial class Comment
    {
        public int CommentID { get; set; }
        public string Username { get; set; }
        private User User { get; set; }
        public int ProductID { get; set; }
        private Product Product { get; set; }
        public string CommentContent { get; set; }
        public DateTime TimeComment { get; set; }
    }
}