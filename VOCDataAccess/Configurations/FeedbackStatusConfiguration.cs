using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VOCDataAccess.DTOs;

namespace VOCDataAccess.Configurations
{
    public class FeedbackStatusConfiguration : IEntityTypeConfiguration<FeedbackStatusDTO>
    {
        public void Configure(EntityTypeBuilder<FeedbackStatusDTO> builder)
        {
            builder.ToTable("Table_Statuses");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.Name).HasColumnType("nvarchar(255)");
            builder.Property(s => s.Priority).HasColumnType("tinyint");
            builder.Property(s => s.Description).HasColumnType("nvarchar(500)");
            builder.Property(s => s.BackgroundStyle).HasColumnType("varchar(50)");
            builder.HasData(
                new FeedbackStatusDTO
                {
                    Id = 1,
                    Name = "Mới",
                    Priority = 1,
                    BackgroundStyle = "info"
                },
                new FeedbackStatusDTO
                {
                    Id = 2,
                    Name = "Đang xử lý",
                    Priority = 2,
                    BackgroundStyle = "warning"
                },
                new FeedbackStatusDTO
                {
                    Id = 3,
                    Name = "Đã phản hồi",
                    Priority = 3,
                    BackgroundStyle = "success"
                },
                new FeedbackStatusDTO
                {
                    Id = 4,
                    Name = "Đã hoàn thành",
                    Priority = 4,
                    BackgroundStyle = "success"
                },
                new FeedbackStatusDTO
                {
                    Id = 5,
                    Name = "Đã đóng",
                    Priority = 5,
                    BackgroundStyle = "success"
                },
                new FeedbackStatusDTO
                {
                    Id = 6,
                    Name = "Mở lại",
                    Priority = 6,
                    BackgroundStyle = "primary"
                }

            );
        }
    }
}
