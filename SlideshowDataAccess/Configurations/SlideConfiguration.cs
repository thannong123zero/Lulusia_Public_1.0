using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SlideshowDataAccess.DTOs;

namespace SlideshowDataAccess.Configurations
{
    public class SlideConfiguration : IEntityTypeConfiguration<SlideDTO>
    {
        public void Configure(EntityTypeBuilder<SlideDTO> builder)
        {
            builder.ToTable("Table_Slides");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.HasOne(s => s.SlideTheme).WithMany(ts => ts.Slides).HasForeignKey(s => s.SlideThemeId);
        }
    }
}
