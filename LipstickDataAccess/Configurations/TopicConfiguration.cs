using LipstickDataAccess.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LipstickDataAccess.Configurations
{
    public class TopicConfiguration : IEntityTypeConfiguration<TopicDTO>
    {
        public void Configure(EntityTypeBuilder<TopicDTO> builder)
        {
            builder.ToTable("Table_Topics");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.NameEN).IsRequired().HasMaxLength(225);
            builder.Property(s => s.NameVN).IsRequired().HasMaxLength(225);
            builder.Property(s => s.IsActive).HasDefaultValue(false);
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.Priority).HasDefaultValue(0);
        }
    }
}
