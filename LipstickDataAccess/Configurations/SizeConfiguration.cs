using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LipstickDataAccess.DTOs;

namespace LipstickDataAccess.Configurations
{
    public class SizeConfiguration : IEntityTypeConfiguration<SizeDTO>
    {
        public void Configure(EntityTypeBuilder<SizeDTO> builder)
        {
            builder.ToTable("Table_Sizes");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.NameEN).IsRequired().HasMaxLength(100);
            builder.Property(s => s.NameVN).IsRequired().HasMaxLength(100);
            builder.Property(s => s.IsActive).HasDefaultValue(false);
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
        }
    }
}