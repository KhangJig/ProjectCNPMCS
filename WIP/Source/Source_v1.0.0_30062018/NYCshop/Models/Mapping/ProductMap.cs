using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NYCshop.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductID);

            // Properties
            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Username)
                .HasMaxLength(128);

            this.Property(t => t.ProductName)
                .IsRequired();

            this.Property(t => t.Describe)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Products");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.SubCategoryID).HasColumnName("SubCategoryID");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.UploadDate).HasColumnName("UploadDate");
            this.Property(t => t.Describe).HasColumnName("Describe");
            this.Property(t => t.Quanlity).HasColumnName("Quanlity");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.SaleStatus).HasColumnName("SaleStatus");
            this.Property(t => t.Censor).HasColumnName("Censor");
            this.Property(t => t.Viewed).HasColumnName("Viewed");

            // Relationships
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.SubCategoryID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.Username);

        }
    }
}
