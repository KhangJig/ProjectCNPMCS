using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.ReplyMetadatas
{
    public class ReplyViewModelMetadata
    {
        [Key]
        [Display(Name = "ReplyID", ResourceType = typeof(ReplyDisplay))]
        public int ReplyID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(UserDisplay))]
        public string Name { get; set; }

        [Display(Name = "CommentID", ResourceType = typeof(CommentDisplay))]
        public int CommentID { get; set; }

        [Required(ErrorMessageResourceName = "ReplyContentRequired", ErrorMessageResourceType = typeof(ReplyErrorMsg))]
        [Display(Name = "ReplyContent", ResourceType = typeof(ReplyDisplay))]
        public string ReplyContent { get; set; }

        [Required(ErrorMessageResourceName = "TimeReplyRequired", ErrorMessageResourceType = typeof(ReplyErrorMsg))]
        [Display(Name = "TimeReply", ResourceType = typeof(ReplyDisplay))]
        public DateTime TimeReply { get; set; }
    }
}