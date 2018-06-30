using NYCshop.Models;
using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata
{
    public class CommentMetadata
    {
        [Key]
        [Display(Name = "CommentID", ResourceType = typeof(CommentDisplay))]
        public int CommentID { get; set; }

        [Display(Name = "Username", ResourceType = typeof(UserDisplay))]
        public string Username { get; set; }
        [ForeignKey("Username")]
        private User User { get; set; }

        [Display(Name = "ProductID", ResourceType = typeof(ProductDisplay))]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        private Product Product { get; set; }

        [Required(ErrorMessageResourceName = "CommentContentRequired", ErrorMessageResourceType = typeof(CommentErrorMsg))]
        [Display(Name = "CommentContent", ResourceType = typeof(CommentDisplay))]
        public string CommentContent { get; set; }

        [Required(ErrorMessageResourceName = "TimeCommentRequired", ErrorMessageResourceType = typeof(CommentErrorMsg))]
        [Display(Name = "TimeComment", ResourceType = typeof(CommentDisplay))]
        public DateTime TimeComment { get; set; }
    }

    public class CommentInProductViewModelMetadata
    {
        [Key]
        [Display(Name = "CommentID", ResourceType = typeof(CommentDisplay))]
        public int CommentID { get; set; }

        [Display(Name = "ProductID", ResourceType = typeof(ProductDisplay))]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        private Product Product { get; set; }

        [Display(Name = "Name", ResourceType = typeof(UserDisplay))]
        public string Name { get; set; }

        [Display(Name = "CommentContent", ResourceType = typeof(CommentDisplay))]
        public string CommentContent { get; set; }

        [Display(Name = "TimeComment", ResourceType = typeof(CommentDisplay))]
        public DateTime TimeComment { get; set; }

        [Display(Name = "Các phản hồi")]
        public List<ReplyViewModel> Replies { get; set; }
    }
}