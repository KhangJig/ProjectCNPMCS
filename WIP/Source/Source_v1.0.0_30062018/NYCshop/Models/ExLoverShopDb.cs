using NYCshop.Metadata;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NYCshop.Models
{
    /// <summary>
    /// Lớp sử dụng kỹ thuật code first để thao tác với CSDL
    /// </summary>
    public class ExLoverShopDb : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ImageUrl> ImageUrls { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Spam> Spams { get; set; }
        public DbSet<WishList> WishLists { get; set; }
    }
}