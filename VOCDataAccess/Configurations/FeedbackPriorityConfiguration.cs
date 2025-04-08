using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VOCDataAccess.DTOs;

namespace VOCDataAccess.Configurations
{
    public class FeedbackPriorityConfiguration : IEntityTypeConfiguration<FeedbackPriorityDTO>
    {
        public void Configure(EntityTypeBuilder<FeedbackPriorityDTO> builder)
        {
            builder.ToTable("Table_Priorities");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.Name).HasColumnType("nvarchar(255)");
            builder.Property(s => s.Priority).HasColumnType("tinyint");
            builder.Property(s => s.Description).HasColumnType("nvarchar(500)");
            builder.Property(s => s.BackgroundStyle).HasColumnType("varchar(50)");
            builder.HasData(
                new FeedbackPriorityDTO
                {
                    Id = 1,
                    Name = "Thấp",
                    Priority = 1,
                    BackgroundStyle = "info"
                },
                new FeedbackPriorityDTO
                {
                    Id = 2,
                    Name = "Bình thường",
                    Priority = 2,
                    BackgroundStyle = "warning"
                },
                new FeedbackPriorityDTO
                {
                    Id = 3,
                    Name = "Cao",
                    Priority = 3,
                    BackgroundStyle = "danger"
                }
            );
        }
    }
}
