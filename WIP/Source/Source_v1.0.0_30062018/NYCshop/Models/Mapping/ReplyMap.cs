using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NYCshop.Models.Mapping
{
    public class ReplyMap : EntityTypeConfiguration<Reply>
    {
        public ReplyMap()
        {
            // Primary Key
            this.HasKey(t => t.ReplyID);

            // Properties
            this.Property(t => t.Username)
                .HasMaxLength(128);

            this.Property(t => t.ReplyContent)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Replies");
            this.Property(t => t.ReplyID).HasColumnName("ReplyID");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.CommentID).HasColumnName("CommentID");
            this.Property(t => t.ReplyContent).HasColumnName("ReplyContent");
            this.Property(t => t.TimeReply).HasColumnName("TimeReply");

            // Relationships
            this.HasRequired(t => t.Comment)
                .WithMany(t => t.Replies)
                .HasForeignKey(d => d.CommentID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Replies)
                .HasForeignKey(d => d.Username);

        }
    }
}
