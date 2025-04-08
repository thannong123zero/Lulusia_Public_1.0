using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VOCDataAccess.DTOs;

namespace VOCDataAccess.Configurations
{
    public class ForwardFeedbackConfiguration : IEntityTypeConfiguration<ForwardFeedbackDTO>
    {
        public void Configure(EntityTypeBuilder<ForwardFeedbackDTO> builder)
        {
            builder.ToTable("Table_ForwardFeedbacks");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
        }
    }
}
