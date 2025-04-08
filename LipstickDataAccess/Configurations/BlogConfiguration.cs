using LipstickDataAccess.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LipstickDataAccess.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<BlogDTO>
    {
        public void Configure(EntityTypeBuilder<BlogDTO> builder)
        {
            builder.ToTable("Table_Blogs");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.SubjectEN).IsRequired();
            builder.Property(s => s.SubjectVN).IsRequired();
            builder.Property(s => s.ContentEN).IsRequired();
            builder.Property(s => s.ContentVN).IsRequired();
            builder.Property(s => s.Avatar).IsRequired();
        }
    }
}
