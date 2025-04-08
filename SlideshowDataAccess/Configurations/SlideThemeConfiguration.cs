using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SlideshowDataAccess.DTOs;

namespace SlideshowDataAccess.Configurations
{
    public class SlideThemeConfiguration : IEntityTypeConfiguration<SlideThemeDTO>
    {
        public void Configure(EntityTypeBuilder<SlideThemeDTO> builder)
        {
            builder.ToTable("Table_SlideThemes");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
        }
    }
}
