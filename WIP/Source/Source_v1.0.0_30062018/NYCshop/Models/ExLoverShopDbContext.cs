using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using NYCshop.Models.Mapping;

namespace NYCshop.Models
{
    public partial class ExLoverShopDbContext : DbContext
    {
        static ExLoverShopDbContext()
        {
            Database.SetInitializer<ExLoverShopDbContext>(null);
        }

        public ExLoverShopDbContext()
            : base("Name=ExLoverShopDbContext")
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<ImageUrl> ImageUrls { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Spam> Spams { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new ErrorLogMap());
            modelBuilder.Configurations.Add(new ImageUrlMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ReplyMap());
            modelBuilder.Configurations.Add(new SpamMap());
            modelBuilder.Configurations.Add(new SubCategoryMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
