using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.Models
{
    public partial class Reply
    {
        public int ReplyID { get; set; }
        public string Username { get; set; }
        private User User { get; set; }
        public int CommentID { get; set; }
        private Comment Comment { get; set; }
        public string ReplyContent { get; set; }
        public DateTime TimeReply { get; set; }
    }
}