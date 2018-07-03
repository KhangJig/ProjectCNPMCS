using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.Models
{
    public partial class WishList
    {
        public WishList() { }

        public WishList(string username, int productID)
        {
            this.Username = username;
            this.ProductID = productID;
        }

        public string Username { get; set; }
        public virtual User User { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}