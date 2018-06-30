using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NYCshop.Models.Mapping
{
    public class SpamMap : EntityTypeConfiguration<Spam>
    {
        public SpamMap()
        {
            // Primary Key
            this.HasKey(t => t.SpamID);

            // Properties
            this.Property(t => t.Reporter)
                .HasMaxLength(128);

            this.Property(t => t.SpamContent)
                .IsRequired();

            this.Property(t => t.Resolver)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Spams");
            this.Property(t => t.SpamID).HasColumnName("SpamID");
            this.Property(t => t.Reporter).HasColumnName("Reporter");
            this.Property(t => t.SpamContent).HasColumnName("SpamContent");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ReportDate).HasColumnName("ReportDate");
            this.Property(t => t.Resolved).HasColumnName("Resolved");
            this.Property(t => t.ResolveDate).HasColumnName("ResolveDate");
            this.Property(t => t.Resolver).HasColumnName("Resolver");
            this.Property(t => t.ProperReport).HasColumnName("ProperReport");

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.Spams)
                .HasForeignKey(d => d.ProductID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Spams)
                .HasForeignKey(d => d.Reporter);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.Spams1)
                .HasForeignKey(d => d.Resolver);

        }
    }
}
