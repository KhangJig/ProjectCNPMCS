using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NYCshop.Models.Mapping
{
    public class ImageUrlMap : EntityTypeConfiguration<ImageUrl>
    {
        public ImageUrlMap()
        {
            // Primary Key
            this.HasKey(t => t.ImageID);

            // Properties
            this.Property(t => t.ImageID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Url)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ImageUrls");
            this.Property(t => t.ImageID).HasColumnName("ImageID");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.ProductID).HasColumnName("ProductID");

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ImageUrls)
                .HasForeignKey(d => d.ProductID);

        }
    }
}
