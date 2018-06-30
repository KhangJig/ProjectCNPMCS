using NYCshop.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Models
{
    [MetadataType(typeof(CommentMetadata))]
    public class Comment
    {
        public int CommentID { get; set; }
        public string Username { get; set; }
        private User User { get; set; }
        public int ProductID { get; set; }
        private Product Product { get; set; }
        public string CommentContent { get; set; }
        public DateTime TimeComment { get; set; }
    }

    [MetadataType(typeof(CommentInProductViewModelMetadata))]
    public class CommentInProductViewModel
    {
        public int CommentID { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        private Product Product { get; set; }
        public string CommentContent { get; set; }
        public DateTime TimeComment { get; set; }
        public List<ReplyViewModel> Replies { get; set; }
    }
}