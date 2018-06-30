using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NYCshop.Models.Mapping
{
    public class ErrorLogMap : EntityTypeConfiguration<ErrorLog>
    {
        public ErrorLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ErrorID);

            // Properties
            this.Property(t => t.ErrorContent)
                .IsRequired();

            this.Property(t => t.FunctionName)
                .IsRequired();

            this.Property(t => t.Username)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("ErrorLogs");
            this.Property(t => t.ErrorID).HasColumnName("ErrorID");
            this.Property(t => t.ErrorContent).HasColumnName("ErrorContent");
            this.Property(t => t.FunctionName).HasColumnName("FunctionName");
            this.Property(t => t.OccurDate).HasColumnName("OccurDate");
            this.Property(t => t.Username).HasColumnName("Username");

            // Relationships
            this.HasOptional(t => t.User)
                .WithMany(t => t.ErrorLogs)
                .HasForeignKey(d => d.Username);

        }
    }
}
