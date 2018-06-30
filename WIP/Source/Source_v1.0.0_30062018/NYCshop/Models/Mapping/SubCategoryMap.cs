using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NYCshop.Models.Mapping
{
    public class SubCategoryMap : EntityTypeConfiguration<SubCategory>
    {
        public SubCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.SubCategoryID);

            // Properties
            this.Property(t => t.SubCategoryName)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SubCategories");
            this.Property(t => t.SubCategoryID).HasColumnName("SubCategoryID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.SubCategoryName).HasColumnName("SubCategoryName");

            // Relationships
            this.HasRequired(t => t.Category)
                .WithMany(t => t.SubCategories)
                .HasForeignKey(d => d.CategoryID);

        }
    }
}
