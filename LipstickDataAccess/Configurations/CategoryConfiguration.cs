using LipstickDataAccess.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LipstickDataAccess.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryDTO>
    {
        public void Configure(EntityTypeBuilder<CategoryDTO> builder)
        {
            builder.ToTable("Table_Categories");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.NameEN).IsRequired().HasMaxLength(225);
            builder.Property(s => s.NameVN).IsRequired().HasMaxLength(225);
            builder.Property(s => s.IsActive).HasDefaultValue(true);
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.InNavbar).HasDefaultValue(false);
            builder.Property(s => s.Priority).HasDefaultValue(0);
        }
    }
}
