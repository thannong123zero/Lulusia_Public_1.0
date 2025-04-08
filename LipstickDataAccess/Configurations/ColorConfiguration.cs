using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LipstickDataAccess.DTOs;

namespace LipstickDataAccess.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<ColorDTO>
    {
        public void Configure(EntityTypeBuilder<ColorDTO> builder)
        {
            builder.ToTable("Table_Colors");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.NameEN).IsRequired().HasMaxLength(100);
            builder.Property(s => s.NameVN).IsRequired().HasMaxLength(100);
            builder.Property(s => s.IsActive).HasDefaultValue(false);
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
        }
    }
}