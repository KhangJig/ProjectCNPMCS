using NYCshop.Models;
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
        public int CommentID { get; set; }

        public string Username { get; set; }
        
        private User User { get; set; }

        public int ProductID { get; set; }
        private Product Product { get; set; }

        public string CommentContent { get; set; }
    }
}