using NYCshop.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Models
{
    [MetadataType(typeof(ReplyMetadata))]
    public class Reply
    {
        public int ReplyID { get; set; }
        public string Username { get; set; }
        private User User { get; set; }
        public int CommentID { get; set; }
        private Comment Comment { get; set; }
        public string ReplyContent { get; set; }
        public DateTime TimeReply { get; set; }
    }

    [MetadataType(typeof(ReplyViewModelMetadata))]
    public class ReplyViewModel
    {
        public int ReplyID { get; set; }
        public string Name { get; set; }
        public int CommentID { get; set; }
        public string ReplyContent { get; set; }
        public DateTime TimeReply { get; set; }
    }
}