using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NYCshop.Models.Mapping
{
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            // Primary Key
            this.HasKey(t => t.CommentID);

            // Properties
            this.Property(t => t.Username)
                .HasMaxLength(128);

            this.Property(t => t.CommentContent)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Comments");
            this.Property(t => t.CommentID).HasColumnName("CommentID");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.CommentContent).HasColumnName("CommentContent");
            this.Property(t => t.TimeComment).HasColumnName("TimeComment");

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.Comments)
                .HasForeignKey(d => d.ProductID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Comments)
                .HasForeignKey(d => d.Username);

        }
    }
}
