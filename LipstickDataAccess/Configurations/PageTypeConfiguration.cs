using LipstickDataAccess.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LipstickDataAccess.Configurations
{
    public class PageTypeConfiguration : IEntityTypeConfiguration<PageTypeDTO>
    {
        public void Configure(EntityTypeBuilder<PageTypeDTO> builder)
        {
            builder.ToTable("Table_PageTypes");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Label).IsRequired().HasMaxLength(100);
        }
    }
}
