using NYCshop.Models;
using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.WishListMetadatas
{
    public class WishListMetadata
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "Username", ResourceType = typeof(UserDisplay))]
        [MinLength(6, ErrorMessageResourceName = "UsernameMinLength", ErrorMessageResourceType = typeof(UserErrorMsg))]
        public string Username { get; set; }
        [ForeignKey("Username")]
        public virtual User User { get; set; }

        [Key]
        [Column(Order = 1)]
        [Display(Name = "ProductID", ResourceType = typeof(ProductDisplay))]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}