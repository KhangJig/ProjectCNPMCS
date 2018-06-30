using NYCshop.Metadata.ReplyMetadatas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.ViewModels.ReplyViewModels
{
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