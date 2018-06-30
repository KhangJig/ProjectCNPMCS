using NYCshop.Metadata.CommentMetadatas;
using NYCshop.Models;
using NYCshop.ViewModels.ReplyViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.ViewModels.CommentViewModel
{
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