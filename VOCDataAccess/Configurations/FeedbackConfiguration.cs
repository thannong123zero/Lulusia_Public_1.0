using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VOCDataAccess.DTOs;

namespace VOCDataAccess.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<FeedbackDTO>
    {
        public void Configure(EntityTypeBuilder<FeedbackDTO> builder)
        {
            builder.ToTable("Table_Feedbacks");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.IsActive).HasDefaultValue(true);
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.Code).IsRequired();
            builder.Property(s => s.Code).HasColumnType("char(36)");
            builder.HasIndex(s => s.Code).IsUnique();
            builder.Property(s => s.StatusId).HasColumnType("tinyint");
            builder.Property(s => s.PriorityId).HasColumnType("tinyint");
            builder.Property(s => s.FullName).HasColumnType("nvarchar(150)");
            builder.Property(s => s.PhoneNumber).HasColumnType("varchar(10)");
            builder.Property(s => s.Email).HasColumnType("varchar(255)");
            builder.Property(s => s.Images).HasColumnType("nvarchar(255)");
            builder.Property(s => s.Title).HasColumnType("nvarchar(255)");
            builder.Property(s => s.CreatedBy).HasColumnType("varchar(100)");
            builder.Property(s => s.ModifiedBy).HasColumnType("varchar(100)");
        }
    }
}
