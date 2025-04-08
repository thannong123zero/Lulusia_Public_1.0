using LipstickDataAccess.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LipstickDataAccess.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<BrandDTO>
    {
        public void Configure(EntityTypeBuilder<BrandDTO> builder)
        {
            builder.ToTable("Table_Brands");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.Name).IsRequired().HasMaxLength(225);
        }
    }
}
